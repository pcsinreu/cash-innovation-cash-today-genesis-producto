Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Proceso
Imports Prosegur.Genesis

Public Class AgrupacionPorProceso

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
    ''' Busca as divisas com oid encontrado no metodo getprocesodetails e retona as agrupações.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaAgrupacionProceso(oidProceso As String) As GetProcesoDetail.AgrupacionProcesoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaAgrupacionProceso.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaAgrupacion As New ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProcesoColeccion

        'Percorre o dt e retorna uma coleção de agrupaciones.
        objRetornaAgrupacion = RetornaColAgrupacion(dt)

        Return objRetornaAgrupacion
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de agrupações.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Private Shared Function RetornaColAgrupacion(dt As DataTable) As GetProcesoDetail.AgrupacionProcesoColeccion

        Dim objRetornaAgrupacion As New ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProcesoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaAgrupacion.Add(PopularAgrupacionProceso(dr))
            Next

        End If

        Return objRetornaAgrupacion
    End Function

    ''' <summary>
    ''' Popula um objeto agruapcion proceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    Private Shared Function PopularAgrupacionProceso(dr As DataRow) As ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso

        Dim objAgrupacion As New ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso

        Util.AtribuirValorObjeto(objAgrupacion.Codigo, dr("COD_AGRUPACION"), GetType(String))

        Util.AtribuirValorObjeto(objAgrupacion.Descripcion, dr("DES_AGRUPACION"), GetType(String))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaParcialMin, dr("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaParcialMax, dr("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaBultoMin, dr("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaBultoMax, dr("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaRemesaMin, dr("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))

        Util.AtribuirValorObjeto(objAgrupacion.ToleranciaRemesaMax, dr("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

        Return objAgrupacion
    End Function

    ''' <summary>
    ''' Preenche Agrupaciones por Proceso - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularAgrupacionProceso(Oid_Proceso As String) As GetProcesos.AgrupacionColeccion

        'Cria objetos Agrupacion e AgrupacionColeccion
        Dim objAgrupacion As GetProcesos.Agrupacion = Nothing
        Dim objAgrupColeccion As GetProcesos.AgrupacionColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaAgrupacionesPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim Agrupaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Agrupaciones IsNot Nothing AndAlso Agrupaciones.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objAgrupColeccion = New GetProcesos.AgrupacionColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In Agrupaciones.Rows

                'Instancia objeto Agrupacion
                objAgrupacion = New GetProcesos.Agrupacion

                'Preenche Agrupacion
                With objAgrupacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                    'Preenche coleção de divisas por agrupacion
                    .Divisas = AccesoDatos.DivisaPorAgrupacion.PopularDivisaAgrupacion(row("OID_AGRUPACION").ToString())

                    'Preenche coleção de medio pago por agrupacion
                    .MedioPago = AccesoDatos.MedioPagoPorAgrupacion.PopularMedioPagoAgrupacion(row("OID_AGRUPACION").ToString())

                End With

                'Adciona objeto agrupacion à coleção de agrupaciones
                objAgrupColeccion.Add(objAgrupacion)

            Next

        End If

        'Retorna objeto preenchido
        Return objAgrupColeccion

    End Function

#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' Insere agrupacion por proceso.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <param name="oidAgrupacion"></param>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Public Shared Sub AltaAgrupacionPorProceso(oidProceso As String, _
                                              oidAgrupacion As String, _
                                              codDelegacion As String, _
                                              codUsuario As String, _
                                              objAgrupacion As ContractoServicio.Proceso.SetProceso.AgrupacionProceso, _
                                              ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaAgrupacionPorProceso.ToString())
            comando.CommandType = CommandType.Text

            MontaParameter("OID_AGRUPACION_PROCESO", Guid.NewGuid.ToString, comando)
            MontaParameter("OID_AGRUPACION", oidAgrupacion, comando)
            MontaParameter("OID_PROCESO", oidProceso, comando)
            MontaParameter("COD_DELEGACION", codDelegacion, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MIN", objAgrupacion.ToleranciaParcialMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_PARCIAL_MAX", objAgrupacion.TolerenciaParcialMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MIN", objAgrupacion.ToleranciaBultoMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_BULTO_MAX", objAgrupacion.ToleranciaBultoMax, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MIN", objAgrupacion.ToleranciaRemesaMin, comando)
            MontaParameterDecimal("NUM_TOLERANCIA_REMESA_MAX", objAgrupacion.ToleranciaRemesaMax, comando)
            MontaParameter("COD_USUARIO", codUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            objTransacion.AdicionarItemTransacao(comando)

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
