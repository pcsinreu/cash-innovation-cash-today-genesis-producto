Imports System.Xml.Serialization
Imports System.Xml

Namespace Morfologia.VerificarMorfologia

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 29/12/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarMorfologia")> _
    <XmlRoot(Namespace:="urn:VerificarMorfologia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _bolExiste As Boolean

#End Region

#Region "[Propriedades]"

        Public Property BolExiste() As Boolean
            Get
                Return _bolExiste
            End Get
            Set(value As Boolean)
                _bolExiste = value
            End Set
        End Property

#End Region

    End Class

End Namespace