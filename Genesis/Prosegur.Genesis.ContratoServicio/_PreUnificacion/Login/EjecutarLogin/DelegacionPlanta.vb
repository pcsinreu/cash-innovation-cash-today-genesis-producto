Imports System.Xml.Serialization

Namespace Login.EjecutarLogin

    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLogin")> _
    <XmlRoot(Namespace:="urn:EjecutarLogin")> _
    Public Class DelegacionPlanta
        Inherits Delegacion

        Private _Plantas As New List(Of Planta)

        Public Property Plantas() As List(Of Planta)
            Get
                Return _Plantas
            End Get
            Set(value As List(Of Planta))
                _Plantas = value
            End Set
        End Property

    End Class

End Namespace