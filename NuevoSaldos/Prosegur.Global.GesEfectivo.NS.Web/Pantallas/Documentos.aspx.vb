Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class Documentos
    Inherits Base

#Region "[PROPRIEDADES]"

    Private _TipoSitioDocumento As Enumeradores.TipoSitio?
    Public Property TipoSitioDocumento() As Enumeradores.TipoSitio?
        Get
            If Not _TipoSitioDocumento.HasValue Then
                _TipoSitioDocumento = RecuperarEnum(Of Enumeradores.TipoSitio)(ddlTipoSitioDocumentos.SelectedValue)
            End If
            Return _TipoSitioDocumento
        End Get
        Set(value As Enumeradores.TipoSitio?)
            _TipoSitioDocumento = value
        End Set
    End Property

    Private _EstadoDocumento As List(Of Enumeradores.EstadoDocumento)
    Public Property EstadoDocumento() As List(Of Enumeradores.EstadoDocumento)
        Get
            If _EstadoDocumento Is Nothing OrElse _EstadoDocumento.Count = 0 Then
                _EstadoDocumento = New List(Of Enumeradores.EstadoDocumento)
                _EstadoDocumento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumento)(ddlCodigoEstadoDocumento.SelectedValue))
            End If
            Return _EstadoDocumento
        End Get
        Set(value As List(Of Enumeradores.EstadoDocumento))
            _EstadoDocumento = value
        End Set
    End Property

    Public ReadOnly Property Filtro() As Clases.Transferencias.FiltroDocumentos
        Get
            If _TipoSitioDocumento IsNot Nothing AndAlso _EstadoDocumento IsNot Nothing AndAlso ultimaConsulta IsNot Nothing AndAlso
                ultimaConsulta.Parametro IsNot Nothing Then
                Return ultimaConsulta.Parametro
            End If
            Return Nothing
        End Get
    End Property

    Public Property ultimaConsulta() As Peticion(Of Clases.Transferencias.FiltroDocumentos)
        Get
            Return Session("ultimaConsultaDocumento")
        End Get
        Set(value As Peticion(Of Clases.Transferencias.FiltroDocumentos))
            Session("ultimaConsultaDocumento") = value
        End Set
    End Property

    Public Property estadoTela() As String
        Get
            Return Session("estadoTela")
        End Get
        Set(value As String)
            Session("estadoTela") = value
        End Set
    End Property

    Private WithEvents _ucSectores As UcMaquina
    Public Property ucSectores() As UcMaquina
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\UcMaquina.ascx")
                _ucSectores.ID = "ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)


            End If
            Return _ucSectores
        End Get
        Set(value As UcMaquina)
            _ucSectores = value
        End Set
    End Property


    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    'Public Property Plantas As ObservableCollection(Of Clases.Planta)
    '    Get
    '        Return ucSectores.Plantas
    '    End Get
    '    Set(value As ObservableCollection(Of Clases.Planta))
    '        ucSectores.Plantas = value
    '    End Set
    'End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property



