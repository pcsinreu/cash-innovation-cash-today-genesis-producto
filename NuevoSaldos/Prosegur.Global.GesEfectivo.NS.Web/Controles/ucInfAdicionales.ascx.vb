Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.IO
Imports System.Drawing
Imports Prosegur.Genesis.ContractoServicio.Entidades
Imports Prosegur.Genesis.Unificacion
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

''' <summary>
''' Informaciones Adicionales
''' </summary>
''' <remarks></remarks>
Public Class ucInfAdicionales
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Class Formatos
        Public Const Texto As String = "1"
        Public Const Entero As String = "2"
        Public Const [Decimal] As String = "3"
        Public Const Fecha As String = "4"
        Public Const FechaHora As String = "5"
        Public Const Booleano As String = "6"
        Public Const Lista As String = "7"
    End Class

    Public Property Modo As Genesis.Comon.Enumeradores.Modo
    Public Property Terminos As IEnumerable(Of TerminoIAC)
    Private _controlesTerminos As New List(Of ControleTermino)

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTitulo.Text = Traduzir("013_termino_titulo")
    End Sub

    Public Overrides Sub Focus()
        MyBase.Focus()

        If (Me._controlesTerminos IsNot Nothing AndAlso Me._controlesTerminos.Count > 0) Then
            Dim controle As WebControl = _controlesTerminos.First().Controle
            For Each c As ControleTermino In _controlesTerminos
                If (c.Controle.TabIndex < controle.TabIndex) Then
                    controle = c.Controle
                End If
            Next

            controle.Focus()
        End If
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    For Each controle In _controlesTerminos
    '        Aplicacao.Util.Utilidad.ConfigurarTabIndex(controle.Controle)
    '    Next
    'End Sub

    Public Overrides Function ValidarControl() As List(Of String)
        Dim resultadoValidacao As New List(Of String)()
        For Each controle In _controlesTerminos
            If (controle.Controle.Enabled AndAlso controle.Controle.Visible) Then
                resultadoValidacao.AddRange(controle.ValidarControl())
            End If
        Next
        Return resultadoValidacao
    End Function

    Protected Overrides Sub Render(writer As System.Web.UI.HtmlTextWriter)
        MyBase.Render(writer)

        For Each controle In _controlesTerminos
            Page.ClientScript.RegisterForEventValidation(controle.Controle.UniqueID)
        Next

    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Terminos IsNot Nothing Then
            ucInfAdicionales.Style.Item("display") = "block"
            CargarTerminos()
        Else
            ucInfAdicionales.Style.Item("display") = "none"
        End If
    End Sub

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If Not Page.ClientScript.IsClientScriptIncludeRegistered("autoNumeric.js") Then
            Page.ClientScript.RegisterClientScriptInclude(Me.GetType(), "autoNumeric.js", ResolveUrl("~/js/autoNumeric.js"))
        End If
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        For Each controle In _controlesTerminos
            ' 6. por ultimo se o controle possuir algum script este é registrado
            Dim script As String = controle.ObtenerScript()
            If Not String.IsNullOrEmpty(script) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "script_" & controle.Controle.ID, script, True)
            End If
        Next

    End Sub

#End Region

