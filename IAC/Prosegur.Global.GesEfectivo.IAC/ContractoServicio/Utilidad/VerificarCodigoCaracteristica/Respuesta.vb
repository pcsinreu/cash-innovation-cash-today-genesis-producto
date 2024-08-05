Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoCaracteristica

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoCaracteristica")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoCaracteristica")> _
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