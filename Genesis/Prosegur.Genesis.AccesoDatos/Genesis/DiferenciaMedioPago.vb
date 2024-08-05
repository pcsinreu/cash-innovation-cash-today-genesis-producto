Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    Public Class DiferenciaMedioPago

#Region "Consultar"

        Public Shared Function RecuperarValorTipoMedioPago(IdentificadorElemento As String, _
                                                           IdentificadorDivisa As String, _
                                                           TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.ValorTipoMedioPago)

            ' Recebe o filtro do documento
            'Dim filtroDocumento As String = If(Not String.IsNullOrWhiteSpace(IdentificadorDocumento), " AND DMP.OID_DOCUMENTO = []OID_DOCUMENTO", String.Empty)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DiferenciaMedioPagoRecuperarValorDivisa
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_REMESA = []OID_ELEMENTO AND DMP.OID_BULTO IS NULL AND DMP.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_BULTO = []OID_ELEMENTO AND DMP.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_PARCIAL = []OID_ELEMENTO ")

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValores As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValores = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                For Each dr In dt.Rows

                    objValores.Add(New Clases.ValorTipoMedioPago With { _
                    .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(String)), _
                    .TipoValor = Enumeradores.TipoValor.Diferencia, _
                    .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))), _
                    .InformadoPor = Enumeradores.TipoContado.NoDefinido})

                Next

            End If

            Return objValores
        End Function

        ''' <summary>
        ''' Recupera os valores declarados
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="IdMedioPago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorMedioPago(IdElemento As String, IdMedioPago As String,
                                                       TipoElemento As Enumeradores.TipoElemento) As Clases.ValorMedioPago

            ' Recebe o filtro do documento
            'Dim filtroDocumento As String = If(Not String.IsNullOrWhiteSpace(IdentificadorDocumento), " AND DMP.OID_DOCUMENTO = []OID_DOCUMENTO", String.Empty)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DiferenciaMedioPagoRecuperarValor
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_REMESA = []OID_ELEMENTO AND DMP.OID_BULTO IS NULL AND DMP.OID_PARCIAL IS NULL ")
                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DMP.OID_REMESA = []OID_ELEMENTO AND DMP.OID_BULTO IS NULL AND DMP.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_BULTO = []OID_ELEMENTO AND DMP.OID_PARCIAL IS NULL ")
                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DMP.OID_BULTO = []OID_ELEMENTO AND DMP.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = String.Format(cmd.CommandText, " AND DMP.OID_PARCIAL = []OID_ELEMENTO ")
                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DMP.OID_PARCIAL = []OID_ELEMENTO ")

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            'If Not String.IsNullOrWhiteSpace(filtroDocumento) Then
            '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))
            'End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdMedioPago))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.ValorMedioPago = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValor = New Clases.ValorMedioPago

                With objValor
                    .Importe = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE"), GetType(Integer))
                    .InformadoPor = Enumeradores.TipoContado.NoDefinido
                    .Cantidad = Util.AtribuirValorObj(dt.Rows(0)("CANTIDAD"), GetType(Integer))
                    .TipoValor = Enumeradores.TipoValor.Diferencia
                End With

            End If

            Return objValor
        End Function

#End Region

        ''' <summary>
        ''' Insere uma diferencia efectivo.
        ''' </summary>
        ''' <param name="identificadorRemesa">Es una referencia a la entidad de REMESA</param>
        ''' <param name="identificadorBulto">Es una referencia a la entidad de BULTO</param>
        ''' <param name="identificadorParcial">Es una referencia a la entidad de PARCIAL</param>
        ''' <param name="identificadorDivisa">Es una referencia a la entidad de DIVISA</param>
        ''' <param name="identificadorMedioPago">Es una referencia a la entidad de MEDIO PAGO</param>
        ''' <param name="tipoMedioPago">Identificacion del Tipo de Medio Pago</param>
        ''' <param name="importe">Indica el valor del Importe</param>
        ''' <param name="cantidad">Indica Cantidad informada</param>
        ''' <param name="nivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total</param>
        ''' <param name="usuario">Usuario de creación</param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaMedioPagoInserir(identificadorRemesa As String, _
                                                     identificadorBulto As String, _
                                                     identificadorParcial As String, _
                                                     identificadorDivisa As String, _
                                                     identificadorMedioPago As String, _
                                                     tipoMedioPago As String, _
                                                     importe As Decimal, _
                                                     cantidad As Decimal, _
                                                     nivelDetalhe As String, _
                                                     usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaMedioPagoInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIFERENCIA_MEDIO_PAGO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, tipoMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Excluir os contados MedioPago da remesa.
        ''' </summary>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaMedioPagoExcluirRemesa(identificadorRemessa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaMedioPagoExcluirRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar as diferenças MedioPago da remesa.
        ' ''' </summary>
        ' ''' <param name="identificadorRemesa">Identificador da Remessa</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DiferenciaMedioPagoActualizarRemesa(identificadorRemesa As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DiferenciaMedioPagoActualizarRemesa, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os diferencia MedioPago do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaMedioPagoExcluirBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaMedioPagoExcluirBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar as diferenças MedioPago do malote.
        ' ''' </summary>
        ' ''' <param name="identificadorBulto">Identificador da Malote</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DiferenciaMedioPagoActualizarBulto(identificadorBulto As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DiferenciaMedioPagoActualizarBulto, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os diferencia MedioPago do parcial.
        ''' </summary>
        ''' <param name="identificadorParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub DiferenciaMedioPagoExcluirParcial(identificadorParcial As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DiferenciaMedioPagoExcluirParcial)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar as diferenças MedioPago do parcial.
        ' ''' </summary>
        ' ''' <param name="identificadorParcial">Identificador da Parcial</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DiferenciaMedioPagoActualizarParcial(identificadorParcial As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DiferenciaMedioPagoActualizarParcial, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

    End Class

End Namespace
