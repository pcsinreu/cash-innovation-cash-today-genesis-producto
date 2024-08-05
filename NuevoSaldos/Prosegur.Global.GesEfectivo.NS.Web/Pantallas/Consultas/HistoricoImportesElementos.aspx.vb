Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis

Public Class HistoricoImportesElementos
    Inherits Base

#Region "[PROPRIEDADES]"

    Private _identificadorElemento As String = Nothing
    Public ReadOnly Property IdentificadorElemento() As String
        Get
            If String.IsNullOrEmpty(_identificadorElemento) Then
                _identificadorElemento = Request.QueryString("IdentificadorElemento")
            End If
            Return _identificadorElemento
        End Get
    End Property

    Private _TipoElemento As Enumeradores.TipoElemento?
    Public ReadOnly Property TipoElemento() As Enumeradores.TipoElemento?
        Get
            If Not _TipoElemento.HasValue AndAlso Not String.IsNullOrEmpty(Request.QueryString("TipoElemento")) Then
                Try
                    _TipoElemento = [Enum].Parse(GetType(Enumeradores.TipoElemento), Request.QueryString("TipoElemento"))
                Catch ex As Exception
                    _TipoElemento = Nothing
                End Try
            End If
            Return _TipoElemento
        End Get
    End Property

    Private _Filtro As ucFiltroHistImpElementos
    Public ReadOnly Property Filtro() As ucFiltroHistImpElementos
        Get
            If _Filtro Is Nothing Then
                _Filtro = LoadControl("~\Controles\ucFiltroHistImpElementos.ascx")
                _Filtro.ID = "Filtro"
                AddHandler _Filtro.Erro, AddressOf ErroControles
                If phFiltro.Controls.Count = 0 Then
                    phFiltro.Controls.Add(_Filtro)
                End If
            End If
            Return _Filtro
        End Get
    End Property


#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONSULTAR_HISTORICOIMPORTESELEMENTOS
        MyBase.ValidarAcesso = False
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("063_Titulo")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub ConfigurarControles()

    End Sub

    Public Sub Buscar(Optional esBuscaDefecto As Boolean = False)

    End Sub

#End Region

End Class