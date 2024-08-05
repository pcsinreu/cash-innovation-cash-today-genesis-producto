Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports System.Configuration
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Cliente

#Region "NOVOS METODOS"


    Public Shared Function GetClientes(Of T As ContractoServicio.Cliente.GetClientes.Cliente)(objPeticion As ContractoServicio.Cliente.GetClientes.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion, Optional completo As Boolean = False, Optional codigoExato As Boolean = False) As ContractoServicio.Cliente.GetClientes.ClienteColeccion(Of T)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetClientes)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPeticion.OidCliente) Then
            comando.CommandText &= " AND C.OID_CLIENTE = :OID_CLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidCliente))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodCliente) Then
            If codigoExato AndAlso completo Then
                comando.CommandText &= " AND C.COD_CLIENTE = :COD_CLIENTE"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodCliente))
            Else
                comando.CommandText &= " AND UPPER(C.COD_CLIENTE) LIKE :COD_CLIENTE "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.CodCliente.ToUpper() & "%"))
            End If
        End If

        If Not String.IsNullOrEmpty(objPeticion.DesCliente) Then
            comando.CommandText &= " AND UPPER(C.DES_CLIENTE) LIKE :DES_CLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DesCliente.ToUpper() & "%"))
        End If

        If objPeticion.BolVigente IsNot Nothing Then
            comando.CommandText &= " AND C.BOL_VIGENTE = :BOL_VIGENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.BolVigente))
        End If

        If objPeticion.CodTipoCliente IsNot Nothing Then
            comando.CommandText &= " AND T.COD_TIPO_CLIENTE = :COD_TIPO_CLIENTE"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodTipoCliente))
        End If

        If objPeticion.BolTotalizadorSaldo IsNot Nothing Then
            comando.CommandText &= " AND (:BOL_TOTALIZADOR_SALDO IS NULL OR C.BOL_TOTALIZADOR_SALDO = :BOL_TOTALIZADOR_SALDO)"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objPeticion.BolTotalizadorSaldo))
        End If

        If objPeticion.BolAbonaPorSaldo IsNot Nothing Then
            comando.CommandText &= " AND C.BOL_ABONA_POR_TOTAL = :BOL_ABONA_POR_TOTAL"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ABONA_POR_TOTAL", ProsegurDbType.Logico, objPeticion.BolAbonaPorSaldo))
        End If

        If objPeticion.TipoBanco IsNot Nothing Then
            If objPeticion.TipoBanco = ContractoServicio.Cliente.GetClientes.TipoBanco.Capital OrElse objPeticion.TipoBanco = ContractoServicio.Cliente.GetClientes.TipoBanco.Todos Then

                comando.CommandText &= " AND C.BOL_BANCO_CAPITAL = 1"
            End If

            If objPeticion.TipoBanco = ContractoServicio.Cliente.GetClientes.TipoBanco.Comision OrElse objPeticion.TipoBanco = ContractoServicio.Cliente.GetClientes.TipoBanco.Todos Then

                comando.CommandText &= " AND C.BOL_BANCO_COMISION = 1"
            End If


        End If

        comando.CommandText &= " ORDER BY C.COD_CLIENTE"

        Dim dtCliente As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        Return RetornaColecaoCliente(Of T)(dtCliente, completo)

    End Function

    Public Shared Function GetClientesDetalle(objPeticion As ContractoServicio.Cliente.GetClientesDetalle.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Cliente.GetClientesDetalle.ClienteColeccion(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)

        Dim col As New ContractoServicio.Cliente.GetClientesDetalle.ClienteColeccion(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)
        Dim ret = GetClientes(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)(objPeticion, parametrosRespuestaPaginacion, True, objPeticion.CodigoExacto)

        col.AddRange(ret.GetRange(0, ret.Count))
        ret = Nothing

        Return col

    End Function

    Public Shared Function GetClienteByCodigo(objPeticion As ContractoServicio.Cliente.GetClientesDetalle.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Cliente.GetClientesDetalle.ClienteColeccion(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)

        Dim col As New ContractoServicio.Cliente.GetClientesDetalle.ClienteColeccion(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)
        Dim ret = GetClientes(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)(objPeticion, parametrosRespuestaPaginacion, True, True)

        col.AddRange(ret.GetRange(0, ret.Count))
        ret = Nothing

        Return col

    End Function

    Public Shared Function AltaCliente(objCliente As ContractoServicio.Cliente.SetClientes.Cliente, codigoUsuario As String) As String

        Dim oidCliente As String

        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaClienteV2)
            comando.CommandType = CommandType.Text

            oidCliente = Guid.NewGuid.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.DesCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objCliente.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.oidTipoCliente))
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.CodTipoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objCliente.BolTotalizadorSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ABONA_POR_TOTAL", ProsegurDbType.Logico, objCliente.BolAbonaPorTotalSaldo))

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BANCO_CAPITAL", ProsegurDbType.Logico, objCliente.BolBancoCapital))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BANCO_COMISION", ProsegurDbType.Logico, objCliente.BolBancoComision))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PORCENT_COMISION", ProsegurDbType.Numero_Decimal, objCliente.PorcComisionCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_BANCARIO", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodBancario))

            'Saldo historico
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_SALDO_HISTORICO", ProsegurDbType.Logico, objCliente.BolGrabaSaldoHistorico))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FECHA_SALDO_HISTORICO", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodFechaSaldoHistorico))


            objTransacao.AdicionarItemTransacao(comando)

            objTransacao.RealizarTransacao()

            Return oidCliente

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("055_msg_erro_Insereregistro"))
        End Try

        Return Nothing

    End Function

    ' Buscar nivel saldo do cliente
    Public Shared Function BuscarClienteNivelSaldo(oidCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarClienteNivelSaldo.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Atauliza os dados do cliente
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Shared Sub ActualizarCliente(objCliente As ContractoServicio.Cliente.SetClientes.Cliente,
                                        codigoUsuario As String,
                                        ByRef objTransacao As Transacao, ReplicarVigenteSubclientePtoServicio As Boolean)

        Try

            'Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.OidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.DesCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objCliente.BolVigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.oidTipoCliente))
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.CodTipoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_TOTALIZADOR_SALDO", ProsegurDbType.Logico, objCliente.BolTotalizadorSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ABONA_POR_TOTAL", ProsegurDbType.Logico, objCliente.BolAbonaPorTotalSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BANCO_CAPITAL", ProsegurDbType.Logico, objCliente.BolBancoCapital))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BANCO_COMISION", ProsegurDbType.Logico, objCliente.BolBancoComision))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PORCENT_COMISION", ProsegurDbType.Numero_Decimal, objCliente.PorcComisionCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_BANCARIO", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodBancario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

             'Saldo historico
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_SALDO_HISTORICO", ProsegurDbType.Logico, objCliente.BolGrabaSaldoHistorico))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FECHA_SALDO_HISTORICO", ProsegurDbType.Identificador_Alfanumerico, objCliente.CodFechaSaldoHistorico))

            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarClienteV2)
            comando.CommandType = CommandType.Text

            objTransacao.AdicionarItemTransacao(comando)

            'Atualiza Conforme status se for diferente do atual
            If ReplicarVigenteSubclientePtoServicio Then
                SubCliente.AtualizaSubclientePuntoServicio(objCliente.OidCliente, objCliente.BolVigente, codigoUsuario, objTransacao)
            End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("055_msg_erro_Atualizaregistro"))
        End Try

    End Sub

    ''' <summary>
    ''' Efetuar a baixa do cliente, subcliente e punto de servicio
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <remarks></remarks>
    Public Shared Sub BajaCliente(objCliente As ContractoServicio.Cliente.SetClientes.Cliente, codigoUsuario As String)
        Try

            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objCliente.OidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            comando.CommandText = Util.PrepararQuery(My.Resources.BajaClienteV2)
            comando.CommandType = CommandType.Text
            objTransacao.AdicionarItemTransacao(comando)

            SubCliente.BajaSubCliente(objCliente.OidCliente, codigoUsuario, objTransacao)

            objTransacao.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("055_msg_erro_BajaRegistro"))
        End Try
    End Sub

    Private Shared Function RetornaColecaoCliente(Of T As ContractoServicio.Cliente.GetClientes.Cliente)(dtCliente As DataTable, completo As Boolean) As ContractoServicio.Cliente.GetClientes.ClienteColeccion(Of T)

        Dim objClienteColeccion As New ContractoServicio.Cliente.GetClientes.ClienteColeccion(Of T)

        If dtCliente.Rows.Count > 0 AndAlso dtCliente IsNot Nothing Then

            Dim objCliente As ContractoServicio.Cliente.GetClientes.Cliente = Nothing

            For Each dr In dtCliente.Rows

                If completo Then
                    objCliente = New ContractoServicio.Cliente.GetClientesDetalle.Cliente()
                Else
                    objCliente = New ContractoServicio.Cliente.GetClientes.Cliente()
                End If

                Util.AtribuirValorObjeto(objCliente.OidCliente, dr("OID_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.CodCliente, dr("COD_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.DesCliente, dr("DES_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCliente.OidTipoCliente, dr("OID_TIPO_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.CodTipoCliente, dr("COD_TIPO_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.DesTipoCliente, dr("DES_TIPO_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.BolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCliente.BolAbonaPorSaldoTotal, dr("BOL_ABONA_POR_TOTAL"), GetType(Boolean))

                Util.AtribuirValorObjeto(objCliente.CodBancario, dr("COD_BANCARIO"), GetType(String))

                If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(String))) Then
                    objCliente.PorcComisionCliente = Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(Decimal))
                End If
                Util.AtribuirValorObjeto(objCliente.BolBancoComision, dr("BOL_BANCO_COMISION"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCliente.BolBancoCapital, dr("BOL_BANCO_CAPITAL"), GetType(Boolean))

                'Saldo Historico
                Util.AtribuirValorObjeto(objCliente.BolGrabaSaldoHistorico, dr("BOL_SALDO_HISTORICO"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCliente.CodFechaSaldoHistorico, dr("COD_FECHA_SALDO_HISTORICO"), GetType(String))


                If completo Then
                    Dim objCompleto As ContractoServicio.Cliente.GetClientesDetalle.Cliente = objCliente
                    'Recupera os Codigos Ajenos
                    objCompleto.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_CLIENTE").ToString())

                    'Recupera as Direcciones
                    objCompleto.Direcciones = Direccion.RecuperaDireccionesBase(dr("OID_CLIENTE").ToString())
                End If

                objClienteColeccion.Add(objCliente)
            Next
        End If

        Return objClienteColeccion

    End Function

    Private Shared Function RetornaColecaoCliente(Of T)(dtCliente As DataTable) As T

        Select Case GetType(T)
            Case GetType(ContractoServicio.Cliente.GetClienteByCodigoAjeno.Cliente)

                If dtCliente.Rows.Count > 0 AndAlso dtCliente IsNot Nothing Then

                    For Each dr In dtCliente.Rows

                        Dim objCliente As New ContractoServicio.Cliente.GetClienteByCodigoAjeno.Cliente

                        Util.AtribuirValorObjeto(objCliente.oidCliente, dr("OID_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.codCliente, dr("COD_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.desCliente, dr("DES_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.oidTipoCliente, dr("OID_TIPO_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.codTipoCliente, dr("COD_TIPO_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.desTipoCliente, dr("DES_TIPO_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(objCliente.bolTotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(Boolean))

                        Return CType(objCliente, Object)
                    Next
                End If

        End Select

    End Function

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Busca o oid do Cliente
    ''' </summary>
    ''' <param name="Codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Shared Function BuscarOidCliente(Codigo As String) As String

        Dim OidCliente As String = String.Empty

        If Codigo IsNot Nothing AndAlso Codigo <> String.Empty Then

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.BuscarOidCliente.ToString())
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Codigo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                OidCliente = dt.Rows(0)("OID_CLIENTE").ToString
            End If

        End If
        Return OidCliente

    End Function

    Public Shared Function BuscarDesCliente(CodCliente As String) As String

        If Not String.IsNullOrEmpty(CodCliente) Then

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            With comando

                .CommandText = Util.PrepararQuery(My.Resources.BuscarDesCliente)
                .CommandType = CommandType.Text

                ' setar parametros
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))

            End With

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Return dt.Rows(0)("DES_CLIENTE").ToString

            End If

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica código do Cliente
    ''' </summary>
    ''' <param name="CodCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/03/2013 Criado
    ''' </history>
    Public Shared Function VerificarCodigoCliente(CodCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoCliente.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0


    End Function

    ''' <summary>
    ''' Verifica descrição do Cliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarDescripcionCliente(DesCliente As String) As Boolean

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionCliente.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CLIENTE", ProsegurDbType.Descricao_Longa, DesCliente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' retornar objeto
        Return Convert.ToInt32(dt.Rows(0).Item("QUANTIDADE")) > 0

    End Function

    ''' <summary>
    ''' Obtém combo de clientes
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Public Shared Function getComboClientes(objPeticion As ContractoServicio.Utilidad.GetComboClientes.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion

        ' criar objeto cliente coleccion
        Dim objClientes As New ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append(My.Resources.GetComboClientes.ToString)


        If objPeticion.TotalizadorSaldo Then
            query.Append(" AND C.BOL_TOTALIZADOR_SALDO = 1 ")
        End If

        If objPeticion.TipoBanco Then
            query.Append(" AND TC.COD_TIPO_CLIENTE = '1' ")
        End If

        If objPeticion.Vigente IsNot Nothing Then
            query.Append(" AND C.BOL_VIGENTE = []BOL_VIGENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))
        End If

        ' adicionar filtros caso eles sejam informados
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.Identificador, "OID_CLIENTE", comando, "AND", "C"))
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.Codigo, "COD_CLIENTE", comando, "AND", "C"))
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.Descripcion, "DES_CLIENTE", comando, "AND", "C"))

        ' adicionar ordenação
        query.Append(" ORDER BY C.COD_CLIENTE ")

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        'Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim dtQuery As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objClientes.Add(PopularGetComboClientes(dr))
            Next

        End If

        ' retornar coleção de clientes
        Return objClientes

    End Function

    ''' <summary>
    ''' Popula um objeto cliente com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Shared Function PopularGetComboClientes(dr As DataRow) As ContractoServicio.Utilidad.GetComboClientes.Cliente

        ' criar objeto formato
        Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente

        Util.AtribuirValorObjeto(objCliente.Codigo, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objCliente.Descripcion, dr("DES_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objCliente.OidCliente, dr("OID_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objCliente.CodigoAjenoCliente, dr("COD_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objCliente.DescripcionAjenoCliente, dr("DES_AJENO"), GetType(String))
        Util.AtribuirValorObjeto(objCliente.TotalizadorSaldo, dr("BOL_TOTALIZADOR_SALDO"), GetType(String))

        ' retorna objeto preenchido
        Return objCliente

    End Function

    ''' <summary>
    ''' Obtem os clientes, subclientes e puntoservicios a serem enviados para o Sistema de Saldos, 
    ''' com o objetivo de sincronizar os dados entre os sistemas
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 24/09/09 Criado
    ''' [vinicius.gama] 22/10/09 Alterado - Mudança na query para se retornar um numero limitado de registros
    ''' </history>
    Public Shared Function GetClientesSubClientesPuntoServicios(Optional UtilizarReglaAutomatas As Boolean = False) As Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.ClienteColeccion

        ' Declaro variaveis
        Dim Clientes As New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.ClienteColeccion
        Dim Cliente As Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.Cliente
        Dim SubCliente As Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.SubCliente
        Dim PuntoServicio As Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.PuntoServicio

        ' Executa comendo
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As String = My.Resources.GetClientesSubClientesPuntoServicios.ToString()

        If UtilizarReglaAutomatas Then
            query = String.Format(query, "2")
        Else
            query = String.Format(query, "0")
        End If

        comando.CommandText = Util.PrepararQuery(query)
        comando.CommandType = CommandType.Text

        ' Parametro que limita o numero de rows retornados
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "num_linhas", ProsegurDbType.Inteiro_Longo, Convert.ToInt32(ConfigurationManager.AppSettings("CantidadRegistros"))))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim idCliente As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' Le todos os clientes, sub-clientes e punto servicios e adiciona na colecao
            For Each rowCliente As DataRow In dt.Rows

                ' Le o cliente
                If rowCliente("cod_cliente") IsNot DBNull.Value Then

                    idCliente = rowCliente("oid_cliente")

                    ' Verifica se o cliente ja existe na colecao de clientes
                    Dim verificarCliente = From ECli In Clientes
                                           Where ECli.OidCliente = idCliente

                    ' Se nao existir, sera criado
                    If verificarCliente.Count = 0 Then

                        Cliente = New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.Cliente
                        Cliente.OidCliente = rowCliente("oid_cliente")
                        Cliente.CodCliente = rowCliente("cod_cliente")
                        Cliente.DescripcionCliente = rowCliente("des_cliente")
                        Cliente.Enviado = rowCliente("cliente_enviado")
                        Cliente.SubClientes = New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.SubClienteColeccion

                        Dim idSubCliente As String = String.Empty

                        ' Procuro todos os sub clientes do cliente
                        For Each rowSubCliente As DataRow In dt.Select("oid_cliente = '" & Cliente.OidCliente & "'")

                            ' Validação
                            If rowSubCliente("cod_subcliente") IsNot DBNull.Value Then

                                idSubCliente = rowSubCliente("oid_subcliente")

                                ' Verifica se o sub cliente ja existe para o cliente que esta sendo criado
                                Dim verificarSubCliente = From ESubCli In Cliente.SubClientes
                                                          Where ESubCli.OidSubCliente = idSubCliente

                                ' Se nao existir sera criado
                                If verificarSubCliente.Count = 0 Then

                                    SubCliente = New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.SubCliente
                                    SubCliente.OidSubCliente = rowSubCliente("oid_subcliente")
                                    SubCliente.CodSubCliente = rowSubCliente("cod_subcliente")
                                    SubCliente.DescripcionSubCliente = rowSubCliente("des_subcliente")
                                    SubCliente.Enviado = rowSubCliente("subcliente_enviado")
                                    SubCliente.PuntoServicios = New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.PuntoServicioColeccion

                                    Dim idPuntoServicio As String = String.Empty

                                    ' Procuro todos os punto servicios do sub cliente
                                    For Each rowPuntoServicio As DataRow In dt.Select("oid_subcliente = '" & SubCliente.OidSubCliente & "'")

                                        ' Validacao
                                        If rowPuntoServicio("cod_pto_servicio") IsNot DBNull.Value Then

                                            idPuntoServicio = rowPuntoServicio("oid_pto_servicio")

                                            ' Verifica se o punto servicio ja existe para o sub cliente que esta sendo criado
                                            Dim verificarPtoServicio = From EPto In SubCliente.PuntoServicios
                                                                       Where EPto.OidPuntoServicio = idPuntoServicio

                                            ' Se nao existir sera criado
                                            If verificarPtoServicio.Count = 0 Then

                                                PuntoServicio = New Integracion.ContractoServicio.GetClientesSubClientesPuntoServicios.PuntoServicio
                                                PuntoServicio.OidPuntoServicio = rowPuntoServicio("oid_pto_servicio")
                                                PuntoServicio.CodPuntoServicio = rowPuntoServicio("cod_pto_servicio")
                                                PuntoServicio.DescripcionPuntoServicio = rowPuntoServicio("des_pto_servicio")
                                                PuntoServicio.Enviado = rowPuntoServicio("pto_enviado")

                                                ' Adiciona na colecao
                                                SubCliente.PuntoServicios.Add(PuntoServicio)

                                            End If

                                        End If

                                    Next ' Punto Servicio

                                    ' Adiciona na colecao
                                    Cliente.SubClientes.Add(SubCliente)

                                End If

                            End If

                        Next ' Sub cliente

                        ' Adiciona na colecao
                        Clientes.Add(Cliente)

                    End If

                End If

            Next ' Cliente

        End If

        comando.Connection.Close()
        comando.Connection.Dispose()
        comando.Dispose()
        comando = Nothing

        Return Clientes

    End Function

    Public Shared Function GetClienteByCodigoAjeno(Of T)(identificadorAjeno As String, clienteCodigoAjeno As String) As T

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetClienteByCodigoAjeno)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(identificadorAjeno) Then
            comando.CommandText &= " AND CA.COD_IDENTIFICADOR = :COD_IDENTIFICADOR"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICADOR", ProsegurDbType.Identificador_Alfanumerico, identificadorAjeno))
        End If

        If Not String.IsNullOrEmpty(clienteCodigoAjeno) Then
            comando.CommandText &= " AND CA.COD_AJENO = :COD_AJENO"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, clienteCodigoAjeno))
        End If

        comando.CommandText &= " ORDER BY C.COD_CLIENTE"

        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Return RetornaColecaoCliente(Of T)(dtCliente)

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta o cliente e os seus subclientes.
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/02/2009 Created
    ''' </history>
    Public Shared Sub BajaCliente(objCliente As SetCliente.Cliente, codigoUsuario As String)

        Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaCliente.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacao.AdicionarItemTransacao(comando)

        SubCliente.BajaSubClienteNivelCliente(objCliente, codigoUsuario, objTransacao)

        objTransacao.RealizarTransacao()

    End Sub
#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o canal no DB.
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [vinicius.gama] 05/08/2010 Alterado - Associa proceso a um cliente novo caso exista valores default
    ''' </history>
    Public Shared Sub AltaCliente(objCliente As SetCliente.Cliente, codigoUsuario As String)

        Try

            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaCliente.ToString())
            comando.CommandType = CommandType.Text

            Dim oidCliente As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, oidCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objCliente.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CLIENTE", ProsegurDbType.Descricao_Longa, objCliente.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objCliente.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)

            If objCliente.SubClientes IsNot Nothing Then
                For Each sc As SetCliente.SubCliente In objCliente.SubClientes
                    SubCliente.AltaSubCliente(sc, codigoUsuario, oidCliente, objtransacion)
                Next
            End If

            AssociaProcesoClienteCasoExistaProcesoESubCanalDefecto(oidCliente, codigoUsuario, objtransacion)

            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKCliente"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Cliente do DB.
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Sub ActualizarCliente(objCliente As SetCliente.Cliente,
                                        codigoUsuario As String,
                                        ByRef objtransacion As Transacao)
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            Dim query As New StringBuilder
            query.Append("UPDATE gepr_tcliente SET ")

            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objCliente.Vigente, ProsegurDbType.Logico))

            If objCliente.Vigente Then
                query.Append(Util.AdicionarCampoQuery("des_cliente = []des_cliente,", "des_cliente", comando, objCliente.Descripcion, ProsegurDbType.Descricao_Longa))
            End If

            query.Append("cod_usuario = []cod_usuario, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

            query.Append("bol_enviado_saldos = []bol_enviado_saldos, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_enviado_saldos", ProsegurDbType.Logico, 0))

            query.Append("fyh_actualizacion = []fyh_actualizacion ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

            query.Append("WHERE cod_cliente = []cod_cliente ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_cliente", ProsegurDbType.Identificador_Alfanumerico, objCliente.Codigo))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            objtransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("013_msg_Erro_UKCliente"))
        End Try

    End Sub

    ''' <summary>
    ''' Atualiza o status do cliente. O campo exportado define se ele sera enviado para o saldos para atualizacao ou insercao
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 24/09/09 Criado
    ''' </history>
    Public Shared Sub ActualizarClienteExportado(CodCliente As String, Exportado As Boolean)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarClienteExportado.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_enviado_saldos", ProsegurDbType.Logico, Exportado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_cliente", ProsegurDbType.Identificador_Alfanumerico, CodCliente))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

#Region "[OUTROS MÉTODOS]"

    ''' <summary>
    ''' Cria um processo para o cliente, caso exista um proceso defaul e um subcanal defaul
    ''' </summary>
    ''' <param name="oidCliente">Oid do cliente criado</param>
    ''' <param name="codigoUsuario">Codigo do cliente que solicita</param>
    ''' <param name="objtransacion">Objeto da Transacao</param>
    ''' <history>
    ''' [vinicius.gama] Criado em 05/08/2010
    ''' </history>
    Private Shared Sub AssociaProcesoClienteCasoExistaProcesoESubCanalDefecto(oidCliente As String, codigoUsuario As String, objtransacion As Transacao)

        ' Cria um proceso para o cliente caso exista um proceso e um subcanal definidos como default
        Dim ProcesoDefecto As DataRow = Proceso.GetRowDatosProcesoDefecto()
        Dim SubCanalesDefecto As List(Of String) = SubCanal.GetDatosSubCanalDefecto()

        ' Caso exista o proceso e o subcanal
        If ProcesoDefecto IsNot Nothing AndAlso SubCanalesDefecto IsNot Nothing AndAlso SubCanalesDefecto.Count > 0 Then

            ' Cria um ProcesoPontoServico e guarda o oid
            Dim OidProcesoPorPtoServicio = ProcesoPorPServicio.AltaProcesoPorPServicio(ProcesoDefecto("oid_proceso"),
                                                                                       oidCliente,
                                                                                       String.Empty,
                                                                                       String.Empty,
                                                                                       String.Empty,
                                                                                       String.Empty,
                                                                                       String.Empty,
                                                                                       String.Empty,
                                                                                       ProcesoDefecto("cod_delegacion"),
                                                                                       codigoUsuario,
                                                                                       True,
                                                                                       objtransacion)

            For Each CodSubCanal In SubCanalesDefecto

                ' Cria um ProcesoCanal
                ProcesoPorSubCanal.AltaProcesoSubCanal(OidProcesoPorPtoServicio,
                                                       String.Empty,
                                                       codigoUsuario,
                                                       objtransacion,
                                                       CodSubCanal)

            Next

        End If

    End Sub

#End Region

End Class
