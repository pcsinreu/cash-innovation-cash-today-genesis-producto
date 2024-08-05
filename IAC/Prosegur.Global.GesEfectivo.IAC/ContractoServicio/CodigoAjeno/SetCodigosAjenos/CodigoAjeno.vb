
Namespace CodigoAjeno.SetCodigosAjenos

    ''' <summary>
    ''' Classe CodigoAjeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 19/04/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class CodigoAjeno
        Inherits CodigoAjenoBase

#Region "[VARIAVEIS]"

        Private _OidTablaGenesis As String
        Private _CodTipoTablaGenesis As String
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"


        Public Property OidTablaGenesis() As String
            Get
                Return _OidTablaGenesis
            End Get
            Set(value As String)
                _OidTablaGenesis = value
            End Set
        End Property

        Public Property CodTipoTablaGenesis() As String
            Get
                Return _CodTipoTablaGenesis
            End Get
            Set(value As String)
                _CodTipoTablaGenesis = value
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
