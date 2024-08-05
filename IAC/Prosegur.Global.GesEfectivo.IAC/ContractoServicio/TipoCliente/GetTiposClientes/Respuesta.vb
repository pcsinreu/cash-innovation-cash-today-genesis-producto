Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoCliente.GetTiposClientes
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTiposClientes")> _
    <XmlRoot(Namespace:="urn:GetTiposClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _TipoCliente As TipoClienteColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoCliente() As TipoClienteColeccion
            Get
                Return _TipoCliente
            End Get
            Set(value As TipoClienteColeccion)
                _TipoCliente = value
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

