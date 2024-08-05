Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboRedes

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboRedes")> _
    <XmlRoot(Namespace:="urn:GetComboRedes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _redes As List(Of Red)

#End Region

#Region "[Propriedades]"

        Public Property Redes() As List(Of Red)
            Get
                Return _redes
            End Get
            Set(value As List(Of Red))
                _redes = value
            End Set
        End Property

#End Region

    End Class

End Namespace