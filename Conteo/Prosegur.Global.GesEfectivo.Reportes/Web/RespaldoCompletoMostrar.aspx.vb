Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class RespaldoCompletoMostrar
    Inherits Base

#Region "[ATRIBUTOS]"

    ' Define a variável para receber tipo o relatório será exibido
    Private _FormatoSalida As ContractoServ.Enumeradores.eFormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV

    ' Define a variável para receber o nome do arquivo
    Private _NomeArquivo As String = String.Empty

    ' Define a variável para receber a data inicial informada pelo usuário
    Private _DataInicial As DateTime = DateTime.MinValue

    ' Define a variável para receber a data final informada pelo usuário
    Private _DataFinal As DateTime = DateTime.MinValue

    ' Define a variável para receber o processo informado pelo usuário
    Private _Processo As String = String.Empty

    ' Define a variável para receber a planta informada pelo usuário
    Private _Planta As String = String.Empty

    ' Define a variável para receber o cliente informado pelo usuário
    Private _Cliente As String = String.Empty

    ' Define a variável para receber o tipo da data
    Private _TipoData As Integer = 0

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
        MyBase.PaginaAtual = Enumeradores.eTelas.RESPALDO_COMPLETO
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
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub CarregarDados()

        ' Verifica o formato de saída
        If (_FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then
            'Envia os dados para download
            EnviarDownload(DirectCast(Session("objRespaldosCompletos"), ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion), _NomeArquivo)
        Else
            ' Cria o DataSet tipado
            Dim dsRespaldosCompletos As New RespaldosCompletos
            ' Cria a linha de parametros
            Dim drParametro As RespaldosCompletos.ParametroRow = dsRespaldosCompletos.Parametro.NewParametroRow

            ' Adiciona os parametros
            drParametro.Recuento = _Processo
            drParametro.FechaIni = _DataInicial
            drParametro.FechaFin = _DataFinal
            drParametro.FechaTipo = _TipoData
            drParametro.Planta = _Planta
            drParametro.Cliente = _Cliente
            drParametro.Sustituir_F22_Por_OidRemesaOri = Util.VerificarParametroNoWebConfig("Sustituir_F22_Por_OidRemesaOri")
            drParametro.Exhibir_CodSubCliente = Util.VerificarParametroNoWebConfig("Exhibir_CodSubCliente")

            ' Adiciona o parametro na tabela de parametros
            dsRespaldosCompletos.Parametro.Rows.Add(drParametro)
            ' Popula o data set com os dados recuperados do objeto
            dsRespaldosCompletos.PopularRespaldoCompleto(DirectCast(Session("objRespaldosCompletos"), ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF))
            ' Passa o título do relatório
            rptRespaldoCompleto.TituloRelatorio = Traduzir("005_titulo_pagina")
            ' Passa o tipo do relatório que será exibido
            rptRespaldoCompleto.TipoRelatorio = _FormatoSalida
            ' Carrega os dados do relatório
            rptRespaldoCompleto.FonteDados = dsRespaldosCompletos
            ' Carrega o reporte de acordo com o formato de saída
            rptRespaldoCompleto.Report = String.Format("crRespaldoCompleto{0}.rpt", _FormatoSalida.ToString)
            ' Passa o nome do arquivo que deverá ser gerado
            rptRespaldoCompleto.NomeArquivo = _NomeArquivo

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
    ''' Em 03/08/2009 por (Carlos Bomtempo/Magnum Oliveira)
    ''' </history>
    Private Shared Function GerarArquivoCSV(ByRef objDados As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion, _
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

        ' Joga o título do campo no registro
        If sRegistro <> String.Empty Then sRegistro &= sDelimitadorColunas
        sRegistro &= Traduzir("005_csv_lbl_recuento").ToUpper & sDelimitadorColunas                         ' Recuento
        sRegistro &= Traduzir("005_csv_lbl_fecha").ToUpper & sDelimitadorColunas                            ' Fecha
        sRegistro &= Traduzir("005_csv_lbl_letra").ToUpper & sDelimitadorColunas                            ' Letra

        If (sustituirF22PorOidRemesaOri) Then
            sRegistro &= Traduzir("005_csv_lbl_oidRemesaOri").ToUpper & sDelimitadorColunas     ' OidRemesaOri
        Else
            sRegistro &= Traduzir("005_csv_lbl_f22").ToUpper & sDelimitadorColunas              ' F22
        End If

        If (exhibirCodSubCliente) Then
            sRegistro &= Traduzir("005_csv_lbl_codSubCliente").ToUpper & sDelimitadorColunas    ' CodSubCliente
        End If

        sRegistro &= Traduzir("005_csv_lbl_estacion").ToUpper & sDelimitadorColunas                         ' Estacion
        sRegistro &= Traduzir("005_csv_lbl_descricion_estacion").ToUpper & sDelimitadorColunas              ' Descricion Estacion

        ' Para cada informação do IAC
        For Each informacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC In objDados.First().InformacionesIAC
            ' Grava o cabeçalho da informação do IAC
            sRegistro &= informacaoIAC.Descricao & sDelimitadorColunas                                      ' Informação de IAC
        Next

        sRegistro &= Traduzir("005_csv_lbl_medio_pago").ToUpper & sDelimitadorColunas                       ' Medio Pago
        sRegistro &= Traduzir("005_csv_lbl_descricion_divisa").ToUpper & sDelimitadorColunas                ' Descricion Divisa
        sRegistro &= Traduzir("005_csv_lbl_ingresado_por_sobre").ToUpper & sDelimitadorColunas              ' Ingresado por Sobre
        sRegistro &= Traduzir("005_csv_lbl_recontado").ToUpper & sDelimitadorColunas                        ' Recontado
        sRegistro &= Traduzir("005_csv_lbl_recontado_ingresado").ToUpper & sDelimitadorColunas              ' Recontado - Ingresado
        sRegistro &= Traduzir("005_csv_lbl_observaciones").ToUpper & sDelimitadorColunas                    ' Observaciones
        sRegistro &= Traduzir("005_csv_lbl_falsos").ToUpper & sDelimitadorColunas                           ' Falsos

        ' Adiciona no CSV a linha de cabeçalho
        objCSV.Append(sRegistro)
        objCSV.Append(sDelimitadorRegistros)
        sRegistro = String.Empty

        ' Ordena a coluna (numero do recibo F22)
        Dim fDados = From d In objDados _
                     Where d.Divisa <> String.Empty _
                     Group By d.DescricionDivisa, d.DescricionSucursal, d.F22, d.OidRemesaOri, d.CodSubCliente, d.Fecha, d.Letra, d.MedioPago, d.Observaciones, d.Recuento, d.Sucursal, d.Parcial _
                     Into IngresadoSobreT = Max(d.IngresadoSobre), ContadoT = Sum(d.Contado) _
                     Order By F22 Ascending _
                     Select IngresadoSobreT, ContadoT, DescricionDivisa, DescricionSucursal, F22, OidRemesaOri, CodSubCliente, Fecha, Letra, MedioPago, Observaciones, Recuento, Sucursal, Parcial

        ' Loop pelos registros para copiá-los para a planilha   
        For Each objeto In fDados
            Dim objetoLocal = objeto

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

            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.Sucursal.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionSucursal.Trim()) & sDelimitadorColunas

            Dim objInformacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion = _
                ReturnInformacionIAC((From d In objDados _
                          Where d.F22 = objetoLocal.F22 AndAlso d.MedioPago = objetoLocal.MedioPago AndAlso _
                          d.DescricionDivisa = objetoLocal.DescricionDivisa AndAlso d.Parcial = objetoLocal.Parcial _
                          Select d.InformacionesIAC).ToList())

            ' Para cada informação do IAC
            For Each informacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC In objInformacaoIAC
                ' Grava o valor da informação do IAC
                sRegistro &= Util.ConfiguraCalificadorTexto(Convert.ToString(informacaoIAC.Valor)) & sDelimitadorColunas
            Next

            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.MedioPago.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(objeto.DescricionDivisa.Trim()) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.IngresadoSobreT.ToString())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal(objeto.ContadoT.ToString())) & sDelimitadorColunas
            sRegistro &= Util.ConfiguraCalificadorTexto(Util.ConfiguraSeparadorDecimal((objeto.ContadoT - objeto.IngresadoSobreT).ToString())) & sDelimitadorColunas

            ' Verificar o campo observaciones, se tiver valor inserir o caracter (') antes do texto
            ' para que o excel entenda que é um texto e não uma fórmula conforme dúvida 1485 resolvida.
            Dim strObservaciones As String = String.Empty
            If Not String.IsNullOrEmpty(objeto.Observaciones.ToString()) Then
                strObservaciones = objeto.Observaciones.ToString().Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
            End If
            sRegistro &= Util.ConfiguraCalificadorTexto(strObservaciones) & sDelimitadorColunas

            Dim objFalsos As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.FalsoColeccion = (From d In objDados _
                                                                                                    Where d.F22 = objetoLocal.F22 AndAlso d.MedioPago = objetoLocal.MedioPago AndAlso d.DescricionDivisa = objetoLocal.DescricionDivisa _
                                                                                                    Select d.Falsos).FirstOrDefault()
            Dim strFalsos As String = String.Empty
            For Each falso As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Falso In objFalsos

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
    Public Shared Sub EnviarDownload(ByRef objDados As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion, _
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

    Private Shared Function ReturnInformacionIAC(colInformIAC As List(Of ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion)) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion

        For Each objIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion In colInformIAC

            For Each informacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC In objIAC

                If Not String.IsNullOrEmpty(informacaoIAC.Valor) Then

                    Return objIAC

                End If

            Next

        Next

        Return colInformIAC.FirstOrDefault()

    End Function

#End Region


End Class