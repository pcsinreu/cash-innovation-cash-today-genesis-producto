Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper

''' <summary>
''' Classe contratual do petição Controle Helper.
''' </summary>
<Serializable()>
Public Class PeticionHelperTipoContenedor
    Inherits BasePeticionPaginacion

    Public Property Codigo As String

    Public Property Descripcion As String

    Public Property Vigente As Boolean?

    Public Property OrdenacaoSQL As SerializableDictionary(Of String, OrderSQL)

    Public Property JuncaoSQL As SerializableDictionary(Of String, JoinSQL)

    Public Property FiltroSQL As SerializableDictionary(Of Tabela, List(Of ArgumentosFiltro))

    Public Property Tabela As Tabela

    Public Property Query As QueryHelperControl

    Public Property DadosPeticao As List(Of PeticionTipoContenedor)

    ''' <summary>
    ''' Indica se consulta será feita por like ou não
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UsarLike As Boolean = True

End Class

''' <summary>
''' Classe Peticion Controle Helper.
''' </summary>
<Serializable()>
Public Class PeticionTipoContenedor

    Private _Identificador As String
    Private _Codigo As String
    Private _Descripcion As String
    Private _IdentificadorPai As String
    Private _Vigente As Boolean?
    Private _NecCantidad As Integer
    Private _ValorMaximoImporte As Integer
    Private _TabelaIdentificador As Helper.Enumeradores.Tabelas.TabelaHelper
    Private _TabelaIdentificadorPai As Helper.Enumeradores.Tabelas.TabelaHelper

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

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(value As String)
            _Descripcion = value
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

    Public Property Vigente As Boolean?
        Get
            Return _Vigente
        End Get
        Set(value As Boolean?)
            _Vigente = value
        End Set
    End Property
    ''' <summary>
    ''' Obtém e Define a Tabela que o Identificador pertence.
    ''' </summary>
    Public Property TabelaIdentificador() As Helper.Enumeradores.Tabelas.TabelaHelper
        Get
            Return _TabelaIdentificador
        End Get
        Set(value As Helper.Enumeradores.Tabelas.TabelaHelper)
            _TabelaIdentificador = value
        End Set
    End Property

    ''' <summary>
    ''' Obtém e Define a Tabela que o IdentificadorPai pertence.
    ''' </summary>
    Public Property TabelaIdentificadorPai() As Helper.Enumeradores.Tabelas.TabelaHelper
        Get
            Return _TabelaIdentificadorPai
        End Get
        Set(value As Helper.Enumeradores.Tabelas.TabelaHelper)
            _TabelaIdentificadorPai = value
        End Set
    End Property

End Class