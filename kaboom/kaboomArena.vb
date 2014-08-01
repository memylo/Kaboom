Imports CupCake
Imports CupCake.Core.Events
Imports CupCake.Core
Imports CupCake.Players
Imports System.Timers
Imports CupCake.Messages.Blocks
Imports CupCake.Core.Storage
Imports CupCake.Upload
Imports EEPhysics
Imports CupCake.Messages.Receive

Public Class kaboomArena
    Inherits CupCakeMuffin

    'FU**ING MUCH VARIABLES Q:Q TOO LATE TO COMMENT ON EVERTHING..
    Private Random As New Random
    Private MainChatter As Chatter
    Private MainWriter As kaboomWriter
    Private MainStoragePlatform As StoragePlatform
    Private MainUploadService As UploadService
    Public WithEvents ArenaTimer As Timer
    Public WithEvents MoveTimer As Timer
    Private ArenaRectangle As Rectangle
    Public Player As New Player
    Public Highscore As Integer = 0
    Private Lifes As Integer = 4
    Private Score As Integer = 0
    Private Stopped As Boolean = False
    Private Paused As Boolean = False
    Private PauseTimeout As Integer
    Public BombPoint As Point
    Public PlatformPoint As Point
    Public Speed As Integer = 500
    Public BombChance As Integer = 15
    Private PhysicsWorld As New PhysicsWorld

    Protected Overloads Overrides Sub Enable()
        'ENABLE EVENTS AND TIMERS
        Events.Bind(Of ReceiveEvent)(AddressOf ReceiveEvent)
        ArenaTimer = GetTimer(Speed)
        MoveTimer = GetTimer(50)
    End Sub
    Private Sub ReceiveEvent(ByVal sender As Object, ByVal e As ReceiveEvent)
        'PHYSICS EVENT STUFF
        PhysicsWorld.HandleMessage(e.PlayerIOMessage)
    End Sub
    Public Function init(chatter As Chatter, arenaRectangle As Rectangle, writer As kaboomWriter, storagePlatform As StoragePlatform, uploadService As UploadService) As kaboomArena
        'A NEW BOT GOT ADDED, GIVE HIM ALL THE COOL VARIABLES..
        Me.Chatter.Name = "!"
        Me.MainChatter = chatter
        Me.MainWriter = writer
        Me.MainStoragePlatform = storagePlatform
        Me.MainUploadService = uploadService
        Me.ArenaRectangle = arenaRectangle
        BombPoint = New Point(arenaRectangle.X + arenaRectangle.Width / 2, arenaRectangle.Y)
        Events.Bind(Of MovePlayerEvent)(AddressOf MovePlayer)
        Return Me
    End Function

    Private Async Sub MovePlayer(ByVal sender As Object, ByVal e As MovePlayerEvent)
        IsAPlayerThere()
        'CHECK IF PLAYER IS WHERE HE SHOULD BE, DOESNT RLY WORK THOUGH
        If Not (e.InnerEvent.BlockX > ArenaRectangle.X - 2 And e.InnerEvent.BlockX < ArenaRectangle.X + ArenaRectangle.Width + 2 And
            e.InnerEvent.BlockY > ArenaRectangle.Y And e.InnerEvent.BlockY < ArenaRectangle.Y + ArenaRectangle.Height + 5) Then
            Exit Sub
        End If
        'THE WAIT FOR KEY / IDLE CHECK UNPAUSER
        If ArenaTimer.Enabled Then
            If Paused Then
                If e.Player.Username = Player.Username Then
                    Await UnPauseArena()
                End If
            End If
        End If
    End Sub
    Private Sub ArenaTick() Handles ArenaTimer.Elapsed
        'EACH TICK DO BLABLA
        If Not Stopped And Not Paused Then
            If Player.IsDisconnected Then ResetArena()
            UpdateSpeed()
            MoveBandit()
            DropBomb()
            MoveBomb()
        End If
    End Sub
    Private Sub UpdateSpeed()
        'MAKE IT HARDER OVER THE TIME..
        If Speed > 100 Then
            Speed -= 2
            ArenaTimer.Interval = Speed
        End If
    End Sub
    Private Sub MoveBandit()
        'MOVE THE MAD BANDIT
        Dim ModifierX = Random.Next(-1, 2)
        If ArenaRectangle.X < BombPoint.X + ModifierX And BombPoint.X + ModifierX < ArenaRectangle.X + ArenaRectangle.Width And
            Not ModifierX = 0 Then
            BombPoint.X += ModifierX
            UploadService.UploadBlock(BombPoint.X, BombPoint.Y, Block.Special2)
            For x As Integer = ArenaRectangle.X To ArenaRectangle.X + ArenaRectangle.Width
                If Not (x = BombPoint.X) Then
                    UploadService.UploadBlock(x, ArenaRectangle.Y, Block.GravityNothing)
                End If
            Next
        End If
    End Sub
    Private Sub DropBomb()
        'DROP NEW BOOOOMBS
        If Random.Next(0, BombChance) = 0 Then
            UploadService.UploadBlock(BombPoint.X, BombPoint.Y + 1, Block.BasicRed)
        End If
    End Sub
    Private Async Function MoveBomb() As Task
        'GET ALL RED BLOCKS
        Dim NeedToBeRemovedBombList As New List(Of Point)
        Dim CurrentBombList As New List(Of Point)
        For x As Integer = ArenaRectangle.X To ArenaRectangle.X + ArenaRectangle.Width
            For y As Integer = ArenaRectangle.Y To ArenaRectangle.Y + ArenaRectangle.Height
                If WorldService(Layer.Foreground, x, y).Block = Block.BasicRed Then
                    CurrentBombList.Add(New Point(x, y))
                End If
            Next
        Next
        'DELETE THE BLOCK; PLACE NEW ONE; DECIDE IF IT DID EXPLODE OR IF ITS CATCHD
        For Each Bomb In CurrentBombList
            UploadService.UploadBlock(Bomb.X, Bomb.Y, Block.GravityNothing)
            NeedToBeRemovedBombList.Add(New Point(Bomb.X, Bomb.Y))

            If WorldService(Layer.Foreground, Bomb.X, Bomb.Y + 1).Block = Block.BasicYellow Or
             WorldService(Layer.Foreground, Bomb.X, Bomb.Y).Block = Block.BasicYellow Then
                BombCatched()
            ElseIf Bomb.Y = ArenaRectangle.Height + ArenaRectangle.Y And Bomb.X >= PlatformPoint.X - 1 And Bomb.X <= PlatformPoint.X + 1 Then
                BombCatched()
            Else

                If Bomb.Y < ArenaRectangle.Y + ArenaRectangle.Height Then
                    UploadService.UploadBlock(Bomb.X, Bomb.Y + 1, Block.BasicRed)
                Else
                    Await BombExploded()
                End If
            End If
        Next
        For Each Bomb In NeedToBeRemovedBombList
            UploadService.UploadBlock(Bomb.X, Bomb.Y, Block.GravityNothing)
        Next
    End Function
    Private Async Sub MovePlatform() Handles MoveTimer.Elapsed
        If Not Stopped And Not Paused Then
            If Not IsAPlayerThere() Then Exit Sub
            Dim phyPlayer As PhysicsPlayer = PhysicsWorld.Players(Player.UserId)
            Dim pos As Point = New Point(phyPlayer.X / 16, phyPlayer.Y / 16)
            If pos.X > ArenaRectangle.X And pos.X < ArenaRectangle.X + ArenaRectangle.Width Then
                UploadService.UploadBlock(pos.X, ArenaRectangle.Y + ArenaRectangle.Height, Block.BasicYellow)
                PlatformPoint = New Point(pos.X, ArenaRectangle.Y + ArenaRectangle.Height)
                If pos.X - 1 > ArenaRectangle.X And pos.X < ArenaRectangle.X + ArenaRectangle.Width Then
                    UploadService.UploadBlock(pos.X - 1, ArenaRectangle.Y + ArenaRectangle.Height, Block.BasicYellow)
                End If
                If pos.X > ArenaRectangle.X And pos.X + 1 < ArenaRectangle.X + ArenaRectangle.Width Then
                    UploadService.UploadBlock(pos.X + 1, ArenaRectangle.Y + ArenaRectangle.Height, Block.BasicYellow)
                End If
                For x As Integer = ArenaRectangle.X To ArenaRectangle.X + ArenaRectangle.Width
                    If Not (x = pos.X Or x = pos.X + 1 Or x = pos.X - 1) Then
                        UploadService.UploadBlock(x, ArenaRectangle.Y + ArenaRectangle.Height, Block.GravityNothing)
                    End If
                Next
            End If
        ElseIf Paused Then
            PauseTimeout -= 1
            If PauseTimeout = 200 Then
                Chatter.Reply(Player.Username, "Show you are not afk (by moving) or you will get killed!")
            End If
            If PauseTimeout = 0 Then
                MainChatter.Kill(Player.Username)
                Await ResetArena()
            End If
        End If
    End Sub
    Private Async Function PauseArena() As Task
        Dim x As Integer = (ArenaRectangle.X + ArenaRectangle.Width / 2)
        Dim y As Integer = ArenaRectangle.Y + ArenaRectangle.Height + 2
        Stopped = True
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y - 1, Block.BasicRed)
        MainUploadService.UploadBlock(x + 1, y, 161)
        MainUploadService.UploadBlock(x - 1, y, 161)
        Await TeleportPlayer()
        Await WaitSomeTime(2000)
        Await RevertBlocks()
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y - 1, Block.BasicYellow)
        BombPoint.X = ArenaRectangle.X + ArenaRectangle.Width / 2
        UploadService.UploadBlock(BombPoint.X, BombPoint.Y, Block.Special2)
        MovePlatform()
        Paused = True
        PauseTimeout = 500
        Stopped = False
    End Function
    Private Async Function UnPauseArena() As Task
        Dim x As Integer = (ArenaRectangle.X + ArenaRectangle.Width / 2)
        Dim y As Integer = ArenaRectangle.Y + ArenaRectangle.Height + 2
        UploadService.UploadBlock(x, y, Block.GravityNothing)
        Await RevertBlocks()
        For i As Integer = 0 To 3
            UploadService.UploadBlock(x + 1, y, Block.GravityNothing)
            UploadService.UploadBlock(x - 1, y, Block.GravityNothing)
        Next
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y - 1, Block.BasicGreen)
        Paused = False
    End Function
    Private Async Function BombExploded() As Task
        'MAKE GAME STOP AND REVERT
        Lifes -= 1
        Select Case Lifes
            Case 3
                UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 1, Block.GravityNothing)
                Await PauseArena()
            Case 2
                UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 3, Block.GravityNothing)
                Await PauseArena()
            Case 1
                UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 5, Block.GravityNothing)
                Await PauseArena()
            Case 0
                If Player.IsDisconnected Then ResetArena()
                MainChatter.Kill(Player.Username)
                Dim playerHighscore As Integer = MainStoragePlatform.Get("kabom", Player.Username & "highscore")
                Dim highscore As Integer = MainStoragePlatform.Get("kabom", "highscore")
                Dim highscorePlayer As String = MainStoragePlatform.Get("kabom", "highscorePlayer")
                If Score > playerHighscore Then
                    MainStoragePlatform.Set("kabom", Player.Username & "highscore", Score)
                    Chatter.Chat(Player.Username & "'s new HS: [" & Score & "P].")
                End If
                If Score > highscore Then
                    MainStoragePlatform.Set("kabom", "highscore", Score)
                    MainStoragePlatform.Set("kabom", "highscorePlayer", Player.Username)
                    Chatter.Chat(Player.Username & " broke the HS! [" & Score & "P]!")
                End If
                MainUploadService.UploadLabel(24, 79, LabelBlock.DecorationSign, "highscore: [" & Score & "P] by " & Player.Username)
                ResetArena()
        End Select
    End Function
    Private Sub BombCatched()
        Score += 1
        Dim tempScore As Integer = 100000 + Score
        Dim spot1 As Integer = Val(tempScore.ToString.ToCharArray()(1))
        Dim spot2 As Integer = Val(tempScore.ToString.ToCharArray()(2))
        Dim spot3 As Integer = Val(tempScore.ToString.ToCharArray()(3))
        Dim spot4 As Integer = Val(tempScore.ToString.ToCharArray()(4))
        Dim spot5 As Integer = Val(tempScore.ToString.ToCharArray()(5))
        If Score = 1 Then
            MainWriter.PushScore(ArenaRectangle, spot1, spot2, spot3, spot4, spot5, True)
        Else
            MainWriter.PushScore(ArenaRectangle, spot1, spot2, spot3, spot4, spot5)
        End If
    End Sub

    Public Async Sub JoinArena(player As Player)
        Me.ArenaTimer.Start()
        Me.MoveTimer.Start()
        Me.Stopped = True
        Me.Player = player
        Await TeleportPlayer()
        RevertBlocks()
        Await PauseArena()
    End Sub
    Private Async Function TeleportPlayer() As Task
        Dim teleportTarget As Point = New Point((ArenaRectangle.X + ArenaRectangle.Width / 2) + 1, ArenaRectangle.Y + ArenaRectangle.Height + 3)
        Dim teleportAttempts As Integer = 0
        While Not (Player.BlockX = teleportTarget.X And Player.BlockY = teleportTarget.Y Or teleportAttempts = 3)
            If Not IsAPlayerThere() Then Exit Function
            teleportAttempts += 1
            MainChatter.Teleport(Player.Username, teleportTarget.X, teleportTarget.Y)
            Await WaitSomeTime(500)
        End While
    End Function
    Private Async Function ResetArena() As Task
        'RESET BLOCKS
        MainWriter.PushScore(ArenaRectangle, 20, 21, 22, 0, 24)
        Me.MoveTimer.Stop()
        Me.Stopped = False
        Me.Paused = False
        Await Me.RevertBlocks()
        Me.ArenaTimer.Stop()
        Me.Lifes = 4
        Me.Score = 0
        Me.Speed = 500
        Me.Player = PlayerService.OwnPlayer
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 1, Block.BasicYellow)
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 3, Block.BasicYellow)
        UploadService.UploadBlock(ArenaRectangle.X - 2, ArenaRectangle.Y + 5, Block.BasicYellow)
    End Function
    Private Async Function RevertBlocks() As Task
        For i As Integer = 0 To 5
            For x As Integer = ArenaRectangle.X To ArenaRectangle.X + ArenaRectangle.Width
                For y As Integer = ArenaRectangle.Y To ArenaRectangle.Y + ArenaRectangle.Height
                    UploadService.UploadBlock(x, y, Block.GravityNothing)
                    UploadService.UploadBlock(x, y, Block.GravityNothing)
                Next
            Next
            Await WaitSomeTime(200)
        Next
    End Function

    Public Function IsAPlayerThere() As Boolean
        If IsNothing(Player) Then
            Return False
        End If
        If Player.IsDisconnected Then
            ResetArena()
            Return False
        End If
        Return True
    End Function
    Public Async Function WaitSomeTime(time As Integer) As Task
        Await Task.Delay(time)
        IsAPlayerThere()
    End Function
End Class
