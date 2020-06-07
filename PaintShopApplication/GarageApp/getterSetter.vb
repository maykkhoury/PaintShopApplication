Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.IO
Public Class getterSetter

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try


            Dim fs = New FileStream(txtPath.Text, FileMode.Open, FileAccess.Read)

            Dim d As New StreamReader(fs)
            'creating a new StreamReader and passing the filestream object fs as argument
            d.BaseStream.Seek(0, SeekOrigin.Begin)
            'Seek method is used to move the cursor to different positions in a file, in this code, to
            'the beginning
            Dim vAttribut As New ArrayList()
            Dim vMethod As New ArrayList()

            While d.Peek() > -1
                Dim strLine As String = d.ReadLine().Trim()

                'peek method of StreamReader object tells how much more data is left in the file

                ' Print the content on the console
                Dim firstSpaceIndex As Integer = strLine.IndexOf(" ")
                Dim attrib As String = strLine.Substring(0, firstSpaceIndex).Trim()
                strLine = strLine.Substring(firstSpaceIndex).Trim()

                Dim attributType As String = ""
                If strLine.ToUpper().IndexOf("LONG") <> -1 OrElse strLine.ToUpper().IndexOf("INTEGER") <> -1 Then
                    attributType = "Integer"
                End If

                If strLine.ToUpper().IndexOf("CHAR") <> -1 Then
                    attributType = "String"
                End If
                If strLine.ToUpper().IndexOf("DATE") <> -1 Then
                    attributType = "Date"
                End If

                If strLine.ToUpper().IndexOf("DOUBLE") <> -1 Then
                    attributType = "Double"
                End If

                Dim attributStr As String = "Private " & attrib & "Att As " & attributType & vbCrLf

                vAttribut.Add(attributStr)

                'method
                Dim propertyStr As String = vbCrLf & "Property " & attrib & " As " & attributType & vbCrLf
                propertyStr &= "Get " & vbCrLf
                propertyStr &= "Return " & attrib & "Att " & vbCrLf
                propertyStr &= "End Get " & vbCrLf
                propertyStr &= "Set(ByVal Value As " & attributType & ") " & vbCrLf
                propertyStr += attrib & "Att = Value " & vbCrLf
                propertyStr &= "End Set " & vbCrLf
                propertyStr &= "End Property " & vbCrLf

                vMethod.Add(propertyStr)
            End While


            Dim attribut(vAttribut.Count - 1) As String
            vAttribut.CopyTo(attribut)
            Dim method(vMethod.Count - 1) As String
            vMethod.CopyTo(method)

            'Close the input stream
            d.Close()


            Try
                ' Create file 
                fs = New FileStream(txtPath.Text & ".Class.txt", FileMode.Create, FileAccess.Write)
                'declaring a FileStream and creating a word document file named file with
                'access mode of writing
                Dim s As New StreamWriter(fs)
                'creating a new StreamWriter and passing the filestream object fs as argument
                s.BaseStream.Seek(0, SeekOrigin.End)
                'the seek method is used to move the cursor to next position to avoid text to be
                'overwritten
                'writing text to the newly created file

                For i As Integer = 0 To attribut.Length - 1
                    s.WriteLine(attribut(i))
                Next i
                For i As Integer = 0 To method.Length - 1
                    s.WriteLine(method(i))
                Next i
                'Close the output stream
                s.Close() 'Catch exception if any
            Catch e1 As Exception
                MsgBox("Error: " & e1.Message)
            End Try

            'Catch exception if any
        Catch e2 As Exception
            MsgBox("Error: " & e2.Message)
        End Try

    End Sub

End Class
