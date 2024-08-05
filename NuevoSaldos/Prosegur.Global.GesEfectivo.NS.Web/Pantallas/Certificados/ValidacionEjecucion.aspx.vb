Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Web.Login.Configuraciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports ContractoGenesis = Prosegur.Genesis.ContractoServicio
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports Prosegur.Global.GesEfectivo.IAC

Public Class ValidacionEjecucion
    Inherits Base

#Region "VARIÁVEIS"

#End Region

#Region "[PROPRIEDADES]"

    Public Property Certificado() As Certificacion.DatosCertificacion.Peticion
        Get
            Return ViewState("Certificado")
        End Get
        Set(value As Certificacion.DatosCertificacion.Peticion)
            ViewState("Certificado") = value
        End Set
    End Property

    Public Property ParametrosAuxiliares() As ValidacionEjecucionAux
        Get
            Return DirectCast(ViewState("ParametrosAuxiliares"), ValidacionEjecucionAux)
        End Get
        Set(value As ValidacionEjecucionAux)
            ViewState("ParametrosAuxiliares") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                Master.Titulo = Traduzir("006_lblTituloEjecutaCertificado")
                Master.HabilitarHistorico = True

                ConsumirValidacionCertificado()
                ComsumirParametrosAuxiliares()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        lblTituloEntrada.Text = Traduzir("002_lblDadosSaida")
        lblAviso.Text = Traduzir("006_lblAviso")
        lblClienteTotalizador.Text = Traduzir("002_lblClienteTotalizador")
        lblTipoCertificado.Text = Traduzir("002_lblTipoCertificado")
        lblFecha.Text = Traduzir("002_lblFechaHasta")
        lblIdentificador.Text = Traduzir("002_lblIdentificador")
        lblUltimoEjecucion.Text = Traduzir("006_lblUltimaExecucao")
        lblCliSubCliPtoTotalizaSaldo.Text = Traduzir("002_lblClienteTotalizadordeSaldo")
        btnCancelar.Text = Traduzir("002_btnEliminar")
        btnEjecutarCertificado.Text = Traduzir("002_btnExecutar")
        listaDelegaciones.titulo = Traduzir("002_lblDelegaciones")
        listaSubCanales.titulo = Traduzir("002_lblSubCanal")
        listaSectores.titulo = Traduzir("002_lblSectores")
        listaClientes.titulo = Traduzir("006_lblCliente")
        listaSubClientes.titulo = Traduzir("006_lblSubcliente")
        listaPuntosServicio.titulo = Traduzir("006_lblPuntoServicio")

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False

    End Sub
    Protected Overrides Sub AdicionarScripts()

    End Sub

#End Region

