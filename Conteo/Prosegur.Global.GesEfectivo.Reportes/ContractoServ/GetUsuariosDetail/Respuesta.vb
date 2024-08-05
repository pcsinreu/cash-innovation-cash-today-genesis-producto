Imports System.Xml.Serialization
Imports System.Xml

Namespace GetUsuariosDetail

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 27/09/2012 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GetUsuariosDetail")> _
    <XmlRoot(Namespace:="urn:GetUsuariosDetail")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _usuarios As New UsuarioColeccion

#End Region

#Region "Propriedades"

        Public Property Usuarios() As UsuarioColeccion
            Get
                Return _usuarios
            End Get
            Set(value As UsuarioColeccion)
                _usuarios = value
            End Set
        End Property

#End Region


    End Class
End Namespace