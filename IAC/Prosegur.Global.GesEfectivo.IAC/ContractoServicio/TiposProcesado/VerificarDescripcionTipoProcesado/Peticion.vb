Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposProcesado.VerificarDescripcionTipoProcesado
    <XmlType(Namespace:="urn:VerificarDescripcionTipoProcesado")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionTipoProcesado")> _
    <Serializable()> _
    Public Class Peticion

        Private _descripcion As String

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property
    End Class

End Namespace