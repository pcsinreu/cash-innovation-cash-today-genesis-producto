Imports System.Xml.Serialization
Imports System.Xml


Namespace Canal.GetSubCanalesByCertificado

    <XmlType(Namespace:="urn:GetSubCanalesByCertificado")> _
    <XmlRoot(Namespace:="urn:GetSubCanalesByCertificado")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _codigoCertificado As String

#End Region

#Region "Propriedades"

        Public Property codigoCertificado() As String
            Get
                Return _codigoCertificado
            End Get
            Set(value As String)
                _codigoCertificado = value
            End Set
        End Property

#End Region

    End Class

End Namespace