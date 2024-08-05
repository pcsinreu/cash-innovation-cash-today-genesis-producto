Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoPuntosServicio.getTiposPuntosServicio
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getTiposPuntosServicio")> _
    <XmlRoot(Namespace:="urn:getTiposPuntosServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _TipoPuntoServicio As TipoPuntosServicioColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoPuntoServicio() As TipoPuntosServicioColeccion
            Get
                Return _TipoPuntoServicio
            End Get
            Set(value As TipoPuntosServicioColeccion)
                _TipoPuntoServicio = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
