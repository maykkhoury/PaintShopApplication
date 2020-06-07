Imports System

Public Class TestJDBC
	Friend Const BUNDLE As String = "testJDBC"
	Friend Shared bundle As PropertyResourceBundle = Nothing

	Shared Sub Main(ByVal argv() As String)
		bundle =CType(PropertyResourceBundle.getBundle(BUNDLE), PropertyResourceBundle)
		If bundle.getString("isTestOracle").Equals("true") Then
			testOracleCnx()
		End If
		If bundle.getString("isTestLocalDB2").Equals("true") Then
			testLocalDB2Cnx()
		End If
		If bundle.getString("isTestRemoteDB2").Equals("true") Then
			testRemoteDB2Cnx()
		End If
		If bundle.getString("isTestAS400Cnx").Equals("true") Then
			testAS400Cnx()
		End If
		If bundle.getString("isTestAS400NatifCnx").Equals("true") Then
			testAS400NatifCnx()
		End If
	End Sub

	  Public Shared Sub printError(ByVal typeCnx As String, ByVal e As SQLException)
		  Console.WriteLine(typeCnx & " failed:")
		  e.printStackTrace(System.out)
		Console.WriteLine("SQLState : " & e.sQLState)
		Console.WriteLine("ErrorCode : " & e.errorCode)
		Dim [next] As SQLException = e.nextException
		If [next] IsNot Nothing Then
			[next].printStackTrace(System.out)
			Console.WriteLine("SQLState  : " & [next].sQLState)
			Console.WriteLine("ErrorCode : " & [next].errorCode)
		End If
		Console.WriteLine("---------------------------------------")
	  End Sub
	  Public Shared Sub testOracleCnx()
		Try
			Type.GetType(bundle.getString("oracleDriver"))
			Dim connection As Connection = DriverManager.getConnection(bundle.getString("oracleUrl"), bundle.getString("oracleuser"), bundle.getString("oraclepwd"))
			 connection.autoCommit = False

'		 	String strSelect = "select * from Abonnement";
'			PreparedStatement stm = connection.prepareStatement(strSelect);
'			ResultSet rs = stm.executeQuery();
'			while(rs.next()) {
'				System.out.println("Hello");
'			}
'			rs.close();
'			stm.close();
'			connection.close();
'		 	
			Console.WriteLine("OracleCnx OK")

'			String sqlsrc = "INSERT INTO compte (" + 
'                                    "id_compte, " +                    // 1
'                                    "tycon, " +                        // 2, 1= RIB, 2 = IBAN
'                                    "compt, " +                        // 3
'                                    "id_devise, " +                    // 4
'                                    "id_nature_compte," +              // 5
'                                    "nutit, " +                        // 6, Titulaire principal
'                                    "id_agence, " +                    // 7, Agence SAB
'                                    "ctsab, " +                        // 8, Compte SAB
'                                    "id_devise_decouvert_auto, " +     // 9, Devise de découvert autorisé
'                                    "montant_decouvert_auto, " +       // 10, Montant de découvert autorisé
'                                    "id_gestionnaire)" ;             // 11 Valeur en dur
'
'			sqlsrc = sqlsrc + "VALUES (" + "?,?,?,?,?,?,?,?,?,?,1)";
'			PreparedStatement sql_trace = connection.prepareStatement(sqlsrc);
'			for (int i = 1; i<200; i++) {
'				sql_trace.setInt(1, i);
'				sql_trace.setString(2,"3");
'				sql_trace.setString(3,"00000019001");
'				sql_trace.setString(4,"2");
'				sql_trace.setInt(5,15);
'				sql_trace.setString(6,"1");
'				sql_trace.setInt(7,999999001);
'				sql_trace.setString(8,"00000019001");
'				sql_trace.setInt(9,2);
'				sql_trace.setBigDecimal(10,new BigDecimal(0));
'				sql_trace.executeUpdate();
'			}
'			
'			
'			String sqlins = "INSERT INTO servicecompte (" + 
'                                    "id_service, " + // 1
'                                    "id_compte, "  + // 2
'                                    "daadh)" +      // 3 Date d'adhésion                            
'                        "     VALUES (" + "?,?,?)";

