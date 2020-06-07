Public Class Form1

    Dim i As Integer
    Dim i2 As Integer

    Dim thread As System.Threading.Thread
    Dim thread2 As System.Threading.Thread

    Private Sub countup()
        Do Until i = 10000
            i = i + 1
            Label1.Text = i
            Me.Refresh()
        Loop
    End Sub

    Private Sub countup2()
        Do Until i2 = 10000
            i2 = i2 + 1
            Label2.Text = i2
            Me.Refresh()
        Loop
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        thread = New System.Threading.Thread(AddressOf countup)
        thread.Start()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        thread2 = New System.Threading.Thread(AddressOf countup2)
        thread2.Start()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class