#Region "[METODOS]"

    Private Sub CargarTerminos()

        _controlesTerminos.Clear()
        phControles.Controls.Clear()

        For Each itemTermino In Terminos
            If Not (Me.Modo = Genesis.Comon.Enumeradores.Modo.Consulta) OrElse (itemTermino IsNot Nothing AndAlso Not String.IsNullOrEmpty(itemTermino.Valor)) Then
                ' 1. adicionado ao container

                Dim constructor As ControleTermino = Nothing

                ' 3. pelo código do termino escolhemos um controle
                If itemTermino.ValoresPosibles IsNot Nothing AndAlso itemTermino.ValoresPosibles.Count > 0 Then
                    constructor = New TerminoLista(Me, itemTermino)
                Else
                    Select Case itemTermino.Formato.Codigo
                        Case Formatos.Texto
                            constructor = New TerminoTexto(Me, itemTermino)
                        Case Formatos.Entero
                            constructor = New TerminoEntero(Me, itemTermino)
                        Case Formatos.[Decimal]
                            constructor = New TerminoDecimal(Me, itemTermino)
                        Case Formatos.Fecha
                            constructor = New TerminoFecha(Me, itemTermino)
                        Case Formatos.FechaHora
                            constructor = New TerminoFechaHora(Me, itemTermino)
                        Case Formatos.Booleano
                            constructor = New TerminoBooleano(Me, itemTermino)
                        Case Formatos.Lista
                            constructor = New TerminoLista(Me, itemTermino)
                        Case Else
                            Throw New NotImplementedException()

                    End Select

                End If

                constructor.ConfigurarLabel(itemTermino.MostrarDescripcionConCodigo)
                constructor.ConfigurarAceptarDigitacion(IIf(Modo = Genesis.Comon.Enumeradores.Modo.Alta OrElse Modo = Genesis.Comon.Enumeradores.Modo.Modificacion OrElse Modo = Genesis.Comon.Enumeradores.Modo.ModificarTerminos, True, False))

                If itemTermino.TieneValoresPosibles Then
                    constructor.ConfigurarValoresPosibles(itemTermino.ValoresPosibles)
                End If

                If itemTermino.Mascara IsNot Nothing Then
                    constructor.ConfigurarMascara(itemTermino.Mascara.ExpresionRegular)
                End If

                If Not String.IsNullOrEmpty(itemTermino.Valor) Then
                    constructor.ConfigurarValorInicial(itemTermino.Valor)
                End If

                constructor.ConfigurarModo(_Modo)

                phControles.Controls.Add(New LiteralControl("<div>"))
                phControles.Controls.Add(constructor.Label)
                phControles.Controls.Add(New LiteralControl("<br />"))
                phControles.Controls.Add(constructor.Controle)
                phControles.Controls.Add(New LiteralControl("</div>"))

                _controlesTerminos.Add(constructor)
            End If
        Next

        If phControles.Controls.Count = 0 Then
            ucInfAdicionales.Style.Item("display") = "none"
        End If

    End Sub

    Public Sub GuardarDatos()

        For Each controle In _controlesTerminos
            If (controle.Controle.Enabled AndAlso controle.Controle.Visible) Then
                controle.GuardarDatos()
            End If
        Next

        OnControleAtualizado(New ControleEventArgs With {.Controle = "InfAdicionales"})

    End Sub

#End Region

