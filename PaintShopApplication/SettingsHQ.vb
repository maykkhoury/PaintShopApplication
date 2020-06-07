Public Class SettingsHQ

    Private Sub settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setUnitsCombo()
        setCurrenciesCombo()
        setFormLabels()

    End Sub
    Private Sub setFormLabels()
        butDone.Text = getLabelDescription("butDone")
        grpSettings.Text = getLabelDescription("grpSettings")
        lbCurrency.Text = getLabelDescription("lbCurrency")
        lbUnit.Text = getLabelDescription("lbUnit")
        Me.Text = getLabelDescription("Settings")
    End Sub

    Private Sub setUnitsCombo()
        cmbUnit.Items.Clear()
        Dim i As Integer
        For i = 0 To units.Length - 1
            'get unit name
            Dim value As String = ""
            Dim unitLanguage As UnitLanguage() = units(i).unitLanguage
            Dim j As Integer
            For j = 0 To unitLanguage.Length - 1
                If unitLanguage(j).id_language = chosenLanguage.id_language Then
                    value = unitLanguage(j).name_unit.Trim
                    Exit For
                End If
            Next j
            '------------------------

            cmbUnit.Items.Add(value)

            If Not chosenUnit Is Nothing Then
                If units(i).id_unit = chosenUnit.id_unit Then
                    cmbUnit.SelectedIndex = i
                End If
            End If
        Next i
    End Sub

    Private Sub setCurrenciesCombo()
        cmbCurrency.Items.Clear()
        Dim i As Integer
        For i = 0 To currencies.Length - 1
            'get currency name
            Dim value As String = ""
            Dim currencyLanguage As CurrencyLanguage() = currencies(i).currencyLanguage
            Dim j As Integer
            For j = 0 To currencyLanguage.Length - 1
                If currencyLanguage(j).id_language = chosenLanguage.id_language Then
                    value = currencyLanguage(j).label.Trim
                    value = currencies(i).symbol.Trim & " - " & currencies(i).code_currency.Trim & " - " & value
                    Exit For
                End If
            Next j
            '------------------------

            cmbCurrency.Items.Add(value)

            If Not chosenCurrency Is Nothing Then
                If currencies(i).id_currency = chosenCurrency.id_currency Then
                    cmbCurrency.SelectedIndex = i
                End If
            End If
        Next i
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDone.Click

        If cmbUnit.SelectedIndex >= 0 Then
            chosenUnit = units(cmbUnit.SelectedIndex)
        End If
        If cmbCurrency.SelectedIndex >= 0 Then
            chosenCurrency = currencies(cmbCurrency.SelectedIndex)
        End If
        setVariables()
        edit.lbUnitMesureValue.Text = chosenUnit.getUnitName(chosenLanguage.id_language)
        edit.lbCurrencyDetailsValue.Text = chosenCurrency.code_currency

        'garageHome.addResultsToListviewFormula(curFormulas)
        If Not curSubFamily Is Nothing Then
            garageHome.addResultsToListviewFamily(curSubFamily, False)
            If Not selectedFormulaColors Is Nothing Then
                'edit.generatesDetails()
            End If

        End If

        Me.Close()

    End Sub
End Class