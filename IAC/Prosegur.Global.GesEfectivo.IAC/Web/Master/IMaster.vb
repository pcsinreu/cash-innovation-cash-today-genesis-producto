Public MustInherit Class IMaster
    Inherits System.Web.UI.MasterPage

    Public Overridable ReadOnly Property ControleErro As Erro
        Get
            Return Nothing
        End Get
    End Property

    Public MustOverride Sub ExibirModal(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True, Optional disparaEvento As Boolean = False, Optional botao As String = "")

End Class
