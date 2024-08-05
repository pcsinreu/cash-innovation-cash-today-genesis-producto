Imports Newtonsoft.Json
Imports Prosegur.Genesis.Comon
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class ucBusqueda
    Inherits UcBase


#Region "[PROPRIEDADES]"

    Private _Tipo As Enumeradores.TipoBusqueda
    Public Property Tipo() As Enumeradores.TipoBusqueda
        Get
            Return _Tipo
        End Get
        Set(value As Enumeradores.TipoBusqueda)
            _Tipo = value
        End Set
    End Property

    Private _Titulo As String
    Public Property Titulo() As String
        Get
            Return _Titulo
        End Get
        Set(value As String)
            _Titulo = value
        End Set
    End Property

    Private _Codigo As String
    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(value As String)
            _Codigo = value
        End Set
    End Property

    Private _Descripcion As String
    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(value As String)
            _Descripcion = value
        End Set
    End Property

    Private _AtributoDataBinding As String
    Public Property AtributoDataBinding() As String
        Get
            Return _AtributoDataBinding
        End Get
        Set(value As String)
            _AtributoDataBinding = value
        End Set
    End Property

    Private _EsMulti As Boolean
    Public Property EsMulti() As Boolean
        Get
            Return _EsMulti
        End Get
        Set(value As Boolean)
            _EsMulti = value
        End Set
    End Property

    Private _VisibilidadInicial As Boolean
    Public Property VisibilidadInicial() As Boolean
        Get
            Return _VisibilidadInicial
        End Get
        Set(value As Boolean)
            _VisibilidadInicial = value
        End Set
    End Property

    Public ReadOnly Property TipoString() As String
        Get
            Return _Tipo.RecuperarValor()
        End Get
    End Property

    Public ReadOnly Property EsMultiString() As String
        Get
            If _EsMulti Then
                Return "1"
            End If
            Return "0"
        End Get
    End Property

    Public ReadOnly Property setStyle() As String
        Get
            Dim _style = "style='"

            If _VisibilidadInicial Then
                _style &= "display:block;"
            Else
                _style &= "display:none;"
            End If

            If _EsMulti Then
                _style &= "height:auto;"
            End If
            Return _style & "'"
        End Get
    End Property

    Private _valoresMulti As String
    Public Property ValoresMulti As String
        Get
            Return _valoresMulti
        End Get
        Set(value As String)
            _valoresMulti = value
        End Set
    End Property

    Private _utilitarioBusca As String
    Public Property UtilitarioBusca As String
        Get
            Return _utilitarioBusca
        End Get
        Set(value As String)
            _utilitarioBusca = value
        End Set
    End Property

    Private _idAssociacao As String
    Public Property IDAssociacao As String
        Get
            Return _idAssociacao
        End Get
        Set(value As String)
            _idAssociacao = value
        End Set
    End Property

    Private _idAssociacaoPadre As String
    Public Property IDAssociacaoPadre As String
        Get
            Return _idAssociacaoPadre
        End Get
        Set(value As String)
            _idAssociacaoPadre = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
        End If

        Dim _DiccionarioBusqueda As New Dictionary(Of String, String)() From {
                {"msg_loading", Traduzir("071_Comon_msg_loading")},
                {"msg_obtenerValores", Traduzir("071_Comon_msg_obtenerValores")},
                {"msg_nenhumRegistroEncontrado", Traduzir("071_Comon_msg_nenhumRegistroEncontrado")},
                {"msg_nenhumRegistroSeleccionado", Traduzir("071_Comon_msg_nenhumRegistroSeleccionado")},
                {"msg_informacionesInvalidas", Traduzir("071_Comon_msg_informacionesInvalidas")},
                {"Campo_Codigo", Traduzir("071_BusquedaAvanzada_Campo_Codigo")},
                {"Campo_Descripcion", Traduzir("071_BusquedaAvanzada_Campo_Descripcion")},
                {"msg_Anular_Abono", Traduzir("071_Busqueda_msg_Anular_Abono")}
            }

        Dim _Script As String = "var _DiccionarioBusqueda = JSON.parse('" & JsonConvert.SerializeObject(_DiccionarioBusqueda) & "');"

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "script_UcBusqueda_Diccionario", _Script, True)
        ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "script_UcBusqueda", ResolveUrl("~/js/UcBusqueda.js"))
        ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "script_Comun", ResolveUrl("~/js/Abono/Comun.js"))
    End Sub

#End Region

End Class