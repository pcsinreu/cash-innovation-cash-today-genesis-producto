Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class ContadoPorPuestoMostrar
    Inherits Base

#Region "[VARIÁVEIS]"

    ' Base para criar uma linha nova para ser preenchida e adicionada ao relatório do tipo .CSV
    ' key = nomecoluna, 
    ' value = valor que será exibido no relatório
    Dim _templateColuna As New Dictionary(Of String, String)

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Tipo de formato escolhido pelo usuário
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private ReadOnly Property FormatoSalida() As ContractoServ.Enumeradores.eFormatoSalida
        Get
            Return Request.QueryString("Exibir")
        End Get
    End Property

    ''' <summary>
    ''' Nome do arquivo a ser gerado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private ReadOnly Property NomeArquivo() As String
        Get
            Return Request.QueryString("NomeArquivo")
        End Get
    End Property

    ''' <summary>
    ''' Delimitador colunas do formato CSV
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Shared ReadOnly Property DelimitadorColunas() As String
        Get

            Dim sDelimitadorColunas As String = String.Empty

            ' se a chave SeparadorColumnas existe no Web.Config
            If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorColumnas") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Length() > 0) Then
                sDelimitadorColunas = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorColumnas").Trim().Substring(0, 1)
            Else
                sDelimitadorColunas = ";"
            End If

            Return sDelimitadorColunas

        End Get
    End Property

    ''' <summary>
    ''' Delimitador falsos do formato CSV
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Shared ReadOnly Property DelimitadorFalsos() As String
        Get

            Dim sDelimitadorFalsos As String = String.Empty

            ' se a chave SeparadorFalsos existe no Web.Config
            If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("SeparadorFalsos") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Length() > 0) Then
                sDelimitadorFalsos = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("SeparadorFalsos").Trim().Substring(0, 1)
            Else
                sDelimitadorFalsos = "#"
            End If

            Return sDelimitadorFalsos

        End Get
    End Property

    ''' <summary>
    ''' Delimitador registros do formato CSV
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Shared ReadOnly Property DelimitadorRegistros() As String
        Get
            Return vbNewLine
        End Get
    End Property

    ''' <summary>
    ''' Os dados do relatório armazenados na sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private ReadOnly Property DadosRelatorio() As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)
        Get
            Return DirectCast(Session("objContadoPuesto"), List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))
        End Get
    End Property

    ''' <summary>
    ''' Retorna os atributos do form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/08/2009 Criado
    ''' </history>
    Private ReadOnly Property AtributosForm() As Atributos
        Get
            Return DirectCast(Session("AtributosContadoPuesto"), Atributos)
        End Get
    End Property

#End Region

#Region "[MÉTODOS BASE]"

    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parâmetros da base
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.CONTADO_PUESTO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

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
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("008_lbl_titulo_busqueda")
        Me.lblTitulo.Text = Traduzir("008_lbl_titulo_busqueda")
        lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString()

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Sub CarregarDados()

        ' Verifica o formato de saída
        If (FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV) Then

            'Envia os dados para download do arquivo .CSV
            EnviarDownloadCSV()

        Else

            ' envia os dados para download do arquivo .PDF
            EnviarDownloadPDF()

        End If

        ' limpa dados da sessão
        LimparSessoes()

    End Sub

    ''' <summary>
    ''' Gera um relatório em formato CSV e envia para download (disponível apenas para aplicações ASP.NET)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Public Sub EnviarDownloadCSV()

        ' gera arquivo csv
        Dim relatorio As New RelatorioCSV(DadosRelatorio, Me.AtributosForm.ConIncidencias, Me.AtributosForm.ConDenominacion)

        ' Gera um arquivo CSV com os dados do grid
        Dim sCSV As String = relatorio.GerarRelatorio()

        ' Envia para o usuário o string do CSV
        Util.DescarregarCSV(sCSV, NomeArquivo & ".csv")

    End Sub

    ''' <summary>
    ''' Gera um relatório em formato .PDF e envia para download 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Public Sub EnviarDownloadPDF()

        ' Cria o DataSet tipado
        Dim dsContadoPuesto As New ContadoPuesto

        ' Cria a linha de parametros
        Dim drParametro As ContadoPuesto.ParametroRow = dsContadoPuesto.Parametro.NewParametroRow

        ' Adiciona os parametros
        With drParametro

            .Planta = AtributosForm.Delegacion
            .FechaProcesoIni = AtributosForm.FechaProcesoIni
            .FechaProcesoFin = AtributosForm.FechaProcesoFin
            .FechaTranspIni = AtributosForm.FechaTransporteIni
            .FechaTranspFin = AtributosForm.FechaTransporteFin
            .Puesto = AtributosForm.Puesto
            .Operario = AtributosForm.Operario
            .HoraIni = AtributosForm.HoraIni
            .HoraFin = AtributosForm.HoraFin
            .NumRemesa = AtributosForm.NumRemesa
            .NumPrecinto = AtributosForm.NumPrecinto
            .ConDenominacion = AtributosForm.ConDenominacion
            .ConIncidencia = AtributosForm.ConIncidencias
            .TipoFecha = AtributosForm.TipoFecha

            If String.IsNullOrEmpty(AtributosForm.NombreCliente) Then
                .Cliente = String.Empty
            Else
                .Cliente = AtributosForm.CodCliente & " - " & AtributosForm.NombreCliente
            End If

            If String.IsNullOrEmpty(AtributosForm.NombreSubcliente) Then
                .Subcliente = String.Empty
            Else
                .Subcliente = AtributosForm.CodSubcliente & " - " & AtributosForm.NombreSubcliente
            End If

        End With

        ' Adiciona o parametro na tabela de parametros
        dsContadoPuesto.Parametro.Rows.Add(drParametro)

        ' Popula o data set com os dados recuperados do objeto
        dsContadoPuesto.Popular(DadosRelatorio)

        ' Passa o título do relatório
        rptContadoPuesto.TituloRelatorio = Traduzir("008_titulo_pagina")

        ' Passa o tipo do relatório que será exibido
        rptContadoPuesto.TipoRelatorio = ContractoServ.Enumeradores.eFormatoSalida.PDF

        ' Carrega os dados do relatório
        rptContadoPuesto.FonteDados = dsContadoPuesto

        ' Passa o nome do arquivo que deverá ser gerado
        rptContadoPuesto.NomeArquivo = NomeArquivo

    End Sub

    ''' <summary>
    ''' Limpa os dados armazenados na sessão
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 18/08/2009 Criado
    ''' </history>
    Private Sub LimparSessoes()

        Session("objContadoPuesto") = Nothing
        Session("AtributosContadoPuesto") = Nothing

    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

        Try

            ' Carrega os dados do relatório
            CarregarDados()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[RELATÓRIO CSV]"

    ''' <summary>
    ''' Classe para manipular um relatório no formato .CSV
    ''' </summary>
    ''' <remarks></remarks>
    Private Class RelatorioCSV

