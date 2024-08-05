Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Unificacion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

Public Class Formulario_v2
    Inherits Base

#Region "[PROPRIEDADES]"

    Private _Modo As Enumeradores.Modo?
    Public ReadOnly Property Modo() As Enumeradores.Modo
        Get
            If Not _Modo.HasValue Then
                If Request.QueryString("Modo") Is Nothing Then
                    _Modo = Enumeradores.Modo.Consulta
                Else
                    _Modo = [Enum].Parse(GetType(Enumeradores.Modo), Request.QueryString("Modo"), True)
                End If
            End If
            Return _Modo.Value
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_FORMULARIOS
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()

        Me.txtColorFormulario.Attributes.Add("onfocusout", "MudaCor(this)")
        Me.txtColorFormulario.Attributes.Add("onclick", "showColorPicker(this,document.forms[0]." & txtColorFormulario.ClientID & ")")
        Me.txtCodigoFormulario.Attributes.Add("onchange", "validarCodigoFormulario()")
        Me.rbCaracteristicaPrincipalGestionRemesa.Attributes.Add("onchange", "controlePaso1()")
        Me.rbCaracteristicaPrincipalGestionBultos.Attributes.Add("onchange", "controlePaso1()")
        Me.rbCaracteristicaPrincipalGestionFondos.Attributes.Add("onchange", "controlePaso1()")
        Me.rbCaracteristicaPrincipalCierres.Attributes.Add("onchange", "controlePaso1()")
        Me.rbCaracteristicaPrincipalOtrosMovimientos.Attributes.Add("onchange", "controlePaso1()")
        Me.rbGestionElementosAltas.Attributes.Add("onchange", "controlePaso1Elementos()")
        Me.rbGestionElementosBajas.Attributes.Add("onchange", "controlePaso1Elementos()")
        Me.rbGestionElementosReenvios.Attributes.Add("onchange", "controlePaso1Elementos()")
        Me.rbGestionElementosActas.Attributes.Add("onchange", "controlePaso1Elementos()")
        Me.rbGestionElementosSustitucion.Attributes.Add("onchange", "controlePaso1Elementos()")
        Me.rbGestionFondosMovimientoFondos.Attributes.Add("onchange", "controlePaso1Fondos()")
        Me.rbGestionFondosAjustes.Attributes.Add("onchange", "controlePaso1Fondos()")
        Me.rbGestionFondosSustitucion.Attributes.Add("onchange", "controlePaso1Fondos()")
        Me.rbGestionFondosSolicitacion.Attributes.Add("onchange", "controlePaso1Fondos()")
        Me.chkImprime.Attributes.Add("onchange", " exibirCopias()")

    End Sub

    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()

            Master.FindControl("pnlMenuRodape").Visible = False
            Master.Titulo = String.Format("{0} ", Traduzir("045_Titulo"))

            If (Not Page.IsPostBack) Then
                litAlert.Text = String.Empty
                ConfigurarControles()
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        MyBase.TraduzirControles()

        lblPaso0.Text = Traduzir("045_Paso_0")
        lblPaso1.Text = Traduzir("045_Paso_1")
        lblPaso2.Text = Traduzir("045_Paso_2")
        lblPaso3.Text = Traduzir("045_Paso_3")
        lblPaso4.Text = Traduzir("045_Paso_4")
        lblPaso5.Text = Traduzir("045_Paso_5")

        litDicionario.Text = "<script type='text/javascript'>" & vbNewLine
        litDicionario.Text &= "var Extension_Invalido = '" & Traduzir("045_Extension_Invalido") & "';" & vbNewLine
        litDicionario.Text &= "var CamposObrigatorios = '" & Traduzir("045_CamposObrigatorios") & "';" & vbNewLine
        litDicionario.Text &= "var legAgregarTodos = '" & Traduzir("btnAgregarTodos") & "';" & vbNewLine
        litDicionario.Text &= "var legDesAgregarTodos = '" & Traduzir("btnDesAgregarTodos") & "';" & vbNewLine
        litDicionario.Text &= "var Modo = '" & Modo.ToString() & "';" & vbNewLine
        litDicionario.Text &= "</script>" & vbNewLine


        'wMain.StepNextButtonText = Traduzir("045_Siguiente")
        'wMain.StartNextButtonText = Traduzir("045_Siguiente")
        'wMain.StepPreviousButtonText = Traduzir("045_Anterior")
        'wMain.FinishCompleteButtonText = Traduzir("045_Grabar")
        'wMain.FinishPreviousButtonText = Traduzir("045_Anterior")
        'wMain.CancelButtonText = Traduzir("045_Cancelar")

        ' itens passo 1
        lblCodigoFormulario.Text = Traduzir("045_Codigo")
        lblDescripcionFormulario.Text = Traduzir("045_Descripcion")
        lblColorFormulario.Text = Traduzir("045_Color_Cabecera")
        lblTipoDocumento.Text = Traduzir("045_Tipo_Documento")
        chkDocumentoIndividual.Text = Traduzir("045_Movimento_Individual")
        chkDocumentoGrupo.Text = Traduzir("045_Movimento_Agrupada")
        chkActivo.Text = Traduzir("045_Disponible_Para_Uso")
        lblCargar.Text = Traduzir("045_Cargar")
        lblIcono.Text = Traduzir("045_Ruta_Cargar_Icono")

        '' itens passo 2
        lblCaracteristicaPrincipal.Text = Traduzir("045_Caracteristica_Principal")
        rbCaracteristicaPrincipalCierres.Text = Traduzir("045_Cierres")
        rbCaracteristicaPrincipalGestionBultos.Text = Traduzir("045_Gestion_Bultos")
        rbCaracteristicaPrincipalGestionFondos.Text = Traduzir("045_Gestion_Fondos")
        rbCaracteristicaPrincipalGestionRemesa.Text = Traduzir("045_Gestion_Remesas")
        rbCaracteristicaPrincipalOtrosMovimientos.Text = Traduzir("045_Otros_Movimentos")
        lblGestionElementos.Text = Traduzir("045_Gestion_Elementos_Realizar")
        lblGestionFondos.Text = Traduzir("045_Gestion_Fondos_Realizar")
        lblCierres.Text = Traduzir("045_Cuadres_Cierres")
        rbGestionElementosActas.Text = Traduzir("045_Actas")
        rbGestionElementosAltas.Text = Traduzir("045_Altas")
        rbGestionElementosBajas.Text = Traduzir("045_Bajas")
        rbGestionElementosReenvios.Text = Traduzir("045_Reenvios")
        rbGestionElementosSustitucion.Text = Traduzir("045_Sustitucion")
        lblTipoBulto.Text = Traduzir("045_Tipo_Bulto_Tratar")
        ckbkPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")
        rbGestionFondosMovimientoFondos.Text = Traduzir("045_Movimientos_De_Fondos")
        rbGestionFondosAjustes.Text = Traduzir("045_Ajustes")
        rbGestionFondosSustitucion.Text = Traduzir("045_Sustitucion")
        rbGestionFondosSolicitacion.Text = Traduzir("045_Solicitacion")
        rbCierresTesoro.Text = Traduzir("045_Cierre_De_Tesoro")
        rbCierresCaja.Text = Traduzir("045_Cierre_De_Caja")
        rbCierresCuadreFisico.Text = Traduzir("045_Cuadre_Fisico")
        rbGestionElementosBajasBajaElementos.Text = Traduzir("045_Baja_Remesa_Sistema")
        rbGestionElementosBajasSalidasRecorrido.Text = Traduzir("045_Salida_Remesa_Ruta")
        rbGestionElementosReenviosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionElementosReenviosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        rbGestionElementosActasActaRecuento.Text = Traduzir("045_Acta_Recuento")
        rbGestionElementosActasActaClasificado.Text = Traduzir("045_Acta_Clasificado")
        rbGestionElementosActasActaEmbolsado.Text = Traduzir("045_Acta_Embolsado")
        rbGestionElementosActasActaDesembolsado.Text = Traduzir("045_Acta_Desembolsado")
        lblFiltro.Text = Traduzir("045_Basado_Reporte")
        rbGestionFondosMovimientoFondosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionFondosMovimientoFondosEntreCanales.Text = Traduzir("045_Entre_Canales")
        rbGestionFondosMovimientoFondosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        rbGestionFondosMovimientoFondosEntreClientes.Text = Traduzir("045_Entre_Clientes")
        lblBajas.Text = Traduzir("045_Gestion_Elementos_Bajas_Detalles")
        lblReenvios.Text = Traduzir("045_Gestion_Elementos_Reenvio_Detalles")
        lblActas.Text = Traduzir("045_Gestion_Elementos_Actas_Detalles")
        lblFondosMovimiento.Text = Traduzir("045_Gestion_Fondos_Movimiento_Detalles")


        ' itens passo 3
        lblTipoSectorOrigen.Text = Traduzir("045_Tipos_Sectores_Origen")
        lblTipoSectorDestino.Text = Traduzir("045_Tipos_Sectores_Destino")



        'lblAccionContable.Text = Traduzir("045_Accion_Contable")
        'lblAccionContableEstadosPosibles.Text = Traduzir("045_Estados_Posibles_Documento")
        'lblAccionContableAccionesPosibles.Text = Traduzir("045_Acciones_Contables_Estados")
        'lblAccionContableDisponibleOrigen.Text = Traduzir("045_Disponible_Origen")
        'lblAccionContableNoDisponibleOrigen.Text = Traduzir("045_No_Disponible_Origen")
        'lblAccionContableDisponibleDestino.Text = Traduzir("045_Disponible_Destino")
        'lblAccionContableNoDisponibleDestino.Text = Traduzir("045_No_Disponible_Destino")
        'lblAccionContableEstadoConfirmado.Text = Traduzir("045_Confirmado")
        'lblAccionContableEstadoAceptado.Text = Traduzir("045_Aceptado")
        'lblAccionContableEstadoRechazado.Text = Traduzir("045_Rechazado")
        chkImprime.Text = Traduzir("045_Se_Imprime")
        lblImprimeCopias.Text = Traduzir("045_Copias")
        lblIacIndividual.Text = Traduzir("045_IAC_Documento")
        lblIACGrupo.Text = Traduzir("045_IAC_Documento_Grupo")
        chkCodigoExternoObligatorio.Text = Traduzir("045_Codigo_Externo_Obligatorio")
        chkIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        chkIntegracionRecepcionEnvio.Text = Traduzir("045_Permite_Integracion_Recepcion")
        chkIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        chkIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        'lblResumenIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        'lblResumenIntegracionRecepcion.Text = Traduzir("045_Permite_Integracion_Recepcion")
        'lblResumenIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        'lblResumenIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        chkSolicitacionFondos.Text = Traduzir("045_Permite_Solicitacion_Fondos")
        chkContestarSolicitacionFondos.Text = Traduzir("045_Permite_Contestar_Solicitacion_Fondos")

        '' paso 7

        'lblResumenIdentificacion.Text = Traduzir("045_Identificacion")
        'lblResumenCodigo.Text = Traduzir("045_Codigo") & ":"
        'lblResumenDescripcion.Text = Traduzir("045_Descripcion") & ":"
        'lblResumenColor.Text = Traduzir("045_Color")
        'lblResumenTipoDocumento.Text = Traduzir("045_Tipo_Doc")
        'lblResumenIndividual.Text = Traduzir("045_Permite_Generar_Individual")
        'lblResumenGrupo.Text = Traduzir("045_Permite_Generar_Agrupado")
        'lblResumenActivo.Text = Traduzir("045_Disponible_Para_Uso")
        'lblCodigoExternoObligatorio.Text = Traduzir("045_Codigo_Externo_Obligatorio")

        'lblResumenObjectivo.Text = Traduzir("045_Objectivo")
        'lblResumenCaracteristicas.Text = Traduzir("045_Caracteristicas")
        'lblResumenTiposBultos.Text = Traduzir("045_Tipo_De_Bultos")

        'lblResumenVisibilidadUso.Text = Traduzir("045_Visibilidad_Uso")
        'lblResumenTiposSectoresOrigen.Text = Traduzir("045_Documento_Podra_Creado_Sectores")
        'lblResumenTiposSectoresDestino.Text = Traduzir("045_Documento_Podra_Enviado_Sectores")

        'lblResumenOperacionesContablesOtrosDatos.Text = Traduzir("045_Operaciones_Contables_Otros_Datos")
        'lblResumenAccionContable.Text = Traduzir("045_Accion_Contable") & ": "
        'lblResumenAccionContableEstado.Text = Traduzir("045_Estado")
        'lblResumenAccionContableDisponibleOrigen.Text = Traduzir("045_Disponible_Origen")
        'lblResumenAccionContableNoDisponibleOrigen.Text = Traduzir("045_No_Disponible_Origen")
        'lblResumenAccionContableDisponibleDestino.Text = Traduzir("045_Disponible_Destino")
        'lblResumenAccionContableNoDisponibleDestino.Text = Traduzir("045_No_Disponible_Destino")
        'lblResumenAccionContableConfirmado.Text = Traduzir("045_Confirmado")
        'lblResumenAccionContableAceptado.Text = Traduzir("045_Aceptado")
        'lblResumenAccionContableRechazado.Text = Traduzir("045_Rechazado")
        'lblResumenPermiteLlegarSaldoNegativo.Text = Traduzir("045_Permite_Llegar_Saldo_Negativo")
        'lblResumenImprime.Text = Traduzir("045_Impreso_Al_Confirmmar")
        'lblResumenImprimeCopias.Text = Traduzir("045_Copias_Impresion_Para")
        'lblResumenIAC.Text = Traduzir("045_IAC_Documento") & ":"
        'lblResumenIACGrupo.Text = Traduzir("045_IAC_Documento_Grupo") & ":"
        'lblResumenIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        'lblResumenIntegracionRecepcion.Text = Traduzir("045_Permite_Integracion_Recepcion")
        'lblResumenIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        'lblResumenIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        'lblResumenSolicitacionFondos.Text = Traduzir("045_Permite_Solicitacion_Fondos")
        'lblResumenContestarSolicitacionFondos.Text = Traduzir("045_Permite_Contestar_Solicitacion_Fondos")

    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function ValidarCodigoFormulario(codigoFormulario As String) As String
        If LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPorCodigo(codigoFormulario) IsNot Nothing Then
            Return Traduzir("045_Codigo_Formulario_Existe")
        End If
        Return "OK"
    End Function

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configurar los Controles en la pantallas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarControles()

        ConfigurarTiposDocumento()
        ConfigurarTiposBultos()
        ConfigurarFiltros()
        ConfigurarTipoSectores()
        CarregaDropDownIAC()

    End Sub

    Public Sub ConfigurarTiposDocumento()
        Try
            ddlTipoDocumento.Items.Clear()
            ddlTipoDocumento.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))

            For Each item In LogicaNegocio.GenesisSaldos.MaestroDocumentos.RecuperaTiposDocumentos()
                ddlTipoDocumento.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ConfigurarTiposBultos()
        Try
            ckbTipoBulto.Items.Clear()
            Dim tiposBultos = LogicaNegocio.Genesis.TipoBulto.ObtenerTiposBultos

            If tiposBultos IsNot Nothing AndAlso tiposBultos.Count > 0 Then

                ckbTipoBulto.DataTextField = "Descripcion"
                ckbTipoBulto.DataValueField = "Identificador"
                ckbTipoBulto.DataSource = tiposBultos
                ckbTipoBulto.DataBind()

            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ConfigurarFiltros()
        Try
            ddlFiltro.Items.Clear()
            ddlFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))

            For Each item In LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFiltrosFormulario
                ddlFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ConfigurarTipoSectores()
        Try
            cklTipoSectorOrigen.Items.Clear()
            cklTipoSectorDestino.Items.Clear()
            Dim tiposSectores = LogicaNegocio.Genesis.TipoSector.ObtenerTiposSectores

            If tiposSectores IsNot Nothing AndAlso tiposSectores.Count > 0 Then
                cklTipoSectorOrigen.DataTextField = "Descripcion"
                cklTipoSectorOrigen.DataValueField = "Identificador"
                cklTipoSectorOrigen.DataSource = tiposSectores
                cklTipoSectorOrigen.DataBind()

                cklTipoSectorDestino.DataTextField = "Descripcion"
                cklTipoSectorDestino.DataValueField = "Identificador"
                cklTipoSectorDestino.DataSource = tiposSectores
                cklTipoSectorDestino.DataBind()
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub CarregaDropDownIAC()
        Try
            ddlIacIndividual.Items.Clear()
            ddlIacIndividual.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlIACGrupo.Items.Clear()
            ddlIACGrupo.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            For Each item In LogicaNegocio.Genesis.InformacaoAdicionalCliente.ObtenerInformacionesAdicionais
                ddlIacIndividual.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlIACGrupo.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

End Class