Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.RecuperarModulo

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:RecuperarModulo")> _
    <XmlRoot(Namespace:="urn:RecuperarModulo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Modulos1 As List(Of Modulo)

#End Region

#Region "[Propriedades]"


        Public Property Modulos() As List(Of Modulo)
            Get
                Return _Modulos1
            End Get
            Set(value As List(Of Modulo))
                _Modulos1 = value
            End Set
        End Property

#End Region

    End Class

End Namespace