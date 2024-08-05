Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio

Public Class BusquedaCliente
    Inherits UcBase

    Private WithEvents ucPopupBusquedaCliente As PopupBusquedaCliente
    Private WithEvents ucPopupBusquedaSubcliente As PopupBusquedaSubCliente
    Private WithEvents ucPopupBusquedaPuntoServicio As PopupBusquedaPuntoServicio

    Public Property ClienteSelecionado As Entidades.Cliente
        Get
            Return ViewState("Cliente")
        End Get
        Set(value As Entidades.Cliente)
            ViewState("ClienteSelecionado") = value
            HabilitaSubcliente(value IsNot Nothing)
            NotificarControleAtualizado("Cliente")
        End Set
    End Property

    Public Property SubclienteSelecionado As Entidades.Subcliente
        Get
            Return ViewState("Subcliente")
        End Get
        Set(value As Entidades.Subcliente)
            ViewState("SubclienteSelecionado") = value
            HabilitaPuntoServicio(value IsNot Nothing)
            NotificarControleAtualizado("Subcliente")
        End Set
    End Property

    Public Property PuntoServicioSelecionado As Entidades.PuntoServicio
        Get
            Return ViewState("PuntoServicio")
        End Get
        Set(value As Entidades.PuntoServicio)
            ViewState("PuntoServicioSelecionado") = value
            NotificarControleAtualizado("PuntoServicio")
        End Set
    End Property

    Protected Overrides Sub Inicializar()

        ucPopupBusquedaCliente = LoadControl("~/Controles/PopupBusquedaCliente.ascx")
        popupBuscaCliente.PopupBase = ucPopupBusquedaCliente

        ucPopupBusquedaSubcliente = LoadControl("~/Controles/PopupBusquedaSubCliente.ascx")
        popupBuscaSubcliente.PopupBase = ucPopupBusquedaSubcliente

        ucPopupBusquedaPuntoServicio = LoadControl("~/Controles/PopupBusquedaPuntoServicio.ascx")
        popupBuscaPuntoServicio.PopupBase = ucPopupBusquedaPuntoServicio

    End Sub

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()

        lblCodigoCliente.Text = Tradutor.Traduzir("001_codigo")
        lblCodigoSubcliente.Text = Tradutor.Traduzir("001_codigo")
        lblCodigoPuntoServicio.Text = Tradutor.Traduzir("001_codigo")
    End Sub

    Private Sub ucBuscaCliente_Erro(sender As Object, e As ErroEventArgs) Handles ucPopupBusquedaCliente.Erro
        NotificarErro(e.Erro)
    End Sub

    Private Sub ucBuscaSubcliente_Erro(sender As Object, e As ErroEventArgs) Handles ucPopupBusquedaSubcliente.Erro
        NotificarErro(e.Erro)
    End Sub

    Private Sub ucPopupBusquedaPuntoServicio_Erro(sender As Object, e As ErroEventArgs) Handles ucPopupBusquedaPuntoServicio.Erro
        NotificarErro(e.Erro)
    End Sub

    Private Sub ucBuscaCliente_Fechado(sender As Object, e As PopupEventArgs) Handles ucPopupBusquedaCliente.Fechado
        Try
            If e.Resultado IsNot Nothing Then
                Dim cliente = DirectCast(e.Resultado, List(Of Entidades.Cliente)).First
                txtCodigoCliente.Text = cliente.CodCliente
                txtDescricaoCliente.Text = cliente.DesCliente
                txtDescricaoCliente.ToolTip = cliente.DesCliente
                ClienteSelecionado = cliente
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub ucBuscaSubcliente_Fechado(sender As Object, e As PopupEventArgs) Handles ucPopupBusquedaSubcliente.Fechado
        Try
            If e.Resultado IsNot Nothing Then
                Dim subCliente = DirectCast(e.Resultado, List(Of Entidades.Subcliente)).First
                txtCodigoSubcliente.Text = subCliente.CodSubcliente
                txtDescricaoSubcliente.Text = subCliente.DesSubcliente
                txtDescricaoSubcliente.ToolTip = subCliente.DesSubcliente
                SubclienteSelecionado = subCliente
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub ucPopupBusquedaPuntoServicio_Fechado(sender As Object, e As PopupEventArgs) Handles ucPopupBusquedaPuntoServicio.Fechado
        Try
            If e.Resultado IsNot Nothing Then
                Dim puntoServicio = DirectCast(e.Resultado, List(Of Entidades.PuntoServicio)).First
                txtCodigoPuntoServicio.Text = puntoServicio.CodPtoServicio
                txtDescricaoPuntoServicio.Text = puntoServicio.DesPtoServicio
                txtDescricaoPuntoServicio.ToolTip = puntoServicio.DesPtoServicio
                PuntoServicioSelecionado = puntoServicio
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub imbBuscaCliente_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbBuscaCliente.Click
        Try
            ucPopupBusquedaCliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtDescricaoCliente.Text)
            popupBuscaCliente.AbrirPopup()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub imbBuscaSubcliente_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbBuscaSubcliente.Click
        Try
            ucPopupBusquedaSubcliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtCodigoSubcliente.Text, txtDescricaoSubcliente.Text)
            popupBuscaSubcliente.AbrirPopup()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub imbBuscaPuntoServicio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbBuscaPuntoServicio.Click
        Try
            ucPopupBusquedaPuntoServicio.ConfigurarValorPadrao(txtCodigoSubcliente.Text, txtCodigoPuntoServicio.Text, txtDescricaoPuntoServicio.Text)
            popupBuscaPuntoServicio.AbrirPopup()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub txtCodigoCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoCliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodigoCliente.Text) Then
                Dim cliente = ucPopupBusquedaCliente.RecuperarClientes(txtCodigoCliente.Text, String.Empty)
                If cliente IsNot Nothing AndAlso cliente.Count > 0 Then
                    If cliente.Count = 1 Then
                        txtCodigoCliente.Text = cliente.FirstOrDefault.CodCliente
                        txtDescricaoCliente.Text = cliente.FirstOrDefault.DesCliente
                        txtDescricaoCliente.ToolTip = cliente.FirstOrDefault.DesCliente
                        ClienteSelecionado = cliente.FirstOrDefault
                    Else
                        txtDescricaoCliente.Text = String.Empty
                        ucPopupBusquedaCliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtDescricaoCliente.Text)
                        popupBuscaCliente.AbrirPopup()
                    End If
                    Return
                End If
            End If
            If ClienteSelecionado IsNot Nothing Then
                txtCodigoCliente.Text = String.Empty
                txtDescricaoCliente.Text = String.Empty
                ClienteSelecionado = Nothing
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub txtCodigoSubcliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoSubcliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodigoSubcliente.Text) Then
                Dim subcliente = ucPopupBusquedaSubcliente.RecuperarSubclientes(txtCodigoCliente.Text, txtCodigoSubcliente.Text, String.Empty)
                If subcliente IsNot Nothing AndAlso subcliente.Count > 0 Then
                    If subcliente.Count = 1 Then
                        txtCodigoSubcliente.Text = subcliente.FirstOrDefault.CodSubcliente
                        txtDescricaoSubcliente.Text = subcliente.FirstOrDefault.DesSubcliente
                        txtDescricaoSubcliente.ToolTip = subcliente.FirstOrDefault.DesSubcliente
                        SubclienteSelecionado = subcliente.FirstOrDefault
                    Else
                        txtDescricaoSubcliente.Text = String.Empty
                        ucPopupBusquedaSubcliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtCodigoSubcliente.Text, txtDescricaoSubcliente.Text)
                        popupBuscaSubcliente.AbrirPopup()
                    End If
                    Return
                End If
            End If
            If SubclienteSelecionado IsNot Nothing Then
                txtCodigoSubcliente.Text = String.Empty
                txtDescricaoSubcliente.Text = String.Empty
                SubclienteSelecionado = Nothing
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub txtCodigoPuntoServicio_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoPuntoServicio.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodigoPuntoServicio.Text) Then
                Dim puntoServicio = ucPopupBusquedaPuntoServicio.RecuperarPuntosServicio(txtCodigoSubcliente.Text, txtCodigoPuntoServicio.Text, String.Empty)
                If puntoServicio IsNot Nothing AndAlso puntoServicio.Count > 0 Then
                    If puntoServicio.Count = 1 Then
                        txtCodigoPuntoServicio.Text = puntoServicio.FirstOrDefault.CodPtoServicio
                        txtDescricaoPuntoServicio.Text = puntoServicio.FirstOrDefault.DesPtoServicio
                        txtDescricaoPuntoServicio.ToolTip = puntoServicio.FirstOrDefault.DesPtoServicio
                        PuntoServicioSelecionado = puntoServicio.FirstOrDefault
                        NotificarControleAtualizado("PuntoServicio")
                    Else
                        txtDescricaoPuntoServicio.Text = String.Empty
                        ucPopupBusquedaPuntoServicio.ConfigurarValorPadrao(txtCodigoSubcliente.Text, txtCodigoPuntoServicio.Text, txtDescricaoPuntoServicio.Text)
                        popupBuscaPuntoServicio.AbrirPopup()
                    End If
                End If
            Else
                If PuntoServicioSelecionado IsNot Nothing Then
                    txtCodigoPuntoServicio.Text = String.Empty
                    txtDescricaoPuntoServicio.Text = String.Empty
                    PuntoServicioSelecionado = Nothing
                End If
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Protected Sub txtDescricaoCliente_TextChanged(sender As Object, e As EventArgs) Handles txtDescricaoCliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDescricaoCliente.Text) Then
                Dim cliente = ucPopupBusquedaCliente.RecuperarClientes(String.Empty, txtDescricaoCliente.Text)
                If cliente IsNot Nothing AndAlso cliente.Count = 1 Then
                    txtCodigoCliente.Text = cliente.FirstOrDefault.CodCliente
                    txtDescricaoCliente.Text = cliente.FirstOrDefault.DesCliente
                    txtDescricaoCliente.ToolTip = cliente.FirstOrDefault.DesCliente
                    ClienteSelecionado = cliente.FirstOrDefault
                    HabilitaSubcliente(True)
                    NotificarControleAtualizado("Cliente")
                ElseIf cliente IsNot Nothing AndAlso cliente.Count > 1 Then
                    txtCodigoCliente.Text = String.Empty
                    ucPopupBusquedaCliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtDescricaoCliente.Text)
                    popupBuscaCliente.AbrirPopup()
                Else
                    txtCodigoCliente.Text = String.Empty
                    txtDescricaoCliente.Text = String.Empty
                    ClienteSelecionado = Nothing
                    HabilitaSubcliente(False)
                    NotificarControleAtualizado("Cliente")
                End If
            Else
                HabilitaSubcliente(False)
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Protected Sub txtDescricaoSubcliente_TextChanged(sender As Object, e As EventArgs) Handles txtDescricaoSubcliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDescricaoSubcliente.Text) Then
                Dim subCliente = ucPopupBusquedaSubcliente.RecuperarSubclientes(txtCodigoCliente.Text, String.Empty, txtDescricaoSubcliente.Text)
                If subCliente IsNot Nothing AndAlso subCliente.Count = 1 Then
                    txtCodigoSubcliente.Text = subCliente.FirstOrDefault.CodSubCliente
                    txtDescricaoSubcliente.Text = subCliente.FirstOrDefault.DesSubCliente
                    txtDescricaoSubcliente.ToolTip = subCliente.FirstOrDefault.DesSubCliente
                    SubclienteSelecionado = subCliente.FirstOrDefault
                    NotificarControleAtualizado("Subcliente")
                ElseIf subCliente IsNot Nothing AndAlso subCliente.Count > 1 Then
                    txtCodigoSubcliente.Text = String.Empty
                    ucPopupBusquedaSubcliente.ConfigurarValorPadrao(txtCodigoCliente.Text, txtCodigoSubcliente.Text, txtDescricaoSubcliente.Text)
                    popupBuscaSubcliente.AbrirPopup()
                Else
                    txtCodigoSubcliente.Text = String.Empty
                    txtDescricaoSubcliente.Text = String.Empty
                    SubclienteSelecionado = Nothing
                    NotificarControleAtualizado("Subcliente")
                End If
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Protected Sub txtDescricaoPuntoServicio_TextChanged(sender As Object, e As EventArgs) Handles txtDescricaoPuntoServicio.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDescricaoPuntoServicio.Text) Then
                Dim puntoServicio = ucPopupBusquedaPuntoServicio.RecuperarPuntosServicio(txtCodigoSubcliente.Text, String.Empty, txtDescricaoPuntoServicio.Text)
                If puntoServicio IsNot Nothing AndAlso puntoServicio.Count = 1 Then
                    txtCodigoPuntoServicio.Text = puntoServicio.FirstOrDefault.CodPtoServicio
                    txtDescricaoPuntoServicio.Text = puntoServicio.FirstOrDefault.DesPtoServicio
                    txtDescricaoPuntoServicio.ToolTip = puntoServicio.FirstOrDefault.DesPtoServicio
                    PuntoServicioSelecionado = puntoServicio.FirstOrDefault
                    NotificarControleAtualizado("PuntoServicio")
                ElseIf puntoServicio IsNot Nothing AndAlso puntoServicio.Count > 1 Then
                    txtCodigoPuntoServicio.Text = String.Empty
                    ucPopupBusquedaPuntoServicio.ConfigurarValorPadrao(txtCodigoSubcliente.Text, txtCodigoPuntoServicio.Text, txtDescricaoPuntoServicio.Text)
                    popupBuscaPuntoServicio.AbrirPopup()

                Else
                    txtCodigoPuntoServicio.Text = String.Empty
                    txtDescricaoPuntoServicio.Text = String.Empty
                    PuntoServicioSelecionado = Nothing
                    NotificarControleAtualizado("PuntoServicio")
                End If
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Private Sub HabilitaSubcliente(habilita As Boolean)
        txtCodigoSubcliente.Enabled = habilita
        txtDescricaoSubcliente.Enabled = habilita
        imbBuscaSubcliente.Enabled = habilita
    End Sub

    Private Sub HabilitaPuntoServicio(habilita As Boolean)
        txtCodigoPuntoServicio.Enabled = habilita
        txtDescricaoPuntoServicio.Enabled = habilita
        imbBuscaPuntoServicio.Enabled = habilita
    End Sub

End Class