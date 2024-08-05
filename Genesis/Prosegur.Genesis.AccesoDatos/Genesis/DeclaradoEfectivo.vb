Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    Public Class DeclaradoEfectivo

#Region "Consultar"

        ''' <summary>
        ''' Recupera os valores declarados por total
        ''' </summary>
        ''' <param name="IdentificadorElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorTotalDivisa(IdentificadorElemento As String,
                                                         IdentificadorDivisa As String, _
                                                         TipoElemento As Enumeradores.TipoElemento, _
                                                         nivelDetalle As Enumeradores.TipoNivelDetalhe) As Clases.Valor

            ' Recebe o filtro do documento
            'Dim filtroDocumento As String = If(Not String.IsNullOrWhiteSpace(IdentificadorDocumento), " AND DE.OID_DOCUMENTO = []OID_DOCUMENTO", String.Empty)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DeclaradoEfectivoRecuperarValorDivisa
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Bulto

                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Parcial

                    ' cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_PARCIAL = []OID_ELEMENTO ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_PARCIAL = []OID_ELEMENTO ")

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            'If Not String.IsNullOrWhiteSpace(filtroDocumento) Then
            '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))
            'End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdentificadorElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(nivelDetalle)))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValor As Clases.Valor = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (nivelDetalle = Enumeradores.TipoNivelDetalhe.Total) Then
                    objValor = New Clases.ValorEfectivo
                ElseIf (nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral) Then
                    objValor = New Clases.ValorDivisa
                End If

                With objValor
                    .Importe = Util.AtribuirValorObj(dt.Rows(0)("IMPORTE"), GetType(String))
                    .TipoValor = Enumeradores.TipoValor.Declarado
                End With

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

            ' Recebe o filtro do documento
            'Dim filtroDocumento As String = If(Not String.IsNullOrWhiteSpace(IdentificadorDocumento), " AND DE.OID_DOCUMENTO = []OID_DOCUMENTO", String.Empty)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DeclaradoEfectivoRecuperarValor
            cmd.CommandType = CommandType.Text

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_REMESA = []OID_ELEMENTO AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Bulto

                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_BULTO = []OID_ELEMENTO AND DE.OID_PARCIAL IS NULL ")

                Case Enumeradores.TipoElemento.Parcial

                    'cmd.CommandText = String.Format(cmd.CommandText, filtroDocumento & " AND DE.OID_PARCIAL = []OID_ELEMENTO ")
                    cmd.CommandText = String.Format(cmd.CommandText, " AND DE.OID_PARCIAL = []OID_ELEMENTO ")

            End Select

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            'If Not String.IsNullOrWhiteSpace(filtroDocumento) Then
            '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))
            'End If

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
                        .TipoValor = Enumeradores.TipoValor.Declarado
                        .UnidadMedida = UnidadMedida.RecuperarUnidadMedida(Nothing, Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)))
                    End With
                    valoresDenominacion.Add(objValor)
                Next

            End If

            Return valoresDenominacion
        End Function

#End Region

        ''' <summary>
        ''' Insere um declarado Efectivo.
        ''' </summary>
        ''' <param name="identificadorRemesa">Es una referencia a la entidad de REMESA</param>
        ''' <param name="identificadorBulto">Es una referencia a la entidad de BULTO</param>
        ''' <param name="identificadorParcial">Es una referencia a la entidad de PARCIAL</param>
        ''' <param name="identificadorDivisa">Es una referencia a la entidad de DIVISA</param>
        ''' <param name="identificadorDenominacion">Es una referencia a la entidad de DENOMINACION</param>
        ''' <param name="identificadorUnidadeMedida">Es una referencia a la entidad de UNIDAD MEDIDA</param>
        ''' <param name="tipoEfectivoTotal">Cuando el NIVEL de DETALLE sea T, será utilizado para indicar lo tipo de Total: B=Billete; M=Moneda</param>
        ''' <param name="importe">Indica el valor del Importe</param>
        ''' <param name="cantidad">Indica Cantidad informada</param>
        ''' <param name="nivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total</param>
        ''' <param name="ingresado">Indica si es un declarado INGRESADO: 1=Sí; 0=No</param>
        ''' <param name="usuario">Usuario de creación</param>
        ''' <remarks></remarks>
        Public Shared Sub DeclaradoEfectivoInserir(identificadorRemesa As String, _
                                                   identificadorBulto As String, _
                                                   identificadorParcial As String, _
                                                   identificadorDivisa As String, _
                                                   identificadorDenominacion As String, _
                                                   identificadorUnidadeMedida As String, _
                                                   tipoEfectivoTotal As String, _
                                                   importe As Decimal, _
                                                   cantidad As Long, _
                                                   nivelDetalhe As String, _
                                                   ingresado As Boolean, _
                                                   usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DeclaradoEfectivoInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DECLARADO_EFECTIVO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumentoElemento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, If(String.IsNullOrEmpty(identificadorDenominacion), DBNull.Value, identificadorDenominacion)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadeMedida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, tipoEfectivoTotal))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_INGRESADO", ProsegurDbType.Logico, ingresado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui os declarados efectivos do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeclaradoEfectivoExcluirBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DeclaradoEfectivoExcluirBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os declarados efectivo do malote.
        ' ''' </summary>
        ' ''' <param name="identificadorBulto">Identificador do Malote</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DeclaradoEfectivoActualizarBulto(identificadorBulto As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DeclaradoEfectivoActualizarBulto, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Excluir os declarados efectivo da remessa.
        ''' </summary>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeclaradoEfectivoExcluirRemesa(identificadorRemessa As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DeclaradoEfectivoExcluirRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os declarados efectivo da remessa.
        ' ''' </summary>
        ' ''' <param name="identificadorRemesa">Identificador da Remessa</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DeclaradoEfectivoActualizarRemesa(identificadorRemesa As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DeclaradoEfectivoActualizarRemesa, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

        ''' <summary>
        ''' Exclui os declarados efectivos do bulto.
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeclaradoEfectivoExcluirParcial(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DeclaradoEfectivoExcluirParcial)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ' ''' <summary>
        ' ''' Atualizar os declarados efectivo do parcial.
        ' ''' </summary>
        ' ''' <param name="identificadorParcial">Identificador do Parcial</param>
        ' ''' <param name="estadoDocumento">Estado do documento elemento</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub DeclaradoEfectivoActualizarParcial(identificadorParcial As String, estadoDocumento As Enumeradores.EstadoDocumento)

        '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DeclaradoEfectivoActualizarParcial, Util.RetornarEstadoDocumentoElemento(estadoDocumento)))
        '    cmd.CommandType = CommandType.Text

        '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

        '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        'End Sub

    End Class

End Namespace
