Imports System.Runtime.Serialization

Namespace Login.ObtenerAplicacionVersion

    ''' <summary>
    ''' AplicacionVersion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 11/05/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class AplicacionVersion

#Region " Variáveis "

        Private _OidAplicacionVersion As String
        Private _OidAplicacion As String
        Private _OidVersionPadre As String
        Private _CodigoVersion As String
        Private _FechaVersion As String
        Private _ContenidoVersion As String
        Private _ArchivoVersion As Byte()

#End Region

#Region "Propriedades"

        Public Property OidAplicacionVersion() As String
            Get
                Return _OidAplicacionVersion
            End Get
            Set(value As String)
                _OidAplicacionVersion = value
            End Set
        End Property

        Public Property OidAplicacion() As String
            Get
                Return _OidAplicacion
            End Get
            Set(value As String)
                _OidAplicacion = value
            End Set
        End Property

        Public Property OidVersionPadre() As String
            Get
                Return _OidVersionPadre
            End Get
            Set(value As String)
                _OidVersionPadre = value
            End Set
        End Property


        Public Property CodigoVersion() As String
            Get
                Return _CodigoVersion
            End Get
            Set(value As String)
                _CodigoVersion = value
            End Set
        End Property


        Public Property FechaVersion() As String
            Get
                Return _FechaVersion
            End Get
            Set(value As String)
                _FechaVersion = value
            End Set
        End Property

        Public Property ContenidoVersion() As String
            Get
                Return _ContenidoVersion
            End Get
            Set(value As String)
                _ContenidoVersion = value
            End Set
        End Property

        Public Property ArchivoVersion() As Byte()
            Get
                Return _ArchivoVersion
            End Get
            Set(value As Byte())
                _ArchivoVersion = value
            End Set
        End Property

#End Region

    End Class

End Namespace