Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoPtoServicio

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danienines] 13/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoPtoServicio")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoPtoServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _existe As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Existe() As Boolean
            Get
                Return _existe
            End Get
            Set(value As Boolean)
                _existe = value
            End Set
        End Property

#End Region

    End Class

End Namespace