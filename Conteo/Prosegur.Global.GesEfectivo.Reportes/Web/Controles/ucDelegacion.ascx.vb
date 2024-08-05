Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

Public Class ucDelegacion
    Inherits UserControlBase

    Public Event SelectedChanged As EventHandler
#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RegistrarScripts()
        TraduzirControles()

    End Sub

#End Region

#Region "[PROPRIEDADES]"


    Public ReadOnly Property TodosSelecionado As Boolean
        Get
            Dim selecionados = ObtemSelecionados()
            If selecionados IsNot Nothing AndAlso selecionados.Count = DelegacionesCarregadas.Count Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public Property MultiSelecao As Boolean
        Get
            Return ViewState("MultiSelecao")
        End Get
        Set(value As Boolean)
            ViewState("MultiSelecao") = value
        End Set
    End Property

    Public Property CampoObrigatorio As Boolean
        Get
            Return ViewState("CampoObrigatorio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorio") = value
        End Set
    End Property

    Private Property DelegacionesCarregadas As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
        Get
            Return ViewState("DelegacionesCarregadas")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion)
            ViewState("DelegacionesCarregadas") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    Public Function RecuperarSelecionado(Optional ValidarDatosRellenados As Boolean = True) As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
        Dim DelegacionesSelecionados As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = ObtemSelecionados()

        If ValidarDatosRellenados AndAlso CampoObrigatorio AndAlso (DelegacionesSelecionados Is Nothing OrElse DelegacionesSelecionados.Count = 0) Then
            csvDelegaciones.IsValid = False
            Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), Traduzir("025_lbldelegacion")))
            Return Nothing
        End If

        Return DelegacionesSelecionados
    End Function

    Public Sub PopularControle()

        PopularDelegaciones(RecuperaDelegacion())

    End Sub

    Private Sub PopularDelegaciones(Delegaciones As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion)

        If MultiSelecao Then
            rblDelegaciones.Visible = False
            cblDelegaciones.Visible = True

            If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 1 Then
                chkTodos.Visible = True
                chkTodos.Text = Traduzir("gen_todos")
            End If

            cblDelegaciones.Items.Clear()
            cblDelegaciones.AppendDataBoundItems = True
            cblDelegaciones.Items.Clear()


            Delegaciones.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each item In Delegaciones
                cblDelegaciones.Items.Add(New ListItem(String.Format("{0} - {1}", item.Codigo, item.Descripcion),
                                                     item.Codigo))
            Next

        Else
            rblDelegaciones.Visible = True
            cblDelegaciones.Visible = False

            rblDelegaciones.AppendDataBoundItems = True
            rblDelegaciones.Items.Clear()

            Delegaciones.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each item In Delegaciones
                rblDelegaciones.Items.Add(New ListItem(String.Format("{0} - {1}", item.Codigo, item.Descripcion),
                                                     item.Codigo))
            Next

        End If
        DelegacionesCarregadas = Delegaciones
    End Sub

    Public Sub PopularControle(DelegacionesSelecionadas As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion, _
                                Optional DelegacionesUsuario As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = Nothing)
        If DelegacionesUsuario Is Nothing Then
            PopularControle()
        Else
            PopularDelegaciones(DelegacionesUsuario)
        End If

        If DelegacionesSelecionadas IsNot Nothing AndAlso DelegacionesSelecionadas.Count > 0 Then

            If MultiSelecao Then
                For Each item As ListItem In cblDelegaciones.Items
                    Dim value = item.Value
                    Dim existe = From p In DelegacionesSelecionadas
                                 Where p.Codigo = value

                    If existe IsNot Nothing AndAlso existe.Count > 0 Then
                        item.Selected = True
                    End If
                Next
            Else

                For Each item As ListItem In rblDelegaciones.Items
                    Dim value = item.Value
                    Dim existe = From p In DelegacionesSelecionadas
                                 Where p.Codigo = value

                    If existe IsNot Nothing AndAlso existe.Count > 0 Then
                        item.Selected = True
                        Exit For
                    End If

                Next

            End If

        End If

        If MultiSelecao Then
            cblDelegaciones_SelectedIndexChanged(Nothing, Nothing)
        Else
            rblDelegaciones_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    Public Sub PopularControle(SelecionarTodos As Boolean, Optional DelegacionesUsuario As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = Nothing)
        If DelegacionesUsuario Is Nothing Then
            PopularControle()
        Else
            PopularDelegaciones(DelegacionesUsuario)
        End If
        If SelecionarTodos Then
            If MultiSelecao Then
                For Each item As ListItem In cblDelegaciones.Items
                    item.Selected = True
                Next
                chkTodos.Checked = True
            Else

                For Each item As ListItem In rblDelegaciones.Items
                    item.Selected = True
                    Exit For
                Next

            End If
        End If

        If MultiSelecao Then
            cblDelegaciones_SelectedIndexChanged(Nothing, Nothing)
        Else
            rblDelegaciones_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Function RecuperaDelegacion() As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
        'Pega os valores da delegação da tabela

        Dim objProxyUtilidad As New ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta
        objRespuesta = objProxyUtilidad.GetComboDelegaciones()

        If Not Util.TratarRetornoServico(objRespuesta.CodigoError, objRespuesta.MensajeError, ContractoServ.Login.ResultadoOperacionLoginLocal.Error) Then
            Me.EnviarMensagemErro(objRespuesta.MensajeError)
            Return Nothing
        End If

        Return objRespuesta.Delegaciones
    End Function

    Private Sub RegistrarScripts()
        If MultiSelecao Then
            Dim scripts = String.Format("SelecionarTodosCheckBoxList('{0}','{1}');", _
                                         cblDelegaciones.ClientID, chkTodos.ClientID)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "SelecionarTodos" & Me.ClientID, scripts, True)
        End If

    End Sub

    Private Sub TraduzirControles()
        lblDelegacion.Text = Traduzir("025_lbldelegacion")
    End Sub

    Private Function ObtemSelecionados() As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
        Dim result As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = Nothing

        If DelegacionesCarregadas Is Nothing OrElse DelegacionesCarregadas.Count = 0 Then Return Nothing

        If MultiSelecao Then
            Dim selecionados = (From item In cblDelegaciones.Items.Cast(Of ListItem)()
                                           Where item.Selected
                                           Select New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion _
                                           With {.Codigo = item.Value, _
                                                 .Descripcion = item.Text}).ToList()

            Dim resultado = (From p In DelegacionesCarregadas
                            Join s In selecionados On s.Codigo Equals p.Codigo
                            Select p).ToList()
            result = New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
            result.AddRange(resultado)

        Else

            Dim selecionados = (From item In rblDelegaciones.Items.Cast(Of ListItem)()
                                          Where item.Selected
                                          Select New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion _
                                          With {.Codigo = item.Value, _
                                                .Descripcion = item.Text}).ToList()

            Dim resultado = (From p In DelegacionesCarregadas
                            Join s In selecionados On s.Codigo Equals p.Codigo
                            Select p).ToList()
            result = New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
            result.AddRange(resultado)

        End If

        Return result
    End Function

    Public Sub Limpar()
        If MultiSelecao Then
            Dim selecionados = From p In cblDelegaciones.Items.Cast(Of ListItem)()
                               Where p.Selected = True
                               Select p

            For Each item In selecionados
                item.Selected = False
            Next

        Else
            rblDelegaciones.SelectedItem.Selected = False
        End If


    End Sub

    Public Sub CambiarEstadoDelegacion(selected As Boolean, codDelegacion As String, multiSelecao As Boolean)
        If multiSelecao Then
            cblDelegaciones.Items.FindByValue(codDelegacion).Selected = selected
            chkTodos.Checked = TodosSelecionado
        Else
            rblDelegaciones.Items.FindByValue(codDelegacion).Selected = selected
        End If
    End Sub

    Public Sub AutoPostBack(postBack As Boolean)
        Me.chkTodos.AutoPostBack = postBack
        Me.cblDelegaciones.AutoPostBack = postBack
        Me.rblDelegaciones.AutoPostBack = postBack
    End Sub

    Protected Sub cblDelegaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblDelegaciones.SelectedIndexChanged
        ' verifica se todos os itens estão selecionados
        If cblDelegaciones.Items.Cast(Of ListItem)().Where(Function(l) l.Selected).Count = cblDelegaciones.Items.Count Then
            Me.chkTodos.Checked = True
        Else
            Me.chkTodos.Checked = False
        End If

        RaiseEvent SelectedChanged(Me, e)
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodos.CheckedChanged
        If Me.chkTodos.Checked Then
            For Each item In cblDelegaciones.Items.Cast(Of ListItem)().Where(Function(l) Not l.Selected)
                item.Selected = True
            Next
        Else
            Me.cblDelegaciones.ClearSelection()
        End If

        RaiseEvent SelectedChanged(Me, e)
    End Sub

    Protected Sub rblDelegaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblDelegaciones.SelectedIndexChanged
        ' verifica se todos os itens estão selecionados
        If rblDelegaciones.Items.Cast(Of ListItem)().Where(Function(l) l.Selected).Count = cblDelegaciones.Items.Count Then
            Me.chkTodos.Checked = True
        Else
            Me.chkTodos.Checked = False
        End If

        RaiseEvent SelectedChanged(Me, e)
    End Sub

#End Region

#Region "[CONTRUTORES]"

    Public Sub New()
        MultiSelecao = True
        CampoObrigatorio = True
    End Sub

#End Region

End Class