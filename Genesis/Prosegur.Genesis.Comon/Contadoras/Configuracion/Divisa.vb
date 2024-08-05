
Namespace Contadoras.Configuracion

    <Serializable()> _
    Public Class Divisa

        Private _Denominaciones As New List(Of Denominacion)
        Private _Nombre As String

        <System.Xml.Serialization.XmlAttribute()> _
        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Denominaciones() As List(Of Denominacion)
            Get
                Return _Denominaciones
            End Get
            Set(value As List(Of Denominacion))
                _Denominaciones = value
            End Set
        End Property

    End Class

End Namespace