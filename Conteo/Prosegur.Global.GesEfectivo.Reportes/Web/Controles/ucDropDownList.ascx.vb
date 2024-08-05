Imports Prosegur.Genesis.Comunicacion

Public Class ucDropDownList
    Inherits System.Web.UI.UserControl

#Region "Propriedades"
    Public Property CampoObrigatorio As Boolean
        Get
            Return ViewState("CampoObrigatorio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorio") = value
        End Set
    End Property
#End Region

#Region "[METODOS]"
    Public Sub PopularControle(peticion As Prosegur.Genesis.ContractoServicio.Dinamico.Peticion)
        Dim proxy As New ListadosConteo.ProxyDinamico
        Dim respuesta As New Prosegur.Genesis.ContractoServicio.Dinamico.Respuesta

        respuesta = proxy.Consultar(peticion)
        If Not String.IsNullOrEmpty(respuesta.MensajeError) Then
            Throw New Exception(respuesta.MensajeError)
        End If

        Me.PopularControle("Descripcion", "Codigo", respuesta.Valores)

    End Sub

    Public Sub PopularControle(DataTextField As String, DataValueField As String, dataSource As Object)
        Me.ddl.DataTextField = "Descripcion"
        Me.ddl.DataValueField = "Codigo"
        Me.ddl.DataSource = dataSource
        Me.ddl.DataBind()
    End Sub
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

End Class