Module Modules

    Public conString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "//moteurDB.mdb;"
    Public empid As String = "", empname As String = "", didnotpaySearch As Boolean

#Region "Client Management"

    Public Function getClientHistory(ByVal clid) As String
        getClientHistory = ""
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM clientampere,ampere where ampereid=ampere_id and client_id=" & clid
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                getClientHistory = getClientHistory & "On " & DR.Item("registdate") & " took " & DR.Item("ampereValue") & " Ampere(" & DR.Item("price") & " LL)" & Chr(13) & Chr(10)
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getLastClientNumber() As Long
        Try
            Dim DB As New OleDb.OleDbConnection, i As Integer
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [client]"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            Dim tmp As Long, max As Long
            While DR.Read
                tmp = Val(DR.Item("clientid"))
                If tmp < 90000 Then
                    If tmp >= max Then
                        max = tmp
                    End If
                End If
            End While
            Dim clid As Integer
            For i = 1 To max + 1
                If Not clientexist(i) Then
                    clid = i
                    Exit For
                End If
            Next
            getLastClientNumber = clid
            DR.Close()
            DB.Close()
        Catch
        End Try
    End Function
    Public Function clientexist(ByVal clid) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [client] where clientid=" & clid
            'clientupdate.txtcode.Text = DBC.CommandText
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            clientexist = False
            While DR.Read
                clientexist = True
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function deleteClient(ByVal clid) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            'Dim ampid As Double = getIDlastampere(campere)
            DBC.CommandText = "update [clientampere] set ampere_value=0 WHERE client_id = " & clid & " AND (DateDiff(""s"",registdate,'" & Now & "')<= 0)"
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                deleteClient = True
            Else
                deleteClient = False
            End If
            DB.Close()
            If saveClientampere(clid, 0) Then
                deleteClient = True
                updateclientid(clid, 90000)
            Else
                deleteClient = False
            End If
        Catch ex As Exception
            deleteClient = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function updateclientid(ByVal clid, ByVal incnum) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            'Dim ampid As Double = getIDlastampere(campere)
            'Dim incnum As Long = 90000
            DBC.CommandText = "update [client] set clientId=clientId + " & incnum & " WHERE clientid = " & clid
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                updateclientid = True
            Else
                updateclientid = False
            End If
            DB.Close()

        Catch ex As OleDb.OleDbException
            incnum = incnum * 2
            If ex.ErrorCode = -2147467259 Then
                updateclientid(clid, incnum)
            Else
                MsgBox(ex.ErrorCode & vbNewLine & ex.Message.ToString)
            End If
        End Try
    End Function
    Public Function getIDlastampere(ByVal campere) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM ampere where [amperevalue]=" & campere & " AND priceDate = (select max(priceDate) from ampere where amperevalue=" & campere & ")"
            'clientupdate.txtcode.Text = DBC.CommandText
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                getIDlastampere = Val(DR.Item("ampereid"))
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function updateClientampere(ByVal clid, ByVal campere) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            'Dim ampid As Double = getIDlastampere(campere)
            DBC.CommandText = "update [clientampere] set registdate=""" & Now & """ WHERE client_id=" & clid & " AND ampere_value=" & campere
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                updateClientampere = True
            Else
                updateClientampere = False
            End If
            DB.Close()
        Catch ex As Exception
            updateClientampere = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function saveClientampere(ByVal clid, ByVal campere) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into [clientAmpere] values(""" & clid & """,""" & campere & """,""" & Now & """)"
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                saveClientampere = True
            Else
                saveClientampere = False
            End If
            DB.Close()
        Catch ex As Exception
            saveClientampere = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function saveClient(ByVal clid, ByVal cname, ByVal clphone, ByVal claddress, ByVal clstreet, ByVal clbuilding, ByVal clfloor, ByVal clcode, ByVal clbox, ByVal clampere) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into client values(""" & clid & """,""" & Trim(cname) & """,""" & clphone & """,""" & claddress & """,""" & clstreet & """,""" & clbuilding & """,""" & clfloor & """,""" & clcode & """," & Val(clbox) & "," & False & ",""" & Today & """)"
            If DBC.ExecuteNonQuery() Then
                If saveClientampere(clid, clampere) Then
                    saveClient = True
                End If

            Else
                saveClient = False
            End If
            DB.Close()
        Catch ex As Exception
            saveClient = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function updateClient(ByVal clid, ByVal cname, ByVal clphone, ByVal claddress, ByVal clstreet, ByVal clbuilding, ByVal clfloor, ByVal clcode, ByVal clbox, ByVal clampere) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "update [client] set clientid=" & clid & " , clientname=""" & Trim(cname) & """ , clientphone=""" & clphone & """"
            DBC.CommandText = DBC.CommandText & " , clientaddress=""" & claddress & """ , clientstreet=""" & clstreet & """ , clientbuilding=""" & clbuilding & """ , clientfloor=""" & clfloor & """, clientcode=""" & clcode & """,clientbox=" & Val(clbox) & " WHERE clientid=" & clid & ""
            If DBC.ExecuteNonQuery() Then
                ' If updatetonewampereprice = True Then
                'If checkifclientampereexist(clid, clampere) = False Then
                If saveClientampere(clid, clampere) Then
                    updateClient = True
                    'Else
                    '    updateClient = False
                    'End If
                    'Else
                    '    updateClient = True
                Else
                    updateClient = False
                End If
            Else
                updateClient = False
            End If
            DB.Close()
        Catch ex As Exception
            updateClient = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function checkifclientampereexist(ByVal clid, ByVal clampere) As Boolean
        Try
            Dim con As New OleDb.OleDbConnection(conString), query As String = ""
            query = "select * from [clientampere] where [client_id]=" & clid & " and ampere_value=" & clampere
            con.Open()
            Dim cmd As New OleDb.OleDbCommand(query, con)
            Dim db_reader As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            If db_reader.Read Then
                Return True
            Else
                Return False
            End If
            con.Close()
        Catch
        End Try
    End Function
#End Region
#Region "User And Login management"
    Public Function insertuser(ByVal userid, ByVal password) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into [user] values(""" & userid & """,""" & password & """)"
            If DBC.ExecuteNonQuery() Then
                insertuser = True
            Else
                insertuser = False
            End If
            DB.Close()
        Catch ex As Exception
            insertuser = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function checkIfUserExists(ByVal u) As Boolean
        'MsgBox("ok")
        Try
            Dim con As New OleDb.OleDbConnection(conString), query As String = ""
            query = "select * from [user] where userid=""" & LCase(Trim(u)) & """"
            con.Open()
            Dim cmd As New OleDb.OleDbCommand(query, con)
            Dim db_reader As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            If db_reader.Read Then
                Return True
            Else
                Return False
            End If
            con.Close()
        Catch
        End Try
    End Function
    Public Function checkpassword(ByVal user, ByVal pass) As Integer
        empid = user
        'MsgBox(empid)
        Try
            Dim con As New OleDb.OleDbConnection(conString), query As String = ""
            query = "select * from [user] where userid=""" & user & """"
            con.Open()
            Dim cmd As New OleDb.OleDbCommand(query, con)
            Dim db_reader As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            Dim password As String = ""
            If db_reader.Read Then
                password = db_reader.Item("password")
                If Trim(UCase(password)) <> Trim(UCase(pass)) Then
                    Return 3
                Else
                    userid = db_reader.Item("userid")
                    Return 1
                End If
            Else
                Return 2
            End If
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getpassword(ByVal empid) As String
        getpassword = ""
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM employee where user_name=""" & empid & """"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader

            While DR.Read
                getpassword = DR.Item("pass_word")
            End While
            DR.Close()
            DB.Close()
        Catch
        End Try
    End Function
    Public Function changepassw(ByVal cusern, ByVal opass, ByVal npass) As Boolean 'Change The Password
        Try
            If checkpassword(cusern, opass) = 1 Then
                Dim cn As New OleDb.OleDbConnection(conString)
                Dim cmd As New OleDb.OleDbCommand("UPDATE [user] SET [password]=""" & npass & """  WHERE [userid]=""" & cusern & """", cn)
                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Ampere Managment"
    Public Function noclientWithThisampere(ByVal ampereid) As Boolean
        Try
            Dim con As New OleDb.OleDbConnection(conString), query As String = ""
            query = "select * from [clientampere] where ampere_id=" & ampereid
            con.Open()
            Dim cmd As New OleDb.OleDbCommand(query, con)
            Dim db_reader As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            If db_reader.Read Then
                Return False
            Else
                Return True
            End If
            con.Close()
        Catch
        End Try
    End Function
    Public Function deleteampere(ByVal ampid) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [ampere] where ampereid = " & ampid
            If DBC.ExecuteNonQuery() Then
                deleteampere = True
            Else
                deleteampere = False
            End If
            DB.Close()
        Catch ex As Exception
            deleteampere = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function saveAmpereschedule(ByVal ampvalue, ByVal ampyear, ByVal m1, ByVal m2, ByVal m3, ByVal m4, ByVal m5, ByVal m6, ByVal m7, ByVal m8, ByVal m9, ByVal m10, ByVal m11, ByVal m12) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into ampereSchedule values(""" & ampvalue & """,""" & ampyear & """,""" & m1 & """,""" & m2 & """,""" & m3 & """,""" & m4 & """,""" & m5 & """,""" & m6 & """,""" & m7 & """,""" & m8 & """,""" & m9 & """,""" & m10 & """,""" & m11 & """,""" & m12 & """)"
            If DBC.ExecuteNonQuery() Then
                saveAmpereschedule = True
            Else
                saveAmpereschedule = False
            End If
            DB.Close()
        Catch ex As Exception
            saveAmpereschedule = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Sub deletethisyearampere(ByVal mskyear)
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [ampereSchedule] where ampereyear = " & mskyear
            DBC.ExecuteNonQuery()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Public Function getamperecost(ByVal amperevalue, ByVal clmonth, ByVal ampyear) As Double
        Try
            Dim monthstring As String = ""
            Select Case Val(clmonth)
                Case 1
                    monthstring = "jan"
                Case 2
                    monthstring = "feb"
                Case 3
                    monthstring = "mar"
                Case 4
                    monthstring = "apr"
                Case 5
                    monthstring = "may"
                Case 6
                    monthstring = "jun"
                Case 7
                    monthstring = "jul"
                Case 8
                    monthstring = "aug"
                Case 9
                    monthstring = "sep"
                Case 10
                    monthstring = "oct"
                Case 11
                    monthstring = "nov"
                Case 12
                    monthstring = "dec"
            End Select
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand

            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "SELECT * FROM  ampereSchedule WHERE ampere_value=" & amperevalue & " AND ampereyear=" & ampyear

            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                getamperecost = DR.Item(monthstring)
            End While
            'If Val(clmonth) >= 11 Then
            'MsgBox(DBC.CommandText)
            'MsgBox("getamperecost=" & getamperecost)
            'End If
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getampereForThismonth(ByVal clid, ByVal amonth, ByVal ayear) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM clientampere where client_id=" & clid
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            Dim valarray(,) As String, i As Integer = 1, testdate As Long
            Select Case amonth
                Case "January"
                    testdate = Val(ayear & "0199126060")
                Case "February"
                    testdate = Val(ayear & "0299126060")
                Case "March"
                    testdate = Val(ayear & "0399126060")
                Case "April"
                    testdate = Val(ayear & "0499126060")
                Case "May"
                    testdate = Val(ayear & "0599126060")
                Case "June"
                    testdate = Val(ayear & "0699126060")
                Case "July"
                    testdate = Val(ayear & "0799126060")
                Case "August"
                    testdate = Val(ayear & "0899126060")
                Case "September"
                    testdate = Val(ayear & "0999126060")
                Case "October"
                    testdate = Val(ayear & "1099126060")
                Case "November"
                    testdate = Val(ayear & "1199126060")
                Case "December"
                    testdate = Val(ayear & "1299126060")
            End Select
            Dim readdate As Long, c As Integer
            While DR.Read
                ReDim Preserve valarray(2, i)
                readdate = DR.Item("registdate").year
                If DR.Item("registdate").month < 10 Then
                    readdate = readdate & "0" & DR.Item("registdate").month
                Else
                    readdate = readdate & DR.Item("registdate").month
                End If

                If DR.Item("registdate").day < 10 Then
                    readdate = readdate & "0" & DR.Item("registdate").day
                Else
                    readdate = readdate & DR.Item("registdate").day
                End If

                If DR.Item("registdate").hour < 10 Then
                    readdate = readdate & "0" & DR.Item("registdate").hour
                Else
                    readdate = readdate & DR.Item("registdate").hour
                End If

                If DR.Item("registdate").minute < 10 Then
                    readdate = readdate & "0" & DR.Item("registdate").minute
                Else
                    readdate = readdate & DR.Item("registdate").minute
                End If

                If DR.Item("registdate").second < 10 Then
                    readdate = readdate & "0" & DR.Item("registdate").second
                Else
                    readdate = readdate & DR.Item("registdate").second
                End If

                'MsgBox(readdate & vbNewLine & testdate)
                If readdate < testdate Then
                    valarray(1, i) = DR.Item("ampere_value")
                    valarray(2, i) = readdate
                    'MsgBox(valarray(2, i))
                    i = i + 1
                End If
            End While
            'For c = 1 To i - 1
            '    MsgBox(valarray(2, c))
            'Next
            'gBox(getampereForThismonth)
            getampereForThismonth = getmaximum(valarray, i - 1)
            ' MsgBox(getampereForThismonth)
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getmaximum(ByVal valarray, ByVal n) As Long
        Dim i As Integer, tmp As Long
        tmp = 0
        For i = 1 To n
            If valarray(2, i) > tmp Then
                tmp = valarray(2, i)
                getmaximum = valarray(1, i)
                'MsgBox("max " & valarray(1, i))
            End If
        Next
    End Function
#End Region
#Region "Cost management"
    'cost type management
    Public Function getLastCostTypeNumber() As Long
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [costtype]"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            Dim tmp As Long, max As Long
            While DR.Read
                tmp = Val(DR.Item("costtypeId"))
                If tmp >= max Then
                    max = tmp
                End If
                'getLastCostTypeNumber = DR.Item("costtypeId")
            End While
            getLastCostTypeNumber = max + 1
            DR.Close()
            DB.Close()
        Catch
        End Try
    End Function
    Public Function saveCostType(ByVal ctid, ByVal cdesc) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into costtype values(""" & ctid & """,""" & cdesc & """)"
            If DBC.ExecuteNonQuery() Then
                saveCostType = True
            Else
                saveCostType = False
            End If
            DB.Close()
        Catch ex As Exception
            saveCostType = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function noCostidWithThisampere(ByVal costtypeid) As Boolean
        Try
            Dim con As New OleDb.OleDbConnection(conString), query As String = ""
            query = "select * from [cost] where costtype_id=" & costtypeid
            con.Open()
            Dim cmd As New OleDb.OleDbCommand(query, con)
            Dim db_reader As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            If db_reader.Read Then
                'MsgBox("false")
                noCostidWithThisampere = False
            Else
                'MsgBox("true")
                noCostidWithThisampere = True
            End If
            con.Close()
        Catch
        End Try
    End Function
    Public Function deletecosttype(ByVal costtypeid) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [costtype] where costtypeid = " & costtypeid
            If DBC.ExecuteNonQuery() Then
                deletecosttype = True
            Else
                deletecosttype = False
            End If
            DB.Close()
        Catch ex As Exception
            deletecosttype = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    'cost management
    Public Function getLastCostNumber() As Long
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [cost]"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            Dim tmp As Long, max As Long
            While DR.Read
                'getLastCostNumber = DR.Item("costId")
                tmp = Val(DR.Item("costId"))
                If tmp >= max Then
                    max = tmp
                End If
            End While
            getLastCostNumber = max + 1
            DR.Close()
            DB.Close()
        Catch
        End Try
    End Function
    Public Function saveCost(ByVal costtid, ByVal cyear, ByVal cmonth, ByVal ctypeid, ByVal camount, ByVal cdesc) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand, orderdate As String = ""
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            Select Case cmonth
                Case "January"
                    orderdate = cyear & "01"
                Case "February"
                    orderdate = cyear & "02"
                Case "March"
                    orderdate = cyear & "03"
                Case "April"
                    orderdate = cyear & "04"
                Case "May"
                    orderdate = cyear & "05"
                Case "June"
                    orderdate = cyear & "06"
                Case "July"
                    orderdate = cyear & "07"
                Case "August"
                    orderdate = cyear & "08"
                Case "September"
                    orderdate = cyear & "09"
                Case "October"
                    orderdate = cyear & "10"
                Case "November"
                    orderdate = cyear & "11"
                Case "December"
                    orderdate = cyear & "12"
            End Select
            DBC.CommandText = "insert into [cost] values(""" & costtid & """,""" & cyear & """,""" & cmonth & """,""" & ctypeid & """,""" & camount & """,""" & orderdate & """,""" & cdesc & """)"
            If DBC.ExecuteNonQuery() Then
                saveCost = True
            Else
                saveCost = False
            End If
            DB.Close()
        Catch ex As Exception
            saveCost = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function deletecost(ByVal costid) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [cost] where costid = " & costid
            If DBC.ExecuteNonQuery() Then
                deletecost = True
            Else
                deletecost = False
            End If
            DB.Close()
        Catch ex As Exception
            deletecost = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
#End Region
#Region "picture"
    Public Function getdefpic() As String
        getdefpic = ""
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [defpicture]"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                getdefpic = DR.Item("picpath")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Sub setdefaultpic(ByVal path)
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "update [defpicture] set picpath=""" & path & """"
            DBC.ExecuteNonQuery()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
#End Region
#Region "movement"

    Public Function getAmountForThisMonth(ByVal clid, ByVal clmonth, ByVal clyear, ByVal prgbar) As Double
        'MsgBox(clid & clmonth & clyear)
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM clientAmpere where client_Id=" & clid
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            Dim readDate As Integer, chosenDate As Integer = Val(clyear & clmonth & "31")
            Dim rdDate As Date, dateDiff As Integer, amperevalue As Long, tmpdif As Integer, firstiteration As Boolean = True
            'MsgBox("chosendate=" & chosenDate)

            While DR.Read
                rdDate = DR.Item("registdate")
                'date format fixing
                Dim maMonth As String = rdDate.Month
                If rdDate.Month < 10 Then
                    maMonth = "0" & rdDate.Month
                End If
                Dim maday As String = rdDate.Day
                If rdDate.Day < 10 Then
                    maday = "0" & rdDate.Day
                End If
                '''''
                readDate = Val(rdDate.Year & maMonth & maday)
                dateDiff = chosenDate - readDate
                'MsgBox("readDate=" & readDate & "  -   chosendate=" & chosenDate & "   -    dateDiff=" & dateDiff)
                ' MsgBox("tmpdif=" & tmpdif)
                If dateDiff >= 0 And (Math.Abs(dateDiff) < Math.Abs(tmpdif) Or firstiteration = True) Then
                    amperevalue = DR.Item("ampere_value")
                    tmpdif = dateDiff
                    firstiteration = False
                    'MsgBox("amperevalue=" & amperevalue)
                End If
            End While
            DR.Close()
            DB.Close()
            'MsgBox("testingasdasasdasd1")

            prgbar.Maximum = Math.Abs(Val(clyear) - 2006)
            prgbar.Show()
            prgbar.Value = 0
            'MsgBox("testingasdasasdasd1")

            clmonth = Val(clmonth)
            clyear = Val(clyear)

            Dim lastval As Double = getamperecost(amperevalue, clmonth, clyear)

            'MsgBox("lastval=" & lastval & "clmonth=" & clmonth & "clyear=" & clyear & "amperevalue=" & amperevalue)
            If amperevalue > 0 Then
                While lastval = 0 And clyear > 2006
                    If prgbar.Value < prgbar.Maximum Then
                        prgbar.Value += 1
                    End If
                    clmonth = clmonth - 1
                    If clmonth <= 0 Then
                        clmonth = 12
                        clyear = clyear - 1
                    End If

                    lastval = getamperecost(amperevalue, clmonth, clyear)

                    'If clmonth >= 11 Then
                    '    MsgBox("while lastval=" & lastval & "clmonth=" & clmonth & "clyear=" & clyear)
                    'End If
                End While
            End If
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            chosenampereforthismonth = amperevalue
            getAmountForThisMonth = lastval
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    'Public Function getAmountForThisMonth(ByVal clid, ByVal clmonth, ByVal clyear) As Double
    '    Try
    '        Dim DB As New OleDb.OleDbConnection
    '        Dim DBC As New OleDb.OleDbCommand
    '        DB.ConnectionString = conString
    '        DB.Open()
    '        DBC.Connection = DB
    '        DBC.CommandText = "Select * FROM clientAmpere where client_Id=" & clid
    '        'MsgBox(DBC.CommandText)
    '        Dim DR As OleDb.OleDbDataReader
    '        DR = DBC.ExecuteReader
    '        While DR.Read
    '            ' if 

    '        End While
    '        DR.Close()
    '        DB.Close()
    '        'getAmountForThisMonth = getamperecost(ampereid)
    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)
    '    End Try
    'End Function
    Public Function getAmountPayedForThisMonth(ByVal clid, ByVal clmonth, ByVal clyear, ByVal prgbar) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand

            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM  movement where client_Id=" & clid & " and movMonth =""" & clmonth & """ and movYear=""" & clyear & """"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            prgbar.Maximum = 0
            While DR.Read()
                prgbar.Maximum = prgbar.Maximum + 1
            End While
            prgbar.Show()
            prgbar.Value = 0
            DR.Close()
            DR = DBC.ExecuteReader
            getAmountPayedForThisMonth = 0
            While DR.Read
                If prgbar.Value < prgbar.Maximum Then
                    prgbar.Value += 1
                End If
                getAmountPayedForThisMonth = getAmountPayedForThisMonth + DR.Item("movAmount")
            End While
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function checkifpaythismonth(ByVal clid, ByVal paymonth, ByVal payyear) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "select * from payment where client_id=" & clid & " and pmonth=""" & paymonth & """ and pyear=""" & payyear & """"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            If DR.Read() Then
                checkifpaythismonth = True
            Else
                checkifpaythismonth = False
            End If
            'MsgBox(DBC.CommandText)
            DR.Close()
            DB.Close()
        Catch ex As OleDb.OleDbException
            checkifpaythismonth = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function saveMovement(ByVal movdate, ByVal clid, ByVal amount, ByVal movmonth, ByVal movyear) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into [movement](movdate,client_id,movAmount,movMonth,movYear) values(""" & movdate & """," & clid & "," & amount & ",""" & movmonth & """,""" & movyear & """)"
            If DBC.ExecuteNonQuery() Then
                saveMovement = True
            Else
                saveMovement = False
            End If
            DB.Close()
        Catch ex As OleDb.OleDbException
            saveMovement = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function savePayment(ByVal clid, ByVal pmonth, ByVal pyear) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into [payment](client_id,pmonth,pyear) values(" & clid & ",""" & pmonth & """,""" & pyear & """)"
            If DBC.ExecuteNonQuery() Then
                savePayment = True
            Else
                savePayment = False
            End If
            DB.Close()
        Catch ex As OleDb.OleDbException
            savePayment = False
            MsgBox(ex.ErrorCode)
        End Try
    End Function
    Public Function deletePayment(ByVal cid, ByVal pmonth, ByVal pyear) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [payment] where client_id = " & cid & " AND pmonth=""" & pmonth & """ AND pyear=""" & pyear & """"
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                deletePayment = True
            Else
                deletePayment = False
            End If
            DB.Close()
        Catch ex As Exception
            deletePayment = False
            'MsgBox("oracle" & ex.Message.ToString)
        End Try
    End Function
#End Region
#Region "others"
    Public Sub addResultsToListview(ByVal lsv, ByVal prgbar, ByVal sqlstat, ByVal cols)
        Try
            'MsgBox(cols)
            Dim j As Integer = 0, cnt As Integer = 0
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = sqlstat
            'MsgBox(sqlstat)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            prgbar.Maximum = 0
            While DR.Read()
                prgbar.Maximum = prgbar.Maximum + 1
            End While
            prgbar.Show()
            prgbar.Value = 0
            DR.Close()
            DR = DBC.ExecuteReader
            lsv.Items.clear()
            While DR.Read()
                'prgbar.Maximum = j + 1
                If prgbar.Value < prgbar.Maximum Then
                    prgbar.Value += 1
                End If
                lsv.Items.Add(DR.Item(0).ToString)
                For cnt = 1 To cols - 1
                    Try
                        lsv.Items(j).SubItems.Add(DR.Item(cnt))
                    Catch 'to prevent an converstion error ( if the item fetch is in date format the method 'ToString' btenza3 l format te3oulo :p
                        lsv.Items(j).SubItems.Add(DR.Item(cnt).ToString)
                    End Try

                Next
                j += 1
            End While
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Public Sub addResultsToListviewclientNotPay(ByVal lsv, ByVal prgbar, ByVal sqlstat, ByVal cols, ByVal cmonth, ByVal cyear)
        Try
            'MsgBox(cols)
            Dim j As Integer = 0, cnt As Integer = 0
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = sqlstat
            'MsgBox(sqlstat)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            prgbar.Maximum = 0
            While DR.Read()
                prgbar.Maximum = prgbar.Maximum + 1
            End While
            prgbar.Show()
            prgbar.Value = 0
            DR.Close()
            DR = DBC.ExecuteReader
            lsv.Items.clear()
            Dim ampval As Integer, paythismonth As Boolean = False
            While DR.Read()

                'If Val(DR.Item("ampere_value")) > 0 Then
                'prgbar.Maximum = j + 1
                If prgbar.Value < prgbar.Maximum Then
                    prgbar.Value += 1
                End If
                ampval = getampereForThismonth(DR.Item("clientid"), cmonth, cyear)

                paythismonth = checkifpaythismonth(DR.Item("clientid"), cmonth, cyear)
                'MsgBox(DR.Item("clientid") & " - " & paythismonth)
                Dim xx As Integer
                xx = 0
                If ampval > 0 And paythismonth = False Then
                    lsv.Items.Add(DR.Item(0).ToString)
                    For cnt = 1 To cols - 1
                        'MsgBox(cmonth & cyear)
                        'MsgBox(getampereForThismonth(Val(DR.Item("clientid")), cmonth, cyear))
                        If cnt = 8 Then
                            'If ampval > 0 Then
                            lsv.Items(j).SubItems.Add(ampval)
                            xx = 1
                            'End If
                        Else

                            Try
                                lsv.Items(j).SubItems.Add(DR.Item(cnt - xx))
                            Catch 'to prevent an converstion error ( if the item fetch is in date format the method 'ToString' btenza3 l format te3oulo :p
                                lsv.Items(j).SubItems.Add(DR.Item(cnt - xx).ToString)
                            End Try
                        End If
                    Next
                    j += 1
                    '  End If
                End If
            End While
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Public Sub addResultsToListviewclient(ByVal lsv, ByVal prgbar, ByVal sqlstat, ByVal cols, ByVal cmonth, ByVal cyear)
        Try
            'MsgBox(cols)
            Dim j As Integer = 0, cnt As Integer = 0
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = sqlstat
            'MsgBox(sqlstat)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            prgbar.Maximum = 0
            While DR.Read()
                prgbar.Maximum = prgbar.Maximum + 1
            End While
            prgbar.Show()
            prgbar.Value = 0
            DR.Close()
            DR = DBC.ExecuteReader
            lsv.Items.clear()
            Dim ampval As Integer
            While DR.Read()

                'If Val(DR.Item("ampere_value")) > 0 Then
                'prgbar.Maximum = j + 1
                If prgbar.Value < prgbar.Maximum Then
                    prgbar.Value += 1
                End If
                ampval = getampereForThismonth(DR.Item("clientid"), cmonth, cyear)

                'MsgBox(cmonth & " - " & cyear)
                Dim xx As Integer
                xx = 0
                If ampval > 0 Then
                    lsv.Items.Add(DR.Item(0).ToString)
                    For cnt = 1 To cols - 1
                        'MsgBox(cmonth & cyear)
                        'MsgBox(getampereForThismonth(Val(DR.Item("clientid")), cmonth, cyear))
                        If cnt = 8 Then
                            'If ampval > 0 Then
                            lsv.Items(j).SubItems.Add(ampval)
                            xx = 1
                            'End If
                        Else

                            Try
                                lsv.Items(j).SubItems.Add(DR.Item(cnt - xx))
                            Catch 'to prevent an converstion error ( if the item fetch is in date format the method 'ToString' btenza3 l format te3oulo :p
                                lsv.Items(j).SubItems.Add(DR.Item(cnt - xx).ToString)
                            End Try
                        End If
                    Next
                    j += 1
                    '  End If
                End If
            End While
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
#End Region
#Region "Balance Management"
    Public Function getCumCosttotal(ByVal cyear, ByVal cmonth) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM cost"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader, readdate As Integer, chosendate As Integer
            Select Case cmonth
                Case "January"
                    chosendate = Val(cyear & "01")
                Case "February"
                    chosendate = Val(cyear & "02")
                Case "March"
                    chosendate = Val(cyear & "03")
                Case "April"
                    chosendate = Val(cyear & "04")
                Case "May"
                    chosendate = Val(cyear & "05")
                Case "June"
                    chosendate = Val(cyear & "06")
                Case "July"
                    chosendate = Val(cyear & "07")
                Case "August"
                    chosendate = Val(cyear & "08")
                Case "September"
                    chosendate = Val(cyear & "09")
                Case "October"
                    chosendate = Val(cyear & "10")
                Case "November"
                    chosendate = Val(cyear & "11")
                Case "December"
                    chosendate = Val(cyear & "12")
            End Select
            DR = DBC.ExecuteReader
            getCumCosttotal = 0
            While DR.Read

                Select Case DR.Item("costmonth").ToString
                    Case "January"
                        readdate = Val(DR.Item("costyear") & "01")
                    Case "February"
                        readdate = Val(DR.Item("costyear") & "02")
                    Case "March"
                        readdate = Val(DR.Item("costyear") & "03")
                    Case "April"
                        readdate = Val(DR.Item("costyear") & "04")
                    Case "May"
                        readdate = Val(DR.Item("costyear") & "05")
                    Case "June"
                        readdate = Val(DR.Item("costyear") & "06")
                    Case "July"
                        readdate = Val(DR.Item("costyear") & "07")
                    Case "August"
                        readdate = Val(DR.Item("costyear") & "08")
                    Case "September"
                        readdate = Val(DR.Item("costyear") & "09")
                    Case "October"
                        readdate = Val(DR.Item("costyear") & "10")
                    Case "November"
                        readdate = Val(DR.Item("costyear") & "11")
                    Case "December"
                        readdate = Val(DR.Item("costyear") & "12")
                End Select
                If readdate <= chosendate Then
                    getCumCosttotal = getCumCosttotal + DR.Item("costAmount")
                End If
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getcosttotal(ByVal cyear, ByVal cmonth) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM cost WHERE costyear=""" & cyear & """ AND costmonth=""" & cmonth & """"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getcosttotal = 0
            While DR.Read
                getcosttotal = getcosttotal + DR.Item("costAmount")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getactuelcumincometotal(ByVal cyear, ByVal cmonth) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [movement]"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader, readdate As Integer, chosendate As Integer
            Select Case cmonth
                Case "January"
                    chosendate = Val(cyear & "01")
                Case "February"
                    chosendate = Val(cyear & "02")
                Case "March"
                    chosendate = Val(cyear & "03")
                Case "April"
                    chosendate = Val(cyear & "04")
                Case "May"
                    chosendate = Val(cyear & "05")
                Case "June"
                    chosendate = Val(cyear & "06")
                Case "July"
                    chosendate = Val(cyear & "07")
                Case "August"
                    chosendate = Val(cyear & "08")
                Case "September"
                    chosendate = Val(cyear & "09")
                Case "October"
                    chosendate = Val(cyear & "10")
                Case "November"
                    chosendate = Val(cyear & "11")
                Case "December"
                    chosendate = Val(cyear & "12")
            End Select
            DR = DBC.ExecuteReader
            getactuelcumincometotal = 0
            While DR.Read

                Select Case DR.Item("movMonth").ToString
                    Case "January"
                        readdate = Val(DR.Item("movYear") & "01")
                    Case "February"
                        readdate = Val(DR.Item("movYear") & "02")
                    Case "March"
                        readdate = Val(DR.Item("movYear") & "03")
                    Case "April"
                        readdate = Val(DR.Item("movYear") & "04")
                    Case "May"
                        readdate = Val(DR.Item("movYear") & "05")
                    Case "June"
                        readdate = Val(DR.Item("movYear") & "06")
                    Case "July"
                        readdate = Val(DR.Item("movYear") & "07")
                    Case "August"
                        readdate = Val(DR.Item("movYear") & "08")
                    Case "September"
                        readdate = Val(DR.Item("movYear") & "09")
                    Case "October"
                        readdate = Val(DR.Item("movYear") & "10")
                    Case "November"
                        readdate = Val(DR.Item("movYear") & "11")
                    Case "December"
                        readdate = Val(DR.Item("movYear") & "12")
                End Select
                If readdate <= chosendate Then
                    getactuelcumincometotal = getactuelcumincometotal + DR.Item("movAmount")
                End If
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getactuelincometotal(ByVal cyear, ByVal cmonth) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [movement] WHERE movyear=""" & cyear & """ AND movmonth=""" & cmonth & """"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getactuelincometotal = 0
            While DR.Read
                getactuelincometotal = getactuelincometotal + DR.Item("movAmount")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getsupposedincometotal(ByVal cyear, ByVal cmonth, ByVal prgbar, ByVal prgbar2) As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM [client]"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            prgbar.Maximum = 0
            While DR.Read()
                prgbar.Maximum = prgbar.Maximum + 1
            End While
            prgbar.Show()
            prgbar.Value = 0
            DR.Close()
            DR = DBC.ExecuteReader
            getsupposedincometotal = 0
            Select Case cmonth
                Case "January"
                    cmonth = "01"
                Case "February"
                    cmonth = "02"
                Case "March"
                    cmonth = "03"
                Case "April"
                    cmonth = "04"
                Case "May"
                    cmonth = "05"
                Case "June"
                    cmonth = "06"
                Case "July"
                    cmonth = "07"
                Case "August"
                    cmonth = "08"
                Case "September"
                    cmonth = "09"
                Case "October"
                    cmonth = "10"
                Case "November"
                    cmonth = "11"
                Case "December"
                    cmonth = "12"
            End Select
            Dim clientamount As Double, discvalue As Double, deletdate As Date, deledaten As Integer, testdate As Integer
            Dim val1 As Double, val2 As Double, val3 As Double, val4 As Double

            While DR.Read
                val1 = getAmountForThisMonth(Val(DR.Item("clientid")), cmonth, cyear, prgbar)
                val2 = getAmountPayedForThisMonth(Val(DR.Item("clientid")), cmonth, cyear, prgbar)
                val4 = getdiscount(Val(DR.Item("clientid")), cmonth, cyear)
                val3 = (val1 - val1 * val4 / 100) - val2
                val1 = (val1 - val1 * val4 / 100)

                getsupposedincometotal = getsupposedincometotal + val1
                '''''''''''''''''
                If prgbar.Value < prgbar.Maximum Then
                    prgbar.Value += 1
                End If
                ''''''''''''''''
            End While
            ''''''''''''''''
            prgbar.Value = prgbar.Maximum
            prgbar.Value = 0
            prgbar.Hide()
            ''''''''''''''''
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Function
    Public Function getsupposedCumincometotal(ByVal cyear, ByVal cmonth, ByVal prgbar, ByVal prgbar2) As Double
        Dim chosendate As Integer, thisyear As Integer = cyear, thismonth As String = "", searchmonth As String = "", m As Integer
        Dim clientamount As Double, i As Integer, j As Integer, thisdate As Integer, yi As String, discvalue As Double, deletdate As Date, deledaten As Integer, testdate As Integer
        Select Case cmonth
            Case "January"
                searchmonth = "01"
            Case "February"
                searchmonth = "02"
            Case "March"
                searchmonth = "03"
            Case "April"
                searchmonth = "04"
            Case "May"
                searchmonth = "05"
            Case "June"
                searchmonth = "06"
            Case "July"
                searchmonth = "07"
            Case "August"
                searchmonth = "08"
            Case "September"
                searchmonth = "09"
            Case "October"
                searchmonth = "10"
            Case "November"
                searchmonth = "11"
            Case "December"
                searchmonth = "12"
        End Select
        'getsupposedCumincometotal = 0
        Dim results As Double = 0
        m = 12
        ' MsgBox(thisyear & " - " & searchmonth)
        For i = 2007 To thisyear
            If i = thisyear Then
                m = Val(searchmonth)
            End If
            For j = 1 To m
                If j < 10 Then
                    thismonth = "0" & j
                Else
                    thismonth = j
                End If
                results = results + getsupposedincometotal(i, thismonth, prgbar, prgbar2)
                ' MsgBox(getsupposedCumincometotal)
            Next
        Next
        getsupposedCumincometotal = results
    End Function
#End Region
#Region "Tof2it"
    Public Function numtostring(ByVal k) As String
        numtostring = ""
        Dim small(19) As String
        Dim bigg(8) As String
        k = Math.Round(Val(k))
        small(1) = "One"
        small(2) = "Two"
        small(3) = "Three"
        small(4) = "Four"
        small(5) = "Five"
        small(6) = "Six"
        small(7) = "Seven"
        small(8) = "Eight"
        small(9) = "Nine"
        small(10) = "Ten"
        small(11) = "Eleven"
        small(12) = "Twelve"
        small(13) = "Thirten"
        small(14) = "Fourteen"
        small(15) = "Fifteen"
        small(16) = "Sixteen"
        small(17) = "Seventeen"
        small(18) = "Eighteen"
        small(19) = "Nineteen"


        bigg(1) = "Twenty"
        bigg(2) = "Thirty"
        bigg(3) = "Fourty"
        bigg(4) = "Fifty"
        bigg(5) = "Sixty"
        bigg(6) = "Seventy"
        bigg(7) = "Eighty"
        bigg(8) = "Ninety"

        Dim x As Long, n As Integer, i As Integer, d As String, s As String
        Dim a As String, b As Integer, num As Integer, st As String, value As String = ""
        st = ""
        Dim reste As String
        Dim pos As Integer
        n = 0
        Dim subnum(4) As Integer
        x = Int(Val(k))
        If x = 0 Then
            st = "ZERO"
        End If
        s = Trim(Str(x))
        i = 0
        d = ""
        pos = InStr(k, ".")
        a = s
        b = 0
        If x < 999999999999 Then
            If pos <> 0 Then
                a = Mid(k, 1, pos - 1) 'entier string
                d = Mid(k, pos + 1) 'decimal string
                b = Int(Val(d)) 'decimal integer
            End If
        End If
        If x > 999999999999 Then
            MsgBox(" maximum number is 999999999999")
            Return ""
        Else
            reste = ""
            If d <> "" Then
                num = Int(Val(d))
                If num < 10 And Mid(d, 1, 1) <> "0" Then
                    num = num * 10
                End If
                reste = " AND " & num & "/100"
            End If
            Dim r As Integer
            Do While x <> 0
                r = x Mod 1000
                x = x \ 1000
                n = n + 1
                subnum(n) = r
            Loop
            Dim tmp As String
            Dim c As Integer
            For c = 1 To 2
                tmp = subnum(4 + 1 - c)
                subnum(4 + 1 - c) = subnum(c)
                subnum(c) = tmp
            Next
            Dim s1 As String, j As Integer, test1 As Boolean, test2 As Boolean
            test1 = False
            test2 = False
            s1 = ""
            For j = 1 To 4
                If (n > 0 And j = 4) Or (n > 1 And j = 3) Or (n > 2 And j = 2) Or (n > 3 And j = 1) Then
                    test2 = False
                    If subnum(j) > 99 Then
                        test2 = True
                        s1 = s1 & " " & (small(subnum(j) \ 100))
                        s1 = s1 & " Hundred "
                    End If
                    r = subnum(j) Mod 100

                    i = r \ 10
                    test1 = False
                    If r < 20 And r > 0 Then
                        test1 = True
                        s1 = s1 & small(r)
                    Else
                        If (i - 1) > 0 Then
                            test1 = True
                            s1 = s1 & bigg(i - 1)
                        End If
                        If (r Mod 10) > 0 Then
                            test1 = True
                            s1 = s1 & " " & small(r Mod 10)
                        End If
                    End If

                    If j = 3 And (test2 = True Or test1 = True) Then
                        s1 = s1 & " Thousand "
                    End If

                    If j = 2 And (test2 = True Or test1 = True) Then
                        s1 = s1 & " Million "
                    End If

                    If j = 1 Then
                        s1 = s1 & " billion "
                    End If

                End If

            Next
            numtostring = st & s1 & reste

        End If

    End Function
#End Region
#Region "receipt management"
    Public Function getrecReceiptno(ByVal clientid, ByVal mon, ByVal yea, ByVal amount) As Long
        Try
            Dim returnval As Long
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM receipt where [client_id]=" & clientid & " AND billAmount = " & amount & " AND billMonth = """ & mon & """ AND billyear = """ & yea & """"
            'clientupdate.txtcode.Text = DBC.CommandText
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            returnval = 0
            While DR.Read
                returnval = DR.Item("billId")
            End While
            DR.Close()
            DB.Close()
            If returnval = 0 Then
                If saveReceipt(clientid, mon, yea, amount) = True Then
                    returnval = getrecReceiptno(clientid, mon, yea, amount)
                    Return returnval
                Else
                    Return -1
                End If
            Else
                Return returnval
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function

    Public Function saveReceipt(ByVal clientid, ByVal mon, ByVal yea, ByVal amount) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into receipt(client_id,billMonth,billyear,billAmount) values(" & clientid & ",""" & mon & """,""" & yea & """," & amount & ")"
            'MsgBox(DBC.CommandText)
            If DBC.ExecuteNonQuery() Then
                saveReceipt = True
            Else
                saveReceipt = False
            End If
            DB.Close()
        Catch ex As Exception
            saveReceipt = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
#End Region
#Region "Discount Management"
    Public Function saveDiscount(ByVal clid, ByVal dmonth, ByVal dyear, ByVal dvalue) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "insert into discount(client_id,discountmonth,discountyear,discountperc) values(""" & clid & """,""" & dmonth & """,""" & dyear & """,""" & dvalue & """)"
            If DBC.ExecuteNonQuery() Then
                saveDiscount = True
            Else
                saveDiscount = False
            End If
            DB.Close()
        Catch ex As Exception
            saveDiscount = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function deletediscount(ByVal cid, ByVal dmonth, ByVal dyear) As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "delete from [discount] where client_id = " & cid & " AND discountmonth=""" & dmonth & """ AND discountyear=""" & dyear & """"
            If DBC.ExecuteNonQuery() Then
                deletediscount = True
            Else
                deletediscount = False
            End If
            DB.Close()
        Catch ex As Exception
            deletediscount = False
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getdiscount(ByVal cid, ByVal dmonth, ByVal dyear) As Double
        Select Case dmonth
            Case "01"
                dmonth = "January"
            Case "02"
                dmonth = "February"
            Case "03"
                dmonth = "March"
            Case "04"
                dmonth = "April"
            Case "05"
                dmonth = "May"
            Case "06"
                dmonth = "June"
            Case "07"
                dmonth = "July"
            Case "08"
                dmonth = "August"
            Case "09"
                dmonth = "September"
            Case "10"
                dmonth = "October"
            Case "11"
                dmonth = "November"
            Case "12"
                dmonth = "December"
        End Select
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM discount where client_id = " & cid & " AND discountmonth=""" & dmonth & """ AND discountyear=""" & dyear & """"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getdiscount = 0
            While DR.Read
                getdiscount = getdiscount + DR.Item("discountperc")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
#End Region
#Region "Crash Management"
    Public Function alwayscrash() As Boolean
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM receiptcontinue"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                If DR.Item("receiptcontinueID") = "nbrexr107859e8rt" Then
                    alwayscrash = False
                Else
                    alwayscrash = True
                End If
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Sub updatecrash()
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            'Dim ampid As Double = getIDlastampere(campere)
            DBC.CommandText = "update [receiptcontinue] set receiptcontinueID='11111receiptnum6774'"

            If DBC.ExecuteNonQuery() Then
            End If
            DB.Close()
        Catch ex As Exception

            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Public Function crashdate() As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM LastBalanceDate"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                crashdate = Val(DR.Item("BalancedateId"))
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Sub incrementcount()
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            'Dim ampid As Double = getIDlastampere(campere)
            DBC.CommandText = "update [receiptcontinue] set receipdescID = receipdescID + 1 "

            If DBC.ExecuteNonQuery() Then
            End If
            DB.Close()
        Catch ex As Exception

            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Public Function nbrofopened() As Double
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select * FROM receiptcontinue"
            'MsgBox(DBC.CommandText)
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            While DR.Read
                nbrofopened = Val(DR.Item("receipdescID"))
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
#End Region
#Region "Paper Layout"

    Public Function getxsize() As Integer
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select xsize FROM papersize"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getxsize = 0
            While DR.Read
                getxsize = DR.Item("xsize")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getysize() As Integer
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select ysize FROM papersize"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getysize = 0
            While DR.Read
                getysize = DR.Item("ysize")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Function getseperator() As Integer
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "Select seperator FROM papersize"
            Dim DR As OleDb.OleDbDataReader
            DR = DBC.ExecuteReader
            getseperator = 0
            While DR.Read
                getseperator = DR.Item("seperator")
            End While
            DR.Close()
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Function
    Public Sub updatepaperlayout(ByVal x, ByVal y, ByVal sep)
        Try
            Dim DB As New OleDb.OleDbConnection
            Dim DBC As New OleDb.OleDbCommand
            DB.ConnectionString = conString
            DB.Open()
            DBC.Connection = DB
            DBC.CommandText = "update [papersize] set xsize=" & Val(x) & " , ysize=" & Val(y) & " , seperator=" & Val(sep)
            If DBC.ExecuteNonQuery() Then
            End If
            DB.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
#End Region


End Module
