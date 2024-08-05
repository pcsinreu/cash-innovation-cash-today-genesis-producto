Imports Prosegur.DbHelper
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data
Imports System.Net

Public Class AccesoDB

    Public Shared Function ArmarComandoSP(SPw As SPWrapper, Conn As OracleConnection) As OracleCommand
        Dim oCom As OracleCommand = Nothing
        If SPw IsNot Nothing Then
            If SPw.SP <> "" Then

                'comando
                If Conn IsNot Nothing Then
                    oCom = New OracleCommand(SPw.SP, Conn)
                Else
                    oCom = New OracleCommand(SPw.SP)
                End If
                oCom.CommandType = SPw.Tipo
                oCom.BindByName = True

                'parametros
                If SPw.Params IsNot Nothing Then
                    For Each p As ParamWrapper In SPw.Params
                        Dim oParam As OracleParameter = CrearParamSP(p)
                        oCom.Parameters.Add(oParam)
                    Next
                End If

            End If

        End If

        Return oCom
    End Function

    Private Shared Function CrearParamSP(Param As ParamWrapper) As OracleParameter
        'según el tipo declarado, determino el tipo del parámetro, y lo creo con su dirección y valor
        Dim oParam As OracleParameter = Nothing
        Dim pNombre As String = Param.Nombre
        Dim pDIr As System.Data.ParameterDirection = Param.Direccion

        Select Case Param.Tipo
            Case ParamWrapper.ParamTypes.Integer
                oParam = New OracleParameter(pNombre, OracleDbType.Int32, pDIr)
            Case ParamWrapper.ParamTypes.Long
                oParam = New OracleParameter(pNombre, OracleDbType.Int64, pDIr)
            Case ParamWrapper.ParamTypes.Decimal
                oParam = New OracleParameter(pNombre, OracleDbType.Decimal, pDIr)
            Case ParamWrapper.ParamTypes.String
                oParam = New OracleParameter(pNombre, OracleDbType.Varchar2, pDIr)
                oParam.Size = 32767
            Case ParamWrapper.ParamTypes.Observacion
                oParam = New OracleParameter(pNombre, OracleDbType.Varchar2, pDIr)
            Case ParamWrapper.ParamTypes.Date
                oParam = New OracleParameter(pNombre, OracleDbType.Date, pDIr)
            Case ParamWrapper.ParamTypes.Timestamp
                oParam = New OracleParameter(pNombre, OracleDbType.TimeStamp, pDIr)
            Case ParamWrapper.ParamTypes.Boolean
                oParam = New OracleParameter(pNombre, OracleDbType.Byte, pDIr)
            Case ParamWrapper.ParamTypes.RefCursor
                oParam = New OracleParameter(pNombre, OracleDbType.RefCursor, pDIr)
            Case ParamWrapper.ParamTypes.Blob
                oParam = New OracleParameter(pNombre, OracleDbType.Blob, pDIr)
        End Select

        If oParam IsNot Nothing Then
            If Param.Direccion <> ParameterDirection.Output AndAlso Param.Direccion <> ParameterDirection.ReturnValue Then
                If Param.EsArray Then
                    oParam.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    If Param.ListaValores.Count = 0 Then
                        Select Case Param.Tipo
                            Case ParamWrapper.ParamTypes.Integer, ParamWrapper.ParamTypes.Long, ParamWrapper.ParamTypes.Decimal, ParamWrapper.ParamTypes.Boolean
                                oParam.Value = New OracleDecimal(0) {OracleDecimal.Null}
                            Case ParamWrapper.ParamTypes.String
                                oParam.Value = New OracleString(0) {OracleString.Null}
                            Case ParamWrapper.ParamTypes.Date
                                oParam.Value = New OracleDate(0) {OracleDate.Null}
                        End Select
                        oParam.Size = 0
                    Else
                        oParam.Value = Param.Valor
                    End If
                Else
                    oParam.Value = Param.Valor
                End If
            End If
        End If

        Return oParam
    End Function

    Public Shared Function EjecutarSP(ByRef SPw As SPWrapper, IdConexion As String, Transaccionar As Boolean, Optional ByRef TransaccionActual As Transaccion = Nothing) As DataSet
        Dim ds As DataSet = Nothing

        Dim sCon As String = ""

        If Not (TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing) Then
            sCon = AcessoDados.RecuperarStringConexao(IdConexion)
        End If

        If sCon <> "" OrElse (TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing) Then
            Try
                'ATENCION!!: Para poder depurar los SP de PL/SQL *****************************************************************************************
                '
                '   Instalar Oracle Developer Tools For Visual Studio 2013
                '       Disponible A PARTIR de ODAC 12c Release 2 (12.1.0.1.2)
                '   En (menu) Tools->Options->Oracle Developer Tools->PL/SQL Debugging indicar IP y puerto del equipo local, y conexiones a depurar
                '   
                '   Agregar conexión a la DB en el Server Explorer, con el usuario de la conexión que se va a depurar
                '   El usuario de la conexión a depurar tienen que tener los permisos:
                '       Grant debug any procedure, debug connect session to <usuario>;
                '   Compliar el Package (o SP) con  la opción Compile Debug
                '   Espablecer los break points necesarios en el SP
                '
                '   Como es una aplicación Web que se adjunta al IIS local
                '   NO checkear (menu) Tools->Oracle Application Debugging
                '   (si no se requiere depurar PL/SQL, no hacer los pasos que siguen)
                '   ANTES de establecer la conexión a la DB
                '   1.- Establecer la variable de entorno ORA_DEBUG_JDWP
                '   2.- Seleccionar (menu) Tools->Start Oracle eExternal Application Debugger. Asignarle el mismo host y port que la variable de entorno
                '   3.- Luego de cerrar la conexión, o ante un error, eliminar la variable de entorno ORA_DEBUG_JDWP
                '
                '   Cuando los SP estén correctos, 
                '   Volver a compilar el Package SIN la opcion Compile Debug, solo Compile
                '******************************************************************************************************************************************

