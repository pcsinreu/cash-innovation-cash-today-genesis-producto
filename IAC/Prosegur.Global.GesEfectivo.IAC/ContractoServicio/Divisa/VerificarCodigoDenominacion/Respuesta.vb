Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.VerificarCodigoDenominacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoDenominacion")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoDenominacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Existe As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Existe() As Boolean
            Get
                Return _Existe
            End Get
            Set(value As Boolean)
                _Existe = value
            End Set
        End Property

#End Region

    End Class

End Namespace