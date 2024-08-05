Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.GetIac

    <XmlType(Namespace:="urn:GetIac")> _
    <XmlRoot(Namespace:="urn:GetIac")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _codidoIac As String
        Private _descripcionIac As String
        Private _vigente As Boolean
        Private _terminosIac As TerminosIacColeccion

#End Region

#Region "Propriedades"

        Public Property CodidoIac() As String
            Get
                Return _codidoIac
            End Get
            Set(value As String)
                _codidoIac = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property TerminosIac() As TerminosIacColeccion
            Get
                Return _terminosIac
            End Get
            Set(value As TerminosIacColeccion)
                _terminosIac = value
            End Set
        End Property
#End Region
    End Class
End Namespace