#If DEBUG Then

                Dim objParametrosDebug As Comon.ParametrosDebug = Comon.Util.RecuperarParametrosDebug

                If objParametrosDebug IsNot Nothing AndAlso objParametrosDebug.DebugarProcedure Then
                    'ejecutar esta línea SOLAMENTE si se pretende depurar el código PL-SQL
                    'si se ejecuta, a continuación seleccionar (menu) Tools->Start Oracle eExternal Application Debugger
                    Dim sHostName As String = Dns.GetHostName()
                    Dim ipE As IPHostEntry = Dns.GetHostEntry(sHostName)
                    Environment.SetEnvironmentVariable("ORA_DEBUG_JDWP", String.Format("host={0};port=65000", ipE.AddressList(0)), EnvironmentVariableTarget.Process)

                End If
#End If

                Dim oCon As OracleConnection = Nothing

                Try

                    If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing Then
                        oCon = TransaccionActual.TransaccionEnCurso.Connection
                    Else
                        oCon = New OracleConnection(sCon)
                    End If

                    If oCon IsNot Nothing Then
                        If oCon.State = ConnectionState.Closed Then
                            oCon.Open()
                        End If

                        'Dim oCom As OracleCommand
                        'If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing Then
                        '    oCom = ArmarComandoSP(SPw, Nothing)
                        'Else
                        '    oCom = ArmarComandoSP(SPw, oCon)
                        'End If

                        Using oCom As OracleCommand = ArmarComandoSP(SPw, oCon)

                            Dim ini As Date = Now
                            Try

                                If TransaccionActual IsNot Nothing Then

                                    If TransaccionActual.IniciarTransaccion <> True Then
                                        ' Caso sea una transacion, debe llamar el sinicializar solamente no inicio de la transacion. Para no ocurrir el error del Commit
                                        SPw.SP = SPw.SP.Replace(String.Format("gepr_putilidades_{0}.sinicializar_sesion;", Comon.Util.Version), "")
                                    End If

                                    If TransaccionActual.TransaccionarAtomico Then
                                        Transaccionar = True
                                    Else
                                        If TransaccionActual.TransaccionEnCurso Is Nothing Then
                                            If TransaccionActual.IniciarTransaccion Then
                                                TransaccionActual.TransaccionEnCurso = oCon.BeginTransaction
                                                TransaccionActual.IniciarTransaccion = False
                                            Else
                                                Throw New Exception("Ambito de transaccion inválido")
                                            End If
                                            'Else
                                            'oCom.Transaction = TransaccionActual.TransaccionEnCurso
                                        End If

                                    End If
                                End If

                                Dim oTran As OracleTransaction = Nothing
                                Dim enTransaccion As Boolean = False

                                If Transaccionar Then
                                    oTran = oCon.BeginTransaction
                                    oCom.Transaction = oTran
                                    enTransaccion = True
                                End If

                                If SPw.NoQuery Then
                                    oCom.ExecuteNonQuery()
                                    SPw.ConservarValoresSalida(oCom)
                                Else
                                    ds = New DataSet
                                    Using da As OracleDataAdapter = New OracleDataAdapter(oCom)
                                        da.Fill(ds)
                                    End Using
                                    SPw.ConservarValoresSalida(oCom)
                                    'cerrar lo cursores en la DB
                                    If oCom.Parameters IsNot Nothing Then
                                        For Each p As OracleParameter In oCom.Parameters
                                            p.Dispose()
                                        Next
                                    End If
                                End If

                                If enTransaccion Then
                                    oTran.Commit()
                                    oTran.Dispose()
                                    oTran = Nothing
                                    enTransaccion = False
                                End If

                                If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing AndAlso TransaccionActual.CerrarTransaccion Then
                                    TransaccionActual.TransaccionEnCurso.Commit()
                                    TransaccionActual.TransaccionEnCurso.Dispose()
                                End If

                                If SPw.RegistraEjecucionExterna Then
                                    'fuera de la transaccion para poder medirla
                                    Try
                                        SPw.AsignarRegistroEjecucionExterna(DateDiffSecFraccion(Now, ini), SPWrapper.CodTransaccion.Externa, SPWrapper.CodResultado.Commit)
                                        Using oComReg As OracleCommand = ArmarComandoSP(SPw.SpRegTransacExt, oCon)
                                            oComReg.ExecuteNonQuery()
                                            If oComReg IsNot Nothing AndAlso oComReg.Connection IsNot Nothing AndAlso (TransaccionActual Is Nothing OrElse TransaccionActual.TransaccionEnCurso Is Nothing) Then
                                                oComReg.Connection.Close()
                                                oComReg.Dispose()
                                            End If
                                        End Using

                                    Catch ex As Exception
                                        'nada
                                    End Try
                                End If

                            Catch ex As Exception

                                If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing Then
                                    TransaccionActual.TransaccionEnCurso.Rollback()
                                    TransaccionActual.TransaccionEnCurso.Dispose()
                                End If

                                GravarParametrosProcedure(SPw, oCon, ex)

                                If SPw.RegistraEjecucionExterna Then
                                    'fuera de la transaccion para poder medirla
                                    Try
                                        SPw.AsignarRegistroEjecucionExterna(DateDiffSecFraccion(Now, ini), SPWrapper.CodTransaccion.Externa, SPWrapper.CodResultado.Rollback)
                                        Using oComReg As OracleCommand = ArmarComandoSP(SPw.SpRegTransacExt, oCon)
                                            oComReg.ExecuteNonQuery()
                                            If oComReg IsNot Nothing AndAlso oComReg.Connection IsNot Nothing AndAlso (TransaccionActual Is Nothing OrElse TransaccionActual.TransaccionEnCurso Is Nothing) Then
                                                oComReg.Connection.Close()
                                                oComReg.Dispose()
                                            End If
                                        End Using
                                    Catch exr As Exception
                                        'nada
                                    End Try
                                End If

                                If oCon IsNot Nothing Then
                                    If oCon.State <> ConnectionState.Closed Then
                                        oCon.Close()
                                    End If
                                    oCon.Dispose()
                                    oCon = Nothing
                                End If

                                Throw New Exception("Error al ejecutar comando [" & SPw.SP & "]", ex)
                            End Try
                        End Using
                    Else
                        Throw New Exception("Conexión inválida") 'TODO: ver normalización de errores
                    End If
                Finally
                    If oCon IsNot Nothing Then
                        If TransaccionActual Is Nothing OrElse TransaccionActual.TransaccionEnCurso Is Nothing OrElse TransaccionActual.CerrarTransaccion Then
                            If oCon.State <> ConnectionState.Closed Then
                                oCon.Close()
                            End If
                            oCon.Dispose()
                            oCon = Nothing
                        End If
                    End If
                End Try


                If ds IsNot Nothing Then
                    Dim j As Integer = 0
                    For i As Integer = 0 To SPw.Params.Count - 1
                        If SPw.Params(i).Tipo = ParamWrapper.ParamTypes.RefCursor AndAlso (SPw.Params(i).Direccion = ParameterDirection.Output OrElse SPw.Params(i).Direccion = ParameterDirection.InputOutput) AndAlso SPw.Params(i).NombreTabla <> "" Then
                            If ds.Tables.Count > j Then
                                ds.Tables(j).TableName = SPw.Params(i).NombreTabla
                            End If
                            j += 1
                        End If
                    Next
                    Descriptor.Describir(ds, "")
                End If

