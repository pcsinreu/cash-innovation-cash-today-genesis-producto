Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario

'Imports Prosegur.Genesis.Comon.Pantallas


Public Class MasterModal
    Inherits System.Web.UI.MasterPage

    Private _ValidacaoAcesso As ValidacaoAcesso = Nothing



    Private _Historico As List(Of KeyValuePair(Of String, String))
    Public ReadOnly Property Historico() As List(Of KeyValuePair(Of String, String))
        Get
            If _Historico Is Nothing Then
                _Historico = Session("Master_Historico")
                If _Historico Is Nothing Then
                    _Historico = New List(Of KeyValuePair(Of String, String))()
                    Session("Master_Historico") = _Historico
                End If
            End If
            Return _Historico
        End Get
    End Property

    Public Property HaySoloUnoSector As Boolean
        Get
            Return Session("linkSectorHabilitado")
        End Get
        Set(value As Boolean)
            Session("linkSectorHabilitado") = value
        End Set
    End Property

    Private _HabilitarHistorico As Boolean = True
    Public Property HabilitarHistorico() As Boolean
        Get
            Return _HabilitarHistorico
        End Get
        Set(value As Boolean)
            _HabilitarHistorico = value
        End Set
    End Property


    Private Sub AdicionaHistorico(itemHistorico As KeyValuePair(Of String, String))
        Dim item As Nullable(Of KeyValuePair(Of String, String))
        item = Historico.Find(Function(i) String.Compare(i.Key, itemHistorico.Key, True) = 0)
        If item IsNot Nothing Then
            Historico.Remove(item)
        End If
        If Historico.Count = 5 Then
            Historico.RemoveAt(0)
        End If
        If Not itemHistorico.Key.Contains("EsPopup=True") Then
            Historico.Add(itemHistorico)
        End If

    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = 0 ; var _AjusteVerano = 0;", True)

        'Registra variaveis de tradução que são usadas no calendario
        ' Dim imgUrl As String = ResolveUrl("~/App_Themes/Padrao/css/img/button/Calendar.png")
        Dim imgUrl As String = ResolveUrl("~/Imagenes/Calendar.png")
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetTraducao",
                                                    String.Format("var _bntProximo = '{0}';" &
                                                                  "var _btnAnterior = '{1}';" &
                                                                  "var _btnAgora = '{2}';" &
                                                                  "var _btnConfirma = '{3}';" &
                                                                  "var _meses = '{4}';" &
                                                                  "var _dias = '{5}';" &
                                                                  "var _horas = '{6}';" &
                                                                  "var _minutos = '{7}';" &
                                                                  "var _segundos = '{8}';" &
                                                                  "var _imgCalendar = '{9}';",
                                                                  Tradutor.Traduzir("bntProximo"),
                                                                  Tradutor.Traduzir("btnAnterior"),
                                                                  Tradutor.Traduzir("btnAgora"),
                                                                  Tradutor.Traduzir("btnConfirma"),
                                                                  Tradutor.Traduzir("meses"),
                                                                  Tradutor.Traduzir("dias"),
                                                                  Tradutor.Traduzir("horas"),
                                                                  Tradutor.Traduzir("minutos"),
                                                                  Tradutor.Traduzir("segundos"),
                                                                  imgUrl), True)

    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

    End Sub
    Public Sub ExibirModal(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True, Optional disparaEvento As Boolean = False, Optional botao As String = "")
        If urlCaminho.Contains("?") Then
            urlCaminho &= "&divModal=" & divModal_Modal.ClientID & "&ifrModal=" & ifrModal_Modal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
        Else
            urlCaminho &= "?divModal=" & divModal_Modal.ClientID & "&ifrModal=" & ifrModal_Modal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
        End If
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "exibirModal", "ExibirUrlModal_Modal('#" & divModal_Modal.ClientID & "', '#" & ifrModal_Modal.ClientID & "', '" & urlCaminho & "','" & tituloModal & "', " & altura & ", " & largura & "," & efetuarReload.ToString().ToLower() & "," & disparaEvento.ToString().ToLower() & ",'" & botao.ToString() & "' );", True)
    End Sub
End Class