#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()
        Dim tituloEstado As String = String.Empty
        Dim tituloTipoSitio As String = String.Empty

        ' Menu Principal
        If (TipoSitioDocumento = Enumeradores.TipoSitio.Origen) Then
            tituloTipoSitio = Traduzir("039_Emitidos")
        ElseIf (TipoSitioDocumento = Enumeradores.TipoSitio.Destino) Then
            tituloTipoSitio = Traduzir("039_Recebidos")
        End If

        Select Case True
            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.EnCurso)
                tituloEstado = Traduzir("039_EnCurso")
            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.Confirmado)

                If (TipoSitioDocumento = Enumeradores.TipoSitio.Origen) Then
                    tituloEstado = Traduzir("039_Confirmado")
                ElseIf (TipoSitioDocumento = Enumeradores.TipoSitio.Destino) Then
                    tituloEstado = Traduzir("039_ImpresoReceber")
                End If

            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.Aceptado)

                If (TipoSitioDocumento = Enumeradores.TipoSitio.Origen) Then
                    tituloEstado = Traduzir("039_Aceptados")
                ElseIf (TipoSitioDocumento = Enumeradores.TipoSitio.Destino) Then
                    tituloEstado = Traduzir("039_RecebidoAceptados")
                End If

            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.Rechazado)

                If (TipoSitioDocumento = Enumeradores.TipoSitio.Origen) Then
                    tituloEstado = Traduzir("039_Rechazados")
                ElseIf (TipoSitioDocumento = Enumeradores.TipoSitio.Destino) Then
                    tituloEstado = Traduzir("039_RecebidoRechazados")
                End If

            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.Sustituido)
                tituloEstado = Traduzir("039_Substituido")

            Case EstadoDocumento.Contains(Enumeradores.EstadoDocumento.Anulado)
                tituloEstado = Traduzir("039_Anulado")

        End Select

        ' Titulos FieldSet
        lblTitulo.Text = Traduzir("033_lblTituloFiltro")
        'lblTituloFiltro2.Text = Traduzir("033_lblTituloFiltro2")

        ' Campos Filtro
        lblDiponibilidad.Text = Traduzir("033_lblDisponibilidad")

        lblNumeroComprovante.Text = Traduzir("033_lblNumeroComprovante")
        lblNumeroComprovanteDesde.Text = Traduzir("033_lblNumeroComprovanteDesde")
        lblNumeroComprovanteHasta.Text = Traduzir("033_lblNumeroComprovanteHasta")

        lblFechaCreacion.Text = Traduzir("033_lblFechaCreacion")
        lblFechaCreacionDesde.Text = Traduzir("033_lblFechaCreacionDesde")
        lblFechaCreacionHasta.Text = Traduzir("033_lblFechaCreacionHasta")

        lblBuscarPor.Text = Traduzir("033_lblBuscarPor")

        lblSemRegistro.Text = Traduzir("lblSemRegistro")

        btnBuscar.Text = Traduzir("btnBuscar")

        lblTipoSitioDocumentos.Text = Traduzir("033_lblTipoSitioDocumentos")
        lblCodigoEstadoDocumento.Text = Traduzir("033_lblCodigoEstadoDocumento")


        'Master.Titulo = String.Format("{0} - {1} - {2}", Traduzir("033_Titulo"), tituloTipoSitio, tituloEstado)
        Master.Titulo = Traduzir("033_Titulo")
    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaCreacionDesde.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaCreacionHasta.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DOCUMENTOS
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
        MyBase.CodFuncionalidad = "CONSULTA_DOCUMENTOS"
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        GoogleAnalyticsHelper.TrackAnalytics(Me, "Documento")
        ConfigurarControle_Sector()
        Try
            Master.FindControl("pnlMenuRodape").Visible = False

            If Not Me.IsPostBack Then
                If EstadoDocumento.Contains(Enumeradores.EstadoDocumento.EnCurso) Then
                    dvDiponibilidad.Style.Item("display") = "none"
                End If





                'Al ingresar en la pantalla, el campo será cargado con los datos de la delegación logada.
                Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)
                Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
                Me.ucSectores.AtualizarRegistrosDelegacion()
                Me.ucSectores.DelegacionHabilitado = True


                Me.ucSectores.Sectores = New ObservableCollection(Of Clases.Sector)

                'Al ingresar en la pantalla, el campo será cargado con los datos del sector padre primer nivel do sector logado.
                ' Dim sector = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectorPadrePrimerNivel(Base.InformacionUsuario.SectorSeleccionado.Identificador)


                Me.ucSectores.AtualizarRegistrosSector()


                cargarDisponibilidad()
                cargarBuscarPor()
                cargarTipoSitioDocumentos()
                cargarCodigoEstadoDocumento()
                carregaStringEstadoTela()
                cargarFiltro()

            End If
            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))).SetupGridViewPaginacion(grvResultadoDocumentos, AddressOf PopularGridView, Function(p) p.Retorno)

            txtFechaCreacionDesde.Focus()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub


    Private Sub grvResultadoDocumentos_PageIndexChanged(sender As Object, e As System.EventArgs) Handles grvResultadoDocumentos.PageIndexChanged
        ultimaConsulta = Nothing
    End Sub

    Protected Sub grvResultadoDocumentos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvResultadoDocumentos.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(0).Text = Traduzir("033_grid_cor")
            e.Row.Cells(1).Text = Traduzir("033_grid_codigo_externo")
            e.Row.Cells(2).Text = Traduzir("033_grid_codigo_Comprovante")
            e.Row.Cells(3).Text = Traduzir("033_grid_formulario")
            e.Row.Cells(4).Text = Traduzir("033_grid_canal_cliente")
            e.Row.Cells(5).Text = Traduzir("033_grid_sector_origem")
            e.Row.Cells(6).Text = Traduzir("033_grid_centro_processo_origem")
            e.Row.Cells(7).Text = Traduzir("033_grid_centro_processo_destino")
            e.Row.Cells(8).Text = Traduzir("033_grid_sector_destino")
            e.Row.Cells(9).Text = Traduzir("033_grid_fecha_hora_creacion")
            e.Row.Cells(10).Text = Traduzir("033_grid_usuario_creacion")
            e.Row.Cells(11).Text = Traduzir("033_grid_fecha_hora_modificacion")
            e.Row.Cells(12).Text = Traduzir("033_grid_usuario_modificacion")
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            ImpresionLinhaValoresGrid(e)
        End If

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            ValidarConsulta()
            grvResultadoDocumentos.PageIndex = 0
            ultimaConsulta = Nothing
            MontarStringEstadoTela()
            grvResultadoDocumentos.DataBind()
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub


    Private Sub ValidarConsulta()

        Dim incompletos As String = ""
        If Delegaciones.Count = 0 Then

            If Me.IsPostBack Then
                incompletos += ",  " + MyBase.RecuperarValorDic("delegacion") '"DELEGACION"
            End If
        End If

        If Sectores.Count = 0 Then
            If Me.IsPostBack Then
                incompletos += ",  " + MyBase.RecuperarValorDic("MAE")  '"MAQUINA"
            End If
        End If

        If Not String.IsNullOrWhiteSpace(incompletos) Then
            Dim msgError = String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), incompletos.Substring(2))
            Throw New Excepcion.NegocioExcepcion(msgError)
        End If


    End Sub
    Private Sub Documentos_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        If TipoSitioDocumento = Enumeradores.TipoSitio.Destino Then
            If grvResultadoDocumentos.Columns(7).Visible = False Then
                grvResultadoDocumentos.Columns(7).Visible = True
            End If
            If grvResultadoDocumentos.Columns(8).Visible = False Then
                grvResultadoDocumentos.Columns(8).Visible = True
            End If
            If grvResultadoDocumentos.Columns(11).Visible = False Then
                grvResultadoDocumentos.Columns(11).Visible = True
            End If
            If grvResultadoDocumentos.Columns(12).Visible = False Then
                grvResultadoDocumentos.Columns(12).Visible = True
            End If
        Else
            If grvResultadoDocumentos.Columns(5).Visible = False Then
                grvResultadoDocumentos.Columns(5).Visible = True
            End If
            If grvResultadoDocumentos.Columns(6).Visible = False Then
                grvResultadoDocumentos.Columns(6).Visible = True
            End If
            If grvResultadoDocumentos.Columns(4).Visible = False Then
                grvResultadoDocumentos.Columns(4).Visible = True
            End If
        End If
        grvResultadoDocumentos.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))
    End Sub

    Protected Sub redirecionaDocumento_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Try

            Dim Parametros = e.CommandArgument.ToString().Split(",")

            If Parametros(1) = "INDIVIDUAL" Then
                Response.Redireccionar("Documento.aspx?IdentificadorSector=" & Sectores(0).Identificador & "&IdentificadorDocumento=" & Parametros(0) + "&Modo=" + Enumeradores.Modo.Consulta.ToString() + "&SectorHijo=" + Parametros(2) + estadoTela)
            Else
                Response.Redireccionar("GrupoDocumento.aspx?IdentificadorSector=" & Sectores(0).Identificador & "&IdentificadorGrupoDocumentos=" & Parametros(0) + "&Modo=" + Enumeradores.Modo.Consulta.ToString() + "&SectorHijo=" + Parametros(2) + estadoTela)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub


    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try
            If Me.ucSectores.Delegaciones IsNot Nothing Then
                Delegaciones = Me.ucSectores.Delegaciones
            End If
            'If Me.ucSectores.Plantas IsNot Nothing Then
            '    Plantas = Me.ucSectores.Plantas
            'End If
            If Me.ucSectores.Sectores IsNot Nothing Then
                Sectores = Me.ucSectores.Sectores
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub


#End Region

#Region "[METODOS]"

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

    Private Sub carregaStringEstadoTela()


        If Request.QueryString("ddl_s") IsNot Nothing Then

            ddlTipoSitioDocumentos.SelectedIndex = Request.QueryString("ddl_s")
            ddlCodigoEstadoDocumento.SelectedIndex = Request.QueryString("ddl_e")
            ddlDisponibilidad.SelectedIndex = Request.QueryString("ddl_d")
            ddlBuscarPor.SelectedIndex = Request.QueryString("ddl_b")
            txtFechaCreacionDesde.Text = Request.QueryString("dt_de")
            txtFechaCreacionHasta.Text = Request.QueryString("dt_ate")
            txtFechaCreacionHasta.Text = Request.QueryString("dt_ate")
            txtFechaCreacionHasta.Text = Request.QueryString("dt_ate")
            txtNumeroComprovanteDesde.Text = Request.QueryString("num_de")
            txtNumeroComprovanteHasta.Text = Request.QueryString("num_ate")

            Dim SectorSelecionado = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerPorOid(Request.QueryString("oid_s"))

            If SectorSelecionado Is Nothing Then

                Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
            Else
                Me.ucSectores.Delegaciones.Add(SectorSelecionado.Delegacion)
                Me.ucSectores.Sectores.Add(SectorSelecionado)

                Me.ucSectores.AtualizarRegistrosSector()

                Me.ucSectores.SectorHabilitado = True
            End If
            Me.ucSectores.AtualizarRegistrosDelegacion()
            TipoSitioDocumento = RecuperarEnum(Of Enumeradores.TipoSitio)(ddlTipoSitioDocumentos.SelectedValue)
            EstadoDocumento.Clear()
            EstadoDocumento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumento)(ddlCodigoEstadoDocumento.SelectedValue))
            MontaFiltro()
        End If


        'estadoTela = "&oid_d=" + Request.QueryString("oid_d")
        'estadoTela = estadoTela + "&oid_s=" + Request.QueryString("oid_d")
        'estadoTela = estadoTela + "&ddl_s=" + Request.QueryString("ddl_s")
        'estadoTela = estadoTela + "&ddl_e=" + Request.QueryString("ddl_e")
        'estadoTela = estadoTela + "&ddl_d=" + Request.QueryString("ddl_d")
        'estadoTela = estadoTela + "&ddl_b=" + Request.QueryString("ddl_b")
        'estadoTela = estadoTela + "&dt_de=" + Request.QueryString("dt_de")
        'estadoTela = estadoTela + "&dt_ate=" + Request.QueryString("dt_ate")
        'estadoTela = estadoTela + "&num_de=" + Request.QueryString("num_de") 'IIf(Not String.IsNullOrEmpty(Request.QueryString("num_de")), Request.QueryString("num_de"), "")
        'estadoTela = estadoTela + "&num_ate=" + Request.QueryString("num_ate") 'IIf(Not String.IsNullOrEmpty(Request.QueryString("num_ate")), Request.QueryString("num_ate"), "")


        'estadoTela = estadoTela + "&num_ate=" + IIf(Not String.IsNullOrEmpty(Request.QueryString("num_ate")), Request.QueryString("num_ate"), "")
        'estadoTela = estadoTela + "&num_ate=" + IIf(Not String.IsNullOrEmpty(Request.QueryString("num_ate")), Request.QueryString("num_ate"), "")

    End Sub


    Public Sub MontarStringEstadoTela()
        estadoTela = String.Empty
        estadoTela = "&oid_d=" + ucSectores.Delegaciones(0).Identificador
        estadoTela = estadoTela + "&oid_s=" + ucSectores.Sectores(0).Identificador

        estadoTela = estadoTela + "&ddl_s=" + ddlTipoSitioDocumentos.SelectedIndex.ToString '
        estadoTela = estadoTela + "&ddl_e=" + ddlCodigoEstadoDocumento.SelectedIndex.ToString '
        estadoTela = estadoTela + "&ddl_d=" + ddlDisponibilidad.SelectedIndex.ToString '
        estadoTela = estadoTela + "&ddl_b=" + ddlBuscarPor.SelectedIndex.ToString '

        estadoTela = estadoTela + "&dt_de=" + txtFechaCreacionDesde.Text '
        estadoTela = estadoTela + "&dt_ate=" + txtFechaCreacionHasta.Text '
        estadoTela = estadoTela + "&num_de=" + txtNumeroComprovanteDesde.Text
        estadoTela = estadoTela + "&num_ate=" + txtNumeroComprovanteHasta.Text



    End Sub
    Public Sub cargarDisponibilidad()

        ddlDisponibilidad.Items.Clear()
        ddlDisponibilidad.Items.Add(Traduzir("033_ddlAmbos"))
        ddlDisponibilidad.Items.Add(Traduzir("033_ddlDisponible"))
        ddlDisponibilidad.Items.Add(Traduzir("033_ddlNoDisponible"))

    End Sub

    Public Sub cargarBuscarPor()

        ddlBuscarPor.Items.Clear()
        ddlBuscarPor.Items.Add(Traduzir("033_ddlSeleccionar"))
        ddlBuscarPor.Items.Add(Traduzir("033_ddlNumeroComprovante"))
        ddlBuscarPor.Items.Add(Traduzir("033_ddlNumeroExterno"))

        lblInfNumeroComprovante.Text = Traduzir("033_ddlNumeroComprovante")
        lblInfCodigoExterno.Text = Traduzir("033_ddlNumeroExterno")

        lblInfValores.Text = Traduzir("033_lblInfValores")

    End Sub

    Public Sub cargarTipoSitioDocumentos()
        'TODO: colocar Traduzir
        Dim values As New Dictionary(Of String, String)
        values.Add("O", Traduzir("033_Origem"))
        values.Add("D", Traduzir("033_Destino"))


        ddlTipoSitioDocumentos.DataSource = values
        ddlTipoSitioDocumentos.DataTextField = "Value"
        ddlTipoSitioDocumentos.DataValueField = "Key"
        ddlTipoSitioDocumentos.DataBind()
    End Sub

    Public Sub cargarCodigoEstadoDocumento()
        'TODO: colocar Traduzir
        Dim values As New Dictionary(Of String, String)

        ' values.Add("NU", "Nuevo")
        values.Add("EC", Traduzir("064_EstadoDocumento_EnCurso"))
        values.Add("CF", Traduzir("064_EstadoDocumento_Confirmado"))
        values.Add("AN", Traduzir("064_EstadoDocumento_Anulado"))
        values.Add("AC", Traduzir("064_EstadoDocumento_Aceptado"))
        values.Add("RC", Traduzir("064_EstadoDocumento_Rechazado"))
        values.Add("SU", Traduzir("064_EstadoDocumento_Sustituido"))
        values.Add("EE", Traduzir("064_EstadoDocumento_Eliminado"))


        ddlCodigoEstadoDocumento.DataSource = values
        ddlCodigoEstadoDocumento.DataTextField = "Value"
        ddlCodigoEstadoDocumento.DataValueField = "Key"
        ddlCodigoEstadoDocumento.SelectedValue = "AC"
        ddlCodigoEstadoDocumento.DataBind()




    End Sub




    Public Sub cargarFiltro()

        ' Verifica se existe algum Filtro salvo e se ele tem as mesmas configurações da tela
        If Filtro IsNot Nothing AndAlso Filtro.EstadoDocumento IsNot Nothing AndAlso Filtro.EstadoDocumento.Count > 0 AndAlso
            EstadoDocumento IsNot Nothing AndAlso EstadoDocumento.Count > 0 AndAlso Filtro.EstadoDocumento(0) = EstadoDocumento(0) AndAlso
            Sectores.Count > 0 AndAlso Filtro.IdentificadorSector = Sectores(0).Identificador AndAlso
            Filtro.TipoSitioDocumento = TipoSitioDocumento Then

            If Request.QueryString("ddl_s") Is Nothing Then
                ' Copia Datas do filtro salvo
                If Filtro.FechaCreacionDesde <> New DateTime() Then
                    txtFechaCreacionDesde.Text = Filtro.FechaCreacionDesde.ToString("dd/MM/yyyy HH:mm:ss")
                End If
                If Filtro.FechaCreacionHasta <> New DateTime() Then
                    txtFechaCreacionHasta.Text = Filtro.FechaCreacionHasta.ToString("dd/MM/yyyy HH:mm:ss")
                End If
            End If
            ' Copia Numeros comprovantes do filtro salvo
            txtNumeroComprovanteDesde.Text = Filtro.NumeroComprovanteDesde
            txtNumeroComprovanteHasta.Text = Filtro.NumeroComprovanteHasta

            ' Copia disponibilidade
            If Filtro.PorDisponibilidad IsNot Nothing Then
                If Filtro.PorDisponibilidad Then
                    ddlDisponibilidad.SelectedIndex = 1
                Else
                    ddlDisponibilidad.SelectedIndex = 2
                End If
            Else
                ddlDisponibilidad.SelectedIndex = 0
            End If

            ' Copia informações adicionais
            ddlBuscarPor.SelectedIndex = 0
            If Filtro.NumerosComprobantes IsNot Nothing AndAlso Filtro.NumerosComprobantes.Count > 0 Then
                ddlBuscarPor.SelectedIndex = 1
                hidItensAdicionados.Value = String.Empty
                lstItensAdicionados.InnerHtml = String.Empty
                For Each num In Filtro.NumerosComprobantes
                    If num.Replace("'", "").Trim.Length > 0 Then
                        hidItensAdicionados.Value &= num.Replace("'", "") & ";"
                        lstItensAdicionados.InnerHtml &= "<div id='item_" & num.Replace("'", "") & "'>" & num.Replace("'", "") & "<button type='button' value='X' onclick=" & Chr(34) & "javascript:eliminarComprovante(" & num & ");" & Chr(34) & " /></div>"
                    End If
                Next
                dvInfNumeroComprovante.Style.Item("display") = "block"
                dvInfValores.Style.Item("display") = "block"
            End If
            If Filtro.NumerosExternos IsNot Nothing AndAlso Filtro.NumerosExternos.Count > 0 Then
                ddlBuscarPor.SelectedIndex = 2
                hidItensAdicionados.Value = String.Empty
                For Each num In Filtro.NumerosExternos
                    If num.Replace("'", "").Trim.Length > 0 Then
                        hidItensAdicionados.Value &= num.Replace("'", "") & ";"
                        lstItensAdicionados.InnerHtml &= "<div id='item_" & num.Replace("'", "") & "'>" & num.Replace("'", "") & "<button type='button' value='X' onclick=" & Chr(34) & "javascript:eliminarComprovante(" & num & ");" & Chr(34) & " /></div>"
                    End If
                Next
                dvInfCodigoExterno.Style.Item("display") = "block"
                dvInfValores.Style.Item("display") = "block"
            End If

        Else
            ultimaConsulta = Nothing
            txtFechaCreacionDesde.Text = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).AddDays(-3).ToString("dd/MM/yyyy HH:mm:ss")
            txtFechaCreacionHasta.Text = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).AddDays(1).ToString("dd/MM/yyyy HH:mm:ss")
            txtNumeroComprovanteDesde.Text = String.Empty
            txtNumeroComprovanteHasta.Text = String.Empty
            ddlDisponibilidad.SelectedIndex = 0
            ddlBuscarPor.SelectedIndex = 0
            txtInfCodigoExterno.Text = String.Empty
            txtInfNumeroComprovante.Text = String.Empty
            hidItensAdicionados.Value = String.Empty
        End If

    End Sub

    Public Sub ImpresionLinhaValoresGrid(e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim Item As Clases.Transferencias.DocumentoGrupoDocumento = e.Row.DataItem

        e.Row.Cells(0).Style.Add("background", If(Item.CorFormulario.Name.Substring(0, 1).ToString <> "#", "#" & Item.CorFormulario.Name, Item.CorFormulario.Name) & " !important; width:5px;")

        Dim identificadorSector As String
        If Sectores.Count > 0 Then
            identificadorSector = Sectores(0).Identificador
        End If


        CType(e.Row.FindControl("redirecionaDocumento"), ImageButton).CommandArgument = Item.Identificador + "," + Item.Tipo + "," + (Item.IdentificadorSector <> identificadorSector).ToString()

        CType(e.Row.FindControl("Criado"), Label).Text = Item.FechaHoraCreacion.ToString("dd/MM/yyyy HH:mm:ss")

        CType(e.Row.FindControl("Resolvido"), Label).Text = Item.FechaHoraModificacion.ToString("dd/MM/yyyy HH:mm:ss")

        If Item.TipoSitio = Enumeradores.TipoSitio.Origen Then

            ImpresionCuentaDestino(e, Item)

        ElseIf Item.TipoSitio = Enumeradores.TipoSitio.Destino Then

            ImpresionCuentaOrigen(e, Item)

        End If

    End Sub

    Public Sub ImpresionCuentaDestino(e As System.Web.UI.WebControls.GridViewRowEventArgs, Item As Clases.Transferencias.DocumentoGrupoDocumento)
        CType(e.Row.FindControl("AoCentroProc"), Label).Text = Item.DescripcionSectorOrigenDestino
        If Item.DescripcionSubCanal IsNot Nothing Then
            CType(e.Row.FindControl("CanalCliente"), Label).Text = Item.DescripcionSubCanal
        End If
    End Sub

    Public Sub ImpresionCuentaOrigen(e As System.Web.UI.WebControls.GridViewRowEventArgs, Item As Clases.Transferencias.DocumentoGrupoDocumento)
        If Item.DescripcionSectorOrigenDestino IsNot Nothing Then
            CType(e.Row.FindControl("DoCentroProc"), Label).Text = Item.DescripcionSectorOrigenDestino
        End If
        If Item.UsuarioModificacion IsNot Nothing Then
            CType(e.Row.FindControl("UsuarioResolucao"), Label).Text = Item.UsuarioModificacion
        End If
        CType(e.Row.FindControl("Resolvido"), Label).Text = Item.FechaHoraModificacion.ToString("dd/MM/yyyy HH:mm:ss")
    End Sub

    Public Function MontaFiltro() As Clases.Transferencias.FiltroDocumentos

        Dim objFiltro As New Clases.Transferencias.FiltroDocumentos

        Try

            If Delegaciones.Count = 0 Then
                If Me.IsPostBack Then
                    Throw New Excepcion.NegocioExcepcion("Dados Incompletos Delegacion")
                End If
            Else
                objFiltro.Delegacion = Delegaciones(0)
            End If


            'If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario.SectorSeleccionado IsNot Nothing AndAlso
            '    Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
            '    objFiltro.Delegacion = Base.InformacionUsuario.DelegacionSeleccionada
            'End If

            If Sectores.Count = 0 Then
                If Me.IsPostBack Then
                    Throw New Excepcion.NegocioExcepcion("Dados Incompletos Maquina")
                End If
            Else
                objFiltro.IdentificadorSector = Sectores(0).Identificador
            End If

            'objFiltro.IdentificadorSector = InformacionUsuario.SectorSeleccionado.Identificador
            objFiltro.TipoSitioDocumento = TipoSitioDocumento
            objFiltro.EstadoDocumento = EstadoDocumento
            objFiltro.NumeroComprovanteDesde = IIf(Not String.IsNullOrEmpty(txtNumeroComprovanteDesde.Text), txtNumeroComprovanteDesde.Text, Nothing)
            objFiltro.NumeroComprovanteHasta = IIf(Not String.IsNullOrEmpty(txtNumeroComprovanteHasta.Text), txtNumeroComprovanteHasta.Text, Nothing)
            objFiltro.FechaCreacionDesde = IIf(Not String.IsNullOrEmpty(txtFechaCreacionDesde.Text), txtFechaCreacionDesde.Text, Nothing)
            objFiltro.FechaCreacionHasta = IIf(Not String.IsNullOrEmpty(txtFechaCreacionHasta.Text), txtFechaCreacionHasta.Text, Nothing)

            If ddlDisponibilidad.SelectedIndex = 0 Then
                objFiltro.PorDisponibilidad = Nothing
            ElseIf ddlDisponibilidad.SelectedIndex = 1 Then
                objFiltro.PorDisponibilidad = True
            ElseIf ddlDisponibilidad.SelectedIndex = 2 Then
                objFiltro.PorDisponibilidad = False
            End If

            objFiltro.NumerosComprobantes = New List(Of String)
            objFiltro.NumerosExternos = New List(Of String)
            If hidItensAdicionados.Value IsNot Nothing AndAlso hidItensAdicionados.Value.Length > 0 Then
                If ddlBuscarPor.SelectedIndex = 1 Then
                    For Each valor In hidItensAdicionados.Value.Split(";")
                        objFiltro.NumerosComprobantes.Add("'" & valor.Replace("'", "").Trim & "'")
                    Next
                ElseIf ddlBuscarPor.SelectedIndex = 2 Then
                    For Each valor In hidItensAdicionados.Value.Split(";")
                        objFiltro.NumerosExternos.Add("'" & valor.Replace("'", "").Trim & "'")
                    Next
                End If
            End If

            ' objFiltro.ConsiderarSectoresHijos = Not chkExcluirSubSectoresPuestor.Checked

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

        Return objFiltro

    End Function

    Public Function ObtenerDocumentos(e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))), objFiltro As Clases.Transferencias.FiltroDocumentos) As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

        Dim objPeticion As New Peticion(Of Clases.Transferencias.FiltroDocumentos)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()
        Dim objRespuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

        If ultimaConsulta IsNot Nothing Then
            objPeticion = ultimaConsulta
            grvResultadoDocumentos.PageIndex = objPeticion.ParametrosPaginacion.IndicePagina
        Else
            objPeticion.ParametrosPaginacion.RegistrosPorPagina = Aplicacao.Util.Utilidad.getMaximoRegistrosPageGrid(If(Session("MaximoRegistrosPageGrid") IsNot Nothing AndAlso IsNumeric(Session("MaximoRegistrosPageGrid")), Session("MaximoRegistrosPageGrid"), 0))
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
            objPeticion.Parametro = objFiltro

            ultimaConsulta = objPeticion
        End If

        objRespuesta = LogicaNegocio.GenesisSaldos.Documento.ObtenerListaDocumentos(objPeticion)

        Return objRespuesta
    End Function

    Private Sub PopularGridView(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))))

        Try
            Dim respuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

            Dim objFiltro As Clases.Transferencias.FiltroDocumentos = MontaFiltro()
            respuesta = ObtenerDocumentos(e, objFiltro)

            If respuesta.Retorno IsNot Nothing AndAlso respuesta.Retorno.Count > 0 Then
                dvEmptyData.Style.Item("display") = "none"
            End If

            e.RespuestaPaginacion = respuesta
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

#End Region


#Region "       Helpers     "

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.SolamenteSectoresPadre = True
        'Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True
        'Me.ucSectores.PlantaHabilitado = False
        'Me.ucSectores.NoExhibirPlanta = True

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        ' If Plantas IsNot Nothing Then
        ' Me.ucSectores.Plantas = Plantas
        ' End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If
        'Me.ucSectores.ucPlanta.Visible = False
        Me.ucSectores.ucMaquina.Titulo = "MAE"
    End Sub
#End Region
End Class
