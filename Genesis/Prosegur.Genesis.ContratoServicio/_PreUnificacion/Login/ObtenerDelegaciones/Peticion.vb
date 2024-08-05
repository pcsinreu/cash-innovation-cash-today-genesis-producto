Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerDelegaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")> _
    <DataContract()> _
    Public Class Peticion

#Region " Variáveis "
        Private _NombreContinente As String
        Private _CodigoPais As String
        Private _NombreZona As String
#End Region

#Region "Propriedades"

        <DataMember()> _
        Public Property NombreContinente() As String
            Get
                Return _NombreContinente
            End Get
            Set(value As String)
                _NombreContinente = value
            End Set
        End Property

        <DataMember()> _
        Public Property CodigoPais() As String
            Get
                Return _CodigoPais
            End Get
            Set(value As String)
                _CodigoPais = value
            End Set
        End Property

        <DataMember()> _
        Public Property NombreZona() As String
            Get
                Return _NombreZona
            End Get
            Set(value As String)
                _NombreZona = value
            End Set
        End Property

#End Region

    End Class
End Namespace