Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionByCertificado

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionByCertificado")> _
    <XmlRoot(Namespace:="urn:GetDelegacionByCertificado")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _CodigoCertificado As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigoCertificado As String
            Get
                Return _CodigoCertificado
            End Get
            Set(value As String)
                _CodigoCertificado = value
            End Set
        End Property
#End Region

    End Class
End Namespace
