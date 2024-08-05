Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class BilletajeSucursalMostrar
    Inherits Base

#Region "[ATRIBUTOS]"

    ' Define a variável para receber tipo o relatório será exibido
    Private _FormatoSalida As ContractoServ.Enumeradores.eFormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV

    ' Define a variável para receber o nome do arquivo
    Private _NomeArquivo As String = String.Empty

    ' Define a variável para receber a planta informada pelo usuário
    Private _Planta As String = String.Empty

    ' Define a variável para receber o cliente informado pelo usuário
    Private _Cliente As String = String.Empty

    ' Define a variável para receber a data inicial informada pelo usuário
    Private _DataInicial As DateTime = DateTime.MinValue

    ' Define a variável para receber a data final informada pelo usuário
    Private _DataFinal As DateTime = DateTime.MinValue

    ' Define a variável para receber o processo informado pelo usuário
    Private _Processo As String = String.Empty

    ' Define a variável para receber o tipo da data
    Private _TipoData As Integer = 0

#End Region

#Region "[MÉTODOS BASE]"

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    ''' <summary>
    ''' Configura tab index da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
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
        MyBase.PaginaAtual = Enumeradores.eTelas.BILLETAJE_SUCURSAL
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
    ''' [magnum.oliveira] 15/06/2009 Criado
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
    ''' [magnum.oliveira] 10/12/2009 Alterado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Me.Page.Title = Traduzir("003_lbl_titulo_busqueda")
        Me.lblTitulo.Text = Traduzir("003_lbl_titulo_busqueda")
        lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("Exibir") IsNot Nothing) Then
            ' Recupera a opção escolhida para exibir o relatório
            _FormatoSalida = Request.QueryString("Exibir")
        End If

        ' Se existir
        If (Request.QueryString("NomeArquivo") IsNot Nothing) Then
            ' Recupera o nome do arquivo
            _NomeArquivo = Request.QueryString("NomeArquivo")
        End If

        ' Se existir
        If (Request.QueryString("Planta") IsNot Nothing) Then
            ' Recupera a planta
            _Planta = Request.QueryString("Planta")
        End If

        ' Se existir
        If (Request.QueryString("Cliente") IsNot Nothing) Then
            'Recupera o cliente
            _Cliente = Request.QueryString("Cliente")
        End If

        ' Se existir
        If (Request.QueryString("DataInicial") IsNot Nothing) Then
            ' Recupera a data inicial
            _DataInicial = Request.QueryString("DataInicial")
        End If

        ' Se existir
        If (Request.QueryString("DataFinal") IsNot Nothing) Then
            ' Recupera a data final
            _DataFinal = Request.QueryString("DataFinal")
        End If

        ' Se existir
        If (Request.QueryString("Processo") IsNot Nothing) Then
            ' Recupera o processo
            _Processo = Request.QueryString("Processo")
        End If

        ' Se existir
        If (Request.QueryString("TipoData") IsNot Nothing) Then
            ' Recupera o tipo da data
            _TipoData = Request.QueryString("TipoData")
        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/06/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub CarregarDados()

        ' Verifica o formato de saída
        If (_FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
            'Envia os dados para download
            EnviarDownload(DirectCast(Session("objBilletajesSucursais"), ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion), _NomeArquivo)
        Else
            ' Cria o DataSet tipado
            Dim dsBilletajeSucursal As New BilletajesSucursais
            ' Cria a linha de parametros
            Dim drParametro As BilletajesSucursais.ParametroRow = dsBilletajeSucursal.Parametro.NewParametroRow

            ' Adiciona os parametros
            drParametro.Recuento = _Processo
            drParametro.FechaInicial = _DataInicial
            drParametro.FechaFinal = _DataFinal
            drParametro.TipoFecha = _TipoData
            drParametro.Planta = _Planta
            drParametro.Cliente = _Cliente
            drParametro.Sustituir_F22_Por_OidRemesaOri = Util.VerificarParametroNoWebConfig("Sustituir_F22_Por_OidRemesaOri")
            drParametro.Exhibir_CodSubCliente = Util.VerificarParametroNoWebConfig("Exhibir_CodSubCliente")
            drParametro.CodigoCalidad = Traduzir("rpt_003_txt_lbl_codido_calidad")

            ' Adiciona o parametro na tabela de parametros
            dsBilletajeSucursal.Parametro.Rows.Add(drParametro)
            ' Popula o data set com os dados recuperados do objeto
            dsBilletajeSucursal.PopularBilletajeSucursal(DirectCast(Session("objBilletajesSucursais"), ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion))
            ' Passa o título do relatório
            rptBilletajePorSucursal.TituloRelatorio = Traduzir("003_titulo_pagina")
            ' Passa o tipo do relatório que será exibido
            rptBilletajePorSucursal.TipoRelatorio = _FormatoSalida
            ' Carrega os dados do relatório
            rptBilletajePorSucursal.FonteDados = dsBilletajeSucursal
            ' Carrega o reporte de acordo com o formato de saída
            rptBilletajePorSucursal.Report = String.Format("crBilletajeSucursal{0}.rpt", _FormatoSalida.ToString)
            ' Passa o nome do arquivo que deverá ser gerado
            rptBilletajePorSucursal.NomeArquivo = _NomeArquivo

        End If

    End Sub

    ''' <summary>
    ''' Função de conversão de Ilist para CSV
    ''' </summary>
    ''' <param name="objDados">Objeto com os dados retornados do banco</param>
    ''' <param name="sDelimitadorColunas">Delimitador da coluna</param>
    ''' <param name="sDelimitadorRegistros">Delimitador da linha</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' Em 22/07/2009 por (Carlos Bomtempo/Magnum Oliveira)
    ''' </history>
    Private Shared Function GerarArquivoCSV(ByRef objDados As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion, _
    Optional sDelimitadorColunas As String = ";", _
    Optional sDelimitadorRegistros As String = vbNewLine) As String

        ' se a chave SeparadorColumnas existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorColumnas") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Length() > 0) Then
            sDelimitadorColunas = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Substring(0, 1)
        End If

        Dim sRegistro As String = ""
        Dim objCSV As New StringBuilder("")

        'Carrega do arquivo Web.Config os valores das chaves Sustituir_F22_Por_OidRemesaOri e Exhibir_CodSubCliente
        Dim sustituirF22PorOidRemesaOri As Boolean = Util.VerificarParametroNoWebConfig("Sustituir_F22_Por_OidRemesaOri")
        Dim exhibirCodSubCliente As Boolean = Util.VerificarParametroNoWebConfig("Exhibir_CodSubCliente")

        ' Recupera a cultura correte
        Dim culturaCorrente = Threading.Thread.CurrentThread.CurrentCulture

        ' Joga o título do campo no registro
        If sRegistro <> "" Then sRegistro &= sDelimitadorColunas
        sRegistro &= Traduzir("003_csv_lbl_recuento").ToUpper & sDelimitadorColunas             ' Recuento
        sRegistro &= Traduzir("003_csv_lbl_fecha").ToUpper & sDelimitadorColunas                ' Fecha
        sRegistro &= Traduzir("003_csv_lbl_letra").ToUpper & sDelimitadorColunas                ' Letra

        If (sustituirF22PorOidRemesaOri) Then
            sRegistro &= Traduzir("003_csv_lbl_oidRemesaOri").ToUpper & sDelimitadorColunas     ' OidRemesaOri
        Else
            sRegistro &= Traduzir("003_csv_lbl_f22").ToUpper & sDelimitadorColunas              ' F22
        End If

        If (exhibirCodSubCliente) Then
            sRegistro &= Traduzir("003_csv_lbl_codSubCliente").ToUpper & sDelimitadorColunas    ' CodSubCliente
        End If

        sRegistro &= Traduzir("003_csv_lbl_estacion").ToUpper & sDelimitadorColunas             ' Estacion
        sRegistro &= Traduzir("003_csv_lbl_descricion_estacion").ToUpper & sDelimitadorColunas  ' Descricion Estacion
        sRegistro &= Traduzir("003_csv_lbl_descricion_divisa").ToUpper & sDelimitadorColunas    ' Descricion Divisa
        sRegistro &= Traduzir("003_csv_lbl_medio_pago").ToUpper & sDelimitadorColunas           ' Medio Pago
        sRegistro &= Traduzir("003_csv_lbl_unidad").ToUpper & sDelimitadorColunas               ' Unidad
        sRegistro &= Traduzir("003_csv_lbl_calidad").ToUpper & sDelimitadorColunas               ' Calidad
        sRegistro &= Traduzir("003_csv_lbl_multiplicador").ToUpper & sDelimitadorColunas        ' Multiplicador
        sRegistro &= Traduzir("003_csv_lbl_cantidad").ToUpper & sDelimitadorColunas             ' Cantidad
        sRegistro &= Traduzir("003_csv_lbl_valor").ToUpper                                      ' Valor        

        ' Adiciona no CSV a linha de cabeçalho
        objCSV.Append(sRegistro)
        objCSV.Append(sDelimitadorRegistros)
        sRegistro = ""

        ' Define a cultura corrente
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(Constantes.Cultura.EN_US)

        ' Loop pelos registros para copiá-los para a planilha
        For Each objeto As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSV In (From d In objDados Order By d.F22, d.Estacion, d.CodigoDivisa, d.MedioPago, d.Multiplicador Descending, d.EsBillete)

            ' Escreve o registro conforme veio no banco
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Recuento.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Fecha.ToString("dd-MM-yyyy")) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Letra.Trim()) & sDelimitadorColunas

            If (sustituirF22PorOidRemesaOri) Then
                sRegistro &= Util.ConfiguraCalificadorTexto(objeto.OidRemesaOri.Trim()) & sDelimitadorColunas
            Else
                sRegistro &= Util.ConfiguraCalificadorTexto(objeto.F22.Trim()) & sDelimitadorColunas
            End If

            If (exhibirCodSubCliente) Then
                sRegistro &= Util.ConfiguraCalificadorTexto(objeto.CodSubCliente.Trim()) & sDelimitadorColunas
            End If

            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Estacion.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionEstacion.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionDivisa.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(If(objeto.MedioPago.ToLower.Equals(Constantes.DESCRICAO_EFETIVO), _
                             If(objeto.EsBillete, Traduzir("lbl_billete"), Traduzir("lbl_moneda")), _
                             objeto.MedioPago.Trim())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Unidad.Trim()) & sDelimitadorColunas
            sRegistro &= If(Not String.IsNullOrEmpty(objeto.CodCalidad), Util.ConfiguraCalificadorTexto(Traduzir("003_csv_lbl_codigo_calidad")), String.Empty) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.Multiplicador.ToString("F"))) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.Cantidad.ToString("F"))) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.Valor.ToString("F")))

            ' Adiciona no CSV a linha do registro de dados
            objCSV.Append(sRegistro)
            objCSV.Append(sDelimitadorRegistros)
            sRegistro = ""

        Next

        ' Define a cultura corrente
        Threading.Thread.CurrentThread.CurrentCulture = culturaCorrente

        ' Retorna o string do arquivo CSV gerado
        Return objCSV.ToString()

    End Function

    ''' <summary>
    ''' Gera um relatório em formato CSV e envia para download (disponível apenas para aplicações ASP.NET)
    ''' </summary>
    ''' <param name="objDados"></param>
    ''' <param name="sNomeArquivo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' Em 22/07/2009 por Magnum Oliveira
    ''' </history>
    Public Sub EnviarDownload(ByRef objDados As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion, _
                              sNomeArquivo As String)

        ' Gera um arquivo CSV com os dados do grid
        Dim sCSV As String = GerarArquivoCSV(objDados)
        ' Envia para o usuário o string do CSV
        Util.DescarregarCSV(sCSV, sNomeArquivo & ".csv")

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