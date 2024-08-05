Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSubCliente.getTiposSubclientes
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getTiposSubclientes")> _
    <XmlRoot(Namespace:="urn:getTiposSubclientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _TipoSubCliente As TipoSubClienteColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoSubCliente() As TipoSubClienteColeccion
            Get
                Return _TipoSubCliente
            End Get
            Set(value As TipoSubClienteColeccion)
                _TipoSubCliente = value
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

