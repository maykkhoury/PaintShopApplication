Public Class formComposed

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExit.Click
        Me.Close()
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1A.Click, Button2A.Click, Button3A.Click, Button1B.Click, Button2B.Click, Button3B.Click
        Dim but As Button = CType(sender, Button)
        Dim formulaId As Integer = but.AccessibleDescription
        If formulaId = -1 Or formulaId = -2 Then
            MsgBox("Color " & but.Text & " does not exist", MsgBoxStyle.Critical)
            Exit Sub
        End If

        edit.quantitiesWithAllDecimals = Nothing
        edit.quantitiesWithAllDecimalsCum = Nothing

        selectedFormulaColors = Nothing
        ratio = 1
        edit.hideRadioVariants()
        Dim startWithLitre As Boolean = False
        If chosenUnit.code_unit = "L" Then
            startWithLitre = True
        End If
        edit.generatesDetail(startWithLitre, formulaId, Me.AccessibleDescription)

        'edit.grpEdit.Visible = True
        Me.Visible = False
        edit.formulaDup = Nothing
        edit.ShowDialog()


    End Sub



    Private Sub formComposed_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        selectedFormulaColors = Nothing
        ratio = 1
        edit.hideRadioVariants()
        Dim startWithLitre As Boolean = False
        If chosenUnit.code_unit = "L" Then
            startWithLitre = True
        End If
    End Sub
End Class