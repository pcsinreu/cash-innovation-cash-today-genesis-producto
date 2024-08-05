Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class SaldosCliente
    Inherits Base


#Region "[PROPRIEDADES]"

    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    Public Property Plantas As ObservableCollection(Of Clases.Planta)
        Get
            Return ucSectores.Plantas
        End Get
        Set(value As ObservableCollection(Of Clases.Planta))
            ucSectores.Plantas = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Public Property Canales As ObservableCollection(Of Clases.Canal)
        Get
            Return ucCanal.Canales
        End Get
        Set(value As ObservableCollection(Of Clases.Canal))
            ucCanal.Canales = value
        End Set
    End Property

    Private WithEvents _ucCanal As ucCanal
    Public Property ucCanal() As ucCanal
        Get
            If _ucCanal Is Nothing Then
                _ucCanal = LoadControl("~\Controles\ucCanal.ascx")
                _ucCanal.ID = "ucCanal"
                AddHandler _ucCanal.Erro, AddressOf ErroControles
                phCanal.Controls.Add(_ucCanal)
            End If
            Return _ucCanal
        End Get
        Set(value As ucCanal)
            _ucCanal = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = "ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private WithEvents _ucSectores As ucSector
    Public Property ucSectores() As ucSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucSector.ascx")
                _ucSectores.ID = "ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As ucSector)
            _ucSectores = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_SALDOS_CLIENTE
        MyBase.ValidarAcesso = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("056_lblTitulo")
        Me.lblSubTitulo.Text = Traduzir("056_lblSubTitulo")
        Me.ucFormato.Titulo = Traduzir("056_formato")
        Me.btnGenerarReporte.Text = Traduzir("056_btnGerar")
        Me.chkDetalharSetor.Text = Traduzir("056_detalhar_sector")
        Me.chkDetalharDivisa.Text = Traduzir("056_detalhar_divisa")
        Me.chkClienteSemSaldo.Text = Traduzir("056_considerar_cliente_sem_saldo")
        Me.chkTodosNiveis.Text = Traduzir("056_considerar_todos_niveis")
        Me.lgTipoValor.InnerText = Traduzir("056_lengda_tipo_valor")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        GoogleAnalyticsHelper.TrackAnalytics(Me, "Saldos Cliente")
        ConfigurarControle_Sector()
        ConfigurarControle_Cliente()
        ConfigurarControle_Canal()

        If Not Me.IsPostBack Then
            AjustarControlFormato()

            'Ao entrar na tela, carregar os dados inciais
            If InformacionUsuario.Delegaciones IsNot Nothing Then
                'Al ingresar en la pantalla, el campo será cargado con los datos de la delegación logada.
                Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)
                Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
                Me.ucSectores.AtualizarRegistrosDelegacion()

                'Al ingresar en la pantalla, el campo será cargado con los datos de la planta logada.
                Me.ucSectores.Plantas = New ObservableCollection(Of Clases.Planta)
                Dim planta = InformacionUsuario.DelegacionSeleccionada.Plantas(0).Clonar
                'Identificador pai da delegação
                planta.CodigoMigracion = InformacionUsuario.DelegacionSeleccionada.Identificador
                Me.ucSectores.Plantas.Add(planta)
                Me.ucSectores.AtualizarRegistrosPlanta()

                Me.ucSectores.Sectores = New ObservableCollection(Of Clases.Sector)

                'Al ingresar en la pantalla, el campo será cargado con los datos del sector padre primer nivel do sector logado.
                'Dim sector = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectorPadrePrimerNivel(Base.InformacionUsuario.SectorSeleccionado.Identificador)

                ''Identificador pai do sector
                'sector.CodigoMigracion = planta.Identificador
                'Me.ucSectores.Sectores.Add(sector)
                'Me.ucSectores.AtualizarRegistrosSector()

                'Foco no próximo controle vazio
                Me.ucCanal.Focus()
            Else
                Me.ucSectores.Focus()
            End If
        End If

        AddHandler ucFiltroDivisas.Erro, AddressOf ErroControles
    End Sub

    Protected Sub btnGenerarReporte_Click(sender As Object, e As System.EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim mensagem As String = ValidarGerar()

            If Not String.IsNullOrEmpty(mensagem) Then
                MyBase.MostraMensagemErro(mensagem)
            Else
                ' Recupera os parametros do relatório.
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)

                'Lista os parametros do relatório
                Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
                Dim nomeRelatorio As String = "SALDOS V2"
                Dim fullPathReport As String = Nothing

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                    dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
                End If

                fullPathReport = String.Format("{0}/SALDOS_V2/{1}", dirRelatorio, nomeRelatorio)

                Dim listaParametros As New List(Of RSE.ParameterValue)
                Dim bolCertificadoConsulta As Integer = 0

                'Recupera o pais da delegação que o usuário está logado.
                Dim pais As New Prosegur.Genesis.Comon.Clases.Pais
                If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)
                End If

                'Controle Sector --recupera as delegações
                If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then
                    For Each objDelegacion In Me.Delegaciones
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DELEGACION", .Value = objDelegacion.Identificador})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DELEGACION", .Value = Nothing})
                End If

                'Controle Sector --recupera as plantas
                If Me.Plantas IsNot Nothing AndAlso Me.Plantas.Count > 0 Then
                    For Each objPlanta In Me.Plantas
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PLANTA", .Value = objPlanta.Identificador})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PLANTA", .Value = String.Empty})
                End If

                'Controle Sector --recupera os sectores
                If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
                    For Each objSector In Me.Sectores
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SECTOR", .Value = objSector.Identificador})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SECTOR", .Value = String.Empty})
                End If

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    For Each objCliente In Clientes
                        If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = String.Empty})

                            For Each objSubCliente In objCliente.SubClientes
                                If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCLIENTE", .Value = String.Empty})

                                    For Each objPtoServicio In objSubCliente.PuntosServicio
                                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PTO_SERVICIO", .Value = objPtoServicio.Identificador})
                                    Next
                                Else
                                    'se não escolheu punto de servicio então envia subcliente
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCLIENTE", .Value = objSubCliente.Identificador})
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PTO_SERVICIO", .Value = String.Empty})
                                End If
                            Next
                        Else
                            'se não escolheu niveis de subcliente então envia o cliente..
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = objCliente.Identificador})

                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCLIENTE", .Value = String.Empty})
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PTO_SERVICIO", .Value = String.Empty})
                        End If
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = String.Empty})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCLIENTE", .Value = String.Empty})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PTO_SERVICIO", .Value = String.Empty})
                End If

                If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
                    For Each objCanal In Canales
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CANAL", .Value = objCanal.Identificador})

                        If objCanal.SubCanales IsNot Nothing AndAlso objCanal.SubCanales.Count > 0 Then
                            For Each objSubCanal In objCanal.SubCanales
                                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCANAL", .Value = objSubCanal.Identificador})
                            Next
                        Else
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCANAL", .Value = String.Empty})
                        End If
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CANAL", .Value = String.Empty})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUBCANAL", .Value = String.Empty})
                End If

                If Me.ucFiltroDivisas.IdentificadoresDivisas IsNot Nothing AndAlso Me.ucFiltroDivisas.IdentificadoresDivisas.Count > 0 Then
                    For Each divisa In Me.ucFiltroDivisas.IdentificadoresDivisas
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DIVISA", .Value = divisa})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DIVISA", .Value = String.Empty})
                End If

                If Me.ucFiltroDivisas.IdentificadoresEfectivos IsNot Nothing AndAlso Me.ucFiltroDivisas.IdentificadoresEfectivos.Count > 0 Then
                    For Each denominacion In Me.ucFiltroDivisas.IdentificadoresEfectivos
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DENOMINACION", .Value = denominacion})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DENOMINACION", .Value = String.Empty})
                End If

                If Me.ucFiltroDivisas.IdentificadoresMediosPago IsNot Nothing AndAlso Me.ucFiltroDivisas.IdentificadoresMediosPago.Count > 0 Then
                    For Each medioPago In Me.ucFiltroDivisas.IdentificadoresMediosPago
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_MEDIO_PAGO", .Value = medioPago})
                    Next
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_MEDIO_PAGO", .Value = String.Empty})
                End If

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_TOTAL_EFECTIVO", .Value = Me.ucFiltroDivisas.TotalesEfectivo})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_TOTAL_MEDIO_PAGO", .Value = Me.ucFiltroDivisas.TotalesTipoMedioPago})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_CLIENTE_SEM_SALDO", .Value = Me.chkClienteSemSaldo.Checked})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_DETALHAR_DIVISA", .Value = Me.chkDetalharDivisa.Checked})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_DETALHAR_SECTOR", .Value = Me.chkDetalharSetor.Checked})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSID_EFECTIVO_MEDIO_PAGO", .Value = If(rbTiposDatosAmbos.Checked, "1", If(rbTipoDatosEfectivos.Checked, "2", "3"))})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_TODOS_NIVEIS", .Value = Me.chkTodosNiveis.Checked})

                Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
                Dim nomeArquivo = String.Format("{0}.{1}", nomeRelatorio, extensao)
                Session(nomeArquivo) = Buffer
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "download_relatorio", String.Format("window.location.href = '../Download.aspx?NOME_ARQUIVO={0}'", nomeArquivo), True)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Public Sub ucCanal_OnControleAtualizado() Handles _ucCanal.UpdatedControl
        Try
            If ucCanal.Canales IsNot Nothing Then
                Canales = ucCanal.Canales
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try
            If Me.ucSectores.Delegaciones IsNot Nothing Then
                Delegaciones = Me.ucSectores.Delegaciones
            End If
            If Me.ucSectores.Plantas IsNot Nothing Then
                Plantas = Me.ucSectores.Plantas
            End If
            If Me.ucSectores.Sectores IsNot Nothing Then
                Sectores = Me.ucSectores.Sectores
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[METODOS]"

