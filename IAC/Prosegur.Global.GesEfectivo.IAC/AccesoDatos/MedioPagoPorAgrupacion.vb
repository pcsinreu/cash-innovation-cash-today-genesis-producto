Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe MedioPagoPorAgrupacion
''' </summary>
''' <remarks></remarks>
Public Class MedioPagoPorAgrupacion

#Region "[CONSULTAR]"

    Public Shared Function RetornarMediosPagoAgrupacion(oidAgrupacion) As GetProceso.MedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objMediosPago As New GetProceso.MedioPagoColeccion()

        'RETORNA MEDIOS PAGO POR AGRUPACION
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaMedioPagoAgrupacion.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, oidAgrupacion))

        Dim dtMedioPagoAgrupacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dtMedioPagoAgrupacion retornou algum registro
        If dtMedioPagoAgrupacion IsNot Nothing AndAlso dtMedioPagoAgrupacion.Rows.Count > 0 Then

            'Percorre o dtMedioPagoAgrupacion
            For Each drMedioPagoAgrupacion As DataRow In dtMedioPagoAgrupacion.Rows

                'Armazena o dr em um novo objMedioPagoAgrupacion
                objMediosPago.Add(PopulaMedioPagoGetProceso(drMedioPagoAgrupacion))

            Next

        End If

        Return objMediosPago

    End Function

    ''' <summary>
    ''' Popula o objMedioPago para operação GetProceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaMedioPagoGetProceso(dr As DataRow) As GetProceso.MedioPago

        Dim objMedioPago As New GetProceso.MedioPago

        If dr("COD_MEDIO_PAGO") IsNot DBNull.Value Then
            objMedioPago.Codigo = dr("COD_MEDIO_PAGO")
        End If

        If dr("DES_MEDIO_PAGO") IsNot DBNull.Value Then
            objMedioPago.Descripcion = dr("DES_MEDIO_PAGO")
        End If

        If dr("OBS_MEDIO_PAGO") IsNot DBNull.Value Then
            objMedioPago.Observaciones = dr("OBS_MEDIO_PAGO")
        End If

        If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value Then

            objMedioPago.CodigoTipo = dr("COD_TIPO_MEDIO_PAGO")
            objMedioPago.DescripcionTipo = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("cod_tipo_medio_pago"))

        End If

        If dr("COD_ISO_DIVISA") IsNot DBNull.Value AndAlso dr("DES_DIVISA") IsNot DBNull.Value Then
            objMedioPago.Divisa = New GetProceso.Divisa() With {.CodigoISO = dr("COD_ISO_DIVISA"), .Descripcion = dr("DES_DIVISA")}
        End If

        Return objMedioPago
    End Function

    ''' <summary>
    ''' Popula Medio Pago Agrupacion GetProcesos
    ''' </summary>
    ''' <param name="Oid_Agrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularMedioPagoAgrupacion(Oid_Agrupacion As String) As GetProcesos.MedioPagoColeccion

        'Cria objetos MedioPago e MedioPagoColeccion
        Dim objMedioPago As GetProcesos.MedioPago = Nothing
        Dim objMedioPagoColeccion As GetProcesos.MedioPagoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetMedioPagoPorAgrupacion.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, Oid_Agrupacion))

        'Preenche DataTable com o resultado da consulta
        Dim MedioPagos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MedioPagos IsNot Nothing AndAlso MedioPagos.Rows.Count > 0 Then

            'Cria uma nova instância do objeto MedioPagoColeccion
            objMedioPagoColeccion = New GetProcesos.MedioPagoColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In MedioPagos.Rows

                'Cria uma nova instância do objeto MedioPago
                objMedioPago = New GetProcesos.MedioPago

                'Preenche propriedades do objeto MedioPago
                With objMedioPago

                    Util.AtribuirValorObjeto(.Codigo, row("COD_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_MEDIO_PAGO"), GetType(String))

                    Util.AtribuirValorObjeto(.CodigoTipoMedioPago, row("COD_TIPO_MEDIO_PAGO"), GetType(String))

                    If Not String.IsNullOrEmpty(row("COD_TIPO_MEDIO_PAGO")) Then
                        .DescripcionTipoMedioPago = TipoMedioPago.ObterTipoMedioPagoDescripcion(row("COD_TIPO_MEDIO_PAGO"))
                    End If

                    Util.AtribuirValorObjeto(.CodigoIsoDivisa, row("COD_ISO_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionDivisa, row("DES_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                'Adciona objeto MedioPago à coleção de MedioPago
                objMedioPagoColeccion.Add(objMedioPago)

            Next

        End If

        'Retorna coleção de MedioPago preenchida
        Return objMedioPagoColeccion

    End Function


#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere um relacionamento entre medio pago e agrupacion
    ''' </summary>
    ''' <param name="OidMedioPago"></param>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Sub AltaMedioPagoPorAgrupacion(OidMedioPago As String, _
                                                 OidAgrupacion As String, _
                                                 CodigoUsuario As String, _
                                                 ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaMedioPagoPorAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' gerar guid
        Dim OidMedioPagoPorAgrupacion As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO_AGRUPACION", ProsegurDbType.Objeto_Id, OidMedioPagoPorAgrupacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, OidAgrupacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar para transacao
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Apagar todos os registros para um OidAgrupacion
    ''' </summary>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Sub BorrarMediosPagoPorAgrupacion(OidAgrupacion As String, _
                                                    ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BorrarMedioPagoPorAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, OidAgrupacion))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class