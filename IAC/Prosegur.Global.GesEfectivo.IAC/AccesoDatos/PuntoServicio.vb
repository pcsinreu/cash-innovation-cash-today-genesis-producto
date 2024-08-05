Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class PuntoServicio

#Region "NOVOS METODOS"


    Public Shared Function GetPuntoServicio(Of T As ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion, Optional completo As Boolean = False) As ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicioColeccion(Of T)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetPuntoServicio)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPeticion.CodCliente) Then
            comando.CommandText &= " AND C.COD_CLIENTE = :COD_CLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodCliente))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodSubcliente) Then
            comando.CommandText &= " AND S.COD_SUBCLIENTE = :COD_SUBCLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodSubcliente))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodPtoServicio) Then
            comando.CommandText &= " AND UPPER(P.COD_PTO_SERVICIO) = :COD_PTO_SERVICIO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodPtoServicio.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(objPeticion.DesPtoServicio) Then
            comando.CommandText &= " AND UPPER(P.DES_PTO_SERVICIO) LIKE :DES_PTO_SERVICIO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DesPtoServicio.ToUpper() & "%"))
        End If

        If objPeticion.BolVigente IsNot Nothing Then
            comando.CommandText &= " AND P.BOL_VIGENTE = :BOL_VIGENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.BolVigente))
        End If

        If objPeticion.OidTipoPuntoServicio IsNot Nothing Then
            comando.CommandText &= " AND P.OID_TIPO_PUNTO_SERVICIO = :OID_TIPO_PUNTO_SERVICIO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidTipoPuntoServicio))
        End If

        If objPeticion.BolTotalizadorSaldo IsNot Nothing Then
            comando.CommandText &= " AND P.BOL_TOTALIZADOR_SALDO = :BOL_TOTALIZADOR_SALDO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPeticion.BolTotalizadorSaldo))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidPuntoServicio) Then
            comando.CommandText &= " AND P.OID_PTO_SERVICIO = :OID_PTO_SERVICIO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidPuntoServicio))
        End If

        comando.CommandText &= " ORDER BY P.COD_PTO_SERVICIO"

        Dim dtPuntoServicio As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        Return RetornaColecaoPuntoServicio(Of T)(dtPuntoServicio, completo)

    End Function

    Public Shared Function GetPuntoServicioDetalle(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicioColeccion(Of ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio)

        Dim col As New ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicioColeccion(Of ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio)
        Dim ret = GetPuntoServicio(Of ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio)(objPeticion, parametrosRespuestaPaginacion, True)

        col.AddRange(ret.GetRange(0, ret.Count))
        ret = Nothing

        Return col

    End Function

    Public Shared Function AltaPuntoServicio(objPuntoServicio As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio, codigoUsuario As String) As String

        Dim oidPuntoServicio As String

        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaPuntoServicioV2)
            comando.CommandType = CommandType.Text

            oidPuntoServicio = Guid.NewGuid.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, oidPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, objPuntoServicio.OidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPuntoServicio.CodPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPuntoServicio.DesPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPuntoServicio.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, objPuntoServicio.OidTipoPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPuntoServicio.BolTotalizadorSaldo))

            objTransacao.AdicionarItemTransacao(comando)

            objTransacao.RealizarTransacao()

            Return oidPuntoServicio
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("056_msg_erro_Insereregistro"))
        End Try

        Return Nothing


    End Function

    ' Buscar nivel saldo ponto de serviço
    Public Shared Function BuscarPuntoServicioNivelSaldo(oidPuntoServicio As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarPuntoServicioNivelSaldo.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidPuntoServicio))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    Public Shared Sub ActualizarPuntoServicio(objPuntoServicio As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio, codigoUsuario As String)

        Try

            Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, objPuntoServicio.OidPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, objPuntoServicio.OidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPuntoServicio.CodPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPuntoServicio.DesPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPuntoServicio.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, objPuntoServicio.OidTipoPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPuntoServicio.BolTotalizadorSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarPuntoServicioV2)
            comando.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("056_msg_erro_Atualizaregistro"))
        End Try

    End Sub

    Public Shared Sub BajaPuntoServicio(objPuntoServicio As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio, codigoUsuario As String)
        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, objPuntoServicio.OidPuntoServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            comando.CommandText = Util.PrepararQuery(My.Resources.BajaPuntoServicioV2)
            comando.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("056_msg_erro_BajaRegistro"))
        End Try
    End Sub

    Private Shared Function RetornaColecaoPuntoServicio(Of T As ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)(dtPuntoServicio As DataTable, completo As Boolean) As ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicioColeccion(Of T)

        Dim objPuntoServicioColeccion As New ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicioColeccion(Of T)

        If dtPuntoServicio.Rows.Count > 0 AndAlso dtPuntoServicio IsNot Nothing Then

            Dim objPuntoServicio As ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio = Nothing

            For Each dr In dtPuntoServicio.Rows

                If completo Then
                    objPuntoServicio = New ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio()
                Else
                    objPuntoServicio = New ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio()
                End If


                Util.AtribuirValorObjeto(objPuntoServicio.OidPuntoServicio, dr("OID_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.CodPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.DesPuntoServicio, dr("DES_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(objPuntoServicio.OidTipoPuntoServicio, dr("OID_TIPO_PUNTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.BolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

                Util.AtribuirValorObjeto(objPuntoServicio.OidCliente, dr("OID_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.DesCliente, dr("DES_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.CodCliente, dr("COD_CLIENTE"), GetType(String))

                Util.AtribuirValorObjeto(objPuntoServicio.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))

                Util.AtribuirValorObjeto(objPuntoServicio.CodTipoPuntoServicio, dr("COD_TIPO_PUNTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.DesTipoPuntoServicio, dr("DES_TIPO_PUNTO_SERVICIO"), GetType(String))

                If completo Then
                    Dim objCompleto As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio = objPuntoServicio
                    'Recupera os Codigos Ajenos
                    objCompleto.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_PTO_SERVICIO").ToString())

                    'Recupera as Direcciones
                    objCompleto.Direcciones = Direccion.RecuperaDireccionesBase(dr("OID_PTO_SERVICIO").ToString())
                End If

                objPuntoServicioColeccion.Add(objPuntoServicio)
            Next
        End If

        Return objPuntoServicioColeccion
    End Function

    Private Shared Function RetornaColecaoPuntoServicio(Of T)(dtRetorno As DataTable) As T

        Select Case GetType(T)
            Case GetType(ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.PuntoServicio)

                If dtRetorno.Rows.Count > 0 AndAlso dtRetorno IsNot Nothing Then

                    For Each dr In dtRetorno.Rows

                        Dim objPuntoServicio As New ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.PuntoServicio

                        Util.AtribuirValorObjeto(objPuntoServicio.OidPuntoServicio, dr("OID_PTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.CodPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.DesPuntoServicio, dr("DES_PTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.OidTipoPuntoServicio, dr("OID_TIPO_PUNTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.BolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

                        Util.AtribuirValorObjeto(objPuntoServicio.OidCliente, dr("OID_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.DesCliente, dr("DES_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.CodCliente, dr("COD_CLIENTE"), GetType(String))

                        Util.AtribuirValorObjeto(objPuntoServicio.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))

                        Util.AtribuirValorObjeto(objPuntoServicio.CodTipoPuntoServicio, dr("COD_TIPO_PUNTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(objPuntoServicio.DesTipoPuntoServicio, dr("DES_TIPO_PUNTO_SERVICIO"), GetType(String))

                        Return CType(objPuntoServicio, Object)
                    Next
                End If

        End Select

    End Function

#End Region

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Busca Todos os SubCanais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function BuscaTodosPuntoServicio(oidSubCliente As String) As DataTable

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTodosPuntoServicio.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", _
                                                          ProsegurDbType.Identificador_Alfanumerico, oidSubCliente))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function PopularBuscaPuntoServicio(dr As DataRow) As SetCliente.PuntoServicio

        Dim objPuntoServicio As New SetCliente.PuntoServicio

        Util.AtribuirValorObjeto(objPuntoServicio.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objPuntoServicio

    End Function

    ''' <summary>
    ''' Retorna os SubClientes e seus punto de servicio
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function GetComboPuntosServiciosBySubcliente(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Cliente

        ' criar objeto cliente
        Dim objCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Cliente

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim filtro As New StringBuilder

        ' obter query
        If objPeticion.BolATM Then
            comando.CommandText = My.Resources.GetComboPuntosServiciosByClienteSubclienteATM.ToString
        Else
            comando.CommandText = My.Resources.GetComboPuntosServiciosByClienteSubcliente.ToString
        End If

        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))

        'chama o metodo para montar a query
        filtro = MontaQueryPuntosServiciosBySubcliente(objPeticion, comando)

        comando.CommandText = comando.CommandText & filtro.ToString

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        If objPeticion.TotalizadorSaldo Then
            comando.CommandText &= " AND PTSERV.BOL_TOTALIZADOR_SALDO = :BOL_TOTALIZADOR_SALDO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPeticion.TotalizadorSaldo))
        End If

        If objPeticion.vigente IsNot Nothing Then
            comando.CommandText &= " AND PTSERV.BOL_VIGENTE = :BOL_VIGENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.vigente))
        End If

        ' executar query
        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' percorre o dt e devolve um objcliente
        objCliente = RetornaColByClienteSubCliente(dtCliente)

        ' retornar coleção de termino
        Return objCliente
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 09/11/2012
    ''' </history>
    Public Shared Function GetComboPuntosServiciosByClientesSubClientes(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim filtro As String = GrupoCliente.MontarFiltroCliente2(GrupoCliente.ConverteClienteColeccion(objPeticion.Clientes), comando, True)

        comando.CommandText = Util.PrepararQuery(String.Format(My.Resources.GetComboPuntosServiciosByClientesSubclientes, filtro))

        ' executar query
        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' percorre o dt e retorna os clientes
        Return RetornaColByClientesSubClientes(dtCliente)

    End Function

    ''' <summary>
    ''' Monta query metodo GetComboPuntosServiciosByClienteSubcliente
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function MontaQueryPuntosServiciosBySubcliente(peticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtro As New StringBuilder

        'monta a query de acordo com os parametros informados
        filtro.Append(Util.MontarClausulaIn(peticion.CodigoSubcliente, "COD_SUBCLIENTE", comando, "AND", "SUBC"))

        filtro.Append(Util.MontarClausulaLikeUpper(peticion.CodigoPuntoServicio, "COD_PTO_SERVICIO", comando, "AND", "PTSERV"))

        filtro.Append(Util.MontarClausulaLikeUpper(peticion.DescripcionPuntoServicio, "DES_PTO_SERVICIO", comando, "AND", "PTSERV"))

        Return filtro
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna o cliente com seus subclientes e punto de servicio
    ''' </summary>
    ''' <param name="dtCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </sumary>
    Private Shared Function RetornaColByClienteSubCliente(dtCliente As DataTable) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Cliente

        Dim objCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Cliente
        Dim objsubCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente
        Dim codigoSubCliente As String = String.Empty

        'Verifica se o dt contem clientes
        If dtCliente IsNot Nothing _
            AndAlso dtCliente.Rows.Count > 0 Then

            Util.AtribuirValorObjeto(objCliente.Codigo, dtCliente.Rows(0)("COD_CLIENTE"), GetType(String))

            Util.AtribuirValorObjeto(objCliente.Descripcion, dtCliente.Rows(0)("DES_CLIENTE"), GetType(String))

            objCliente.SubClientes = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubClienteColeccion

            'Percorre o dt e retorna os subclientes.
            objCliente.SubClientes = SubCliente.RetornaColSubClientes(dtCliente)

            Dim codPuntoServicio As New List(Of String)

            'Percorre o dt e armazena agora os seus punto de servicio
            If objCliente.SubClientes.Count > 0 AndAlso objCliente.SubClientes IsNot Nothing Then

                For Each sc As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente In objCliente.SubClientes

                    sc.PuntosServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

                    sc.PuntosServicio = RetornaPuntoServico(dtCliente, sc.Codigo, codPuntoServicio)

                Next
            End If

        End If
        codigoSubCliente = Nothing

        Return objCliente

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna os clientes com subclientes e ptos de serviço
    ''' </summary>
    ''' <param name="dtClientes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 09/11/2012
    ''' </history>
    Private Shared Function RetornaColByClientesSubClientes(dtClientes As DataTable) As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion

        Dim clientes As New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Dim cliente As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing
        Dim subcliente As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = Nothing

        Dim auxCliente As String = String.Empty
        Dim auxSubcliente As String = String.Empty

        For Each row In dtClientes.Rows

            ' novo cliente
            If Not auxCliente.Equals(row("COD_CLIENTE")) Then

                ' se não for o primeiro
                If cliente IsNot Nothing Then
                    cliente.SubClientes.Add(subcliente)
                    clientes.Add(cliente)
                End If

                cliente = New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente

                cliente.Codigo = row("COD_CLIENTE") : cliente.Descripcion = row("DES_CLIENTE")

                cliente.SubClientes = New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
                subcliente = Nothing

                auxCliente = row("COD_CLIENTE")

            End If

            ' novo subcliente
            If subcliente Is Nothing OrElse Not auxSubcliente.Equals(row("COD_SUBCLIENTE")) Then

                ' se não for o primeiro
                If subcliente IsNot Nothing Then cliente.SubClientes.Add(subcliente)

                subcliente = New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente

                subcliente.Codigo = row("COD_SUBCLIENTE") : subcliente.Descripcion = row("DES_SUBCLIENTE")
                subcliente.PuntosServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion

                auxSubcliente = row("COD_SUBCLIENTE")

            End If

            ' ponto de serviço
            Dim ptoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio
            Util.AtribuirValorObjeto(ptoServicio.Codigo, row("COD_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(ptoServicio.Descripcion, row("DES_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(ptoServicio.CodigoAjenoPuntoServicio, row("COD_AJENO"), GetType(String))
            Util.AtribuirValorObjeto(ptoServicio.DescripcionAjenoPuntoServicio, row("DES_AJENO"), GetType(String))
            subcliente.PuntosServicio.Add(ptoServicio)

        Next

        ' adiciona últimos subcliente e cliente
        If cliente IsNot Nothing Then
            If subcliente IsNot Nothing Then cliente.SubClientes.Add(subcliente)
            clientes.Add(cliente)
        End If

        Return If(clientes.Count = 0, Nothing, clientes)

    End Function

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function SelectColPuntoServicio(objColPunto As List(Of String), codigo As String) As Boolean

        Dim retorno = From c In objColPunto Where c = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Popula Punto de Servicio
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaGetPuntoServicioBySubCliente(dr As DataRow) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

        Dim objPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

        Util.AtribuirValorObjeto(objPuntoServicio.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.OidPuntoServicio, dr("OID_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.CodigoAjenoPuntoServicio, dr("COD_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.DescripcionAjenoPuntoServicio, dr("DES_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objPuntoServicio.TotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

        Return objPuntoServicio

    End Function

    ''' <summary>
    ''' Verifica se o subcliente tem algum punto de servicio.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Created
    ''' </history>
    Public Shared Function VerificaPuntoServicio(dr As DataRow) As Boolean
        If dr("COD_PTO_SERVICIO") IsNot DBNull.Value Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Retorna uma coleção de punto servicio
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="codigo"></param>
    ''' <param name="codPuntoServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaPuntoServico(dt As DataTable, codigo As String, ByRef codPuntoServicio As List(Of String)) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

        Dim objPuntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Dim objColPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

        For Each dr As DataRow In dt.Rows

            If SelectColPuntoServicio(codPuntoServicio, dr("COD_SUBCLIENTE") & "|" & dr("COD_PTO_SERVICIO")) = False Then

                If codigo = dr("COD_SUBCLIENTE") Then

                    If VerificaPuntoServicio(dr) Then

                        objPuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
                        objPuntoServicio = PopulaGetPuntoServicioBySubCliente(dr)
                        objColPuntoServicio.Add(objPuntoServicio)
                        codPuntoServicio.Add(dr("COD_SUBCLIENTE") & "|" & dr("COD_PTO_SERVICIO"))

                    End If

                End If

            End If
        Next

        Return objColPuntoServicio

    End Function

    ''' <summary>
    ''' Busca o oid do Punto de Servicio
    ''' </summary>
    ''' <param name="Codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 Criado
    ''' </history>
    Public Shared Function BuscaOidPuntoServicio(codigo As String, oidSubCliente As String) As String


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidPuntoServicio.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidSubCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dt esta preenchido.
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("OID_PTO_SERVICIO").ToString
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' Verifica código PtoServicio
    ''' </summary>
    ''' <param name="CodCliente"></param>
    ''' <param name="CodSubCliente"></param>
    ''' <param name="CodPtoServicio"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/03/2013 Criado
    ''' </history>
    Public Shared Function VerificarCodigoPtoServicio(CodCliente As String, CodSubCliente As String, CodPtoServicio As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoPtoServicio.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodPtoServicio))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0


    End Function

    ''' <summary>
    ''' Verifica descrição do PtoServicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarDescripcionPtoServicio(CodCliente As String, CodSubCliente As String, DesPtoServicio As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionPtoServicio.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, CodCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, codSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, DesPtoServicio))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Verifica se o subcliente enviado está ativo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Criado por pgoncalves - 04/07/2013
    Public Shared Function VerificaSubclienteAtivo(OidSubCliente As String) As ContractoServicio.SubCliente.GetSubClientes.SubCliente

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaSubclienteAtivo)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, OidSubCliente))

        Dim dtSubcliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim subCliente As New ContractoServicio.SubCliente.GetSubClientes.SubCliente

        If dtSubcliente.Rows.Count > 0 AndAlso _
            dtSubcliente.Rows IsNot Nothing Then
            For Each dr As DataRow In dtSubcliente.Rows
                subCliente.BolVigente = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean))
                subCliente.OidCliente = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                subCliente.OidSubCliente = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))
            Next
        End If

        Return subCliente
    End Function

    ''' <summary>
    ''' Verifica se o cliente enviado está ativo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Criado por pgoncalves - 04/07/2013
    Public Shared Function VerificaClienteAtivo(OidSubCliente As String) As Boolean

        Dim objSubCliente As New ContractoServicio.SubCliente.GetSubClientes.SubCliente

        objSubCliente = VerificaSubclienteAtivo(OidSubCliente)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaClienteAtivo)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidCliente))

        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim resultado As Boolean = False

        If dtCliente.Rows.Count > 0 AndAlso dtCliente.Rows IsNot Nothing Then
            For Each dr As DataRow In dtCliente.Rows
                resultado = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean))
            Next
        End If

        Return resultado

    End Function

    Public Shared Function GetPuntoServicioByCodigoAjeno(Of T)(identificadorAjeno As String, puntoServicioCodigoAjeno As String) As T

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetPuntoServicioByCodigoAjeno)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(identificadorAjeno) Then
            comando.CommandText &= " AND CA.COD_IDENTIFICADOR = :COD_IDENTIFICADOR"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, identificadorAjeno))
        End If

        If Not String.IsNullOrEmpty(puntoServicioCodigoAjeno) Then
            comando.CommandText &= " AND CA.COD_AJENO = :COD_AJENO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, puntoServicioCodigoAjeno))
        End If

        comando.CommandText &= " ORDER BY P.COD_PTO_SERVICIO"

        Dim dtRetorno As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Return RetornaColecaoPuntoServicio(Of T)(dtRetorno)

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta Todos os SubCanais do Canal Informado
    ''' </summary>
    ''' <param name="CodigoCliente"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Sub BajaPuntoServicioNivelCliente(CodigoCliente As String, _
                                                       CodigoUsuario As String, _
                                                       ByRef objTransacion As DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaPuntoServicio.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        objTransacion.AdicionarItemTransacao(comando)


    End Sub

    ''' <summary>
    ''' Atualiza o status do puntos de acordo com o status do subcliente
    ''' </summary>
    ''' <param name="OidCliente"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Sub BajaPuntoServicioNivelSubCliente(OidCliente As String, _
                                                       CodigoUsuario As String, _
                                                       ByRef objTransacion As DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaPuntoServicioNivelSubCliente.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, OidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacion.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Deleta Todos os SubCanais do Canal Informado
    ''' </summary>
    ''' <param name="oidSubCliente"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Sub BajaPuntoServicioByOidSubCliente(oidSubCliente As String, _
                                                       CodigoUsuario As String, _
                                                       ByRef objTransacion As DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaPuntoServicioByOidSubCliente.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o SubCanal no DB.
    ''' </summary>
    ''' <param name="objPuntoServicio"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Sub AltaPuntoServicio(objPuntoServicio As SetCliente.PuntoServicio, _
                                        CodigoUsuario As String, _
                                        oidSubCliente As String, ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            ' Obtêm o comando
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaPuntoServicio.ToString())
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPuntoServicio.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, objPuntoServicio.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPuntoServicio.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKPuntoServicio"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do SubCanais do DB.
    ''' </summary>
    ''' <param name="objPuntoServicio">Objeto com os dados do Ponto de Serviço</param>
    ''' <param name="CodigoUsuario">Código do Usuário</param>
    ''' <param name="oidPuntoServicio">Identificador do Sub Cliente</param>
    ''' <param name="objtransacion">Objeto com a transação</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [maoliveira]   12/05/2010 Alterado
    ''' </history>
    Public Shared Sub ActualizarPuntoServicio(objPuntoServicio As SetCliente.PuntoServicio, _
                                              CodigoUsuario As String, _
                                              oidPuntoServicio As String, _
                                              ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' Obtêm o comando
            Dim query As New StringBuilder
            query.Append("UPDATE GEPR_TPUNTO_SERVICIO SET ")

            ' adicionar campos
            query.Append(Util.AdicionarCampoQuery("BOL_VIGENTE = []BOL_VIGENTE,", "BOL_VIGENTE", comando, objPuntoServicio.Vigente, ProsegurDbType.Logico))

            If objPuntoServicio.Vigente Then
                query.Append(Util.AdicionarCampoQuery("DES_PTO_SERVICIO = []DES_PTO_SERVICIO,", "DES_PTO_SERVICIO", comando, objPuntoServicio.Descripcion, ProsegurDbType.Descricao_Longa))
            End If

            query.Append("COD_USUARIO = []COD_USUARIO, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

            query.Append("BOL_ENVIADO_SALDOS = []BOL_ENVIADO_SALDOS, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ENVIADO_SALDOS", ProsegurDbType.Logico, 0))

            query.Append("FYH_ACTUALIZACION = []FYH_ACTUALIZACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            ' adicionar clausula where
            query.Append("WHERE OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidPuntoServicio))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKPuntoServicio"))
        End Try

    End Sub

    ''' <summary>
    ''' Atualiza o status do punto servicio. O campo exportado define se ele sera enviado para o saldos para atualizacao ou insercao
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 24/09/09 Criado
    ''' </history>
    Public Shared Sub ActualizarPuntoServicioExportado(CodCliente As String, CodSubCliente As String, _
                                                       CodPuntoServicio As String, Exportado As Boolean)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarPuntoServicioExportado.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ENVIADO_SALDOS", ProsegurDbType.Logico, Exportado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodPuntoServicio))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

    Public Shared Sub ActualizarMaquina(oidPuntoServicio As String, oidMaquina As String, _
                                                       codUsuario As String, _
                                            Optional ByRef objTransacion As Transacao = Nothing)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarMaqPuntoServicio.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Identificador_Alfanumerico, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Longa, codUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.UtcNow))

        If objTransacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            objTransacion.AdicionarItemTransacao(comando)
        End If

    End Sub

    Public Shared Sub DesvincularMaquina(oidMaquina As String, _
                                                       codUsuario As String, _
                                            Optional ByRef objTransacion As Transacao = Nothing)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.DesvincularMaqPuntoServicio.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Identificador_Alfanumerico, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Longa, codUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.UtcNow))

        If objTransacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            objTransacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

    ''' <summary>
    ''' Atualiza o status do punto de servicio conforme o status do subcliente
    ''' </summary>
    ''' <param name="oidCliente"></param>
    ''' <param name="BolVigente"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Shared Sub AtualizaPuntoServicioConformeSubcliente(oidCliente As String, BolVigente As Boolean, _
                                                              CodigoUsuario As String, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaSubclientePuntoServicio)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        'AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        objTransacao.AdicionarItemTransacao(comando)
    End Sub

    ''' <summary>
    ''' Atualuza o punto de servicio conforme a atualização do subcliente
    ''' </summary>
    ''' <param name="oidCliente"></param>
    ''' <param name="BolVigente"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Shared Sub AtualizaPuntoServicioConformeSubclienteV2(oidCliente As String, BolVigente As Boolean, _
                                                              CodigoUsuario As String, ByRef objTransacao As Transacao)
        Dim OidSubCliente As String = String.Empty

        Dim command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        command.CommandText = Util.PrepararQuery(My.Resources.GetOidSubcliente)
        command.CommandType = CommandType.Text

        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))

        Dim dtSubcliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, command)

        If dtSubcliente.Rows.Count > 0 AndAlso dtSubcliente.Rows IsNot Nothing Then
            For Each dt In dtSubcliente.Rows
                OidSubCliente = Util.AtribuirValorObj(dt("OID_SUBCLIENTE"), GetType(String))
                Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
                comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaSubclientePuntoServicioV2)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, OidSubCliente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

                objTransacao.AdicionarItemTransacao(comando)
            Next
        End If

    End Sub

End Class