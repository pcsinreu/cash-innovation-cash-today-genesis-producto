Imports System.Xml.Serialization
Imports System.Xml

Namespace Puesto.SetPuesto

    <XmlType(Namespace:="urn:SetPuesto")> _
    <XmlRoot(Namespace:="urn:SetPuesto")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _Accion As Enumeradores.Accion
        Private _CodigoDelegacion As String
        Private _CodigoAplicacion As String
        Private _CodigoPuesto As String
        Private _CodigoHostPuesto As String
        Private _PuestoVigente As Boolean
        Private _CodigoUsuario As String
#End Region

#Region "Propriedades"

        Public Property Accion As Enumeradores.Accion
            Get
                Return _Accion
            End Get
            Set(value As Enumeradores.Accion)
                _Accion = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
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

        Public Property CodigoHostPuesto() As String
            Get
                Return _CodigoHostPuesto
            End Get
            Set(value As String)
                _CodigoHostPuesto = value
            End Set
        End Property

        Public Property PuestoVigente() As Boolean
            Get
                Return _PuestoVigente
            End Get
            Set(value As Boolean)
                _PuestoVigente = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class
End Namespace
