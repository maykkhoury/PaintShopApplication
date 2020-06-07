Imports Microsoft.Office.Interop.Excel
Imports System.Data.DataTable
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Threading


Public Class importPrices

    Private readExcelhread As Thread
    Private startRead As Boolean = False
    Private iPrg As Integer = 0

    Private logFile As File
    Private objWriter As System.IO.StreamWriter

    Private Sub butBrowse_Click(sender As Object, e As EventArgs) Handles butBrowse.Click
        doBrowse()
    End Sub

    Private Sub txtExcelFile_TextChanged(sender As Object, e As EventArgs) Handles txtExcelFile.DoubleClick
        doBrowse()
    End Sub
    Private Sub doBrowse()
        'msgbox("*******doBrowse")
        fileDialogPrices.InitialDirectory = My.Application.Info.DirectoryPath
        fileDialogPrices.Multiselect = False
        fileDialogPrices.Filter = "Excel Worksheets 2003(*.xls)|*.xls|Excel Worksheets 2007(*.xlsx)|*.xlsx"
        If fileDialogPrices.ShowDialog() = DialogResult.OK Then
            txtExcelFile.Text = fileDialogPrices.FileName
            'msgbox("*******doBrowse:" & fileDialogPrices.FileName)
        Else
            Exit Sub
        End If
    End Sub
    Private Sub readExcelFileHelper()
        Try
            'msgbox("*********readExcelFile readExcelFileHelper inside")
            If Me.InvokeRequired Then
                'msgbox("*********readExcelFile readExcelFileHelper invoke")
                Me.Invoke(New MethodInvoker(AddressOf readExcelFileHelper))
                Return
            End If
            'msgbox("*********readExcelFile readExcelFileHelper invoked")
            lbLoading.Visible = True
            butUpdate.Enabled = False
            butBrowse.Enabled = False
            txtExcelFile.Enabled = False
            prgBarImport.Visible = True
            Dim excelFilePath As String = txtExcelFile.Text
            Dim uniqueFileName As String = My.Application.Info.DirectoryPath & "//excelFileImport" & Now.TimeOfDay.ToString.Replace(":", "").Replace(".", "") & ".log"
            'msgbox("*********readExcelFile readExcelFileHelper uniqueFileName=" & uniqueFileName)
            Dim myExcelFile As StreamWriter = New StreamWriter(uniqueFileName)
            'msgbox("*********readExcelFile readExcelFileHelper StreamWriter=" & uniqueFileName)
            Dim unitsTab() As Unit = getUnits()
            'msgbox("*********readExcelFile readExcelFileHelper got units")
            Dim id_unitLitre As Integer
            For i = 0 To unitsTab.Length - 1
                If unitsTab(i).code_unit.Trim = "L" Then
                    id_unitLitre = unitsTab(i).id_unit
                End If
            Next
            'msgbox("*********readExcelFile readExcelFileHelper looped units")
            Dim obook As Excel.Workbook
            'msgbox("*********readExcelFile readExcelFileHelper obook")
            Dim oapp As Excel.Application
            oapp = New Excel.Application
            obook = oapp.Workbooks.Open(excelFilePath)
            'msgbox("*********readExcelFile readExcelFileHelper obook.open")
            Dim errorMsgs As String = ""

            'counting rows
            'msgbox("*********readExcelFile readExcelFileHelper counting starting")
            Dim count As Integer = 0
            For i = 2 To obook.ActiveSheet.rows.count
                Dim colorName As String = obook.ActiveSheet.Cells(i, 1).Value()
                If colorName Is Nothing Then
                    Exit For
                End If
                count += 1
            Next
            'msgbox("*********readExcelFile readExcelFileHelper count=" & count)
            prgBarImport.Maximum = count
            prgBarImport.Step = 1
            For Me.iPrg = 2 To obook.ActiveSheet.rows.count
                Dim colorName As String = obook.ActiveSheet.Cells(iPrg, 1).Value()
                'msgbox("******************************readExcelFile readExcelFileHelper Me.iPrg=" & Me.iPrg)
                'msgbox("******************************readExcelFile readExcelFileHelper colorName=" & colorName)
                If colorName Is Nothing Then
                    'msgbox("******************************readExcelFile readExcelFileHelper exit ")
                    Exit For
                End If
                prgBarImport.PerformStep()
                'msgbox("******************************readExcelFile readExcelFileHelper stepped")
                Dim errorFound As Boolean = False
                'check if code exists
                Dim thisColorDB As Color()
                'msgbox("******************************readExcelFile readExcelFileHelper showAlternativeName:" & chosenGarage.showAlternativeName)

                Dim encColorName As String = encryptData(colorName.Trim, password & chosenGarage.id_garage)
                If chosenGarage.showAlternativeName Then
                    Dim whereClause As String = "WHERE (name_color_alternative)='" & encColorName & "'"
                    whereClause &= " or ((name_color_alternative is null or name_color_alternative='') and (name_color)='" & encColorName & "')"
                    'msgbox("******************************readExcelFile readExcelFileHelper :" & whereClause)
                    thisColorDB = getColors(whereClause)
                Else
                    Dim whereClause As String = "WHERE (name_color)='" & encColorName & "'"
                    'msgbox("******************************readExcelFile readExcelFileHelper :" & whereClause)
                    thisColorDB = getColors(whereClause)
                End If
                'msgbox("******************************readExcelFile readExcelFileHelper got color")
                Dim thisColor As Color = Nothing
                If thisColorDB IsNot Nothing Then
                    If thisColorDB.Length > 0 Then
                        thisColor = thisColorDB(0)
                    End If
                End If
                If thisColor Is Nothing Then
                    errorFound = True
                    errorMsgs &= "Error at row " & iPrg & " - Color " & colorName & " doesn't exist" & vbNewLine
                End If
                '--------------------

                Dim unit As String = obook.ActiveSheet.Cells(iPrg, 2).Value()
                'msgbox("******************************readExcelFile readExcelFileHelper excel unit:" & unit)
                'check if unit exists
                Dim thisUnitDB As Unit = getUnitByCode(unit)

                If thisUnitDB Is Nothing Then
                    errorFound = True
                    errorMsgs &= "Error at row " & iPrg & " - Unit " & unit & " doesn't exist" & vbNewLine
                End If
                'msgbox("******************************readExcelFile readExcelFileHelper got unit:")
                '--------------------


                Dim price As String = obook.ActiveSheet.Cells(iPrg, 3).Value()
                'msgbox("******************************readExcelFile readExcelFileHelper got price:" & price)
                If Not IsNumeric(price) Then
                    errorMsgs &= "Error at row " & iPrg & " - Price " & price & " is not numeric" & vbNewLine
                End If

                If Not errorFound Then
                    'update color garage price
                    Dim garagePriceDB() As GaragePrice = getGaragePricesDB(" WHERE id_color=" & thisColor.id_color)
                    Dim garagePrice As GaragePrice = Nothing
                    If garagePriceDB IsNot Nothing Then
                        If garagePriceDB.Length > 0 Then
                            garagePrice = garagePriceDB(0)
                        End If
                    End If

                    If garagePrice IsNot Nothing Then
                        'update garage Price
                        If Not updateGarageColor(thisColor.id_color, price / (thisUnitDB.rateToLitre * 1000), chosenCurrency.id_currency, id_unitLitre) Then
                            errorMsgs &= "Error at row " & iPrg & " - Problem update new garage price with the folowing info: " & colorName & "-" & unit & "-" & price & vbNewLine
                        End If
                    Else
                        If Not insertIntoGaragePrice(thisColor.id_color, price / (thisUnitDB.rateToLitre * 1000), chosenCurrency.id_currency, id_unitLitre) Then
                            errorMsgs &= "Error at row " & iPrg & " - Problem inserting new garage price with the folowing info: " & colorName & "-" & unit & "-" & price & vbNewLine
                        End If
                    End If

                End If

            Next
            If errorMsgs.Length > 0 Then
                myExcelFile.Write(errorMsgs)
                myExcelFile.Close()
                'msgbox("ERROR: Please check the log file", 'msgboxStyle.Exclamation)
                Process.Start(uniqueFileName)
                Me.Dispose()
            Else
                myExcelFile.Close()
                File.Delete(uniqueFileName)
                'msgbox("Data successfully imported", 'msgboxStyle.Information)
                Me.Dispose()
            End If


            lbLoading.Visible = False
            butUpdate.Enabled = True
            butBrowse.Enabled = True
            txtExcelFile.Enabled = True
            prgBarImport.Value = 0
            prgBarImport.Visible = False
            obook.Close()
        Catch ex As Exception
            'msgbox("error is:" & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

   


   

    Private Sub butUpdate_Click(sender As Object, e As EventArgs) Handles butUpdate.Click
        'msgbox("*********butUpdate_Click clicked")
        If txtExcelFile.Text Is Nothing Then
            Return
        ElseIf txtExcelFile.Text = "" Then
            Return
        End If
        'msgbox("*********butUpdate_Click readExcelFileHelperNoPrg")
       
        'msgbox("*********butUpdate_Click " & startRead)
        startRead = True

        'msgbox("*********butUpdate_Click after " & startRead)
    End Sub


    Private Sub importPrices_Load(sender As Object, e As EventArgs) Handles Me.Load
        'File.Create(My.Application.Info.DirectoryPath & "//debugConsoleOut.out")
        If Not File.Exists(My.Application.Info.DirectoryPath & "//debugConsoleOut.out") Then
            Dim fileStre As FileStream = File.Create(My.Application.Info.DirectoryPath & "//debugConsoleOut.out")
            fileStre.Close()
        End If

        Try
            objWriter = New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "\\debugConsoleOut.out", True)
            'msgbox("*******file visited")
        Catch ex As Exception
            'msgbox(ex.Message)
        End Try

        'msgbox("*******starting thread")

        readExcelhread = New Thread(AddressOf readExcelFile)
        readExcelhread.IsBackground = True
        readExcelhread.Start()
        ''msgbox("*******thread started visited")

    End Sub


    Private Sub readExcelFile()
        If objWriter IsNot Nothing Then
            'msgbox("*********readExcelFile started " & startRead)
        End If

        Do
            ''msgbox("*********readExcelFile doing started " & startRead)
            Thread.Sleep(1000)
            If startRead Then
                'msgbox("*********readExcelFile readExcelFileHelper called")
                Try
                    readExcelFileHelper()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                'readExcelFileHelper()
                Exit Do
            End If
        Loop While 1 = 1

    End Sub

End Class