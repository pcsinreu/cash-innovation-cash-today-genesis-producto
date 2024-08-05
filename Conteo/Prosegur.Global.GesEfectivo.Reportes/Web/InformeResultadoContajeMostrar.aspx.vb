Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class InformeResultadoContajeMostrar
    Inherits Base


#Region "[PROPRIEDADES]"

    Public ReadOnly Property Remesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa
        Get
            Return DirectCast(Session("InformeResultadoContaje"), ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)
        End Get
    End Property
    ''' <summary>
    ''' Nome Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NomeDocumento() As String
        Get
            Return Session("ResultadoContajeNomeDocumento")
        End Get
        Set(value As String)
            Session("ResultadoContajeNomeDocumento") = value
        End Set
    End Property

#End Region

#Region "[ATRIBUTOS]"

    ' Define a variável para receber tipo o relatório será exibido
    Private _FormatoSalida As ContractoServ.Enumeradores.eFormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.PDF

    ' Define a variável para receber o nome do arquivo
    Private _NomeArquivo As String = String.Empty

#End Region

#Region "[MÉTODOS BASE]"

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    ''' <summary>
    ''' Configura tab index da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parâmetros da base
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 10/12/2009 Alterado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.INFORME_RESULTADO_CONTAJE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    ''' <summary>
    ''' Inicializa página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 29/06/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub AdicionarScripts()

    End Sub

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Traduzir os controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 10/12/2009 Alterado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Me.Page.Title = Traduzir("005_lbl_titulo_busqueda")
        Me.lblTitulo.Text = Traduzir("005_lbl_titulo_busqueda")
        lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("NomeArquivo") IsNot Nothing) Then
            ' Recupera o nome do arquivo
            _NomeArquivo = Request.QueryString("NomeArquivo")
        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub CarregarDados()

        ' Cria o DataSet tipado
        Dim dsResultadoContaje As New InformeResultadoContaje

        ' Popula o data set com os dados recuperados do objeto
        dsResultadoContaje.PopularInformeResultadoContaje(Remesa)
        ' Passa o título do relatório
        rptInformeResultadoContaje.TituloRelatorio = Traduzir("005_titulo_pagina")
        ' Passa o tipo do relatório que será exibido
        rptInformeResultadoContaje.TipoRelatorio = _FormatoSalida
        ' Carrega os dados do relatório
        rptInformeResultadoContaje.FonteDados = dsResultadoContaje
        ' Carrega o reporte de acordo com o formato de saída
        rptInformeResultadoContaje.Report = String.Format("crInformeResultadoContaje.rpt", _FormatoSalida.ToString)
        ' Passa o nome do arquivo que deverá ser gerado
        rptInformeResultadoContaje.NomeArquivo = NomeDocumento

    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

        Try
            ' Recuperar Parametros
            RecuperarParametros()

            ' Carrega os dados do relatório
            CarregarDados()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

End Class