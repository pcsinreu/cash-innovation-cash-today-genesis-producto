Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.VerificarDescricaoParametroOpcion
    <XmlType(Namespace:="urn:VerificarDescricaoParametroOpcion")> _
    <XmlRoot(Namespace:="urn:VerificarDescricaoParametroOpcion")> _
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
