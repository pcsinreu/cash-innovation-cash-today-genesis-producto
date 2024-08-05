Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.SetDelegacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetDelegacion")> _
    <XmlRoot(Namespace:="urn:SetDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"
        Private _CodigoDelegacion As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

#End Region
    End Class
End Namespace
