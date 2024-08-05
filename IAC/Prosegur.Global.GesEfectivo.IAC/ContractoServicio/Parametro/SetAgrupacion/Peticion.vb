Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.SetAgrupacion

    <XmlType(Namespace:="urn:SetAgrupacion")> _
    <XmlRoot(Namespace:="urn:SetAgrupacion")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _DescripcionCorta As String
        Private _DescripcionLarga As String
        Private _NecOrden As Nullable(Of Integer)
        Private _CodigoUsuario As String

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

        Public Property CodigoNivel() As String
            Get
                Return _CodigoNivel
            End Get
            Set(value As String)
                _CodigoNivel = value
            End Set
        End Property

        Public Property DescripcionCorta() As String
            Get
                Return _DescripcionCorta
            End Get
            Set(value As String)
                _DescripcionCorta = value
            End Set
        End Property

        Public Property DescripcionLarga() As String
            Get
                Return _DescripcionLarga
            End Get
            Set(value As String)
                _DescripcionLarga = value
            End Set
        End Property

        Public Property NecOrden() As Nullable(Of Integer)
            Get
                Return _NecOrden
            End Get
            Set(value As Nullable(Of Integer))
                _NecOrden = value
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
