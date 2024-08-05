Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.SetValor

    <XmlType(Namespace:="urn:SetValor")> _
    <XmlRoot(Namespace:="urn:SetValor")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _valor As Valor

#End Region

#Region "Propriedades"

        Public Property Valor() As Valor
            Get
                Return _valor
            End Get
            Set(value As Valor)
                _valor = value
            End Set
        End Property

#End Region

    End Class
End Namespace