#Region "METODOS"

    Private Sub ConsumirValidacionCertificado()

        Try

            If Session("objPeticionGenerar") IsNot Nothing Then

                Certificado = DirectCast(Session("objPeticionGenerar"), Certificacion.DatosCertificacion.Peticion)

                Session.Remove("objPeticionGenerar")

                If Certificado IsNot Nothing Then

                    Dim objPeticion As New Certificacion.ObtenerNivelSaldos.Peticion
                    Dim objRespuesta As New Certificacion.ObtenerNivelSaldos.Respuesta

                    lblDatoClienteTotalizador.Text = If(Certificado.Cliente IsNot Nothing, Certificado.Cliente.Codigo, String.Empty)
                    lblDatoClienteTotalizador.ToolTip = If(Certificado.Cliente IsNot Nothing, Certificado.Cliente.Codigo, String.Empty)

                    lblDatoFecha.Text = Certificado.FyhCertificado.ToString()
                    lblDatoFecha.ToolTip = Certificado.FyhCertificado.ToString()

                    Dim CodigoCertificado As String = If(Certificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO, _
                                                     Certificado.CodigoCertificadoDefinitivo, _
                                                     Certificado.CodigoCertificado)

                    lblDatoIdentificador.Text = CodigoCertificado
                    lblDatoIdentificador.ToolTip = CodigoCertificado

                    If Certificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then

                        lblDatoTipoCertificado.Text = Traduzir("006_lblDefinitivo")
                        lblDatoTipoCertificado.ToolTip = Traduzir("006_lblDefinitivo")

                    ElseIf Certificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then

                        lblDatoTipoCertificado.Text = Traduzir("006_lblProvisionalCon")
                        lblDatoTipoCertificado.ToolTip = Traduzir("006_lblProvisionalCon")

                    Else

                        lblDatoTipoCertificado.Text = Traduzir("006_lblProvisionalSim")
                        lblDatoTipoCertificado.ToolTip = Traduzir("006_lblProvisionalSim")

                    End If

                    If Certificado.Cliente IsNot Nothing Then
                        objPeticion.CodClienteTotalizador = Certificado.Cliente.Codigo
                    End If

                    objPeticion.CodSubCanal = Certificado.CodigosSubCanales
                    objPeticion.Delegacion = InformacionUsuario.DelegacionSeleccionada

                    Dim objAccion As New AccionObtenerNivelSaldos()
                    objRespuesta = objAccion.Ejecutar(objPeticion)

                    If objRespuesta.NivelSaldos Is Nothing Then
                        Exit Sub
                    End If

                    Dim objPuntoServicioCollecion As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicioColeccion(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio)
                    Dim objSubclienteCollecion As New IAC.ContractoServicio.SubCliente.GetSubClientes.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente)
                    Dim objClienteCollecion As New IAC.ContractoServicio.Cliente.GetClientes.ClienteColeccion(Of IAC.ContractoServicio.Cliente.GetClientes.Cliente)

                    Dim objCliente As IAC.ContractoServicio.Cliente.GetClientes.Cliente = Nothing
                    Dim objSubcliente As IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente = Nothing
                    Dim objPuntoServicio As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio = Nothing

                    For Each item In objRespuesta.NivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo

                        objCliente = New IAC.ContractoServicio.Cliente.GetClientes.Cliente
                        objSubcliente = New IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente
                        objPuntoServicio = New IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio

                        objCliente.CodCliente = item.CodCliente
                        objCliente.DesCliente = item.DesCliente

                        objSubcliente.CodSubCliente = item.CodSubcliente
                        objSubcliente.DesSubCliente = item.DesSubcliente

                        objPuntoServicio.CodPuntoServicio = item.CodPtoServicio
                        objPuntoServicio.DesPuntoServicio = item.DesPtoServicio

                        objClienteCollecion.Add(objCliente)
                        objPuntoServicioCollecion.Add(objPuntoServicio)
                        objSubclienteCollecion.Add(objSubcliente)

                    Next

                    CargarClientes(objClienteCollecion)
                    CargarSubClientes(objSubclienteCollecion)
                    CargarPuntosServicios(objPuntoServicioCollecion)

                End If

            Else
                btnCancelar.Enabled = False
                btnEjecutarCertificado.Enabled = False
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub ComsumirParametrosAuxiliares()

        If Session("parametrosAuxiliares") IsNot Nothing Then

            ParametrosAuxiliares = DirectCast(Session("parametrosAuxiliares"), ValidacionEjecucionAux)
            Session.Remove("parametrosAuxiliares")

            If ParametrosAuxiliares IsNot Nothing Then

                CargarDelegaciones(ParametrosAuxiliares.Delegaciones)
                CargarSubCanales(ParametrosAuxiliares.Subcanales)
                CargarSectores(ParametrosAuxiliares.Sectores)

                If ParametrosAuxiliares.CodigosUltimosCertificados IsNot Nothing AndAlso ParametrosAuxiliares.CodigosUltimosCertificados.Count > 0 Then

                    Dim Codigos As String = Join(CType((From ue In ParametrosAuxiliares.CodigosUltimosCertificados Select ue), IEnumerable(Of String)).ToArray, vbCrLf)
                    lblDatoUltimoEjecucion.Text = Codigos
                    lblDatoUltimoEjecucion.ToolTip = Codigos

                End If

                lblDatoClienteTotalizador.Text += " - " & ParametrosAuxiliares.DescricaoCliente
                lblDatoClienteTotalizador.ToolTip += " - " & ParametrosAuxiliares.DescricaoCliente

            End If
        End If

    End Sub

    Private Sub CargarDelegaciones(delegaciones As IAC.ContractoServicio.Delegacion.DelegacionColeccion)

        If delegaciones IsNot Nothing AndAlso delegaciones.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each delegacion In delegaciones

                If Not String.IsNullOrEmpty(delegacion.CodDelegacion) AndAlso Not lista.ContainsKey(delegacion.CodDelegacion) Then
                    lista.Add(delegacion.CodDelegacion, delegacion.Description)
                End If
            Next

            listaDelegaciones.Lista = lista

        End If

    End Sub

    Private Sub CargarSubCanales(subCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)

        If subCanales IsNot Nothing AndAlso subCanales.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each subCanal In subCanales

                If Not String.IsNullOrEmpty(subCanal.Codigo) AndAlso Not lista.ContainsKey(subCanal.Codigo) Then
                    lista.Add(subCanal.Codigo, subCanal.Descripcion)
                End If
            Next

            listaSubCanales.Lista = lista

        End If

    End Sub

    Private Sub CargarSectores(sectores As IAC.ContractoServicio.Setor.GetSectores.SetorColeccion)

        If sectores IsNot Nothing AndAlso sectores.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each sector In sectores

                If Not String.IsNullOrEmpty(sector.codSector) AndAlso Not lista.ContainsKey(sector.codSector) Then
                    lista.Add(sector.codSector, sector.desSector)
                End If

            Next

            listaSectores.Lista = lista

        End If

    End Sub

    Private Sub CargarClientes(clientes As IAC.ContractoServicio.Cliente.GetClientes.ClienteColeccion(Of IAC.ContractoServicio.Cliente.GetClientes.Cliente))

        If clientes IsNot Nothing AndAlso clientes.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each cliente In clientes

                If Not String.IsNullOrEmpty(cliente.CodCliente) AndAlso Not lista.ContainsKey(cliente.CodCliente) Then
                    lista.Add(cliente.CodCliente, cliente.DesCliente)
                End If

            Next

            listaClientes.Lista = lista

        End If

    End Sub

    Private Sub CargarSubClientes(subClientes As IAC.ContractoServicio.SubCliente.GetSubClientes.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientes.SubCliente))

        If subClientes IsNot Nothing AndAlso subClientes.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each subCliente In subClientes

                If Not String.IsNullOrEmpty(subCliente.CodSubCliente) AndAlso Not lista.ContainsKey(subCliente.CodSubCliente) Then
                    lista.Add(subCliente.CodSubCliente, subCliente.DesSubCliente)
                End If

            Next

            listaSubClientes.Lista = lista

        End If

    End Sub

    Private Sub CargarPuntosServicios(puntosServicios As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicioColeccion(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.PuntoServicio))

        If puntosServicios IsNot Nothing AndAlso puntosServicios.Count > 0 Then

            Dim lista As New Dictionary(Of String, String)

            For Each puntoServicio In puntosServicios

                If Not String.IsNullOrEmpty(puntoServicio.CodPuntoServicio) AndAlso Not lista.ContainsKey(puntoServicio.CodPuntoServicio) Then
                    lista.Add(puntoServicio.CodPuntoServicio, puntoServicio.DesPuntoServicio)
                End If
            Next

            listaPuntosServicio.Lista = lista

        End If

    End Sub

    Private Function ValidaParametroCerficados() As Boolean

        If String.IsNullOrEmpty(lblDatoClienteTotalizador.Text) Then
            Return False
        End If
        Return True
    End Function

