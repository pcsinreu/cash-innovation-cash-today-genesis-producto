Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis
    ''' <summary>
    ''' Classe GrupoTerminosIAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/09/2013
    ''' </history>
    Public Class GrupoTerminosIAC


        Public Shared Sub obtenerGrupoTerminosIACPorProcesoIAC(ByRef filtroConsulta As ObservableCollection(Of Clases.Transferencias.FiltroProcesoIAC))

            If filtroConsulta IsNot Nothing AndAlso filtroConsulta.Count > 0 Then

                Dim dtProcesoIAC As DataTable = Nothing

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim queryDefecto As String = My.Resources.obtenerGrupoTerminosIACPorProcesoIAC.ToString
                        Dim i As Integer = 0

                        command.CommandText = ""
                        command.CommandType = CommandType.Text

                        For Each _filtro In filtroConsulta

                            If i > 0 Then command.CommandText &= vbNewLine & " UNION " & vbNewLine

                            command.CommandText &= String.Format("SELECT * FROM (" & queryDefecto & ") Q" & i.ToString, "_" & i.ToString)

                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoCliente))
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoDelegacion))
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoSubCanal))
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DELEGACIONCENTRAL_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoDelegacionCentral))

                            If String.IsNullOrEmpty(_filtro.codigoSubCliente) Then
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
                            Else
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoSubCliente))
                            End If

                            If String.IsNullOrEmpty(_filtro.codigoPuntoServicio) Then
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
                            Else
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO_" & i.ToString, ProsegurDbType.Identificador_Alfanumerico, _filtro.codigoPuntoServicio))
                            End If

                            i = i + 1

                        Next

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                        dtProcesoIAC = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    End Using

                Catch ex As Exception
                    Throw ex
                Finally
                    GC.Collect()
                End Try

                If dtProcesoIAC IsNot Nothing AndAlso dtProcesoIAC.Rows.Count > 0 Then

                    For Each proceso In dtProcesoIAC.Rows

                        Dim item As Clases.Transferencias.FiltroProcesoIAC = filtroConsulta.FirstOrDefault(Function(x) x.codigoCliente = Util.AtribuirValorObj(proceso("COD_CLIENTE"), GetType(String)) AndAlso
                                                                                                               x.codigoDelegacion = Util.AtribuirValorObj(proceso("COD_DELEGACION"), GetType(String)) AndAlso
                                                                                                               x.codigoPuntoServicio = Util.AtribuirValorObj(proceso("COD_PTO_SERVICIO"), GetType(String)) AndAlso
                                                                                                               x.codigoSubCanal = Util.AtribuirValorObj(proceso("COD_SUBCANAL"), GetType(String)) AndAlso
                                                                                                               x.codigoSubCliente = Util.AtribuirValorObj(proceso("COD_SUBCLIENTE"), GetType(String)))

                        If proceso.Table.Columns.Contains("COD_IAC_PARCIAL") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(proceso("COD_IAC_PARCIAL"), GetType(String))) Then
                            item.grupoTerminosIAC_Parcial = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(Util.AtribuirValorObj(proceso("COD_IAC_PARCIAL"), GetType(String)))
                        End If

                        If proceso.Table.Columns.Contains("COD_IAC_BULTO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(proceso("COD_IAC_BULTO"), GetType(String))) Then
                            item.grupoTerminosIAC_Bulto = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(Util.AtribuirValorObj(proceso("COD_IAC_BULTO"), GetType(String)))
                        End If

                        If proceso.Table.Columns.Contains("OID_IAC_REMESA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(proceso("OID_IAC_REMESA"), GetType(String))) Then
                            item.grupoTerminosIAC_Remesa = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(Util.AtribuirValorObj(proceso("OID_IAC_REMESA"), GetType(String)))
                        End If

                    Next

                End If

            End If

        End Sub

        Public Shared Function RecuperarIACs() As List(Of Clases.GrupoTerminosIAC)
            Dim grupoTerminosIACs As List(Of Clases.GrupoTerminosIAC)

            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = sarmarWrapperRecuperarIACs()
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                grupoTerminosIACs = poblarRespuestaRecuperarIACs(ds)

            Catch ex As Exception
                Throw ex
            End Try


            Return grupoTerminosIACs
        End Function

        Private Shared Function poblarRespuestaRecuperarIACs(ds As DataSet) As List(Of Clases.GrupoTerminosIAC)
            Dim lstGrupos As New List(Of Clases.GrupoTerminosIAC)()
            Dim objTermino As Clases.TerminoIAC
            Dim objGrupo As Clases.GrupoTerminosIAC

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("iac_terminos") Then
                    For Each fila As DataRow In ds.Tables("iac_terminos").Rows
                        objTermino = New Clases.TerminoIAC

                        objTermino.Identificador = Util.AtribuirValorObj(fila("OID_TERMINO"), GetType(String))
                        objTermino.Codigo = Util.AtribuirValorObj(fila("COD_TERMINO"), GetType(String))
                        objTermino.Descripcion = Util.AtribuirValorObj(fila("DES_TERMINO"), GetType(String))


                        objGrupo = lstGrupos.Where(Function(x) x.Codigo = Util.AtribuirValorObj(fila("COD_IAC"), GetType(String))).FirstOrDefault

                        If objGrupo Is Nothing Then
                            objGrupo = New Clases.GrupoTerminosIAC()
                            objGrupo.TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)

                            objGrupo.Codigo = Util.AtribuirValorObj(fila("COD_IAC"), GetType(String))
                            objGrupo.Identificador = Util.AtribuirValorObj(fila("OID_IAC"), GetType(String))
                            objGrupo.Descripcion = Util.AtribuirValorObj(fila("DES_IAC"), GetType(String))
                            lstGrupos.Add(objGrupo)
                        End If
                        objGrupo.TerminosIAC.Add(objTermino)
                    Next
                End If
            End If

            Return lstGrupos
        End Function

        Private Shared Function sarmarWrapperRecuperarIACs() As SPWrapper
            Dim SP As String = String.Format("GEPR_PTERMINOS_IAC_{0}.srecupera_iac_terminos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$rcIAC", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "iac_terminos")

            Return spw
        End Function

        Shared Function ObtenerGrupoTerminosIAC(identificadores As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerGrupoIAC_v2
                Dim filtro As String = ""

                If identificadores IsNot Nothing Then
                    If identificadores.Count = 1 Then
                        filtro &= " AND IAC.OID_IAC = []OID_IAC "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Descricao_Curta, identificadores(0)))
                    ElseIf identificadores.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadores, "OID_IAC", cmd, "AND", "IAC", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function



        ''' <summary>
        ''' Obtener el GrupoTerminosIAC por lo Identificador
        ''' </summary>
        ''' <param name="identificadorGrupoTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerGrupoTerminosIACPorIdentificador(identificadorGrupoTerminosIAC As String) As DataTable

            If String.IsNullOrEmpty(identificadorGrupoTerminosIAC) Then
                Return Nothing
            End If

            Dim dt As DataTable
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoTerminosIACRecuperarPorIdentificador)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificadorGrupoTerminosIAC))

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using
            Return dt

        End Function





















        ''' <summary>
        ''' Recupera os GrupoTerminosIAC.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarGrupoTerminosIAC(identificador As String) As Clases.GrupoTerminosIAC

            If String.IsNullOrEmpty(identificador) Then
                Return Nothing
            End If

            Dim objGrupoTerminosIAC As Clases.GrupoTerminosIAC = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoTerminosIACRecuperarPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificador))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count Then

                objGrupoTerminosIAC = New Clases.GrupoTerminosIAC
                With objGrupoTerminosIAC
                    .Identificador = identificador
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_IAC"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_IAC"), GetType(String))
                    .Observacion = Util.AtribuirValorObj(dt.Rows(0)("OBS_IAC"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(Boolean))
                    .EsInvisible = Util.AtribuirValorObj(dt.Rows(0)("BOL_INVISIBLE"), GetType(Boolean))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(DateTime))
                    .CopiarDeclarados = Util.AtribuirValorObj(dt.Rows(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                    .TerminosIAC = TerminoIAC.RecuperarTerminosIAC(identificador)
                End With

            End If

            Return objGrupoTerminosIAC

        End Function

        ''' <summary>
        ''' Recupera os GrupoTerminosIAC.
        ''' </summary>
        ''' <param name="Codigo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarGrupoTerminosIACPorCodigo(Codigo As String) As Clases.GrupoTerminosIAC

            If String.IsNullOrEmpty(Codigo) Then
                Return Nothing
            End If

            Dim objGrupoTerminosIAC As Clases.GrupoTerminosIAC = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoTerminosIACRecuperarPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IAC", ProsegurDbType.Objeto_Id, Codigo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count Then

                objGrupoTerminosIAC = New Clases.GrupoTerminosIAC
                With objGrupoTerminosIAC
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_IAC"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_IAC"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_IAC"), GetType(String))
                    .Observacion = Util.AtribuirValorObj(dt.Rows(0)("OBS_IAC"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(Boolean))
                    .EsInvisible = Util.AtribuirValorObj(dt.Rows(0)("BOL_INVISIBLE"), GetType(Boolean))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(DateTime))
                    .CopiarDeclarados = Util.AtribuirValorObj(dt.Rows(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                    .TerminosIAC = TerminoIAC.RecuperarTerminosIAC(Util.AtribuirValorObj(dt.Rows(0)("OID_IAC"), GetType(String)))
                End With

            End If

            Return objGrupoTerminosIAC

        End Function
    End Class

End Namespace
