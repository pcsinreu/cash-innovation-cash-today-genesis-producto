Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class DetalleParcialesMostrar
    Inherits Base

#Region "[ATRIBUTOS]"

    ' Define a variável para receber tipo o relatório será exibido
    Private _FormatoSalida As ContractoServ.Enumeradores.eFormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV

    ' Define a variável para receber o nome do arquivo
    Private _NomeArquivo As String = String.Empty

    ' Define a variável para receber a Delegacion
    Private _Delegacion As String = String.Empty

    ' Define a variável para receber a FechaDesde informada pelo usuário
    Private _FechaDesde As DateTime = DateTime.MinValue

    ' Define a variável para receber a FechaHasta informada pelo usuário
    Private _FechaHasta As DateTime = DateTime.MinValue

    ' Define a variável para receber o NumeroRemesa informado pelo usuário
    Private _NumeroRemesa As String = String.Empty

    ' Define a variável para receber o NumeroPrecinto informado pelo usuário
    Private _NumeroPrecinto As String = String.Empty

    ' Define a variável para receber o Cliente informado pelo usuário
    Private _Cliente As String = String.Empty

    ' Define a variável para receber o SubCliente informado pelo usuário
    Private _SubCliente As String = String.Empty

    ' Define a variável para receber o tipo da data
    Private _TipoData As Integer = 0

    ' Define a variável para receber se tem denominação
    Private Shared _ConDenominacion As Boolean = False

    ' Define a variável para receber se tem incidência
    Private Shared _ConIncidencia As Boolean = False

    ' armazena a estrutura dos dados do arquivo CSV
    Private Shared _DetalleParcialesDivisas As New List(Of DetalleParcialesDivisa)
    Private Shared _IACs As New Dictionary(Of String, String)

    ''' <summary>
    ''' Os dados do relatório armazenados na sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 26/08/2009 Criado
    ''' </history>
    Private ReadOnly Property DadosRelatorio() As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)
        Get
            Return DirectCast(Session("objDetalleParciales"), List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial))
        End Get
    End Property

#End Region

#Region "[MÉTODOS BASE]"

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    ''' <summary>
    ''' Configura tab index da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parâmetros da base
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.DETALLE_PARCIALES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    ''' <summary>
    ''' Inicializa página.
    ''' </summary>
    ''' <remarks></remarks>
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
    Protected Overrides Sub TraduzirControles()
        Me.Page.Title = Traduzir("007_lbl_titulo_busqueda")
        Me.lblTitulo.Text = Traduzir("007_lbl_titulo_busqueda")
        lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("Exibir") IsNot Nothing) Then
            ' Recupera a opção escolhida para exibir o relatório
            _FormatoSalida = Request.QueryString("Exibir")
        End If

        ' Nome arquivo
        If (Request.QueryString("NomeArquivo") IsNot Nothing) Then
            ' Recupera o nome do arquivo
            _NomeArquivo = Request.QueryString("NomeArquivo")
        End If

        ' Delegacion
        If (Request.QueryString("Delegacion") IsNot Nothing) Then
            ' Recupera a Delegacion
            _Delegacion = Request.QueryString("Delegacion")
        End If

        ' FechaDesde
        If (Request.QueryString("FechaDesde") IsNot Nothing) Then
            ' Recupera a FechaDesde
            _FechaDesde = Request.QueryString("FechaDesde")
        End If

        ' FechaHasta
        If (Request.QueryString("FechaHasta") IsNot Nothing) Then
            ' Recupera a FechaHasta
            _FechaHasta = Request.QueryString("FechaHasta")
        End If

        ' NumeroRemesa
        If (Request.QueryString("NumeroRemesa") IsNot Nothing) Then
            ' Recupera o NumeroRemesa
            _NumeroRemesa = Request.QueryString("NumeroRemesa")
        End If

        ' NumeroPrecinto
        If (Request.QueryString("NumeroPrecinto") IsNot Nothing) Then
            ' Recupera o NumeroPrecinto
            _NumeroPrecinto = Request.QueryString("NumeroPrecinto")
        End If

        ' Cliente
        If (Request.QueryString("Cliente") IsNot Nothing) Then
            'Recupera o cliente
            _Cliente = Request.QueryString("Cliente")
        End If

        ' SubCliente
        If (Request.QueryString("SubCliente") IsNot Nothing) Then
            'Recupera o cliente
            _SubCliente = Request.QueryString("SubCliente")
        End If

        ' TipoData
        If (Request.QueryString("TipoData") IsNot Nothing) Then
            ' Recupera o tipo da data
            _TipoData = Request.QueryString("TipoData")
        End If

        ' Denominação
        If (Request.QueryString("ConDenominacion") IsNot Nothing) Then
            ' Recupera se tem denominação
            _ConDenominacion = Request.QueryString("ConDenominacion")
        End If

        ' Incidência
        If (Request.QueryString("ConIncidencia") IsNot Nothing) Then
            ' Recupera se tem incidência
            _ConIncidencia = Request.QueryString("ConIncidencia")
        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarDados()

        ' Verifica o formato de saída
        If (_FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
            'Envia os dados para download (formato CSV)
            EnviarDownloadCSV(DirectCast(Session("objDetalleParciales"), List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)), _NomeArquivo)
        Else
            'Envia os dados para download (formato PDF)
            EnviarDownloadPDF()
        End If

    End Sub

    ''' <summary>
    ''' Função de conversão de Ilist para CSV
    ''' </summary>
    ''' <param name="objDados">Objeto com os dados retornados do banco</param>
    ''' <param name="sDelimitadorColunas">Delimitador da coluna</param>
    ''' <param name="sDelimitadorRegistros">Delimitador da linha</param>
    ''' <param name="sDelimitadorFalsos">Delimitador da linha</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GerarArquivoCSV(ByRef objDados As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), _
    Optional sDelimitadorColunas As String = ";", _
    Optional sDelimitadorRegistros As String = vbNewLine, _
    Optional sDelimitadorFalsos As String = "#") As String

        Dim bDetalleParcialesDenominacionEnFila As Boolean = False

        ' se a chave SeparadorColumnas existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorColumnas") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Length() > 0) Then
            sDelimitadorColunas = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Substring(0, 1)
        End If

        ' se a chave SeparadorFalsos existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorFalsos") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Length() > 0) Then
            sDelimitadorFalsos = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Substring(0, 1)
        End If

        ' se a chave SeparadorFalsos existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("DetalleParcialesDenominacionEnFila") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("DetalleParcialesDenominacionEnFila").Trim().Length() > 0) Then
            bDetalleParcialesDenominacionEnFila = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("DetalleParcialesDenominacionEnFila").Trim().Substring(0, 1)
        End If

        Dim objCSV As New StringBuilder(String.Empty)

        ' carrega as informações de estrutura do arquivo
        DefineInformacoesRelatorio(objDados)

        ' adiciona o cabecalho do arquivo CSV
        GeraCabecalhoArquivoCSV(objCSV, sDelimitadorColunas, sDelimitadorRegistros, bDetalleParcialesDenominacionEnFila)

        ' adiciona as linhas para o arquivo CSV
        GeraCabecalhoLinhasCSV(objDados, objCSV, sDelimitadorColunas, sDelimitadorRegistros, bDetalleParcialesDenominacionEnFila)

        ' Retorna o string do arquivo CSV gerado
        Return objCSV.ToString()

    End Function

    ''' <summary>
    ''' Gera um relatório em formato .PDF e envia para download 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Public Sub EnviarDownloadPDF()

        ' Cria o DataSet tipado
        Dim dsRelatorioPDF As New DetalleParciales

        ' Cria a linha de parametros
        Dim drParametro As DetalleParciales.ParametroRow = dsRelatorioPDF.Parametro.NewParametroRow

        ' Adiciona os parametros
        With drParametro

            .Planta = _Delegacion
            .TipoFecha = _TipoData

            If _TipoData = Constantes.TipoFecha.PROCESO Then
                .FechaProcesoIni = _FechaDesde
                .FechaProcesoFin = _FechaHasta
            Else
                .FechaTranspIni = _FechaDesde
                .FechaTranspFin = _FechaHasta
            End If

            .NumRemesa = _NumeroRemesa
            .NumPrecinto = _NumeroPrecinto
            .ConDenominacion = _ConDenominacion
            .ConIncidencia = _ConIncidencia
            .Cliente = _Cliente
            .Subcliente = _SubCliente

        End With

        ' Adiciona o parametro na tabela de parametros
        dsRelatorioPDF.Parametro.Rows.Add(drParametro)

        ' Popula o data set com os dados recuperados do objeto
        dsRelatorioPDF.Popular(DadosRelatorio)

        ' Passa o título do relatório
        rptDetalleParciales.TituloRelatorio = Traduzir("007_titulo_pagina")

        ' Passa o tipo do relatório que será exibido
        rptDetalleParciales.TipoRelatorio = ContractoServ.Enumeradores.eFormatoSalida.PDF

        ' Carrega os dados do relatório
        rptDetalleParciales.FonteDados = dsRelatorioPDF

        ' Passa o nome do arquivo que deverá ser gerado
        rptDetalleParciales.NomeArquivo = _NomeArquivo

    End Sub

    ''' <summary>
    ''' Gera um relatório em formato CSV e envia para download (disponível apenas para aplicações ASP.NET)
    ''' </summary>
    ''' <param name="objDados"></param>
    ''' <param name="sNomeArquivo"></param>
    ''' <remarks></remarks>
    Public Shared Sub EnviarDownloadCSV(ByRef objDados As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), _
                              sNomeArquivo As String)

        ' Gera um arquivo CSV com os dados do grid
        Dim sCSV As String = GerarArquivoCSV(objDados)
        ' Envia para o usuário o string do CSV
        Util.DescarregarCSV(sCSV, sNomeArquivo & ".csv")

    End Sub

    ''' <summary>
    ''' Recupera as informações e monta uma estrutura dos dados disponíveis
    ''' </summary>
    ''' <param name="objDados"></param>
    ''' <remarks></remarks>
    Public Shared Sub DefineInformacoesRelatorio(ByRef objDados As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial))

        _DetalleParcialesDivisas.Clear()
        _IACs.Clear()

        For Each dp As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial In objDados

            Dim tempDetalleParcialesDivisa As DetalleParcialesDivisa

            ' adiciona os efectivos e denominações disponíveis
            For Each efectivo In dp.Efectivos
                Dim efectivoLocal = efectivo
                tempDetalleParcialesDivisa = _DetalleParcialesDivisas.Find(Function(dpd) dpd.Divisa = efectivoLocal.Divisa)
                ' verifica se já existe a divisa
                If tempDetalleParcialesDivisa Is Nothing Then
                    tempDetalleParcialesDivisa = New DetalleParcialesDivisa
                    tempDetalleParcialesDivisa.Divisa = efectivo.Divisa
                    tempDetalleParcialesDivisa.DivisaOrdem = OrdemDivisa(efectivo.Divisa)
                    _DetalleParcialesDivisas.Add(tempDetalleParcialesDivisa)
                End If
                ' verifica se já existe o meio de pagamento
                If Not tempDetalleParcialesDivisa.MediosPago.Contains("efectivo") Then
                    tempDetalleParcialesDivisa.MediosPago.Add("efectivo")
                End If

                Dim ValorDivisa As String = efectivo.Denominacion & " " & efectivo.Calidad

                ' verifica se já existe a denominação
                If efectivo.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE Then
                    If Not tempDetalleParcialesDivisa.Billetes.Contains(ValorDivisa.Trim) Then
                        tempDetalleParcialesDivisa.Billetes.Add(ValorDivisa.Trim)
                    End If
                Else
                    If Not tempDetalleParcialesDivisa.Monedas.Contains(ValorDivisa.Trim) Then
                        tempDetalleParcialesDivisa.Monedas.Add(ValorDivisa.Trim)
                    End If
                End If

            Next

            ' adiciona os meios de pagamentos disponíveis
            For Each medioPago In dp.MediosPago
                Dim medioPagoLocal = medioPago
                tempDetalleParcialesDivisa = _DetalleParcialesDivisas.Find(Function(dpd) dpd.Divisa = medioPagoLocal.Divisa)
                ' verifica se já existe a divisa
                If tempDetalleParcialesDivisa Is Nothing Then
                    tempDetalleParcialesDivisa = New DetalleParcialesDivisa
                    tempDetalleParcialesDivisa.Divisa = medioPago.Divisa
                    tempDetalleParcialesDivisa.DivisaOrdem = OrdemDivisa(medioPago.Divisa)
                    _DetalleParcialesDivisas.Add(tempDetalleParcialesDivisa)
                End If
                ' verifica se já existe o meio de pagamento
                If Not tempDetalleParcialesDivisa.MediosPago.Contains(medioPago.TipoMedioPago) Then
                    tempDetalleParcialesDivisa.MediosPago.Add(medioPago.TipoMedioPago)
                End If
            Next

            ' adiciona os declarados disponíveis
            For Each declarado In dp.Declarados
                Dim declaradoLocal = declarado
                tempDetalleParcialesDivisa = _DetalleParcialesDivisas.Find(Function(dpd) dpd.Divisa = declaradoLocal.Divisa)
                ' verifica se já existe a divisa
                If tempDetalleParcialesDivisa Is Nothing Then
                    tempDetalleParcialesDivisa = New DetalleParcialesDivisa
                    tempDetalleParcialesDivisa.Divisa = declarado.Divisa
                    tempDetalleParcialesDivisa.DivisaOrdem = OrdemDivisa(declarado.Divisa)
                    _DetalleParcialesDivisas.Add(tempDetalleParcialesDivisa)
                End If
                ' verifica se já existe o meio de pagamento
                If Not tempDetalleParcialesDivisa.Declarados.Contains(declarado.TipoDeclarado) Then
                    tempDetalleParcialesDivisa.Declarados.Add(declarado.TipoDeclarado)
                End If
            Next

            ' adiciona os iacs disponíveis
            For Each informacao In dp.IACs
                ' verifica se já existe o iac
                If Not _IACs.ContainsKey(informacao.CodigoTermino) Then
                    _IACs.Add(informacao.CodigoTermino, informacao.Descricao)
                End If
            Next

        Next

    End Sub

    ''' <summary>
    ''' Gera a linha de cabeçalho do arquivo CSV
    ''' </summary>
    ''' <param name="objCSV"></param>
    ''' <param name="sDelimitadorColunas"></param>
    ''' <param name="sDelimitadorRegistros"></param>
    ''' <remarks></remarks>
    Public Shared Sub GeraCabecalhoArquivoCSV(ByRef objCSV As StringBuilder, ByRef sDelimitadorColunas As String, ByRef sDelimitadorRegistros As String, bDetalleParcialesDenominacionEnFila As Boolean)

        Dim cabecalho As String = String.Empty

        ' COD CLIENTE
        cabecalho &= Traduzir("007_csv_lbl_codigo_cliente").ToUpper & sDelimitadorColunas

        ' NOMBRE CLIENTE
        cabecalho &= Traduzir("007_csv_lbl_nombre_cliente").ToUpper & sDelimitadorColunas

        ' COD SUBCL
        cabecalho &= Traduzir("007_csv_lbl_codigo_subcliente").ToUpper & sDelimitadorColunas

        ' NOMBRE SUBCL
        cabecalho &= Traduzir("007_csv_lbl_nombre_subcliente").ToUpper & sDelimitadorColunas

        ' PTO SERVICIO
        cabecalho &= Traduzir("007_csv_lbl_punto_servicio").ToUpper & sDelimitadorColunas

        ' FECHA PROCESO
        cabecalho &= Traduzir("007_csv_lbl_fecha_proceso").ToUpper & sDelimitadorColunas

        ' FECHA TRANSP
        cabecalho &= Traduzir("007_csv_lbl_fecha_transporte").ToUpper & sDelimitadorColunas

        ' Nº REMESA
        cabecalho &= Traduzir("007_csv_lbl_numero_remesa").ToUpper & sDelimitadorColunas

        ' Nº PRECINTO
        cabecalho &= Traduzir("007_csv_lbl_numero_precinto").ToUpper & sDelimitadorColunas

        ' V. DECL REMESA {DIVISA}
        For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
            For Each declarado In dpDivisa.Declarados.FindAll(Function(d) d = "R")
                cabecalho &= Traduzir("007_csv_lbl_valor_declarado_remesa").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas
            Next
        Next

        ' V. DECL BULTO {DIVISA}
        For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
            For Each declarado In dpDivisa.Declarados.FindAll(Function(d) d = "B")
                cabecalho &= Traduzir("007_csv_lbl_valor_declarado_bulto").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas
            Next
        Next

        ' Nº PARCIAL
        cabecalho &= Traduzir("007_csv_lbl_numero_parcial").ToUpper & sDelimitadorColunas

        ' IACS
        For Each informacao In _IACs
            cabecalho &= informacao.Value.ToUpper() & sDelimitadorColunas
        Next

        For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)

            ' V. DECL PARCIAL {DIVISA}
            If dpDivisa.Declarados.Count > 0 Then
                If dpDivisa.Declarados.Contains("P") Then
                    cabecalho &= Traduzir("007_csv_lbl_valor_declarado_parcial").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas
                End If
            End If

            ' VALOR CONTADO {DIVISA}
            If dpDivisa.MediosPago.Count > 0 Then
                cabecalho &= Traduzir("007_csv_lbl_valor_contado").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas
            End If

            ' {MEDIO PAGO} {DIVISA}
            For Each medioPago In dpDivisa.MediosPago
                cabecalho &= medioPago.ToUpper() & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas
            Next

            If _ConIncidencia Then

                ' FALSOS {DIVISA}
                cabecalho &= Traduzir("007_csv_lbl_falsos").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas

                ' DIFERENCIA {DIVISA}
                cabecalho &= Traduzir("007_csv_lbl_diferencia").ToUpper & " " & dpDivisa.Divisa.ToUpper() & sDelimitadorColunas

            End If

        Next

        If _ConIncidencia Then

            ' OBSERVACIONES
            cabecalho &= Traduzir("007_csv_lbl_observaciones").ToUpper & sDelimitadorColunas

        End If

        If _ConDenominacion Then

            If Not bDetalleParcialesDenominacionEnFila Then

                For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                    ' {DIVISA} B {DENOMINAÇÃO}
                    For Each denominacao In dpDivisa.Billetes.OrderBy(Function(d) d)
                        cabecalho &= dpDivisa.Divisa.ToUpper() & " " & Traduzir("gen_csv_lbl_billete") & denominacao.ToUpper() & sDelimitadorColunas
                    Next
                    ' {DIVISA} M {DENOMINAÇÃO}
                    For Each denominacao In dpDivisa.Monedas.OrderBy(Function(m) m)
                        cabecalho &= dpDivisa.Divisa.ToUpper() & " " & Traduzir("gen_csv_lbl_moneda") & denominacao.ToUpper() & sDelimitadorColunas
                    Next
                Next

            Else

                ' DENOMINACIÓN
                cabecalho &= Traduzir("007_csv_lbl_denominacion").ToUpper & sDelimitadorColunas

                ' CALIDAD
                cabecalho &= Traduzir("007_csv_lbl_calidad").ToUpper & sDelimitadorColunas

                ' CANTIDAD
                cabecalho &= Traduzir("007_csv_lbl_cantidad").ToUpper & sDelimitadorColunas

                ' IMPORTE
                cabecalho &= Traduzir("007_csv_lbl_importe").ToUpper & sDelimitadorColunas

            End If

        End If

        ' Adiciona no CSV a linha de cabeçalho
        objCSV.Append(cabecalho)
        objCSV.Append(sDelimitadorRegistros)

    End Sub

    ''' <summary>
    ''' Gera as linhas do arquivo CSV
    ''' </summary>
    ''' <param name="objDados"></param>
    ''' <param name="objCSV"></param>
    ''' <param name="sDelimitadorColunas"></param>
    ''' <param name="sDelimitadorRegistros"></param>
    ''' <remarks></remarks>
    Public Shared Sub GeraCabecalhoLinhasCSV(ByRef objDados As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), ByRef objCSV As StringBuilder, ByRef sDelimitadorColunas As String, ByRef sDelimitadorRegistros As String, bDetalleParcialesDenominacionEnFila As Boolean)

        Dim linha As String
        Dim linhaTmp As String
        Dim totalEfectivo As Decimal
        Dim totalDeclarado As Decimal
        Dim totalContado As Decimal
        Dim totalFalsos As Decimal

        For Each detalleParcial In objDados

            linha = String.Empty
            linhaTmp = String.Empty
            totalEfectivo = 0
            totalDeclarado = 0
            totalContado = 0
            totalFalsos = 0

            ' COD CLIENTE
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.CodigoCliente.ToUpper) & sDelimitadorColunas

            ' NOMBRE CLIENTE
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.NomeCliente.ToUpper) & sDelimitadorColunas

            ' COD SUBCL
            linha &= Util.ConfiguraCalificadorTexto(IIf(detalleParcial.CodigoSubCliente.Equals("0"), String.Empty, detalleParcial.CodigoSubCliente.ToUpper)) & sDelimitadorColunas

            ' NOMBRE SUBCL
            linha &= Util.ConfiguraCalificadorTexto(IIf(detalleParcial.NomeSubCliente.Equals(" "), String.Empty, detalleParcial.NomeSubCliente.ToUpper)) & sDelimitadorColunas

            ' PTO SERVICIO
            linha &= Util.ConfiguraCalificadorTexto(IIf(detalleParcial.PuntoServicio.Equals("0 -  "), String.Empty, detalleParcial.PuntoServicio.ToUpper)) & sDelimitadorColunas

            ' FECHA PROCESO
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.FechaProceso.ToString("dd-MM-yyyy")) & sDelimitadorColunas

            ' FECHA TRANSP
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.FechaTransporte.ToString("dd-MM-yyyy")) & sDelimitadorColunas

            ' Nº REMESA
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.NumeroRemesa.ToUpper) & sDelimitadorColunas

            ' Nº PRECINTO
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.NumeroPrecinto.ToUpper) & sDelimitadorColunas

            For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                Dim dpDivisaLocal = dpDivisa
                For Each declarado In dpDivisa.Declarados.FindAll(Function(d) d = "R")
                    Dim declaradoLocal = declarado

                    linhaTmp = (New Decimal(0)).ToString("N2")
                    Dim dec = detalleParcial.Declarados.Find(Function(d) d.Divisa = dpDivisaLocal.Divisa AndAlso d.TipoDeclarado = declaradoLocal)
                    If dec IsNot Nothing Then
                        linhaTmp = Util.ConfiguraSeparadorDecimal(dec.ImporteTotal.ToString("N2"))
                    End If

                    ' V. DECL REMESA {DIVISA}
                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                Next
            Next

            For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                Dim dpDivisaLocal = dpDivisa
                For Each declarado In dpDivisa.Declarados.FindAll(Function(d) d = "B")
                    Dim declaradoLocal = declarado

                    linhaTmp = (New Decimal(0)).ToString("N2")
                    Dim dec = detalleParcial.Declarados.Find(Function(d) d.Divisa = dpDivisaLocal.Divisa AndAlso d.TipoDeclarado = declaradoLocal)
                    If dec IsNot Nothing Then
                        linhaTmp = Util.ConfiguraSeparadorDecimal(dec.ImporteTotal.ToString("N2"))
                    End If

                    ' V. DECL BULTO {DIVISA}
                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                Next
            Next

            ' Nº PARCIAL
            linha &= Util.ConfiguraCalificadorTexto(detalleParcial.NumeroParcial.ToUpper) & sDelimitadorColunas

            ' IACS
            For Each informacao In _IACs
                Dim informacaoLocal = informacao
                linhaTmp = String.Empty
                Dim inf = detalleParcial.IACs.FindAll(Function(i) i.CodigoTermino = informacaoLocal.Key)
                If inf IsNot Nothing AndAlso inf.Count > 0 Then
                    Dim iacs As String = String.Empty
                    For Each infor In inf
                        If iacs <> String.Empty Then
                            iacs &= " - "
                        End If
                        iacs &= infor.Valor
                    Next
                    linhaTmp = iacs
                End If
                linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas
            Next

            For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                Dim dpDivisaLocal = dpDivisa

                totalDeclarado = 0
                totalContado = 0
                totalFalsos = 0

                If dpDivisa.Declarados.Count > 0 Then
                    If dpDivisa.Declarados.Contains("P") Then

                        Dim declaradoParcial = detalleParcial.Declarados.Find(Function(d) d.Divisa = dpDivisaLocal.Divisa AndAlso d.TipoDeclarado = "P")
                        If declaradoParcial IsNot Nothing Then
                            totalDeclarado = declaradoParcial.ImporteTotal
                        End If

                        ' V. DECL PARCIAL {DIVISA}
                        linha &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(totalDeclarado.ToString("N2"))) & sDelimitadorColunas

                    End If
                End If

                If dpDivisa.MediosPago.Count > 0 Then

                    For Each medioPago In dpDivisa.MediosPago
                        Dim medioPagoLocal = medioPago
                        If medioPago = "efectivo" Then
                            For Each efe In detalleParcial.Efectivos.FindAll(Function(e) e.Divisa = dpDivisaLocal.Divisa)
                                totalContado += (efe.Denominacion * efe.Unidades)
                            Next
                        Else
                            Dim med = detalleParcial.MediosPago.Find(Function(m) m.TipoMedioPago = medioPagoLocal AndAlso m.Divisa = dpDivisaLocal.Divisa)
                            If med IsNot Nothing Then
                                totalContado += med.Valor
                            End If
                        End If
                    Next

                    ' VALOR CONTADO {DIVISA}
                    linha &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(totalContado.ToString("N2"))) & sDelimitadorColunas

                End If

                For Each medioPago In dpDivisa.MediosPago
                    Dim medioPagoLocal = medioPago
                    If medioPago = "efectivo" Then

                        totalEfectivo = 0
                        For Each efe In detalleParcial.Efectivos.FindAll(Function(e) e.Divisa = dpDivisaLocal.Divisa)
                            totalEfectivo += (efe.Denominacion * efe.Unidades)
                            totalFalsos += (efe.Denominacion * efe.Falsos)
                        Next

                        ' {MEDIO PAGO} {DIVISA}
                        linha &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(totalEfectivo.ToString("N2"))) & sDelimitadorColunas

                    Else

                        linhaTmp = (New Decimal(0)).ToString("N2")
                        Dim med = detalleParcial.MediosPago.Find(Function(m) m.TipoMedioPago = medioPagoLocal AndAlso m.Divisa = dpDivisaLocal.Divisa)
                        If med IsNot Nothing Then
                            linhaTmp = med.Valor.ToString("N2")
                        End If

                        ' {MEDIO PAGO} {DIVISA}
                        linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                    End If
                Next

                If _ConIncidencia Then

                    ' FALSOS {DIVISA}
                    linha &= Util.ConfiguraCalificadorTexto(totalFalsos.ToString("N2")) & sDelimitadorColunas

                    ' DIFERENCIA {DIVISA}
                    linha &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal((totalContado - totalDeclarado).ToString("N2"))) & sDelimitadorColunas

                End If

            Next

            If _ConIncidencia Then

                Dim obs As String = String.Empty

                For Each observacao In detalleParcial.Observaciones
                    If obs <> String.Empty Then
                        obs += " - "
                    End If
                    obs += observacao.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
                Next

                ' OBSERVACIONES
                linha &= Util.ConfiguraCalificadorTexto(obs) & sDelimitadorColunas

            End If

            If _ConDenominacion Then

                If Not bDetalleParcialesDenominacionEnFila Then

                    For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                        Dim dpDivisaLocal = dpDivisa
                        For Each denominacao In dpDivisa.Billetes.OrderBy(Function(d) d)
                            Dim denominacaoLocal = denominacao

                            linhaTmp = "0"
                            Dim den = detalleParcial.Efectivos.Where(Function(d) (d.Denominacion & " " & d.Calidad).Trim = denominacaoLocal _
                                                                        AndAlso d.Divisa = dpDivisaLocal.Divisa _
                                                                        AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE)
                            If den IsNot Nothing AndAlso den.Count > 0 Then
                                linhaTmp = den.Sum(Function(de) de.Unidades)
                            End If

                            ' {DIVISA} B {DENOMINAÇÃO}
                            linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                        Next

                        For Each denominacao In dpDivisa.Monedas.OrderBy(Function(d) d)
                            Dim denominacaoLocal = denominacao
                            linhaTmp = "0"
                            Dim den = detalleParcial.Efectivos.Where(Function(d) (d.Denominacion & " " & d.Calidad).Trim = denominacaoLocal _
                                                                        AndAlso d.Divisa = dpDivisaLocal.Divisa _
                                                                        AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA)
                            If den IsNot Nothing AndAlso den.Count > 0 Then
                                linhaTmp = den.Sum(Function(de) de.Unidades)
                            End If

                            ' {DIVISA} M {DENOMINAÇÃO}
                            linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                        Next
                    Next

                Else

                    Dim linhaBase As String = linha
                    linha = String.Empty

                    For Each dpDivisa In _DetalleParcialesDivisas.OrderBy(Function(dpd) dpd.DivisaOrdem).ThenBy(Function(dpd) dpd.Divisa)
                        Dim dpDivisaLocal = dpDivisa
                        For Each denominacao In dpDivisa.Billetes.OrderBy(Function(d) d)
                            Dim denominacaoLocal = denominacao

                            linha = String.Empty

                            Dim den = detalleParcial.Efectivos.Where(Function(d) (d.Denominacion & " " & d.Calidad).Trim = denominacaoLocal _
                                                                        AndAlso d.Divisa = dpDivisaLocal.Divisa _
                                                                        AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE)

                            Dim denUni = den.FirstOrDefault()

                            ' DENOMINACIÓN
                            If denominacao.IndexOf(" ") > 0 Then
                                ' se tem um espaço na descrição da denominação,
                                ' significa que é uma denominação com "deteriorado" e por
                                ' isso, deve ser removida a última palavra (deteriorado)
                                linha &= Util.ConfiguraCalificadorTexto(dpDivisa.Divisa.ToString & " " & ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE & denominacao.Split(" ")(0)) & sDelimitadorColunas
                            Else
                                linha &= Util.ConfiguraCalificadorTexto(dpDivisa.Divisa.ToString & " " & ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE & denominacao) & sDelimitadorColunas
                            End If

                            linhaTmp = "0"
                            If den IsNot Nothing AndAlso den.Count > 0 Then
                                Dim sUnidades = den.Sum(Function(de) de.Unidades)
                                If sUnidades > 0 Then

                                    ' CALIDAD
                                    linha &= Util.ConfiguraCalificadorTexto(If(denominacao.IndexOf(" ") > 0, Traduzir("007_csv_lbl_codigo_calidad"), String.Empty)) & sDelimitadorColunas

                                    linhaTmp = sUnidades

                                    ' CANTIDAD
                                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                                    linhaTmp = linhaTmp * denUni.Denominacion

                                    ' IMPORTE
                                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                                End If

                                objCSV.Append(linhaBase & linha)
                                objCSV.Append(sDelimitadorRegistros)

                            End If
                        Next

                        For Each denominacao In dpDivisa.Monedas.OrderBy(Function(d) d)
                            Dim denominacaoLocal = denominacao

                            linha = String.Empty

                            Dim den = detalleParcial.Efectivos.Where(Function(d) (d.Denominacion & " " & d.Calidad).Trim = denominacaoLocal _
                                                                        AndAlso d.Divisa = dpDivisaLocal.Divisa _
                                                                        AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA)
                            Dim denUni = den.FirstOrDefault()

                            ' DENOMINACIÓN
                            If denominacao.IndexOf(" ") > 0 Then
                                ' se tem um espaço na descrição da denominação,
                                ' significa que é uma denominação com "deteriorado" e por
                                ' isso, deve ser removida a última palavra (deteriorado)
                                linha &= Util.ConfiguraCalificadorTexto(dpDivisa.Divisa.ToString & " " & ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA & denominacao.Split(" ")(0)) & sDelimitadorColunas
                            Else
                                linha &= Util.ConfiguraCalificadorTexto(dpDivisa.Divisa.ToString & " " & ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA & denominacao) & sDelimitadorColunas
                            End If

                            linhaTmp = "0"
                            If den IsNot Nothing AndAlso den.Count > 0 Then
                                Dim sUnidades = den.Sum(Function(de) de.Unidades)
                                If sUnidades > 0 Then

                                    ' CALIDAD
                                    linha &= Util.ConfiguraCalificadorTexto(If(denominacao.IndexOf(" ") > 0, Traduzir("007_csv_lbl_codigo_calidad"), String.Empty)) & sDelimitadorColunas

                                    linhaTmp = den.Sum(Function(de) de.Unidades)

                                    ' CANTIDAD
                                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                                    linhaTmp = linhaTmp * denUni.Denominacion

                                    ' IMPORTE
                                    linha &= Util.ConfiguraCalificadorTexto(linhaTmp) & sDelimitadorColunas

                                End If

                                objCSV.Append(linhaBase & linha)
                                objCSV.Append(sDelimitadorRegistros)

                            End If

                        Next
                    Next

                End If

            End If

            If Not bDetalleParcialesDenominacionEnFila OrElse Not _ConDenominacion Then
                ' Adiciona no CSV a linha de cabeçalho
                objCSV.Append(linha)
                objCSV.Append(sDelimitadorRegistros)
            End If
        Next

    End Sub

    ''' <summary>
    ''' Retorna o valor da ordem da divisa de acordo com o solicitado pelo usuário
    ''' </summary>
    ''' <param name="divisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function OrdemDivisa(divisa As String) As Integer

        Dim ordem As Integer

        Select Case divisa.Trim().ToUpper()
            Case "EURO"
                ordem = 1
            Case "DOLAR", "DÓLAR"
                ordem = 2
            Case "LIBRA"
                ordem = 3
            Case Else
                ordem = 4
        End Select

        Return ordem

    End Function

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

Public Class DetalleParcialesDivisa

    Private _Divisa As String
    Private _DivisaOrdem As Integer
    Private _MediosPago As New List(Of String)
    Private _Billetes As New List(Of String)
    Private _Monedas As New List(Of String)
    Private _Declarados As New List(Of String)

    Public Property Divisa() As String
        Get
            Return _Divisa
        End Get
        Set(value As String)
            _Divisa = value
        End Set
    End Property

    Public Property DivisaOrdem() As Integer
        Get
            Return _DivisaOrdem
        End Get
        Set(value As Integer)
            _DivisaOrdem = value
        End Set
    End Property

    Public Property MediosPago() As List(Of String)
        Get
            Return _MediosPago
        End Get
        Set(value As List(Of String))
            _MediosPago = value
        End Set
    End Property

    Public Property Billetes() As List(Of String)
        Get
            Return _Billetes
        End Get
        Set(value As List(Of String))
            _Billetes = value
        End Set
    End Property

    Public Property Monedas() As List(Of String)
        Get
            Return _Monedas
        End Get
        Set(value As List(Of String))
            _Monedas = value
        End Set
    End Property

    Public Property Declarados() As List(Of String)
        Get
            Return _Declarados
        End Get
        Set(value As List(Of String))
            _Declarados = value
        End Set
    End Property

End Class