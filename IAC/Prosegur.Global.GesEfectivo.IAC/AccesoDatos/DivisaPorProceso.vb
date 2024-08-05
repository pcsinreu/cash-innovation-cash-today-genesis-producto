Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Proceso
Imports Prosegur.Genesis

''' <summary>
''' Classe divisa por proceso
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 16/03/2009 - Criado
''' </history>
Public Class DivisaPorProceso

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    Public Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Busca as divisas com oid encontrado no metodo getprocesodetails e retorna uma coleção de divisas.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaDivisaProceso(oidProceso As String) As GetProcesoDetail.DivisaProcesoColeccion
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaDivisaPorProceso.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaDivisas As New ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaDivisas = RetornaColDivisa(dt)

        Return objRetornaDivisas
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de divisas.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Private Shared Function RetornaColDivisa(dt As DataTable) As GetProcesoDetail.DivisaProcesoColeccion

        Dim objRetornaDivisa As New ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaDivisa.Add(PopularDivisaProceso(dr))
            Next
        End If

        Return objRetornaDivisa
    End Function

    ''' <summary>
    ''' Popula um objeto divisa proceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    Private Shared Function PopularDivisaProceso(dr As DataRow) As ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso

        Dim objDivisa As New ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso

        Util.AtribuirValorObjeto(objDivisa.Codigo, dr("COD_ISO_DIVISA"), GetType(String))

        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))

        Util.AtribuirValorObjeto(objDivisa.Orden, dr("NEC_NUM_ORDEN"), GetType(Integer))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaParcialMin, dr("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaParcialMax, dr("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaBultoMin, dr("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaBultolMax, dr("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaRemesaMin, dr("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objDivisa.ToleranciaRemesaMax, dr("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))
       
        Return objDivisa
    End Function

    ''' <summary>
    ''' Preencher Medio Pago Efectivos (Divisas) por Proceso - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularMedioPagoEfectivoProceso(Oid_Proceso As String) As GetProcesos.DivisaProcesoColeccion

        'Cria objetos DivisaProceso e DivisaProcesoColeccion
        Dim objDivProceso As GetProcesos.DivisaProceso = Nothing
        Dim objDivProcesoColeccion As GetProcesos.DivisaProcesoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoEfectivoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim MPEfectivo As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MPEfectivo IsNot Nothing AndAlso MPEfectivo.Rows.Count > 0 Then

            'Instancia objeto do tipo DivisaProcesoColeccion
            objDivProcesoColeccion = New GetProcesos.DivisaProcesoColeccion

            'Variável para comparação de CodIsoDivisa
            Dim CodIsoDivisa As String = String.Empty

            'Cria objeto do tipo Denominacion
            Dim objDenominacion As GetProcesos.Denominacion = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In MPEfectivo.Rows

                If CodIsoDivisa <> row("COD_ISO_DIVISA").ToString() Then

                    'Instancia objeto DivisaProceso
                    objDivProceso = New GetProcesos.DivisaProceso

                    'Preenche propriedades objeto DivisaProceso
                    With objDivProceso

                        Util.AtribuirValorObjeto(.CodigoISO, row("COD_ISO_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

                        'Cria nova instância para denominaciones
                        .Denominaciones = New GetProcesos.DenominacionColeccion

                    End With

                    'Adciona objeto divisa à coleção de divisas
                    objDivProcesoColeccion.Add(objDivProceso)

                End If

                'Instancia objeto Denominacion
                objDenominacion = New GetProcesos.Denominacion

                With objDenominacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.EsBillete, row("BOL_BILLETE"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.Valor, row("NUM_VALOR"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Peso, row("NUM_PESO"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                objDivProceso.Denominaciones.Add(objDenominacion)
                CodIsoDivisa = row("COD_ISO_DIVISA").ToString()

            Next

        End If

        Return objDivProcesoColeccion

    End Function

#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' Insere agrupacion por proceso.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <param name="oidDivisa"></param>
    ''' <param name="objDivisa"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub AltaDivisaPorProceso(oidProceso As String, _
                                           oidDivisa As String, _
                                           codDelegacion As String, _
                                           codUsuario As String, _
                                           objDivisa As ContractoServicio.Proceso.SetProceso.DivisaProceso, _
                                           ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaDivisaProceso.ToString())
            comando.CommandType = CommandType.Text

            MontaParameter("OID_DIVISA_PROCESO", Guid.NewGuid.ToString, comando)
            MontaParameter("OID_DIVISA", oidDivisa, comando)
            MontaParameter("OID_PROCESO", oidProceso, comando)
            MontaParameter("COD_DELEGACION", codDelegacion, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MIN", objDivisa.ToleranciaParcialMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MAX", objDivisa.ToleranciaParcialMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MIN", objDivisa.ToleranciaBultoMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MAX", objDivisa.ToleranciaBultolMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MIN", objDivisa.ToleranciaRemesaMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MAX", objDivisa.ToleranciaRemesaMax, comando)
            MontaParameter("COD_USUARIO", codUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_NUM_ORDEN", ProsegurDbType.Inteiro_Longo, objDivisa.Orden))


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
    ''' [anselmo.gois] 23/03/2009 - Criado
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
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Private Shared Sub MontaParameterDecimal(campo As String, objeto As Decimal, ByRef comando As IDbCommand)

        If objeto <> Nothing AndAlso objeto <> 0 Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Numero_Decimal, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Numero_Decimal, 0))

        End If


    End Sub

#End Region

#Region "[BORRAR]"

    Public Shared Sub BajaDivisaPorProceso(idProceso As String, ByRef objTransacion As Transacao)

        Using objCmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            objCmd.CommandText = Util.PrepararQuery(My.Resources.BajaDivisaPorProceso.ToString())
            objCmd.CommandType = CommandType.Text

            objCmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, idProceso))

            objTransacion.AdicionarItemTransacao(objCmd)

        End Using

    End Sub

#End Region

End Class