#Region "     Helpers     "

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.SolamenteSectoresPadre = True
        Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True
        Me.ucSectores.PlantaHabilitado = True

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        If Plantas IsNot Nothing Then
            Me.ucSectores.Plantas = Plantas
        End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If

    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.PtoServicioHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

    Protected Sub ConfigurarControle_Canal()

        Me.ucCanal.SelecaoMultipla = True
        Me.ucCanal.CanalHabilitado = True
        Me.ucCanal.SubCanalHabilitado = True

        If Canales IsNot Nothing Then
            Me.ucCanal.Canales = Canales
        End If

    End Sub

#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

    Private Sub AjustarControlFormato()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("056_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("056_formato_excel")))
        Me.ucFormato.Opciones = lista
    End Sub

    Private Function ValidarGerar() As String
        Dim mensagem As String = String.Empty
        Dim sbMensagens As New StringBuilder

        'Verifica se os clientes selecionados são totalizadores de saldo
        If Me.ucClientes.Clientes IsNot Nothing AndAlso Me.ucClientes.Clientes.Count > 0 Then
            'se for considerar todos os níveis então verificar se os clientes sem subCliente nem punto servicio
            'são totalizadores de saldo ou se possui subCliente totalizador ou se possui punto de servicio totalizador..

            Dim clientesSemSubCliente = Me.ucClientes.Clientes.Where(Function(c) c.SubClientes Is Nothing OrElse c.SubClientes.Count = 0).ToList
            If Me.chkTodosNiveis.Checked Then
                If clientesSemSubCliente IsNot Nothing AndAlso clientesSemSubCliente.Count > 0 Then
                    Dim identificadoresCliente = clientesSemSubCliente.Select(Function(c) c.Identificador).ToList
                    Dim IdentificadoresClienteTotalizadorSaldo = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.IdentificadoresClienteOuSubClienteouPuntoServicioTotalizadorSaldo(identificadoresCliente)
                    If IdentificadoresClienteTotalizadorSaldo IsNot Nothing AndAlso IdentificadoresClienteTotalizadorSaldo.Count > 0 Then
                        'Verifica se algum cliente selecionado não está na lista de totalizadores
                        For Each clienteNaoTotalizador In clientesSemSubCliente.Where(Function(c) Not IdentificadoresClienteTotalizadorSaldo.Contains(c.Identificador))
                            mensagem += String.Format("<br/>{0}-{1}", clienteNaoTotalizador.Codigo, clienteNaoTotalizador.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            sbMensagens.Append(String.Format(Traduzir("056_clienteSubClientePontoServico_nao_totalizador"), mensagem))
                            mensagem = String.Empty
                        End If
                    Else
                        For Each clienteNaoTotalizador In clientesSemSubCliente
                            mensagem += String.Format("<br/>{0}-{1}", clienteNaoTotalizador.Codigo, clienteNaoTotalizador.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            sbMensagens.Append(String.Format(Traduzir("056_clienteSubClientePontoServico_nao_totalizador"), mensagem))
                            mensagem = String.Empty
                        End If
                    End If
                End If

                'seleciona os subclientes que não possui pontos de servico
                Dim subClientesSemPuntoServicio As New List(Of Clases.SubCliente)
                For Each cliente In Me.ucClientes.Clientes.Where(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0)
                    For Each subCliente In cliente.SubClientes.Where(Function(s) s.PuntosServicio Is Nothing OrElse s.PuntosServicio.Count = 0)
                        subClientesSemPuntoServicio.Add(subCliente)
                    Next
                Next

                If subClientesSemPuntoServicio.Count > 0 Then
                    Dim identificadoresSubCliente = subClientesSemPuntoServicio.Select(Function(s) s.Identificador).ToList

                    Dim IdentificadoresSubClienteTotalizadorSaldo = Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.IdentificadoresSubClientePuntoServicioTotalizadorSaldo(identificadoresSubCliente)
                    If IdentificadoresSubClienteTotalizadorSaldo IsNot Nothing AndAlso IdentificadoresSubClienteTotalizadorSaldo.Count > 0 Then
                        'Verifica se algum subcliente selecionado não está na lista de totalizadores

                        For Each objSubCliente In subClientesSemPuntoServicio.Where(Function(s) Not IdentificadoresSubClienteTotalizadorSaldo.Contains(s.Identificador))
                            mensagem += String.Format("<br/>{0}-{1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            If sbMensagens.Length > 0 Then
                                sbMensagens.AppendLine("<br/>")
                            End If

                            sbMensagens.Append(String.Format(Traduzir("056_SubClientePontoServico_nao_totalizador"), mensagem))
                            mensagem = String.Empty
                        End If
                    Else
                        For Each objSubCliente In subClientesSemPuntoServicio
                            mensagem += String.Format("<br/>{0}-{1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            If sbMensagens.Length > 0 Then
                                sbMensagens.AppendLine("<br/>")
                            End If

                            sbMensagens.Append(String.Format(Traduzir("056_SubClientePontoServico_nao_totalizador"), mensagem))
                            mensagem = String.Empty
                        End If
                    End If
                End If
            Else
                If clientesSemSubCliente IsNot Nothing AndAlso clientesSemSubCliente.Count > 0 Then
                    Dim identificadoresCliente = clientesSemSubCliente.Select(Function(c) c.Identificador).ToList
                    Dim IdentificadoresClienteTotalizadorSaldo = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.IdentificadoresClienteTotalizadorSaldo(identificadoresCliente)
                    If IdentificadoresClienteTotalizadorSaldo IsNot Nothing AndAlso IdentificadoresClienteTotalizadorSaldo.Count > 0 Then
                        'Verifica se algum cliente selecionado não está na lista de totalizadores
                        For Each clienteNaoTotalizador In clientesSemSubCliente.Where(Function(c) Not IdentificadoresClienteTotalizadorSaldo.Contains(c.Identificador))
                            mensagem += String.Format("<br/>{0}-{1}", clienteNaoTotalizador.Codigo, clienteNaoTotalizador.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            sbMensagens.Append(String.Format(Traduzir("056_cliente_nao_totalizador_saldo"), mensagem))
                            mensagem = String.Empty
                        End If
                    Else
                        For Each clienteNaoTotalizador In clientesSemSubCliente
                            mensagem += String.Format("<br/>{0}-{1}", clienteNaoTotalizador.Codigo, clienteNaoTotalizador.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            sbMensagens.Append(String.Format(Traduzir("056_cliente_nao_totalizador_saldo"), mensagem))
                            mensagem = String.Empty
                        End If
                    End If
                End If

                'seleciona os subclientes que não possui pontos de servico
                Dim subClientesSemPuntoServicio As New List(Of Clases.SubCliente)
                For Each cliente In Me.ucClientes.Clientes.Where(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0)
                    For Each subCliente In cliente.SubClientes.Where(Function(s) s.PuntosServicio Is Nothing OrElse s.PuntosServicio.Count = 0)
                        subClientesSemPuntoServicio.Add(subCliente)
                    Next
                Next

                If subClientesSemPuntoServicio.Count > 0 Then
                    Dim identificadoresSubCliente = subClientesSemPuntoServicio.Select(Function(s) s.Identificador).ToList

                    Dim IdentificadoresSubClienteTotalizadorSaldo = Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.IdentificadoresSubClienteTotalizadorSaldo(identificadoresSubCliente)
                    If IdentificadoresSubClienteTotalizadorSaldo IsNot Nothing AndAlso IdentificadoresSubClienteTotalizadorSaldo.Count > 0 Then
                        'Verifica se algum subcliente selecionado não está na lista de totalizadores

                        For Each objSubCliente In subClientesSemPuntoServicio.Where(Function(s) Not IdentificadoresSubClienteTotalizadorSaldo.Contains(s.Identificador))
                            mensagem += String.Format("<br/>{0}-{1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            If sbMensagens.Length > 0 Then
                                sbMensagens.AppendLine("<br/>")
                            End If

                            sbMensagens.Append(String.Format(Traduzir("056_subcliente_nao_totalizador_saldo"), mensagem))
                            mensagem = String.Empty
                        End If
                    Else
                        For Each objSubCliente In subClientesSemPuntoServicio
                            mensagem += String.Format("<br/>{0}-{1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                        Next

                        If Not String.IsNullOrEmpty(mensagem) Then
                            If sbMensagens.Length > 0 Then
                                sbMensagens.AppendLine("<br/>")
                            End If

                            sbMensagens.Append(String.Format(Traduzir("056_subcliente_nao_totalizador_saldo"), mensagem))
                            mensagem = String.Empty
                        End If
                    End If
                End If
            End If

            'seleciona os punto de servicio 
            Dim puntoServicio As New List(Of Clases.PuntoServicio)
            For Each cliente In Me.ucClientes.Clientes.Where(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0)
                For Each subCliente In cliente.SubClientes.Where(Function(s) s.PuntosServicio IsNot Nothing AndAlso s.PuntosServicio.Count > 0)
                    puntoServicio.AddRange(subCliente.PuntosServicio)
                Next
            Next

            'se existe algum punto de servcio então verificar se eles são totalizadores de saldo.
            If puntoServicio.Count > 0 Then
                Dim identificadoresPuntoServicio = puntoServicio.Select(Function(s) s.Identificador).ToList

                Dim IdentificadoresPuntoServicioTotalizadorSaldo = Prosegur.Genesis.LogicaNegocio.Genesis.PuntoServicio.IdentificadoresPuntoServicioTotalizadorSaldo(identificadoresPuntoServicio)
                If IdentificadoresPuntoServicioTotalizadorSaldo IsNot Nothing AndAlso IdentificadoresPuntoServicioTotalizadorSaldo.Count > 0 Then
                    'Verifica se algum punto de servicio selecionado não está na lista de totalizadores

                    For Each objSubCliente In puntoServicio.Where(Function(s) Not IdentificadoresPuntoServicioTotalizadorSaldo.Contains(s.Identificador))
                        mensagem += String.Format("<br/>{0}- {1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                    Next

                    If Not String.IsNullOrEmpty(mensagem) Then
                        If sbMensagens.Length > 0 Then
                            sbMensagens.AppendLine("<br/>")
                        End If

                        sbMensagens.Append(String.Format(Traduzir("056_pontoServicio_nao_totalizador_saldo"), mensagem))
                        mensagem = String.Empty
                    End If
                Else
                    For Each objSubCliente In puntoServicio
                        mensagem += String.Format("<br/>{0}- {1}", objSubCliente.Codigo, objSubCliente.Descripcion)
                    Next

                    If Not String.IsNullOrEmpty(mensagem) Then
                        If sbMensagens.Length > 0 Then
                            sbMensagens.AppendLine("<br/>")
                        End If

                        sbMensagens.Append(String.Format(Traduzir("056_pontoServicio_nao_totalizador_saldo"), mensagem))
                        mensagem = String.Empty
                    End If
                End If
            End If

        End If

        Me.ucFiltroDivisas.GuardarDatos()
        Me.ucFormato.GuardarDatos()

        If Me.Delegaciones Is Nothing OrElse Me.Delegaciones.Count = 0 Then
            If sbMensagens.Length > 0 Then
                sbMensagens.AppendLine("<br/>")
            End If
            sbMensagens.Append(Traduzir("056_campoDelegacion_obrigatorio"))
        End If

        If Me.ucFiltroDivisas.IdentificadoresDivisas Is Nothing OrElse Me.ucFiltroDivisas.IdentificadoresDivisas.Count = 0 Then
            If sbMensagens.Length > 0 Then
                sbMensagens.AppendLine("<br/>")
            End If
            sbMensagens.Append(Traduzir("056_campoDivisa_obrigatorio"))
        End If

        Return sbMensagens.ToString
    End Function

#End Region

#End Region

End Class