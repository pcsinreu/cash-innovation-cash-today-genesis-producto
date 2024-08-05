Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario

Public Class ClientesXContenedores
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

#Region "[EVENTOS]"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ConfigurarControle_Sector()
        ConfigurarControle_Cliente()
        ConfigurarControle_Canal()

        If Not Me.IsPostBack Then
            AjustarControlFormato()
            AjustarControlDiscriminarPor()
            CargarEstado()

            'Ao entrar na tela, carregar os dados inciais
            If InformacionUsuario.SectorSeleccionado IsNot Nothing Then
                'Al ingresar en la pantalla, el campo será cargado con los datos de la delegación logada.
                Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)
                Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
                Me.ucSectores.AtualizarRegistrosDelegacion()

                'Al ingresar en la pantalla, el campo será cargado con los datos de la planta logada.
                Me.ucSectores.Plantas = New ObservableCollection(Of Clases.Planta)
                Dim planta = InformacionUsuario.SectorSeleccionado.Planta.Clonar
                'Identificador pai da delegação
                planta.CodigoMigracion = InformacionUsuario.DelegacionSeleccionada.Identificador
                Me.ucSectores.Plantas.Add(planta)
                Me.ucSectores.AtualizarRegistrosPlanta()

                Me.ucSectores.Sectores = New ObservableCollection(Of Clases.Sector)

                'Al ingresar en la pantalla, el campo será cargado con los datos del sector padre primer nivel do sector logado.
                Dim sector = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectorPadrePrimerNivel(Base.InformacionUsuario.SectorSeleccionado.Identificador)

                'Identificador pai do sector
                sector.CodigoMigracion = planta.Identificador
                Me.ucSectores.Sectores.Add(sector)
                Me.ucSectores.AtualizarRegistrosSector()

                'Foco no próximo controle vazio
                Me.ucCanal.Focus()
            Else
                Me.ucSectores.Focus()
            End If

        End If

        AnadirEventos()

        Me.ucFiltroDivisas.MostrarSolamenteDivisas = True

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
                Dim nomeRelatorio As String = If(ucDiscriminarPor.ItemSelecionado = "S", "rptContenedoresXClientes", "rptContenedoresXClientesSector")
                Dim fullPathReport As String = Nothing

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                    dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
                End If

                fullPathReport = String.Format("{0}/PACK_MODULAR/{1}", dirRelatorio, nomeRelatorio)

                Dim listaParametros As New List(Of RSE.ParameterValue)
                Dim bolCertificadoConsulta As Integer = 0

                'Recupera o pais da delegação que o usuário está logado.
                Dim pais As New Prosegur.Genesis.Comon.Clases.Pais
                If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)
                End If

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_IDIOMA", .Value = If(Tradutor.CulturaSistema IsNot Nothing AndAlso _
                                                                            Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name), _
                                                                            Tradutor.CulturaSistema.Name, _
                                                                            If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty))})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_SECTOR_CLIENTE_CANAL", .Value = If(ucDiscriminarPor.ItemSelecionado = "S", False, True)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_MAYORNIVEL", .Value = Convert.ToInt32(Not chkNoConsiderarHijos.Checked)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_PACKMODULAR", .Value = Convert.ToInt32(chkContenedoresPackModular.Checked)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_DIVISAS", .Value = ObtenerValorParametroDivisas(ucFiltroDivisas.IdentificadoresDivisas)})

                If Me.listaEstadoContenedor.valoresSeleccionados IsNot Nothing AndAlso Me.listaEstadoContenedor.valoresSeleccionados.Count > 0 Then

                    Dim CabecEstado = String.Empty
                    Dim ParamEstado = String.Empty

                    For Each estado In listaEstadoContenedor.valoresSeleccionados

                        If (CabecEstado = String.Empty) Then
                            CabecEstado = Traduzir(RecuperarEnum(Of Enumeradores.EstadoContenedor)(estado).ToString())
                            ParamEstado = estado
                        Else
                            CabecEstado += " | " + Traduzir(RecuperarEnum(Of Enumeradores.EstadoContenedor)(estado).ToString())
                            ParamEstado += "," + estado
                        End If

                    Next

                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODESTADOCONTENEDOR", .Value = ParamEstado})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecEstado", .Value = CabecEstado})

                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODESTADOCONTENEDOR", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecEstado", .Value = Nothing})
                End If

                'Controle Sector --recupera os sectores
                If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
                    Dim CabecSector = String.Empty
                    Dim ParamSector = String.Empty

                    For Each objSector In Me.Sectores
                        If (CabecSector = String.Empty) Then
                            CabecSector = objSector.Descripcion
                            ParamSector = objSector.Identificador
                        Else
                            CabecSector += " | " + objSector.Descripcion
                            ParamSector += "," + objSector.Identificador
                        End If
                    Next

                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSECTOR", .Value = ParamSector})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecSector", .Value = CabecSector})
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecSector", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSECTOR", .Value = Nothing})
                End If

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    Dim CabecCliente = String.Empty
                    For Each objCliente In Clientes
                        CabecCliente = objCliente.Descripcion
                        If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODCLIENTE", .Value = Nothing})

                            For Each objSubCliente In objCliente.SubClientes
                                If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSUBCLIENTE", .Value = Nothing})

                                    For Each objPtoServicio In objSubCliente.PuntosServicio
                                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODPUNTODESERVICIO", .Value = objPtoServicio.Identificador})
                                    Next
                                Else
                                    'se não escolheu punto de servicio então envia subcliente
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSUBCLIENTE", .Value = objSubCliente.Identificador})
                                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODPUNTODESERVICIO", .Value = Nothing})
                                End If
                            Next
                        Else
                            'se não escolheu niveis de subcliente então envia o cliente..
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODCLIENTE", .Value = objCliente.Identificador})
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSUBCLIENTE", .Value = Nothing})
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODPUNTODESERVICIO", .Value = Nothing})
                        End If
                    Next
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecCliente", .Value = CabecCliente})
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODCLIENTE", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODSUBCLIENTE", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODPUNTODESERVICIO", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecCliente", .Value = Nothing})
                End If

                If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
                    Dim CabecCanal = String.Empty
                    For Each objCanal In Canales
                        listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODCANAL", .Value = objCanal.Identificador})
                        CabecCanal = objCanal.Descripcion

                        If objCanal.SubCanales IsNot Nothing AndAlso objCanal.SubCanales.Count > 0 Then
                            For Each objSubCanal In objCanal.SubCanales
                                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_SUBCODCANAL", .Value = objSubCanal.Identificador})
                                CabecCanal += objSubCanal.Descripcion
                            Next
                        Else
                            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_SUBCODCANAL", .Value = Nothing})
                        End If
                    Next
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecCanal", .Value = CabecCanal})
                Else
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODCANAL", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_SUBCODCANAL", .Value = Nothing})
                    listaParametros.Add(New RSE.ParameterValue() With {.Name = "CampoCabecCanal", .Value = Nothing})
                End If

                Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
                Dim nomeArquivo = String.Format("{0}.{1}", Traduzir("076_lblSubTitulo"), extensao)
                Session(nomeArquivo) = Buffer
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "download_relatorio", String.Format("window.location.href = '../Download.aspx?NOME_ARQUIVO={0}'", nomeArquivo), True)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "[HELPERS]"
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

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_CLIENTE_X_CONTENEDORES
        MyBase.ValidarAcesso = True

    End Sub
    Protected Overrides Sub TraduzirControles()

        Me.Master.Titulo = Traduzir("076_Cabecera")
        Me.lblSubTitulo.Text = Traduzir("076_lblSubTitulo")
        Me.btnGenerarReporte.Text = Traduzir("057_btnGerar")
        Me.ucFormato.Titulo = Traduzir("057_formato")
        Me.listaEstadoContenedor.titulo = Traduzir("EstadoContenedor")
        Me.ucDiscriminarPor.Titulo = Traduzir("076_TituloUcDiscriminarPor")
        Me.chkNoConsiderarHijos.Text = Traduzir("076_TextChkNoConsiderarHijos")
        Me.chkContenedoresPackModular.Text = Traduzir("076_TextchkContenedoresPackModular")
    End Sub

#End Region

#Region "[MÉTODOS]"
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub
    Private Sub AjustarControlFormato()

        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("056_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("056_formato_excel")))
        Me.ucFormato.Opciones = lista

    End Sub
    Private Sub CargarParametroDivisa(ByRef Parametros As List(Of RSE.ParameterValue))

        Me.ucFiltroDivisas.GuardarDatos()
        If Me.ucFiltroDivisas.IdentificadoresDivisas IsNot Nothing AndAlso Me.ucFiltroDivisas.IdentificadoresDivisas.Count > 0 Then
            For Each objDivisa In Me.ucFiltroDivisas.IdentificadoresDivisas
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DIVISAS_MEDIOS", .Value = objDivisa})
            Next
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DIVISAS_MEDIOS", .Value = String.Empty})
        End If

        Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_TIPO_VALOR", .Value = Me.ucFiltroDivisas.TipoValor})

    End Sub
    Private Sub AjustarControlDiscriminarPor()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("S", Traduzir("075_OpcionDiscriminadoPorSector")))
        lista.Add(New KeyValuePair(Of String, String)("SCC", Traduzir("075_OpcionDiscriminadoPorSectorClienteYCanal")))
        Me.ucDiscriminarPor.Opciones = lista
    End Sub
    Private Sub CargarEstado()
        Dim lista As New Dictionary(Of String, String)
        lista.Add(Enumeradores.EstadoContenedor.Armado.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Armado.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.Anulado.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Anulado.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.Nuevo.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Nuevo.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.Pendiente.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Pendiente.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.Procesado.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Procesado.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.EnTransito.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.EnTransito.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.Desarmado.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.Desarmado.ToString()))
        lista.Add(Enumeradores.EstadoContenedor.NoDefinido.RecuperarValor(), Traduzir(Enumeradores.EstadoContenedor.NoDefinido.ToString()))
        listaEstadoContenedor.Lista = lista
    End Sub
    Private Sub AnadirEventos()
        AddHandler Me.ucFiltroDivisas.Erro, AddressOf ErroControles
    End Sub
    Private Function ValidarGerar() As String
        Dim retorno As String = String.Empty
        Me.ucFormato.GuardarDatos()
        Me.ucDiscriminarPor.GuardarDatos()
        Me.ucFiltroDivisas.GuardarDatos()

        Return retorno
    End Function

    Private Function ObtenerValorParametroDivisas(divisas As List(Of String)) As String
        Dim ParametroReportDivisa = String.Empty

        For Each objDivisa As String In divisas

            If (String.IsNullOrEmpty(ParametroReportDivisa)) Then
                ParametroReportDivisa += objDivisa
            Else
                ParametroReportDivisa += "," + objDivisa
            End If

        Next

        Return If(String.IsNullOrEmpty(ParametroReportDivisa), Nothing, ParametroReportDivisa)
    End Function

#End Region

End Class