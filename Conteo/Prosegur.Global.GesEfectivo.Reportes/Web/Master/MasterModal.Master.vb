Imports System.Reflection
Imports Prosegur.Framework.Dicionario

Namespace Master

    Public Class MasterModal
        Inherits System.Web.UI.MasterPage

        Private _ValidacaoAcesso As ValidacaoAcesso = Nothing

        Public Property InformacionUsuario() As ContractoServ.Login.InformacionUsuario
            Get

                ' se sessão não foi setado
                If Session("BaseInformacoesUsuario") IsNot Nothing Then

                    ' tentar recuperar objeto da sessao
                    Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServ.Login.InformacionUsuario)

                    ' retornar objeto
                    Return Info

                End If

                Return New ContractoServ.Login.InformacionUsuario

            End Get
            Set(value As ContractoServ.Login.InformacionUsuario)
                Session("BaseInformacoesUsuario") = value
            End Set
        End Property

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

        Public ReadOnly Property ControleErro() As Erro
            Get
                Return Erro1
            End Get
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
            _ValidacaoAcesso = New ValidacaoAcesso(InformacionUsuario)

        End Sub

        Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
          
        End Sub
    
    End Class
End Namespace