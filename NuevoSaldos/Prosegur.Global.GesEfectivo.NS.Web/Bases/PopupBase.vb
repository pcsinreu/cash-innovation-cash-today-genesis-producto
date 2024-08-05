Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class PopupBase
    Inherits UcBase
    Implements IPostBackEventHandler

    Public Event Fechado As EventHandler(Of PopupEventArgs)
    Public Event FechadoAtualizar As EventHandler(Of PopupEventArgs)
    Protected Overridable Sub OnFechado(e As PopupEventArgs)
        RaiseEvent Fechado(Me, e)
    End Sub

    Public Sub FecharPopup()
        FecharPopup(Nothing)
    End Sub

    Public Sub FecharPopup(e As Object)
        OnFechado(New PopupEventArgs(e))
    End Sub

    Public Sub RaisePostBackEvent(IPostBackEventHandler As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent
        If IPostBackEventHandler = "FecharPopup" Then
            RaiseEvent FechadoAtualizar(Me, Nothing)
        End If
    End Sub

#Region "[PROPRIEDADES]"
    Public Property Titulo As String

    Private Property dicionario() As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
        Get
            If Session("dicionario") IsNot Nothing Then
                Return Session("dicionario")
            Else
                Session("dicionario") = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            Return Session("dicionario")
        End Get
        Set(value As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String)))
            Session("dicionario") = value
        End Set
    End Property


    Public Sub CarregaDicinario()

        CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
        CarregaChavesDicinario(Me.CodFuncionalidad)

    End Sub



    Public Property CodFuncionalidad() As String
        Get
            Return ViewState("CodFuncionalidad")
        End Get
        Set(value As String)
            ViewState("CodFuncionalidad") = value
        End Set
    End Property
    Public ReadOnly Property CodFuncionalidadGenerica() As String
        Get
            Return "GENERICO"
        End Get
    End Property
#End Region

    Public Function RecuperarValorDic(chave) As String
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 AndAlso (dicionario.ContainsKey(Me.CodFuncionalidad) OrElse dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                Dim chavesDic = dicionario(Me.CodFuncionalidad)
                If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                    Return chavesDic(chave)
                End If

                chavesDic = dicionario(Me.CodFuncionalidadGenerica)
                If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                    Return chavesDic(chave)
                End If

            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

        Return chave
    End Function
    Public Sub MostraMensagemExcecao(exception As Exception)
        ' logar erro no banco
        'Utilidad.LogarErroAplicacao(Nothing, exception.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))
        MostraMensagemErro(exception.ToString(), String.Empty)
    End Sub
    Public Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                      Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Private Sub CarregaChavesDicinario(CodigoFuncionalidad As String)
        If Not String.IsNullOrEmpty(CodigoFuncionalidad) Then
            If dicionario Is Nothing Then
                dicionario = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            'Se já tiver carregado os dicionarios da funcionalidade nao carrega novamente
            If dicionario.ContainsKey(CodigoFuncionalidad) AndAlso dicionario(CodigoFuncionalidad).Values IsNot Nothing AndAlso dicionario(CodigoFuncionalidad).Values.Count > 0 Then
                Exit Sub
            End If

            Dim codigoCultura As String = If(CulturaSistema IsNot Nothing AndAlso
                                                                             Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                             CulturaSistema.Name,
                                                                             If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))

            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion
            With peticion
                .CodigoFuncionalidad = CodigoFuncionalidad
                .Cultura = codigoCultura
            End With
            Dim respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(peticion)

            If dicionario.ContainsKey(CodigoFuncionalidad) Then
                dicionario(CodigoFuncionalidad) = respuesta.Valores
            Else
                dicionario.Add(CodigoFuncionalidad, respuesta.Valores)
            End If
        End If
    End Sub
End Class
