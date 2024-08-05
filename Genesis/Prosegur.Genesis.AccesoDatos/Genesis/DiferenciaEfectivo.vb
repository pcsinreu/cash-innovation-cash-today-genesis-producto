Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class DiferenciaEfectivo

#Region "Consultar"

        ''' <summary>
        ''' Recupera os valores declarados
        ''' </summary>
        ''' <param name="IdentificadorElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorTotalDivisa(IdentificadorElemento As String,
                                                         IdentificadorDivisa As String,
                                                         TipoElemento As Enumeradores.TipoElemento,
                                                         nivelDetalle As Enumeradores.TipoNivelDetalhe) As Clases.Valor

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DiferenciaEfectivoRecuperarValorDivisa
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_PARCIAL = []OID_ELEMENTO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(nivelDetalle)))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.Valor = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                If (nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral) Then
                    objValor = New Clases.ValorDivisa
                ElseIf (nivelDetalle = Enumeradores.TipoNivelDetalhe.Total) Then
                    objValor = New Clases.ValorEfectivo
                End If

                For Each dr In dt.Rows

                    With objValor
                        .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Diferencia
                    End With
                Next

            End If

            Return objValor
        End Function

        ''' <summary>
        ''' Recupera os valores declarados
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="IdDenominacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValoresPorDenominacion(IdElemento As String, IdDenominacion As String,
                                                             TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.ValorDenominacion)

            Dim valoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DiferenciaEfectivoRecuperar
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
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, IdDenominacion))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.ValorDenominacion = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                valoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                For Each dr In dt.Rows
                    objValor = New Clases.ValorDenominacion

                    With objValor
                        .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD"), GetType(String))
                        .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Diferencia
                        .UnidadMedida = UnidadMedida.RecuperarUnidadMedida(Nothing, Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)))
                    End With

                    valoresDenominacion.Add(objValor)
                Next

            End If

            Return valoresDenominacion
        End Function

#End Region

        ''' <summary>
        ''' Insere uma diferencia efectivo.
        ''' </summary>
        ''' <param name="identificadorRemesa">Es una referencia a la entidad de REMESA</param>
        ''' <param name="identificadorBulto">Es una referencia a la entidad de BULTO</param>
        ''' <param name="identificadorParcial">Es una referencia a la entidad de PARCIAL</param>
        ''' <param name="identificadorDivisa">Es una referencia a la entidad de DIVISA</param>
        ''' <param name="identificadorDenominacion">Es una referencia a la entidad de DENOMINACION</param>
        ''' <param name="identificadorUnidadMedida">Es una referencia a la entidad de UNIDAD MEDIDA</param>
        ''' <param name="tipoEfectivoTotal">Cuando el NIVEL de DETALLE sea T, será utilizado para indicar lo tipo de Total: B=Billete; M=Moneda</param>
        ''' <param name="importe">Indica el valor del Importe</param>
        ''' <param name="cantidad">Indica Cantidad informada</param>
        ''' <param name="nivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total</param>
        ''' <param name="usuario">Usuario de creación</param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaEfectivoInserir(identificadorRemesa As String, _
                                                    identificadorBulto As String, _
                                                    identificadorParcial As String, _
                                                    identificadorDivisa As String, _
                                                    identificadorDenominacion As String, _
                                                    identificadorUnidadMedida As String, _
                                                    tipoEfectivoTotal As String, _
                                                    importe As Decimal, _
                                                    cantidad As Decimal, _
                                                    nivelDetalhe As String, _
                                                    usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaEfectivoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIFERENCIA_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadMedida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, tipoEfectivoTotal))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Excluir os contados efectivo da remesa.
        ''' </summary>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaEfectivoExcluirRemesa(identificadorRemessa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaEfectivoExcluirRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui os diferencia efectivos do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaEfectivoExcluirBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaEfectivoExcluirBulto)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui os diferencia efectivos do parcial.
        ''' </summary>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaEfectivoExcluirParcial(identificadorParcial As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaEfectivoExcluirParcial)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

    End Class

End Namespace