#End Region

#Region "EVENTOS"

    Private Sub btnEjecutarCertificado_Click(sender As Object, e As System.EventArgs) Handles btnEjecutarCertificado.Click

        Dim objRespuesta As New Certificacion.GenerarCertificado.Respuesta
        Dim objPeticion As New Certificacion.GenerarCertificado.Peticion

        Try

            If Not ValidaParametroCerficados() Then
                Exit Sub
            End If

            With objPeticion
                .DelegacionLogada = InformacionUsuario.DelegacionSeleccionada
                .Cliente = Certificado.Cliente
                .CodigoCertificado = Certificado.CodigoCertificado
                .CodigoCertificadoDefinitivo = Certificado.CodigoCertificadoDefinitivo
                .CodigoEstado = Certificado.CodigoEstado
                .CodigoExterno = Certificado.CodigoExterno
                .CodigoPais = Certificado.CodigoPais
                .CodigosDelegaciones = Certificado.CodigosDelegaciones
                .CodigosSectores = Certificado.CodigosSectores
                .CodigosSubCanales = Certificado.CodigosSubCanales
                .DelegacionLogada = Certificado.DelegacionLogada
                .DescripcionEstado = Certificado.DescripcionEstado
                .EsTodasDelegaciones = Certificado.EsTodasDelegaciones
                .EsTodosCanales = Certificado.EsTodosCanales
                .EsTodosSectores = Certificado.EsTodosSectores
                .FyhCertificado = Certificado.FyhCertificado
                .GmtCreacion = Certificado.GmtCreacion
                .IdentificadorCertificado = Certificado.IdentificadorCertificado
                .UsuarioCreacion = Certificado.UsuarioCreacion
            End With


            Dim objAccion As New AccionGenerarCertificacion()
            objRespuesta = objAccion.Ejecutar(objPeticion)

            If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                Exit Sub
            End If

            If Session("objPeticionGenerar") IsNot Nothing Then
                Session.Remove("objPeticionGenerar")
            End If

            If Certificado.CodigoEstado = ContractoGenesis.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then
                Response.Redireccionar("~/Pantallas/Certificados/ResultadoCertificacion.aspx?Codcertificado=" & Certificado.CodigoCertificadoDefinitivo)
            Else
                Response.Redireccionar("~/Pantallas/Certificados/ResultadoCertificacion.aspx?Codcertificado=" & Certificado.CodigoCertificado)
            End If


        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        If Not String.IsNullOrEmpty(Certificado.CodigoEstado) Then
            Response.Redireccionar("~/Pantallas/Certificados/ConvertirCertificado.aspx?Tipo=" & Certificado.CodigoEstado)
        Else
            Response.Redireccionar("~/Pantallas/Certificados/Default.aspx")
        End If

    End Sub

#End Region
End Class