Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe DivisaPorAgrupacion
''' </summary>
''' <remarks></remarks>
Public Class DivisaPorAgrupacion

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere um relacionamento entre divisa e agrupacion
    ''' </summary>
    ''' <param name="OidDivisa"></param>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Sub AltaDivisaPorAgrupacion(OidDivisa As String, _
                                              OidAgrupacion As String, _
                                              CodigoUsuario As String, _
                                              ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaDivisaPorAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' gerar guid
        Dim OidDivisaPorAgrupacion As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA_AGRUPACION", ProsegurDbType.Objeto_Id, OidDivisaPorAgrupacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, OidDivisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, OidAgrupacion))
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
    Public Shared Sub BorrarDivisasPorAgrupacion(OidAgrupacion As String, _
                                                 ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BorrarDivisasPorAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, OidAgrupacion))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Preenche divisa por agrupacion - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Agrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularDivisaAgrupacion(Oid_Agrupacion As String) As GetProcesos.DivisaColeccion

        'Cria objetos Divisa e DivisaColeccion
        Dim objDivisa As GetProcesos.Divisa = Nothing
        Dim objDivisaColeccion As GetProcesos.DivisaColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDivisaPorAgrupacion.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, Oid_Agrupacion))

        'Preenche DataTable com o resultado da consulta
        Dim Divisas As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Divisas IsNot Nothing AndAlso Divisas.Rows.Count > 0 Then

            'Cria uma nova instância para do objeto DivisaColeccion
            objDivisaColeccion = New GetProcesos.DivisaColeccion

            'Variável para comparação de CodIsoDivisa
            Dim CodIsoDivisa As String = String.Empty

            'Cria objeto do tipo Denominacion
            Dim objDenominacion As GetProcesos.Denominacion = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In Divisas.Rows

                If CodIsoDivisa <> row("COD_ISO_DIVISA").ToString() Then

                    'Instancia objeto Divisa
                    objDivisa = New GetProcesos.Divisa

                    'Preenche propriedades do objeto
                    With objDivisa

                        Util.AtribuirValorObjeto(.CodigoIso, row("COD_ISO_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                        'Cria nova instância DenominacionColeccion
                        .Denominaciones = New GetProcesos.DenominacionColeccion

                    End With

                    'Adciona objeto divisa à coleção de divisas
                    objDivisaColeccion.Add(objDivisa)

                End If

                'Instancia objeto Denominacion
                objDenominacion = New GetProcesos.Denominacion

                'Preenche propriedades do objeto
                With objDenominacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.EsBillete, row("BOL_BILLETE"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.Valor, row("NUM_VALOR"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Peso, row("NUM_PESO"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                'Adiciona Denominacion à coleção de Denominaciones
                objDivisa.Denominaciones.Add(objDenominacion)

                'Armazena CodIsoDivisa corrente à variável de comparação
                CodIsoDivisa = row("COD_ISO_DIVISA").ToString()

            Next

        End If

        Return objDivisaColeccion

    End Function

#End Region

End Class