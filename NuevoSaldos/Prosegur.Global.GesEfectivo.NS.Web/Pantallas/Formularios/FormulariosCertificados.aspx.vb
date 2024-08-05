Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Util
Imports System.Reflection

Public Class FormulariosCertificados
    Inherits Base

#Region "[PROPRIEDADES]"

    Private _Acciones As ucAcciones
    Private BuscarDados As Boolean = False

    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = Me.ID & "_Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get
    End Property

    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
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


    Private _Filtros As Clases.Transferencias.FiltroConsultaFormulariosCertificados
    Public Property Filtros() As Clases.Transferencias.FiltroConsultaFormulariosCertificados
        Get
            If _Filtros Is Nothing Then
                _Filtros = New Clases.Transferencias.FiltroConsultaFormulariosCertificados
            End If
            Return _Filtros
        End Get
        Set(value As Clases.Transferencias.FiltroConsultaFormulariosCertificados)
            _Filtros = value
        End Set
    End Property

    Public Property RespuestaDadosConfigReporte() As Respuesta(Of List(Of DataRow))
        Get
            Return ViewState("_RespuestaDadosConfigReporte")
        End Get
        Set(value As Respuesta(Of List(Of DataRow)))
            ViewState("_RespuestaDadosConfigReporte") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()
        Try

            MyBase.Inicializar()
            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of DataRow))).SetupGridViewPaginacion(grvResultadoConfigReporte, AddressOf PopulaGridConfigReporte, Function(p) p.Retorno)

            Me.dvFiltro.Style.Item("display") = "block"
            Me.ConfigurarControle_Cliente()

            If Me.IsPostBack Then
                Me.BuscarDados = True
            End If

            If Not IsPostBack Then
                cargarTiposClientes()
                cargarTiposReporte()
            End If

            AjustarAcciones()

        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_REPORTE
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("068_lblTitulo_ConfigReporte")
        Me.lblTituloFiltro.Text = Traduzir("068_lblFiltro_ConfigReporte")
        Me.lblResultados.Text = Traduzir("062_lblResultados")
        Me.btnBuscar.Text = Traduzir("btnBuscar")

        Me.grvResultadoConfigReporte.Columns(0).HeaderText = Traduzir("068_lblCodigo")
        Me.grvResultadoConfigReporte.Columns(1).HeaderText = Traduzir("068_lblDescriccion")
        Me.grvResultadoConfigReporte.Columns(2).HeaderText = Traduzir("068_lblDireccionEnServidor")
        grvResultadoConfigReporte.Columns(3).HeaderText = Traduzir("044_Editar")
        grvResultadoConfigReporte.Columns(4).HeaderText = Traduzir("044_Exluir")

    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub cargarTiposClientes()
        Try
            Dim accionTipoCliente As New IAC.LogicaNegocio.AccionTipoCliente
            Dim peticionTipoCliente As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
            ' peticionTipoCliente.codTipoCliente = objCliente.CodTipoCliente
            peticionTipoCliente.ParametrosPaginacion.RealizarPaginacion = False

            Dim tipoCliente = accionTipoCliente.getTiposClientes(peticionTipoCliente).TipoCliente

            If tipoCliente IsNot Nothing AndAlso tipoCliente.Count > 0 Then

                cklTiposClientes.Items.Clear()
                For Each TP In tipoCliente
                    cklTiposClientes.Items.Add(New ListItem With {.Text = TP.desTipoCliente, .Value = TP.oidTipoCliente})
                Next

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub


    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Me.BuscarDados = True
        Dim respuesta As Respuesta(Of List(Of DataRow))
        respuesta = ConsultaConfigReporte()

        grvResultadoConfigReporte.PageIndex = 0
        ' grvResultadoConfigReporte.DataSource = respuesta.Retorno
        grvResultadoConfigReporte.DataBind()


    End Sub

    Private Sub PopulaGridConfigReporte(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of DataRow))))

        Try
            Dim respuesta As Respuesta(Of List(Of DataRow))
            If Me.BuscarDados Then
                'Para não executar a busca ao entrar na página
                respuesta = ConsultaConfigReporte(e)
                Me.RespuestaDadosConfigReporte = respuesta
            Else
                respuesta = Me.RespuestaDadosConfigReporte
            End If

            If respuesta IsNot Nothing AndAlso respuesta.Retorno IsNot Nothing AndAlso respuesta.Retorno.Count > 0 Then
                e.RespuestaPaginacion = respuesta
                Me.dvTituloResultado.Style.Item("display") = "block"
                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoConfigReporte.CssClass = "ui-datatable ui-datatable-data"
            Else

                Me.dvTituloResultado.Style.Item("display") = "block"

                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoConfigReporte.CssClass = ""

                If Me.BuscarDados Then
                    Me.BuscarDados = False
                    'Se não achou nenhum registro
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                       Aplicacao.Util.Utilidad.CriarChamadaMensagemErro("Não foram encontrado dados para pesquisa realizada", Nothing), True)
                End If

            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Private Sub grvResultadoConfigReporte_PreRender(sender As Object, e As System.EventArgs) Handles grvResultadoConfigReporte.PreRender

        Dim colunaInicial As Integer, colunaFinal As Integer
        colunaInicial = 0
        colunaFinal = 1
        Me.grvResultadoConfigReporte.Columns(0).Visible = True
        Me.grvResultadoConfigReporte.Columns(1).Visible = True

    End Sub

    Private Sub grvResultadoConfigReporte_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvResultadoConfigReporte.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                TratativaRowGrvResultado(e)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub TratativaRowGrvResultado(e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Pega os dados do item atual.
        Dim Item As DataRow = e.Row.DataItem
        DirectCast(e.Row.FindControl("Codigo"), Label).Text = AtribuirValorObj(Item("COD_CONFIG_REPORTE"), GetType(String))
        DirectCast(e.Row.FindControl("Descricion"), Label).Text = AtribuirValorObj(Item("DES_CONFIG_REPORTE"), GetType(String))
        DirectCast(e.Row.FindControl("Direccion"), Label).Text = AtribuirValorObj(Item("DES_DIRECCION"), GetType(String))

        ' Botão Modificar
        CType(e.Row.FindControl("litImgModificar"), System.Web.UI.WebControls.Literal).Text = "<a href='ConfiguracionReporte.aspx?IdentificadorFormulario=" & AtribuirValorObj(Item("OID_CONFIG_REPORTE"), GetType(String)) & "&Modo=" & Enumeradores.Modo.Modificacion.ToString() & "'><img src='../../Imagenes/Editar.png' name='ImgEditar' alt='" & Traduzir("044_Editar") & "' /></a>"

        'Botão Eliminar
        CType(e.Row.FindControl("ImgEliminar"), ImageButton).CommandArgument = AtribuirValorObj(Item("OID_CONFIG_REPORTE"), GetType(String))
        CType(e.Row.FindControl("ImgEliminar"), ImageButton).OnClientClick = "return confirm('" & Traduzir("068_btnExcluirConfiguracaoReporte") & "')"


    End Sub

    Private Function ConsultaConfigReporte(Optional e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of DataRow))) = Nothing) As Respuesta(Of List(Of DataRow))

        Dim objRespuesta As Respuesta(Of List(Of DataRow)) = Nothing
        Dim objPeticion As New Peticion(Of Clases.ConfiguracionReporte)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()

        Dim objFiltro As New Clases.ConfiguracionReporte
        If txtCodigo.Text <> String.Empty Then
            objFiltro.Codigo = txtCodigo.Text
        End If

        If txtDescricao.Text <> String.Empty Then
            objFiltro.Descripcion = txtDescricao.Text
        End If

        Dim strItensSelecionados As String = String.Empty
        If cklTiposClientes IsNot Nothing AndAlso (From tb As ListItem In cklTiposClientes.Items Where tb.Selected).Count > 0 Then
            strItensSelecionados = Join((From tb As ListItem In cklTiposClientes.Items Where tb.Selected Select tb.Value).ToArray(), ",")
            objFiltro.TiposClientes = New ObservableCollection(Of Clases.TipoCliente)

            For Each itemselecionado In cklTiposClientes.Items
                If DirectCast(itemselecionado, ListItem).Selected Then
                    objFiltro.TiposClientes.Add(New Clases.TipoCliente With {.Identificador = DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value})
                End If
            Next
        End If

        Dim strTiposSelecionados As String = String.Empty
        If cklTiposReporte IsNot Nothing AndAlso (From tb As ListItem In cklTiposReporte.Items Where tb.Selected).Count > 0 Then
            strTiposSelecionados = Join((From tb As ListItem In cklTiposReporte.Items Where tb.Selected Select tb.Value).ToArray(), ",")
            objFiltro.TiposReporte = New ObservableCollection(Of Enumeradores.TipoReporte)

            For Each itemselecionado In cklTiposReporte.Items
                If DirectCast(itemselecionado, ListItem).Selected Then
                    objFiltro.TiposReporte.Add(EnumExtension.RecuperarEnum(Of Enumeradores.TipoReporte)(DirectCast(itemselecionado, System.Web.UI.WebControls.ListItem).Value))
                End If
            Next
        End If

        If (DirectCast(phCliente.Controls(0), Object).Clientes) IsNot Nothing Then
            objFiltro.Clientes = New ObservableCollection(Of Clases.Cliente)
            objFiltro.Clientes = DirectCast(phCliente.Controls(0), Object).Clientes()
        End If



        objPeticion.Parametro = objFiltro

        If e Is Nothing Then
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        Else
            objPeticion.ParametrosPaginacion.RegistrosPorPagina = e.RegistrosPorPagina
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
        End If

        objRespuesta = LogicaNegocio.GenesisSaldos.FormulariosCertificados.ObtenerFormulariosCertificados(objPeticion)


        Return objRespuesta

    End Function
    Protected Sub ImgEliminar_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Try
            LogicaNegocio.GenesisSaldos.FormulariosCertificados.ExcluirFormulariosCertificado(e.CommandArgument)
            btnBuscar_Click(Nothing, Nothing)
        Catch ex As Exception
            Master.ControleErro.MostrarMensagemErro(ex)
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
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.NoExhibirSubCliente = True
        Me.ucClientes.PtoServicioHabilitado = False
        Me.ucClientes.NoExhibirPtoServicio = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub


    Public Shared Function ObtenerVersion() As String
        Dim version = Assembly.GetExecutingAssembly.GetName.Version
        Return version.Build.ToString.PadLeft(4, "0"c) & version.Revision.ToString.PadLeft(4, "0"c)
    End Function

    Private Sub AjustarAcciones()

        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        AddHandler Acciones.onAccionConfigurarNovoReporte, AddressOf Acciones_onAccionConfigurarNovoReporte
        Acciones.btnConfigurarNovoReporteVisible = True
        Acciones.btnCancelarVisible = False

    End Sub

    Private Sub Acciones_onAccionConfigurarNovoReporte()
        Try
            Response.Redireccionar("ConfiguracionReporte.aspx?Identificador=" & String.Empty & "&Modo=" & Enumeradores.Modo.Alta.ToString())
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try

            If Master.Historico.Count > 1 Then
                Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
            Else
                Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
            End If
            'End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

    Private Sub cargarTiposReporte()
        Try


            Dim objTiposReporte As New UI.WebControls.ListItemCollection

            objTiposReporte.Add(New ListItem With { _
                                .Value = Comon.Enumeradores.TipoReporte.Certificacion.GetHashCode, _
                                .Text = Traduzir("068_Tipo_Reporte_Certificado")})

            objTiposReporte.Add(New ListItem With { _
                               .Value = Comon.Enumeradores.TipoReporte.AbonoVisualizacion.GetHashCode, _
                               .Text = Traduzir("068_Tipo_Reporte_Abono_Vizualicacion")})

            objTiposReporte.Add(New ListItem With { _
                                .Value = Comon.Enumeradores.TipoReporte.AbonoExportacion.GetHashCode, _
                                .Text = Traduzir("068_Tipo_Reporte_Abono_Exportacion")})

            cklTiposReporte.DataTextField = "Text"
            cklTiposReporte.DataValueField = "Value"
            cklTiposReporte.DataSource = objTiposReporte
            cklTiposReporte.DataBind()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

End Class
