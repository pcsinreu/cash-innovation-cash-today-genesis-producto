Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports System.Threading
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Public Class BusquedaReciboF22Respaldo
    Inherits Base

#Region "[ATRIBUTOS]"

    Private Valida As New List(Of String)
    Private Const strNomeExtensaoArquivo As String = "R.TXT"
    ' constantes do cabeçalho do arquivo
    Private Const CONST_LABEL_FILE_NAME As String = "# FILE NAME: {0}"
    Private Const CONST_LABEL_VERSION As String = "# VERSION NUMBER: {0}"
    Private Const CONST_LABEL_FILE_REFERENCE As String = "# FILE REFERENCE: {0}"
    Private Const CONST_LABEL_GENERATION_DATE As String = "# GENERATION DATE: {0}"
    Private Const CONST_LABEL_RECORDS As String = "# RECORDS: {0}"
    Private Const CONST_LABEL_CONTROL_1 As String = "# CONTROL 1: {0}"
    Private Const CONST_LABEL_CONTROL_2 As String = "# CONTROL 2: {0}"
    Private Const CONST_LABEL_END As String = "# END"
    Private Const CONST_SEPARADOR As String = ","
    Private Const CONST_DIRETORIO_LISTADO As String = "\Arquivos\"
    Private Const CONST_DICCIONARIO_COD_MEDIO_PAGO As String = "013_cod_"
    Private Const CONST_DICCIONARIO_DES_MEDIO_PAGO As String = "013_des_"

    Public Enum eDirecao As Integer
        Esquerda = 0
        Direita = 1
    End Enum

    Public Enum eTipoRegistro As Integer
        F22 = 0
        Respaldo = 1
    End Enum

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteSelecionado() As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

#End Region

#Region "[METODOS BASE]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        pbo = New PostBackOptions(btnBuscar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBuscar.OnClientClick = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnBuscar.ClientID & "," & btnLimpar.ClientID & "');"

        pbo = New PostBackOptions(btnLimpar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnLimpar.OnClientClick = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnBuscar.ClientID & "," & btnLimpar.ClientID & "');"

        'Adiciona a Precedencia ao Buscar
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnBuscar.ClientID & "';", True)

        ' Adiciona Scripts aos controles
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtFechaDesdeFinConteo.Enabled = True
                Me.txtFechaHastaFinConteo.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.RECIBO_F22_RESPALDO
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
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            ASPxGridView.RegisterBaseScript(Page)
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("013_titulo_pagina")

            ConfigurarControle_Cliente()

            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                ' Carrega os dados iniciais dos controles
                CarregarControles()

            End If

            ' setar foco no campo codigo
            Me.ddlDelegacion.Focus()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try

            Dim script As String = String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaDesdeFinConteo.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaHastaFinConteo.ClientID, "true", 2)

            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("013_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("013_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("013_lbl_delegacion")
        Me.lblFechaFinConteo.Text = Traduzir("013_lbl_fecha_fin_conteo")
        Me.lblFechaDesdeFinConteo.Text = Traduzir("lbl_desde")
        Me.lblFechaHastaFinConteo.Text = Traduzir("lbl_hasta")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.btnLimpar.Text = Traduzir("btnLimpiar")
        Me.btnBuscar.ToolTip = Traduzir("btnBuscar")
        Me.btnLimpar.ToolTip = Traduzir("btnLimpiar")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Me.ConsultarDados()
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Me.LimparCampos()
        Me.Response.Redireccionar(Me.ResolveUrl("BusquedaReciboF22Respaldo.aspx"))
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Limpa os campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub LimparCampos()

        ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        txtFechaDesdeFinConteo.Text = String.Empty
        txtFechaHastaFinConteo.Text = String.Empty
        Clientes = Nothing
        'Carregar o cliente com a chave do arquivo de configuração
        CarregarClienteArquivoConfiguracao()

    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        ' Define a mascara do período inicial digitado
        Me.txtFechaDesdeFinConteo.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaHastaFinConteo.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub CarregarControles()

        ' carregar delegações da coleção para o combo
        Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

        ' Inicializa o controle de delegação com a mesma delegação selecionada na tela de login
        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)

        ' Carrega o código do cliente informado no arquivo de configuração
        CarregarClienteArquivoConfiguracao()

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado    
    ''' </history>
    Private Sub ValidarControles()

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("013_lbl_delegacion")))
        End If

        ' Verifica se o cliente foi preenchido
        If Clientes Is Nothing OrElse Clientes.Count = 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("013_lbl_cliente")))
        End If

        ' Verifica se a data de contagem inicial foi preenchida
        If txtFechaDesdeFinConteo.Text = String.Empty Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("013_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de contagem final foi preenchida
        If txtFechaHastaFinConteo.Text = String.Empty Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de contagem inicial é uma data válida
        If txtFechaDesdeFinConteo.Text <> String.Empty AndAlso (txtFechaDesdeFinConteo.Text.Length < 10 OrElse Not (IsDate(txtFechaDesdeFinConteo.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("013_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaDesdeFinConteo.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de contagem inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaDesdeFinConteo.Text = txtFechaDesdeFinConteo.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de contagem final é uma data válida
        If txtFechaHastaFinConteo.Text <> String.Empty AndAlso (txtFechaHastaFinConteo.Text.Length < 10 OrElse Not (IsDate(txtFechaHastaFinConteo.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("013_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaHastaFinConteo.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de contagem final não foi informada
            ' coloca a hora final do dia
            txtFechaHastaFinConteo.Text = txtFechaHastaFinConteo.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de contagem inicial é maior do que a data de contagem inicial
        If (IsDate(txtFechaDesdeFinConteo.Text) AndAlso IsDate(txtFechaHastaFinConteo.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaDesdeFinConteo.Text), Convert.ToDateTime(txtFechaHastaFinConteo.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("013_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta"), Traduzir("013_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

    End Sub

    ''' <summary>
    ''' Recupera os dados do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub ConsultarDados()

        ' Nome do arquivo de relatório
        Dim strNomeArquivo As String = Date.Today.ToString("yyMMdd") & strNomeExtensaoArquivo

        Try
            ' Valida os controles usados no filtro
            Me.ValidarControles()

            ' Se não existe erro
            If Valida.Count = 0 Then

                ' Cria objeto para chamada do serviço
                Dim objReciboF22Respaldo As New ListadosConteo.ProxyReciboF22Respaldo

                ' Define os valores dos filtros da consulta
                Dim objPeticion As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Peticion
                objPeticion.CodigoDelegacion = ddlDelegacion.SelectedValue
                objPeticion.CodigoCliente = Clientes.FirstOrDefault().Codigo
                objPeticion.FechaDesde = Date.Parse(txtFechaDesdeFinConteo.Text)
                objPeticion.FechaHasta = Date.Parse(txtFechaHastaFinConteo.Text).AddSeconds(59)

                ' Recupera os dados do recibo F22 para popular o relatório
                Dim objRespuesta As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Respuesta = objReciboF22Respaldo.ListarReciboF22Respaldo(objPeticion)

                ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório 
                Dim msgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                    MyBase.MostraMensagem(msgErro)
                    Exit Sub
                End If

                ' se o diretório aina não existe, ele será criado
                If Not System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory() & CONST_DIRETORIO_LISTADO) Then
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory() & CONST_DIRETORIO_LISTADO)
                End If

                Dim arqRelReciboF22Respaldo As New System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory() & CONST_DIRETORIO_LISTADO & strNomeArquivo)

                Try

                    ' Escreve cabeçalho do arquivo
                    EscreverCabecalhoRelatorio(arqRelReciboF22Respaldo, objRespuesta.ReciboF22Respaldo)

                    ' escreve o conteúdo do arquivo
                    EscreverConteudoRelatorio(arqRelReciboF22Respaldo, objRespuesta.ReciboF22Respaldo)

                    ' adicionando linha para encerrar o arquivo
                    arqRelReciboF22Respaldo.WriteLine(CONST_LABEL_END)

                Finally

                    ' liberando o arquivo texto
                    arqRelReciboF22Respaldo.Flush()
                    arqRelReciboF22Respaldo.Close()

                End Try

                ' Define os parametros que serão passados para a exibição do relatório
                Dim parametros As String = "?NomeArquivo=" & strNomeArquivo

                ' Chama a página que exibirá o relatório
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('ReciboF22RespaldoMostrar.aspx" & parametros.Replace("'", "\'") & "') ;", True)

            Else

                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))

            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Responsável por gravar o conteúdo do cabeçalho no arquivo
    ''' </summary>
    ''' <param name="arqRelatorio"></param>
    ''' <remarks></remarks>
    Private Sub EscreverCabecalhoRelatorio(ByRef arqRelatorio As System.IO.StreamWriter, objReciboF22RespaldoColeccion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        ''(es la suma total de la columna 14 (Suma de Valores declarados de Sobres)
        ''(es la suma total de la columna 15 (Suma de Valores recontados de Sobres)
        Dim valorSomaDeclarados As Decimal = Decimal.Zero
        Dim valorSomaRecontados As Decimal = Decimal.Zero
        CarregarSomaValores(valorSomaDeclarados, valorSomaRecontados, objReciboF22RespaldoColeccion)

        'seta valores dos labels para o cabeçalho do arquivo TXT
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_FILE_NAME, Date.Today.ToString("yyMMdd") & strNomeExtensaoArquivo))
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_VERSION, "1.0"))
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_FILE_REFERENCE, "GENESIS – LISTADOS DE CONTEO"))
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_GENERATION_DATE, Date.Now.ToString("yyyyMMdd hh:mm:ss")))

        Dim numeroRegistros As Integer = 0
        For Each objReciboF22Respaldo As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In objReciboF22RespaldoColeccion
            numeroRegistros += objReciboF22Respaldo.ColMedioPagoDeclarados.Count
        Next
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_RECORDS, numeroRegistros.ToString()))

        arqRelatorio.WriteLine(String.Format(CONST_LABEL_CONTROL_1, valorSomaDeclarados.ToString("0.0").Replace(",", ".")))
        arqRelatorio.WriteLine(String.Format(CONST_LABEL_CONTROL_2, valorSomaRecontados.ToString("0.0").Replace(",", ".")))

    End Sub

    Private Sub CarregarSomaValores(ByRef valorSomaDeclarados As Decimal, _
                                    ByRef valorSomaRecontados As Decimal, _
                                    objReciboF22RespaldoColeccion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        If objReciboF22RespaldoColeccion.Count > 0 Then


            For i As Integer = 0 To objReciboF22RespaldoColeccion.Count - 1 Step 1



                For Each objEfecMedioPago As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado In objReciboF22RespaldoColeccion(i).ColMedioPagoDeclarados
                    valorSomaDeclarados += objEfecMedioPago.ValorDeclaradoSobres
                    valorSomaRecontados += objEfecMedioPago.ValorRecontadoSobres
                Next

            Next

        End If

    End Sub

    ''' <summary>
    ''' Responsável por gravar o conteúdo do corpo no arquivo
    ''' </summary>
    ''' <param name="arqRelatorio"></param>
    ''' <remarks></remarks>
    Private Sub EscreverConteudoRelatorio(ByRef arqRelatorio As System.IO.StreamWriter, objReciboF22RespaldoColeccion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        For Each objReciboF22Respaldo As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In objReciboF22RespaldoColeccion

            For Each objMedioPagoDeclarado As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado In objReciboF22Respaldo.ColMedioPagoDeclarados.OrderBy(Function(p) p.CodigoDivisa)

                arqRelatorio.Write(objReciboF22Respaldo.TipoRegistro & CONST_SEPARADOR)
                arqRelatorio.Write(AjutarDescricaoCampo(objReciboF22Respaldo.CodigoRecuento, 8) & CONST_SEPARADOR)
                arqRelatorio.Write(objReciboF22Respaldo.FechaRecaudacion.ToString("dd/MM/yyyy") & CONST_SEPARADOR)
                arqRelatorio.Write(objReciboF22Respaldo.FechaSesion.ToString("dd/MM/yyyy") & CONST_SEPARADOR)
                arqRelatorio.Write(objReciboF22Respaldo.LetraReciboTransporte & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(AjutarDescricaoCampo(objReciboF22Respaldo.NumReciboTransporte, 10), 10, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(objReciboF22Respaldo.SucursalCliente, 3, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(AjutarDescricaoCampo(objReciboF22Respaldo.DescripcionSucursal, 20), 20, eDirecao.Direita) & CONST_SEPARADOR)

                If eTipoRegistro.F22 = Integer.Parse(objReciboF22Respaldo.TipoRegistro) Then
                    arqRelatorio.Write(PreencheEspacos(String.Empty, 6, eDirecao.Direita) & CONST_SEPARADOR)
                    arqRelatorio.Write(PreencheEspacos(String.Empty, 10, eDirecao.Direita) & CONST_SEPARADOR)
                Else

                    arqRelatorio.Write(PreencheEspacos(objReciboF22Respaldo.Legajo, 6, eDirecao.Esquerda) & CONST_SEPARADOR)
                    If objReciboF22Respaldo.NumSobre.ToString().Equals("0") Then
                        arqRelatorio.Write(PreencheEspacos(String.Empty, 10, eDirecao.Esquerda) & CONST_SEPARADOR)
                    Else
                        arqRelatorio.Write(PreencheEspacos(objReciboF22Respaldo.NumSobre, 10, eDirecao.Esquerda) & CONST_SEPARADOR)
                    End If

                End If

                'Diferencia (recontado(#15) - declarado(#14))
                objMedioPagoDeclarado.DiferenciaRecontadoDeclarado = objMedioPagoDeclarado.ValorRecontadoSobres - objMedioPagoDeclarado.ValorDeclaradoSobres

                If Not objMedioPagoDeclarado.CodigoMedioPago.Equals(String.Empty) Then

                    Dim codMedioPago As String = Traduzir(CONST_DICCIONARIO_COD_MEDIO_PAGO & objMedioPagoDeclarado.CodigoMedioPago)
                    Dim descMedioPago As String = Traduzir(CONST_DICCIONARIO_DES_MEDIO_PAGO & objMedioPagoDeclarado.CodigoMedioPago)

                    ' se é medio pago que não está descrito no dicionário
                    If codMedioPago.IndexOf("[") <> -1 Then
                        arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.CodigoMedioPago, 5, eDirecao.Direita) & CONST_SEPARADOR)
                        arqRelatorio.Write(PreencheEspacos(IIf(objMedioPagoDeclarado.DescripcionMedioPago.StartsWith("013_"), Traduzir(objMedioPagoDeclarado.DescripcionMedioPago), objMedioPagoDeclarado.DescripcionMedioPago), 15, eDirecao.Direita) & CONST_SEPARADOR)
                    Else
                        arqRelatorio.Write(PreencheEspacos(codMedioPago, 5, eDirecao.Direita) & CONST_SEPARADOR)
                        arqRelatorio.Write(PreencheEspacos(descMedioPago, 15, eDirecao.Direita) & CONST_SEPARADOR)
                    End If

                Else
                    arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.CodigoMedioPago, 5, eDirecao.Direita) & CONST_SEPARADOR)
                    arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.DescripcionMedioPago, 15, eDirecao.Direita) & CONST_SEPARADOR)
                End If

                arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.ValorDeclaradoF22.ToString("0.00").Replace(",", "."), 11, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.ValorDeclaradoSobres.ToString("0.00").Replace(",", "."), 11, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.ValorRecontadoSobres.ToString("0.00").Replace(",", "."), 11, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.Write(PreencheEspacos(objMedioPagoDeclarado.DiferenciaRecontadoDeclarado.ToString("0.00").Replace(",", "."), 11, eDirecao.Esquerda) & CONST_SEPARADOR)
                arqRelatorio.WriteLine(PreencheEspacos(AjutarDescricaoCampo(objReciboF22Respaldo.Observaciones, 45), 45, eDirecao.Direita))

            Next

        Next

    End Sub

    ''' <summary>
    ''' Ajuta o tamanho da descrição do processo para o permitido no arquivo de relatório
    ''' </summary>
    ''' <param name="descricaoCampo">Descrição a ser impressa no relatório</param>
    ''' <param name="tamanho">Tamanho máximo permitido para o campo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AjutarDescricaoCampo(descricaoCampo As String, tamanho As Integer)

        If descricaoCampo.Length > tamanho Then
            descricaoCampo = descricaoCampo.Substring(0, tamanho)
        End If

        Return descricaoCampo

    End Function

    ''' <summary>
    ''' Preenche uma string com espaços vazios à esquerda até o tamanho desejado
    ''' </summary>
    ''' <param name="strValor">string a completar com espaços</param>
    ''' <param name="tamanho">Qual o tamanho da string com os espaços</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PreencheEspacos(strValor As String, _
                                     tamanho As Integer, _
                                     direccion As eDirecao)

        While strValor.Length < tamanho

            If direccion = eDirecao.Esquerda Then
                strValor = " " & strValor
            Else
                strValor &= " "
            End If

        End While

        Return strValor

    End Function


    ''' <summary>
    ''' Busca o cliente de acordo com o código na Chave do WebConfig
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub CarregarClienteArquivoConfiguracao()

        ' Caso esté informado algún código en el parámetro “Cliente_Defalt_F22_Respaldo” del parámetro de configuración
        If Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Cliente_Default_F22_Respaldo") IsNot Nothing Then

            Dim strCodCliente As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Cliente_Default_F22_Respaldo").ToString()

            If Not String.IsNullOrEmpty(strCodCliente) Then

                'el sistema realizará una búsqueda por cliente con el código informado
                Dim objRespuestaCliente As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta
                objRespuestaCliente = GetComboClientes(strCodCliente)

                ' Se não houve erro na execução do serviço
                If objRespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                    ' valida se o serviço encontrou algum cliente
                    If objRespuestaCliente.Clientes.Count > 0 Then

                        'Como o serviço retorna todos os cliente que tem o código parecido com o informado, neste caso no
                        'formulário o código deve ser idêntico ao configurado no Web.Config
                        For Each objCliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente In objRespuestaCliente.Clientes

                            If objCliente.Codigo.Equals(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Cliente_Default_F22_Respaldo").ToString()) Then

                                ' Seta a viewstate da página para o cliente selecionado
                                ClienteSelecionado = objCliente

                            End If

                        Next

                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Obtém os dados de cliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Function GetComboClientes(strCodigoCliente As String) As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.Vigente = True

        If Not String.IsNullOrEmpty(strCodigoCliente) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(strCodigoCliente.ToUpper)
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

#End Region
#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.PtoServicioHabilitado = False
        Me.ucClientes.PtoServicoObrigatorio = False


        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Exception, e.Erro.Message, e.Erro.ToString()))
    End Sub
#End Region
End Class