Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetCodigoDelegacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 18/05/2012 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetCodigoDelegacion")> _
    <XmlRoot(Namespace:="urn:GetCodigoDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _CodigoDelegacion As String

#End Region

#Region "[Propriedades]"

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
