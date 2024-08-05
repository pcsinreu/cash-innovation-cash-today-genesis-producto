Imports System.Xml.Serialization
Imports System.Xml


Namespace Canal.GetSubCanalesByCanal

    <XmlType(Namespace:="urn:GetSubCanalesByCanal")> _
    <XmlRoot(Namespace:="urn:GetSubCanalesByCanal")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _codigoCanal As List(Of String)

#End Region

#Region "Propriedades"

        Public Property codigoCanal() As List(Of String)
            Get
                Return _codigoCanal
            End Get
            Set(value As List(Of String))
                _codigoCanal = value
            End Set
        End Property

#End Region

    End Class

End Namespace