#If DEBUG Then
                If Not String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ORA_DEBUG_JDWP", EnvironmentVariableTarget.Process)) Then
                    Environment.SetEnvironmentVariable("ORA_DEBUG_JDWP", "", EnvironmentVariableTarget.Process)
                End If
#End If

            Catch ex As Exception

#If DEBUG Then
                If Not String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ORA_DEBUG_JDWP", EnvironmentVariableTarget.Process)) Then
                    Environment.SetEnvironmentVariable("ORA_DEBUG_JDWP", "", EnvironmentVariableTarget.Process)
                End If
#End If

                Throw New Exception("Error al ejecutar comando [" & SPw.SP & "]", ex)

            End Try
        Else
            Throw New Exception("Conexión inválida") 'TODO: ver normalización de errores
        End If

        Return ds
    End Function

    Public Shared Sub TransactionCommit(ByRef TransaccionActual As Transaccion)

        If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso.Connection IsNot Nothing Then
            Dim oCon As OracleConnection = TransaccionActual.TransaccionEnCurso.Connection
            TransaccionActual.TransaccionEnCurso.Commit()
            TransaccionActual.TransaccionEnCurso.Dispose()
            oCon.Close()
            oCon.Dispose()
        End If

    End Sub

    Public Shared Sub TransactionRollback(ByRef TransaccionActual As Transaccion)

        If TransaccionActual IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso IsNot Nothing AndAlso TransaccionActual.TransaccionEnCurso.Connection IsNot Nothing Then

            Dim oCon As OracleConnection = TransaccionActual.TransaccionEnCurso.Connection
            TransaccionActual.TransaccionEnCurso.Rollback()
            TransaccionActual.TransaccionEnCurso.Dispose()
            oCon.Close()
            oCon.Dispose()
        End If


    End Sub

    Private Shared Function DateDiffSecFraccion(Fecha1 As Date, Fecha2 As Date) As Single
        'tick=1 diez millonésima de segundo
        Dim TicksDiferencia As Long = Fecha1.Ticks - Fecha2.Ticks
        Return TicksDiferencia / 10000000
    End Function

    Private Shared Sub GravarParametrosProcedure(ByRef SPw As SPWrapper, oCon As OracleConnection, objEx As Exception)
        Try

            If SPw.Tipo <> CommandType.StoredProcedure Then
                Exit Sub
            End If

            'Dim strConexao = AcessoDados.RecuperarStringConexao(sCon)
            Dim sql As New System.Text.StringBuilder
            sql.Append("SELECT POSITION, ARGUMENT_NAME,DATA_TYPE,PLS_TYPE,CHAR_LENGTH, DATA_LENGTH,DATA_PRECISION, TYPE_NAME,TYPE_SUBNAME")
            sql.Append(" FROM SYS.ALL_ARGUMENTS")
            sql.Append(" WHERE PACKAGE_NAME = UPPER(:PACKAGE_NAME)")
            sql.Append(" AND OBJECT_NAME =  UPPER(:OBJECT_NAME)")
            sql.Append(" AND ARGUMENT_NAME IS NOT NULL")
            sql.Append(" ORDER BY POSITION")

            Dim sp As New SPWrapper(sql.ToString(), False)
            Dim procedure = SPw.SP.Replace(String.Format("gepr_putilidades_{0}.sinicializar_sesion;", Comon.Util.Version), "").Split(".")
            If procedure.Count = 2 Then

                If oCon IsNot Nothing AndAlso oCon.State <> ConnectionState.Open Then
                    oCon.Open()
                End If

                Using oCom As OracleCommand = New OracleCommand(sql.ToString, oCon)
                    oCom.Parameters.Add("PACKAGE_NAME", procedure(0))
                    oCom.Parameters.Add("OBJECT_NAME", procedure(1))

                    Dim ds = New DataSet
                    Dim da = New OracleDataAdapter(oCom)
                    da.Fill(ds)
                    da.Dispose()

                    If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                        'REF CURSOR
                        Dim dt As DataTable = ds.Tables(0)
                        Dim dec As New System.Text.StringBuilder
                        dec.AppendLine("DECLARE")

                        For Each dr In dt.Rows
                            If dr("TYPE_NAME") IsNot DBNull.Value Then
                                dec.AppendLine(String.Format("{0} {1}.{2};", dr("ARGUMENT_NAME"), dr("TYPE_NAME"), dr("TYPE_SUBNAME")))

                            ElseIf dr("DATA_TYPE") = "VARCHAR2" Then
                                If dr("DATA_LENGTH") Is DBNull.Value Then
                                    dec.AppendLine(String.Format("{0} VARCHAR2(4000);", dr("ARGUMENT_NAME")))
                                Else
                                    dec.AppendLine(String.Format("{0} VARCHAR2({1});", dr("ARGUMENT_NAME"), dr("DATA_LENGTH")))
                                End If

                            ElseIf dr("DATA_TYPE") = "REF CURSOR" Then
                                dec.AppendLine(String.Format("{0} sys_refcursor;", dr("ARGUMENT_NAME")))
                            Else
                                dec.AppendLine(String.Format("{0} {1};", dr("ARGUMENT_NAME"), dr("DATA_TYPE")))
                            End If
                        Next

                        dec.AppendLine()
                        dec.AppendLine("BEGIN")
                        dec.AppendLine()

                        Dim valor As New System.Text.StringBuilder
                        Dim executar As New System.Text.StringBuilder

                        For Each param In SPw.Params
                            Dim dr As DataRow = dt.Select(String.Format("ARGUMENT_NAME='{0}'", param.Nombre.ToUpper())).FirstOrDefault
                            If dr IsNot Nothing Then
                                If param.ListaValores IsNot Nothing AndAlso param.ListaValores.Count > 0 Then
                                    Dim index As Int16 = 1
                                    For Each valorPar In param.ListaValores
                                        If valorPar IsNot Nothing AndAlso valorPar IsNot DBNull.Value Then
                                            If param.Tipo = ParamWrapper.ParamTypes.Date OrElse param.Tipo = ParamWrapper.ParamTypes.Timestamp Then
                                                valor.AppendLine(String.Format("{0}({1}) := to_date('{2}','DD/MM/YYYY HH24:mi:ss');", param.Nombre, index, Convert.ToDateTime(valorPar.ToString).ToString("dd/MM/yyyy hh:mm:ss")))
                                            ElseIf (param.Tipo = ParamWrapper.ParamTypes.Boolean) Then
                                                valor.AppendLine(String.Format("{0}({1}) := '{2}';", param.Nombre, index, If(CBool(valorPar.ToString), "1", "0")))
                                            ElseIf param.Tipo = ParamWrapper.ParamTypes.String Then
                                                valor.AppendLine(String.Format("{0}({1}) := '{2}';", param.Nombre, index, valorPar.ToString))
                                            Else
                                                valor.AppendLine(String.Format("{0}({1}) := {2};", param.Nombre, index, valorPar.ToString))
                                            End If
                                        Else
                                            valor.AppendLine(String.Format("{0}({1}) := NULL;", param.Nombre, index))
                                        End If

                                        index = index + 1
                                    Next
                                Else
                                    If param.Valor IsNot Nothing AndAlso param.Valor IsNot DBNull.Value Then
                                        If param.Valor.ToString() = "System.Object[]" Then
                                            If Not param.EsArray Then
                                                valor.AppendLine(String.Format("{0} := NULL;", param.Nombre))
                                            End If
                                        Else
                                            If param.Tipo = ParamWrapper.ParamTypes.Date OrElse param.Tipo = ParamWrapper.ParamTypes.Timestamp Then
                                                valor.AppendLine(String.Format("{0} := to_date('{1}','DD/MM/YYYY HH24:mi:ss');", param.Nombre, Convert.ToDateTime(param.Valor.ToString()).ToString("dd/MM/yyyy hh:mm:ss")))
                                            ElseIf param.Tipo = ParamWrapper.ParamTypes.String Then
                                                valor.AppendLine(String.Format("{0} := '{1}';", param.Nombre, param.Valor.ToString()))
                                            ElseIf (param.Tipo = ParamWrapper.ParamTypes.Boolean) Then
                                                valor.AppendLine(String.Format("{0} := {1};", param.Nombre, If(CBool(param.Valor), "1", "0")))
                                            Else
                                                valor.AppendLine(String.Format("{0} := {1};", param.Nombre, param.Valor.ToString()))
                                            End If
                                        End If
                                    Else
                                        valor.AppendLine(String.Format("{0} := NULL;", param.Nombre))
                                    End If
                                End If
                            Else
                                valor.AppendLine(String.Format("PARAMETRO_NAO_DECLARADO {0}", param.Nombre))
                            End If

                            executar.AppendLine(String.Format(",{0} =>{0}", param.Nombre))
                        Next

                        Dim strAux = executar.ToString().Substring(1) + ");"
                        executar = New Text.StringBuilder

                        executar.AppendLine("-- EXECUTAR A PROCEDURE")
                        dec.AppendLine()
                        executar.AppendLine(String.Format("{0}(", SPw.SP))
                        executar.AppendLine(strAux)
                        dec.AppendLine()
                        executar.AppendLine("END;")

                        If objEx IsNot Nothing Then

                            Dim Erros As String() = objEx.Message.Split("***")

                            If Erros IsNot Nothing AndAlso Erros.Count > 2 Then

                                Dim objCodigoEjecucion As String = (From ce In Erros Where ce.Contains("CODIGO_EJECUCION")).FirstOrDefault

                                If Not String.IsNullOrEmpty(objCodigoEjecucion) Then

                                    Dim CodigoEjecucion As Integer = objCodigoEjecucion.Split(":").LastOrDefault

                                    If CodigoEjecucion > 0 Then
                                        GravarParametrosProcedureBD(CodigoEjecucion, dec.ToString + valor.ToString + executar.ToString)
                                    End If

                                End If

                            End If


                        End If


                    End If

                End Using

            End If
        Catch ex As Exception
            'Faz nada
        End Try

    End Sub

    Private Shared Sub GravarParametrosProcedureBD(CodigoEjecucion As Integer, Mensagem As String)

        Dim sql As New System.Text.StringBuilder

        sql.Append("INSERT INTO SAPR_TDATOS_EJECUCION (COD_EJECUCION, BIN_DATOS_EJECUCION) ")
        sql.AppendLine(" VALUES (:COD_EJECUCION, :BIN_DATOS_EJECUCION) ")

        Dim wrapper As New DataBaseHelper.SPWrapper(sql.ToString, True, CommandType.Text)

        wrapper.AgregarParam("COD_EJECUCION", ProsegurDbType.Objeto_Id, CodigoEjecucion)
        wrapper.AgregarParam("BIN_DATOS_EJECUCION", ParamWrapper.ParamTypes.Observacion, Mensagem)

        DataBaseHelper.AccesoDB.EjecutarSP(wrapper, "GENESIS", False)

    End Sub

End Class
