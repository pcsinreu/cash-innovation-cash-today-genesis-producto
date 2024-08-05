Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoCliente.SetTiposClientes

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetTiposClientes")> _
    <XmlRoot(Namespace:="urn:SetTiposClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _codTipoCliente As String
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoCliente() As String
            Get
                Return _codTipoCliente
            End Get
            Set(value As String)
                _codTipoCliente = value
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
