Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario

Public Class Transacciones
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_TRANSACCIONES
        MyBase.ValidarAcesso = True

    End Sub

    Protected Overrides Sub TraduzirControles()
        Me.Master.Titulo = Traduzir("057_lblTitulo")
        Me.lblSubTitulo.Text = Traduzir("057_lblSubTitulo")
        Me.btnGenerarReporte.Text = Traduzir("057_btnGerar")

        Me.ucFechaDesde.Titulo = Traduzir("057_fechahoradesde")
        Me.ucFechaHasta.Titulo = Traduzir("057_fechahorahasta")
        Me.ucFormularios.Titulo = Traduzir("057_formularios")
        Me.lblTituloFiltroFecha.Text = Traduzir("057_lblTituloFiltroFecha")
        Me.lblNotificado.Text = Traduzir("057_lblNotificado")
        Me.lblAcreditado.Text = Traduzir("057_lblAcreditado")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Saldos Transacciones")
        Me.ConfigurarControle_Sector()
        Me.ConfigurarControle_Cliente()
        Me.ConfigurarControle_Canal()

        If Not Me.IsPostBack Then
            Me.CargarFechas()
            Me.CargarFiltroFecha()
            Me.CargarFormularios()
            Me.CargarColumnasAdicionales()
            Me.ucFiltroDivisas.FiltroTransacionesExibir()
            Me.ucSectores.Focus()
        End If

        Me.AnadirEventos()

        Me.ucFiltroDivisas.MostrarTiposValores = True

    End Sub

    Protected Sub btnGenerarReporte_Click(sender As Object, e As System.EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim Mesajes As StringBuilder = Validacion()

            If Mesajes.Length > 0 Then
                MyBase.MostraMensagemErro(Mesajes.ToString)

            Else

                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)

                'Lista os parametros do relatório
                Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
                Dim nomeRelatorio As String = "REL_TRANSACCIONES_48"
                Dim fullPathReport As String = String.Empty

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                    dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
                End If

                fullPathReport = String.Format("{0}/TRANSACCIONES_V5/{1}", dirRelatorio, nomeRelatorio)

                Dim Parametros As New List(Of RSE.ParameterValue)

                'Recupera o pais da delegação que o usuário está logado.
                Dim pais As New Prosegur.Genesis.Comon.Clases.Pais
                If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)

                End If

                Dim codigoCultura As String = If(Tradutor.CulturaSistema IsNot Nothing AndAlso _
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name), _
                                                                                Tradutor.CulturaSistema.Name, _
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty))

                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COD_CULTURA", .Value = codigoCultura})
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_OID_DELEGACION_LOGADA", .Value = Base.InformacionUsuario.DelegacionSeleccionada.Identificador})

                Me.CargarParametroDelegacion(Parametros)
                Me.CargarParametroPlanta(Parametros)
                Me.CargarParametroSector(Parametros)
                Me.CargarParametroCliente(Parametros)
                Me.CargarParametroCanal(Parametros)
                Me.CargarParametroFecha(Parametros)
                Me.CargarParametroDisponible(Parametros)
                Me.CargarParametroFormulario(Parametros)
                Me.CargarParametroDivisa(Parametros)
                Me.CargarParametrosColumnasAdicionales(Parametros)
                Me.CargarParametrosFiltroFecha(Parametros)

                If ucFiltroDivisas.NoConsiderarSectoresHijos Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_SECTORES_HIJOS", .Value = "0"})
                Else
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_SECTORES_HIJOS", .Value = "1"})
                End If

                If rbNotificadoAmbos.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_NOTIFICADOS", .Value = "2"})
                ElseIf rbNotificado.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_NOTIFICADOS", .Value = "1"})
                ElseIf rbNoNotificado.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_NOTIFICADOS", .Value = "0"})
                End If

                If rbAcreditadoAmbos.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_ACREDITADOS", .Value = "2"})
                ElseIf rbAcreditado.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_ACREDITADOS", .Value = "1"})
                ElseIf rbAcreditado.Checked Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_CONSIDERAR_ACREDITADOS", .Value = "0"})
                End If

                Dim Extension As String = If(Me.ucFiltroDivisas.FormatoItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFiltroDivisas.FormatoItemSelecionado, Parametros, Extension)
                Dim nomeArquivo = String.Format("{0}.{1}", nomeRelatorio, Extension)
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

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ucSubCliente.MultiSelecao = True
        Me.ucClientes.ucPtoServicio.MultiSelecao = True
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

#Region "     TextBox     "

    Private Sub CargarFechas()

        Me.ucFechaDesde.ValorPadron = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).AddDays(-1) '' DateTime.UtcNow.AddDays(-1)Base.InformacionUsuario.DelegacionSeleccionada.Codigo
        Me.ucFechaHasta.ValorPadron = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada)

    End Sub

#End Region

#Region "     CheckBoxList     "

    Private Sub CargarFormularios()

        Dim Formularios As List(Of Clases.Formulario) = Genesis.LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularios

        If Formularios IsNot Nothing AndAlso Formularios.Count > 0 Then
            Dim kvpFormularios As New List(Of KeyValuePair(Of String, String))
            For Each formulario In Formularios.OrderBy(Function(x) x.Descripcion)
                kvpFormularios.Add(New KeyValuePair(Of String, String)(formulario.Identificador, formulario.Descripcion))
            Next formulario

            Me.ucFormularios.Valores = kvpFormularios
            Me.ucFormularios.Mesaje = Traduzir("057_sinformularios")

        End If

    End Sub

#End Region

#Region "     General     "

    Private Sub AnadirEventos()

        AddHandler Me.ucFechaDesde.Erro, AddressOf ErroControles
        AddHandler Me.ucFechaHasta.Erro, AddressOf ErroControles
        AddHandler Me.ucFormularios.Erro, AddressOf ErroControles
        AddHandler Me.ucFiltroDivisas.Erro, AddressOf ErroControles

    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

    Private Function Validacion() As StringBuilder
        Dim Mesajes As New StringBuilder
        Dim FechaDesde As DateTime = Nothing
        Dim FechaHasta As DateTime = Nothing

        If Me.Delegaciones Is Nothing OrElse Me.Delegaciones.Count = 0 Then
            Mesajes.AppendLine(Traduzir("057_delegacionobligatorio"))
        End If

        If Me.Clientes Is Nothing OrElse Me.Clientes.Count = 0 Then
            Mesajes.AppendLine(Traduzir("057_clienteobligatorio"))
        End If

        If Me.Canales Is Nothing OrElse Me.Canales.Count = 0 Then
            Mesajes.AppendLine(Traduzir("057_canalobligatorio"))
        End If

        Me.ucFechaDesde.GuardarDatos()
        Me.ucFechaHasta.GuardarDatos()
        If String.IsNullOrEmpty(Me.ucFechaDesde.Valor) AndAlso String.IsNullOrEmpty(Me.ucFechaHasta.Valor) Then
            Mesajes.AppendLine(Traduzir("057_fechadesdeobligatorio"))
            Mesajes.AppendLine(Traduzir("057_fechahastaobligatorio"))

        ElseIf String.IsNullOrEmpty(Me.ucFechaDesde.Valor) AndAlso Not String.IsNullOrEmpty(Me.ucFechaHasta.Valor) Then
            Mesajes.AppendLine(Traduzir("057_fechadesdeobligatorio"))

        ElseIf Not String.IsNullOrEmpty(Me.ucFechaDesde.Valor) AndAlso String.IsNullOrEmpty(Me.ucFechaHasta.Valor) Then
            Mesajes.AppendLine(Traduzir("057_fechahastaobligatorio"))

        Else
            If DateTime.Parse(Me.ucFechaDesde.Valor) > DateTime.Parse(Me.ucFechaHasta.Valor) Then
                Mesajes.AppendLine(Traduzir("057_fechadesdefechahasta"))

            End If
        End If

        Return Mesajes
    End Function

    Private Sub CargarParametroDelegacion(ByRef Parametros As List(Of RSE.ParameterValue))

        If Delegaciones IsNot Nothing Then
            For Each objDelegacion In Me.Delegaciones
                If Not String.IsNullOrEmpty(objDelegacion.Identificador) Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DELEGACION", .Value = objDelegacion.Identificador})
                End If
            Next
        End If

    End Sub

    Private Sub CargarParametroPlanta(ByRef Parametros As List(Of RSE.ParameterValue))

        If Plantas IsNot Nothing AndAlso Plantas.Count > 0 Then
            For Each objPlanta In Me.Plantas
                If Not String.IsNullOrEmpty(objPlanta.Identificador) Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PLANTA", .Value = objPlanta.Identificador})
                End If
            Next
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PLANTA", .Value = String.Empty})
        End If

    End Sub

    Private Sub CargarParametroSector(ByRef Parametros As List(Of RSE.ParameterValue))

        If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then
            Dim objSectores As New StringBuilder
            For Each objSector In Me.Sectores
                If Not String.IsNullOrEmpty(objSector.Identificador) Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SECTOR", .Value = objSector.Identificador})
                    objSectores.Append(objSector.Codigo & " - " & objSector.Descripcion)
                End If
            Next
            Parametros.Add(New RSE.ParameterValue() With {.Name = "DES_SECTORES", .Value = String.Join("/", objSectores)})
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SECTOR", .Value = String.Empty})
        End If
    End Sub

    Private Sub CargarParametroCliente(ByRef Parametros As List(Of RSE.ParameterValue))

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            Dim objClientesSubPto As New StringBuilder
            For Each objCliente In Clientes
                If Not String.IsNullOrEmpty(objCliente.Identificador) Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = objCliente.Identificador})
                    objClientesSubPto.Append(objCliente.Codigo & " - " & objCliente.Descripcion)

                    If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then
                        For Each objSubCliente In objCliente.SubClientes

                            If Not String.IsNullOrEmpty(objSubCliente.Identificador) Then

                                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CLIENTE", .Value = objSubCliente.Identificador})

                                objClientesSubPto.Append(objSubCliente.Codigo & " - " & objSubCliente.Descripcion)

                                If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                                    For Each objPtoServicio In objSubCliente.PuntosServicio
                                        If Not String.IsNullOrEmpty(objPtoServicio.Identificador) Then
                                            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PUNTO_SERVICIO", .Value = objPtoServicio.Identificador})

                                            objClientesSubPto.Append(objPtoServicio.Codigo & " - " & objPtoServicio.Descripcion)
                                        End If
                                    Next
                                End If
                            End If

                        Next
                    End If
                End If
            Next

            If Not Parametros.Exists(Function(p) p.Name = "P_COM_CLIENTE") Then
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = String.Empty})
            End If

            If Not Parametros.Exists(Function(p) p.Name = "P_COM_SUB_CLIENTE") Then
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CLIENTE", .Value = String.Empty})
            End If

            If Not Parametros.Exists(Function(p) p.Name = "P_COM_PUNTO_SERVICIO") Then
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PUNTO_SERVICIO", .Value = String.Empty})
            End If

            Parametros.Add(New RSE.ParameterValue() With {.Name = "COD_DES_CLI_SUBCLI_PTO", .Value = String.Join("/", objClientesSubPto)})
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CLIENTE", .Value = String.Empty})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CLIENTE", .Value = String.Empty})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_PUNTO_SERVICIO", .Value = String.Empty})
        End If

    End Sub

    Private Sub CargarParametroCanal(ByRef Parametros As List(Of RSE.ParameterValue))

        If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
            Dim objCanales As New StringBuilder
            For Each objCanal In Canales

                If Not String.IsNullOrEmpty(objCanal.Identificador) Then
                    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CANALES", .Value = objCanal.Identificador})
                    objCanales.Append(objCanal.Codigo & " - " & objCanal.Descripcion)

                    If objCanal.SubCanales IsNot Nothing AndAlso objCanal.SubCanales.Count > 0 Then
                        For Each objSubCanal In objCanal.SubCanales
                            If Not String.IsNullOrEmpty(objSubCanal.Identificador) Then
                                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CANALES", .Value = objSubCanal.Identificador})
                            End If
                        Next
                        'Else
                        '    Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CANALES", .Value = String.Empty})
                    End If
                End If

            Next
            If Parametros.FirstOrDefault(Function(x) x.Name = "P_COM_SUB_CANALES") Is Nothing Then
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CANALES", .Value = String.Empty})
            End If
            Parametros.Add(New RSE.ParameterValue() With {.Name = "COD_DES_CANALES", .Value = String.Join("/", objCanales)})
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_CANALES", .Value = String.Empty})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_SUB_CANALES", .Value = String.Empty})
        End If

    End Sub

    Private Sub CargarParametroFecha(ByRef Parametros As List(Of RSE.ParameterValue))

        Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_FECHA_DESDE", .Value = Me.ucFechaDesde.Valor})
        Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_FECHA_HASTA", .Value = Me.ucFechaHasta.Valor})

    End Sub

    Private Sub CargarParametroDisponible(ByRef Parametros As List(Of RSE.ParameterValue))
        If Me.ucFiltroDivisas.ConsiderarValoresItemSelecionado = "AMBOS" Then
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DISPONIBLE", .Value = "1"})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_NO_DISPONIBLE", .Value = "0"})

        ElseIf Me.ucFiltroDivisas.ConsiderarValoresItemSelecionado = "DISPONIVEL" Then
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DISPONIBLE", .Value = "1"})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_NO_DISPONIBLE", .Value = "1"})

        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_DISPONIBLE", .Value = "0"})
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_NO_DISPONIBLE", .Value = "0"})
        End If

    End Sub

    Private Sub CargarParametroFormulario(ByRef Parametros As List(Of RSE.ParameterValue))

        Me.ucFormularios.GuardarDatos()
        If Me.ucFormularios.ItensSelecionados IsNot Nothing AndAlso Me.ucFormularios.ItensSelecionados.Count > 0 Then
            For Each objFormulario In Me.ucFormularios.ItensSelecionados
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_FORMULARIO", .Value = objFormulario})
            Next
        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_FORMULARIO", .Value = String.Empty})
        End If

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

    Private Sub CargarParametrosColumnasAdicionales(ByRef Parametros As List(Of RSE.ParameterValue))

        If Me.ucColumnasAdicionales.RetornarItensSelecionados IsNot Nothing AndAlso Me.ucColumnasAdicionales.RetornarItensSelecionados.Count > 0 Then
            For Each columnaseleccionada As ListItem In Me.ucColumnasAdicionales.RetornarItensSelecionados
                Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_COLUMNAS_ADICIONALES", .Value = columnaseleccionada.Value})
            Next columnaseleccionada

        Else
            Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_COLUMNAS_ADICIONALES", .Value = String.Empty})
        End If

        Parametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_TIPO_VALOR", .Value = Me.ucFiltroDivisas.TipoValor})

    End Sub

    Private Sub CargarParametrosFiltroFecha(ByRef Parametros As List(Of RSE.ParameterValue))
        Parametros.Add(New RSE.ParameterValue() With {.Name = "P_FILTRO_FECHA", .Value = ddlFiltroFecha.SelectedValue})
    End Sub
#End Region

#Region "     Columnas adicionales     "

    Private Sub CargarColumnasAdicionales()

        ucColumnasAdicionales.PreencherTitulos(Traduzir("057_columnasadicionalesdisponibles"), Traduzir("057_columnasadicionalesseleccionados"))

        Dim ColumnasAdicionales As New ListItemCollection From {
                                                                New ListItem With {.Text = Traduzir("057_puntoservicioorigen"), .Value = "PUNTO_SERVICIO_ORIGEN"},
                                                                New ListItem With {.Text = Traduzir("057_subclientedestino"), .Value = "SUBCLIENTE_DESTINO"},
                                                                New ListItem With {.Text = Traduzir("057_puntoserviciodestino"), .Value = "PUNTO_SERVICIO_DESTINO"}
                                                               }

        ucColumnasAdicionales.PreencherControle(ColumnasAdicionales)

        Dim ComprimentoLista As Integer = 250
        Dim AlturaLista As Integer = 210
        If ucFormularios IsNot Nothing AndAlso ucFormularios.SelecionarTodos Then
            AlturaLista = 225
        End If
        ucColumnasAdicionales.ConfigurarControle(True, True, True, ComprimentoLista, AlturaLista)

    End Sub

    Private Sub CargarFiltroFecha()
        Dim item As New ListItem(Traduzir("057_FiltroFechaCreacion"), "CREACION")
        item.Selected = True
        ddlFiltroFecha.Items.Add(item)
        ddlFiltroFecha.Items.Add(New ListItem(Traduzir("057_FiltroFechaGestion"), "GESTION"))
    End Sub
#End Region

#End Region

End Class