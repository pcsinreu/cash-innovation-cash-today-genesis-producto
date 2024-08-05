Imports System.Xml.Serialization
Imports System.Xml

Namespace Login

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 09/02/2009 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:Login")> _
    <XmlRoot(Namespace:="urn:Login")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _InformacionUsuario As InformacionUsuario
        Private _ResultadoOperacion As ResultadoOperacionLoginLocal

#End Region

#Region " Propriedades "

        Public Property InformacionUsuario() As InformacionUsuario
            Get
                Return _InformacionUsuario
            End Get
            Set(value As InformacionUsuario)
                _InformacionUsuario = value
            End Set
        End Property

        Public Property ResultadoOperacion() As ResultadoOperacionLoginLocal
            Get
                Return _ResultadoOperacion
            End Get
            Set(value As ResultadoOperacionLoginLocal)
                _ResultadoOperacion = value
            End Set
        End Property

#End Region

#Region "Contrutores"

        Public Sub New()
            _InformacionUsuario = New InformacionUsuario()
        End Sub

#End Region

    End Class

End Namespace