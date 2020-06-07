Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

Public Class TestVb
	Friend Const BUNDLE As String = "testJDBC"
	Friend Shared bundle As PropertyResourceBundle = Nothing

	Shared Sub Main(ByVal args() As String)

		Try
            Dim fstream As New FileInputStream(args(0))
		  ' Get the object of DataInputStream
		  Dim [in] As New DataInputStream(fstream)
		  Dim br As New BufferedReader(New InputStreamReader([in]))
		  Dim strLine As String
		  Dim vAttribut As New ArrayList()
		  Dim vMethod As New ArrayList()


		  'Read File Line By Line
		  strLine = br.readLine()
		  Do While strLine IsNot Nothing
		  ' Print the content on the console
			  Dim firstSpaceIndex As Integer = strLine.IndexOf(" ")
			  Dim attribut As String = strLine.Substring(0,firstSpaceIndex).Trim()
			  strLine = strLine.Substring(firstSpaceIndex).Trim()

			  Dim attributType As String=""
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
				  attributType = "Decimal"
			  End If

			  Dim attributStr As String = "Private " & attribut & "Att As " & attributType & vbCrLf

			  vAttribut.Add(attributStr)

			  'method
			  Dim propertyStr As String = vbCrLf & "Property " & attribut & " As " & attributType & vbCrLf
			  propertyStr &= "Get " & vbCrLf
			  propertyStr &= "Return " & attribut & "Att " & vbCrLf
			  propertyStr &= "End Get " & vbCrLf
			  propertyStr &= "Set(ByVal Value As " & attributType & ") " & vbCrLf
			  propertyStr += attribut & "Att = Value " & vbCrLf
			  propertyStr &= "End Set " & vbCrLf
			  propertyStr &= "End Property " & vbCrLf

			  vMethod.Add(propertyStr)
			  strLine = br.readLine()
		  Loop

		  Dim attribut(vAttribut.Count - 1) As String
		  vAttribut.CopyTo(attribut)
		  Dim method(vMethod.Count - 1) As String
		  vMethod.CopyTo(method)

		  'Close the input stream
		  [in].close()

		  Try
			  ' Create file 
			  Dim fstreamWrite As New FileWriter(args(0) & "Class.txt")
			  Dim out As New BufferedWriter(fstreamWrite)
			  For i As Integer = 0 To attribut.Length - 1
				  out.write(attribut(i))
			  Next i
			  For i As Integer = 0 To method.Length - 1
				  out.write(method(i))
			  Next i
			  'Close the output stream
			  out.close() 'Catch exception if any
		  Catch e As Exception
			  System.err.println("Error: " & e.Message)
		  End Try

 'Catch exception if any
		Catch e As Exception
			System.err.println("Error: " & e.Message)
		End Try

	End Sub

End Class
