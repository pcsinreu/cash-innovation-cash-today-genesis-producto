Imports System.Web.UI
Imports Prosegur
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon
Imports System.Web.Script.Serialization

Public Class ucCheckBoxList
    Inherits UcBase

#Region "Variaveis"

#End Region

#Region "Propriedades"

#Region "    Entrada    "

    ''' <summary>
    ''' Define el titulo del controle
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property Titulo As String

    ''' <summary>
    ''' Define los valores que serán cargados en el componente
    ''' </summary>
    ''' <value>List(Of KeyValuePair(Of String, String)</value>
    ''' <returns>List(Of KeyValuePair(Of String, String)</returns>
    ''' <remarks></remarks>
    Public Property Valores As List(Of KeyValuePair(Of String, String))

    ''' <summary>
    ''' Define se la opcion de selecionar todos itens se quedará visible o no
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Property SelecionarTodos As Boolean

    ''' <summary>
    ''' Define la mesaje de exibición cuando no hay registro en la lista Valores
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property Mesaje As String

#End Region

#Region "    Salida    "

    Private _ItensSelecionados As List(Of String)
    ''' <summary>
    ''' Amalcena los datos selecionados en el controle
    ''' </summary>
    ''' <value>List(Of String)</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItensSelecionados As List(Of String)
        Get
            If _ItensSelecionados Is Nothing Then
                _ItensSelecionados = New List(Of String)
            End If
            Return _ItensSelecionados
        End Get
        Set(value As List(Of String))
            _ItensSelecionados = value
        End Set
    End Property

#End Region

#End Region

#Region "ViewStates"

    ''' <summary>
    ''' Almacena la lista de tooltips que fueran cargados en el primer ejecución de la página
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ToolTips As List(Of KeyValuePair(Of String, String))
        Get
            If ViewState("tooltip") Is Nothing Then
                ViewState("tooltip") = New List(Of KeyValuePair(Of String, String))
            End If
            Return ViewState("tooltip")
        End Get
        Set(value As List(Of KeyValuePair(Of String, String)))
            ViewState("tooltip") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.divCheckBoxList.ID = "divCheckBoxList_" & Me.ID

            If Not Me.IsPostBack Then
                Me.CargarDatos()

            Else
                Me.CargarToolTips()

            End If

        Catch ex As Genesis.Excepcion.NegocioExcepcion
            MyBase.NotificarErro(ex)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar el controle con la definición de los parámetros de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDatos()

        Me.lbltituloCheckBoxList.Text = Me.Titulo
        Me.chkMarcarTodos.Visible = Me.SelecionarTodos
        Me.lblchkMarcarTodos.Visible = Me.SelecionarTodos

        Dim Mesajes As New StringBuilder
        If Me.Validar(Mesajes) Then

            Dim jss As New JavaScriptSerializer
            Dim parametros As New ParametrosJson(Me.ID)
            Me.hdnJson.Value = jss.Serialize(parametros)

            Dim ListaItems As List(Of ListItem) = ConverterValores()
            If ListaItems IsNot Nothing Then

                For Each item In ListaItems
                    Me.chkGenerico.Items.Add(item)

                Next item

                Me.chkMarcarTodos.Visible = Me.SelecionarTodos
                Me.chkGenerico.Visible = True
                Me.lblMesaje.Text = String.Empty

            Else
                Me.chkGenerico.Items.Clear()
                Me.chkMarcarTodos.Visible = False
                Me.chkGenerico.Visible = False
                Me.lblMesaje.Text = Me.Mesaje

            End If

        Else
            Throw New Genesis.Excepcion.NegocioExcepcion(Mesajes.ToString)

        End If

    End Sub

    ''' <summary>
    ''' Repor os tooltips ao controle, porque são perdidos durante o postback
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarToolTips()

        For Each item As ListItem In Me.chkGenerico.Items
            Dim itemLocal = item
            If ToolTips.Exists(Function(x) x.Key = itemLocal.Value) Then
                item.Attributes.Add("title", ToolTips.Where(Function(x) x.Key = itemLocal.Value).FirstOrDefault.Value)
            End If

        Next item

    End Sub

    ''' <summary>
    ''' Validar el unico parámetro obligatorio del controle
    ''' </summary>
    ''' <param name="Mesajes">Lista de los errores</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Validar(ByRef Mesajes As StringBuilder) As Boolean

        If String.IsNullOrEmpty(Me.Titulo) Then
            Mesajes.AppendLine(String.Format(Traduzir("014_parametro_obrigatorio"), "Titulo"))
        End If

        If Mesajes.Length > 0 Then
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' Almacenar los datos selecionados en el controle
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()

        If Me.chkGenerico IsNot Nothing AndAlso Me.chkGenerico.Items.Count > 0 Then
            Me.ItensSelecionados = New List(Of String)
            For Each item As WebControls.ListItem In Me.chkGenerico.Items
                If item.Selected Then
                    Me.ItensSelecionados.Add(item.Value)

                End If

            Next item
        End If

    End Sub

    ''' <summary>
    ''' Converter los valores para una lista de clase Conversion {Identificador y Descripcion}
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConverterValores() As List(Of ListItem)

        If Me.Valores IsNot Nothing AndAlso Me.Valores.Count > 0 Then
            Dim ListaItems As New List(Of ListItem)
            For Each valor In Valores
                Dim Item As New ListItem
                With Item
                    Dim valorNovo As String = valor.Value
                    If valor.Value.Length > 30 Then
                        valorNovo = valor.Value.Remove(30) & "..."
                        .Attributes.Add("title", valor.Value)
                        ToolTips.Add(New KeyValuePair(Of String, String)(valor.Key, valor.Value))

                    End If
                    .Value = valor.Key
                    .Text = valorNovo

                End With

                ListaItems.Add(Item)

            Next valor
            Return ListaItems
        End If
        Return Nothing
    End Function

#End Region

#Region "Json"

    Private Class ParametrosJson

#Region "    Propriedades    "

        Public Property IDComponente As String
        Public Property TituloLabelMarcado As String
        Public Property TituloLabelDesmarcado As String

#End Region

#Region "    Constructores    "

        Sub New(Titulo As String)
            Me.IDComponente = Titulo
            TituloLabelMarcado = "Marcar todos"
            TituloLabelDesmarcado = "Desmarcar todos"

        End Sub

#End Region

    End Class

#End Region

End Class