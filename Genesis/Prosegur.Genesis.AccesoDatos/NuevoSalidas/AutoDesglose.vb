Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace NuevoSalidas

    Public Class AutoDesglose

        ''' <summary>
        ''' Função que recupera AutoDesglose para uma Divisa
        ''' </summary>
        ''' <param name="codIsoDivisa"></param>
        ''' <param name="codConfiguracion"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo]	05/05/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarAutoDesglosePorDivisa(codIsoDivisa As String, _
                                                              codConfiguracion As String) As List(Of Clases.AutoDesglose)

            Dim objRespuesta As New List(Of Clases.AutoDesglose)()

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_AutoDesglose_RecuperarAutoDesglosePorDivisa)
                'Adiciona parâmetro à consulta
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, codIsoDivisa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, codConfiguracion))
            End With

            'recupera o registro
            Dim dtAutoDesglose = DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dtAutoDesglose.Rows.Count > 0 Then
                'Percorre a lista de autodesglose
                For Each dr As DataRow In dtAutoDesglose.Rows
                    'Instancia um objeto do tipo AutoDesglose
                    Dim objAutoDesglose As New Clases.AutoDesglose
                    'Atualiza as propriedades
                    With objAutoDesglose
                        .CodIsoDivisa = Util.ValidarValor(dr("COD_ISO_DIVISA"))
                        .CodConfiguracion = Util.ValidarValor(dr("COD_CONFIGURACION"))
                        .CodDenominacion = Util.ValidarValor(dr("COD_DENOMINACION"))
                        .NumPorcentaje = Util.ValidarValor(dr("NUM_PORCENTAJE"))
                        .NumValorFacial = Util.ValidarValor(dr("NUM_VALOR_FACIAL"))
                    End With
                    'Adiciona um objeto AutoDesglose na coleção de AutoDesglose
                    objRespuesta.Add(objAutoDesglose)
                Next
            End If

            Return objRespuesta
        End Function

    End Class

End Namespace