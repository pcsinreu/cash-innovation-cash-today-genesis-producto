Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerVersiones

    <Serializable>
    <XmlType(Namespace:="urn:ObtenerVersiones")>
    <XmlRoot(Namespace:="urn:ObtenerVersiones")>
    <DataContract()>
    Public Class Peticion

#Region " Variáveis "
        Private _CodigoAplicacion As String
#End Region

#Region "Propriedades"
        <DataMember()> _
        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property
#End Region

    End Class

End Namespace