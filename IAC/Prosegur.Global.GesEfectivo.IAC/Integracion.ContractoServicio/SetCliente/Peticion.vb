Imports System.Xml.Serialization
Imports System.Xml


Namespace SetCliente

    <XmlType(Namespace:="urn:SetCliente")> _
    <XmlRoot(Namespace:="urn:SetCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"
        Private _clientes As ClienteColeccion
        Private _codUsuario As String
#End Region

#Region "Propriedades"

        Public Property Clientes() As ClienteColeccion
            Get
                Return _clientes
            End Get
            Set(value As ClienteColeccion)
                _clientes = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace