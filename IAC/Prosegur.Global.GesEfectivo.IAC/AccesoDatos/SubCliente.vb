Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon

Public Class SubCliente


#Region "NOVOS METODOS"


    Public Shared Function GetSubClientes(Of T As ContractoServicio.SubCliente.GetSubClientes.SubCliente)(objPeticion As ContractoServicio.SubCliente.GetSubClientes.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion, Optional completo As Boolean = False) As ContractoServicio.SubCliente.GetSubClientes.SubClienteColeccion(Of T)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSubClientes)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPeticion.OidSubCliente) Then
            comando.CommandText &= " AND S.OID_SUBCLIENTE = :OID_SUBCLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidSubCliente))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodCliente) Then
            comando.CommandText &= " AND C.COD_CLIENTE = :COD_CLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodCliente))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodSubCliente) Then
            comando.CommandText &= " AND UPPER(S.COD_SUBCLIENTE) LIKE :COD_SUBCLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.CodSubCliente.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.DesSubCliente) Then
            comando.CommandText &= " AND UPPER(S.DES_SUBCLIENTE) LIKE :DES_SUBCLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DesSubCliente.ToUpper() & "%"))
        End If

        If objPeticion.BolVigente IsNot Nothing Then
            comando.CommandText &= " AND S.BOL_VIGENTE = :BOL_VIGENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.BolVigente))
        End If

        If objPeticion.OidTipoSubCliente IsNot Nothing Then
            comando.CommandText &= " AND S.OID_TIPO_SUBCLIENTE = :OID_TIPO_SUBCLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidTipoSubCliente))
        End If

        If objPeticion.BolTotalizadorSaldo IsNot Nothing Then
            comando.CommandText &= " AND S.BOL_TOTALIZADOR_SALDO = :BOL_TOTALIZADOR_SALDO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPeticion.BolTotalizadorSaldo))
        End If

        comando.CommandText &= " ORDER BY S.COD_SUBCLIENTE"


        Dim dtSubCliente As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        Return RetornaColecaoSubCliente(Of T)(dtSubCliente, completo)

    End Function

    Public Shared Function GetSubClientesDetalle(objPeticion As ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)

        Dim col As New ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
        Dim ret = GetSubClientes(Of ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)(objPeticion, parametrosRespuestaPaginacion, True)

        col.AddRange(ret.GetRange(0, ret.Count))
        ret = Nothing

        Return col

    End Function

    Public Shared Function AltaSubCliente(objSubCliente As ContractoServicio.SubCliente.SetSubClientes.SubCliente, codigoUsuario As String) As String

        Dim oidSubCliente As String

        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaSubClienteV2)
            comando.CommandType = CommandType.Text

            oidSubCliente = Guid.NewGuid.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objSubCliente.CodSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objSubCliente.DesSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objSubCliente.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidTipoSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objSubCliente.BolTotalizadorSaldo))

            objTransacao.AdicionarItemTransacao(comando)

            objTransacao.RealizarTransacao()

            Return oidSubCliente
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("057_msg_erro_Insereregistro"))
        End Try

        Return Nothing


    End Function

    ' Buscar nivel saldo subcliente
    Public Shared Function BuscarSubClienteNivelSaldo(oidSubCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarSubClienteNivelSaldo.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidSubCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    Public Shared Function BuscarPuntoServicioNivelSaldo(oidSubCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarSubClienteNivelSaldo.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidSubCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    Public Shared Sub ActualizarSubCliente(objSubCliente As ContractoServicio.SubCliente.SetSubClientes.SubCliente, codigoUsuario As String,
                                           ByRef objTransacao As Transacao, ReplicarVigenteSubclientePtoServicio As Boolean)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objSubCliente.CodSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objSubCliente.DesSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objSubCliente.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidTipoSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objSubCliente.BolTotalizadorSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarSubClienteV2)
            comando.CommandType = CommandType.Text

            objTransacao.AdicionarItemTransacao(comando)

            'Atualiza o punto de serviço conforme o status do subcliente
            If ReplicarVigenteSubclientePtoServicio Then
                AccesoDatos.PuntoServicio.AtualizaPuntoServicioConformeSubcliente(objSubCliente.OidSubCliente, objSubCliente.BolVigente, codigoUsuario, objTransacao)
            End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("057_msg_erro_Atualizaregistro"))
        End Try

    End Sub

    Public Shared Sub BajaSubCliente(objSubCliente As ContractoServicio.SubCliente.SetSubClientes.SubCliente, codigoUsuario As String, ByRef objTransacao As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, objSubCliente.OidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            comando.CommandText = Util.PrepararQuery(My.Resources.BajaSubClienteV2)
            comando.CommandType = CommandType.Text

            objTransacao.AdicionarItemTransacao(comando)

            AccesoDatos.PuntoServicio.AtualizaPuntoServicioConformeSubcliente(objSubCliente.OidSubCliente, objSubCliente.BolVigente, codigoUsuario, objTransacao)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("057_msg_erro_BajaRegistro"))
        End Try

    End Sub

    Private Shared Function RetornaColecaoSubCliente(Of T As ContractoServicio.SubCliente.GetSubClientes.SubCliente)(dtSubCliente As DataTable, completo As Boolean) As ContractoServicio.SubCliente.GetSubClientes.SubClienteColeccion(Of T)

        Dim objSubClienteColeccion As New ContractoServicio.SubCliente.GetSubClientes.SubClienteColeccion(Of T)

        If dtSubCliente.Rows.Count > 0 AndAlso dtSubCliente IsNot Nothing Then

            Dim objSubCliente As ContractoServicio.SubCliente.GetSubClientes.SubCliente = Nothing

            For Each dr In dtSubCliente.Rows

                If completo Then
                    objSubCliente = New ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente()
                Else
                    objSubCliente = New ContractoServicio.SubCliente.GetSubClientes.SubCliente()
                End If

                Util.AtribuirValorObjeto(objSubCliente.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.OidCliente, dr("OID_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(objSubCliente.OidTipoSubCliente, dr("OID_TIPO_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.CodTipoSubCliente, dr("COD_TIPO_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.DesTipoSubCliente, dr("DES_TIPO_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.BolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

                Util.AtribuirValorObjeto(objSubCliente.CodCliente, dr("COD_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubCliente.DesCliente, dr("DES_CLIENTE"), GetType(String))

                If completo Then
                    Dim objCompleto As ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente = objSubCliente
                    'Recupera os Codigos Ajenos
                    objCompleto.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_SUBCLIENTE").ToString())

                    'Recupera as Direcciones
                    objCompleto.Direcciones = Direccion.RecuperaDireccionesBase(dr("OID_SUBCLIENTE").ToString())
                End If

                objSubClienteColeccion.Add(objSubCliente)
            Next
        End If

        Return objSubClienteColeccion

    End Function

    Private Shared Function RetornaColecaoSubCliente(Of T)(dtRetorno As DataTable) As T

        Select Case GetType(T)
            Case GetType(ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.SubCliente)

                If dtRetorno.Rows.Count > 0 AndAlso dtRetorno IsNot Nothing Then

                    For Each dr In dtRetorno.Rows

                        Dim objSubCliente As New ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.SubCliente

                        Util.AtribuirValorObjeto(objSubCliente.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.OidCliente, dr("OID_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.CodSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.DesSubCliente, dr("DES_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.OidTipoSubCliente, dr("OID_TIPO_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.CodTipoSubCliente, dr("COD_TIPO_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.DesTipoSubCliente, dr("DES_TIPO_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.BolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

                        Util.AtribuirValorObjeto(objSubCliente.CodCliente, dr("COD_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objSubCliente.DesCliente, dr("DES_CLIENTE"), GetType(String))

                        Return CType(objSubCliente, Object)
                    Next
                End If

        End Select

    End Function

    ''' <summary>
    ''' Atualiza o status do subcliente conforme cliente
    ''' </summary>
    ''' <param name="oidCliente"></param>
    ''' <param name="BolVigente"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Shared Sub AtualizaSubclientePuntoServicio(oidCliente As String, _
                                                      BolVigente As Boolean, _
                                                      CodigoUsuario As String, _
                                                      ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaSubClienteConformeCliente)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

        'AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        objTransacao.AdicionarItemTransacao(comando)

        'Atualiza o  punto de servicio conforme status do cliente, subcliente 
        AccesoDatos.PuntoServicio.AtualizaPuntoServicioConformeSubclienteV2(oidCliente, BolVigente, CodigoUsuario, objTransacao)

    End Sub

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

    Public Shared Function BuscarSubCliente(Codigo As String) As ContractoServicio.SubCliente.GetSubClientes.SubCliente
        Dim retorno As ContractoServicio.SubCliente.GetSubClientes.SubCliente = Nothing
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarSubCliente.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            retorno = New ContractoServicio.SubCliente.GetSubClientes.SubCliente With {.OidCliente = dt.Rows(0)("OID_CLIENTE").ToString(),
                                                                                       .OidSubCliente = dt.Rows(0)("OID_SUBCLIENTE").ToString(),
                                                                                       .CodSubCliente = dt.Rows(0)("COD_SUBCLIENTE").ToString(),
                                                                                       .DesSubCliente = dt.Rows(0)("DES_SUBCLIENTE").ToString()}
        End If

        Return retorno
    End Function

    ''' <summary>
    ''' Busca o oid do SubCliente
    ''' </summary>
    ''' <param name="Codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Shared Function BuscarOidSubCliente(Codigo As String, oidCliente As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarOidSubCliente.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim OidSubCliente As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            OidSubCliente = dt.Rows(0)("OID_SUBCLIENTE").ToString
        End If

        Return OidSubCliente

    End Function

    ''' <summary>
    ''' Busca o oid do SubCliente
    ''' </summary>
    ''' <param name="oidCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/05/2010 Criado
    ''' </history>
    Public Shared Function BuscarTodosOidsSubClienteByOidCliente(oidCliente As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarTodosOidsSubClientesByOidCliente.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
    End Function

    ''' <summary>
    ''' Verifica código do SubCliente
    ''' </summary>  
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/03/2013 Criado
    ''' </history>
    Public Shared Function VerificarCodigoSubCliente(codCliente As String, codSubCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoSubCliente.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Verifica descrição do SubCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarDescripcionSubCliente(codCliente As String, desCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionSubCliente.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Descricao_Longa, desCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Obtem os dados dos subclientes
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Public Shared Function GetComboSubclientesByCliente(objPeticion As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion

        ' criar objeto cliente
        Dim objCliente As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        Dim filtro As New StringBuilder

        ' monta o filtro
        filtro.Append(Util.MontarClausulaIn(objPeticion.CodigosClientes, "COD_CLIENTE", comando, "AND"))

        If objPeticion.TotalizadorSaldo Then
            filtro.Append(String.Format(" AND SUBC.BOL_TOTALIZADOR_SALDO = {0} ", If(objPeticion.TotalizadorSaldo, "1", "0")))
        End If

        If objPeticion.vigente IsNot Nothing Then
            filtro.Append(String.Format(" AND SUBC.BOL_VIGENTE = {0} ", If(objPeticion.vigente, "1", "0")))
        End If

        ' chama o metodo para montar o filtro de código e descrição
        filtro.Append(MontaQuerySubClientesGetComboSubClientes(objPeticion.CodigoSubcliente, objPeticion.DescripcionSubcliente, comando))

        query.Append(String.Format(My.Resources.GetComboSubClientesByCliente, filtro))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Percorre o dt e retorna o cliente e seus subclientes.
        objCliente = PercorreDtSubCliente(dtCliente)

        Return objCliente

    End Function

    ''' <summary>
    ''' Monta query metodo getSubClientesByCliente
    ''' </summary>
    ''' <param name="cod"></param>
    ''' <param name="descripcion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function MontaQuerySubClientesGetComboSubClientes(cod As String, descripcion As String, ByRef comando As IDbCommand) As StringBuilder

        Dim filtro As New StringBuilder

        'monta a query de acordo com os parametros informados
        If cod IsNot Nothing AndAlso cod <> String.Empty Then

            filtro.Append(" AND UPPER(SUBC.COD_SUBCLIENTE) LIKE []COD_SUBCLIENTE ")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & cod.ToUpper & "%"))

        End If

        If descripcion IsNot Nothing AndAlso descripcion <> String.Empty Then

            filtro.Append(" AND UPPER(SUBC.DES_SUBCLIENTE) LIKE []DES_SUBCLIENTE ")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Descricao_Longa, "%" & descripcion.ToUpper & "%"))

        End If

        Return filtro

    End Function

    ''' <summary>
    ''' Popula um objeto subcliente.
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaGetSubClientesBycliente(dr As DataRow) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

        Dim objSubCliente As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

        Util.AtribuirValorObjeto(objSubCliente.Codigo, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objSubCliente.OidSubCliente, dr("OID_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objSubCliente.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objSubCliente.CodigoAjenoSubCliente, dr("COD_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objSubCliente.DescripcionAjenoSubCliente, dr("DES_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objSubCliente.TotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

        Return objSubCliente
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna o cliente com seus subclientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </sumary>
    Private Shared Function PercorreDtSubCliente(dtClientes As DataTable) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion

        Dim objClienteColeccion As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
        Dim objCliente As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.Cliente
        Dim auxCodCliente As String = String.Empty

        If dtClientes IsNot Nothing AndAlso dtClientes.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtClientes.Rows

                ' se for um cliente novo
                If Not auxCodCliente.Equals(dr("COD_CLIENTE")) Then

                    ' se não for o primeiro cliente adiciona à coleção
                    If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then objClienteColeccion.Add(objCliente)

                    ' atualiza o auxiliar para verificar o código de cliente
                    auxCodCliente = dr("COD_CLIENTE")

                    ' instancia um cliente novo
                    objCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.Cliente
                    objCliente.SubClientes = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion

                    ' copia os campos do cliente
                    Util.AtribuirValorObjeto(objCliente.Codigo, dr("COD_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(objCliente.Descripcion, dr("DES_CLIENTE"), GetType(String))

                End If

                ' adicionar para coleção
                objCliente.SubClientes.Add(PopulaGetSubClientesBycliente(dr))

            Next dr

            ' adiciona o último cliente à coleção
            objClienteColeccion.Add(objCliente)

        End If

        Return objClienteColeccion

    End Function

    ''' <summary>
    ''' Popula SubCliente GetComboPuntoServicio
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaSubClienteComboPuntoServicio(dr As DataRow) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente

        Dim objSubCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente

        Util.AtribuirValorObjeto(objSubCliente.Codigo, dr("COD_SUBCLIENTE"), GetType(String))

        Util.AtribuirValorObjeto(objSubCliente.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))

        Return objSubCliente

    End Function

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function SelectColSubCliente(objSubCliente As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubClienteColeccion, codigo As String) As Boolean

        Dim retorno = From c In objSubCliente Where c.Codigo = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Retorna uma coleção de subclientes
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function RetornaColSubClientes(dt As DataTable) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubClienteColeccion

        Dim objColSubCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubClienteColeccion
        Dim objSubCliente As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente
        Dim codigoSubCliente As String = String.Empty

        For Each dr As DataRow In dt.Rows

            codigoSubCliente = dr("COD_SUBCLIENTE")

            If SelectColSubCliente(objColSubCliente, codigoSubCliente) = False Then
                objSubCliente = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente
                objSubCliente = SubCliente.PopulaSubClienteComboPuntoServicio(dr)
                objColSubCliente.Add(objSubCliente)
            End If

        Next

        Return objColSubCliente

    End Function

    ''' <summary>
    ''' Verifica se o cliente enviado está ativo
    ''' </summary>
    ''' <param name="OidCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [pgoncalves] 04/07/2013 Criado
    ''' </history>
    Public Shared Function VerificaClienteAtivo(OidCliente As String) As Boolean

        Dim resultado As Boolean = False
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaClienteAtivo)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, OidCliente))

        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtCliente.Rows.Count > 0 AndAlso dtCliente.Rows IsNot Nothing Then
            For Each dr As DataRow In dtCliente.Rows
                resultado = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean))
            Next
        End If

        Return resultado
    End Function

    Public Shared Function GetSubclienteByCodigoAjeno(Of T)(identificadorAjeno As String, subclienteCodigoAjeno As String) As T

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSubclienteByCodigoAjeno)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(identificadorAjeno) Then
            comando.CommandText &= " AND CA.COD_IDENTIFICADOR = :COD_IDENTIFICADOR"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, identificadorAjeno))
        End If

        If Not String.IsNullOrEmpty(subclienteCodigoAjeno) Then
            comando.CommandText &= " AND CA.COD_AJENO = :COD_AJENO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, subclienteCodigoAjeno))
        End If

        comando.CommandText &= " ORDER BY S.COD_SUBCLIENTE"

        Dim dtRetorno As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Return RetornaColecaoSubCliente(Of T)(dtRetorno)

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta Todos os SubCanais do Canal Informado
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/02/2009 Created
    ''' </history>
    Public Shared Sub BajaSubClienteNivelCliente(objCliente As SetCliente.Cliente, _
                                                 CodigoUsuario As String, _
                                                 ByRef objTransacion As DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaSubCliente.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        objTransacion.AdicionarItemTransacao(comando)

        PuntoServicio.BajaPuntoServicioNivelCliente(objCliente.Codigo, CodigoUsuario, objTransacion)

    End Sub

    ''' <summary>
    ''' Deleta Todos os SubCanais do Canal Informado
    ''' </summary>
    ''' <param name="oidCliente"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Shared Sub BajaSubCliente(oidCliente As String, _
                                     CodigoUsuario As String, _
                                     ByRef objTransacion As DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaSubClienteNivelSubCliente.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        objTransacion.AdicionarItemTransacao(comando)

        PuntoServicio.BajaPuntoServicioNivelSubCliente(oidCliente, CodigoUsuario, objTransacion)

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o SubCanal no DB.
    ''' </summary>
    ''' <param name="objSuBCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/02/2009 Created
    ''' </history>
    Public Shared Sub AltaSubCliente(objSuBCliente As SetCliente.SubCliente, _
                                     CodigoUsuario As String, _
                                     oidCliente As String, _
                                     ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            ' Obtêm o comando
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaSubCliente.ToString())
            comando.CommandType = CommandType.Text

            Dim oidSubCliente As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidSubCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objSuBCliente.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCLIENTE", ProsegurDbType.Descricao_Longa, objSuBCliente.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objSuBCliente.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)

            If objSuBCliente.PuntoServicio IsNot Nothing Then
                For Each ps As SetCliente.PuntoServicio In objSuBCliente.PuntoServicio
                    PuntoServicio.AltaPuntoServicio(ps, CodigoUsuario, oidSubCliente, objtransacion)
                Next
            End If


        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKSubCliente"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Canal do DB.
    ''' </summary>
    ''' <param name="objSubCliente">Objeto com os dados do sub cliente</param>
    ''' <param name="codigoUsuario">Código do usuário</param>
    ''' <param name="oidCliente">Identificador do cliente</param>
    ''' <param name="oidSubCliente">Identificador do sub cliente</param>
    ''' <param name="objtransacion">Objeto com a transação</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [maoliveira]   12/05/2010 Alterado
    ''' </history>
    Public Shared Sub ActualizarSubCliente(objSubCliente As SetCliente.SubCliente, _
                                           codigoUsuario As String, _
                                           oidCliente As String, _
                                           oidSubCliente As String, _
                                           ByRef objtransacion As Transacao)
        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            Dim query As New StringBuilder
            query.Append("UPDATE gepr_tsubcliente SET ")

            ' adicionar campos
            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objSubCliente.Vigente, ProsegurDbType.Logico))

            If objSubCliente.Vigente Then
                query.Append(Util.AdicionarCampoQuery("des_subcliente = []des_subcliente,", "des_subcliente", comando, objSubCliente.Descripcion, ProsegurDbType.Descricao_Longa))
            End If

            query.Append("COD_USUARIO = []COD_USUARIO, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            query.Append("BOL_ENVIADO_SALDOS = []BOL_ENVIADO_SALDOS, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ENVIADO_SALDOS", ProsegurDbType.Logico, 0))

            query.Append("FYH_ACTUALIZACION = []FYH_ACTUALIZACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            ' adicionar clausula where
            query.Append("WHERE OID_SUBCLIENTE = []OID_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidSubCliente))

            ' adicionar condição
            query.Append("AND OID_CLIENTE = []OID_CLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidCliente))


            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKSubCliente"))
        End Try

    End Sub

    ''' <summary>
    ''' Atualiza o status do sub cliente. O campo exportado define se ele sera enviado para o saldos para atualizacao ou insercao
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 24/09/09 Criado
    ''' </history>
    Public Shared Sub ActualizarSubClienteExportado(CodCliente As String, CodSubCliente As String, Exportado As Boolean)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarSubClienteExportado.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ENVIADO_SALDOS", ProsegurDbType.Logico, Exportado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

End Class