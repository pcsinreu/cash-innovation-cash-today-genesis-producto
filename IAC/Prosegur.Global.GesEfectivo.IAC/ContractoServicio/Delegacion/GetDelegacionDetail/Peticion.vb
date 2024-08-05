Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionDetail")> _
    <XmlRoot(Namespace:="urn:GetDelegacionDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoDelegacione As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoDelegacione() As String
            Get
                Return _CodigoDelegacione
            End Get
            Set(value As String)
                _CodigoDelegacione = value
            End Set
        End Property

#End Region

    End Class
End Namespace

