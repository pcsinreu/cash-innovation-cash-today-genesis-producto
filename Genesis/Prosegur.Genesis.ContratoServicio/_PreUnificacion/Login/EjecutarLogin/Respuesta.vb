Imports System.Xml.Serialization

Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLogin")> _
    <XmlRoot(Namespace:="urn:EjecutarLogin")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _Usuario As Usuario
        Private _ResultadoOperacion As ResultadoOperacionLoginLocal
        Private _Aplicaciones As New AplicacionVersionColeccion

#End Region

#Region "Propriedades"

        Public Property Usuario() As Usuario
            Get
                Return _Usuario
            End Get
            Set(value As Usuario)
                _Usuario = value
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

        Public Property Aplicaciones() As AplicacionVersionColeccion
            Get
                Return _Aplicaciones
            End Get
            Set(value As AplicacionVersionColeccion)
                _Aplicaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace