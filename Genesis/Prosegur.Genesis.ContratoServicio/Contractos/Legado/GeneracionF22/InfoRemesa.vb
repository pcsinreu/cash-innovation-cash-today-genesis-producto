Imports System.Xml.Serialization

Namespace Legado.GeneracionF22

    ''' <summary>
    ''' Entidad InfoRemesa
    ''' </summary>
    ''' <history>[abueno] 13/07/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class InfoRemesa
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _OidRemesaSalidas As String
        Private _OidRemesaLegado As String
        Private _OidDocumentoSaldos As String

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad OidRemesaSalidas
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidRemesaSalidas() As String
            Get
                Return _OidRemesaSalidas
            End Get
            Set(value As String)
                _OidRemesaSalidas = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad OidRemesaLegado
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidRemesaLegado() As String
            Get
                Return _OidRemesaLegado
            End Get
            Set(value As String)
                _OidRemesaLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad OidDocumentoSaldos
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidDocumentoSaldos() As String
            Get
                Return _OidDocumentoSaldos
            End Get
            Set(value As String)
                _OidDocumentoSaldos = value
            End Set
        End Property

#End Region

    End Class

End Namespace