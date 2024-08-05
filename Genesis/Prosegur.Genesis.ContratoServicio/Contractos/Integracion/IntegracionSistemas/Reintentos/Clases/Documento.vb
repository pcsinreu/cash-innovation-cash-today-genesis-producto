Namespace Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
    Public Class Documento
        Public Property CodExterno As String
        Public Property FechaGestion As DateTime
        Public Property CodCanal As String
        Public Property DesCanal As String
        Public Property CodSubCanal As String
        Public Property DesSubCanal As String
        Public Property Formulario As Formulario

        Public ReadOnly Property Cod_Des_Formulario() As String
            Get
                Return Formulario.ToString()
            End Get
        End Property
        Public ReadOnly Property Canal_Subcanal() As String
            Get
                Return String.Format("{0} - {1} / {2} - {3}", CodCanal, DesCanal, CodSubCanal, DesSubCanal)
            End Get
        End Property
        Public Sub New()
            Formulario = New Formulario
        End Sub
    End Class
End Namespace
