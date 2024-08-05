Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerAplicacionVersion
    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 09/05/2012 Criado
    ''' </history>

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerAplicacionVersion")> _
    <XmlRoot(Namespace:="urn:ObtenerAplicacionVersion")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _AplicacionVersion As AplicacionVersion

#End Region

#Region "Propriedades"

        Public Property AplicacionVersion() As AplicacionVersion
            Get
                Return _AplicacionVersion
            End Get
            Set(value As AplicacionVersion)
                _AplicacionVersion = value
            End Set
        End Property

#End Region

    End Class

End Namespace