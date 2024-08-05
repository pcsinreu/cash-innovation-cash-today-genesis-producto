Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Proceso
Imports Prosegur.Genesis

''' <summary>
''' Classe medio pago por proceso
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 17/03/2009 - Criado
''' </history>
Public Class MedioPagoPorProceso

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    Public Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Busca as os medios pago referente ao oid do proceso retornado na consulta getprocesodetail
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaMedioPagoProceso(oidProceso As String, _
                                                 oidProcesoSubCanal As String) As GetProcesoDetail.MedioPagoProcesoColeccion
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoPorProceso.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaDivisas As New ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaDivisas = RetornaColMedioPago(dt, oidProcesoSubCanal)

        Return objRetornaDivisas
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de medios pago.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 - Criado
    ''' </history>
    Private Shared Function RetornaColMedioPago(dt As DataTable, _
                                             oidProcesoSubCanal As String) As GetProcesoDetail.MedioPagoProcesoColeccion

        Dim objRetornaMedioPago As New GetProcesoDetail.MedioPagoProcesoColeccion
        Dim objMedioPago As GetProcesoDetail.MedioPagoProceso
        Dim oidMedioPago As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                ' adicionar para objeto
                objMedioPago = PopularMedioPagoProceso(dr, oidMedioPago)

                objMedioPago.TerminosMedioPago = New GetProcesoDetail.TerminoMedioPagoColeccion

                'chama o metodo terminosmediopago que devolvera uma coleção de terminos.
                objMedioPago.TerminosMedioPago = AccesoDatos.TerminoMedioPago.BuscaTerminoMedioPago(oidMedioPago, oidProcesoSubCanal)

                objRetornaMedioPago.Add(objMedioPago)

            Next

        End If

        Return objRetornaMedioPago
    End Function

    ''' <summary>
    ''' Popula um objeto medio pago por proceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    Private Shared Function PopularMedioPagoProceso(dr As DataRow, _
                                                    ByRef oidMedioPago As String) As GetProcesoDetail.MedioPagoProceso

        Dim objMedioPago As New GetProcesoDetail.MedioPagoProceso

        Util.AtribuirValorObjeto(objMedioPago.CodigoTipoMedioPago, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))

        If Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
            objMedioPago.DescripcionTipoMedioPago = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
        End If

        Util.AtribuirValorObjeto(objMedioPago.CodigoIsoDivisa, dr("COD_ISO_DIVISA"), GetType(String))

        Util.AtribuirValorObjeto(objMedioPago.DescripcionDivisa, dr("DES_DIVISA"), GetType(String))

        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))

        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))

        Util.AtribuirValorObjeto(objMedioPago.EsMercancia, dr("BOL_MERCANCIA"), GetType(Boolean))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaParcialMin, dr("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaParcialMax, dr("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaBultoMin, dr("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaBultolMax, dr("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaRemesaMin, dr("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objMedioPago.ToleranciaRemesaMax, dr("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(oidMedioPago, dr("OID_MEDIO_PAGO"), GetType(String))

        Return objMedioPago
    End Function

    ''' <summary>
    ''' Preenche coleção de medio pago por proceso - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularMedioPagoProceso(Oid_Proceso As String) As GetProcesos.MedioPagoColeccion

        'Cria objetos MedioPago e MedioPagoColeccion
        Dim objMedioPago As GetProcesos.MedioPago = Nothing
        Dim objMedioPagoColeccion As GetProcesos.MedioPagoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

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
                    Util.AtribuirValorObjeto(.DescripcionTipoMedioPago, row("DES_TIPO_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoIsoDivisa, row("COD_ISO_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionDivisa, row("DES_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))
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

#Region "[INSERT]"

    ''' <summary>
    ''' Insere medio pago por proceso.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <param name="oidMedioPago"></param>
    ''' <param name="objMedioPago"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Public Shared Sub AltaMedioPagoPorProceso(oidProceso As String, _
                                              oidMedioPago As String, _
                                              codDelegacion As String, _
                                              codUsuario As String, _
                                              objMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProceso, ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaMedioPagoPorProceso.ToString())
            comando.CommandType = CommandType.Text

            MontaParameter("OID_MEDIO_PAGO_PROCESO", Guid.NewGuid.ToString, comando)
            MontaParameter("OID_PROCESO", oidProceso, comando)
            MontaParameter("OID_MEDIO_PAGO", oidMedioPago, comando)
            MontaParameter("COD_DELEGACION", codDelegacion, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MIN", objMedioPago.ToleranciaParcialMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MAX", objMedioPago.ToleranciaParcialMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MIN", objMedioPago.ToleranciaBultoMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MAX", objMedioPago.ToleranciaBultolMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MIN", objMedioPago.ToleranciaRemesaMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MAX", objMedioPago.ToleranciaRemesaMax, comando)
            MontaParameter("COD_USUARIO", codUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            '#If DEBUG Then
            '            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            '#Else
            objTransacion.AdicionarItemTransacao(comando)
            '#End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
        End Try

    End Sub

    ''' <summary>
    ''' Monda o parameter
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="objeto"></param>
    ''' <param name="comando"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Shared Sub MontaParameter(campo As String, objeto As String, ByRef comando As IDbCommand)

        If objeto IsNot Nothing AndAlso objeto <> String.Empty Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))

        End If


    End Sub

    ''' <summary>
    ''' Monda o parameter
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="objeto"></param>
    ''' <param name="comando"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Shared Sub MontaParameterDecimal(campo As String, objeto As Decimal, ByRef comando As IDbCommand)

        If objeto <> Nothing AndAlso objeto <> 0 Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Numero_Decimal, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Numero_Decimal, 0))

        End If


    End Sub
#End Region

End Class
