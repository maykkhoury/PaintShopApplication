Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.IO
Public Class getlabelsForm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try


            Dim fs = New FileStream("jla3.txt", FileMode.Open, FileAccess.Read)

            Dim d As New StreamReader(fs)
            'creating a new StreamReader and passing the filestream object fs as argument
            d.BaseStream.Seek(0, SeekOrigin.Begin)
            'Seek method is used to move the cursor to different positions in a file, in this code, to
            'the beginning

            Dim vLines As New ArrayList()

            While d.Peek() > -1
                Dim strLine As String = d.ReadLine().Trim()

                'peek method of StreamReader object tells how much more data is left in the file

                '

                If strLine.IndexOf(".Text ") >= 0 Then
                    If strLine.IndexOf("Me.Text") >= 0 Then
                        strLine = strLine.Replace("Me.Text", "garageHome.Text")
                    End If
                    strLine = strLine.Replace("Me.", "")
                    vLines.Add(strLine)
                End If


            End While

            Dim lines(vLines.Count - 1) As String
            vLines.CopyTo(lines)

            'Close the input stream
            d.Close()


            Try
                ' Create file 
                fs = New FileStream("jla3output.txt", FileMode.Create, FileAccess.Write)
                'declaring a FileStream and creating a word document file named file with
                'access mode of writing
                Dim s As New StreamWriter(fs)
                'creating a new StreamWriter and passing the filestream object fs as argument
                s.BaseStream.Seek(0, SeekOrigin.End)
                'the seek method is used to move the cursor to next position to avoid text to be
                'overwritten
                'writing text to the newly created file

                For i As Integer = 0 To lines.Length - 1


                    Dim str As String = lines(i)
                    Dim label_name As String
                    label_name = str.Substring(0, str.IndexOf(".Text"))

                    Dim value As String
                    value = str.Substring(str.IndexOf("= """) + 3)
                    value = value.Trim.Substring(0, value.Length - 1)
                    addLabel(label_name, value, 1)

                    Dim line As String = lines(i)
                    line = line.Substring(0, line.IndexOf("= """))
                    line = line & "= getLabelDescription(""" & label_name & """)"
                    s.WriteLine(line)

                Next i
                'fs.Close()
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
