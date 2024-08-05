Imports System.Xml.Serialization

Namespace Login.GetDelegacionesUsuario

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra]  07/03/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _Delegaciones As New DelegacionColeccion()

#End Region

#Region "Propriedades"

        Public Property Delegaciones() As DelegacionColeccion
            Get
                Return _Delegaciones
            End Get
            Set(value As DelegacionColeccion)
                _Delegaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace