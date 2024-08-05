Imports Prosegur.Global.GesEfectivo.Conteo.ContractoServicio
Imports SincronizarATM = Prosegur.Global.GesEfectivo.ATM.ContractoServicio.Integracion
Imports Prosegur.Global.GesEfectivo.Salidas.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration

Public Class SincronizarDivisasResultado
    Inherits Base

    Dim tabela As Table = Nothing
    Dim linha As TableRow = Nothing
    Dim coluna As TableCell = Nothing


#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DIVISAS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try

            CarregarResultado()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        lblTituloPagina.Text = Traduzir("005_titulo_resultado_sincronizacion")
        lblTituloConteo.Text = Traduzir("005_titulo_conteo")
        lblTituloAtm.Text = Traduzir("005_titulo_Atm")
        lblTituloSalidas.Text = Traduzir("005_titulo_salidas")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Property DivisasConteo As SincronizarDivisasConteo.Respuesta
        Get
            Return Session("DivisasConteo")
        End Get
        Set(ByVal value As SincronizarDivisasConteo.Respuesta)
            Session("DivisasConteo") = value
        End Set
    End Property

    'Public Property DivisasATM As SincronizarATM.SincronizarDivisasATM.Respuesta
    '    Get
    '        Return Session("DivisasATM")
    '    End Get
    '    Set(ByVal value As SincronizarATM.SincronizarDivisasATM.Respuesta)
    '        Session("DivisasATM") = value
    '    End Set
    'End Property

    Public Property DivisasSalidas As Divisa.SincronizarDivisasSalidas.Respuesta
        Get
            Return Session("DivisasSalidas")
        End Get
        Set(ByVal value As Divisa.SincronizarDivisasSalidas.Respuesta)
            Session("DivisasSalidas") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    Private Sub CarregarResultado()

        If DivisasConteo IsNot Nothing Then

            ResultadoConteo.CarregarDados(DivisasConteo, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio") & "Conteo/InterfaceLegado.asmx")
        Else
            pnlConteoPrincipal.Visible = False
        End If

        If DivisasSalidas IsNot Nothing Then

            ResultadoSalidas.CarregarDados(DivisasSalidas, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio") & "Salidas/Integracion.asmx")
        Else
            pnlSalidasPrincipal.Visible = False
        End If

        'If DivisasATM IsNot Nothing Then

        '    ResultadoAtm.CarregarDados(DivisasATM, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio") & "ATM/Integracion.asmx")

        'Else
        '    pnlAtmPrincipal.Visible = False
        'End If

    End Sub

#End Region

End Class