#Region "[ENUMERAÇÕES]"

        Private Enum eTipoDeclarado
            Remesa
            Bulto
            Parcial
        End Enum

#End Region

#Region "[VARIÁVEIS]"

        Private _dados As New List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        Private _estruturaRelatorio As New EstruturaRelatorio

        Private _conIncidencias As Boolean = False

        Private _conDenominaciones As Boolean = False

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New(Dados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto), _
                       ConIncidencias As Boolean, ConDenominaciones As Boolean)

            _dados = Dados
            _conIncidencias = ConIncidencias
            _conDenominaciones = ConDenominaciones

            ConfigurarEstruturaRel()

        End Sub

#End Region

#Region "[MÉTODOS]"

        Private Sub ConfigurarEstruturaRel()

            Dim divisa As String = String.Empty

            ' verifica quais divisas possuem valor (só adiciona coluna para as divisas que possuem valor)
            For Each parcial In _dados

                ' verifica colunas declarados
                For Each dec In parcial.Declarados

                    divisa = dec.DesDivisa

                    Select Case dec.TipoDeclarados.ToUpper()
                        Case ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA : _estruturaRelatorio.Configurar(dec.DesDivisa, EstruturaRelatorio.eTipoColuna.DeclaradosRemesa)
                        Case ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO : _estruturaRelatorio.Configurar(dec.DesDivisa, EstruturaRelatorio.eTipoColuna.DeclaradosBulto)
                        Case ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL : _estruturaRelatorio.Configurar(dec.DesDivisa, EstruturaRelatorio.eTipoColuna.DeclaradosParcial)
                    End Select

                Next

                ' verifica coluna efetivos, falsos e denominações
                For Each efe In parcial.Efectivos

                    divisa = efe.Divisa

                    _estruturaRelatorio.Configurar(efe.Divisa, EstruturaRelatorio.eTipoColuna.Efetivos)
                    _estruturaRelatorio.Configurar(efe.Divisa, EstruturaRelatorio.eTipoColuna.Contados)

                    If _conIncidencias AndAlso efe.Falsos > 0 Then
                        _estruturaRelatorio.Configurar(efe.Divisa, EstruturaRelatorio.eTipoColuna.Falsos)
                    End If

                    ' configura colunas denominações
                    If _conDenominaciones Then
                        If efe.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE Then
                            _estruturaRelatorio.ConfigurarBillete(efe.Divisa, efe.Denominacion, efe.Denominacion)
                        Else
                            _estruturaRelatorio.ConfigurarMoneda(efe.Divisa, efe.Denominacion, efe.Denominacion)
                        End If
                    End If

                Next

                ' verifica colunas meios de pagamento
                For Each mp In parcial.MediosPago

                    divisa = mp.Divisa
                    _estruturaRelatorio.ConfigurarMedioPagos(mp.Divisa, mp.TipoMedioPago)
                    _estruturaRelatorio.Configurar(mp.Divisa, EstruturaRelatorio.eTipoColuna.Contados)

                Next

            Next

            ' ordena as denominações
            _estruturaRelatorio.OrdenarDenominacoes()

        End Sub

        ''' <summary>
        ''' Configura as linhas do relatório 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 16/08/2009 Criado
        ''' </history>
        Private Function ConfigurarLinhasCSV() As String

            Dim StbRelatorio As New StringBuilder()
            Dim linhaPlanilha As String

            For Each parcial In _dados

                linhaPlanilha = String.Empty

                ' Escreve o registro conforme veio no banco
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.CodPuesto) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.CodUsuario) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.CodCliente) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.NombreCliente) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.CodSubcliente) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.NombreSubcliente) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.PuntoServicio) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.FechaProceso.ToString("dd-MM-yyyy")) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.FechaTransporte.ToString("dd-MM-yyyy")) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.NumRemesa) & DelimitadorColunas
                linhaPlanilha &= Util.ConfiguraCalificadorTexto(parcial.NumPrecinto) & DelimitadorColunas

                ' adiciona valores das colunas dinâmicas
                linhaPlanilha &= ObterValoresColDinamicas(parcial)

                StbRelatorio.Append(linhaPlanilha)
                StbRelatorio.Append(DelimitadorRegistros)

            Next

            Return StbRelatorio.ToString()

        End Function

        Private Function ObterValoresColDinamicas(Parcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto) As String

            ' representa uma linha da planilha
            Dim sbRegistro As New StringBuilder()

            ' representam as partes da linha da planilha
            Dim strDeclarados As String = String.Empty
            Dim strContados As String = String.Empty
            Dim strEfetivo As String = String.Empty
            Dim strMedioPagos As String = String.Empty
            Dim strFalsos As String = String.Empty
            Dim strDenominaciones As String = String.Empty
            Dim strDiferencas As String = String.Empty
            Dim strObs As String = String.Empty

            Dim valContado As Decimal
            Dim valDecParcial As Decimal
            Dim valDiferencas As Decimal

            For Each parte In _estruturaRelatorio.Partes

                ' inicializa variáveis
                valContado = 0
                valDiferencas = 0
                valDecParcial = 0
                strDeclarados = String.Empty
                strContados = String.Empty
                strEfetivo = String.Empty
                strFalsos = String.Empty
                strMedioPagos = String.Empty
                strDiferencas = String.Empty

                ' obtém declarados remesa
                If parte.ExibeDeclaradosRemesa Then
                    strDeclarados &= Util.ConfiguraCalificadorTexto(ObterDeclarados(Parcial, parte.NomeDivisa, eTipoDeclarado.Remesa).ToString("0.00")) & DelimitadorColunas
                End If

                ' obtém declarados bulto
                If parte.ExibeDeclaradosBulto Then
                    strDeclarados &= Util.ConfiguraCalificadorTexto(ObterDeclarados(Parcial, parte.NomeDivisa, eTipoDeclarado.Bulto).ToString("0.00")) & DelimitadorColunas
                End If

                ' obtém declarados da parcial
                valDecParcial = ObterDeclarados(Parcial, parte.NomeDivisa, eTipoDeclarado.Parcial)

                ' verifica se coluna dec parcial será exibida
                If parte.ExibeDeclaradosParcial Then
                    strDeclarados &= Util.ConfiguraCalificadorTexto(valDecParcial.ToString("0.00")) & DelimitadorColunas
                End If

                ' obtém valores das colunas efetivo, denominações, falsos e calcula contados
                ObterValoresEfetivos(Parcial, parte, strEfetivo, strDenominaciones, strFalsos, valContado)

                ' obtém valores das colunas meios de pagamento e calcula contados
                ObterValoresMedioPagos(Parcial, parte, strMedioPagos, valContado)

                ' adiciona valores contados
                If parte.ExibeContados Then
                    strContados &= Util.ConfiguraCalificadorTexto(valContado.ToString("0.00")) & DelimitadorColunas
                End If

                ' calcula e adiciona diferencas
                If _conIncidencias Then

                    valDiferencas = valContado - valDecParcial

                    strDiferencas &= Util.ConfiguraCalificadorTexto(valDiferencas.ToString("0.00")) & DelimitadorColunas

                End If

                ' monta linha
                sbRegistro.Append(strDeclarados)
                sbRegistro.Append(strContados)
                sbRegistro.Append(strEfetivo)
                sbRegistro.Append(strMedioPagos)
                sbRegistro.Append(strFalsos)
                sbRegistro.Append(strDiferencas)

            Next

            ' obtém observações
            If _conIncidencias Then

                For Each obs In Parcial.Observaciones
                    ' concatena observações removendo quebras de linha.
                    strObs &= obs.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ") & " - "
                Next

                If strObs.Trim().Length > 0 Then
                    ' remove o último traço
                    strObs = strObs.Substring(0, strObs.Length - 3)
                End If

                strObs = Util.ConfiguraCalificadorTexto(strObs) & DelimitadorColunas

            End If

            ' adiciona no final da linha as observações
            sbRegistro.Append(strObs)

            ' adiciona no final da linha a parte referente as denominações
            sbRegistro.Append(strDenominaciones)

            ' retorna linha configurada
            Return sbRegistro.ToString()

        End Function

        Private Sub ObterValoresMedioPagos(Parcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto, _
                                                Parte As ParteRelatorio, _
                                                ByRef strMedioPagos As String, _
                                                ByRef valContados As Decimal)

            Dim tipoMedioPago As String = String.Empty
            Dim valMedioPagos As Decimal

            strMedioPagos = String.Empty

            ' obtém valores dos meios de pagamento
            For Each medioPago In Parte.MediosPagos

                tipoMedioPago = medioPago
                valMedioPagos = 0

                Dim mediosP = (From mp In Parcial.MediosPago _
                               Where mp.Divisa.ToUpper().Trim() = Parte.NomeDivisa.ToUpper().Trim() _
                               AndAlso mp.TipoMedioPago.ToUpper().Trim() = tipoMedioPago.ToUpper().Trim() _
                               Select mp.Valor).ToList

                If mediosP.Count > 0 Then
                    valMedioPagos = mediosP(0)
                    valContados += valMedioPagos
                End If

                strMedioPagos &= Util.ConfiguraCalificadorTexto(valMedioPagos.ToString("0.00")) & DelimitadorColunas

            Next

        End Sub

        Private Sub ObterValoresEfetivos(Parcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto, _
                                       Parte As ParteRelatorio, ByRef strEfetivo As String, ByRef strDenominaciones As String, _
                                       ByRef strFalsos As String, ByRef valContados As Decimal)

            Dim valDenominacion As Decimal
            Dim valEfetivos As Decimal
            Dim valFalsos As Decimal

            ' obtém efetivos da divisa
            Dim efetivos = (From efe In Parcial.Efectivos _
                                Where efe.Divisa.ToUpper().Trim() = Parte.NomeDivisa.ToUpper().Trim() _
                                Select efe.Unidades, efe.Denominacion, efe.Divisa, efe.Falsos, efe.Tipo).ToList

            If Parte.ExibeEfetivos Then

                If efetivos.Count > 0 Then
                    valEfetivos = (From val In efetivos Select val.Denominacion * val.Unidades).Sum
                End If

                valContados = valEfetivos

                strEfetivo = Util.ConfiguraCalificadorTexto(valEfetivos.ToString("0.00")) & DelimitadorColunas

            End If

            ' obtém valores das denominações (se existir denominações na coleção, é pq é para exibir)
            For Each den In Parte.Billetes
                Dim denLocal = den

                valDenominacion = 0

                Dim valD = (From d In efetivos _
                            Where d.Divisa.ToUpper() = denLocal.Divisa.ToUpper() _
                            AndAlso d.Denominacion = denLocal.Valor _
                            AndAlso d.Tipo = denLocal.Tipo _
                            Select d.Unidades).ToList

                If valD.Count > 0 Then
                    valDenominacion &= valD(0)
                End If

                strDenominaciones &= Util.ConfiguraCalificadorTexto(valDenominacion) & DelimitadorColunas

            Next

            ' obtém valores das denominações (se existir denominações na coleção, é pq é para exibir)
            For Each den In Parte.Monedas
                Dim denLocal = den

                valDenominacion = 0

                Dim valD = (From d In efetivos _
                            Where d.Divisa.ToUpper() = denLocal.Divisa.ToUpper() _
                            AndAlso d.Denominacion = denLocal.Valor _
                            AndAlso d.Tipo = denLocal.Tipo _
                            Select d.Unidades).ToList

                If valD.Count > 0 Then
                    valDenominacion &= valD(0)
                End If

                strDenominaciones &= Util.ConfiguraCalificadorTexto(valDenominacion) & DelimitadorColunas

            Next

            ' obtém falsos
            If Parte.ExibeFalsos Then

                Dim efeFalsos = (From e In efetivos Where e.Falsos > 0 Select e.Falsos * e.Denominacion).ToList

                If efeFalsos.Count > 0 Then
                    valFalsos = efeFalsos.Sum.ToString("0.00")
                End If

                strFalsos = Util.ConfiguraCalificadorTexto(valFalsos.ToString("0.00")) & DelimitadorColunas

            End If

        End Sub

        ''' <summary>
        ''' Obtém o declarado do tipo informado (Remessa, bulto ou parcial)
        ''' </summary>
        ''' <param name="Parcial"></param>
        ''' <param name="NomeDivisa"></param>
        ''' <param name="TipoDeclarado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 18/08/2009 Criado
        ''' </history>
        Private Function ObterDeclarados(Parcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto, _
                                         NomeDivisa As String, _
                                         TipoDeclarado As eTipoDeclarado) As Decimal

            Dim valDec As Decimal = 0
            Dim tipoDec As String = String.Empty

            Select Case TipoDeclarado
                Case eTipoDeclarado.Parcial : tipoDec = ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL
                Case eTipoDeclarado.Bulto : tipoDec = ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO
                Case eTipoDeclarado.Remesa : tipoDec = ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA
            End Select

            Dim decParcial = (From dec In Parcial.Declarados _
                                      Where dec.DesDivisa.ToUpper().Trim() = NomeDivisa.ToUpper().Trim() _
                                      AndAlso dec.TipoDeclarados.ToUpper() = tipoDec).ToList()

            If decParcial.Count > 0 Then

                valDec = decParcial(0).NumImporteTotal

            End If

            Return valDec

        End Function

        ''' <summary>
        ''' Configura cabeçalho do relatório no formato .csv
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 16/08/2009 Criado
        ''' </history>
        Private Function ConfigurarCabecalhoCSV() As String

            Dim stbLinhaTitulo As New StringBuilder()

            With stbLinhaTitulo
                .Append(Traduzir("008_csv_lbl_puesto").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_operario").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_codcliente").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_nombrecliente").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_codsubcl").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_nombresubcl").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_pto_servicio").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_fecha_proc").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_fecha_transp").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_num_remesa").ToUpper & DelimitadorColunas)
                .Append(Traduzir("008_csv_lbl_num_precinto").ToUpper & DelimitadorColunas)
            End With

            ' adiciona colunas dinâmicas das divisas que possuem dados
            stbLinhaTitulo.Append(AdicionarCabecalhoColDinamicas())

            stbLinhaTitulo.Append(DelimitadorRegistros)

            Return stbLinhaTitulo.ToString()

        End Function

        ''' <summary>
        ''' Adiciona colunas dinâmicas de acordo com a estrutura do relatório
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 16/08/2009 Criado
        ''' </history>
        Private Function AdicionarCabecalhoColDinamicas() As String

            Dim stbTitulo As New StringBuilder()
            Dim strObs As String = String.Empty
            Dim stbTituloDenominacoes As New StringBuilder()

            ' verifica quais divisas possuem valor (só adiciona coluna para as divisas que possuem valor)
            For Each parte In _estruturaRelatorio.Partes

                ' coluna declarado remesa
                If parte.ExibeDeclaradosRemesa Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_val_dec_rem").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                ' coluna declarado bulto
                If parte.ExibeDeclaradosBulto Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_val_dec_bulto").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                ' coluna declarado parcial 
                If parte.ExibeDeclaradosParcial Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_val_dec_parcial").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                ' coluna contados
                If parte.ExibeContados Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_val_contado").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                ' coluna efetivos
                If parte.ExibeEfetivos Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_efetivo").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                For Each mp In parte.MediosPagos
                    ' adiciona uma coluna para cada meio de pagamento
                    stbTitulo.Append(mp.ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                Next

                ' coluna falsos
                If parte.ExibeFalsos Then
                    stbTitulo.Append(Traduzir("008_csv_lbl_falsos").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                End If

                If _conIncidencias Then
                    ' coluna diferenças
                    stbTitulo.Append(Traduzir("008_csv_lbl_diferencia").ToUpper() & " " & parte.NomeDivisa.ToUpper() & DelimitadorColunas)
                    ' coluna observaciones
                    strObs = Traduzir("008_csv_lbl_obs").ToUpper() & DelimitadorColunas
                End If

                For Each den In parte.Billetes
                    ' adiciona uma coluna para cada denominação
                    stbTituloDenominacoes.Append(den.Descricao & DelimitadorColunas)
                Next

                For Each den In parte.Monedas
                    ' adiciona uma coluna para cada denominação
                    stbTituloDenominacoes.Append(den.Descricao & DelimitadorColunas)
                Next

            Next

            stbTitulo.Append(strObs)
            stbTitulo.Append(stbTituloDenominacoes.ToString())

            Return stbTitulo.ToString()

        End Function

        ''' <summary>
        ''' Gera o conteúdo do relatório
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 16/08/2009 Criado
        ''' </history>
        Public Function GerarRelatorio() As String

            Dim stbRelatorio As New StringBuilder()

            stbRelatorio.Append(ConfigurarCabecalhoCSV())

            stbRelatorio.Append(ConfigurarLinhasCSV())

            Return stbRelatorio.ToString()

        End Function

#End Region

#Region "[CLASSES AUXILIARES]"

        ''' <summary>
        ''' Representa a estrutura do relatório.
        ''' A estrutura é dividida em partes onde cada parte é uma divisa.
        ''' </summary>
        ''' <remarks></remarks>
        Private Class EstruturaRelatorio

            Public Partes As New List(Of ParteRelatorio)

            Public Enum eTipoColuna
                DeclaradosRemesa
                DeclaradosBulto
                DeclaradosParcial
                Efetivos
                Falsos
                Diferencas
                Observacoes
                Contados
            End Enum

            Private Function ObterParteXDivisa(NomeDivisa As String) As ParteRelatorio

                Dim parteDivisa As ParteRelatorio = (From d In Partes Where d.NomeDivisa = NomeDivisa.ToUpper().Trim()).FirstOrDefault()

                If parteDivisa Is Nothing Then

                    parteDivisa = New ParteRelatorio
                    parteDivisa.NomeDivisa = NomeDivisa.ToUpper().Trim()
                    Partes.Add(parteDivisa)

                End If

                Return parteDivisa

            End Function

            Public Sub Configurar(NomeDivisa As String, TipoColuna As eTipoColuna)

                Dim parteDivisa As ParteRelatorio = ObterParteXDivisa(NomeDivisa)

                Select Case TipoColuna
                    Case eTipoColuna.DeclaradosBulto : parteDivisa.ExibeDeclaradosBulto = True
                    Case eTipoColuna.DeclaradosParcial : parteDivisa.ExibeDeclaradosParcial = True
                    Case eTipoColuna.DeclaradosRemesa : parteDivisa.ExibeDeclaradosRemesa = True
                    Case eTipoColuna.Efetivos : parteDivisa.ExibeEfetivos = True
                    Case eTipoColuna.Falsos : parteDivisa.ExibeFalsos = True
                    Case eTipoColuna.Contados : parteDivisa.ExibeContados = True
                End Select

            End Sub

            Public Sub ConfigurarMedioPagos(NomeDivisa As String, MedioPagos As String)

                Dim parteDivisa As ParteRelatorio = ObterParteXDivisa(NomeDivisa)

                If Not parteDivisa.MediosPagos.Contains(MedioPagos.ToUpper().Trim()) Then
                    parteDivisa.MediosPagos.Add(MedioPagos.ToUpper().Trim())
                End If

            End Sub

            Public Sub ConfigurarBillete(NomeDivisa As String, Denominacion As String, Valor As Decimal)

                Dim parteDivisa As ParteRelatorio = ObterParteXDivisa(NomeDivisa)

                Dim strDenominacion As String = NomeDivisa.ToUpper().Trim() & " " & Traduzir("gen_csv_lbl_billete") & Denominacion.Trim()

                If (From d In parteDivisa.Billetes Where d.Divisa = NomeDivisa.ToUpper() _
                    AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE _
                    AndAlso d.Valor = Valor).FirstOrDefault() Is Nothing Then
                    Dim den As New Denominacao(strDenominacion, Valor, NomeDivisa, ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE)
                    parteDivisa.Billetes.Add(den)
                End If

            End Sub

            Public Sub ConfigurarMoneda(NomeDivisa As String, Denominacion As String, Valor As Decimal)

                Dim parteDivisa As ParteRelatorio = ObterParteXDivisa(NomeDivisa)

                Dim strDenominacion As String = NomeDivisa.ToUpper().Trim() & " " & Traduzir("gen_csv_lbl_moneda") & Denominacion.Trim()

                If (From d In parteDivisa.Monedas Where d.Divisa = NomeDivisa.ToUpper() _
                    AndAlso d.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA _
                    AndAlso d.Valor = Valor).FirstOrDefault() Is Nothing Then
                    Dim den As New Denominacao(strDenominacion, Valor, NomeDivisa, ContractoServ.Constantes.C_TIPO_EFECTIVO_MONEDA)
                    parteDivisa.Monedas.Add(den)
                End If

            End Sub

            Public Sub OrdenarDenominacoes()

                For Each p In Partes

                    p.Billetes = p.Billetes.OrderByDescending(Function(den) den.Valor).ToList()

                Next

            End Sub

        End Class

        ''' <summary>
        ''' Representa uma parte (divisa) do relatório.
        ''' Armazena as configurações de cada parte do relatório
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ParteRelatorio

            Public NomeDivisa As String
            Public ExibeDeclaradosRemesa As Boolean = False
            Public ExibeDeclaradosBulto As Boolean = False
            Public ExibeDeclaradosParcial As Boolean = False
            Public ExibeEfetivos As Boolean = False
            Public ExibeFalsos As Boolean = False
            Public ExibeContados As Boolean = False
            Public MediosPagos As New List(Of String)
            Public Billetes As New List(Of Denominacao)
            Public Monedas As New List(Of Denominacao)

        End Class

        Private Class Denominacao

            Public Descricao As String
            Public Tipo As String
            Public Divisa As String
            Public Valor As Decimal

            Public Sub New(Desc As String, Val As Decimal, Div As String, Tip As String)
                Descricao = Desc
                Valor = Val
                Tipo = Tip
                Divisa = Div.ToUpper()
            End Sub

        End Class

#End Region

    End Class

    ''' <summary>
    ''' Classe para manipular um relatório no formato .CSV
    ''' </summary>
    ''' <remarks></remarks>
    Private Class RelatorioCSV_v2

#Region "[CONSTANTES]"

        Private Const C_COL_PUESTO As String = "colPuesto"
        Private Const C_COL_OPERARIO As String = "colOperario"
        Private Const C_COL_COD_CLIENTE As String = "colCodCliente"
        Private Const C_COL_NOM_CLIENTE As String = "colNomCliente"
        Private Const C_COL_COD_SUBC As String = "colCodSubCliente"
        Private Const C_COL_NOM_SUBC As String = "colNomSubCliente"
        Private Const C_COL_PTO_SERV As String = "colPtoServico"
        Private Const C_COL_FECHA_PROC As String = "colFechaProc"
        Private Const C_COL_FECHA_TRANSP As String = "colFechaTransp"
        Private Const C_COL_NUM_REMESA As String = "colNumRemesa"
        Private Const C_COL_NUM_PRECINTO As String = "colNumPrecinto"

#End Region

#Region "[VARIÁVEIS]"

        Private _dados As New List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        Private _colunasPlanilhaFixas As New List(Of Coluna)
        Private _colunasPlanilhaDivisas As New List(Of ColunaXDivisa)

        Private _conIncidencias As Boolean = False

        Private _conDenominaciones As Boolean = False

        Private _numLinhasPlanilha As Integer = 0

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New(Dados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto), _
                       ConIncidencias As Boolean, ConDenominaciones As Boolean)

            _dados = Dados
            _conIncidencias = ConIncidencias
            _conDenominaciones = ConDenominaciones

            CriaColunasFixas()

            PreencherValoresRel()

        End Sub

#End Region

#Region "[MÉTODOS]"

        Private Sub CriaColunasFixas()

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_puesto").ToUpper, C_COL_PUESTO))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_operario").ToUpper, C_COL_OPERARIO))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_codcliente").ToUpper, C_COL_COD_CLIENTE))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_nombrecliente").ToUpper, C_COL_NOM_CLIENTE))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_codsubcl").ToUpper, C_COL_COD_SUBC))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_nombresubcl").ToUpper, C_COL_NOM_SUBC))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_pto_servicio").ToUpper, C_COL_PTO_SERV))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_fecha_proc").ToUpper, C_COL_FECHA_PROC))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_fecha_transp").ToUpper, C_COL_FECHA_TRANSP))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_num_remesa").ToUpper, C_COL_NUM_REMESA))

            _colunasPlanilhaFixas.Add(New Coluna(Traduzir("008_csv_lbl_num_precinto").ToUpper, C_COL_NUM_PRECINTO))

        End Sub

        Private Sub PreencherValoresRel()

            Dim divisa As String
            Dim valEfetivos As Decimal = 0
            Dim valFalsos As Decimal = 0
            Dim valDecParcial As Decimal = 0
            Dim valDiferencas As Decimal = 0
            Dim valContados As Decimal = 0
            Dim valMediosPago As Decimal = 0

            ' verifica quais divisas possuem valor (só adiciona coluna para as divisas que possuem valor)
            For Each parcial In _dados

                divisa = String.Empty
                valEfetivos = 0
                valFalsos = 0
                valDecParcial = 0
                valDiferencas = 0
                valContados = 0
                valMediosPago = 0

                PreencherValor(C_COL_PUESTO, parcial.CodPuesto)
                PreencherValor(C_COL_OPERARIO, parcial.CodUsuario)
                PreencherValor(C_COL_COD_CLIENTE, parcial.CodCliente)
                PreencherValor(C_COL_NOM_CLIENTE, parcial.NombreCliente)
                PreencherValor(C_COL_COD_SUBC, parcial.CodSubcliente)
                PreencherValor(C_COL_NOM_SUBC, parcial.NombreSubcliente)
                PreencherValor(C_COL_PTO_SERV, parcial.PuntoServicio)
                PreencherValor(C_COL_FECHA_PROC, parcial.FechaProceso)
                PreencherValor(C_COL_FECHA_TRANSP, parcial.FechaTransporte)
                PreencherValor(C_COL_NUM_REMESA, parcial.NumRemesa)
                PreencherValor(C_COL_NUM_PRECINTO, parcial.NumPrecinto)

                ' verifica colunas declarados
                For Each dec In parcial.Declarados

                    divisa = dec.DesDivisa

                    PreencherValorDec(dec.NumImporteTotal, dec.TipoDeclarados, dec.DesDivisa)

                    If dec.TipoDeclarados.ToUpper() = ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL.ToUpper() Then
                        valDecParcial = dec.NumImporteTotal
                    End If

                Next

                ' calcula efetivo e falsos
                For Each efe In parcial.Efectivos

                    divisa = efe.Divisa
                    valEfetivos += efe.Unidades * efe.Denominacion

                    If efe.Falsos > 0 Then
                        valFalsos += efe.Falsos * efe.Denominacion
                    End If

                Next

                ' preenche efetivos
                PreencherValorEfetivos(valEfetivos, divisa)

                ' preenche falsos
                If valFalsos > 0 Then
                    PreencherValorFalsos(valFalsos, divisa)
                End If

                ' preenche meios de pagamento
                For Each mp In parcial.MediosPago
                    divisa = mp.Divisa
                    PreencherValorMedioPago(mp.Valor, mp.Divisa, mp.TipoMedioPago)
                Next

                If Not String.IsNullOrEmpty(divisa) Then

                    ' calcula contados
                    PreencherValorContados(valContados, divisa)

                    ' calcula diferenças
                    valDiferencas = valDecParcial - valContados

                End If

                _numLinhasPlanilha += 1

            Next

            ' ordena as denominações
            '_estruturaRelatorio.OrdenarDenominacoes()

        End Sub

        Private Function ObterColunaFixa(IdColuna As String) As Coluna

            ' obtém coluna
            Dim col = (From c As Coluna In _colunasPlanilhaFixas _
                       Where c.Id = IdColuna).FirstOrDefault()

            Return col

        End Function

        Private Function ObterColunaDivisa(Divisa As String) As ColunaXDivisa

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            Return col

        End Function

        Private Sub PreencherValor(IdColuna As String, Valor As String)

            ' obtém coluna
            Dim col = ObterColunaFixa(IdColuna)

            If col Is Nothing Then

                col = AdicionarColuna(IdColuna, Valor)

            End If

            col.Valores.Add(Util.ConfiguraCalificadorTexto(Valor) & DelimitadorColunas)

        End Sub

        Private Function AdicionarColuna(IdColuna As String, Valor As String) As Coluna

            Dim col As New Coluna(IdColuna, IdColuna)

            For i As Integer = 0 To _numLinhasPlanilha

                col.Valores.Add(Util.ConfiguraCalificadorTexto(String.Empty) & DelimitadorColunas)

            Next

            _colunasPlanilhaFixas.Add(col)

            Return col

        End Function

        Private Sub PreencherValorDec(Valor As String, TipoDeclarado As String, Divisa As String)

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            If col Is Nothing Then
                col = AdicionarColunaDivisa(Valor, Divisa)
            End If

            For i As Integer = col.ColDecParcial.Valores.Count To _numLinhasPlanilha
                col.ColDecParcial.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            For i As Integer = col.ColDecBulto.Valores.Count To _numLinhasPlanilha
                col.ColDecBulto.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            For i As Integer = col.ColDecRemesa.Valores.Count To _numLinhasPlanilha
                col.ColDecRemesa.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            Select Case TipoDeclarado.ToUpper()
                Case ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL.ToUpper() : col.ColDecParcial.Valores.Add(Valor)
                Case ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO.ToUpper() : col.ColDecBulto.Valores.Add(Valor)
                Case ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA.ToUpper() : col.ColDecRemesa.Valores.Add(Valor)
            End Select

        End Sub

        Private Sub PreencherValorEfetivos(Valor As String, Divisa As String)

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            If col Is Nothing Then
                col = AdicionarColunaDivisa(Valor, Divisa)
            End If

            For i As Integer = col.ColEfetivo.Valores.Count To _numLinhasPlanilha
                col.ColEfetivo.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            col.ColEfetivo.Valores.Add(Valor)

        End Sub

        Private Sub PreencherValorFalsos(Valor As String, Divisa As String)

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            If col Is Nothing Then
                col = AdicionarColunaDivisa(Valor, Divisa)
            End If

            For i As Integer = col.ColFalsos.Valores.Count To _numLinhasPlanilha
                col.ColFalsos.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            col.ColFalsos.Valores.Add(Valor)

        End Sub

        Private Sub PreencherValorContados(Valor As String, Divisa As String)

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            If col Is Nothing Then
                col = AdicionarColunaDivisa(Valor, Divisa)
            End If

            For i As Integer = col.ColValContado.Valores.Count To _numLinhasPlanilha
                col.ColValContado.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
            Next

            col.ColFalsos.Valores.Add(Valor)

        End Sub

        Private Sub PreencherValorMedioPago(Valor As String, Divisa As String, TipoMP As String)

            ' obtém coluna
            Dim col = (From c As ColunaXDivisa In _colunasPlanilhaDivisas _
                       Where c.Divisa = Divisa.ToUpper()).FirstOrDefault()

            If col Is Nothing Then
                col = AdicionarColunaDivisa(Valor, Divisa, TipoMP)
            End If

            ' verifica se o medio pago existe
            Dim colMP = (From cMP In col.ColMediosPago _
                         Where cMP.Id = TipoMP.ToUpper()).FirstOrDefault()

            If colMP Is Nothing Then

                colMP = New Coluna(TipoMP.ToUpper() & " " & Divisa.ToUpper(), TipoMP.ToUpper())

                col.ColMediosPago.Add(colMP)

            End If

            For Each mp In col.ColMediosPago
                For i As Integer = mp.Valores.Count To _numLinhasPlanilha
                    colMP.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                Next
            Next

            colMP.Valores.Add(Valor)

        End Sub

        Private Function AdicionarColunaDivisa(Valor As String, Divisa As String, _
                                               Optional TipoMP As String = "") As ColunaXDivisa

            Dim col As New ColunaXDivisa(Divisa)

            col.ColDecParcial.Titulo = Traduzir("008_csv_lbl_val_dec_parcial") & " " & Divisa.ToUpper()
            col.ColDecBulto.Titulo = Traduzir("008_csv_lbl_val_dec_bulto") & " " & Divisa.ToUpper()
            col.ColDecRemesa.Titulo = Traduzir("008_csv_lbl_val_dec_rem") & " " & Divisa.ToUpper()
            col.ColEfetivo.Titulo = Divisa.ToUpper()
            col.ColFalsos.Titulo = Traduzir("008_csv_lbl_falsos") & " " & Divisa.ToUpper()
            col.ColValContado.Titulo = Traduzir("008_csv_lbl_val_contado") & " " & Divisa.ToUpper()

            If Not String.IsNullOrEmpty(TipoMP) Then
                col.ColMediosPago.Add(New Coluna(TipoMP.ToUpper() & " " & Divisa.ToUpper(), TipoMP.ToUpper()))
            End If

            For i As Integer = 0 To _numLinhasPlanilha

                col.ColDecParcial.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                col.ColDecBulto.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                col.ColDecRemesa.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                col.ColEfetivo.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                col.ColFalsos.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                col.ColValContado.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)

                If col.ColMediosPago.Count > 0 Then

                    For Each colMP In col.ColMediosPago
                        colMP.Valores.Add(Util.ConfiguraCalificadorTexto("0,00") & DelimitadorColunas)
                    Next

                End If

            Next

            _colunasPlanilhaDivisas.Add(col)

            Return col

        End Function

        ''' <summary>
        ''' Gera o conteúdo do relatório
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 16/08/2009 Criado
        ''' </history>
        Public Function GerarRelatorio() As String

            Dim stbRelatorio As New StringBuilder()
            Dim strRegistro As String

            For i As Integer = 0 To _numLinhasPlanilha

                strRegistro = String.Empty

                strRegistro &= ObterColunaFixa(C_COL_PUESTO).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_OPERARIO).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_COD_CLIENTE).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_NOM_CLIENTE).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_COD_SUBC).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_NOM_SUBC).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_PTO_SERV).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_FECHA_PROC).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_FECHA_TRANSP).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_NUM_REMESA).Valores(i)
                strRegistro &= ObterColunaFixa(C_COL_NUM_PRECINTO).Valores(i)

                For Each colDivisa In _colunasPlanilhaDivisas

                    ' declarados
                    If colDivisa.ColDecParcial.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColDecParcial.Valores(i)
                    End If

                    If colDivisa.ColDecBulto.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColDecBulto.Valores(i)
                    End If

                    If colDivisa.ColDecRemesa.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColDecRemesa.Valores(i)
                    End If

                    ' contados
                    If colDivisa.ColValContado.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColValContado.Valores(i)
                    End If

                    ' efetivo
                    If colDivisa.ColEfetivo.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColEfetivo.Valores(i)
                    End If

                    ' meios de pagamento
                    For Each colMP In colDivisa.ColMediosPago
                        If colMP.Valores.Count - 1 >= i Then
                            strRegistro &= colMP.Valores(i)
                        End If
                    Next

                    ' falsos 
                    If colDivisa.ColFalsos.Valores.Count - 1 >= i Then
                        strRegistro &= colDivisa.ColFalsos.Valores(i)
                    End If

                Next

            Next

            Return stbRelatorio.ToString()

        End Function

#End Region

#Region "[CLASSES AUXILIARES]"

        Private Class Coluna

            Public Titulo As String
            Public Id As String
            Public Valores As New List(Of String)

            Public Sub New()

            End Sub

            Public Sub New(DescTitulo As String, Identificador As String)

                Titulo = DescTitulo
                Id = Identificador

            End Sub

        End Class

        Private Class ColunaXDivisa

            Public Divisa As String

            Public ColDecParcial As New Coluna
            Public ColDecBulto As New Coluna
            Public ColDecRemesa As New Coluna
            Public ColValContado As New Coluna
            Public ColEfetivo As New Coluna
            Public ColMediosPago As New List(Of Coluna)
            Public ColDenominaciones As New List(Of Coluna)
            Public ColFalsos As New Coluna
            Public ColDiferencas As New Coluna

            Public Sub New(DescDivisa As String)

                Divisa = DescDivisa.ToUpper()

            End Sub

        End Class

#End Region

    End Class

#End Region

#Region "[ATRIBUTOS]"

    Public Structure Atributos

        Public ConIncidencias As Boolean
        Public ConDenominacion As Boolean
        Public FechaProcesoIni As DateTime
        Public FechaProcesoFin As DateTime
        Public FechaTransporteIni As DateTime
        Public FechaTransporteFin As DateTime
        Public Puesto As String
        Public CodCliente As String
        Public NombreCliente As String
        Public CodSubcliente As String
        Public NombreSubcliente As String
        Public Operario As String
        Public Delegacion As String
        Public HoraIni As String
        Public HoraFin As String
        Public NumRemesa As String
        Public NumPrecinto As String
        Public TipoFecha As Int16

    End Structure

#End Region

End Class

