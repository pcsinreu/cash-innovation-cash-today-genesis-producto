Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposProcesado.VerificarCodigoTipoProcesado
    <XmlType(Namespace:="urn:VerificarCodigoTipoProcesado")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoTipoProcesado")> _
    <Serializable()> _
    Public Class Peticion

        Private _codigo As String

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

    End Class

End Namespace

