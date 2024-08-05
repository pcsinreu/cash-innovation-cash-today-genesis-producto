Imports System.ComponentModel
Imports System.Drawing.Design

<Themeable(True)> _
<DefaultEvent("Click")> _
Public Class Boton
    Inherits UcBase

    Public Event Click As EventHandler

    ''' <summary>
    ''' Texto a ser exhibido en el botón.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Return btnBoton.Text
        End Get
        Set(value As String)
            btnBoton.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Imagen a ser exhibida en el botón.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ImageUrl() As String
        Get
            Return imgBoton.ImageUrl
        End Get
        Set(value As String)
            imgBoton.ImageUrl = value
        End Set
    End Property
    ''' <summary>
    ''' Propriedad OnClientClick do botão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BotonOnClientClick() As String
        Get
            Return btnBoton.OnClientClick
        End Get
        Set(value As String)
            btnBoton.OnClientClick = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedad OnClientClick da imagen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ImagenOnClientClick() As String
        Get
            Return imgBoton.OnClientClick
        End Get
        Set(value As String)
            imgBoton.OnClientClick = value
        End Set
    End Property

    ''' <summary>
    ''' Css a ser utilizado en el botón.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BotonCssClass() As String
        Get
            Return btnBoton.CssClass
        End Get
        Set(value As String)
            btnBoton.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Css a ser utilizado en la imagen.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ImagenCssClass() As String
        Get
            Return imgBoton.CssClass
        End Get
        Set(value As String)
            imgBoton.CssClass = value
        End Set
    End Property

    Public ReadOnly Property AtajoClientId() As String
        Get
            Return btnBoton.ClientID
        End Get
    End Property

    Private _TeclaAtalho As String
    Public Property TeclaAtalho() As String
        Get
            Return _TeclaAtalho
        End Get
        Set(value As String)
            _TeclaAtalho = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return pnlControles.Enabled
        End Get
        Set(value As Boolean)
            pnlControles.Enabled = value
        End Set
    End Property

    Protected Overridable Sub OnClick(sender As Object, e As EventArgs)
        RaiseEvent Click(sender, e)
    End Sub

    Private Sub btnBoton_Click(sender As Object, e As System.EventArgs) Handles btnBoton.Click
        If Enabled Then
            OnClick(sender, e)
        End If
    End Sub

    Private Sub imgBoton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBoton.Click
        If Enabled Then
            OnClick(sender, e)
        End If
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not String.IsNullOrEmpty(TeclaAtalho) AndAlso Not Page.IsPostBack Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ClientID, String.Format("shortcut.remove('{0}');shortcut.add('{0}',function(){{{1};}});", TeclaAtalho, Page.ClientScript.GetPostBackEventReference(btnBoton, Nothing)), True)
        End If
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(btnBoton)
    'End Sub

End Class