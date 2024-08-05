Imports System.Xml.Serialization
Imports System.Xml

Namespace Puesto.GetPuestoDetail

    <XmlType(Namespace:="urn:GetPuestoDetail")> _
    <XmlRoot(Namespace:="urn:GetPuestoDetail")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _HostPuesto As String
        Private _CodigoPuesto As String


#End Region

#Region "Propriedades"

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property HostPuesto() As String
            Get
                Return _HostPuesto
            End Get
            Set(value As String)
                _HostPuesto = value
            End Set
        End Property

        Public Property CodigoPuesto() As String
            Get
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        

#End Region

    End Class
End Namespace
