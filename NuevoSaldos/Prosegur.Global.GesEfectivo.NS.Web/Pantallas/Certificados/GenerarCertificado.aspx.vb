Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Web.Login.Configuraciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.ContractoServicio
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports Newtonsoft.Json
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Runtime.Serialization

Public Class GenerarCertificado
    Inherits Base

    Dim WithEvents ucPergunta As PopupPergunta

#Region "VARIÁVEIS"

    Private vCertTodosSectores As Boolean
    Private vCertTodosCanales As Boolean
    Private vCertTodasDelegaciones As Boolean

#End Region

#Region "[PROPRIEDADES]"

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
    Public Property Cliente As Clases.Cliente
        Get
            Return ViewState("Cliente")
        End Get
        Set(value As Clases.Cliente)
            ViewState("Cliente") = value
        End Set
    End Property
    Public Property ModoCertificado() As String
        Get
            Return ViewState("ModoCertificado")
        End Get
        Set(value As String)
            ViewState("ModoCertificado") = value
        End Set
    End Property
    Private Property TipoCertificado() As String
        Get
            Return ViewState("TipoCertificado")
        End Get
        Set(value As String)
            ViewState("TipoCertificado") = value
        End Set
    End Property
    Public ReadOnly Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return InformacionUsuario.Delegaciones
        End Get
    End Property
    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            If ViewState("Sectores") Is Nothing Then
                Dim objSectores As New ObservableCollection(Of Clases.Sector)
                For Each delegacion In Delegaciones
                    Dim objAux = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectoresPorDelegacion(delegacion.Identificador)
                    If objAux IsNot Nothing AndAlso objAux.Count > 0 Then
                        objSectores.AddRange(objAux)
                    End If
                Next

                ViewState("Sectores") = objSectores
            End If
            Return ViewState("Sectores")
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ViewState("Sectores") = value
        End Set
    End Property
    Public Property SubCanales As ObservableCollection(Of Clases.SubCanal)
        Get
            Return ViewState("SubCanales")
        End Get
        Set(value As ObservableCollection(Of Clases.SubCanal))
            ViewState("SubCanales") = value
        End Set
    End Property
    Public Property Certificado() As String
        Get
            Return ViewState("Certificado")
        End Get
        Set(value As String)
            ViewState("Certificado") = value
        End Set
    End Property
    Public Property CodigosUltimosCertificados As List(Of String)
        Get
            Return ViewState("CodigosUltimosCertificados")
        End Get
        Set(value As List(Of String))
            ViewState("CodigosUltimosCertificados") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub Inicializar()

        Try

            ConfigurarControleCliente()

            ucPergunta = LoadControl("~/Controles/PopupPergunta.ascx")
            popupCuestion.PopupBase = ucPergunta
            popupCuestion.Height = Nothing

            If Not Page.IsPostBack Then

                Master.HabilitarHistorico = True
                ModoCertificado = Request.QueryString("Modo")
                TipoCertificado = Request.QueryString("Tipo")

                vCertTodasDelegaciones = If(Not String.IsNullOrEmpty(AppSettings("CertTodasDelegaciones")), AppSettings("CertTodasDelegaciones"), False)
                vCertTodosCanales = If(Not String.IsNullOrEmpty(AppSettings("CertTodosCanales")), AppSettings("CertTodosCanales"), False)
                vCertTodosSectores = If(Not String.IsNullOrEmpty(AppSettings("CertTodosSectores")), AppSettings("CertTodosSectores"), False)

                CargarTipoCertificados()
                CargarDelegaciones()
                CargarSubCanales()

                txtFecha.Text = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).ToString("dd/MM/yyyy HH:mm:ss")

                'Se tipo Provisional
                Master.Titulo = Traduzir("002_tituloCertificadoProvisional")
                Certificado = ddlTipoCertificado.SelectedValue

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        lblTipoCertificado.Text = Traduzir("002_lblTipoCertificado")
        lblFecha.Text = Traduzir("002_lblFechaHasta")
        listaDelegaciones.titulo = Traduzir("002_lblDelegaciones")
        listaSubCanales.titulo = Traduzir("002_lblSubCanal")
        listaSectores.titulo = Traduzir("002_lblSectores")

        btnConsultaConfiguracaoSaldo.Text = Traduzir("002_btnConfiguraConfiguracion")
        btnValidarCertificado.Text = Traduzir("002_btnValidaCertificado")

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_GENERAR_CONSULTAR
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub AdicionarScripts()

        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFecha.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

    End Sub

#End Region

#Region "METODOS"
    Private Sub CargarDelegaciones()

        If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
            Dim lista As New Dictionary(Of String, String)
            For Each del In Delegaciones

                lista.Add(del.Identificador, del.Descripcion)

            Next
            listaDelegaciones.cargarLista = listaSectores.ClientID
            listaDelegaciones.obtenerValores = "GenerarCertificado.aspx/obtenerSectores"

            listaDelegaciones.Lista = lista
        End If

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function obtenerSectores(identificador As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            If Not String.IsNullOrEmpty(identificador) Then
                If identificador.EndsWith(";") Then
                    identificador = identificador.TrimEnd(";")
                End If
                Dim _Sectores As ObservableCollection(Of Clases.Sector) = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectoresPorDelegacion(identificador)
                Dim sectoresPadres = _Sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre Is Nothing)

                Dim respostaSectores As New RespuestaObtenerSectores
                If sectoresPadres IsNot Nothing AndAlso sectoresPadres.Count > 0 Then
                    respostaSectores.Lista = New List(Of Clases.Sector)
                    For Each sector In sectoresPadres
                        If sector IsNot Nothing Then
                            respostaSectores.Lista.Add(sector)
                        End If
                    Next
                End If
                _respuesta.Respuesta = respostaSectores

            End If

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function

    Private Sub CargarSubCanales()

        Dim objProxySubcanal As New Comunicacion.ProxyCanal
        Dim objPeticion As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        objPeticion.codigoCanal = Nothing

        objRespuesta = objProxySubcanal.getSubCanalesByCanal(objPeticion)

        If objRespuesta IsNot Nothing AndAlso objRespuesta.Canales IsNot Nothing AndAlso objRespuesta.Canales.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            Dim canales As New ObservableCollection(Of Clases.SubCanal)

            For Each canal In objRespuesta.Canales
                If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                    canal.SubCanales.ForEach(Sub(sc)
                                                 If sc IsNot Nothing Then
                                                     lista.Add(sc.Codigo, sc.Descripcion)
                                                     canales.Add(New Clases.SubCanal With {.Codigo = sc.Codigo, .Descripcion = sc.Descripcion})
                                                 End If
                                             End Sub)
                End If
            Next
            Me.SubCanales = canales
            listaSubCanales.Lista = lista
        End If
    End Sub
    Private Sub CargarSectores()

        Try
            If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then

                'Busca apenas os setores pais
                Dim sectoresPadres = Sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre Is Nothing)

                If sectoresPadres IsNot Nothing AndAlso sectoresPadres.Count > 0 Then

                    Dim lista As New Dictionary(Of String, String)

                    For Each sector In sectoresPadres
                        If sector IsNot Nothing Then
                            lista.Add(sector.Codigo, sector.Descripcion)
                        End If
                    Next

                    listaSectores.Lista = lista

                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Private Sub CargarTipoCertificados()
        ddlTipoCertificado.Items.Clear()
        ddlTipoCertificado.Items.Add(New ListItem(Traduzir("002_lblCertificadoProvisionalSin"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE))
        ddlTipoCertificado.Items.Add(New ListItem(Traduzir("002_lblCertificadoProvisionalCom"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE))
    End Sub
    Protected Sub ConfigurarControleCliente()

        ucClientes.Focus()
        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.NoExhibirSubCliente = True
        Me.ucClientes.NoExhibirPtoServicio = True

        If Cliente IsNot Nothing Then
            Me.ucClientes.Clientes.Clear()
            Me.ucClientes.Clientes.Add(Cliente)
        End If

    End Sub
    Private Function RecuperarDescriptionSubCanal(codigoSubCanal As String) As String

        If Me.SubCanales IsNot Nothing AndAlso Me.SubCanales.Count > 0 Then
            Dim subCanal = Me.SubCanales.Where(Function(d) d IsNot Nothing AndAlso d.Codigo = codigoSubCanal).FirstOrDefault
            If subCanal IsNot Nothing Then
                Return subCanal.Descripcion
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function
    Private Sub PreencherPeticao(ByRef parametrosAuxiliares As ValidacionEjecucionAux,
                                 ByRef objPeticion As GenesisSaldos.Certificacion.ValidarCertificacion.Peticion)

        Dim objSubCanalColeccion As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Dim objDelegacionCollecion As New IAC.ContractoServicio.Delegacion.DelegacionColeccion
        Dim objSectorColeccion As New IAC.ContractoServicio.Setor.GetSectores.SetorColeccion

        Dim objItensSeleccionados As ListItemCollection = Nothing
        objPeticion.DelegacionLogada = InformacionUsuario.DelegacionSeleccionada

        'Delegações Selecionadas
        If listaDelegaciones IsNot Nothing AndAlso listaDelegaciones.valoresSeleccionados IsNot Nothing _
           AndAlso listaDelegaciones.valoresSeleccionados.Count > 0 Then

            objPeticion.CodigoDelegacion = New List(Of String)

            For Each delegacion In listaDelegaciones.valoresSeleccionados

                objPeticion.CodigoDelegacion.Add(RecuperarCodigoDelegacion(delegacion))
                objDelegacionCollecion.Add(New IAC.ContractoServicio.Delegacion.Delegacion With { _
                                           .CodigoDelegacion = RecuperarCodigoDelegacion(delegacion), _
                                           .Description = RecuperarDescriptionDelegacion(delegacion)})

            Next
            parametrosAuxiliares.Delegaciones = objDelegacionCollecion
        End If

        'Sectores Selecionados
        If listaSectores IsNot Nothing AndAlso listaSectores.valoresSeleccionados IsNot Nothing _
           AndAlso listaSectores.valoresSeleccionados.Count > 0 Then

            objPeticion.CodigoSector = New List(Of String)

            For Each sector In listaSectores.valoresSeleccionados

                objPeticion.CodigoSector.Add(RecuperarCodigoSector(sector))
                objSectorColeccion.Add(New IAC.ContractoServicio.Setor.GetSectores.Setor With { _
                                       .codSector = RecuperarCodigoSector(sector), _
                                       .desSector = RecuperarDescriptionSector(sector)})

                RecuperarSectoresHijos(sector, objSectorColeccion, objPeticion.CodigoSector)
            Next
            parametrosAuxiliares.Sectores = objSectorColeccion
        End If

        'SubCanais Selecionados
        If listaSubCanales IsNot Nothing AndAlso listaSubCanales.valoresSeleccionados IsNot Nothing _
           AndAlso listaSubCanales.valoresSeleccionados.Count > 0 Then

            objPeticion.CodigoSubcanal = New List(Of String)

            For Each subCanal In listaSubCanales.valoresSeleccionados

                objPeticion.CodigoSubcanal.Add(subCanal)
                objSubCanalColeccion.Add(New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal With { _
                                         .Codigo = subCanal, _
                                         .Descripcion = RecuperarDescriptionSubCanal(subCanal)})

            Next
            parametrosAuxiliares.Subcanales = objSubCanalColeccion
        End If

        objPeticion.CodigoCliente = Cliente.Codigo
        objPeticion.EstadoCertificado = Certificado
        objPeticion.FechaHoraCertificacion = Convert.ToDateTime(txtFecha.Text)
        If Cliente IsNot Nothing Then
            parametrosAuxiliares.DescricaoCliente = Cliente.Descripcion
        Else
            parametrosAuxiliares.DescricaoCliente = String.Empty
        End If

    End Sub

    Private Sub RecuperarSectoresHijos(identificadorPai As String, ByRef objSectorColeccion As IAC.ContractoServicio.Setor.GetSectores.SetorColeccion, ByRef codigosSectores As List(Of String))
        Dim sectoresHijos = Me.Sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre IsNot Nothing AndAlso _
                                              s.SectorPadre.Identificador = identificadorPai)

        'Adiciona os setores filhos na lista
        If sectoresHijos IsNot Nothing AndAlso sectoresHijos.Count > 0 Then

            For Each sec In sectoresHijos
                codigosSectores.Add(sec.Codigo)
                objSectorColeccion.Add(New IAC.ContractoServicio.Setor.GetSectores.Setor With { _
                                       .codSector = sec.Codigo, _
                                       .desSector = sec.Descripcion})

                RecuperarSectoresHijos(sec.Identificador, objSectorColeccion, codigosSectores)
            Next
        End If

    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Function ValidarCamposObligatorios() As Boolean

        Dim msg As New StringBuilder

        If txtFecha.Text.Equals(String.Empty) Then
            msg.AppendLine(Traduzir("002_lblmsgerroData"))

        ElseIf Not IsDate(txtFecha.Text) Then
            msg.AppendLine(String.Format(Traduzir("err_campo_data_invalida"), Traduzir("002_lblFechaHasta")))
        End If

        'Verifica se o usuário informou um data maior do que a atual
        If Not String.IsNullOrEmpty(txtFecha.Text) AndAlso IsDate(txtFecha.Text) Then
            If Convert.ToDateTime(txtFecha.Text) > DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(InformacionUsuario.DelegacionSeleccionada) Then
                msg.AppendLine(Traduzir("002_lblDataMaiorAtual"))
            End If
        End If

        If listaDelegaciones IsNot Nothing AndAlso listaDelegaciones.valoresSeleccionados.Count = 0 Then
            msg.AppendLine(Traduzir("002_lblmsgErroDelegacion"))
        End If

        If listaSubCanales IsNot Nothing AndAlso listaSubCanales.valoresSeleccionados.Count = 0 Then
            msg.AppendLine(Traduzir("002_lblmsgErroSubcanal"))
        End If

        If listaSectores IsNot Nothing AndAlso listaSectores.valoresSeleccionados.Count = 0 Then
            msg.AppendLine(Traduzir("002_lblmsgErroSector"))
        End If

        If Cliente Is Nothing Then
            msg.AppendLine(Traduzir("002_lblmsgErroCliente"))
        End If

        If msg.Length > 0 Then
            MyBase.MostraMensagemErro(msg.ToString)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub EnviarInformacionesCertificado(parametrosAuxiliares As ValidacionEjecucionAux,
                                               objPeticion As GenesisSaldos.Certificacion.ValidarCertificacion.Peticion)

        Dim Peticion As New GenesisSaldos.Certificacion.DatosCertificacion.Peticion

        parametrosAuxiliares.CodigosUltimosCertificados = CodigosUltimosCertificados

        Peticion.DelegacionLogada = objPeticion.DelegacionLogada

        Peticion.EsTodasDelegaciones = Me.listaDelegaciones.TodosSelecionados
        Peticion.EsTodosCanales = Me.listaSubCanales.TodosSelecionados
        Peticion.EsTodosSectores = Me.listaSectores.TodosSelecionados

        Dim CodigoCertificado As String = GenerarIdentificador()

        Peticion.CodigoCertificado = CodigoCertificado

        Peticion.Cliente = New Clases.Cliente With {.Codigo = Cliente.Codigo}
        Peticion.CodigosDelegaciones = objPeticion.CodigoDelegacion
        Peticion.CodigoEstado = Certificado
        Peticion.CodigoExterno = Peticion.CodigoCertificado
        Peticion.CodigosSectores = objPeticion.CodigoSector
        Peticion.CodigosSubCanales = objPeticion.CodigoSubcanal
        Peticion.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        Peticion.FyhCertificado = objPeticion.FechaHoraCertificacion
        Peticion.GmtCreacion = DateTime.UtcNow
        Peticion.DelegacionLogada = InformacionUsuario.DelegacionSeleccionada

        Session("parametrosAuxiliares") = parametrosAuxiliares
        Session("objPeticionGenerar") = Peticion

        Response.Redireccionar("~/Pantallas/Certificados/ValidacionEjecucion.aspx")

    End Sub
    Private Function GenerarIdentificador() As String

        Try

            Dim objPeticion As New GenesisSaldos.Certificacion.GenerarCodigoCertificado.Peticion
            Dim objRespuesta As New GenesisSaldos.Certificacion.GenerarCodigoCertificado.Respuesta

            objPeticion.CodEstado = Certificado

            If listaDelegaciones.CantidadItens = listaDelegaciones.valoresSeleccionados.Count Then
                objPeticion.BolTodasDelegaciones = True
            ElseIf listaDelegaciones.valoresSeleccionados.Count > 1 Then
                objPeticion.BolVariosDelegaciones = True
            ElseIf listaDelegaciones.valoresSeleccionados.Count = 1 Then
                objPeticion.CodDelegacion = RecuperarCodigoDelegacion(listaDelegaciones.valoresSeleccionados(0))
            End If

            objPeticion.CodCliente = Cliente.Codigo
            objPeticion.FyhCertificado = Convert.ToDateTime(txtFecha.Text)

            If listaSectores.CantidadItens = listaSectores.valoresSeleccionados.Count Then
                objPeticion.BolTodosSectores = True
            ElseIf listaSectores.valoresSeleccionados.Count > 1 Then
                objPeticion.BolVariosSectores = True
            ElseIf listaSectores.valoresSeleccionados.Count = 1 Then
                objPeticion.CodSector = RecuperarCodigoSector(listaSectores.valoresSeleccionados(0))
            End If

            If listaSubCanales.CantidadItens = listaSubCanales.valoresSeleccionados.Count Then
                objPeticion.BolTodosCanales = True
            ElseIf listaSubCanales.valoresSeleccionados.Count > 1 Then
                objPeticion.BolVariosCanales = True
            ElseIf listaSubCanales.valoresSeleccionados.Count = 1 Then
                objPeticion.CodSubcanal = listaSubCanales.valoresSeleccionados(0)
            End If

            Dim objAccion As New AccionGenerarCodigoCertificado()
            objRespuesta = objAccion.Ejecutar(objPeticion)

            Return objRespuesta.CodCertificado

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
        Return Nothing
    End Function

    Private Function RecuperarCodigoDelegacion(Identificador As String) As String

        If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then
            Dim delegacion = Me.Delegaciones.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = Identificador).FirstOrDefault
            If delegacion IsNot Nothing Then
                Return delegacion.Codigo
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function

    Private Function RecuperarDescriptionDelegacion(Identificador As String) As String

        If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then
            Dim delegacion = Me.Delegaciones.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = Identificador).FirstOrDefault
            If delegacion IsNot Nothing Then
                Return delegacion.Descripcion
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function

    Private Function RecuperarCodigoSector(identificador As String) As String

        If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
            Dim sector = Me.Sectores.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = identificador).FirstOrDefault
            If sector IsNot Nothing Then
                Return sector.Codigo
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function

    Private Function RecuperarDescriptionSector(Identificador As String) As String

        If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
            Dim sector = Me.Sectores.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = Identificador).FirstOrDefault
            If sector IsNot Nothing Then
                Return sector.Descripcion
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function

