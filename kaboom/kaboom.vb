Imports CupCake
Imports CupCake.Players
Imports CupCake.Core
Imports CupCake.Messages.Blocks
Imports System.Timers
Imports CupCake.Room
Imports CupCake.Messages.User
Imports CupCake.Upload
Imports CupCake.Core.Events
Imports System.ComponentModel.Composition.Hosting
Imports System.Threading
Imports CupCake.World

Public Class kaboom
    Inherits CupCakeMuffin(Of kaboom)

    Public WorldID As String = "PWmSQ3dUeya0I"

    Public ArenaList As New List(Of kaboomArena)
    Public WithEvents Wait As New System.Timers.Timer
    Public WaitingList As New List(Of Player)
    Public Writer As kaboomWriter

    Public Enabled As Boolean = False

    Protected Overloads Overrides Sub Enable()
        Events.Bind(Of JoinCompleteRoomEvent)(AddressOf JoinComplete)
        Events.Bind(Of MovePlayerEvent)(AddressOf MovePlayer)
    End Sub
    Private Sub JoinComplete(ByVal sender As Object, ByVal e As JoinCompleteRoomEvent)
        'ENABLE A WHOLE BUNCH OF SH**
        Chatter.Name = "KABOM!"
        Chatter.Chat("ENABLED.")
        Chatter.LoadLevel()
        EnableUploaders()
        Wait = GetTimer(50)
        Wait.Start()
        Enabled = True
    End Sub
    Private Sub MovePlayer(ByVal sender As Object, ByVal e As MovePlayerEvent)
        If Enabled Then
            Dim pos As Point = New Point(e.InnerEvent.BlockX, e.InnerEvent.BlockY)
            Dim modi As Point = New Point(e.InnerEvent.ModifierX, e.InnerEvent.ModifierY)
            'LET USER THA GAME JOIN
            If e.InnerEvent.SpeedY = -52 Then
                If WorldService(Layer.Foreground, pos.X, pos.Y + 1).Block = Block.BrickDarkGreen Then
                    JoinAGame(e.Player)
                End If
            End If
            'GIVE USER PERSONAL STATS
            If WorldService(Layer.Background, pos.X, pos.Y).Block = Block.BgSpartaGreen Then
                Dim highscore As Integer = StoragePlatform.Get("kabom", e.Player.Username & "highscore")
                UploadService.UploadLabel(pos.X, pos.Y, LabelBlock.DecorationSign, e.Player.Username & "'s highscore: [" & highscore & "P]")
            End If
        End If
    End Sub
    Public Sub JoinAGame(player As Player, Optional disableTP As Boolean = False)
        'TRY TO FIND A ARENA, IF THE USER ISN'T ALREADAY IN ONE
        Dim Found As Boolean = False
        For Each Arena In ArenaList
            If Not IsNothing(Arena.Player) Then
                If Arena.Player.Username = player.Username Then
                    Exit Sub
                End If
            End If
        Next
        'TRY TO FIND A OPEN ARENA
        For Each Arena In ArenaList
            If Arena.ArenaTimer.Enabled = False Then
                'JOIN ARENAA
                Arena.JoinArena(player)
                Found = True
                'REMOVE IN WAITINGLISTS
                For Each PlayerW In WaitingList
                    If player.Username = PlayerW.Username Then
                        WaitingList.Remove(PlayerW)
                    End If
                Next
                Exit Sub
            End If
        Next
        'ADD IN WAITINGLISTS IF FOUND
        If Not Found Then
            WaitingList.Add(player)
            If Not disableTP Then
                Chatter.Teleport(player.Username, 23, 79)
            End If
        End If
    End Sub
    Private Sub WaitTick() Handles Wait.Elapsed
        'UPDATE HIGHSCORESIGN JUST BCUZ
        Dim highscore As Integer = StoragePlatform.Get("kabom", "highscore")
        Dim highscorePlayer As String = StoragePlatform.Get("kabom", "highscorePlayer")
        UploadService.UploadLabel(24, 79, LabelBlock.DecorationSign, "highscore: [" & highscore & "P] by " & highscorePlayer)

        'IF THERE IS A FREE ARENA
        For Each Arena In ArenaList
            If Arena.ArenaTimer.Enabled = False Then
                For Each Player In WaitingList
                    If Not PlayerService.Players.Contains(Player) Then
                        WaitingList.Remove(Player)
                    Else
                        'JOIN AND REMOVE PLAYER
                        JoinAGame(Player, True)
                        For Each PlayerW In WaitingList
                            If Player.Username = PlayerW.Username Then
                                WaitingList.Remove(PlayerW)
                            End If
                        Next
                        Exit Sub
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub AddWriter(world As String, email As String, password As String)
        'CREATE A CLIENT, CONNECTION, CUPCAKE CLIENT
        Dim client = PlayerIOClient.PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password)
        Dim connection = client.Multiplayer.JoinRoom(world, Nothing)
        Dim cupcake = New CupCake.Host.CupCakeClient()
        cupcake.AggregateCatalog.Catalogs.Add(New TypeCatalog(GetType(kaboomWriter)))
        cupcake.Start(connection)
        Writer = cupcake.MuffinLoader.Get(Of kaboomWriter)()
    End Sub
    Private Sub EnableUploaders()
        'INIT UPLOADERS
        AddWriter(WorldID, "score1@trash-mail.com", "score")
        AddArena("kabom1", WorldID, "kabom1@trash-mail.com", "kabom", New Rectangle(10, 10, 20, 11))
        'AddArena("kabom2", WorldID, "kabom2@trash-mail.com", "kabom", New Rectangle(40, 10, 20, 11))
        'AddArena("kabom3", WorldID, "kabom3@trash-mail.com", "kabom", New Rectangle(70, 10, 20, 11))
        'AddArena("kabom4", WorldID, "kabom4@trash-mail.com", "kabom", New Rectangle(100, 10, 20, 11))
        'AddArena("kabom5", WorldID, "kabom5@trash-mail.com", "kabom", New Rectangle(130, 10, 20, 11))
        ' AddArena("kabom6", WorldID, "kabom6@trash-mail.com", "kabom", New Rectangle(160, 10, 20, 11))
        'AddUploader("moon2", WorldID, "email", "pw",New Rectangle(10, 40, 20, 11))
        'AddUploader("moon3", WorldID, "email", "pw",New Rectangle(40, 40, 20, 11))
        'AddUploader("moon4", WorldID, "email", "pw",New Rectangle(70, 40, 20, 11))
        'AddUploader("tree4", WorldID, "email", "pw",New Rectangle(100, 40, 20, 11))
        'AddUploader("tree4", WorldID, "email", "pw",New Rectangle(130, 40, 20, 11))
        'AddUploader("tree4", WorldID, "email", "pw",New Rectangle(160, 40, 20, 11))
    End Sub
    Private Sub AddArena(id As String, world As String, email As String, password As String, arenaRectangle As Rectangle)
        'CREATE A CLIENT, CONNECTION, CUPCAKE CLIENT
        Dim client = PlayerIOClient.PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password)
        Dim connection = client.Multiplayer.JoinRoom(world, Nothing)
        Dim cupcake = New CupCake.Host.CupCakeClient()
        cupcake.AggregateCatalog.Catalogs.Add(New TypeCatalog(GetType(kaboomArena)))
        cupcake.Start(connection)
        ArenaList.Add(cupcake.MuffinLoader.Get(Of kaboomArena)().init(Chatter, arenaRectangle, Writer, StoragePlatform, UploadService))
    End Sub
End Class