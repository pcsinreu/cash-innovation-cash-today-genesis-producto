Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.GetDivisasPaginacion

    <XmlType(Namespace:="urn:GetDivisasPaginacion")> _
    <XmlRoot(Namespace:="urn:GetDivisasPaginacion")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

        Private _CodigoIso As List(Of String)
        Private _Descripcion As List(Of String)
        Private _Vigente As Boolean

        Public Property CodigoIso() As List(Of String)
            Get
                Return _CodigoIso
            End Get
            Set(value As List(Of String))
                _CodigoIso = value
            End Set
        End Property

        Public Property Descripcion() As List(Of String)
            Get
                Return _Descripcion
            End Get
            Set(value As List(Of String))
                _Descripcion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

    End Class

End Namespace