Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.GetModulo

    <XmlType(Namespace:="urn:GetModulo")> _
    <XmlRoot(Namespace:="urn:GetModulo")> _
    <Serializable()> _
    Public Class Peticion

        Private _oidModulo As String
        Private _codModulo As String
        Private _desModulo As String
        Private _codCliente As String
        Private _bolActivo As Nullable(Of Boolean)
        Private _codModulos As List(Of String)

        Public Property CodModulos As List(Of String)
            Get
                Return _codModulos
            End Get
            Set(value As List(Of String))
                _codModulos = value
            End Set
        End Property

        Public Property OidModulo() As String
            Get
                Return _oidModulo
            End Get
            Set(value As String)
                _oidModulo = value
            End Set
        End Property
        Public Property CodModulo() As String
            Get
                Return _codModulo
            End Get
            Set(value As String)
                _codModulo = value
            End Set
        End Property

        Public Property DesModulo() As String
            Get
                Return _desModulo
            End Get
            Set(value As String)
                _desModulo = value
            End Set
        End Property

        Public Property CodCliente() As String
            Get
                Return _codCliente
            End Get
            Set(value As String)
                _codCliente = value
            End Set
        end Property

        Public Property BolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property
    End Class
End Namespace
