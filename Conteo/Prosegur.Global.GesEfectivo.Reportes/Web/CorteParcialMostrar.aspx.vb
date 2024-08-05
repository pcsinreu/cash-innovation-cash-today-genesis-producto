Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class CorteParcialMostrar
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
    ''' [magnum.oliveira] 29/07/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    ''' <summary>
    ''' Configura tab index da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 29/07/2009 Criado
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
        MyBase.PaginaAtual = Enumeradores.eTelas.CORTE_PARCIAL
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
    ''' [magnum.oliveira] 29/07/2009 Criado
    ''' [magnum.oliveira] 10/12/2009 Alterado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Me.Page.Title = Traduzir("004_lbl_titulo_busqueda")
        Me.lblTitulo.Text = Traduzir("004_lbl_titulo_busqueda")
        lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 29/07/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("Exibir") IsNot Nothing) Then
            ' Recupera a opção escolhida para exibir o relatório
            _FormatoSalida = Request.QueryString("Exibir")
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
        If (Request.QueryString("NomeArquivo") IsNot Nothing) Then
            ' Recupera o nome do arquivo
            _NomeArquivo = Request.QueryString("NomeArquivo")
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
    ''' [magnum.oliveira] 29/07/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub CarregarDados()

        ' Verifica o formato de saída
        If (_FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
            'Envia os dados para download
            EnviarDownload(DirectCast(Session("objCortesParciais"), ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion), _NomeArquivo)
        Else
            ' Cria o DataSet tipado
            Dim dsCortesParciais As New CortesParciais

            ' Cria a linha de parametros
            Dim drParametro As CortesParciais.ParametroRow = dsCortesParciais.Parametro.NewParametroRow

            ' Adiciona os parametros
            drParametro.Recuento = _Processo
            drParametro.FechaIni = _DataInicial
            drParametro.FechaFin = _DataFinal
            drParametro.Planta = _Planta
            drParametro.FechaTipo = _TipoData
            drParametro.Cliente = _Cliente
            drParametro.Sustituir_F22_Por_OidRemesaOri = Util.VerificarParametroNoWebConfig("Sustituir_F22_Por_OidRemesaOri")
            drParametro.Exhibir_CodSubCliente = Util.VerificarParametroNoWebConfig("Exhibir_CodSubCliente")

            ' Adiciona o parametro na tabela de parametros
            dsCortesParciais.Parametro.Rows.Add(drParametro)

            ' Popula o data set com os dados recuperados do objeto
            dsCortesParciais.PopularCorteParcial(DirectCast(Session("objCortesParciais"), ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF))

            ' Passa o título do relatório
            rptCorteParcial.TituloRelatorio = Traduzir("004_titulo_pagina")
            ' Passa o tipo do relatório que será exibido
            rptCorteParcial.TipoRelatorio = _FormatoSalida
            ' Carrega os dados do relatório
            rptCorteParcial.FonteDados = dsCortesParciais
            ' Carrega o reporte de acordo com o formato de saída
            rptCorteParcial.Report = String.Format("crCorteParcial{0}.rpt", _FormatoSalida.ToString)
            ' Passa o nome do arquivo que deverá ser gerado
            rptCorteParcial.NomeArquivo = _NomeArquivo

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
    ''' Em 29/07/2009 por (Carlos Bomtempo/Magnum Oliveira)
    ''' </history>
    Private Shared Function GerarArquivoCSV(ByRef objDados As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion, _
    Optional sDelimitadorColunas As String = ";", _
    Optional sDelimitadorRegistros As String = vbNewLine, _
    Optional sDelimitadorFalsos As String = "#") As String

        ' se a chave SeparadorColumnas existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorColumnas") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Length() > 0) Then
            sDelimitadorColunas = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Substring(0, 1)
        End If

        ' se a chave SeparadorFalsos existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorFalsos") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Length() > 0) Then
            sDelimitadorFalsos = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Substring(0, 1)
        End If

        'Carrega do arquivo Web.Config os valores das chaves Sustituir_F22_Por_OidRemesaOri e Exhibir_CodSubCliente
        Dim sustituirF22PorOidRemesaOri As Boolean = Util.VerificarParametroNoWebConfig("Sustituir_F22_Por_OidRemesaOri")
        Dim exhibirCodSubCliente As Boolean = Util.VerificarParametroNoWebConfig("Exhibir_CodSubCliente")

        Dim sRegistro As String = String.Empty
        Dim objCSV As New StringBuilder(String.Empty)

        ' Recupera a cultura correte
        Dim culturaCorrente = Threading.Thread.CurrentThread.CurrentCulture

        ' Joga o título do campo no registro
        If Not String.IsNullOrEmpty(sRegistro) Then sRegistro &= sDelimitadorColunas
        sRegistro &= Traduzir("004_csv_lbl_recuento").ToUpper & sDelimitadorColunas                         ' Recuento
        sRegistro &= Traduzir("004_csv_lbl_fecha").ToUpper & sDelimitadorColunas                            ' Fecha
        sRegistro &= Traduzir("004_csv_lbl_letra").ToUpper & sDelimitadorColunas                            ' Letra

        If (sustituirF22PorOidRemesaOri) Then
            sRegistro &= Traduzir("004_csv_lbl_oidRemesaOri").ToUpper & sDelimitadorColunas     ' OidRemesaOri
        Else
            sRegistro &= Traduzir("004_csv_lbl_f22").ToUpper & sDelimitadorColunas              ' F22
        End If

        If (exhibirCodSubCliente) Then
            sRegistro &= Traduzir("004_csv_lbl_codSubCliente").ToUpper & sDelimitadorColunas    ' CodSubCliente
        End If

        sRegistro &= Traduzir("004_csv_lbl_estacion").ToUpper & sDelimitadorColunas                         ' Estacion
        sRegistro &= Traduzir("004_csv_lbl_descricion_estacion").ToUpper & sDelimitadorColunas              ' Descricion Estacion
        sRegistro &= Traduzir("004_csv_lbl_medio_pago").ToUpper & sDelimitadorColunas                       ' Medio Pago
        sRegistro &= Traduzir("004_csv_lbl_descricion_medio_pago").ToUpper & sDelimitadorColunas            ' Descricion Medio Pago
        sRegistro &= Traduzir("004_csv_lbl_descricion_divisa").ToUpper & sDelimitadorColunas                ' Descricion Divisa
        sRegistro &= Traduzir("004_csv_lbl_declarado_f22").ToUpper & sDelimitadorColunas                    ' Declarado Remesa F22
        sRegistro &= Traduzir("004_csv_lbl_ingresado_por_sobre").ToUpper & sDelimitadorColunas              ' Ingresado por Sobre
        sRegistro &= Traduzir("004_csv_lbl_recontado").ToUpper & sDelimitadorColunas                        ' Recontado
        sRegistro &= Traduzir("004_csv_lbl_recontado_declarado_remesa").ToUpper & sDelimitadorColunas       ' Recontado Declarado Remessa
        sRegistro &= Traduzir("004_csv_lbl_ingresado_declarado_remesa").ToUpper & sDelimitadorColunas       ' Ingresado Declarado Remessa
        sRegistro &= Traduzir("004_csv_lbl_observaciones").ToUpper & sDelimitadorColunas                    ' Observaciones
        sRegistro &= Traduzir("004_csv_lbl_falsos").ToUpper & sDelimitadorColunas                           ' Falsos

        ' Adiciona no CSV a linha de cabeçalho
        objCSV.Append(sRegistro)
        objCSV.Append(sDelimitadorRegistros)
        sRegistro = String.Empty

        ' Define a cultura corrente
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(Constantes.Cultura.EN_US)

        ' Ordena a coluna (numero do recibo F22)
        Dim fDados = From d In objDados Order By d.F22 Ascending

        ' Loop pelos registros ordenados para copiá-los para a planilha
        For Each objeto As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSV In fDados

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
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.MedioPago.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionMedioPago.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionDivisa.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.DeclaradoRemesa.ToString())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.IngresadoSobre.ToString())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.Recontado.ToString())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal((objeto.Recontado - objeto.DeclaradoRemesa).ToString("F"))) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal((objeto.IngresadoSobre - objeto.DeclaradoRemesa).ToString("F"))) & sDelimitadorColunas

            ' Verificar o campo observaciones, se tiver valor inserir o caracter (') antes do texto
            ' para que o excel entenda que é um texto e não uma fórmula.
            Dim strObservaciones As String = String.Empty
            If Not String.IsNullOrEmpty(objeto.Observaciones.ToString()) Then
                strObservaciones = objeto.Observaciones.ToString().Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
            End If
            sRegistro &= Util.ConfiguraCalificadorTexto(strObservaciones) & sDelimitadorColunas

            Dim strFalsos As String = String.Empty
            For Each falso As ContractoServ.CorteParcial.GetCortesParciais.Falso In objeto.Falsos

                strFalsos &= falso.Tipo & _
                             " - " & falso.Divisa & _
                             " - " & falso.Denominacion & _
                             " - " & (IIf(falso.Tipo = "BILLETE", falso.NumeroSerie & " - " & falso.NumeroPlancha & " - " & falso.Observacion, falso.NumeroUnidades)) & _
                             sDelimitadorFalsos

            Next

            sRegistro &= Util.ConfiguraCalificadorTexto(strFalsos) & sDelimitadorColunas

            ' Adiciona no CSV a linha do registro de dados
            objCSV.Append(sRegistro)
            objCSV.Append(sDelimitadorRegistros)
            sRegistro = String.Empty

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
    Public Shared Sub EnviarDownload(ByRef objDados As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion, _
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
' Teste 2