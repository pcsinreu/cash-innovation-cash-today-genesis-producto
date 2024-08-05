Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe GrupoCliente
''' </summary>
''' <remarks></remarks>
''' <history>
''' [matheus.araujo] 24/10/2012 Criado
''' </history>
Public Class GrupoCliente

    Private Enum Acao
        Alta
        Actualizar
    End Enum

#Region "[CONSULTAR]"

    Public Shared Function getGruposCliente(objPeticion As ContractoServicio.GrupoCliente.GetGruposCliente.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.GrupoCliente.GrupoClienteColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' monta a query
        Dim query As New StringBuilder
        Dim queryWhere As New StringBuilder

        With objPeticion.GrupoCliente

            ' cláusula WHERE
            If Not String.IsNullOrEmpty(.Codigo) Then
                queryWhere.Append(Util.MontarClausulaLikeUpper(toList(.Codigo), "COD_GRUPO_CLIENTE", comando, "AND"))
            End If

            If Not String.IsNullOrEmpty(.Descripcion) Then
                queryWhere.Append(Util.MontarClausulaLikeUpper(toList(.Descripcion), "DES_GRUPO_CLIENTE", comando, "AND"))
            End If

            If Not String.IsNullOrEmpty(.CodTipoGrupoCliente) Then
                queryWhere.Append(Util.MontarClausulaLikeUpper(toList(.CodTipoGrupoCliente), "COD_TIPO_GRUPO_CLIENTE", comando, "AND"))
            End If

            If .Clientes IsNot Nothing AndAlso .Clientes.Count > 0 Then queryWhere.Append(MontarFiltroCliente(.Clientes, comando))

            query.Append(String.Format(My.Resources.GetGruposCliente.ToString, queryWhere.ToString))

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, .Vigente))

        End With

        ' objeto de retorno
        Dim objGruposCliente As New ContractoServicio.GrupoCliente.GrupoClienteColeccion

        ' prepara e executa a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Dim dtGruposCliente As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtGruposCliente IsNot Nothing AndAlso dtGruposCliente.Rows.Count > 0 Then

            For Each dt As DataRow In dtGruposCliente.Rows
                objGruposCliente.Add(PopularGetGruposCliente(dt))
            Next

        End If

        Return objGruposCliente

    End Function

    Public Shared Function getGruposClientesDetalle(objPeticion As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion) As ContractoServicio.GrupoCliente.GrupoClienteDetalleColeccion

        ' cria comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' monta a query
        Dim query As New StringBuilder

        'query.Append(My.Resources.GetGrupoClienteDetail)

        query.AppendFormat(My.Resources.GetGrupoClienteDetail, Util.MontarClausulaIn(objPeticion.Codigo, "COD_GRUPO_CLIENTE", comando, "", "GC"))
        ' filtro
        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Codigo))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executa a consulta
        Dim dtGrupoClienteDetail As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtGrupoClienteDetail IsNot Nothing AndAlso dtGrupoClienteDetail.Rows.Count > 0 Then
            Return PopularGetGrupoClienteDetail(dtGrupoClienteDetail)
        Else
            Return Nothing
        End If

    End Function

    Private Shared Function verificarCodigoGrupoCliente(codGrupoCliente As String) As Boolean

        ' comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarGrupoCliente)
        comando.CommandType = CommandType.Text

        ' parâmetro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codGrupoCliente))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    Private Shared Function GetOidGrupoCliente(codGrupoCliente As String) As String

        ' comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetOidGrupoCliente)
        comando.CommandType = CommandType.Text

        ' parâmetro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codGrupoCliente))

        ' retorno
        Dim oidGrupoCliente As String = String.Empty

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Return If(dt IsNot Nothing AndAlso dt.Rows.Count > 0, dt.Rows(0)("OID_GRUPO_CLIENTE"), String.Empty)

    End Function

#End Region

#Region "[INSERIR]"

    Public Shared Sub SetGrupoCliente(objPeticion As ContractoServicio.GrupoCliente.SetGruposCliente.Peticion, _
                                           ByRef OidGrupoCliente As String, ByRef objTransacion As Transacao)

        AltaActualizaGrupoCliente(objPeticion, If(verificarCodigoGrupoCliente(objPeticion.GrupoCliente.Codigo), Acao.Actualizar, Acao.Alta), OidGrupoCliente, objTransacion)

    End Sub

    Private Shared Sub AltaActualizaGrupoCliente(objPeticion As ContractoServicio.GrupoCliente.SetGruposCliente.Peticion, _acao As Acao, _
                                                 ByRef OidGrupoCliente As String, ByRef objTransacion As Transacao)

        Try

            ' comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' query
            comando.CommandText = Util.PrepararQuery(If(_acao = Acao.Alta, My.Resources.AltaGrupoCliente, My.Resources.ActualizarGrupoCliente))

            ' guid
            OidGrupoCliente = If(_acao = Acao.Alta, Guid.NewGuid().ToString, GetOidGrupoCliente(objPeticion.GrupoCliente.Codigo))

            ' parâmetros para gepr_grupo_cliente
            With comando.Parameters

                If _acao = Acao.Alta Then .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_CLIENTE", ProsegurDbType.Objeto_Id, OidGrupoCliente))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.GrupoCliente.Codigo))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_GRUPO_CLIENTE", ProsegurDbType.Descricao_Longa, objPeticion.GrupoCliente.Descripcion))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.GrupoCliente.Vigente))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.GrupoCliente.CodigoUsuario))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
                .Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_GRUPO_CLIENTE", ProsegurDbType.Descricao_Longa, objPeticion.GrupoCliente.CodTipoGrupoCliente))
            End With

            ' adiciona na transação
            objTransacion.AdicionarItemTransacao(comando)

            ' se for atualização, limpa os detalles para inserir os novos
            If _acao = Acao.Actualizar Then objTransacion.AdicionarItemTransacao(BorrarGrupoClienteDetalle(OidGrupoCliente))

            ' verifica os clientes
            With objPeticion.GrupoCliente

                If .Clientes IsNot Nothing AndAlso .Clientes.Count > 0 Then

                    For Each _cliente In .Clientes

                        ' ' obtém o oid do cliente
                        'Dim oidCliente As String = Cliente.BuscarOidCliente(_cliente.CodCliente)

                        ' se não encontrou oid do cliente
                        If _cliente.OidCliente.Equals(String.Empty) Then
                            Throw New Excepcion.NegocioExcepcion( _
                                Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                String.Format(Traduzir("006_msg_cliente_nao_encontrado"), _cliente.CodCliente))
                        End If

                        ' insere completo
                        objTransacion.AdicionarItemTransacao(AltaGrupoClienteDetalle(_cliente.OidCliente, _cliente.OidSubCliente, _cliente.OidPtoServivico, OidGrupoCliente))

                        '' verifica os subclientes
                        'If _cliente.SubClientes IsNot Nothing AndAlso _cliente.SubClientes.Count > 0 Then

                        '    For Each _subcliente In _cliente.SubClientes

                        '        ' obtém o oid do subcliente
                        '        Dim oidSubCliente As String = _subcliente.OidSubCliente

                        '        ' se não encontrou oid do subcliente
                        '        If oidSubCliente.Equals(String.Empty) Then
                        '            Throw New Prosegur.Global.GesEfectivo.IAC.Excepcion.NegocioExcepcion( _
                        '                Prosegur.Global.GesEfectivo.IAC.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                        '                String.Format(Traduzir("006_msg_subcliente_nao_encontrado"), _subcliente.CodSubCliente))
                        '        End If

                        '        ' verifica ptos de servicio
                        '        If _subcliente.PtosServicio IsNot Nothing AndAlso _subcliente.PtosServicio.Count > 0 Then

                        '            For Each _ptoservicio In _subcliente.PtosServicio

                        '                ' obtém o oid do pto servicio
                        '                Dim oidPtoServicio As String = PuntoServicio.BuscaOidPuntoServicio(_ptoservicio.CodPtoServicio, oidSubCliente)

                        '                ' se não encontrou o oid do pto servicio
                        '                If oidPtoServicio.Equals(String.Empty) Then
                        '                    Throw New Prosegur.Global.GesEfectivo.IAC.Excepcion.NegocioExcepcion( _
                        '                        Prosegur.Global.GesEfectivo.IAC.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                        '                        String.Format(Traduzir("006_msg_ptoservicio_nao_encontrado"), _ptoservicio.CodPtoServicio))
                        '                End If

                        '                ' insere completo
                        '                objTransacion.AdicionarItemTransacao(AltaGrupoClienteDetalle(_cliente.OidCliente, oidSubCliente, oidPtoServicio, OidGrupoCliente))



                        '            Next _ptoservicio

                        '        Else

                        '            'subcliente sem pto servicio, insere null em pto servicio
                        '            objTransacion.AdicionarItemTransacao(AltaGrupoClienteDetalle(_cliente.OidCliente, oidSubCliente, String.Empty, OidGrupoCliente))

                        '        End If

                        '    Next _subcliente

                        'Else

                        '    'cliente sem subcliente, insere null em subcliente e pto servicio
                        '    objTransacion.AdicionarItemTransacao(AltaGrupoClienteDetalle(_cliente.OidCliente, String.Empty, String.Empty, OidGrupoCliente))

                        'End If

                    Next _cliente

                End If


                ''depois

            End With

        Catch ex As Exception

            Excepcion.Util.Tratar(ex, ex.Message)

        End Try

    End Sub



    Private Shared Function AltaGrupoClienteDetalle(oidCliente As String, oidSubCliente As String, oidPtoServicio As String, oidGrupoCliente As String) As IDbCommand

        ' comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaGrupoClienteDetalle)
        comando.CommandType = CommandType.Text

        ' oid
        Dim oidGrupoClienteDetalle As String = Guid.NewGuid.ToString

        ' parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_CLIENTE_DETALLE", ProsegurDbType.Identificador_Alfanumerico, oidGrupoClienteDetalle))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidPtoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidGrupoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        Return comando

    End Function

#End Region

#Region "[EXCLUIR]"

    Private Shared Function BorrarGrupoClienteDetalle(oidGrupoCliente As String) As IDbCommand

        ' comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' query
        comando.CommandText = Util.PrepararQuery(My.Resources.BorrarGrupoClienteDetalle)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidGrupoCliente))

        Return comando

    End Function

#End Region

#Region "[ÚTEIS]"

    Private Shared Function PopularGetGruposCliente(dr As DataRow) As ContractoServicio.GrupoCliente.GrupoCliente

        'objeto de retorno
        Dim obj As New ContractoServicio.GrupoCliente.GrupoCliente

        Util.AtribuirValorObjeto(obj.Codigo, dr("COD_GRUPO_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(obj.Descripcion, dr("DES_GRUPO_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(obj.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(obj.FyhAtualizacion, dr("FYH_ACTUALIZACION"), GetType(Date))
        Util.AtribuirValorObjeto(obj.CodTipoGrupoCliente, dr("COD_TIPO_GRUPO_CLIENTE"), GetType(String))

        Return obj

    End Function

    Private Shared Function PopularGetGrupoClienteDetail(dt As DataTable) As ContractoServicio.GrupoCliente.GrupoClienteDetalleColeccion

        ' grupoCliente para retorno
        Dim GrupoClientes As New ContractoServicio.GrupoCliente.GrupoClienteDetalleColeccion

        For Each row In dt.Rows
            Dim grupoCliente As New ContractoServicio.GrupoCliente.GrupoClienteDetalle
            grupoCliente.Clientes = New ContractoServicio.GrupoCliente.ClienteDetalleColeccion

            ' informações do grupo de cliente
            Util.AtribuirValorObjeto(grupoCliente.oidGrupoCliente, row("OID_GRUPO_CLIENTE"), GetType(String))
            

           

            Dim existe = From p In GrupoClientes
                         Where p.oidGrupoCliente = grupoCliente.oidGrupoCliente

            If existe IsNot Nothing AndAlso existe.Count() = 1 Then
                existe.FirstOrDefault().Clientes.Add(PopularCliente(row))
            Else
                Util.AtribuirValorObjeto(grupoCliente.Codigo, row("COD_GRUPO_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(grupoCliente.Descripcion, row("DES_GRUPO_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(grupoCliente.Vigente, row("BOL_VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(grupoCliente.CodTipoGrupoCliente, row("COD_TIPO_GRUPO_CLIENTE"), GetType(String))
                Dim direcion = Direccion.RecuperaDireccionesBase(grupoCliente.oidGrupoCliente)

                If direcion IsNot Nothing Then
                    grupoCliente.Direccion = direcion.FirstOrDefault
                End If

                GrupoClientes.Add(grupoCliente)
                grupoCliente.Clientes.Add(PopularCliente(row))

            End If

        Next row

        Return (GrupoClientes)

    End Function

    Private Shared Function PopularCliente(dr) As ContractoServicio.GrupoCliente.ClienteDetalle

        Dim cliente As New ContractoServicio.GrupoCliente.ClienteDetalle

        Util.AtribuirValorObjeto(cliente.OidGrupoClienteDetalle, dr("OID_GRUPO_CLIENTE_DETALLE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.OidCliente, dr("OID_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.CodCliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.DesCliente, dr("DES_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(cliente.OidPtoServivico, dr("OID_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(cliente.CodPtoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(cliente.DesPtoServivico, dr("DES_PTO_SERVICIO"), GetType(String))

        Return cliente

    End Function

    Private Shared Function PopularSubCliente(dr As DataRow) As ContractoServicio.GrupoCliente.SubCliente

        Dim subcliente As New ContractoServicio.GrupoCliente.SubCliente
        Util.AtribuirValorObjeto(subcliente.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(subcliente.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(subcliente.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))

        Return subcliente

    End Function

    Private Shared Function PopularPuntoServicio(dr As DataRow) As ContractoServicio.GrupoCliente.PuntoServicio

        Dim ptoServicio As New ContractoServicio.GrupoCliente.PuntoServicio

        Util.AtribuirValorObjeto(ptoServicio.OidPtoServivico, dr("OID_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(ptoServicio.CodPtoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(ptoServicio.DesPtoServicio, dr("DES_PTO_SERVICIO"), GetType(String))

        Return ptoServicio

    End Function

    Private Shared Function toList(str As String) As List(Of String)
        Dim list As New List(Of String) : list.Add(str) : Return list
    End Function

    Public Shared Function ConverteClienteColeccion(clientes1 As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion) As ContractoServicio.GrupoCliente.ClienteColeccion

        Dim clientes2 As New ContractoServicio.GrupoCliente.ClienteColeccion

        For Each _cliente1 In clientes1

            Dim _cliente2 As New ContractoServicio.GrupoCliente.Cliente With {.CodCliente = _cliente1.Codigo, .DesCliente = _cliente1.Descripcion}
            _cliente2.SubClientes = New ContractoServicio.GrupoCliente.SubClienteColeccion

            For Each _subcliente1 In _cliente1.SubClientes

                Dim _subcliente2 As New ContractoServicio.GrupoCliente.SubCliente With {.CodSubCliente = _subcliente1.Codigo, .DesSubCliente = _subcliente1.Descripcion}
                _subcliente2.PtosServicio = New ContractoServicio.GrupoCliente.PuntoServicioColeccion

                For Each _ptoservicio1 In _subcliente1.PuntosServicio

                    _subcliente2.PtosServicio.Add(New ContractoServicio.GrupoCliente.PuntoServicio With {.CodPtoServicio = _ptoservicio1.Codigo, .DesPtoServicio = _ptoservicio1.Descripcion})

                Next _ptoservicio1

                _cliente2.SubClientes.Add(_subcliente2)

            Next _subcliente1

            clientes2.Add(_cliente2)

        Next _cliente1

        Return clientes2

    End Function

    Public Shared Function MontarFiltroCliente2(clientes As ContractoServicio.GrupoCliente.ClienteColeccion, ByRef comando As IDbCommand, Optional ConsiderarDescricao As Boolean = False) As String
        Dim iCliente As Integer = 0, iSubCliente As Integer = 0, iPtoServicio As Integer = 0
        Dim queryWhere As String = "( UPPER(COD_CLIENTE) in {0} AND UPPER(COD_SUBCLIENTE) in {1} {2} )"
        Dim sCodCliente As String = String.Empty
        Dim sCodSubCliente As String = String.Empty
        Dim sCodPtoServicio As String = String.Empty
        Dim sDesPtoServicio As String = String.Empty
        Dim sCodDesPtoServicio As String = String.Empty

        If clientes IsNot Nothing AndAlso clientes.Count > 0 Then

            sCodCliente = "('" & String.Join("','", clientes.Select(Function(f) f.CodCliente).ToArray) & "')"

            sCodSubCliente = "('" & String.Join("','", (From c In clientes
                                                 From s In c.SubClientes
                                                 Select s.CodSubCliente Distinct).ToArray) & "')"

            sCodPtoServicio = String.Join("|", (From c In clientes
                                                 From s In c.SubClientes
                                                 From p In s.PtosServicio
                                                 Where Not String.IsNullOrEmpty(p.CodPtoServicio)
                                                 Select p.CodPtoServicio Distinct).ToArray)


            If ConsiderarDescricao Then
                sDesPtoServicio = String.Join("|", (From c In clientes
                                                     From s In c.SubClientes
                                                     From p In s.PtosServicio
                                                     Where Not String.IsNullOrEmpty(p.DesPtoServicio)
                                                     Select p.DesPtoServicio Distinct).ToArray)

            End If

            If Not String.IsNullOrEmpty(sCodPtoServicio) Then
                sCodPtoServicio = " and regexp_like(UPPER(COD_PTO_SERVICIO)," & "UPPER('" & sCodPtoServicio & "'))"
            End If
            If Not String.IsNullOrEmpty(sDesPtoServicio) Then
                sDesPtoServicio = " and regexp_like(UPPER(DES_PTO_SERVICIO)," & "UPPER('" & sDesPtoServicio & "'))"
            End If
            If Not String.IsNullOrEmpty(sCodPtoServicio) AndAlso Not String.IsNullOrEmpty(sDesPtoServicio) Then
                sCodDesPtoServicio = sCodPtoServicio & " OR " & sDesPtoServicio
            Else
                sCodDesPtoServicio = sCodPtoServicio & sDesPtoServicio
            End If

            queryWhere = " AND " & String.Format(queryWhere, sCodCliente, sCodSubCliente, sCodDesPtoServicio)

            Return queryWhere

        Else
            Return String.Empty
        End If

    End Function


    Public Shared Function MontarFiltroCliente(clientes As ContractoServicio.GrupoCliente.ClienteColeccion, ByRef comando As IDbCommand, Optional ConsiderarDescricao As Boolean = False) As String

        Dim iCliente As Integer = 0, iSubCliente As Integer = 0, iPtoServicio As Integer = 0
        Dim queryWhere As String = "("

        If clientes IsNot Nothing AndAlso clientes.Count > 0 Then

            For Each _cliente In clientes

                ' consulta apenas por cliente
                If _cliente.SubClientes Is Nothing OrElse _cliente.SubClientes.Count = 0 Then

                    If Not String.IsNullOrEmpty(_cliente.CodCliente) Then
                        queryWhere &= ("(UPPER(COD_CLIENTE) = []COD_CLIENTE" & iCliente.ToString & ") OR ")
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE" & iCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _cliente.CodCliente.ToUpper()))
                    End If

                    iCliente += 1 : iSubCliente += 1 : iPtoServicio += 1

                Else

                    For Each _subcliente In _cliente.SubClientes

                        ' consulta subcliente e pto de serviço
                        If _subcliente.PtosServicio Is Nothing OrElse _subcliente.PtosServicio.Count = 0 Then

                            If Not String.IsNullOrEmpty(_cliente.CodCliente) AndAlso Not String.IsNullOrEmpty(_subcliente.CodSubCliente) Then
                                queryWhere &= ("(UPPER(COD_CLIENTE) = []COD_CLIENTE" & iCliente.ToString & " AND " & _
                                               "UPPER(COD_SUBCLIENTE) = []COD_SUBCLIENTE" & iSubCliente.ToString & ") OR ")
                                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE" & iCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _cliente.CodCliente.ToUpper()))
                                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE" & iSubCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _subcliente.CodSubCliente.ToUpper()))
                            End If

                            iCliente += 1 : iSubCliente += 1 : iPtoServicio += 1

                        Else

                            For Each _ptoServicio In _subcliente.PtosServicio

                                If Not String.IsNullOrEmpty(_cliente.CodCliente) AndAlso Not String.IsNullOrEmpty(_subcliente.CodSubCliente) AndAlso (Not String.IsNullOrEmpty(_ptoServicio.CodPtoServicio & _ptoServicio.DesPtoServicio)) Then

                                    queryWhere &= ("(UPPER(COD_CLIENTE) = []COD_CLIENTE" & iCliente.ToString & " AND " & _
                                                   "UPPER(COD_SUBCLIENTE) = []COD_SUBCLIENTE" & iSubCliente.ToString & _
                                                   If(ConsiderarDescricao, _
                                                        ( _
                                                            If(Not String.IsNullOrEmpty(_ptoServicio.CodPtoServicio), " AND UPPER(COD_PTO_SERVICIO) LIKE []COD_PTO_SERVICIO" & iPtoServicio.ToString, "") & _
                                                            If(Not String.IsNullOrEmpty(_ptoServicio.DesPtoServicio), " AND UPPER(DES_PTO_SERVICIO) LIKE []DES_PTO_SERVICIO" & iPtoServicio.ToString, "") & ") OR"
                                                        ), _
                                                        " AND UPPER(COD_PTO_SERVICIO) = []COD_PTO_SERVICIO" & iPtoServicio.ToString & ") OR " _
                                                    ))

                                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE" & iCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _cliente.CodCliente.ToUpper()))
                                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE" & iSubCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _subcliente.CodSubCliente.ToUpper()))
                                    If Not ConsiderarDescricao Then
                                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO" & iSubCliente.ToString, ProsegurDbType.Identificador_Alfanumerico, _ptoServicio.CodPtoServicio.ToUpper()))
                                    Else
                                        If (Not String.IsNullOrEmpty(_ptoServicio.CodPtoServicio)) Then
                                            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO" & iPtoServicio.ToString, ProsegurDbType.Identificador_Alfanumerico, "%" & _ptoServicio.CodPtoServicio.ToUpper() & "%"))
                                        End If
                                        If (Not String.IsNullOrEmpty(_ptoServicio.DesPtoServicio)) Then
                                            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO" & iPtoServicio.ToString, ProsegurDbType.Descricao_Longa, "%" & _ptoServicio.DesPtoServicio.ToUpper() & "%"))
                                        End If
                                    End If

                                    iCliente += 1 : iSubCliente += 1 : iPtoServicio += 1

                                End If
                            Next _ptoServicio

                        End If

                    Next _subcliente

                End If



            Next _cliente

        End If

        Return If(queryWhere.Length = 1, String.Empty, "AND " & queryWhere.Substring(0, queryWhere.Length - 3) & ")")

    End Function

#End Region

End Class
