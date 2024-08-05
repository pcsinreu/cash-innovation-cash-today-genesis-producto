Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucFiltroSaldosCuenta
    Inherits UcBase

#Region "[PROPRIEDADES]"
    Public Property Divisas() As ObservableCollection(Of Clases.Divisa)
        Get
            If ViewState(ID & "_Divisas") Is Nothing Then
                ViewState(ID & "_Divisas") = LogicaNegocio.Genesis.Divisas.ObtenerDivisas()
            End If
            Return ViewState(ID & "_Divisas")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState(ID & "_Divisas") = value
        End Set
    End Property

    Public CodigoIsoDivisaDefecto As String

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()

        If Not IsPostBack Then
            PreencherDivisas(True)
            PreencherValores(True)
        Else
            PreencherDivisas()
            PreencherValores()
        End If


    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblDivisas.Text = Traduzir("043_TituloDivisas")
        lblTipoValores.Text = Traduzir("043_TituloValores")
        Me.chkConsiderarSomaZero.Text = Traduzir("043_ConsiderarSomaZero")
    End Sub

#End Region

#Region "[EVENTOS]"

#End Region

#Region "[METODOS]"

    Public Sub ActualizarFiltros(ByRef objFiltros As Clases.Transferencias.Filtro, ByRef objLegenda As String, Optional esBuscaDefecto As Boolean = False)
        objFiltros.Divisas = RecuperarValoresSelecionados(objLegenda, esBuscaDefecto)

        If Not esBuscaDefecto Then
            For Each item As ListItem In cklTipoValores.Items
                If item.Selected Then
                    objFiltros.TipoValores &= item.Value & ","
                    objLegenda &= " - " & item.Text
                End If
            Next
        Else
            objFiltros.TipoValores = ContractoServicio.Constantes.COD_TIPO_EFECTIVO
        End If

        objFiltros.EsConsiderarSomaZero = Me.chkConsiderarSomaZero.Checked
    End Sub

    ''' <summary>
    ''' Limpa os controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Limpiar()
        Divisas = Nothing
        PreencherDivisas(True)
    End Sub

    Private Function RecuperarValoresSelecionados(ByRef objLegenda As String, Optional esBuscaDefecto As Boolean = False) As ObservableCollection(Of Clases.Divisa)
        Dim objDivisas As New ObservableCollection(Of Clases.Divisa)

        For Each div As ListItem In cklDivisas.Items
            If div.Selected Then
                objDivisas.Add(New Clases.Divisa With {.Descripcion = div.Text, .Identificador = div.Value})
                objLegenda &= " - " & div.Text
            End If
        Next

        'If esBuscaDefecto Then
        '    If objDivisas.Count = 0 AndAlso Divisas.Find(Function(x) x.CodigoISO = CodigoIsoDivisaDefecto) IsNot Nothing Then
        '        objDivisas.Add(Divisas.FirstOrDefault(Function(x) x.CodigoISO = CodigoIsoDivisaDefecto))
        '        objLegenda &= " - " & Divisas.FirstOrDefault(Function(x) x.CodigoISO = CodigoIsoDivisaDefecto).Descripcion
        '        objLegenda &= " - " & Traduzir("027_trv_efectivo")
        '    End If
        'End If

        Return objDivisas
    End Function


#End Region

    Private Sub PreencherDivisas(Optional limpar As Boolean = False)

        If limpar Then
            cklDivisas.Items.Clear()
            For Each div In Divisas
                Dim objItem As New ListItem
                objItem.Text = If(div.Descripcion.Length > 15, div.Descripcion.Substring(0, 13) & "...", div.Descripcion)
                objItem.Value = div.Identificador
                objItem.Attributes.Add("style", "color:" & If(Not String.IsNullOrEmpty(Drawing.ColorTranslator.ToHtml(div.Color)), Drawing.ColorTranslator.ToHtml(div.Color), "#565656") & "; margin: 1px 10px;")

                'If Not String.IsNullOrEmpty(CodigoIsoDivisaDefecto) AndAlso CodigoIsoDivisaDefecto = div.CodigoISO Then
                '    objItem.Selected = True
                'End If
                cklDivisas.Items.Add(objItem)
            Next
        Else
            For Each item As ListItem In cklDivisas.Items
                Dim itemLocal = item
                Dim div As Clases.Divisa = Divisas.Find(Function(x) x.Identificador = itemLocal.Value)
                item.Attributes.Add("style", "color:" & If(Not String.IsNullOrEmpty(Drawing.ColorTranslator.ToHtml(div.Color)), Drawing.ColorTranslator.ToHtml(div.Color), "#565656") & "; margin: 1px 10px;")
            Next
        End If

    End Sub

    Private Sub PreencherValores(Optional limpar As Boolean = False)
        If limpar Then
            cklTipoValores.Items.Clear()
            Dim objItem As New ListItem
            cklTipoValores.Items.Add(New ListItem With {.Text = Traduzir("027_trv_efectivo"), .Value = ContractoServicio.Constantes.COD_TIPO_EFECTIVO, .Selected = True})
            cklTipoValores.Items.Add(New ListItem With {.Text = Traduzir("027_trv_tipo_medio_pago_cheque"), .Value = ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE})
            cklTipoValores.Items.Add(New ListItem With {.Text = Traduzir("027_trv_tipo_medio_pago_ticket"), .Value = ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TICKET})
            cklTipoValores.Items.Add(New ListItem With {.Text = Traduzir("027_trv_tipo_medio_pago_otros_valores"), .Value = ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES})
            cklTipoValores.Items.Add(New ListItem With {.Text = Traduzir("027_trv_tipo_medio_pago_tarjeta"), .Value = ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TARJETAS})
            For Each item As ListItem In cklTipoValores.Items
                item.Attributes.Add("style", "margin: 1px 10px;")
            Next
        End If

    End Sub

    Public Sub ExibirConsiderarSomaZero()
        Me.chkConsiderarSomaZero.Visible = True
    End Sub
End Class