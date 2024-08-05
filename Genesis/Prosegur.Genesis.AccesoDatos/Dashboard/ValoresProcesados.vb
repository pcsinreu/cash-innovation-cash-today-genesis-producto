Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.DbHelper

Namespace Dashboard

    Public Class ValoresProcesados

#Region "[CONSULTAR]"

        Shared Function RetornaValoresProcesados(FechaHoraInicio As DateTime, _
                                                 FechaHoraFin As DateTime, _
                                                 CodigosDelegacion As List(Of String), _
                                                 CodigosSector As List(Of String), _
                                                 CodigosIsoDivisa As List(Of String),
                                                 Optional Top As Integer = -1) As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = My.Resources.Dashboard_ValoresProcesados_RetornaValoresProcesados
                cmd.CommandType = CommandType.Text
                Dim strFiltros As New List(Of String)
                Dim strFiltros2 As New List(Of String)

                If FechaHoraInicio > DateTime.MinValue Then
                    strFiltros.Add("FECHA_HORA >= []FECHA_HORA_INICIO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_INICIO", ProsegurDbType.Data_Hora, FechaHoraInicio))
                End If

                If FechaHoraFin > DateTime.MinValue Then
                    strFiltros.Add("FECHA_HORA <= []FECHA_HORA_FIN")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_FIN", ProsegurDbType.Data_Hora, FechaHoraFin))
                End If

                If CodigosDelegacion IsNot Nothing AndAlso CodigosDelegacion.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosDelegacion, "COD_DELEGACION", cmd))
                End If

                If CodigosSector IsNot Nothing AndAlso CodigosSector.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosSector, "COD_SECTOR", cmd))
                End If

                If CodigosIsoDivisa IsNot Nothing AndAlso CodigosIsoDivisa.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosIsoDivisa, "COD_ISO_DIVISA", cmd))
                End If

                If strFiltros.Count > 0 Then
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, String.Format(cmd.CommandText, "WHERE " + String.Join(Environment.NewLine + "AND ", strFiltros)))
                Else
                    cmd.CommandText = String.Format(cmd.CommandText, "")
                End If

                If Top > 0 Then

                    cmd.CommandText = "SELECT * FROM (" + cmd.CommandText + ") WHERE ROWNUM <= " + Top.ToString()

                End If

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Function

        Shared Function RetornaSomaValoresProcesadosCliente(FechaHoraInicio As DateTime, _
                                                             FechaHoraFin As DateTime, _
                                                             CodigosDelegacion As List(Of String), _
                                                             CodigosSector As List(Of String), _
                                                             CodigosIsoDivisa As List(Of String),
                                                             Optional Top As Integer = -1) As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = My.Resources.Dashboard_RetornaSomaValoresProcesadosCliente
                cmd.CommandType = CommandType.Text
                Dim strFiltros As New List(Of String)

                If FechaHoraInicio > DateTime.MinValue Then
                    strFiltros.Add("FECHA_HORA >= []FECHA_HORA_INICIO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_INICIO", ProsegurDbType.Data_Hora, FechaHoraInicio))
                End If

                If FechaHoraFin > DateTime.MinValue Then
                    strFiltros.Add("FECHA_HORA <= []FECHA_HORA_FIN")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_FIN", ProsegurDbType.Data_Hora, FechaHoraFin))
                End If

                If CodigosDelegacion IsNot Nothing AndAlso CodigosDelegacion.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosDelegacion, "COD_DELEGACION", cmd))
                End If

                If CodigosSector IsNot Nothing AndAlso CodigosSector.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosSector, "COD_SECTOR", cmd))
                End If

                If CodigosIsoDivisa IsNot Nothing AndAlso CodigosIsoDivisa.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosIsoDivisa, "COD_ISO_DIVISA", cmd))
                End If

                If strFiltros.Count > 0 Then
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, String.Format(cmd.CommandText, "WHERE " + String.Join(Environment.NewLine + "AND ", strFiltros)))
                Else
                    cmd.CommandText = String.Format(cmd.CommandText, "")
                End If

                If Top > 0 Then

                    cmd.CommandText = "SELECT * FROM (" + cmd.CommandText + ") WHERE ROWNUM <= " + Top.ToString()

                End If

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Function

        Shared Function RetornaValoresProcesadosPorHora(FechaHoraInicio As DateTime, _
                                                 FechaHoraFin As DateTime, _
                                                 CodigosDelegacion As List(Of String), _
                                                 CodigosSector As List(Of String), _
                                                 Optional Top As Integer = -1) As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = My.Resources.Dashboard_ValoresProcesados_RetornaValoresProcesadosHora
                cmd.CommandType = CommandType.Text
                Dim strFiltros As New List(Of String)

                If FechaHoraInicio > DateTime.MinValue Then
                    strFiltros.Add("MIN.FECHA_HORA >= []FECHA_HORA_INICIO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_INICIO", ProsegurDbType.Data_Hora, FechaHoraInicio))
                End If

                If FechaHoraFin > DateTime.MinValue Then
                    strFiltros.Add("MIN.FECHA_HORA <= []FECHA_HORA_FIN")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA_FIN", ProsegurDbType.Data_Hora, FechaHoraFin))
                End If

                If CodigosDelegacion IsNot Nothing AndAlso CodigosDelegacion.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosDelegacion, "COD_DELEGACION", cmd, [Alias]:="MIN"))
                End If

                If CodigosSector IsNot Nothing AndAlso CodigosSector.Count > 0 Then
                    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosSector, "COD_SECTOR", cmd, [Alias]:="MIN"))
                End If

                'If CodigosIsoDivisa IsNot Nothing AndAlso CodigosIsoDivisa.Count > 0 Then
                '    strFiltros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_DASHBOARD, CodigosIsoDivisa, "COD_ISO_DIVISA", cmd, [Alias]:="MIN"))
                'End If

                If strFiltros.Count > 0 Then
                    Dim filtros = "WHERE " + String.Join(Environment.NewLine + "AND ", strFiltros)
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, String.Format(cmd.CommandText, filtros.Replace("MIN.", "MIN2."), filtros))
                Else
                    cmd.CommandText = String.Format(cmd.CommandText, "")
                End If

                If Top > 0 Then

                    cmd.CommandText = "SELECT * FROM (" + cmd.CommandText + ") WHERE ROWNUM <= " + Top.ToString()

                End If

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Function
#End Region


