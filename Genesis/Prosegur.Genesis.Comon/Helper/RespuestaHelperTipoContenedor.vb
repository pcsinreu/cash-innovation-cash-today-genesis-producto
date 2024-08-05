Imports Prosegur.Genesis.Comon

''' <summary>
''' Classe contratual do resposta Controle Helper.
''' </summary>
<Serializable()>
Public Class RespuestaHelperTipoContenedor
    Inherits BaseRespuestaPaginacion

    ''' <summary>
    ''' Valida se ocorreram modificação nos dados de Resposta Helper.
    ''' </summary>
    Public Property ResultadoModificado As Boolean

    ''' <summary>
    ''' Obtém e Define Lista de dados do tipo Respuesta Helper.
    ''' </summary>
    Public Property DatosRespuesta As List(Of Helper.RespuestaHelperTipoContenedorDatos)
End Class

Namespace Helper

    ''' <summary>
    ''' Classe responsável por gerenciar o Tipo de Resposta do Controle Helper.
    ''' </summary>
    <Serializable()>
    Public Class RespuestaHelperTipoContenedorDatos

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Vigente As Boolean
        Private _NecCantidad As String
        Private _ValorMaximoImporte As Integer
        Private _IdentificadorPai As String
        Private _CodUnidadeMedida As String
        Private _CodTipoUnidadeMedida As String
        Private _IdentificadorUnidadeMedida As String
        Private _DesUnidadeMedida As String
        Private _NumValorUnidadeMedida As Integer
        Private _LlevaPrecinto As Boolean

        ''' <summary>
        ''' Obtém e Define Código Identificador.
        ''' </summary>
        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define Código.
        ''' </summary>
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define cantidad.
        ''' </summary>
        Public Property NecCantidad As Integer
            Get
                Return _NecCantidad
            End Get
            Set(value As Integer)
                _NecCantidad = value
            End Set
        End Property

        Public Property ValorMaximoImporte As Integer
            Get
                Return _ValorMaximoImporte
            End Get
            Set(value As Integer)
                _ValorMaximoImporte = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define Descrição.
        ''' </summary>
        Public Property Descricao As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define valor do Identificador Pai.
        ''' </summary>
        Public Property IdentificadorPai As String
            Get
                Return _IdentificadorPai
            End Get
            Set(value As String)
                _IdentificadorPai = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define valor do Identificador Pai.
        ''' </summary>
        Public Property Vigente As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property CodUnidadeMedida As String
            Get
                Return _CodUnidadeMedida
            End Get
            Set(value As String)
                _CodUnidadeMedida = value
            End Set
        End Property

        Public Property CodTipoUnidadeMedida As String
            Get
                Return _CodTipoUnidadeMedida
            End Get
            Set(value As String)
                _CodTipoUnidadeMedida = value
            End Set
        End Property

        Public Property DesUnidadeMedida As String
            Get
                Return _DesUnidadeMedida
            End Get
            Set(value As String)
                _DesUnidadeMedida = value
            End Set
        End Property

        Public Property NumValorUnidadeMedida As Integer
            Get
                Return _NumValorUnidadeMedida
            End Get
            Set(value As Integer)
                _NumValorUnidadeMedida = value
            End Set
        End Property

        Public Property IdentificadorUnidadeMedida As String
            Get
                Return _IdentificadorUnidadeMedida
            End Get
            Set(value As String)
                _IdentificadorUnidadeMedida = value
            End Set
        End Property

        Public Property LlevaPrecinto As Boolean
            Get
                Return _LlevaPrecinto
            End Get
            Set(value As Boolean)
                _LlevaPrecinto = value
            End Set
        End Property
    End Class
End Namespace