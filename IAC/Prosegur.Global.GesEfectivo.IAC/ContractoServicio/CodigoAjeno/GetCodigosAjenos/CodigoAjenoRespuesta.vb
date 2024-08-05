
Namespace CodigoAjeno.GetCodigosAjenos

    ''' <summary>
    ''' Classe CodigoAjenoRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class CodigoAjenoRespuesta

#Region "[VARIAVEIS]"

        Private _OidCodigoAjeno As String
        Private _CodIdentificador As String
        Private _CodAjeno As String
        Private _DesAjeno As String
        Private _BolDefecto As Boolean
        Private _BolMigrado As Boolean
        Private _BolActivo As Boolean
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidCodigoAjeno() As String
            Get
                Return _OidCodigoAjeno
            End Get
            Set(value As String)
                _OidCodigoAjeno = value
            End Set
        End Property

        Public Property CodIdentificador() As String
            Get
                Return _CodIdentificador
            End Get
            Set(value As String)
                _CodIdentificador = value
            End Set
        End Property

        Public Property CodAjeno() As String
            Get
                Return _CodAjeno
            End Get
            Set(value As String)
                _CodAjeno = value
            End Set
        End Property

        Public Property DesAjeno() As String
            Get
                Return _DesAjeno
            End Get
            Set(value As String)
                _DesAjeno = value
            End Set
        End Property

        Public Property BolDefecto() As Boolean
            Get
                Return _BolDefecto
            End Get
            Set(value As Boolean)
                _BolDefecto = value
            End Set
        End Property

        Public Property BolMigrado() As Boolean
            Get
                Return _BolMigrado
            End Get
            Set(value As Boolean)
                _BolMigrado = value
            End Set
        End Property

        Public Property BolActivo() As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                _BolActivo = value
            End Set
        End Property

        Public Property GmtCreacion() As Date
            Get
                Return _GmtCreacion
            End Get
            Set(value As Date)
                _GmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property GmtModificacion() As Date
            Get
                Return _GmtModificacion
            End Get
            Set(value As Date)
                _GmtModificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
