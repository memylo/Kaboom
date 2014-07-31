Imports CupCake
Imports CupCake.Core
Imports CupCake.Messages.Blocks
Imports System.Threading
Public Class Params
    Public rect As Rectangle
    Public spot1 As Integer
    Public spot2 As Integer
    Public spot3 As Integer
    Public spot4 As Integer
    Public spot5 As Integer
    Public del As Boolean = False
    Public Sub New(rect As Rectangle, spot1 As Integer, spot2 As Integer, spot3 As Integer, spot4 As Integer, spot5 As Integer, Optional del As Boolean = False)
        Me.rect = rect
        Me.spot1 = spot1
        Me.spot2 = spot2
        Me.spot3 = spot3
        Me.spot4 = spot4
        Me.spot5 = spot5
        Me.del = del
    End Sub
End Class
Public Class kaboomWriter
    Inherits CupCakeMuffin
    Public Sub PushScore(rect As Rectangle, spot1 As Integer, spot2 As Integer, spot3 As Integer, spot4 As Integer, spot5 As Integer, Optional del As Boolean = False)
        Dim p As New Params(rect, spot1, spot2, spot3, spot4, spot5, del)
        Dim start As New ParameterizedThreadStart(AddressOf WriteScore)
        Dim einheit As New Thread(start)
        einheit.Start(p)
    End Sub
    Public Sub WriteScore(params As Params)
        Dim x As Integer = params.rect.X + 1
        Dim y As Integer = params.rect.Y + params.rect.Height
        PrintSpot(x, y, params.spot1)
        PrintSpot(x + 4, y, params.spot2)
        PrintSpot(x + 8, y, params.spot3)
        PrintSpot(x + 12, y, params.spot4)
        If params.del = True Then
            PrintSpot(x + 16, y, params.spot5, True)
        Else
            PrintSpot(x + 16, y, params.spot5)
        End If
    End Sub
    Private Sub PrintSpot(x As Integer, y As Integer, Number As Integer, Optional del As Boolean = False)
        If del Then
            UploadService.UploadBlock(x + 2, y + 4, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 4, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 5, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 5, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 6, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 6, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 7, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 7, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 8, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 8, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 9, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 9, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 2, y + 10, Block.SpecialNormalBlack)
            UploadService.UploadBlock(x + 3, y + 10, Block.SpecialNormalBlack)
        End If
        Select Case Number
            Case 0
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 1
                UploadService.UploadBlock(x - 1, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 2
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 3
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 4
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 5
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 6
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 7
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 8
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 9
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 10
                UploadService.UploadBlock(x - 1, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 4, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.SpecialNormalBlack)
            Case 20
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 8, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 21
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 22
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 7, Block.SpecialNormalBlack)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
            Case 24
                UploadService.UploadBlock(x - 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 1, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 4, Block.GravityNothing)
                UploadService.UploadBlock(x + 3, y + 4, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 5, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 5, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 5, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 6, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 6, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 6, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 7, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 7, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 7, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 8, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 8, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 8, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 9, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 9, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 9, Block.GravityNothing)

                UploadService.UploadBlock(x - 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 1, y + 10, Block.GravityNothing)
                UploadService.UploadBlock(x + 2, y + 10, Block.SpecialNormalBlack)
                UploadService.UploadBlock(x + 3, y + 10, Block.GravityNothing)
        End Select
    End Sub

    Protected Overloads Overrides Sub Enable()

    End Sub
End Class
