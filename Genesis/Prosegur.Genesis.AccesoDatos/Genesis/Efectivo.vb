Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Efectivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Efectivo

#Region "Consulta"

        ''' <summary>
        ''' Recupera os valores declarados por total
        ''' </summary>
        ''' <param name="IdentificadorElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorDivisa(IdentificadorElemento As String,
                                                    IdentificadorDivisa As String,
                                                    TipoElemento As Enumeradores.TipoElemento) As Clases.ValorDivisa

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.EfectivoRecuperarValorDivisa
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_PARCIAL = []OID_ELEMENTO ")

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.ValorDivisa = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValor = New Clases.ValorDivisa

                For Each dr In dt.Rows

                    With objValor
                        .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Declarado
                        .InformadoPor = Extenciones.RecuperarEnum(Of Enumeradores.TipoContado)(Util.AtribuirValorObj(dr("COD_TIPO_CONTADO"), GetType(String)))
                    End With

                Next

            End If

            Return objValor
        End Function


        ''' <summary>
        ''' Recupera os valores contado
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="IdDenominacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValoresPorDenominacion(IdElemento As String, IdDenominacion As String, TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.ValorDenominacion)
            Dim valoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoRecuperarValorContadoNivelRemesa)

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoRecuperarValorContadoNivelBulto)

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoRecuperarValorContadoNivelParcial)

            End Select

            cmd.CommandType = CommandType.Text

            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, IdDenominacion))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.ValorDenominacion = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                valoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                For Each dr In dt.Rows
                    objValor = New Clases.ValorDenominacion

                    Dim IdentificadorCalidad As String = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))

                    With objValor
                        .Calidad = If(Not String.IsNullOrEmpty(IdentificadorCalidad), Calidad.ObtenerCalidadePorIdentificador(IdentificadorCalidad), Nothing)
                        .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD"), GetType(String))
                        .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Contado
                        .UnidadMedida = UnidadMedida.RecuperarUnidadMedida(Nothing, Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)))
                    End With

                    valoresDenominacion.Add(objValor)
                Next

            End If

            Return valoresDenominacion
        End Function

#End Region

    End Class

End Namespace