#Region "[INSERIR/ATUALICAR]"

        Shared Sub GrabarDatos(IdentificadorRemesa As String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, "PKG_DASH_CONTEO.SP_GRABAR_DATOS")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(Util.CriarParametroOracle("P_OID_REMESA", ParameterDirection.InputOutput, IdentificadorRemesa, OracleClient.OracleType.VarChar, 36))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Sub

        Shared Sub InsertarActualizar(FechaHoraFinConteo As DateTime, CodigoDelegacion As String, DescripcionDelegacion As String, CodigoSector As String, DescripcionSector As String, _
                                      CodigoCliente As String, DescripcionCliente As String, CodigoProducto As String, DescripcionProducto As String, CodigoUsuario As String, DescripcionUsuario As String, _
                                      CodigoIsoDivisa As String, DescripcionDivisa As String, CodigoDenominacion As String, DescripcionDenominacion As String, EsMecanizado As Boolean, _
                                      NumImporteTotalBilletes As Decimal, NumImporteTotalMonedas As Decimal, NelCantidadTotalBilletes As Long, NelCantidadTotalMonedas As Long, _
                                      NelCantidadTotalMinutos As Long)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, My.Resources.Dashboard_ValoresProcesados_InserirActualizar)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "OID_VALORES_PROCESADOS", ProsegurDbType.Objeto_Id, Guid.NewGuid().ToString()))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA", ProsegurDbType.Data_Hora, FechaHoraFinConteo))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, CodigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_DELEGACION", ProsegurDbType.Descricao_Longa, DescripcionDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_SECTOR", ProsegurDbType.Descricao_Longa, CodigoSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_SECTOR", ProsegurDbType.Descricao_Longa, DescripcionSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, CodigoCliente))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_CLIENTE", ProsegurDbType.Descricao_Longa, DescripcionCliente))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_PRODUCTO", ProsegurDbType.Descricao_Longa, CodigoProducto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_PRODUCTO", ProsegurDbType.Descricao_Longa, DescripcionProducto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_USUARIO", ProsegurDbType.Descricao_Longa, CodigoUsuario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_USUARIO", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_ISO_DIVISA", ProsegurDbType.Descricao_Longa, CodigoIsoDivisa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_DIVISA", ProsegurDbType.Descricao_Longa, DescripcionDivisa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_DENOMINACION", ProsegurDbType.Descricao_Longa, CodigoDenominacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_DENOMINACION", ProsegurDbType.Descricao_Longa, DescripcionDenominacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "BOL_MECANIZADO", ProsegurDbType.Logico, EsMecanizado))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NUM_IMPORTE_TOTAL_BILLETES", ProsegurDbType.Numero_Decimal, NumImporteTotalBilletes))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NUM_IMPORTE_TOTAL_MONEDAS", ProsegurDbType.Numero_Decimal, NumImporteTotalMonedas))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_BILLETES", ProsegurDbType.Inteiro_Longo, NelCantidadTotalBilletes))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_MONEDAS", ProsegurDbType.Inteiro_Longo, NelCantidadTotalMonedas))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_MINUTOS", ProsegurDbType.Inteiro_Longo, NelCantidadTotalMinutos))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Sub

#End Region

    End Class

End Namespace