'                    
'        	PreparedStatement pstmtIns = connection.prepareStatement(sqlins);
'        	for (int i = 1; i<200; i++) {
'        		pstmtIns.setInt(1,999999001);
'	     		pstmtIns.setInt(2,i);
'	     		pstmtIns.setDate(3,new Date(new java.util.Date().getTime()));
'	     		pstmtIns.executeUpdate();
'        	}	
'//			Statement sql_trace = connection.createStatement();
'//      		sql_trace.execute("ALTER TABLE VCOM_FACTURE DISABLE CONSTRAINT VCOM_FACT_fk01");
'//      		sql_trace.execute("truncate table VCOM_VIREMENT");
'//      		sql_trace.execute("ALTER TABLE VCOM_FACTURE ENABLE CONSTRAINT VCOM_FACT_fk01");
'//			System.out.println( "Table truncated" );
'			
'			connection.commit();
'			pstmtIns.close();
'			sql_trace.close();

			connection.close()
		Catch e As SQLException
			printError("OracleCnx", e)
		Catch e As Exception
			Console.WriteLine("OracleCnx failed:")
			Console.WriteLine(e.ToString())
			Console.Write(e.StackTrace)
		End Try
	  End Sub

	Public Shared Sub testLocalDB2Cnx()
		Try
			Type.GetType(bundle.getString("localDB2Driver"))
			Dim connection As Connection = DriverManager.getConnection(bundle.getString("localDB2Url"))
			 connection.autoCommit = False
			Console.WriteLine("LocalDB2Cnx OK")

		Catch e As SQLException
			printError("LocalDB2Cnx", e)
		Catch e As Exception
			Console.WriteLine("LocalDB2Cnx failed:")
			Console.WriteLine(e.ToString())
			Console.Write(e.StackTrace)
		End Try
	End Sub

	Public Shared Sub testRemoteDB2Cnx()
		Try
			Type.GetType(bundle.getString("remoteDB2Driver"))
			Dim connection As Connection = DriverManager.getConnection(bundle.getString("remoteDB2Url"), bundle.getString("remoteDB2user"), bundle.getString("remoteDB2pwd"))
			connection.autoCommit = False
'			String strSelect = "select * from Abonnement where id_abonnement<0";
'			PreparedStatement stm = connection.prepareStatement(strSelect);
'			ResultSet rs = stm.executeQuery();
'			while(rs.next()) {
'				System.out.println("Hello");
'			}
'			rs.close();
'			stm.close();
			connection.commit()
			connection.close()
			Console.WriteLine("RemoteDB2Cnx OK")
		Catch e As SQLException
			printError("RemoteDB2Cnx", e)
		Catch e As Exception
			Console.WriteLine("RemoteDB2Cnx failed:")
			Console.WriteLine(e.ToString())
			Console.Write(e.StackTrace)
		End Try
	End Sub

	Public Shared Sub testAS400Cnx()
	  Try
			Type.GetType(bundle.getString("as400CnxDriver"))
			Dim connection As Connection = DriverManager.getConnection(bundle.getString("as400CnxUrl"), bundle.getString("as400Cnxuser"), bundle.getString("as400Cnxpwd"))
			connection.autoCommit = False
'			String strSelect = "select * from strabge.zcliena0";
'			PreparedStatement stm = connection.prepareStatement(strSelect);
'			ResultSet rs = stm.executeQuery();
'			if(rs.next()) {
'				System.out.println("Hello");
'			}
'			rs.close();
'			stm.close();
			connection.close()
			Console.WriteLine("AS400Cnx OK")
		Catch e As SQLException
			printError("AS400Cnx", e)
		Catch e As Exception
			Console.WriteLine("AS400Cnx failed:")
			Console.WriteLine(e.ToString())
			Console.Write(e.StackTrace)
		End Try
	End Sub

	Public Shared Sub testAS400NatifCnx()
	  Try
			Type.GetType(bundle.getString("as400NatifCnxDriver"))
			Dim connection As Connection = DriverManager.getConnection(bundle.getString("as400NatifCnxUrl"))
			connection.autoCommit = False
			Console.WriteLine("AS400NatifCnx OK")
		Catch e As SQLException
			printError("AS400NatifCnx", e)
		Catch e As Exception
			Console.WriteLine("AS400NatifCnx failed:")
			Console.WriteLine(e.ToString())
			Console.Write(e.StackTrace)
		End Try
	End Sub

End Class
