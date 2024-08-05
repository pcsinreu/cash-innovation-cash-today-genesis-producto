Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace NuevoSalidas

    Public Class Denominacion

        Public Shared Function OrdenarDenominaciones_DividirEnBultos(strDivisasDenominaciones As String) As ObservableCollection(Of Clases.Divisa)
            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            'Se o parâmetro estiver vazio, seu valor será '' para que a query seja executada sem erros
            If String.IsNullOrEmpty(strDivisasDenominaciones) Then strDivisasDenominaciones = "' '"

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = String.Format(My.Resources.Salidas_Denominacion_DividirEnBultos, strDivisasDenominaciones)
            End With
            'Executa a query e retorna um datatable
            Dim dt As DataTable = DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In From row In dt.AsEnumerable() Select row.Field(Of String)("COD_ISO_DIVISA") Distinct
                    'Para cada divisa
                    Dim objDivisa As New Clases.Divisa
                    objDivisa.CodigoISO = codigoIso
                    objDivisa.Descripcion = dt.Select(String.Format("COD_ISO_DIVISA='{0}'", codigoIso)).FirstOrDefault()("DES_DIVISA")
                    objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA='{0}'", codigoIso))
                        'recupera cada denominacion da divisa
                        objDivisa.Denominaciones.Add(New Clases.Denominacion() With {.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)),
                                                                                     .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))})
                    Next

                    divisas.Add(objDivisa)
                Next

            End If

            Return divisas

        End Function

        Public Shared Function RecuperarDenominacionesPorTipoModulo(codTipoModulo As String) As DataTable
            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Denominacion_RecuperarPorTipoModulo)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_TIPO_MODULO", ProsegurDbType.Identificador_Alfanumerico, codTipoModulo))
            End With
            'executa o sql e retorna um valor
            Return DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Public Shared Function RecuperarValoresPorDivisa(codIsoDivisa As String, _
                                                     codDenominaciones As String) As DataTable
            'Se o parâmetro codDenominacion estiver vazio, seu valor será '' para que a query seja executada sem erros
            If String.IsNullOrEmpty(codDenominaciones) Then codDenominaciones = "' '"
            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Denominacion_RecuperarValoresDenominacionesPorDivisa)
                .CommandText = String.Format(.CommandText, codDenominaciones)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, codIsoDivisa))
            End With
            'Executa a query e retorna um datatable
            Return DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        ''' <summary>
        ''' Retorna las denominaciones de la divisa informada
        ''' </summary>
        ''' <param name="CodigoISODivisa"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo] 06/05/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDenominacionesPorDivisa(CodigoISODivisa As String) As ObservableCollection(Of Clases.Denominacion)

            Dim denominaciones As New ObservableCollection(Of Clases.Denominacion)()

            ' criar objeto
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Denominacion_RetornarDenominaciones)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoISODivisa))

            Dim dtDenominaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            Dim objDenominacion As Clases.Denominacion
            'Percorre o dt e retorna uma coleção de bultos de remesas.
            If dtDenominaciones IsNot Nothing AndAlso dtDenominaciones.Rows.Count > 0 Then
                For Each dr As DataRow In dtDenominaciones.Rows
                    objDenominacion = New Clases.Denominacion()
                    Util.AtribuirValorObjeto(objDenominacion.Identificador, dr("OID_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(objDenominacion.Codigo, dr("COD_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(objDenominacion.Descripcion, dr("DES_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(objDenominacion.Valor, dr("NUM_VALOR_FACIAL"), GetType(Double))
                    Util.AtribuirValorObjeto(objDenominacion.EsBillete, dr("BOL_BILLETE"), GetType(Boolean))
                    denominaciones.Add(objDenominacion)
                Next
            End If

            ' retornar objeto
            Return denominaciones

        End Function
    End Class

End Namespace