#End Region

#Region "EVENTOS"

    Protected Sub btnValidarCertificado_Click(sender As Object, e As System.EventArgs) Handles btnValidarCertificado.Click
        Try

            If ValidarCamposObligatorios() Then

                Dim parametrosAuxiliares As New ValidacionEjecucionAux
                Dim objPeticion As New GenesisSaldos.Certificacion.ValidarCertificacion.Peticion
                Dim objRespuesta As New GenesisSaldos.Certificacion.ValidarCertificacion.Respuesta
                Dim objAccion As New AccionValidaCertificacion()

                PreencherPeticao(parametrosAuxiliares, objPeticion)

                objRespuesta = objAccion.ValidarCertificacion(objPeticion)

                'Preenche os codigos dos ultimos certificados executados.
                CodigosUltimosCertificados = objRespuesta.CodigosUltimosCertificados

                If Not (objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT) OrElse _
                    objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_YES_NO)) Then
                    MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                    Exit Sub

                ElseIf objRespuesta.CodigoError.Equals(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_YES_NO) Then
                    ucPergunta.pergunta = objRespuesta.MensajeError & Aplicacao.Util.Utilidad.LineBreak & Traduzir("002_MsgPerguntaCertConflito")
                    ucPergunta.FormatoImagem = Web.PopupPergunta.TipoMensagem.alert
                    popupCuestion.AbrirPopup()
                    Exit Sub
                End If

                'Envia as informações do certificado para a pagina que gera o certificado
                EnviarInformacionesCertificado(parametrosAuxiliares, objPeticion)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Private Sub ucPergunta_Fechado(sender As Object, e As PopupEventArgs) Handles ucPergunta.Fechado

        Try

            If e.Resultado Then

                Dim parametrosAuxiliares As New ValidacionEjecucionAux
                Dim objPeticion As New GenesisSaldos.Certificacion.ValidarCertificacion.Peticion

                'Preenche o petição do serviço
                PreencherPeticao(parametrosAuxiliares, objPeticion)

                'Envia as informações do certificado para a pagina que gera o certificado
                EnviarInformacionesCertificado(parametrosAuxiliares, objPeticion)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Sub btnConsultaConfiguracaoSaldo_Click(sender As Object, e As System.EventArgs) Handles btnConsultaConfiguracaoSaldo.Click
        Try
            HttpContext.Current.Session.Add("ClienteSelecionado", Cliente)
            Response.Redireccionar("~/Pantallas/Certificados/ObtenerNivelSaldos.aspx")
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing AndAlso ucClientes.Clientes.Count > 0 Then
                Cliente = ucClientes.Clientes(0)
                btnConsultaConfiguracaoSaldo.Enabled = True
            Else
                btnConsultaConfiguracaoSaldo.Enabled = False
                Cliente = Nothing
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ddlTipoCertificado_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoCertificado.SelectedIndexChanged

        Certificado = ddlTipoCertificado.SelectedValue

    End Sub

#End Region
    
End Class

<Serializable()> _
    <XmlType(Namespace:="urn:ObtenerSectores")> _
    <XmlRoot(Namespace:="urn:ObtenerSectores")> _
    <DataContract()> _
Public Class RespuestaObtenerSectores
    Inherits RespuestaGenerico

#Region " Variáveis "

    Private _Lista As List(Of Clases.Sector)

#End Region

#Region "Propriedades"

    <DataMember()> _
    Public Property Lista() As List(Of Clases.Sector)
        Get
            Return _Lista
        End Get
        Set(value As List(Of Clases.Sector))
            _Lista = value
        End Set
    End Property

#End Region

End Class