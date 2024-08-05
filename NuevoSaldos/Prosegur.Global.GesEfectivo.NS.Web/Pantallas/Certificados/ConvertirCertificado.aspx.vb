Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Web.Login.Configuraciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Report
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.ContractoServicio
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports Newtonsoft.Json
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Runtime.Serialization

Public Class ConvertirCertificado
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
    Private Property TipoCertificado() As String
        Get
            Return ViewState("TipoCertificado")
        End Get
        Set(value As String)
            ViewState("TipoCertificado") = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ViewState("Delegaciones")
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ViewState("Delegaciones") = value
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
    Public Property CertificadoGerados() As GenesisSaldos.Certificacion.ObtenerCertificado.CertificadoColeccion
        Get
            Return DirectCast(ViewState("CertificadoGerados"), GenesisSaldos.Certificacion.ObtenerCertificado.CertificadoColeccion)
        End Get
        Set(value As GenesisSaldos.Certificacion.ObtenerCertificado.CertificadoColeccion)
            ViewState("CertificadoGerados") = value
        End Set
    End Property
    Public Property OIDCertificadoSeleccionado() As String
        Get
            Return ViewState("OIDCertificadoSeleccionado")
        End Get
        Set(value As String)
            ViewState("OIDCertificadoSeleccionado") = value
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
    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ViewState("Sectores")
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ViewState("Sectores") = value
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
                TipoCertificado = Request.QueryString("Tipo")

                vCertTodasDelegaciones = If(Not String.IsNullOrEmpty(AppSettings("CertTodasDelegaciones")), AppSettings("CertTodasDelegaciones"), False)
                vCertTodosCanales = If(Not String.IsNullOrEmpty(AppSettings("CertTodosCanales")), AppSettings("CertTodosCanales"), False)
                vCertTodosSectores = If(Not String.IsNullOrEmpty(AppSettings("CertTodosSectores")), AppSettings("CertTodosSectores"), False)

                CargarTipoCertificados()
                ExhibirDatosSalidas(False)
            End If

            If TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE _
                OrElse TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then

                'Se tipo Provisional
                Master.Titulo = Traduzir("002_tituloCertificadoProvisional")
                Certificado = ddlTipoCertificado.SelectedValue

            ElseIf TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then

                Master.Titulo = Traduzir("002_tituloCertificadoDefinitivo")
                dvTipoCertificado.Style.Item("display") = "none"
                Certificado = TipoCertificado

                listaDelegaciones.modo = Enumeradores.Modo.Consulta
                listaSectores.modo = Enumeradores.Modo.Consulta
                listaSubCanales.modo = Enumeradores.Modo.Consulta

                txtFecha.Enabled = False

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        lblIdentificador.Text = Traduzir("002_lblIdentificador")
        lblTipoCertificado.Text = Traduzir("002_lblTipoCertificado")
        lblFecha.Text = Traduzir("002_lblFechaHasta")
        listaDelegaciones.titulo = Traduzir("002_lblDelegaciones")
        listaSubCanales.titulo = Traduzir("002_lblSubCanal")
        listaSectores.titulo = Traduzir("002_lblSectores")

        btnConsultaConfiguracaoSaldo.Text = Traduzir("002_btnConfiguraConfiguracion")
        btnValidarCertificado.Text = Traduzir("002_btnValidaCertificado")
        btnConsulta.Text = Traduzir("002_lblConsulta")

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_GENERAR_CONSULTAR
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub AdicionarScripts()

        MyBase.AdicionarScripts()

        'Quando definitivo não pode alterar a data
        If TipoCertificado <> Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFecha.ClientID, "True")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
        End If

    End Sub

#End Region

#Region "METODOS"
    Private Sub CargarDelegaciones(delegaciones As ObservableCollection(Of Clases.Delegacion), delegacionesSeleccionadas As List(Of String))

        If delegaciones IsNot Nothing AndAlso delegaciones.Count > 0 Then
            Me.Delegaciones = delegaciones
            Dim lista As New Dictionary(Of String, String)

            If TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO AndAlso delegacionesSeleccionadas IsNot Nothing _
               AndAlso delegacionesSeleccionadas.Count > 0 Then

                For Each del In delegacionesSeleccionadas
                    Dim delegacion = delegaciones.Where(Function(d) d IsNot Nothing AndAlso d.Codigo = del).FirstOrDefault
                    If delegacion IsNot Nothing Then
                        lista.Add(delegacion.Identificador, delegacion.Descripcion)
                    End If
                Next

            Else

                For Each del In delegaciones

                    lista.Add(del.Identificador, del.Descripcion)

                Next

            End If

            If delegacionesSeleccionadas IsNot Nothing AndAlso delegacionesSeleccionadas.Count > 0 Then

                Dim identificadores As New List(Of String)

                delegacionesSeleccionadas.ForEach(Sub(s)
                                                      Dim delegacion = delegaciones.Where(Function(sc) sc.Codigo = s).FirstOrDefault
                                                      If delegacion IsNot Nothing Then
                                                          identificadores.Add(delegacion.Identificador)
                                                      End If
                                                  End Sub)

                listaDelegaciones.valoresSeleccionados = identificadores
            Else
                listaDelegaciones.valoresSeleccionados = Nothing
            End If

            listaDelegaciones.cargarLista = listaSectores.ClientID
            listaDelegaciones.obtenerValores = "ConvertirCertificado.aspx/obtenerSectores"

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

    Private Sub CargarSubCanales(codigosSubCanales As List(Of String))

        Dim objProxySubcanal As New Comunicacion.ProxyCanal
        Dim objPeticion As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        objPeticion.codigoCanal = Nothing

        If codigosSubCanales IsNot Nothing AndAlso codigosSubCanales.Count > 0 Then

            objRespuesta = objProxySubcanal.getSubCanalesByCanal(objPeticion)

            If objRespuesta IsNot Nothing AndAlso objRespuesta.Canales IsNot Nothing AndAlso objRespuesta.Canales.Count > 0 Then

                Dim lista As New Dictionary(Of String, String)
                Dim canales As New ObservableCollection(Of Clases.SubCanal)

                For Each cod In codigosSubCanales

                    For Each canal In objRespuesta.Canales
                        If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                            canal.SubCanales.ForEach(Sub(sc)
                                                         If sc IsNot Nothing AndAlso sc.Codigo = cod Then
                                                             lista.Add(sc.OidSubCanal, sc.Descripcion)
                                                             canales.Add(New Clases.SubCanal With {.Identificador = sc.OidSubCanal, .Descripcion = sc.Descripcion})
                                                         End If
                                                     End Sub)
                        End If
                    Next

                Next
                Me.SubCanales = canales
                listaSubCanales.Lista = lista
            End If

        End If

    End Sub
    Private Sub CargarSubCanales(subCanales As ObservableCollection(Of Clases.SubCanal), subCanalesSeleccionados As List(Of String))

        If subCanales IsNot Nothing AndAlso subCanales.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)
            Dim canales As New ObservableCollection(Of Clases.SubCanal)

            If TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO AndAlso subCanalesSeleccionados IsNot Nothing _
               AndAlso subCanalesSeleccionados.Count > 0 Then


                For Each subc In subCanalesSeleccionados
                    Dim subCanal = subCanales.Where(Function(s) s IsNot Nothing AndAlso s.Codigo = subc).FirstOrDefault
                    If subCanal IsNot Nothing Then
                        canales.Add(New Clases.SubCanal With {.Identificador = subCanal.Identificador, .Codigo = subCanal.Codigo, .Descripcion = subCanal.Descripcion})
                        lista.Add(subCanal.Identificador, subCanal.Descripcion)
                    End If
                Next


            Else
                For Each subCanal In subCanales

                    canales.Add(New Clases.SubCanal With {.Identificador = subCanal.Identificador, .Codigo = subCanal.Codigo, .Descripcion = subCanal.Descripcion})
                    lista.Add(subCanal.Identificador, subCanal.Descripcion)

                Next
            End If

            Me.SubCanales = canales
            If subCanalesSeleccionados IsNot Nothing AndAlso subCanalesSeleccionados.Count > 0 Then

                Dim identificadores As New List(Of String)

                subCanalesSeleccionados.ForEach(Sub(s)
                                                    Dim subCanal = subCanales.Where(Function(sc) sc.Codigo = s).FirstOrDefault
                                                    If subCanal IsNot Nothing Then
                                                        identificadores.Add(subCanal.Identificador)
                                                    End If
                                                End Sub)

                listaSubCanales.valoresSeleccionados = identificadores
            Else
                listaSubCanales.valoresSeleccionados = Nothing
            End If
            listaSubCanales.Lista = lista

        End If

    End Sub
    Private Function FiltrarSectores(sectores As ObservableCollection(Of Clases.Sector), delegacionesSelecionadas As List(Of String)) As ObservableCollection(Of Clases.Sector)

        If delegacionesSelecionadas Is Nothing OrElse delegacionesSelecionadas.Count = 0 Then
            Return sectores
        End If

        Dim sectoresRetorno As New ObservableCollection(Of Clases.Sector)

        For Each delegacion In delegacionesSelecionadas
            sectoresRetorno.AddRange(sectores.Where(Function(sec) sec.Delegacion.Codigo = delegacion).ToList())
        Next

        Return sectoresRetorno

    End Function
    Private Sub CargarSectores(sectores As ObservableCollection(Of Clases.Sector), sectoresSelecionados As List(Of String), delegacionesSelecionadas As List(Of String))

        Try
            If sectores IsNot Nothing AndAlso sectores.Count > 0 Then
                'Setores pais e filhos
                Me.Sectores = sectores
                Dim lista As New Dictionary(Of String, String)

                sectores = FiltrarSectores(sectores, delegacionesSelecionadas)

                'Busca apenas os setores pais para exibir no controle
                Dim sectoresPadres = sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre Is Nothing)

                If TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO AndAlso sectoresSelecionados IsNot Nothing _
                   AndAlso sectoresSelecionados.Count > 0 Then

                    If sectoresPadres IsNot Nothing AndAlso sectoresPadres.Count > 0 Then

                        For Each sec In sectoresSelecionados
                            Dim sector = sectoresPadres.Where(Function(s) s IsNot Nothing AndAlso s.Codigo = sec).FirstOrDefault
                            If sector IsNot Nothing Then
                                lista.Add(sector.Identificador, sector.Descripcion)
                            End If
                        Next

                    End If

                Else
                    If sectoresPadres IsNot Nothing AndAlso sectoresPadres.Count > 0 Then

                        For Each sector In sectoresPadres
                            If sector IsNot Nothing Then
                                lista.Add(sector.Identificador, sector.Descripcion)
                            End If
                        Next

                    End If
                End If

                If sectoresSelecionados IsNot Nothing AndAlso sectoresSelecionados.Count > 0 AndAlso sectoresPadres IsNot Nothing _
                   AndAlso sectoresPadres.Count > 0 Then

                    Dim identificadores As New List(Of String)

                    sectoresSelecionados.ForEach(Sub(s)
                                                     Dim sector = sectoresPadres.Where(Function(sc) sc.Codigo = s).FirstOrDefault
                                                     If sector IsNot Nothing Then
                                                         identificadores.Add(sector.Identificador)
                                                     End If
                                                 End Sub)


                    listaSectores.valoresSeleccionados = identificadores
                Else
                    listaSectores.valoresSeleccionados = Nothing
                End If

                listaSectores.Lista = lista

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Function BuscarSubCanales() As ObservableCollection(Of Clases.SubCanal)

        Dim objProxySubcanal As New Comunicacion.ProxyCanal
        Dim objPeticion As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        objPeticion.codigoCanal = Nothing

        objRespuesta = objProxySubcanal.getSubCanalesByCanal(objPeticion)
        Dim canales As ObservableCollection(Of Clases.SubCanal) = Nothing

        If objRespuesta IsNot Nothing AndAlso objRespuesta.Canales IsNot Nothing AndAlso objRespuesta.Canales.Count > 0 Then

            canales = New ObservableCollection(Of Clases.SubCanal)

            For Each canal In objRespuesta.Canales
                If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                    canal.SubCanales.ForEach(Sub(sc)
                                                 If sc IsNot Nothing Then
                                                     canales.Add(New Clases.SubCanal With {.Identificador = sc.OidSubCanal, .Codigo = sc.Codigo, .Descripcion = sc.Descripcion})
                                                 End If
                                             End Sub)
                End If
            Next

        End If
        Return canales
    End Function
    Private Function BuscarSectores() As ObservableCollection(Of Clases.Sector)

        Dim objSectores As New ObservableCollection(Of Clases.Sector)
        For Each delegacion In Delegaciones
            Dim objAux = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectoresPorDelegacion(delegacion.Identificador)
            If objAux IsNot Nothing AndAlso objAux.Count > 0 Then
                objSectores.AddRange(objAux)
            End If
        Next
        Return objSectores
    End Function
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

    Private Function RecuperarDescriptionSubCanal(Identificador As String) As String

        If Me.SubCanales IsNot Nothing AndAlso Me.SubCanales.Count > 0 Then
            Dim subCanal = Me.SubCanales.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = Identificador).FirstOrDefault
            If subCanal IsNot Nothing Then
                Return subCanal.Descripcion
            Else
                Return String.Empty
            End If
        Else
            Return String.Empty
        End If

    End Function
    Private Function RecuperarCodigoSubCanal(identificador As String) As String

        If Me.SubCanales IsNot Nothing AndAlso Me.SubCanales.Count > 0 Then
            Dim subCanal = Me.SubCanales.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = identificador).FirstOrDefault
            If subCanal IsNot Nothing Then
                Return subCanal.Codigo
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

                Dim sectorePadre = Me.Sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre Is Nothing AndAlso _
                                                          s.Identificador = sector).FirstOrDefault
                If sectorePadre IsNot Nothing Then

                    'Recupera os setores filhos do setor selecionado (No controle de setores aparecem apenas os setores pais para selecionar)
                    Dim sectoresHijos = Me.Sectores.Where(Function(s) s IsNot Nothing AndAlso s.SectorPadre IsNot Nothing AndAlso _
                                                          s.SectorPadre.Identificador = sectorePadre.Identificador)

                    'Adiciona os setores filhos na lista
                    If sectoresHijos IsNot Nothing AndAlso sectoresHijos.Count > 0 Then

                        For Each sec In sectoresHijos

                            objPeticion.CodigoSector.Add(sec.Codigo)
                            objSectorColeccion.Add(New IAC.ContractoServicio.Setor.GetSectores.Setor With { _
                                                   .codSector = sec.Codigo, _
                                                   .desSector = sec.Descripcion})

                        Next
                    End If

                End If

            Next
            parametrosAuxiliares.Sectores = objSectorColeccion
        End If

        'SubCanais Selecionados
        If listaSubCanales IsNot Nothing AndAlso listaSubCanales.valoresSeleccionados IsNot Nothing _
           AndAlso listaSubCanales.valoresSeleccionados.Count > 0 Then

            objPeticion.CodigoSubcanal = New List(Of String)

            For Each subCanal In listaSubCanales.valoresSeleccionados

                objPeticion.CodigoSubcanal.Add(RecuperarCodigoSubCanal(subCanal))
                objSubCanalColeccion.Add(New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal With { _
                                         .Codigo = RecuperarCodigoSubCanal(subCanal), _
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

        If listaDelegaciones IsNot Nothing AndAlso (listaDelegaciones.valoresSeleccionados Is Nothing OrElse listaDelegaciones.valoresSeleccionados.Count = 0) Then
            msg.AppendLine(Traduzir("002_lblmsgErroDelegacion"))
        End If

        If listaSubCanales IsNot Nothing AndAlso (listaSubCanales.valoresSeleccionados Is Nothing OrElse listaSubCanales.valoresSeleccionados.Count = 0) Then
            msg.AppendLine(Traduzir("002_lblmsgErroSubcanal"))
        End If

        If listaSectores IsNot Nothing AndAlso (listaSectores.valoresSeleccionados Is Nothing OrElse listaSectores.valoresSeleccionados.Count = 0) Then
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

        If vCertTodasDelegaciones Then
            Peticion.EsTodasDelegaciones = True
        Else
            Peticion.EsTodasDelegaciones = False
        End If

        If vCertTodosCanales Then
            Peticion.EsTodosCanales = True
        Else
            Peticion.EsTodosCanales = False
        End If

        If vCertTodosSectores Then
            Peticion.EsTodosSectores = True
        Else
            Peticion.EsTodosSectores = False
        End If

        Dim CodigoCertificado As String = GenerarIdentificador()

        If Certificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then

            Peticion.CodigoCertificado = txtIdentificador.Text
            Peticion.CodigoCertificadoDefinitivo = CodigoCertificado

        Else
            Peticion.CodigoCertificado = CodigoCertificado
        End If

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
                objPeticion.CodSubcanal = RecuperarCodigoSubCanal(listaSubCanales.valoresSeleccionados(0))
            End If

            Dim objAccion As New AccionGenerarCodigoCertificado()
            objRespuesta = objAccion.Ejecutar(objPeticion)

            Return objRespuesta.CodCertificado

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
        Return Nothing
    End Function
    Private Sub ExhibirDatosSalidas(exhibir As Boolean)

        dvResultado.Visible = exhibir
        btnConsulta.Visible = exhibir

    End Sub
    Public Sub ExibeCertificados()

        CertificadoGerados = Nothing

        Try

            If Cliente Is Nothing OrElse String.IsNullOrEmpty(Cliente.Codigo) Then
                ExhibirDatosSalidas(False)
                pnlGrid.Visible = False
                btnConsulta.Visible = False
                Exit Sub
            End If

            Dim CodigoCertificado As New List(Of String)

            Dim objPeticion As New GenesisSaldos.Certificacion.ObtenerCertificado.Peticion
            Dim objRespuesta As New GenesisSaldos.Certificacion.ObtenerCertificado.Respuesta

            CodigoCertificado.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)

            If TipoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE OrElse
               TipoCertificado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then

                CodigoCertificado.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE)
            End If

            objPeticion.codigoCliente = Cliente.Codigo
            objPeticion.estadoCertificado = CodigoCertificado
            objPeticion.IdentificadorDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador

            Dim objAccion As New AccionObtenerCertificado()
            objRespuesta = objAccion.ObtenerCertificado(objPeticion)

            If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                MyBase.MostraMensagemErro(objRespuesta.MensajeError)
            ElseIf objRespuesta.Certificado.Count = 0 Then
                CertificadoGerados = Nothing
                pnlGrid.Visible = False
                gdvCertificados.DataSource = Nothing
                gdvCertificados.DataBind()
                MyBase.MostraMensagemErro(Traduzir("002_lblCertificadoNaoEncontrado"))
                btnConsulta.Visible = False
                btnValidarCertificado.Enabled = False
            ElseIf objRespuesta.Certificado.Count > 0 Then
                If objRespuesta.Certificado IsNot Nothing AndAlso _
                    objRespuesta.Certificado.Count > 0 Then
                    CertificadoGerados = objRespuesta.Certificado
                    pnlGrid.Visible = True
                    btnConsulta.Visible = True
                    gdvCertificados.DataSource = objRespuesta.Certificado
                    gdvCertificados.DataBind()
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Private Sub LimparCampos()
        Try

            listaDelegaciones.LimpiarDatos()
            listaSubCanales.LimpiarDatos()
            listaSectores.LimpiarDatos()
            txtIdentificador.Text = String.Empty

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Private Sub GuardaSelecionadoGrid()

        Try

            If (From p In gdvCertificados.Rows.Cast(Of GridViewRow)() _
               Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked).Count = 0 Then
                MyBase.MostraMensagemErro(Traduzir("002_lblSelecioneCertificado"))
                Exit Sub
            End If

            Dim sel = (From p In gdvCertificados.Rows.Cast(Of GridViewRow)() _
                                Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                                Select New GenesisSaldos.Certificacion.Certificado With
                                        {.IdentificadorCertificado = Convert.ToString(gdvCertificados.DataKeys(p.RowIndex).Value.ToString())}).ToList()

            If sel IsNot Nothing Then
                OIDCertificadoSeleccionado = sel(0).IdentificadorCertificado
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "EVENTOS"
    Private Sub btnConsulta_Click(sender As Object, e As System.EventArgs) Handles btnConsulta.Click
        Try

            LimparCampos()
            GuardaSelecionadoGrid()

            If String.IsNullOrEmpty(OIDCertificadoSeleccionado) Then
                Exit Sub
            End If

            Dim objDelegacionCol As New List(Of String)
            Dim objSectorCol As New List(Of String)
            Dim objSubcanalCol As New List(Of String)

            Dim objPeticion As New GenesisSaldos.Certificacion.DatosCertificacion.Peticion
            Dim objRespuesta As New GenesisSaldos.Certificacion.DatosCertificacion.Respuesta

            objPeticion.IdentificadorCertificado = OIDCertificadoSeleccionado
            objPeticion.DelegacionLogada = InformacionUsuario.DelegacionSeleccionada

            Dim objAccion As New AccionObtenerCertificado
            objRespuesta = objAccion.RecuperarFiltrosCertificado(objPeticion)

            If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                MyBase.MostraMensagemErro(objRespuesta.MensajeError)
            End If

            'Se for certificado definitivo não pode alterar os dados de filtro
            If TipoCertificado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then

                listaDelegaciones.modo = Enumeradores.Modo.Consulta
                listaSectores.modo = Enumeradores.Modo.Consulta
                listaSubCanales.modo = Enumeradores.Modo.Consulta

            End If

            If objRespuesta.Certificado IsNot Nothing Then

                ExhibirDatosSalidas(True)

                ' Os 3 FOR foram inseridos para correção do caso: 0048190: Sistema não está carregando corretamente o filtro com os subcanais e setores antes selecionados
                For _delegacion = 0 To objRespuesta.Certificado.CodigosDelegaciones.Count - 1
                    If Not objDelegacionCol.Contains(objRespuesta.Certificado.CodigosDelegaciones(_delegacion)) Then
                        objDelegacionCol.Add(objRespuesta.Certificado.CodigosDelegaciones(_delegacion))
                    End If
                Next _delegacion

                For _sector = 0 To objRespuesta.Certificado.CodigosSectores.Count - 1
                    If Not objDelegacionCol.Contains(objRespuesta.Certificado.CodigosSectores(_sector)) Then
                        objSectorCol.Add(objRespuesta.Certificado.CodigosSectores(_sector))
                    End If
                Next _sector

                For _subcanal = 0 To objRespuesta.Certificado.CodigosSubCanales.Count - 1
                    If Not objDelegacionCol.Contains(objRespuesta.Certificado.CodigosSubCanales(_subcanal)) Then
                        objSubcanalCol.Add(objRespuesta.Certificado.CodigosSubCanales(_subcanal))
                    End If
                Next _subcanal

                txtIdentificador.Text = objRespuesta.Certificado.CodigoCertificado
                txtIdentificador.ToolTip = objRespuesta.Certificado.CodigoCertificado
                txtFecha.Text = objRespuesta.Certificado.FyhCertificado.ToString()


                If objDelegacionCol IsNot Nothing AndAlso objDelegacionCol.Count > 0 Then
                    CargarDelegaciones(InformacionUsuario.Delegaciones, objDelegacionCol)
                End If


                If objSectorCol IsNot Nothing AndAlso objSectorCol.Count > 0 Then

                    CargarSectores(BuscarSectores(), objSectorCol, objDelegacionCol)

                End If

                Dim SubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal = Nothing

                If objSubcanalCol IsNot Nothing AndAlso objSubcanalCol.Count > 0 Then

                    CargarSubCanales(BuscarSubCanales(), objSubcanalCol)
                End If
            Else
                CargarDelegaciones(InformacionUsuario.Delegaciones, Nothing)
                CargarSubCanales(BuscarSubCanales(), Nothing)
                CargarSectores(BuscarSectores(), Nothing, Nothing)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

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

            ExibeCertificados()

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ddlTipoCertificado_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoCertificado.SelectedIndexChanged

        Certificado = ddlTipoCertificado.SelectedValue

    End Sub
    Protected Sub gdvCertificados_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvCertificados.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(1).Text = Traduzir("007_lblCodigoCertificado")
                e.Row.Cells(2).Text = Traduzir("007_lblCodigoEstado")
                e.Row.Cells(3).Text = Traduzir("007_lblFechaCertificado")
            End If

            If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.DataItem IsNot Nothing Then
                If CType(e.Row.DataItem, GenesisSaldos.Certificacion.ObtenerCertificado.Certificado).codEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then
                    e.Row.Cells(2).Text = Traduzir("002_tituloCertificadoDefinitivo")
                ElseIf CType(e.Row.DataItem, GenesisSaldos.Certificacion.ObtenerCertificado.Certificado).codEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                    e.Row.Cells(2).Text = Traduzir("002_lblCertificadoProvisionalCom")
                Else
                    e.Row.Cells(2).Text = Traduzir("002_lblCertificadoProvisionalSin")
                End If
            End If

            If e.Row.FindControl("rbSelecionado") IsNot Nothing Then

                Dim chk As CheckBox = e.Row.FindControl("rbSelecionado")

                chk.Attributes.Add("onclick", "SetaUnicoRadioButton(this,'" & gdvCertificados.ClientID & "');")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Protected Sub gdvCertificados_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvCertificados.PageIndexChanging
        gdvCertificados.PageIndex = e.NewPageIndex
    End Sub

#End Region

End Class

