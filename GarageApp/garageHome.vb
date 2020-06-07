
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text


Public Class garageHome

    Private cont As Boolean = False, start As Long, pgnum As Long = 1
    Public clientNameColumnIndex As Integer = 0

    Private Sub menExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menExit.Click
        Me.Close()
    End Sub

    Private Sub menUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menUpdate.Click
        updateForm.ShowDialog()
    End Sub

    Private Sub menEditColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menEditColor.Click
        colorPrice.ShowDialog()
    End Sub

    'formula car
    Public Sub loadClients(ByVal carTableName As String, ByVal carName As String, ByVal idCar As Long)
        Dim clientsTab() As String = getClients("clientFormula", carName, idCar)
        Dim i As Integer
        cmbClientName.Items.Clear()
        cmbClientName.Items.Add("")
        If Not clientsTab Is Nothing Then
            For i = 0 To clientsTab.Length - 1
                cmbClientName.Items.Add(clientsTab(i))
            Next
        End If
        cmbClientName.Text = ""
    End Sub
    Private Sub setListViewLabel()
        cctype.Text = getLabelDescription("cctype")
        'ccarname.Text = getLabelDescription("ccarname")
        ccode.Text = getLabelDescription("ccode")
        ccname.Text = getLabelDescription("cname")
        cyear.Text = getLabelDescription("cyear")
        cversion.Text = getLabelDescription("cversion")
        ccardNbr.Text = getLabelDescription("ccardNbr")
        cclient.Text = getLabelDescription("cclient")

    End Sub

    Private Sub setFormLabels()
        setListViewLabel()
        'ccvariant.Text = getLabelDescription("ccvariant")
        Dim versiontxt As String = ""
        Try
            versiontxt = File.ReadAllText(My.Application.Info.DirectoryPath & "//vers.txt", Encoding.UTF8)
        Catch ex As Exception

        End Try

        Me.Text = getLabelDescription("garageHome") & " V " & versiontxt
    End Sub
    Public Sub loadGarageHome()
        lbUnitMesureValue.Text = chosenUnit.getUnitName(chosenLanguage.id_language)
        lbCurrencyDetailsValue.Text = chosenCurrency.code_currency


    End Sub

    Private Sub garageHome_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        login.Close()
    End Sub
    Private Sub garageHome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim newFontColors As Font = New Font(edit.txtColorsDetailHidden.Font.FontFamily, getFontSize(0))
        'edit.txtQuantityDetailHidden.Font = newFontColors
        'edit.txtColorsDetailHidden.Font = newFontColors
        'edit.pctColorDetailHidden.Size = New Size(edit.pctColorDetailHidden.Width, getFontSize(1))


        rd2k_LS.Text = chosenGarage.coat
        setFormLabels()

        loadGarageHome()
        'setChkBackColor()

        'cmbCarNameSearch.Items.Add("")
        Dim carsTab() As Car = getCars("")
        Dim lastChosenCar As Car = getLastChosenCar()
        Dim i As Integer
        For i = 0 To carsTab.Length - 1
            cmbCarNameSearch.Items.Add(carsTab(i).carName)

        Next

        cmbCarNameSearch.Text = carsTab(0).carName





        txtColorCode.Focus()

        Me.CheckForIllegalCrossThreadCalls = False
    End Sub

    Public Sub addResultsToListviewFamily(ByVal formulaTab() As Formula, ByVal resetCurFamily As Boolean, Optional ByVal typeFilter As String = "")
        Try
            Dim i As Integer = 0

            If resetCurFamily Then
                curFamily = formulaTab
                'curSubFamily = formulaTab
            End If

            If typeFilter.Trim <> "" Then
                Dim MyArray As New ArrayList
                For i = 0 To formulaTab.Length - 1
                    If formulaTab(i).type.ToLower.Trim = typeFilter.ToLower.Trim Then
                        MyArray.Add(formulaTab(i))
                    End If
                Next
                formulaTab = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
            End If


            lsvFamily.Items.Clear()
            lsvFamily.BeginUpdate()
            lsvFamily.Sorting = SortOrder.None
            lsvFamily.ListViewItemSorter = Nothing
            setListViewLabel()
            Dim startTime As DateTime
            Dim endTime As DateTime

            startTime = Now
            Dim MyArrayCouche As New ArrayList

            For i = 0 To formulaTab.Length - 1
                If isCouche(formulaTab(i).name_formula) And Not formulaTab(i).isMoved Then
                    Dim coucheIndex As Integer = getCoucheIndex(formulaTab, formulaTab(i).name_formula, "(couche)")
                    Dim couche1Index As Integer = getCoucheIndex(formulaTab, formulaTab(i).name_formula, "(couche 1)")
                    Dim couche2Index As Integer = getCoucheIndex(formulaTab, formulaTab(i).name_formula, "(couche 2)")
                    Dim sousCoucheIndex As Integer = getCoucheIndex(formulaTab, formulaTab(i).name_formula, "(sous-couche)")

                    If sousCoucheIndex <> -1 Then
                        formulaTab(sousCoucheIndex).isMoved = True
                        MyArrayCouche.Add(formulaTab(sousCoucheIndex))
                    End If
                    If coucheIndex <> -1 Then
                        formulaTab(coucheIndex).isMoved = True
                        MyArrayCouche.Add(formulaTab(coucheIndex))
                    End If
                    If couche1Index <> -1 Then
                        formulaTab(couche1Index).isMoved = True
                        MyArrayCouche.Add(formulaTab(couche1Index))
                    End If
                    If couche2Index <> -1 Then
                        formulaTab(couche2Index).isMoved = True
                        MyArrayCouche.Add(formulaTab(couche2Index))
                    End If

                Else
                    If Not formulaTab(i).isMoved Then
                        MyArrayCouche.Add(formulaTab(i))
                    End If
                End If
            Next
            formulaTab = DirectCast(MyArrayCouche.ToArray(GetType(Formula)), Formula())
            endTime = Now
            Dim duration As TimeSpan = endTime - startTime
            myConsoleLog.WriteLine("couche sous couche took:" & duration.ToString)

            Dim searchByColorCode As Boolean = False
            Dim colorCodeSearched As String = txtColorCode.Text.Trim
            If Not colorCodeSearched = "" Then
                searchByColorCode = True
            End If
            If searchByColorCode Then
                'start with the color searched, then the others
                'move [color searched] to first

                Dim MyArrayColorSearched As New ArrayList
                For i = 0 To formulaTab.Length - 1
                    If formulaTab(i).colorCode.ToLower = colorCodeSearched.ToLower Then
                        If Not MyArrayColorSearched.Contains(formulaTab(i)) Then
                            MyArrayColorSearched.Add(formulaTab(i))
                        End If
                    End If
                Next

                'move [color searched][space][kharadjej] to second
                For i = 0 To formulaTab.Length - 1
                    If formulaTab(i).colorCode.ToLower.StartsWith(colorCodeSearched.ToLower & " ") Then
                        If Not MyArrayColorSearched.Contains(formulaTab(i)) Then
                            MyArrayColorSearched.Add(formulaTab(i))
                        End If
                    End If
                Next

                'move [color searched][kharadjej] to third
                For i = 0 To formulaTab.Length - 1
                    If formulaTab(i).colorCode.ToLower.StartsWith(colorCodeSearched.ToLower) And Not formulaTab(i).colorCode.ToLower.StartsWith(colorCodeSearched.ToLower & " ") And Not formulaTab(i).colorCode.ToLower = colorCodeSearched.ToLower Then
                        If Not MyArrayColorSearched.Contains(formulaTab(i)) Then
                            MyArrayColorSearched.Add(formulaTab(i))
                        End If
                    End If
                Next

                'move [...][color searched][...] to fourth
                For i = 0 To formulaTab.Length - 1
                    If formulaTab(i).colorCode.ToLower.Contains(colorCodeSearched.ToLower) And Not formulaTab(i).colorCode.StartsWith(colorCodeSearched.ToLower) And Not formulaTab(i).colorCode.ToLower = colorCodeSearched.ToLower Then
                        If Not MyArrayColorSearched.Contains(formulaTab(i)) Then
                            MyArrayColorSearched.Add(formulaTab(i))
                        End If
                    End If
                Next

                formulaTab = DirectCast(MyArrayColorSearched.ToArray(GetType(Formula)), Formula())
            End If

            startTime = Now

            For i = 0 To formulaTab.Length - 1
                lsvFamily.Items.Add(formulaTab(i).id_formula)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).oldDb)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).type)
                'lsvFamily.Items(i).SubItems.Add("")
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).colorCode)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).name_formula)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).c_year)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).version)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).cardNumber)
                lsvFamily.Items(i).SubItems.Add(formulaTab(i).clientName)

                Dim variantExists As Boolean = False
                Dim formulaVariant As String = formulaTab(i).formulaVariant
                If Not formulaVariant Is Nothing Then
                    If formulaVariant.Trim <> "" Then
                        variantExists = True
                    End If
                End If
                If Not variantExists Then
                    lsvFamily.Items(i).SubItems.Add("")
                Else

                    Dim formulaVariants() As String = formulaVariant.Split("+")
                    Dim v As Integer
                    Dim str As String = ""
                    For v = 0 To formulaVariants.Length - 1
                        str = str & getVariant(formulaVariants(v)).variantDescription & " "
                    Next
                    lsvFamily.Items(i).SubItems.Add(str)

                    lsvFamily.Items(i).UseItemStyleForSubItems = False



                    'lsvFamily.Items(i).SubItems.Add(formulaVariant)
                    ' Dim formulaVariants() As String = formulaVariant.Split("+")
                    Dim j As Integer

                    For j = 0 To formulaVariants.Length - 1
                        If 11 + j > lsvFamily.Columns.Count Then
                            lsvFamily.Columns.Add("", 25, Windows.Forms.HorizontalAlignment.Left)

                        End If
                        lsvFamily.Items(i).SubItems.Add("")
                        lsvFamily.Items(i).SubItems(10 + j).BackColor = getVariant(formulaVariants(j)).variantColor

                    Next
                    If 11 + j > lsvFamily.Columns.Count Then
                        lsvFamily.Columns.Add("", 25, Windows.Forms.HorizontalAlignment.Left)
                    End If

                End If

            Next
            endTime = Now
            duration = endTime - startTime
            myConsoleLog.WriteLine("filling listview took:" & duration.ToString)
            'set the variant
            Dim lastColumnIndex = 0

            For Each column As ColumnHeader In lsvFamily.Columns
                lastColumnIndex += 1
            Next
            'lsvFamily.Columns(lastColumnIndex - 1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)

            'autoresize listview
            For Each column As ColumnHeader In lsvFamily.Columns
                If column.Tag = "cctype" Or column.Tag = "ccode" Or column.Tag = "ccname" Or column.Tag = "cyear" Or column.Tag = "ccardNbr" Or column.Tag = "cvariant" Then
                    column.Width = -2
                End If
                If column.Tag = "cclient" And column.Width <> 0 Then
                    column.Width = -2
                End If
            Next
            '------------------

            lsvFamily.EndUpdate()


        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Private Function filterFormulas(allFormulas As Formula()) As Formula()
        Dim formulaNameChk As Boolean = chkFormulaName.Checked
        Dim formulaName As String = txtFormulaName.Text

        Dim clientNameChk As Boolean = True
        Dim clientName As String = SafeSqlLiteral(cmbClientName.Text)
        If clientName Is Nothing Then
            clientName = ""
        End If

        Dim carName As String = getCarById(lbCarIdSearch.Text).carName
        If carName Is Nothing Then
            carName = ""
        End If
        Dim colorCodeChk As Boolean = chkColorCode.Checked
        Dim colorCode As String = txtColorCode.Text

        Dim cardNumberChk As Boolean = chkCardNumber.Checked
        Dim cardNumber As String = txtCardNumber.Text

        Dim MyArray As New ArrayList

        For i = 0 To allFormulas.Length - 1

            If formulaName.Trim <> "" Then
                If formulaNameChk Then
                    If Not allFormulas(i).name_formula.ToUpper.Contains(formulaName.ToUpper) Then
                        Continue For
                    End If
                Else
                    If Not allFormulas(i).name_formula.ToUpper = formulaName.ToUpper Then
                        Continue For
                    End If
                End If
            End If

            If carName.Trim <> "" Then

                If Not allFormulas(i).name_car.ToUpper = carName.ToUpper Then
                    Continue For
                End If

            End If


            If colorCode.Trim <> "" Then
                If colorCodeChk Then
                    'If carName.ToLower.Contains("ford") Then
                    If Not (allFormulas(i).colorCode.ToUpper.StartsWith(colorCode.ToUpper) Or allFormulas(i).colorCode.ToUpper.StartsWith(" " & colorCode.ToUpper)) Then
                        Continue For
                    End If
                    'Else

                    ' If Not allFormulas(i).colorCode.ToUpper.Contains(colorCode.ToUpper) Then
                    'Continue For
                    'End If
                    'End If
                Else
                    If Not allFormulas(i).colorCode.ToUpper = colorCode.ToUpper Then
                        Continue For
                    End If
                End If
            End If

            If clientName.Trim <> "" Then
                If Not allFormulas(i).clientName Is Nothing Then

                    If Not allFormulas(i).clientName.ToUpper.Contains(clientName.ToUpper) Then
                        Continue For
                    End If
                Else
                    Continue For
                End If
            Else
                If Not (allFormulas(i).clientName Is Nothing Or allFormulas(i).clientName = "") Then
                    Continue For
                End If
            End If

            If cardNumber.Trim <> "" Then
                If cardNumberChk Then
                    If Not allFormulas(i).cardNumber.ToUpper.Contains(cardNumber.ToUpper) Then
                        Continue For
                    End If
                Else
                    If Not allFormulas(i).cardNumber.ToUpper = cardNumber.ToUpper Then
                        Continue For
                    End If
                End If
            End If

            If Not chosenGarage.showAll Then
                Dim condition As Boolean = allFormulas(i).version.ToUpper.StartsWith("S1") Or allFormulas(i).version.ToUpper.StartsWith("S2")
                condition = condition Or allFormulas(i).version.ToUpper.StartsWith("S3") Or allFormulas(i).version.ToUpper.StartsWith("S4")
                condition = condition Or allFormulas(i).version.ToUpper.StartsWith("S5")
                condition = condition Or allFormulas(i).version.ToUpper.StartsWith("SY") Or allFormulas(i).version.ToUpper = "*"
                condition = condition Or allFormulas(i).version = "" Or Not allFormulas(i).cardNumber.ToUpper = ""

                If Not condition Then
                    Continue For
                End If
            End If
            MyArray.Add(allFormulas(i))
        Next

        Return DirectCast(MyArray.ToArray(GetType(Formula)), Formula())

    End Function
    Private Sub goOpt()
        If (txtYearMax.Text <> "" And Not IsNumeric(txtYearMax.Text)) Or (txtYearMin.Text <> "" And Not IsNumeric(txtYearMin.Text)) Or (txtYearSpec.Text <> "" And Not IsNumeric(txtYearSpec.Text)) Then
            MsgBox("Year must be numeric")
            Exit Sub
        End If

        Dim formulaNameChk As Boolean = chkFormulaName.Checked
        Dim formulaName As String = txtFormulaName.Text

        Dim clientNameChk As Boolean = True
        Dim clientName As String = SafeSqlLiteral(cmbClientName.Text)
        If clientName Is Nothing Then
            clientName = ""
        End If

        Dim carName As String = getCarById(lbCarIdSearch.Text).carName
        If carName Is Nothing Then
            carName = ""
        End If
        Dim colorCodeChk As Boolean = chkColorCode.Checked
        Dim colorCode As String = txtColorCode.Text

        Dim cardNumberChk As Boolean = chkCardNumber.Checked
        Dim cardNumber As String = txtCardNumber.Text

        Dim yearMin As String = "" 'txtYearMin.Text
        Dim yearMax As String = "" 'txtYearMax.Text

        Dim yearSpec As String = txtYearSpec.Text

        If (colorCode = "" Or colorCodeChk) And carName = "" And clientName = "" Then
            MsgBox("For a better performance, the search must be at least filtered by car, exact client name, or color code, and if the filter is the color code, 'Contains' must be unchecked.", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            If (colorCode = "" Or colorCodeChk) And carName = "" Then
                clientNameChk = False 'forcing the search by exact client name
            End If


        End If
        Dim oldDb As Boolean = chkUseOldDb.Checked

        'optimization
        Dim allFormulas() As Formula = getFormulasClone()


        myConsoleLog.WriteLine(" start filter:" & Now.TimeOfDay.ToString)
        Dim formulasFiltered As Formula() = filterFormulas(allFormulas)
        myConsoleLog.WriteLine(" end filter:" & Now.TimeOfDay.ToString)

        If clientName.Trim <> "" Then 'merge with the result of old or new DB
            cclient.Width = 60
        Else
            cclient.Width = 0
        End If
        'filterByYear
        If yearSpec <> "" Then
            Dim MyArray As New ArrayList

            For i = 0 To formulasFiltered.Length - 1
                Dim yearStr As String = formulasFiltered(i).c_year
                If yearStr Is Nothing Then
                    Continue For
                End If
                If yearStr.IndexOf("-") > -1 And yearStr.Trim <> "-" Then
                    Dim iYearMin As Long = yearStr.Substring(0, yearStr.IndexOf("-")).Trim
                    Dim iYearMax As Long = 0
                    If yearStr.Substring(yearStr.IndexOf("-") + 1).Trim <> "" Then
                        iYearMax = yearStr.Substring(yearStr.IndexOf("-") + 1).Trim
                    End If


                    If yearSpec >= iYearMin And yearSpec <= iYearMax Then
                        MyArray.Add(formulasFiltered(i))
                    End If
                End If

            Next
            formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
        Else
            If yearMax.Trim <> "" Or yearMin.Trim <> "" Then
                If yearMax.Trim = "" Then
                    yearMax = "9999999999"
                End If
                If yearMin.Trim = "" Then
                    yearMin = "0"
                End If

                Dim MyArray As New ArrayList

                For i = 0 To formulasFiltered.Length - 1
                    Dim yearStr As String = formulasFiltered(i).c_year
                    If yearStr Is Nothing Then
                        Continue For
                    End If
                    If yearStr.IndexOf("-") > -1 And yearStr.Trim <> "-" Then
                        Dim iYearMin As Long = yearStr.Substring(0, yearStr.IndexOf("-")).Trim
                        Dim iYearMax As Long = yearStr.Substring(yearStr.IndexOf("-") + 2).Trim
                        If iYearMin >= CType(yearMin.Trim, Long) And iYearMax <= CType(yearMax.Trim, Long) Then
                            MyArray.Add(formulasFiltered(i))
                        End If
                    End If

                Next
                formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
            End If
        End If
        'formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
        Dim filter As String = ""
        If rdBC.Checked Then
            filter = "BC"
        ElseIf rd2k_LS.Checked Then
            filter = chosenGarage.coat
        End If

        myConsoleLog.WriteLine(" start list:" & Now.TimeOfDay.ToString)
        If Not colorCode Is Nothing And Not "" = colorCode Then
            'Sort by year
            formulasFiltered = sortByYear(formulasFiltered)
            'order by MET and MAT
            formulasFiltered = placeMetAndMat(formulasFiltered)
        End If
        addResultsToListviewFamily(formulasFiltered, True, filter)
        myConsoleLog.WriteLine(" end list:" & Now.TimeOfDay.ToString)

        'lsvFamily.Items.Clear()
        'curFamily = Nothing
        'rdBC.Select()
        'filterFamilyByType("BC")
        myConsoleLog.WriteLine("go disp start:" & Now.TimeOfDay.ToString)
        edit.disposeHiddenTextBoxes()
        myConsoleLog.WriteLine("go disp start:" & Now.TimeOfDay.ToString)
    End Sub

    Private Sub go()
        If (txtYearMax.Text <> "" And Not IsNumeric(txtYearMax.Text)) Or (txtYearMin.Text <> "" And Not IsNumeric(txtYearMin.Text)) Or (txtYearSpec.Text <> "" And Not IsNumeric(txtYearSpec.Text)) Then
            MsgBox("Year must be numeric")
            Exit Sub
        End If

        Dim formulaNameChk As Boolean = chkFormulaName.Checked
        Dim formulaName As String = txtFormulaName.Text

        Dim clientNameChk As Boolean = True
        Dim clientName As String = SafeSqlLiteral(cmbClientName.Text)
        If clientName Is Nothing Then
            clientName = ""
        End If

        If clientName.Trim <> "" Then 'merge with the result of old or new DB
            cclient.Width = 60
        Else
            cclient.Width = 0
        End If

        Dim carId As Integer = lbCarIdSearch.Text
        Dim chosenCar As Car = getCarById(carId)
        Dim carName As String = chosenCar.carName
        If carName Is Nothing Then
            carName = ""
        End If
        Dim colorCodeChk As Boolean = chkColorCode.Checked
        Dim colorCode As String = txtColorCode.Text

        Dim cardNumberChk As Boolean = chkCardNumber.Checked
        Dim cardNumber As String = txtCardNumber.Text

        Dim yearMin As String = "" 'txtYearMin.Text
        Dim yearMax As String = "" 'txtYearMax.Text

        Dim yearspec As String = txtYearSpec.Text
        If Not IsNumeric(yearspec) And yearspec.Trim <> "" Then
            MsgBox("Year must be numeric in the search mode", MsgBoxStyle.Exclamation)
            Exit Sub

        End If
        If (colorCode = "" Or colorCodeChk) And carName = "" Then
            MsgBox("For a better performance, the search must be at least filtered by car ,color code or basic color, and if the filter is the color code, 'Contains' must be unchecked.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim str1 As String = ""
        If formulaName.Trim <> "" Then
            If formulaNameChk Then
                str1 = str1 & " name_formula like '%" & formulaName & "%'"
            Else
                str1 = str1 & " name_formula = '" & formulaName & "'"
            End If
        End If

        'Dim str2 As String = ""
        'If carName.Trim <> "" Then
        ' str2 = str2 & " carName = '" & carName & "'"
        ' End If

        Dim str3 As String = ""
        If colorCode.Trim <> "" Then
            If colorCodeChk Then
                str3 = str3 & " LCASE(colorCode) like '%" & colorCode.ToLower & "%'"
            Else
                str3 = str3 & " LCASE(colorCode) like '" & colorCode.ToLower & "%'"
            End If
        End If

        Dim str4 As String = ""
        If cardNumber.Trim <> "" Then
            If cardNumberChk Then
                str4 = str4 & " cardNumber like '%" & cardNumber & "%'"
            Else
                str4 = str4 & " cardNumber = '" & cardNumber & "'"
            End If
        End If

        Dim str5 As String = ""
        If clientName.Trim <> "" Then
            If clientNameChk Then
                str5 = str5 & " clientName like '%" & clientName.ToUpper & "%'"
            Else
                str5 = str5 & " clientName = '" & clientName.ToUpper & "'"
            End If
        Else
            str5 = str5 & " (clientName IS NULL OR clientName='') "
        End If

        Dim str6 As String = ""
        If Not chosenGarage.showAll Then
            str6 = str6 & " (version not like 'S6%' and version not like 'S7%' and version not like 'S8%' and "
            str6 = str6 & " version not like 'S9%' and version not like 'S10%') "
        End If


        Dim whereStr As String = ""
        If str1 <> "" Then
            whereStr = str1
        End If
        'If str2 <> "" Then
        'If whereStr <> "" Then
        'whereStr = whereStr & " AND " & str2
        'Else
        'whereStr = str2
        'End If
        'End If

        If str3 <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND " & str3
            Else
                whereStr = str3
            End If
        End If

        If str4 <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND " & str4
            Else
                whereStr = str4
            End If
        End If

        If str5 <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND " & str5
            Else
                whereStr = str5
            End If
        End If
        If str6 <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND " & str6
            Else
                whereStr = str6
            End If
        End If
        If whereStr <> "" Then
            whereStr = " AND " & whereStr
        End If
        Dim startTime As DateTime
        Dim endTime As DateTime

        startTime = Now

        Dim formulasFiltered() As Formula = getFormulas(whereStr, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)

        endTime = Now
        Dim duration As TimeSpan = endTime - startTime
        myConsoleLog.WriteLine("getting getFormulas from DB took:" & duration.ToString)


        'filterByYear
        If yearspec <> "" Then
            Dim MyArray As New ArrayList

            For i = 0 To formulasFiltered.Length - 1
                Dim yearStr As String = formulasFiltered(i).c_year
                If yearStr Is Nothing Then
                    Continue For
                End If
                If yearStr.IndexOf("-") > -1 And yearStr.Trim <> "-" Then
                    Dim iYearMin As Long = yearStr.Substring(0, yearStr.IndexOf("-")).Trim
                    Dim iYearMax As Long = 0
                    If yearStr.Substring(yearStr.IndexOf("-") + 1).Trim() <> "" Then
                        iYearMax = yearStr.Substring(yearStr.IndexOf("-") + 1).Trim()
                    End If
                    If yearspec >= iYearMin And yearspec <= iYearMax Then
                        MyArray.Add(formulasFiltered(i))
                    End If
                End If

            Next
            formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())

        Else

            If yearMax.Trim <> "" Or yearMin.Trim <> "" Then
                If yearMax.Trim = "" Then
                    yearMax = "9999999999"
                End If
                If yearMin.Trim = "" Then
                    yearMin = "0"
                End If

                Dim MyArray As New ArrayList

                For i = 0 To formulasFiltered.Length - 1
                    Dim yearStr As String = formulasFiltered(i).c_year
                    If yearStr Is Nothing Then
                        Continue For
                    End If
                    If yearStr.IndexOf("-") > -1 And yearStr.Trim <> "-" Then
                        Dim iYearMin As Long = yearStr.Substring(0, yearStr.IndexOf("-")).Trim
                        Dim iYearMax As Long = yearStr.Substring(yearStr.IndexOf("-") + 2).Trim
                        If iYearMin >= CType(yearMin.Trim, Long) And iYearMax <= CType(yearMax.Trim, Long) Then
                            MyArray.Add(formulasFiltered(i))
                        End If
                    End If

                Next
                formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
            End If
        End If
        'formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
        Dim filter As String = ""
        If rdBC.Checked Then
            filter = "BC"
        ElseIf rd2k_LS.Checked Then
            filter = chosenGarage.coat
        End If

        If Not colorCode Is Nothing And Not "" = colorCode Then
            'Sort by year
            formulasFiltered = sortByYear(formulasFiltered)
            'order by MET and MAT
            formulasFiltered = placeMetAndMat(formulasFiltered)
        End If

        startTime = Now
        addResultsToListviewFamily(formulasFiltered, True, filter)

        endTime = Now
        duration = endTime - startTime
        myConsoleLog.WriteLine("getting addResultsToListviewFamily took:" & duration.ToString)
        'lsvFamily.Items.Clear()
        'curFamily = Nothing
        'rdBC.Select()
        'filterFamilyByType("BC")
        edit.disposeHiddenTextBoxes()
    End Sub

    Private Sub butGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butGo.Click
        go()
    End Sub

    Private Sub cmbCarNameSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCarNameSearch.SelectedIndexChanged
        If cmbCarNameSearch.Text = "" Then
            lbCarIdSearch.Text = "-1"
            Exit Sub
        End If
        'Dim chosenCar As Car

        Dim carsTab() As Car = getCars("")
        chosenCar = carsTab(cmbCarNameSearch.SelectedIndex)

        If Not chosenCar Is Nothing Then
            lbCarIdSearch.Text = chosenCar.id_car
            cmbCarNameSearch.Text = chosenCar.carName
            'Try
            'Dim imgPath As String = My.Application.Info.DirectoryPath & "//images//" & chosenCar.carImgPath
            'imageToPreview = Image.FromFile(imgPath)
            'pctCarImgDetails.Image = imageToPreview
            ''pctCarImg.ImageLocation = imgPath
            'Catch ex As IOException
            'imageToPreview = Nothing
            'pctCarImgDetails.Image = Nothing
            ''MsgBox("Image not found:" & ex.Message.ToString, MsgBoxStyle.Exclamation)
            'End Try
            loadClients(chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
        End If
        setLastChosenCar(chosenCar)
    End Sub

    ' Private Sub lsvFormula_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsvFormula.MouseClick, lsvFormula.KeyDown, lsvFormula.KeyUp
    '     disposeHiddenTextBoxes()
    '     curFamily = Nothing
    '     curSubFamily = Nothing
    '     Dim formulaCodeChosen As String = lsvFormula.SelectedItems(0).SubItems(0).Text
    '
    '    lbSelectedFormulavalue.Text = formulaCodeChosen
    '
    '    Dim whereStr As String = " AND colorCode='" & formulaCodeChosen.Trim & "'"
    '
    '    Dim formulasFiltered() As Formula = getFormulas(whereStr)
    '    'formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())

    '   addResultsToListviewFamily(formulasFiltered, True)
    '   rdBC.Select()
    '   filterFamilyByType("BC")

    'End Sub

    Private Sub filterFamilyByType(ByVal type)
        If Not curFamily Is Nothing Then
            If curFamily.Length > 0 Then

                Dim MyArray As New ArrayList
                Dim i As Integer
                For i = 0 To curFamily.Length - 1
                    If curFamily(i).type.ToLower.Trim = type.ToLower.Trim Or type.Trim = "" Then
                        curFamily(i).isMoved = False
                        MyArray.Add(curFamily(i))
                    End If
                Next

                Dim formulasFiltered() As Formula
                formulasFiltered = DirectCast(MyArray.ToArray(GetType(Formula)), Formula())
                'curSubFamily = formulasFiltered
                addResultsToListviewFamily(formulasFiltered, False)
            End If
        End If
    End Sub

    Private Sub radioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rd2k_LS.CheckedChanged, rdBC.CheckedChanged, rdAny.CheckedChanged
        Dim type As String = ""
        If rd2k_LS.Checked Then
            type = chosenGarage.coat
        ElseIf rdBC.Checked Then
            type = "BC"
        End If
        filterFamilyByType(type)

    End Sub


    Private Sub gotToEdit()
        myConsoleLog.WriteLine("go edit start:" & Now.TimeOfDay.ToString)
        'initiale edit'
        edit.prevQty = 1
        '''''''''
        'test if formula is composed
        Dim idFormula = lsvFamily.SelectedItems(0).SubItems(0).Text
        Dim oldDb As Boolean = lsvFamily.SelectedItems(0).SubItems(1).Text
        Dim selectedFormula As Formula

        'test if it's a client formula
        Dim clientTable As Boolean = False
        clientNameColumnIndex = 0
        For Each column As ColumnHeader In lsvFamily.Columns
            clientNameColumnIndex += 1
            If column.Tag = "cclient" Then
                Exit For
            End If
        Next


        If Not lsvFamily.SelectedItems(0).SubItems(clientNameColumnIndex - 1).Text Is Nothing Then
            If lsvFamily.SelectedItems(0).SubItems(clientNameColumnIndex - 1).Text <> "" Then
                clientTable = True
            End If
        End If
        '
        ' Dim idCar As Long = lbCarIdSearch.Text
        selectedFormula = getFormulaById(idFormula, oldDb, clientTable, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
        Dim fname As String = selectedFormula.name_formula
        Dim fcode As String = selectedFormula.colorCode
        Dim isComposed As Boolean = False
        Dim isBumper As Boolean = False
        'Dim codeF1 As String = ""
        'Dim codeF2 As String = ""
        'Dim codeF3 As String = ""
        'Dim allowZ As Boolean = False
        If fname.IndexOf(" + ") > 0 Or fcode.ToLower.EndsWith("bumper") Then
            'codeF1 = fname.Substring(0, fname.IndexOf("+")).Trim
            'codeF2 = fname.Substring(fname.IndexOf("+") + 1).Trim
            'If IsNumeric(codeF1) And IsNumeric(codeF2) Then
            isComposed = True
            If fcode.ToLower.EndsWith("bumper") Then
                isBumper = True
            End If
            'End If
            'If codeF2.IndexOf(" + ") > 0 Then
            'codeF3 = codeF2.Substring(codeF2.IndexOf("+") + 1).Trim
            'allowZ = True
            'End If
        End If
        If isComposed Then
            'getting formula X ,Y and Z

            Dim idX As Integer = selectedFormula.id_formulaX
            Dim idXp As Integer = selectedFormula.id_formulaXp
            Dim idX2p As Integer = selectedFormula.id_formulaX2p

            Dim idY As Integer = selectedFormula.id_formulaY
            Dim idYp As Integer = selectedFormula.id_formulaYp
            Dim idY2p As Integer = selectedFormula.id_formulaY2p

            Dim idZ As Integer = selectedFormula.id_formulaZ
            Dim idZp As Integer = selectedFormula.id_formulaZp
            Dim idZ2p As Integer = selectedFormula.id_formulaZ2p


            Dim xAssigned As Boolean = False
            Dim yAssigned As Boolean = False
            Dim zAssigned As Boolean = False

            If idX <> -1 And idX <> 0 Then
                xAssigned = True
            Else
                If idXp <> -1 And idXp <> 0 And idX2p <> -1 And idX2p <> 0 Then
                    xAssigned = True
                End If
            End If

            If idY <> -1 And idY <> 0 Then
                yAssigned = True
            Else
                If idYp <> -1 And idYp <> 0 And idY2p <> -1 And idY2p <> 0 Then
                    yAssigned = True
                End If
            End If

            If idZ <> -1 And idZ <> 0 Then
                zAssigned = True
            Else
                If idZp <> -1 And idZp <> 0 And idZ2p <> -1 And idZ2p <> 0 Then
                    zAssigned = True
                End If
            End If

            Dim assignCondition As Boolean = False
            If isBumper Then
                assignCondition = Not xAssigned
            Else
                assignCondition = Not xAssigned Or Not yAssigned Or Not zAssigned
            End If

            If assignCondition Then
                MsgBox("This composed formula is not well formed: the code colors are not referenced to existing formulas.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            'composed formula are never client made
            'Dim idCar As Long = lbCarIdSearch.Text
            If Not isBumper Then


                If idX <> -1 And idX <> 0 Then
                    formComposed.Button1B.Visible = False
                    formComposed.Button1A.Visible = True

                    formComposed.Button1A.AccessibleDescription = idX
                    Dim formula As Formula = getFormulaById(idX, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameX As String = formula.colorCode & " - " & formula.version
                    formComposed.Button1A.Text = clrNameX
                Else
                    formComposed.Button1B.Visible = True
                    formComposed.Button1A.Visible = True

                    formComposed.Button1A.AccessibleDescription = idXp
                    Dim formulap As Formula = getFormulaById(idXp, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameXp As String = formulap.colorCode & " - " & formulap.version
                    formComposed.Button1A.Text = clrNameXp

                    formComposed.Button1B.AccessibleDescription = idX2p
                    Dim formula2p As Formula = getFormulaById(idX2p, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameX2p As String = formula2p.colorCode & " - " & formula2p.version
                    formComposed.Button1B.Text = clrNameX2p

                End If

                If idY <> -1 And idY <> 0 Then
                    formComposed.Button2B.Visible = False
                    formComposed.Button2A.Visible = True

                    formComposed.Button2A.AccessibleDescription = idY
                    Dim formula As Formula = getFormulaById(idY, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameY As String = formula.colorCode & " - " & formula.version
                    formComposed.Button2A.Text = clrNameY
                Else
                    formComposed.Button2B.Visible = True
                    formComposed.Button2A.Visible = True

                    formComposed.Button2A.AccessibleDescription = idYp
                    Dim formulap As Formula = getFormulaById(idYp, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameYp As String = formulap.colorCode & " - " & formulap.version
                    formComposed.Button2A.Text = clrNameYp

                    formComposed.Button2B.AccessibleDescription = idY2p
                    Dim formula2p As Formula = getFormulaById(idY2p, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                    Dim clrNameY2p As String = formula2p.colorCode & " - " & formula2p.version
                    formComposed.Button2B.Text = clrNameY2p

                End If

                If idZ = -2 And idZp = -2 And idZ2p = -2 Then
                    formComposed.Button3A.Visible = False
                    formComposed.Button3B.Visible = False
                    formComposed.grp3.Visible = False
                Else
                    formComposed.grp3.Visible = True
                    If idZ <> -1 And idZ <> 0 And idZ <> -2 Then
                        formComposed.Button3B.Visible = False
                        formComposed.Button3A.Visible = True

                        formComposed.Button3A.AccessibleDescription = idZ
                        Dim formula As Formula = getFormulaById(idZ, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                        Dim clrNameZ As String = formula.colorCode & " - " & formula.version
                        formComposed.Button3A.Text = clrNameZ
                    Else
                        formComposed.Button3B.Visible = True
                        formComposed.Button3A.Visible = True
                        formComposed.Button3A.AccessibleDescription = idZp
                        Dim formulap As Formula = getFormulaById(idZp, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                        Dim clrNameZp As String = formulap.colorCode & " - " & formulap.version
                        formComposed.Button3A.Text = clrNameZp

                        formComposed.Button3B.AccessibleDescription = idZ2p
                        Dim formulap2p As Formula = getFormulaById(idZ2p, oldDb, False, chosenCar.tableName, chosenCar.carName, chosenCar.id_car)
                        Dim clrNameZ2p As String = formulap2p.colorCode & " - " & formulap2p.version
                        formComposed.Button3B.Text = clrNameZ2p

                    End If
                End If

                formComposed.AccessibleDescription = oldDb
                formComposed.ShowDialog()
                Exit Sub
            End If
        End If

        'PREPARE EDIT FORM
        selectedFormulaColors = Nothing
        ratio = 1
        myConsoleLog.WriteLine("goedit - edit.hideRadioVariants start:" & Now.TimeOfDay.ToString)
        edit.hideRadioVariants()
        myConsoleLog.WriteLine("goedit - edit.hideRadioVariants end:" & Now.TimeOfDay.ToString)
        Dim startWithLitre As Boolean = False
        If chosenUnit.code_unit = "L" Then
            startWithLitre = True
        End If
        myConsoleLog.WriteLine("goedit - edit.generatedetails start:" & Now.TimeOfDay.ToString)
        'clear composed info since we are sure this is not a composed case
        edit.id_formulaComposedGlobal = -1
        edit.oldDbComposedGlobal = ""
        '--------------------------

        ' edit.rdNormal.Select()

        edit.quantitiesWithAllDecimals = Nothing
        edit.quantitiesWithAllDecimalsCum = Nothing
        edit.selectNormal = True
        ' edit.rdNormal.Select()

        If isBumper Then
            edit.generatesDetail(startWithLitre, selectedFormula.id_formulaX, oldDb, True)
        Else
            edit.generatesDetail(startWithLitre)
        End If



        myConsoleLog.WriteLine("goedit - edit.generatedetails end:" & Now.TimeOfDay.ToString)
        If edit.formulaCorrupted = True Then
            edit.Dispose()
            Exit Sub
        End If
        'edit.grpEdit.Visible = True
        Me.Visible = False
        edit.formulaDup = Nothing
       
        myConsoleLog.WriteLine("goedit - open dialog start:" & Now.TimeOfDay.ToString)



        'Dim lbPlus As Windows.Forms.Label = CType(Controls.Find("lbPlus", True)(0), Windows.Forms.Label)


        edit.ShowDialog()
        '-------------------------------------------------------

        myConsoleLog.WriteLine("goedit end:" & Now.TimeOfDay.ToString)
    End Sub

    Private Sub lsvFamily_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lsvFamily.MouseDoubleClick
        gotToEdit()
    End Sub
    Private Sub lsvFamily_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lsvFamily.KeyUp
        If e.KeyCode = Keys.Enter Then
            gotToEdit()
        End If
    End Sub
    Private Sub txtColorCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFormulaName.KeyUp, txtYearMin.KeyUp, txtYearMax.KeyUp, txtColorCode.KeyUp
        If e.KeyCode = Keys.Enter Then
            go()
        End If
    End Sub

    Private Sub butExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExit.Click
        Me.Close()
    End Sub

#Region "listViewSorting"
    ' The column currently used for sorting.
    Private m_SortingColumn As ColumnHeader

    ' Sort using the clicked column.
    Private Sub lvwBooks_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lsvFamily.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = lsvFamily.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Replace("< ", "")
            m_SortingColumn.Text = m_SortingColumn.Text.Replace("> ", "")
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lsvFamily.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lsvFamily.Sort()
    End Sub

#End Region

    Private Sub garageHome_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.TopMost = False
    End Sub


    Private Sub butClearUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butClearUpdate.Click
        cmbCarNameSearch.SelectedIndex = 0
        txtColorCode.Text = ""
        chkColorCode.Checked = True
        txtFormulaName.Text = ""
        chkFormulaName.Checked = True
        txtCardNumber.Text = ""
        chkCardNumber.Checked = True
        txtYearSpec.Text = ""
        rdBC.Checked = True
        chkUseOldDb.Checked = False
        cmbClientName.SelectedIndex = 0
        txtColorCode.Focus()
    End Sub

    Private Sub grpMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) ' Handles grpFormulas.Paint
        Dim rect As Rectangle
        rect.Height = lbColorCode.Location.Y - lbCarSearch.Location.Y + lbCarSearch.Height + 10
        rect.Width = (cmbCarNameSearch.Location.X + cmbCarNameSearch.Width + 5) - lbCarSearch.Location.X


        Dim rectPoint As Point
        rectPoint.X = lbCarSearch.Location.X - 3
        rectPoint.Y = lbCarSearch.Location.Y - 6

        rect.Location = rectPoint


        Dim myPen As New Pen(Drawing.Color.Red, 2)
        e.Graphics.DrawRectangle(myPen, rect)

    End Sub

    Private Sub BackupCustomersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupCustomersToolStripMenuItem.Click
        Dim backupFilePath As String = ""
        If FBrowserGarage.ShowDialog() = DialogResult.OK Then
            backupFilePath = FBrowserGarage.SelectedPath
        Else
            Exit Sub
        End If

        'generate backup file
        Try
            Dim objStreamWriter As StreamWriter
            Dim fileName As String = backupFilePath & "\\backup" & Today.Day & "-" & Today.Month & "-" & Today.Year & ".txt"
            Dim newF As FileStream = File.Create(fileName, FileOptions.RandomAccess)
            newF.Close()
            'Pass the file path and the file name to the StreamWriter constructor.
            objStreamWriter = New StreamWriter(fileName)

            Dim clientFormulas() As Formula = getClientFormulaTable()

            'Dim transactions() As Transaction = getTransactionTable()
            'Dim transactionFormulas() As TransactionFormula = getTransactionFormulaTable()
            Dim x As Integer = 0

            For x = 0 To clientFormulas.Length - 1
                Dim line As String = ""
                Dim tablename As String = "*clientformula*#T#*"

                line = "formulaImgPath*#C#*" & clientFormulas(x).formulaImgPath
                objStreamWriter.WriteLine(tablename & line)

                line = "otherPrice*#C#*" & clientFormulas(x).otherPrice
                objStreamWriter.WriteLine(tablename & line)

                line = "id_otherCurreny*#C#*" & clientFormulas(x).id_otherCurreny
                objStreamWriter.WriteLine(tablename & line)

                line = "id_otherUnit*#C#*" & clientFormulas(x).id_otherUnit
                objStreamWriter.WriteLine(tablename & line)

                line = "id_car*#C#*" & clientFormulas(x).id_car
                objStreamWriter.WriteLine(tablename & line)

                line = "name_car*#C#*" & clientFormulas(x).name_car
                objStreamWriter.WriteLine(tablename & line)

                line = "type*#C#*" & clientFormulas(x).type
                objStreamWriter.WriteLine(tablename & line)

                line = "version*#C#*" & clientFormulas(x).version
                objStreamWriter.WriteLine(tablename & line)

                line = "name_formula*#C#*" & clientFormulas(x).name_formula
                objStreamWriter.WriteLine(tablename & line)

                line = "formulaVariant*#C#*" & clientFormulas(x).formulaVariant
                objStreamWriter.WriteLine(tablename & line)

                line = "duplicate*#C#*" & clientFormulas(x).duplicate
                objStreamWriter.WriteLine(tablename & line)

                line = "colorRGB*#C#*" & clientFormulas(x).colorRGB
                objStreamWriter.WriteLine(tablename & line)

                line = "c_year*#C#*" & clientFormulas(x).c_year
                objStreamWriter.WriteLine(tablename & line)

                line = "colorCode*#C#*" & clientFormulas(x).colorCode
                objStreamWriter.WriteLine(tablename & line)

                line = "clientName*#C#*" & clientFormulas(x).clientName
                objStreamWriter.WriteLine(tablename & line)

                line = "oldDb*#C#*" & clientFormulas(x).oldDb
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaX*#C#*" & clientFormulas(x).id_formulaX
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaXp*#C#*" & clientFormulas(x).id_formulaXp
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaX2p*#C#*" & clientFormulas(x).id_formulaX2p
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaY*#C#*" & clientFormulas(x).id_formulaY
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaYp*#C#*" & clientFormulas(x).id_formulaYp
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaY2p*#C#*" & clientFormulas(x).id_formulaY2p
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaZ*#C#*" & clientFormulas(x).id_formulaZ
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaZp*#C#*" & clientFormulas(x).id_formulaZp
                objStreamWriter.WriteLine(tablename & line)

                line = "id_formulaZ2p*#C#*" & clientFormulas(x).id_formulaZ2p
                objStreamWriter.WriteLine(tablename & line)

                line = "cardNumber*#C#*" & clientFormulas(x).cardNumber
                objStreamWriter.WriteLine(tablename & line)

                objStreamWriter.WriteLine("*INSERT clientformula*##*")

                Dim clientFormulaColors() As FormulaColor = getClientFormulaColorByIdFormula(clientFormulas(x).id_formula)
                For y = 0 To clientFormulaColors.Length - 1

                    tablename = "*clientFormulaColor*#T#*"

                    line = "id_formulaColor*#C#*" & clientFormulaColors(y).id_formulaColor
                    objStreamWriter.WriteLine(tablename & line)

                    line = "id_formula*#C#*" & "getFromLastInserted"
                    objStreamWriter.WriteLine(tablename & line)

                    line = "id_color*#C#*" & clientFormulaColors(y).id_color
                    objStreamWriter.WriteLine(tablename & line)

                    line = "quantite*#C#*" & clientFormulaColors(y).quantite
                    objStreamWriter.WriteLine(tablename & line)

                    line = "id_Unit*#C#*" & clientFormulaColors(y).id_Unit
                    objStreamWriter.WriteLine(tablename & line)

                    objStreamWriter.WriteLine("*INSERT clientFormulaColor*##*")

                Next
            Next

           


            'Close the file.
            objStreamWriter.Close()

            MsgBox("Backup file generated at:" & vbNewLine & fileName, MsgBoxStyle.Information)

        Catch ex As IOException
            MsgBox("Backup file not created:" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Public reintegratefilePath As String = ""
    Dim thread As System.Threading.Thread
    Dim thread2 As System.Threading.Thread
    Dim iThread As Integer
    Dim max As Integer

    Private Sub countup()

        Do Until iThread = max
            'i = i + 1

            'lbRemains.Text = iThread & "/" & max
            Me.Refresh()
        Loop


    End Sub
    Private Sub RestoreBackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreBackupToolStripMenuItem.Click
        If MsgBox("Restoring the application will cause the deletion of the current customer data, Are you sure you will restore the right backed up data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If fileDialog.ShowDialog() = DialogResult.OK Then
                reintegratefilePath = fileDialog.FileName
            Else
                Exit Sub
            End If

            Try
                Dim objStreamReader As StreamReader
                'Pass the file path and the file name to the StreamWriter constructor.
                objStreamReader = New StreamReader(reintegratefilePath)
                'Dim max As Integer = 0
                Do While objStreamReader.Peek() >= 0
                    Dim query As String = objStreamReader.ReadLine()
                    max += 1
                    'performQuery(query)
                Loop
                'Close the file.
                objStreamReader.Close()
                'lbRemains.Text = "0/" & max
                'butBrowseUpdate.Enabled = False
                'butUpdateApp.Enabled = False
                'butBackup.Enabled = False
                'butReintegrate.Enabled = False
                butGo.Enabled = False
                menFile.Enabled = False
                lsvFamily.Enabled = False
                thread = New System.Threading.Thread(AddressOf countup)
                thread.Start()

                thread2 = New System.Threading.Thread(AddressOf ReintegrateThread)
                thread2.Start()
                Me.SendToBack()
            Catch ex As IOException
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub ReintegrateThread()
        Try
            Dim errors As String = ""
            Dim objStreamReader As StreamReader
            'Pass the file path and the file name to the StreamWriter constructor.
            objStreamReader = New StreamReader(reintegratefilePath)
            prgUpdate.Maximum = max
            prgUpdate.Step = 1

            Dim tableName As String = ""
            Dim columnName As String = ""

            Dim name_formula As String = ""
            Dim id_car As Integer
            Dim Type As String = ""
            Dim version As String = ""
            Dim formulaVariant As String = ""
            Dim clientName As String = ""
            Dim colorCode As String = ""
            Dim colorRGB As String = ""
            Dim c_year As String = ""
            Dim cardNumber As String = ""


            Dim id_color As Integer
            Dim quantite As String = ""
            Dim id_unit As Integer

            performQuery("DELETE FROM [Clientformula]")
            performQuery("DELETE FROM [ClientformulaColor]")

            Do While objStreamReader.Peek() >= 0
                Dim line As String = objStreamReader.ReadLine() & " "
                iThread += 1
                prgUpdate.PerformStep()

                If (line.Trim = "*INSERT*##*") And (tableName.ToUpper = "CLIENTFORMULA" Or tableName.ToUpper = "CLIENTFORMULACOLOR") Then
                    Dim queryString As String = ""
                    If tableName.ToUpper = "CLIENTFORMULA" Then
                        Dim id_formula As Integer = getLastInsertedFormulaIdClient() + 1

                        queryString = String.Format("INSERT INTO [Clientformula] (name_formula, id_car, type, version, variant,clientName,colorCode,colorRGB,c_year,cardNumber,state,seqnum,id_formula) VALUES('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','I',0,{10})", name_formula, id_car, Type, version, formulaVariant, clientName, colorCode, colorRGB, c_year, cardNumber, id_formula)
                    End If

                    If tableName.ToUpper = "CLIENTFORMULACOLOR" Then
                        Dim id_formula As Integer = getLastInsertedFormulaIdClient()
                        queryString = String.Format("INSERT INTO [ClientformulaColor] (id_formula,id_color,quantite,id_unit,state) VALUES({0},{1},'{2}',{3},'I')", id_formula, id_color, quantite, id_unit)
                    End If

                    tableName = ""
                    errors &= performQuery(queryString)
                    queryString = ""

                    name_formula = ""
                    id_car = -1
                    Type = ""
                    version = ""
                    formulaVariant = ""
                    clientName = ""
                    colorCode = ""
                    colorRGB = ""
                    c_year = ""
                    cardNumber = ""



                    id_color = -1
                    quantite = ""
                    id_unit = -1



                    Continue Do
                End If

                tableName = line.Substring(1, line.IndexOf("*#T#*") - 1)
                columnName = (line.Substring(line.IndexOf("*#T#*") + 5, (line.IndexOf("*#C#*") - 1) - (line.IndexOf("*#T#*") + 4))).ToLower
                If tableName.ToUpper = "CLIENTFORMULACOLOR" Then
                    Dim columnValue As String = line.Substring(line.IndexOf("*#C#*") + 5).Trim



                    If columnName = "id_color" Then
                        id_color = columnValue
                    End If

                    If columnName = "quantite" Then
                        quantite = columnValue
                    End If

                    If columnName = "id_unit" Then
                        id_unit = columnValue
                    End If

                End If

                If tableName.ToUpper = "CLIENTFORMULA" Then
                    Dim columnValue As String = line.Substring(line.IndexOf("*#C#*") + 5).Trim

                    If columnName = "id_car" Then
                        id_car = columnValue
                    End If
                    If columnName = "type" Then
                        Type = columnValue
                    End If

                    If columnName = "version" Then
                        version = columnValue
                    End If

                    If columnName = "name_formula" Then
                        name_formula = columnValue
                    End If

                    If columnName = "formulavariant" Then
                        formulaVariant = columnValue
                    End If

                    If columnName = "colorrgb" Then
                        colorRGB = columnValue
                    End If

                    If columnName = "c_year" Then
                        c_year = columnValue
                    End If

                    If columnName = "colorcode" Then
                        colorCode = columnValue

                    End If

                    If columnName = "clientname" Then
                        clientName = columnValue
                    End If

                    If columnName = "cardnumber" Then
                        cardNumber = columnValue
                    End If

                End If
            Loop



            Try
                prgUpdate.Value = 0
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            If errors.Trim = "" Then
                MsgBox("Your application has been successfully restored:" & vbNewLine & "Please restart your application", MsgBoxStyle.Information)
            Else
                writeLogfile(errors, reintegratefilePath & ".log")
                MsgBox("Your application has not been successfully restored, some errors occured, please check the log file at (" & reintegratefilePath & ".log", MsgBoxStyle.Information)
            End If
            ' butBrowseUpdate.Enabled = True
            'butUpdateApp.Enabled = True
            'butBackup.Enabled = True
            'butReintegrate.Enabled = True
            butGo.Enabled = True
            menFile.Enabled = True
            lsvFamily.Enabled = True
            thread.Abort()
            thread2.Abort()
            Me.Close()
            'Close the file.
        Catch ex As IOException
            butGo.Enabled = True
            menFile.Enabled = True
            lsvFamily.Enabled = True
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub writeLogfile(ByVal str As String, ByVal updateFilelog As String)
        Try
            Dim newF As FileStream = File.Create(updateFilelog, FileOptions.RandomAccess)
            newF.Close()
            'Pass the file path and the file name to the StreamWriter constructor.
            Dim objStreamWriter As New StreamWriter(updateFilelog)
            objStreamWriter.WriteLine(str)
            'Close the file.
            objStreamWriter.Close()
        Catch ex As IOException
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub menSettings_Click(sender As Object, e As EventArgs) Handles menSettings.Click
        Settings.ShowDialog()
    End Sub


    'Private Sub ColorLocatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorLocatorToolStripMenuItem.Click
    '    colorCodeLocator.ShowDialog()
    'End Sub

    Private Sub ColorLocatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorLocatorToolStripMenuItem.Click
        carCodeLocator.ShowDialog()

    End Sub

    Private Sub ImportPricesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportPricesToolStripMenuItem.Click
        importPrices.ShowDialog()
    End Sub
End Class