#Region "[TERMINOS]"


    Public MustInherit Class ControleTermino

        Protected _id As String
        Protected _termino As Termino
        Protected _controle As WebControl
        Protected _label As New Label
        Protected _uc As ucInfAdicionales

        Public ReadOnly Property Controle As WebControl
            Get
                Return _controle
            End Get
        End Property

        Public ReadOnly Property Termino As Termino
            Get
                Return _termino
            End Get
        End Property

        Public ReadOnly Property Label As Label
            Get
                Return _label
            End Get
        End Property

        Public ReadOnly Property ID As String
            Get
                Return _id
            End Get
        End Property

        Sub New(userControlTermino As ucInfAdicionales, ByRef termino As Termino)
            Me._termino = termino
            Me._id = userControlTermino.ID & "_" & termino.Codigo
            Me._uc = userControlTermino
        End Sub

        MustOverride Sub ConfigurarValorInicial(valorTermino As String)

        Overridable Sub ConfigurarLabel(mostrarCodigo As Boolean)
            ' Inclusão de regra de tamanho de campo, porque nomes acima de 20 caracteres estavam ficando escondidos no textbox.
            ' Sendo assim, quando o campo for maior que 20, ele será cortado, e será exibido completo pelo tooltip.
            If _termino.Descripcion.Length > 20 Then
                _label.Text = _termino.Descripcion.Substring(0, 18) & ". :"
                _label.ToolTip = _termino.Descripcion
            Else
                Label.Text = _termino.Descripcion & ":"
            End If
        End Sub

        Overridable Sub ConfigurarModo(modo As Genesis.Comon.Enumeradores.Modo)
            _controle.Enabled = (modo = Genesis.Comon.Enumeradores.Modo.Alta) OrElse (modo = Genesis.Comon.Enumeradores.Modo.Modificacion) OrElse (modo = Genesis.Comon.Enumeradores.Modo.ModificarTerminos)
        End Sub

        Overridable Sub ConfigurarAceptarDigitacion(aceptarDigitacion As Boolean)
        End Sub

        Overridable Sub ConfigurarValoresPosibles(valoresPosibles As ObservableCollection(Of TerminoValorPosible))
        End Sub

        Overridable Sub ConfigurarMascara(expressionRegular As String)
        End Sub

        Overridable Function ObtenerScript() As String
            Return String.Empty
        End Function

        MustOverride Sub GuardarDatos()

        MustOverride Function ValidarControl() As List(Of String)

    End Class

    Private Class TerminoTexto
        Inherits ControleTermino

        Protected _textbox As New TextBox() With {
            .ID = _id,
            .MaxLength = 255,
            .AutoPostBack = False,
            .Width = 150
        }

        Protected script As String = Nothing

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
            _textbox.MaxLength = termino.Longitud

            _controle = _textbox

        End Sub

        Public Overrides Sub ConfigurarAceptarDigitacion(aceptarDigitacion As Boolean)
            _textbox.ReadOnly = Not aceptarDigitacion

            Dim terminoIAC = TryCast(Termino, TerminoIAC)

            If terminoIAC IsNot Nothing AndAlso terminoIAC.EsProtegido AndAlso aceptarDigitacion Then
                _textbox.ReadOnly = True
                _textbox.Style.Add("background-color", "LightGray")
            End If

        End Sub

        Public Overrides Sub ConfigurarValorInicial(valorTermino As String)
            _textbox.Text = valorTermino
        End Sub

        Public Overrides Sub ConfigurarMascara(expressionRegular As String)

            script = String.Format("$(""#{0}"").blur(function(){{ if(!/{1}/.test( $(this).val() ) && $(this).val() ) {{ ExibirMensagem(""{2}"", ""{3}"", ""$('#{0}').focus()"", ""{4}""); }} }});",
                                    _controle.ClientID,
                                    expressionRegular,
                                    String.Format(Traduzir("013_termino_invalido"), _termino.Descripcion),
                                    String.Format(Traduzir("013_termino_invalido_titulo"), _termino.Descripcion),
                                    Traduzir("btnFechar")
                                    )

        End Sub

        Public Overrides Function ObtenerScript() As String
            Return script
        End Function

        Public Overrides Sub GuardarDatos()
            _termino.Valor = _textbox.Text
        End Sub

        Public Overrides Function ValidarControl() As List(Of String)
            Dim resultadoValidacao As New List(Of String)()
            If DirectCast(_termino, TerminoIAC).EsObligatorio AndAlso String.IsNullOrEmpty(_textbox.Text) Then
                resultadoValidacao.Add(String.Format(Traduzir("msg_campo_obrigatorio"), _termino.Descripcion))
            End If
            Return resultadoValidacao
        End Function
    End Class

    Private Class TerminoEntero
        Inherits TerminoTexto

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
        End Sub

        Public Overrides Function ObtenerScript() As String
            Return String.Format("$(""#{0}"").autoNumeric({{aSep : '', aDec: ',', mDec: '0'}});", _textbox.ClientID)
        End Function

    End Class

    Private Class TerminoDecimal
        Inherits TerminoTexto

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
        End Sub

        Public Overrides Function ObtenerScript() As String
            Return String.Format("$(""#{0}"").autoNumeric({{aSep : '', aDec: ',', mDec: '2'}});", _textbox.ClientID)
        End Function

    End Class

    Private Class TerminoBooleano
        Inherits ControleTermino

        Private _checkbox As New CheckBox() With {
            .ID = _id
        }

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
            _controle = _checkbox
        End Sub

        Public Overrides Sub ConfigurarValorInicial(valorTermino As String)
            _checkbox.Checked = Boolean.Parse(valorTermino)
        End Sub

        Public Overrides Sub GuardarDatos()
            _termino.Valor = _checkbox.Checked
        End Sub

        Public Overrides Function ValidarControl() As List(Of String)
            Dim resultadoValidacao As New List(Of String)()
            If DirectCast(_termino, TerminoIAC).EsObligatorio AndAlso Not _checkbox.Checked Then
                resultadoValidacao.Add(String.Format(Traduzir("msg_campo_obrigatorio"), _termino.Descripcion))
            End If
            Return resultadoValidacao
        End Function


        Public Overrides Sub ConfigurarAceptarDigitacion(aceptarDigitacion As Boolean)
            MyBase.ConfigurarAceptarDigitacion(aceptarDigitacion)

            Dim terminoIAC = TryCast(Termino, TerminoIAC)

            If terminoIAC IsNot Nothing AndAlso terminoIAC.EsProtegido AndAlso aceptarDigitacion Then
                _checkbox.Attributes.Add("disabled", "true")
                _checkbox.Style.Add("background-color", "LightGray")
            End If
        End Sub
    End Class

    Private Class TerminoLista
        Inherits ControleTermino

        Private _dropDownList As New DropDownList() With {
            .ID = _id
        }

        Sub New(usercontrolTermino As ucInfAdicionales, ByRef termino As Termino)
            MyBase.New(usercontrolTermino, termino)
            _controle = _dropDownList

        End Sub

        Public Overrides Sub ConfigurarValorInicial(valorTermino As String)
            For Each item As ListItem In _dropDownList.Items
                If item.Text.ToLower() = valorTermino.ToLower() OrElse item.Value.ToLower() = valorTermino.ToLower() Then
                    item.Selected = True
                Else
                    item.Selected = False
                End If
            Next
        End Sub

        Public Overrides Sub ConfigurarValoresPosibles(valoresPosibles As ObservableCollection(Of TerminoValorPosible))
            _dropDownList.Items.Clear()
            _dropDownList.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            For Each valorPossible In valoresPosibles
                Dim listItem As New ListItem(valorPossible.Descripcion, valorPossible.Codigo)
                listItem.Enabled = valorPossible.EstaActivo
                listItem.Selected = valorPossible.ValorDefecto
                _dropDownList.Items.Add(listItem)
            Next
        End Sub

        Public Overrides Sub GuardarDatos()
            _termino.Valor = _dropDownList.SelectedValue
        End Sub

        Public Overrides Function ValidarControl() As List(Of String)
            Dim resultadoValidacao As New List(Of String)()
            If DirectCast(_termino, TerminoIAC).EsObligatorio AndAlso String.IsNullOrEmpty(_dropDownList.SelectedValue) Then
                resultadoValidacao.Add(String.Format(Traduzir("msg_campo_obrigatorio"), _termino.Descripcion))
            End If
            Return resultadoValidacao
        End Function

        Public Overrides Sub ConfigurarAceptarDigitacion(aceptarDigitacion As Boolean)

            Dim terminoIAC = TryCast(Termino, TerminoIAC)
            If terminoIAC IsNot Nothing AndAlso terminoIAC.EsProtegido AndAlso aceptarDigitacion Then
                _dropDownList.Attributes.Add("disabled", "true")
                _dropDownList.Style.Add("background-color", "LightGray")
            End If

        End Sub
    End Class

    Private Class TerminoFecha
        Inherits TerminoTexto

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
            _controle.Width = 128
        End Sub

        Public Overrides Function ObtenerScript() As String

            If _controle.Enabled Then
                Dim scriptCalendario As String = String.Format("AbrirCalendario('{0}','false');", _textbox.ClientID)
                Return scriptCalendario
            Else
                Return Nothing
            End If

        End Function

    End Class

    Private Class TerminoFechaHora
        Inherits TerminoFecha

        Sub New(usercontrolTermino As ucInfAdicionales, termino As Termino)
            MyBase.New(usercontrolTermino, termino)
        End Sub

        Public Overrides Function ObtenerScript() As String

            If _controle.Enabled Then
                Dim scriptCalendario As String = String.Format("AbrirCalendario('{0}','true');", _textbox.ClientID)
                Return scriptCalendario
            Else
                Return Nothing
            End If

        End Function

    End Class

#End Region

End Class