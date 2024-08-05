
Namespace Contadoras.Configuracion

    <Serializable()> _
    Public Class Contadora

        Private _Modelo As String
        Private _Divisas As New List(Of Divisa)

        <System.Xml.Serialization.XmlAttribute()> _
        Public Property Modelo() As String
            Get
                Return _Modelo
            End Get
            Set(value As String)
                _Modelo = value
            End Set
        End Property

        Public Property Divisas() As List(Of Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As List(Of Divisa))
                _Divisas = value
            End Set
        End Property

    End Class

End Namespace