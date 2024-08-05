Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon

Public Class ucRadioButtonList
    Inherits UcBase

#Region "Propriedades"

    ''' <summary>
    ''' Define el titulo del controle
    ''' </summary>
    ''' <value>String</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Titulo As String

    ''' <summary>
    ''' Define las opciones que serán cargadas en el RadioButtonList (StringKey, StringValue)
    ''' </summary>
    ''' <value>List(Of KeyValuePair(Of String, String))</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Opciones As List(Of KeyValuePair(Of String, String))
    ''' <summary>
    ''' Almacena el valor selecionado en el controle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemSelecionado As String

    Public Property AutoPostBack As Boolean

    Public Delegate Sub RadioButtonList_Handler()

    Public Event SelectedIndexChanged As RadioButtonList_Handler

#End Region

#Region "Eventos"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Me.CargarDatos()
                Me.rblGenerico.AutoPostBack = AutoPostBack
            End If

        Catch ex As Genesis.Excepcion.NegocioExcepcion
            MyBase.NotificarErro(ex)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    Protected Sub rblGenerico_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblGenerico.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged()
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarDatos()

        Dim Mesajes As New StringBuilder
        If Validar(Mesajes) Then

            Me.lbltituloRadioButtonList.Text = Me.Titulo

            If Opciones IsNot Nothing AndAlso _Opciones.Count > 0 Then

                Dim Opciones As List(Of Clases.Conversion) = ConverterOpciones()

                rblGenerico.RepeatColumns = Opciones.Count

                rblGenerico.DataSource = Opciones
                rblGenerico.DataValueField = "Identificador"
                rblGenerico.DataTextField = "Descripcion"
                rblGenerico.DataBind()

                rblGenerico.SelectedIndex = 0

            End If

        Else
            Throw New Genesis.Excepcion.NegocioExcepcion(Mesajes.ToString)

        End If

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

        If Me.Opciones Is Nothing OrElse (Me.Opciones IsNot Nothing AndAlso Me.Opciones.Count = 0) Then
            Mesajes.AppendLine(String.Format(Traduzir("014_parametro_obrigatorio"), "Opciones"))
        End If

        If Mesajes.Length > 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' Converte el List(Of KeyValuePair(Of String, String)) en una lista de la clase Opcion
    ''' </summary>
    ''' <returns>List(Of Opcion)</returns>
    ''' <remarks></remarks>
    Private Function ConverterOpciones() As List(Of Genesis.Comon.Clases.Conversion)
        Dim ListaOpciones As List(Of Clases.Conversion) = Nothing
        If Opciones IsNot Nothing AndAlso Opciones.Count > 0 Then
            ListaOpciones = New List(Of Clases.Conversion)
            For Each item In Opciones
                ListaOpciones.Add(New Clases.Conversion With {.Identificador = item.Key, _
                                                              .Descripcion = item.Value
                                                             })
            Next item
        End If
        Return ListaOpciones
    End Function

    ''' <summary>
    ''' Almacenar el valor selecionado en el controle
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()

        If Me.rblGenerico IsNot Nothing Then
            For Each item As System.Web.UI.WebControls.ListItem In Me.rblGenerico.Items
                If item.Selected Then
                    ItemSelecionado = item.Value

                End If

            Next item

        End If

    End Sub

#End Region

End Class