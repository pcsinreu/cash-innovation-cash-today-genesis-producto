Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Drawing
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Web.UI.WebControls
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class Formulario
    Inherits Base

    Private _Modo As Enumeradores.Modo?
    Public ReadOnly Property Modo() As Enumeradores.Modo
        Get
            If Not _Modo.HasValue Then
                _Modo = [Enum].Parse(GetType(Enumeradores.Modo), Request.QueryString("Modo"), True)
            End If
            Return _Modo.Value
        End Get
    End Property

    Private _FormularioGrabado As Clases.Formulario = Nothing
    Public Property FormularioGrabado() As Clases.Formulario
        Get
            If _FormularioGrabado Is Nothing Then
                _FormularioGrabado = ViewState("_FormularioGrabado")
            End If
            Return _FormularioGrabado
        End Get
        Set(value As Clases.Formulario)
            _FormularioGrabado = value
            ViewState("_FormularioGrabado") = _FormularioGrabado
        End Set
    End Property
    Private _IconeParaSalvar() As Byte
    Public Property IconeParaSalvar() As Byte()
        Get
            If _IconeParaSalvar Is Nothing Then
                _IconeParaSalvar = ViewState("_IconeParaSalvar")
            End If
            Return _IconeParaSalvar
        End Get
        Set(value As Byte())
            _IconeParaSalvar = value
            ViewState("_IconeParaSalvar") = _IconeParaSalvar
        End Set
    End Property
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_FORMULARIOS
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub AdicionarScripts()

        MyBase.AdicionarScripts()

        Me.txtColor.Attributes.Add("onfocusout", "MudaCor(this)")
        Me.txtColor.Attributes.Add("onclick", "showColorPicker(this,document.forms[0]." & txtColor.ClientID & ")")

        fupImagem.Attributes.Add("onchange", "return checkFileExtension(this,'" & Traduzir("045_Extension_Invalido") & "');")
        rbGestionFondosAjustes.Attributes.Add("onchange", "exibeTipoAjustes();")
        rbGestionFondosMovimientoFondos.Attributes.Add("onchange", "exibeTipoAjustes();")
        rbGestionFondosSustitucion.Attributes.Add("onchange", "exibeTipoAjustes();")
        rbGestionFondosSolicitacion.Attributes.Add("onchange", "exibeTipoAjustes();")
        rbGestionFondosClasificacion.Attributes.Add("onchange", "exibeTipoAjustes();")

        rbCierresTesoro.Attributes.Add("onchange", "displayOpcionesCierreCaja(); exibeCierreExcluirSectoresHijos(document.getElementById('" + rbCierresTesoro.ClientID + "'));")
        rbCierresCaja.Attributes.Add("onchange", "displayOpcionesCierreCaja(); exibeCierreExcluirSectoresHijos(document.getElementById('" + rbCierresCaja.ClientID + "'));")
        rbCierresCuadreFisico.Attributes.Add("onchange", "displayOpcionesCierreCaja(); exibeCierreExcluirSectoresHijos(document.getElementById('" + rbCierresCuadreFisico.ClientID + "'));")

        rbGestionRemesasAltas.Attributes.Add("onchange", "exibeRemesasExcluirSectoresHijos(document.getElementById('" + rbGestionRemesasAltas.ClientID + "'));")
        rbGestionRemesasBajas.Attributes.Add("onchange", "exibeRemesasExcluirSectoresHijos(document.getElementById('" + rbGestionRemesasBajas.ClientID + "'));")
        rbGestionRemesasReenvios.Attributes.Add("onchange", "exibeRemesasExcluirSectoresHijos(document.getElementById('" + rbGestionRemesasReenvios.ClientID + "'));")
        rbGestionRemesasActas.Attributes.Add("onchange", "exibeRemesasExcluirSectoresHijos(document.getElementById('" + rbGestionRemesasActas.ClientID + "'));")
        rbGestionRemesasSustitucion.Attributes.Add("onchange", "exibeRemesasExcluirSectoresHijos(document.getElementById('" + rbGestionRemesasSustitucion.ClientID + "'));")

        rbGestionBultosAltas.Attributes.Add("onchange", "exibeBultosExcluirSectoresHijos(document.getElementById('" + rbGestionBultosAltas.ClientID + "'));")
        rbGestionBultosBajas.Attributes.Add("onchange", "exibeBultosExcluirSectoresHijos(document.getElementById('" + rbGestionBultosBajas.ClientID + "'));")
        rbGestionBultosReenvios.Attributes.Add("onchange", "exibeBultosExcluirSectoresHijos(document.getElementById('" + rbGestionBultosReenvios.ClientID + "'));")
        rbGestionBultosActas.Attributes.Add("onchange", "exibeBultosExcluirSectoresHijos(document.getElementById('" + rbGestionBultosActas.ClientID + "'));")
        rbGestionBultosSustitucion.Attributes.Add("onchange", "exibeBultosExcluirSectoresHijos(document.getElementById('" + rbGestionBultosSustitucion.ClientID + "'));")

    End Sub
    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()

            Master.FindControl("pnlMenuRodape").Visible = False
            Master.Titulo = String.Format("{0} ", Traduzir("045_Titulo"))

            'Seta mensagem de confirmação ao cancelar
            AdicionaConfirmacaoCancelar()

            If (Not Page.IsPostBack) Then

                EjecutarNavegacion()

                CargaCombosListas()

                Cargar()

                TraduzirControles()

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

        ' wizard
        MyBase.TraduzirControles()
        wMain.WizardSteps(0).Title = Traduzir("045_Paso_1")
        wMain.WizardSteps(1).Title = Traduzir("045_Paso_2")
        wMain.WizardSteps(2).Title = Traduzir("045_Paso_3")
        wMain.WizardSteps(3).Title = Traduzir("045_Paso_4")
        wMain.WizardSteps(4).Title = Traduzir("045_Paso_5")
        wMain.WizardSteps(5).Title = Traduzir("045_Paso_6")
        wMain.WizardSteps(6).Title = Traduzir("045_Confirmacion")

        wMain.StepNextButtonText = Traduzir("045_Siguiente")
        wMain.StartNextButtonText = Traduzir("045_Siguiente")
        wMain.StepPreviousButtonText = Traduzir("045_Anterior")
        wMain.FinishCompleteButtonText = Traduzir("045_Grabar")
        wMain.FinishPreviousButtonText = Traduzir("045_Anterior")
        wMain.CancelButtonText = Traduzir("045_Cancelar")

        ' itens passo 1

        lblCodigo.Text = Traduzir("045_Codigo")
        lblDescripcion.Text = Traduzir("045_Descripcion")
        lblColor.Text = Traduzir("045_Color_Cabecera")
        lblTipoDocumento.Text = Traduzir("045_Tipo_Documento")
        chkDocumentoIndividual.Text = Traduzir("045_Movimento_Individual")
        chkDocumentoGrupo.Text = Traduzir("045_Movimento_Agrupada")
        chkActivo.Text = Traduzir("045_Disponible_Para_Uso")
        lblIcono.Text = Traduzir("045_Ruta_Cargar_Icono")

        ' itens passo 2

        rbCaracteristicaPrincipalCierres.Text = Traduzir("045_Cierres")
        rbCaracteristicaPrincipalGestionBultos.Text = Traduzir("045_Gestion_Bultos")
        rbCaracteristicaPrincipalGestionFondos.Text = Traduzir("045_Gestion_Fondos")
        rbCaracteristicaPrincipalGestionRemesa.Text = Traduzir("045_Gestion_Remesas")
        rbCaracteristicaPrincipalGestionContenedores.Text = Traduzir("045_Gestion_Contenedores")
        rbCaracteristicaPrincipalOtrosMovimientos.Text = Traduzir("045_Otros_Movimentos")

        ' itens passo 3

        '' gestión de remesas
        rbGestionRemesasActas.Text = Traduzir("045_Actas")
        rbGestionRemesasAltas.Text = Traduzir("045_Altas")
        rbGestionRemesasBajas.Text = Traduzir("045_Bajas")
        rbGestionRemesasReenvios.Text = Traduzir("045_Reenvios")
        rbGestionRemesasSustitucion.Text = Traduzir("045_Sustitucion")
        lblGestionRemesasTipoBulto.Text = Traduzir("045_Tipo_Bulto_Tratar")
        chkGestionRemesasPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")
        chkGestionRemesasExcluirSectoresHijos.Text = Traduzir("045_Excluir_Sectores_Hijos")

        '' gestión de bultos
        rbGestionBultosActas.Text = Traduzir("045_Actas")
        rbGestionBultosAltas.Text = Traduzir("045_Altas")
        rbGestionBultosBajas.Text = Traduzir("045_Bajas")
        rbGestionBultosReenvios.Text = Traduzir("045_Reenvios")
        rbGestionBultosSustitucion.Text = Traduzir("045_Sustitucion")
        lblGestionBultosTipoBulto.Text = Traduzir("045_Tipo_Bulto_Tratar")
        chkGestionBultosPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")
        chkGestionBultosExcluirSectoresHijos.Text = Traduzir("045_Excluir_Sectores_Hijos")

        '' gestión contenedores
        rbGestionContenedoresAltas.Text = Traduzir("045_Altas")
        rbGestionContenedoresBajas.Text = Traduzir("045_Bajas")
        rbGestionContenedoresReenvios.Text = Traduzir("045_Reenvios")
        chkGestionContenedoresPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")
        chkGestionContenedoresPackModular.Text = Traduzir("045_Pack_Modular")
        chkGestionContenedoresExcluirSectoresHijos.Text = Traduzir("045_Excluir_Sectores_Hijos")

        '' gestión fondos
        rbGestionFondosMovimientoFondos.Text = Traduzir("045_Movimientos_De_Fondos")
        rbGestionFondosAjustes.Text = Traduzir("045_Ajustes")
        rbGestionFondosSustitucion.Text = Traduzir("045_Sustitucion")
        rbGestionFondosSolicitacion.Text = Traduzir("045_Solicitacion")
        rbGestionFondosClasificacion.Text = Traduzir("045_Clasificacion")
        chkGestionFondosPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")

        '' cierres
        rbCierresTesoro.Text = Traduzir("045_Cierre_De_Tesoro")
        rbCierresCaja.Text = Traduzir("045_Cierre_De_Caja")
        rbCierresCuadreFisico.Text = Traduzir("045_Cuadre_Fisico")
        chkCierreCajaPermiteLlegarSaldoNegativo.Text = Traduzir("045_Llegar_Saldo_Negativo")
        lblOpcionesCierreCaja.Text = Traduzir("045_Opciones_Cierre_Caja")
        chkGestionCierreExcluirSectoresHijos.Text = Traduzir("045_Excluir_Sectores_Hijos")

        ' itens passo 4

        '' gestión de remesas : bajas
        rbGestionRemesasBajasBajaElementos.Text = Traduzir("045_Baja_Remesa_Sistema")
        rbGestionRemesasBajasSalidasRecorrido.Text = Traduzir("045_Salida_Remesa_Ruta")
        lblGestionRemesasBajasFiltro.Text = Traduzir("045_Basado_Reporte")

        '' gestión de remesas : reenvios
        rbGestionRemesasReenviosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionRemesasReenviosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        lblGestionRemesasReenviosFiltro.Text = Traduzir("045_Basado_Reporte")
        chkGestionRemesasReenviosReenvioAutomatico.Text = Traduzir("045_Utilizar_Reenvio_Automatico")

        '' gestión de remesas : actas
        rbGestionRemesasActasActaRecuento.Text = Traduzir("045_Acta_Recuento")
        rbGestionRemesasActasActaClasificado.Text = Traduzir("045_Acta_Clasificado")
        rbGestionRemesasActasActaEmbolsado.Text = Traduzir("045_Acta_Embolsado")
        lblGestionRemesasActasFiltro.Text = Traduzir("045_Basado_Reporte")
        rbGestionRemesasActasActaDesembolsado.Text = Traduzir("045_Acta_Desembolsado")

        '' gestión de bultos : bajas
        rbGestionBultosBajasBajaElementos.Text = Traduzir("045_Baja_Bulto_Sistema")
        rbGestionBultosBajasSalidasRecorrido.Text = Traduzir("045_Salida_Bulto_Ruta")
        lblGestionBultosBajasFiltro.Text = Traduzir("045_Basado_Reporte")

        '' gestión de bultos : reenvios
        rbGestionBultosReenviosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionBultosReenviosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        lblGestionBultosReenviosFiltro.Text = Traduzir("045_Basado_Reporte")
        chkGestionBultosReenviosReenvioAutomatico.Text = Traduzir("045_Utilizar_Reenvio_Automatico")

        '' gestión de bultos : actas
        rbGestionBultosActasActaRecuento.Text = Traduzir("045_Acta_Recuento")
        rbGestionBultosActasActaClasificado.Text = Traduzir("045_Acta_Clasificado")
        rbGestionBultosActasActaEmbolsado.Text = Traduzir("045_Acta_Embolsado")
        lblGestionBultosActasFiltro.Text = Traduzir("045_Basado_Reporte")
        rbGestionBultosActasActaDesembolsado.Text = Traduzir("045_Acta_Desembolsado")


        '' gestión de fondos : movimiento de fondos
        rbGestionFondosMovimientoFondosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionFondosMovimientoFondosEntreCanales.Text = Traduzir("045_Entre_Canales")
        rbGestionFondosMovimientoFondosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        rbGestionFondosMovimientoFondosEntreClientes.Text = Traduzir("045_Entre_Clientes")

        '' gestión de cierre
        rbGestionCierreEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionCierreEntrePlantas.Text = Traduzir("045_Entre_Plantas")

        '' gestión de contenedores: bajas
        rbGestionContenedoresBajasDesarmar.Text = Traduzir("045_Desarmar_Contenedor")
        rbGestionContenedoresBajasSalida.Text = Traduzir("045_Salida_Contenedor")

        '' gestión de contenedores: reenvios
        rbGestionContenedoresReenviosEntreSectores.Text = Traduzir("045_Entre_Sectores")
        rbGestionContenedoresReenviosEntrePlantas.Text = Traduzir("045_Entre_Plantas")
        rbGestionContenedoresReenviosEntreClientes.Text = Traduzir("045_Entre_Clientes")

        ' itens paso 5

        ' itens paso 6

        lblAccionContable.Text = Traduzir("045_Accion_Contable")
        lblAccionContableEstadosPosibles.Text = Traduzir("045_Estados_Posibles_Documento")
        lblAccionContableAccionesPosibles.Text = Traduzir("045_Acciones_Contables_Estados")
        lblAccionContableDisponibleOrigen.Text = Traduzir("045_Disponible_Origen")
        lblAccionContableNoDisponibleOrigen.Text = Traduzir("045_No_Disponible_Origen")
        lblAccionContableDisponibleDestino.Text = Traduzir("045_Disponible_Destino")
        lblAccionContableNoDisponibleDestino.Text = Traduzir("045_No_Disponible_Destino")
        lblAccionContableDisponibleBloqueadoOrigen.Text = Traduzir("045_Disponible_Bloqueado_Origen")
        lblAccionContableDisponibleBloqueadoDestino.Text = Traduzir("045_Disponible_Bloqueado_Destino")
        lblAccionContableEstadoConfirmado.Text = Traduzir("045_Confirmado")
        lblAccionContableEstadoAceptado.Text = Traduzir("045_Aceptado")
        lblAccionContableEstadoRechazado.Text = Traduzir("045_Rechazado")
        chkImprime.Text = Traduzir("045_Se_Imprime")
        lblImprimeCopias.Text = Traduzir("045_Copias")
        lblIAC.Text = Traduzir("045_IAC_Documento")
        lblIACGrupo.Text = Traduzir("045_IAC_Documento_Grupo")
        chkCodigoExternoObligatorio.Text = Traduzir("045_Codigo_Externo_Obligatorio")
        chkIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        chkIntegracionRecepcionEnvio.Text = Traduzir("045_Permite_Integracion_Recepcion")
        chkIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        chkIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        chkModificarTermino.Text = Traduzir("045_Permite_Modificar_Termino")
        lblResumenIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        lblResumenIntegracionRecepcion.Text = Traduzir("045_Permite_Integracion_Recepcion")
        lblResumenIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        lblResumenIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        lblResumenModificarTermino.Text = Traduzir("045_Permite_Modificar_Termino")
        chkSolicitacionFondos.Text = Traduzir("045_Permite_Solicitacion_Fondos")
        chkContestarSolicitacionFondos.Text = Traduzir("045_Permite_Contestar_Solicitacion_Fondos")
        chkMovimientoAceptacionAutomatica.Text = Traduzir("045_Movimiento_Aceptacion_Automatica")

        ' paso 7

        lblResumenIdentificacion.Text = Traduzir("045_Identificacion")
        lblResumenCodigo.Text = Traduzir("045_Codigo") & ":"
        lblResumenDescripcion.Text = Traduzir("045_Descripcion") & ":"
        lblResumenColor.Text = Traduzir("045_Color")
        lblResumenTipoDocumento.Text = Traduzir("045_Tipo_Doc")
        lblResumenIndividual.Text = Traduzir("045_Permite_Generar_Individual")
        lblResumenGrupo.Text = Traduzir("045_Permite_Generar_Agrupado")
        lblResumenActivo.Text = Traduzir("045_Disponible_Para_Uso")
        lblCodigoExternoObligatorio.Text = Traduzir("045_Codigo_Externo_Obligatorio")

        lblResumenObjectivo.Text = Traduzir("045_Objectivo")
        lblResumenCaracteristicas.Text = Traduzir("045_Caracteristicas")
        lblResumenTiposBultos.Text = Traduzir("045_Tipo_De_Bultos")

        lblResumenVisibilidadUso.Text = Traduzir("045_Visibilidad_Uso")
        lblResumenTiposSectoresOrigen.Text = Traduzir("045_Documento_Podra_Creado_Sectores")
        lblResumenTiposSectoresDestino.Text = Traduzir("045_Documento_Podra_Enviado_Sectores")

        lblResumenOperacionesContablesOtrosDatos.Text = Traduzir("045_Operaciones_Contables_Otros_Datos")
        lblResumenAccionContable.Text = Traduzir("045_Accion_Contable") & ": "
        lblResumenAccionContableEstado.Text = Traduzir("045_Estado")
        lblResumenAccionContableDisponibleOrigen.Text = Traduzir("045_Disponible_Origen")
        lblResumenAccionContableNoDisponibleOrigen.Text = Traduzir("045_No_Disponible_Origen")
        lblResumenAccionContableDisponibleDestino.Text = Traduzir("045_Disponible_Destino")
        lblResumenAccionContableNoDisponibleDestino.Text = Traduzir("045_No_Disponible_Destino")
        lblResumenAccionContableDisponibleBloqueadoOrigen.Text = Traduzir("045_Disponible_Bloqueado_Origen")
        lblResumenAccionContableDisponibleBloqueadoDestino.Text = Traduzir("045_Disponible_Bloqueado_Destino")
        lblResumenAccionContableConfirmado.Text = Traduzir("045_Confirmado")
        lblResumenAccionContableAceptado.Text = Traduzir("045_Aceptado")
        lblResumenAccionContableRechazado.Text = Traduzir("045_Rechazado")
        lblResumenPermiteLlegarSaldoNegativo.Text = Traduzir("045_Permite_Llegar_Saldo_Negativo")
        lblResumenImprime.Text = Traduzir("045_Impreso_Al_Confirmmar")
        lblResumenImprimeCopias.Text = Traduzir("045_Copias_Impresion_Para")
        lblResumenIAC.Text = Traduzir("045_IAC_Documento") & ":"
        lblResumenIACGrupo.Text = Traduzir("045_IAC_Documento_Grupo") & ":"
        lblResumenIntegracionSalidas.Text = Traduzir("045_Permite_Enviar_Salidas")
        lblResumenIntegracionRecepcion.Text = Traduzir("045_Permite_Integracion_Recepcion")
        lblResumenIntegracionLegado.Text = Traduzir("045_Permite_Integracion_Legado")
        lblResumenIntegracionConteo.Text = Traduzir("045_Permite_Integracion_Conteo")
        lblResumenModificarTermino.Text = Traduzir("045_Permite_Modificar_Termino")
        lblResumenSolicitacionFondos.Text = Traduzir("045_Permite_Solicitacion_Fondos")
        lblResumenContestarSolicitacionFondos.Text = Traduzir("045_Permite_Contestar_Solicitacion_Fondos")
        lblResumenExcluirSectoresHijos.Text = Traduzir("045_Resumen_Excluir_Sectores_Hijos")
        lblResumenPackModular.Text = Traduzir("045_Resumen_Pack_Modular")

    End Sub

#End Region

    Protected Sub ConfiguraControleWizard()

        wMain.ActiveStepIndex = 0

    End Sub
    Protected Sub CargaCombosListas()

        CarregaDropDownTipoDocumentos()

        CarregaDropDownTiposBultos()

        CarregaFiltrosFormulario()

        CarregaListaTipoSectores()

        CarregaDropDownAccionesContabeis()

        CarregaDropDownIAC()

    End Sub

    Protected Sub EjecutarNavegacion(Optional paso As Integer = -1)

        If paso < 0 Then
            paso = wMain.ActiveStepIndex
        End If

        Select Case paso
            Case 0 ' Passo 1
                EjecutaPaso1()
            Case 1 ' Passo 2
                EjecutaPaso2()
            Case 2 ' Passo 3
                EjecutaPaso3()
            Case 3 ' Passo 4
                EjecutaPaso4()
            Case 4 ' Passo 5
                EjecutaPaso5()
            Case 5 ' Passo 6
                EjecutaPaso6()
            Case 6 ' Passo 7
                EjecutaPaso7()
        End Select

    End Sub
    Protected Sub EjecutaPaso1()

        lblPaso1.Text = Traduzir("045_Paso_1")
        wMain.WizardSteps(0).Title = wMain.HeaderText

    End Sub
    Protected Sub EjecutaPaso2()

        lblPaso2.Text = Traduzir("045_Paso_2")
        wMain.WizardSteps(1).Title = lblPaso2.Text

    End Sub
    Protected Sub EjecutaPaso3()

        pnlWs3Cierres.Visible = False
        pnlWs3GestionBultos.Visible = False
        pnlWs3GestionFondos.Visible = False
        pnlWs3GestionRemesas.Visible = False
        pnlWs3GestionContenedores.Visible = False

        Dim TituloComplementar As String = String.Empty

        If (rbCaracteristicaPrincipalCierres.Checked) Then
            TituloComplementar = Traduzir("045_Cuadres_Cierres")
            ' exibe o painel de cuadres y cierres
            pnlWs3Cierres.Visible = True
        ElseIf (rbCaracteristicaPrincipalGestionBultos.Checked) Then
            TituloComplementar = Traduzir("045_Gestion_Bultos_Realizar")
            ' exibe o painel de gestión de bultos
            pnlWs3GestionBultos.Visible = True
        ElseIf (rbCaracteristicaPrincipalGestionFondos.Checked) Then
            TituloComplementar = Traduzir("045_Gestion_Fondos_Realizar")
            ' exibe o painel de gestión de fondos
            pnlWs3GestionFondos.Visible = True
        ElseIf (rbCaracteristicaPrincipalGestionRemesa.Checked) Then
            TituloComplementar = Traduzir("045_Gestion_Remesas_Realizar")
            ' exibe o painel de gestión de remesas
            pnlWs3GestionRemesas.Visible = True
        ElseIf (rbCaracteristicaPrincipalGestionContenedores.Checked) Then
            TituloComplementar = Traduzir("045_Gestion_Contenedores_Realizar")
            ' exibe o painel de gestión de contenedores
            pnlWs3GestionContenedores.Visible = True
        ElseIf (rbCaracteristicaPrincipalOtrosMovimientos.Checked) Then
            ' caso seja a opção otros movimientos, deverá ser movido
            ' diretamente para o paso 5
            wMain.MoveTo(wMain.WizardSteps(4))
            Exit Sub
        End If

        lblPaso3.Text = String.Format(Traduzir("045_Paso_3") & "{0}", TituloComplementar)
        wMain.WizardSteps(2).Title = lblPaso3.Text

    End Sub
    Protected Sub EjecutaPaso4()

        pnlWs4GestionRemesasBajas.Visible = False
        pnlWs4GestionRemesasReenvios.Visible = False
        pnlWs4GestionRemesasActas.Visible = False

        pnlWs4GestionBultosBajas.Visible = False
        pnlWs4GestionBultosReenvios.Visible = False
        pnlWs4GestionBultosActas.Visible = False

        pnlWs4GestionFondosMovimientoFondos.Visible = False

        pnlWs4GestionContenedoresReenvios.Visible = False
        pnlWs4GestionContenedoresBajas.Visible = False

        Dim TituloComplementar As String = String.Empty

        If (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso Not rbGestionBultosAltas.Checked AndAlso Not rbGestionBultosSustitucion.Checked) Then
            If (rbGestionBultosBajas.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Bultos_Bajas_Detalles")
                ' exibe o painel de gestión de bultos bajas detalles
                pnlWs4GestionBultosBajas.Visible = True
            ElseIf (rbGestionBultosReenvios.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Bultos_Reenvio_Detalles")
                ' exibe o painel de gestión de bultos reenvíos detalles
                pnlWs4GestionBultosReenvios.Visible = True
            ElseIf (rbGestionBultosActas.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Bultos_Actas_Detalles")
                ' exibe o painel de gestión de bultos actas detalles
                pnlWs4GestionBultosActas.Visible = True
                'acta de desembolso apenas para documentos de grupo
                If chkDocumentoGrupo.Checked Then
                    pnlGestionBultosActasActaDesembolsado.Visible = True
                Else
                    pnlGestionBultosActasActaDesembolsado.Visible = False
                End If
            End If
        ElseIf (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) Then
            TituloComplementar = Traduzir("045_Gestion_Fondos_Movimiento_Detalles")
            ' exibe o painel de gestión de fondos movimiento de fondos detalles
            pnlWs4GestionFondosMovimientoFondos.Visible = True
        ElseIf (rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso Not rbGestionRemesasAltas.Checked AndAlso Not rbGestionRemesasSustitucion.Checked) Then
            If (rbGestionRemesasBajas.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Remesas_Bajas_Detalles")
                ' exibe o painel de gestión de remesas bajas detalles
                pnlWs4GestionRemesasBajas.Visible = True
            ElseIf (rbGestionRemesasReenvios.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Remesas_Reenvio_Detalles")
                ' exibe o painel de gestión de remesas reenvíos detalles
                pnlWs4GestionRemesasReenvios.Visible = True
            ElseIf (rbGestionRemesasActas.Checked) Then
                TituloComplementar = Traduzir("045_Gestion_Remesas_Actas_Detalles")
                ' exibe o painel de gestión de remesas actas detalles
                pnlWs4GestionRemesasActas.Visible = True
                'acta de desembolso apenas para documentos de grupo
                If chkDocumentoGrupo.Checked Then
                    pnlGestionRemesasActasActaDesembolsado.Visible = True
                Else
                    pnlGestionRemesasActasActaDesembolsado.Visible = False
                End If
            End If
        ElseIf rbCaracteristicaPrincipalCierres.Checked Then
            TituloComplementar = Traduzir("045_Gestion_Cierres_Detalles")
            pnlWs4GestionCierre.Visible = True
        ElseIf rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso rbGestionContenedoresBajas.Checked Then
            TituloComplementar = Traduzir("045_Gestion_Contenedores_Bajas")
            pnlWs4GestionContenedoresBajas.Visible = True
        ElseIf rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso rbGestionContenedoresReenvios.Checked Then
            TituloComplementar = Traduzir("045_Gestion_Contenedores_Reenvios")
            pnlWs4GestionContenedoresReenvios.Visible = True
        Else
            ' caso seja a opção otros movimientos, gestión de bultos + sustitución, gestión de remesas + sustitución, 
            ' gestión de fondos + sustitución, gestión de fondos + ajustes ou modificación de desglose o calidad, o cierres
            ' deverá ser movidodiretamente para o paso 5
            wMain.MoveTo(wMain.WizardSteps(4))
            Exit Sub
        End If

        lblPaso4.Text = String.Format(Traduzir("045_Paso_4") & "{0}", TituloComplementar)
        wMain.WizardSteps(3).Title = lblPaso4.Text

    End Sub
    Protected Sub EjecutaPaso5()

        pnlWs5TiposSectoresOrigen.Visible = True
        pnlWs5TiposSectoresDestino.Visible = False

        lblTiposSectoresOrigen.Text = Traduzir("045_Tipos_Sectores")
        lblTiposSectoresDestino.Text = Traduzir("045_Tipos_Sectores_Destino")

        Dim TituloComplementar As String = Traduzir("045_Tipos_Sectores_Movimento_Definido")

        If ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso (rbGestionBultosReenvios.Checked OrElse rbGestionBultosAltas.Checked)) OrElse _
            (rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso (rbGestionRemesasReenvios.Checked OrElse rbGestionRemesasAltas.Checked)) OrElse _
            (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked AndAlso Not rbGestionFondosMovimientoFondosEntreClientes.Checked) OrElse _
            (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSolicitacion.Checked) OrElse _
            (rbCaracteristicaPrincipalCierres.Checked AndAlso rbCierresCaja.Checked) OrElse _
            (rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso rbGestionContenedoresReenvios.Checked)) Then
            TituloComplementar = Traduzir("045_Tipos_Sectores_Ori_Dest_Movimento_Definido")
            lblTiposSectoresOrigen.Text = Traduzir("045_Tipos_Sectores_Origen")
            pnlWs5TiposSectoresDestino.Visible = True
        End If

        lblPaso5.Text = String.Format(Traduzir("045_Paso_5") & "{0}", TituloComplementar)
        wMain.WizardSteps(4).Title = lblPaso5.Text

    End Sub
    Protected Sub EjecutaPaso6()

        lblPaso6.Text = Traduzir("045_Paso6_Operaciones_Otros_Datos")
        wMain.WizardSteps(5).Title = lblPaso6.Text

        HabilitaAccionesContable()

        dvSolicitacionFondos.Visible = False
        dvContestarSolicitacionFondos.Visible = False
        chkIntegracionSalidas.Enabled = True
        chkSolicitacionFondos.Enabled = True

        'CheckBox Solicitação de Fundos
        'Gestión de Fondos – Movimiento de Fondos – Entre Sectores
        'Gestión de Fondos – Movimiento de Fondos – Entre Plantas
        If Not chkDocumentoGrupo.Checked AndAlso ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
            AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked)) Then
            dvSolicitacionFondos.Visible = True
            dvContestarSolicitacionFondos.Visible = True
            'Se for solicitação fundos ou contestação de fundos é obrigatório ter integração com o salidas
            If chkSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkContestarSolicitacionFondos.Checked = False
                chkContestarSolicitacionFondos.Enabled = False
            ElseIf chkContestarSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkSolicitacionFondos.Enabled = False
                chkSolicitacionFondos.Checked = False
            End If
        ElseIf Not chkDocumentoGrupo.Checked AndAlso ((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) _
            AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) Then
            dvContestarSolicitacionFondos.Visible = True
            'Se for solicitação fundos ou contestação de fundos é obrigatório ter integração com o salidas
            If chkSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkContestarSolicitacionFondos.Checked = False
                chkContestarSolicitacionFondos.Enabled = False
            ElseIf chkContestarSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkSolicitacionFondos.Enabled = False
                chkSolicitacionFondos.Checked = False
            End If
        ElseIf Not chkDocumentoGrupo.Checked AndAlso ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) _
            AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) Then
            dvContestarSolicitacionFondos.Visible = True
            'Se for solicitação fundos ou contestação de fundos é obrigatório ter integração com o salidas
            If chkSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkContestarSolicitacionFondos.Checked = False
                chkContestarSolicitacionFondos.Enabled = False
            ElseIf chkContestarSolicitacionFondos.Checked Then
                chkIntegracionSalidas.Checked = True
                chkIntegracionSalidas.Enabled = False
                chkSolicitacionFondos.Enabled = False
                chkSolicitacionFondos.Checked = False
            End If
        End If

        'Se for Substituição, Solicitação de fundos ou Contetar Fundos não tem terminos de grupo
        lblIACGrupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))

        ddlIACGrupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))



    End Sub
    Protected Sub EjecutaPaso7()

        lblPaso7.Text = Traduzir("045_Resumen_Vision")
        wMain.WizardSteps(6).Title = lblPaso7.Text

        ' dados de identificação
        ResumenIdentificacion()

        ' dados de objetivo
        ResumenCaracteristicas()
        ResumenTiposBultos()

        ' visibilidade e uso
        ResumenTiposSectores()

        ' operações contábeis e outros dados
        ResumenAccionesContables()
        ResumenOtrosDatos()

    End Sub
    Protected Function EjecutaPasoSiguiente(Optional paso As Integer = -1) As Boolean

        Page.Validate()

        If paso < 0 Then
            paso = wMain.ActiveStepIndex
        End If

        Select Case paso
            Case 0 ' Passo 1
                'Verifica se já existe um formulário com o código informado
                If Modo = Enumeradores.Modo.Alta Then
                    If LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPorCodigo(txtCodigo.Text) IsNot Nothing Then
                        MyBase.MostraMensagemErro(Traduzir("045_Codigo_Formulario_Existe"))
                        Return False
                    End If
                End If
            Case 1 ' Passo 2
                'EjecutaPaso2()
            Case 2 ' Passo 3
                'EjecutaPaso3()
            Case 3 ' Passo 4
                'EjecutaPaso4()
            Case 4 ' Passo 5

                If rbTipoAjustesSoloPositivos.Checked Then
                    CarregaDropDownAccionesContabeis(True)
                ElseIf rbTipoAjustesSoloNegativos.Checked Then
                    CarregaDropDownAccionesContabeis(False)
                Else
                    CarregaDropDownAccionesContabeis()
                End If

                CarregaDropDownAccionesContables(Not pnlWs5TiposSectoresDestino.Visible)

                If Not pnlWs5TiposSectoresDestino.Visible Then
                    Me.rbAccionContableSeleccionar.Attributes.Add("onclick", "DesabilitaAccionesContables('" & ddlAccionContableSeleccionar.ClientID & "','" & Me.txtAccionContableNuevo.ClientID & "','" & Me.dvEstadosAccionesContables.ClientID & "',1,1)")
                    Me.rbAccionContableNuevo.Attributes.Add("onclick", "DesabilitaAccionesContables('" & txtAccionContableNuevo.ClientID & "','" & Me.ddlAccionContableSeleccionar.ClientID & "','" & Me.dvEstadosAccionesContables.ClientID & "',0,1)")
                Else
                    Me.rbAccionContableSeleccionar.Attributes.Add("onclick", "DesabilitaAccionesContables('" & ddlAccionContableSeleccionar.ClientID & "','" & Me.txtAccionContableNuevo.ClientID & "','" & Me.dvEstadosAccionesContables.ClientID & "',1,0)")
                    Me.rbAccionContableNuevo.Attributes.Add("onclick", "DesabilitaAccionesContables('" & txtAccionContableNuevo.ClientID & "','" & Me.ddlAccionContableSeleccionar.ClientID & "','" & Me.dvEstadosAccionesContables.ClientID & "',0,0)")
                End If

                'EjecutaPaso5()

            Case 5 ' Passo 6

                'Se substituição ou solicitação de fundos não tem ação contábil
                If Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse
                         (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse
                     (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) OrElse
                     (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosClasificacion.Checked) OrElse
                     ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso
                      (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) AndAlso
                      chkSolicitacionFondos.Checked)) AndAlso String.IsNullOrEmpty(ddlAccionContableSeleccionar.SelectedValue) AndAlso txtAccionContableNuevo.Text.Trim = String.Empty Then
                    MyBase.MostraMensagemErro(Traduzir("045_Accion_Contable_Obligatorio"))
                    Return False
                End If

                'Recupera as características preenchida na tela para validar as características de integrações
                Dim formulario = RecuperaDatosPantalla()
                If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 Then

                    Try
                        LogicaNegocio.GenesisSaldos.MaestroFormularios.ValidarFormulariosDeIntegraciones(formulario)

                    Catch ex As Excepcion.NegocioExcepcion
                        MyBase.MostraMensagemErro(ex.Message)
                        Return False
                    Catch ex As Exception
                        Throw
                    End Try
                End If

                'EjecutaPaso6()
            Case 6 ' Passo 7
                'EjecutaPaso7()
        End Select

        Return True
    End Function

    Protected Sub Cancelar()

        ' TODO: colocar mensagem de confirmação antes de efetivar o redirect

        Response.Redireccionar("ConfiguracionFormularios.aspx")

    End Sub
    Protected Sub Grabar()

        Try
            Dim formulario As New Clases.Formulario
            Dim estadosAccionesContables As New List(Of Clases.EstadoAccionContable)
            Dim tiposSectoresOrigen As New List(Of Clases.TipoSector)
            Dim tiposSectoresDestino As New List(Of Clases.TipoSector)
            Dim tiposBultos As New List(Of Clases.TipoBulto)
            Dim copias As New List(Of Clases.Copia)

            'Preeche a identificação do formulario a ser salva - Paso 1
            PreencherIdentificacion(formulario)
            'Preeche a caracteristica princiapal do formulario a ser salva - Paso 2
            PreencheCaracteriscaPrincial(formulario)
            'Preeche as caracteristicas do formulario a ser salva - Paso 3
            'Preeche tipo bultos
            PreencheCaracteriscasPaso3(formulario, tiposBultos)
            'Preenche as caracteristicas do formulario a ser salva - Paso 4
            PreencheCaracteriscasPaso4(formulario)
            'Preenche as caracteristicas do formulario a ser salva - Paso 5
            PreencheCaracteriscasPaso5(formulario, tiposSectoresOrigen, tiposSectoresDestino)
            'Preenche as caracteristicas do formulario a ser salva - Paso 6
            PreencheCaracteriscasPaso6(formulario, estadosAccionesContables, copias)

            If Modo = Enumeradores.Modo.Modificacion AndAlso hidIdendificador.Value.Length > 0 Then
                LogicaNegocio.GenesisSaldos.MaestroFormularios.AtualizarFormulario(formulario, estadosAccionesContables, tiposSectoresOrigen, tiposSectoresDestino, tiposBultos, copias)
            Else
                LogicaNegocio.GenesisSaldos.MaestroFormularios.GuardarFormulario(formulario, estadosAccionesContables, tiposSectoresOrigen, tiposSectoresDestino, tiposBultos, copias)
            End If

            Response.Redireccionar("ConfiguracionFormularios.aspx")

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    ''' <summary>
    ''' Recupera as informações preenchidas em cada passo
    ''' Método copiado do Grabar para poder validar no passo seis
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RecuperaDatosPantalla() As Clases.Formulario

        Dim formulario As New Clases.Formulario
        Dim estadosAccionesContables As New List(Of Clases.EstadoAccionContable)
        Dim tiposSectoresOrigen As New List(Of Clases.TipoSector)
        Dim tiposSectoresDestino As New List(Of Clases.TipoSector)
        Dim tiposBultos As New List(Of Clases.TipoBulto)
        Dim copias As New List(Of Clases.Copia)

        'Preeche a identificação do formulario a ser salva - Paso 1
        PreencherIdentificacion(formulario)
        'Preeche a caracteristica princiapal do formulario a ser salva - Paso 2
        PreencheCaracteriscaPrincial(formulario)
        'Preeche as caracteristicas do formulario a ser salva - Paso 3
        'Preeche tipo bultos
        PreencheCaracteriscasPaso3(formulario, tiposBultos)
        'Preenche as caracteristicas do formulario a ser salva - Paso 4
        PreencheCaracteriscasPaso4(formulario)
        'Preenche as caracteristicas do formulario a ser salva - Paso 5
        PreencheCaracteriscasPaso5(formulario, tiposSectoresOrigen, tiposSectoresDestino)
        'Preenche as caracteristicas do formulario a ser salva - Paso 6
        PreencheCaracteriscasPaso6(formulario, estadosAccionesContables, copias)

        Return formulario

    End Function

    Protected Sub Cargar()

        hidIdendificador.Value = Request.QueryString("IdentificadorFormulario")

        If (hidIdendificador IsNot Nothing AndAlso hidIdendificador.Value.Trim().Length > 0) Then

            hidIdendificador.Value = Request.QueryString("IdentificadorFormulario").Trim()
            FormularioGrabado = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormulario(hidIdendificador.Value)
            If FormularioGrabado IsNot Nothing Then
                CargarIdentificacion(FormularioGrabado)
                CargarCaracteristicas(FormularioGrabado)
                CargarTiposBultos(FormularioGrabado)
            End If
        End If

    End Sub

    Protected Sub CargarIdentificacion(formulario As Comon.Clases.Formulario)

        txtCodigo.Text = formulario.Codigo
        txtDescripcion.Text = formulario.Descripcion
        If formulario.Color <> Nothing AndAlso Not formulario.Color.IsEmpty AndAlso Not String.IsNullOrEmpty(formulario.Color.Name) Then
            Dim c As String = "#" & formulario.Color.Name
            txtColor.Text = formulario.Color.Name
            txtColor.Attributes.Add("style", "background:#" & formulario.Color.Name & " 0 0 repeat-x !important; width:99%")
        End If
        If formulario.Icono IsNot Nothing AndAlso formulario.Icono.Length > 0 Then
            IconeParaSalvar = formulario.Icono
            Dim base64String = Convert.ToBase64String(formulario.Icono, 0, formulario.Icono.Length)
            imgPhoto.ImageUrl = "data:image/png;base64," + base64String
            imgPhoto.Visible = True
        End If

        ddlTipoDocumento.SelectedValue = formulario.TipoDocumento.Identificador
        chkDocumentoIndividual.Checked = VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual)
        chkDocumentoGrupo.Checked = VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo)
        chkActivo.Checked = formulario.EstaActivo

    End Sub
    Protected Sub CargarCaracteristicas(formulario As Clases.Formulario)

        'Passo 2
        'Caracteristica principal
        CargaCaracteristicaPrincipal(formulario)

        'Passo 3
        'Caracteristica radio buttons
        CargaCaracteristicaRadioButtonsPaso3(formulario)
        'Caracteristica Check box
        CargaCaracteristicaCheckBoxPaso3(formulario)

        'Passo 4
        'Característica radio buttons,drop down e checkbox
        CargaCaracteristicaPaso4(formulario)

        'Passo 5
        'Carrega os listbox
        CargaCaracteristicaListBoxPaso5(formulario.Identificador)

        'Passo 6
        CargaCaracteristicasPaso6(formulario)

    End Sub
    Protected Sub CargarTiposBultos(formulario As Object)

    End Sub

    Protected Sub ResumenIdentificacion()

        lblResumenCodigoValor.Text = txtCodigo.Text & "<br />"
        lblResumenDescripcionValor.Text = txtDescripcion.Text & "<br />"
        lblResumenColorValor.Text = txtColor.Text & "<br />"
        lblResumenColorValor.Attributes.Add("style", "background:#" & txtColor.Text & " 0 0 repeat-x !important;")
        lblResumenTipoDocumentoValor.Text = TextoValorSelecionado(ddlTipoDocumento) & "<br />"
        lblResumenIndividualValor.Text = TextoValorMarcado(chkDocumentoIndividual) & "<br />"
        lblResumenGrupoValor.Text = TextoValorMarcado(chkDocumentoGrupo) & "<br />"
        lblResumenActivoValor.Text = TextoValorMarcado(chkActivo) & "<br />"
        lblCodigoExternoObligatorioValor.Text = TextoValorMarcado(chkCodigoExternoObligatorio) & "<br />"

    End Sub
    Protected Sub ResumenCaracteristicas()

        Dim caracteristicas As String = String.Empty

        If (rbCaracteristicaPrincipalCierres.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalCierres.Text
            If (rbCierresCaja.Checked) Then
                caracteristicas &= ", " & rbCierresCaja.Text
            ElseIf (rbCierresTesoro.Checked) Then
                caracteristicas &= ", " & rbCierresTesoro.Text
            ElseIf (rbCierresCuadreFisico.Checked) Then
                caracteristicas &= ", " & rbCierresCuadreFisico.Text
            End If
            If (rbGestionCierreEntrePlantas.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasReenviosEntrePlantas.Text
            ElseIf (rbGestionCierreEntreSectores.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasReenviosEntreSectores.Text
            End If
        ElseIf (rbCaracteristicaPrincipalGestionBultos.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalGestionBultos.Text
            If (rbGestionBultosActas.Checked) Then
                caracteristicas &= ", " & rbGestionBultosActas.Text
                If (rbGestionBultosActasActaClasificado.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosActasActaClasificado.Text
                ElseIf (rbGestionBultosActasActaEmbolsado.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosActasActaEmbolsado.Text
                ElseIf (rbGestionBultosActasActaRecuento.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosActasActaRecuento.Text
                ElseIf (rbGestionBultosActasActaDesembolsado.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosActasActaDesembolsado.Text
                End If
            ElseIf (rbGestionBultosAltas.Checked) Then
                caracteristicas &= ", " & rbGestionBultosAltas.Text
            ElseIf (rbGestionBultosBajas.Checked) Then
                caracteristicas &= ", " & rbGestionBultosBajas.Text
                If (rbGestionBultosBajasBajaElementos.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosBajasBajaElementos.Text
                ElseIf (rbGestionBultosBajasSalidasRecorrido.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosBajasSalidasRecorrido.Text
                End If
            ElseIf (rbGestionBultosReenvios.Checked) Then
                caracteristicas &= ", " & rbGestionBultosReenvios.Text
                If (rbGestionBultosReenviosEntrePlantas.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosReenviosEntrePlantas.Text
                ElseIf (rbGestionBultosReenviosEntreSectores.Checked) Then
                    caracteristicas &= ", " & rbGestionBultosReenviosEntreSectores.Text
                End If
            End If
        ElseIf (rbCaracteristicaPrincipalGestionFondos.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalGestionFondos.Text
            If (rbGestionFondosAjustes.Checked) Then
                caracteristicas &= ", " & rbGestionFondosAjustes.Text
            ElseIf (rbGestionFondosMovimientoFondos.Checked) Then
                caracteristicas &= ", " & rbGestionFondosMovimientoFondos.Text
                If (rbGestionFondosMovimientoFondosEntreCanales.Checked) Then
                    caracteristicas &= ", " & rbGestionFondosMovimientoFondosEntreCanales.Text
                ElseIf (rbGestionFondosMovimientoFondosEntreClientes.Checked) Then
                    caracteristicas &= ", " & rbGestionFondosMovimientoFondosEntreClientes.Text
                ElseIf (rbGestionFondosMovimientoFondosEntrePlantas.Checked) Then
                    caracteristicas &= ", " & rbGestionFondosMovimientoFondosEntrePlantas.Text
                ElseIf (rbGestionFondosMovimientoFondosEntreSectores.Checked) Then
                    caracteristicas &= ", " & rbGestionFondosMovimientoFondosEntreSectores.Text
                End If
            ElseIf (rbGestionFondosSustitucion.Checked) Then
                caracteristicas &= ", " & rbGestionFondosSustitucion.Text
            ElseIf (rbGestionFondosSolicitacion.Checked) Then
                caracteristicas &= ", " & rbGestionFondosSolicitacion.Text
            ElseIf (rbGestionFondosClasificacion.Checked) Then
                caracteristicas &= ", " & rbGestionFondosClasificacion.Text
            End If
        ElseIf (rbCaracteristicaPrincipalGestionRemesa.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalGestionRemesa.Text
            If (rbGestionRemesasActas.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasActas.Text
                If (rbGestionRemesasActasActaClasificado.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasActasActaClasificado.Text
                ElseIf (rbGestionRemesasActasActaEmbolsado.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasActasActaEmbolsado.Text
                ElseIf (rbGestionRemesasActasActaRecuento.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasActasActaRecuento.Text
                ElseIf (rbGestionRemesasActasActaDesembolsado.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasActasActaDesembolsado.Text
                End If
            ElseIf (rbGestionRemesasAltas.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasAltas.Text
            ElseIf (rbGestionRemesasBajas.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasBajas.Text
                If (rbGestionRemesasBajasBajaElementos.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasBajasBajaElementos.Text
                ElseIf (rbGestionRemesasBajasSalidasRecorrido.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasBajasSalidasRecorrido.Text
                End If
            ElseIf (rbGestionRemesasReenvios.Checked) Then
                caracteristicas &= ", " & rbGestionRemesasReenvios.Text
                If (rbGestionRemesasReenviosEntrePlantas.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasReenviosEntrePlantas.Text
                ElseIf (rbGestionRemesasReenviosEntreSectores.Checked) Then
                    caracteristicas &= ", " & rbGestionRemesasReenviosEntreSectores.Text
                End If
            End If
        ElseIf (rbCaracteristicaPrincipalGestionContenedores.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalGestionContenedores.Text
            If (rbGestionContenedoresAltas.Checked) Then
                caracteristicas &= ", " & rbGestionContenedoresAltas.Text
            ElseIf (rbGestionContenedoresBajas.Checked) Then
                caracteristicas &= ", " & rbGestionContenedoresBajas.Text
            ElseIf (rbGestionContenedoresReenvios.Checked) Then
                caracteristicas &= ", " & rbGestionContenedoresReenvios.Text
            End If

            If rbGestionContenedoresReenvios.Checked Then
                If (rbGestionContenedoresReenviosEntreClientes.Checked) Then
                    caracteristicas &= ", " & rbGestionContenedoresReenviosEntreClientes.Text
                ElseIf (rbGestionContenedoresReenviosEntrePlantas.Checked) Then
                    caracteristicas &= ", " & rbGestionContenedoresReenviosEntrePlantas.Text
                ElseIf (rbGestionContenedoresReenviosEntreSectores.Checked) Then
                    caracteristicas &= ", " & rbGestionContenedoresReenviosEntreSectores.Text
                End If
            End If

            If rbGestionContenedoresBajas.Checked Then
                If (rbGestionContenedoresBajasDesarmar.Checked) Then
                    caracteristicas &= ", " & rbGestionContenedoresBajasDesarmar.Text
                ElseIf (rbGestionContenedoresBajasSalida.Checked) Then
                    caracteristicas &= ", " & rbGestionContenedoresBajasSalida.Text
                End If
            End If

        ElseIf (rbCaracteristicaPrincipalOtrosMovimientos.Checked) Then
            caracteristicas = rbCaracteristicaPrincipalOtrosMovimientos.Text
        End If

        lblResumenCaracteristicasValor.Text = caracteristicas & "<br />"

    End Sub
    Protected Sub ResumenTiposBultos()

        fsResumenTiposBultos.Visible = (rbCaracteristicaPrincipalGestionBultos.Checked OrElse rbCaracteristicaPrincipalGestionRemesa.Checked)

        If (rbCaracteristicaPrincipalGestionBultos.Checked) Then
            lblResumenTiposBultosValor.Text = TextoValoresSelecionados(chlGestionBultosTipoBulto) & "<br />"
        ElseIf (rbCaracteristicaPrincipalGestionRemesa.Checked) Then
            lblResumenTiposBultosValor.Text = TextoValoresSelecionados(chlGestionRemesasTipoBulto) & "<br />"
        End If

    End Sub
    Protected Sub ResumenTiposSectores()

        lstReumenTiposSectoresOrigen.Items.Clear()
        For Each t In lstTiposSectoresOrigenSeleccionados.Items
            lstReumenTiposSectoresOrigen.Items.Add(t)
        Next

        lstReumenTiposSectoresDestino.Items.Clear()
        For Each t In lstTiposSectoresDestinoSeleccionados.Items
            lstReumenTiposSectoresDestino.Items.Add(t)
        Next

        fsReumenTiposSectoresDestino.Visible = ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso (rbGestionBultosReenvios.Checked OrElse rbGestionBultosAltas.Checked)) OrElse _
                                                  (rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso (rbGestionRemesasReenvios.Checked OrElse rbGestionRemesasAltas.Checked)) OrElse _
                                                  (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked AndAlso Not rbGestionFondosMovimientoFondosEntreClientes.Checked) OrElse _
                                                  (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSolicitacion.Checked) OrElse _
                                                  (rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso rbGestionContenedoresReenvios.Checked))

    End Sub
    Protected Sub ResumenAccionesContables()

        pnlWs7AccionContable.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse
                                             (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse
                                             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) OrElse
                                             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosClasificacion.Checked) OrElse
                                             ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso
                                              (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) AndAlso
                                              chkSolicitacionFondos.Checked))

        If rbAccionContableSeleccionar.Checked Then
            lblResumenAccionContableValor.Text = ddlAccionContableSeleccionar.SelectedItem.Text
        Else
            lblResumenAccionContableValor.Text = txtAccionContableNuevo.Text
        End If

        lblResumenAccionContableConfirmadoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoDisponibleOrigen)
        lblResumenAccionContableConfirmadoNoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoNoDisponibleOrigen)
        lblResumenAccionContableConfirmadoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoDisponibleDestino)
        lblResumenAccionContableConfirmadoNoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoNoDisponibleDestino)
        lblResumenAccionContableConfirmadoDisponibleBloqueadoOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen)
        lblResumenAccionContableConfirmadoDisponibleBloqueadoDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino)

        lblResumenAccionContableAceptadoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoDisponibleOrigen)
        lblResumenAccionContableAceptadoNoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoNoDisponibleOrigen)
        lblResumenAccionContableAceptadoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoDisponibleDestino)
        lblResumenAccionContableAceptadoNoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoNoDisponibleDestino)
        lblResumenAccionContableAceptadoDisponibleBloqueadoOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen)
        lblResumenAccionContableAceptadoDisponibleBloqueadoDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino)

        lblResumenAccionContableRechazadoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoDisponibleOrigen)
        lblResumenAccionContableRechazadoNoDisponibleOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoNoDisponibleOrigen)
        lblResumenAccionContableRechazadoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoDisponibleDestino)
        lblResumenAccionContableRechazadoNoDisponibleDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoNoDisponibleDestino)
        lblResumenAccionContableRechazadoDisponibleBloqueadoOrigen.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen)
        lblResumenAccionContableRechazadoDisponibleBloqueadoDestino.Text = TextoValorSelecionado(ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino)

    End Sub
    Protected Sub ResumenOtrosDatos()

        'Formulários de Substituição, Solicitação de Fundos e Contestar Fundos não podem ser de grupo
        pnlWs7Grupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))

        pnlWs7IACGrupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))

        pnlWs7SolicitacionFondos.Visible = ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
            AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))

        pnlWs7ContestarSolicitacionFondos.Visible = ((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))

        dvResumenPermiteLlegarSaldoNegativo.Visible = (Not rbCaracteristicaPrincipalOtrosMovimientos.Checked AndAlso Not rbCaracteristicaPrincipalCierres.Checked)
        dvResumenExcluirSectoresHijos.Visible = (Not rbCaracteristicaPrincipalOtrosMovimientos.Checked AndAlso Not rbCaracteristicaPrincipalGestionFondos.Checked)
        dvResumenPackModular.Visible = rbCaracteristicaPrincipalGestionContenedores.Checked

        If (rbCaracteristicaPrincipalGestionBultos.Checked) Then
            lblResumenPermiteLlegarSaldoNegativoValor.Text = TextoValorMarcado(chkGestionBultosPermiteLlegarSaldoNegativo) & "<br />"
            lblResumenExcluirSectoresHijosValor.Text = TextoValorMarcado(chkGestionBultosExcluirSectoresHijos) & "<br />"
        ElseIf (rbCaracteristicaPrincipalGestionContenedores.Checked) Then
            lblResumenPermiteLlegarSaldoNegativoValor.Text = TextoValorMarcado(chkGestionContenedoresPermiteLlegarSaldoNegativo) & "<br />"
            lblResumenExcluirSectoresHijosValor.Text = TextoValorMarcado(chkGestionContenedoresExcluirSectoresHijos) & "<br />"
            lblResumenPackModularValor.Text = TextoValorMarcado(chkGestionContenedoresPackModular) & "<br />"
        ElseIf (rbCaracteristicaPrincipalGestionFondos.Checked) Then
            lblResumenPermiteLlegarSaldoNegativoValor.Text = TextoValorMarcado(chkGestionFondosPermiteLlegarSaldoNegativo) & "<br />"
        ElseIf (rbCaracteristicaPrincipalCierres.Checked) Then
            lblResumenExcluirSectoresHijosValor.Text = TextoValorMarcado(chkGestionCierreExcluirSectoresHijos) & "<br />"
        ElseIf (rbCaracteristicaPrincipalGestionRemesa.Checked) Then
            lblResumenPermiteLlegarSaldoNegativoValor.Text = TextoValorMarcado(chkGestionRemesasPermiteLlegarSaldoNegativo) & "<br />"
            lblResumenExcluirSectoresHijosValor.Text = TextoValorMarcado(chkGestionRemesasExcluirSectoresHijos) & "<br />"
        End If

        dvResumenImprimeCopias.Visible = (chkImprime.Checked)

        lblResumenImprimeValor.Text = TextoValorMarcado(chkImprime) & "<br />"
        lblResumenImprimeCopiasValor.Text = TextoValoresSelecionados(lstImprimeDestinos) & "<br />"

        lblResumenIACValor.Text = IIf(ddlIAC.SelectedValue = String.Empty, "------------------------", ddlIAC.SelectedItem.Text) & "<br />"

        lblResumenIACGrupoValor.Text = IIf(ddlIACGrupo.SelectedValue = String.Empty, "------------------------", ddlIACGrupo.SelectedItem.Text) & "<br />"

        lblResumenIntegracionSalidasValor.Text = TextoValorMarcado(chkIntegracionSalidas) & "<br />"

        lblResumenIntegracionRecepcionValor.Text = TextoValorMarcado(chkIntegracionRecepcionEnvio) & "<br />"

        lblResumenIntegracionLegadoValor.Text = TextoValorMarcado(chkIntegracionLegado) & "<br />"

        lblResumenIntegracionConteoValor.Text = TextoValorMarcado(chkIntegracionConteo) & "<br />"

        lblResumenModificarTerminoValor.Text = TextoValorMarcado(chkModificarTermino) & "<br />"

        lblResumenSolicitacionFondosValor.Text = TextoValorMarcado(chkSolicitacionFondos) & "<br />"

        lblResumenContestarSolicitacionFondosValor.Text = TextoValorMarcado(chkContestarSolicitacionFondos) & "<br />"

    End Sub

    Protected Function TextoValorSelecionado(ByRef lista As DropDownList)

        Return If(lista IsNot Nothing AndAlso lista.SelectedItem IsNot Nothing, lista.SelectedItem.Text, "")

    End Function
    Protected Function TextoValoresSelecionados(ByRef lista As ListBox)

        If lista IsNot Nothing AndAlso lista.Items.Count > 0 Then
            Return Join((From tb As ListItem In lista.Items Select tb.Text).ToArray(), ", ")
        Else
            Return "------------------------"
        End If

    End Function
    Protected Function TextoValoresSelecionados(ByRef lista As CheckBoxList)

        If lista IsNot Nothing AndAlso (From tb As ListItem In lista.Items Where tb.Selected).Count > 0 Then
            Return Join((From tb As ListItem In lista.Items Where tb.Selected Select tb.Text).ToArray(), ",")
        Else
            Return "------------------------"
        End If

    End Function
    Protected Function TextoValorMarcado(ByRef check As CheckBox, Optional usarCaracterXComoSi As Boolean = False)

        If (usarCaracterXComoSi) Then
            Return If(check.Checked, "X", "")
        Else
            Return If(check.Checked, Traduzir("045_Si"), Traduzir("045_No"))
        End If

    End Function

#Region "EVENTOS"

    Private Sub ddlAccionContableSeleccionar_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAccionContableSeleccionar.SelectedIndexChanged
        Try
            CargaAccionContable(ddlAccionContableSeleccionar.SelectedValue)
            HabilitaAccionesContable()
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub chkSolicitacionFondos_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkSolicitacionFondos.CheckedChanged
        Try
            If ddlAccionContableSeleccionar.Items IsNot Nothing AndAlso ddlAccionContableSeleccionar.Items.Count > 0 Then
                ddlAccionContableSeleccionar.SelectedIndex = 0
            End If
            CargaAccionContable(ddlAccionContableSeleccionar.SelectedValue)
            HabilitaAccionesContable()
            'Se for Substituição, Solicitação de fundos ou Contestar Fundos não tem terminos de grupo
            lblIACGrupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
                                        OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))

            ddlIACGrupo.Visible = Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
                                         OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked))

            If (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
            AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) Then
                'Se for solicitação fundos é obrigatório ter integração com o salidas
                If chkSolicitacionFondos.Checked Then
                    chkIntegracionSalidas.Checked = True
                    chkIntegracionSalidas.Enabled = False
                    chkContestarSolicitacionFondos.Checked = False
                    chkContestarSolicitacionFondos.Enabled = False
                Else
                    chkIntegracionSalidas.Checked = False
                    chkIntegracionSalidas.Enabled = True
                    chkContestarSolicitacionFondos.Checked = False
                    chkContestarSolicitacionFondos.Enabled = True
                End If
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub chkContestarSolicitacionFondos_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkContestarSolicitacionFondos.CheckedChanged
        Try

            If (rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) _
                AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked) Then
                'Se for contestação de fundos é obrigatório ter integração com o salidas
                If chkContestarSolicitacionFondos.Checked Then
                    chkIntegracionSalidas.Checked = True
                    chkIntegracionSalidas.Enabled = False
                    chkSolicitacionFondos.Checked = False
                    chkSolicitacionFondos.Enabled = False
                    chkSolicitacionFondos.Checked = False
                    'não possui terminos de IAC quando é formulário de Contestar Fundos
                    lblIACGrupo.Visible = False
                    ddlIACGrupo.Visible = False
                Else
                    chkIntegracionSalidas.Checked = False
                    chkIntegracionSalidas.Enabled = True
                    chkSolicitacionFondos.Checked = False
                    chkSolicitacionFondos.Enabled = True
                    lblIACGrupo.Visible = True
                    ddlIACGrupo.Visible = True
                End If
            ElseIf (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) _
                    AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked) Then
                'Se for contestação de fundos é obrigatório ter integração com o salidas
                If chkContestarSolicitacionFondos.Checked Then
                    chkIntegracionSalidas.Checked = True
                    chkIntegracionSalidas.Enabled = False
                    chkSolicitacionFondos.Checked = False
                    chkSolicitacionFondos.Enabled = False
                    'não possui terminos de IAC quando é formulário de Contestar Fundos
                    lblIACGrupo.Visible = False
                    ddlIACGrupo.Visible = False
                Else
                    chkIntegracionSalidas.Checked = False
                    chkIntegracionSalidas.Enabled = True
                    chkSolicitacionFondos.Checked = False
                    chkSolicitacionFondos.Enabled = True
                    lblIACGrupo.Visible = True
                    ddlIACGrupo.Visible = True
                End If

            ElseIf (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                    AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) Then
                'Se for contestação de fundos é obrigatório ter integração com o salidas
                If chkContestarSolicitacionFondos.Checked Then
                    chkIntegracionSalidas.Checked = True
                    chkIntegracionSalidas.Enabled = False
                    chkSolicitacionFondos.Enabled = False
                    chkSolicitacionFondos.Checked = False
                    'não possui terminos de IAC quando é formulário de Contestar Fundos
                    lblIACGrupo.Visible = False
                    ddlIACGrupo.Visible = False
                Else
                    chkIntegracionSalidas.Checked = False
                    chkIntegracionSalidas.Enabled = True
                    chkSolicitacionFondos.Enabled = True
                    chkSolicitacionFondos.Checked = False
                    lblIACGrupo.Visible = True
                    ddlIACGrupo.Visible = True
                End If
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As System.EventArgs) Handles btnUpload.Click
        Try
            If fupImagem.HasFile Then

                Dim imbByte(fupImagem.PostedFile.InputStream.Length) As Byte
                fupImagem.PostedFile.InputStream.Read(imbByte, 0, imbByte.Length)

                If imbByte.Length > 153600 Then
                    MyBase.MostraMensagemErro(Traduzir("045_Tamano_Maximo_Icono"))
                    Exit Sub
                End If

                IconeParaSalvar = imbByte

                Dim fs As System.IO.Stream = fupImagem.PostedFile.InputStream
                Dim br As New System.IO.BinaryReader(fs)
                Dim base64String = Convert.ToBase64String(imbByte, 0, imbByte.Length)
                imgPhoto.ImageUrl = "data:image/png;base64," + base64String
                imgPhoto.Visible = True
            Else
                imgPhoto.ImageUrl = Nothing
                imgPhoto.Visible = False
                IconeParaSalvar = Nothing
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub imgBtnImprimeDestinoQuitar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnImprimeDestinoQuitar.Click
        Try
            If lstImprimeDestinos.Items.Count > 0 AndAlso lstImprimeDestinos.SelectedValue <> String.Empty Then
                lstImprimeDestinos.Items.Remove(lstImprimeDestinos.SelectedItem)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub imgBtnImprimeDestinoAnadir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnImprimeDestinoAnadir.Click
        Try

            If txtImprimeDestinoCopia.Text.Trim().Length() > 0 Then
                lstImprimeDestinos.Items.Add(txtImprimeDestinoCopia.Text.Trim())
                txtImprimeDestinoCopia.Text = String.Empty
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub chkImprime_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkImprime.CheckedChanged
        Try
            If chkImprime.Checked Then
                txtImprimeDestinoCopia.Enabled = True
                imgBtnImprimeDestinoAnadir.Enabled = True
                lstImprimeDestinos.Enabled = True
                imgBtnImprimeDestinoQuitar.Enabled = True
            Else
                txtImprimeDestinoCopia.Enabled = False
                imgBtnImprimeDestinoAnadir.Enabled = False
                lstImprimeDestinos.Enabled = False
                imgBtnImprimeDestinoQuitar.Enabled = False
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub imgBtnTiposSectoresDestinoQuitaTodos_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresDestinoQuitaTodos.Click

        Try
            If lstTiposSectoresDestinoSeleccionados.Items.Count > 0 Then
                Dim objListItem As ListItem
                While lstTiposSectoresDestinoSeleccionados.Items.Count > 0
                    objListItem = lstTiposSectoresDestinoSeleccionados.Items(0)
                    lstTiposSectoresDestinoSeleccionados.Items.Remove(objListItem)
                    lstTiposSectoresDestinoReferencia.Items.Add(objListItem)
                End While
                lstTiposSectoresDestinoReferencia.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Private Sub imgBtnTiposSectoresDestinoQuitaUno_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresDestinoQuitaUno.Click
        Try
            If lstTiposSectoresDestinoSeleccionados.Items.Count > 0 Then
                While lstTiposSectoresDestinoSeleccionados.SelectedItem IsNot Nothing AndAlso lstTiposSectoresDestinoSeleccionados.SelectedItem.Value <> String.Empty
                    Dim objListItem As ListItem = lstTiposSectoresDestinoSeleccionados.SelectedItem
                    lstTiposSectoresDestinoSeleccionados.Items.Remove(objListItem)
                    lstTiposSectoresDestinoReferencia.Items.Add(objListItem)
                End While
                lstTiposSectoresDestinoReferencia.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Private Sub imgBtnTiposSectoresDestinoSeleccionaTodos_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresDestinoSeleccionaTodos.Click
        Try
            If lstTiposSectoresDestinoReferencia.Items.Count > 0 Then
                Dim objListItem As ListItem
                While lstTiposSectoresDestinoReferencia.Items.Count > 0
                    objListItem = lstTiposSectoresDestinoReferencia.Items(0)
                    lstTiposSectoresDestinoReferencia.Items.Remove(objListItem)
                    lstTiposSectoresDestinoSeleccionados.Items.Add(objListItem)
                End While
                lstTiposSectoresDestinoSeleccionados.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Private Sub imgBtnTiposSectoresDestinoSeleccionaUno_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresDestinoSeleccionaUno.Click
        Try
            If lstTiposSectoresDestinoReferencia.Items.Count > 0 Then
                While lstTiposSectoresDestinoReferencia.SelectedItem IsNot Nothing AndAlso lstTiposSectoresDestinoReferencia.SelectedItem.Value <> String.Empty
                    Dim objListItem As ListItem = lstTiposSectoresDestinoReferencia.SelectedItem
                    lstTiposSectoresDestinoReferencia.Items.Remove(objListItem)
                    lstTiposSectoresDestinoSeleccionados.Items.Add(objListItem)
                End While
                lstTiposSectoresDestinoSeleccionados.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub OrdenarListBoxTipoSector(listBox As ListBox)
        Dim list As New List(Of Clases.TipoSector)
        For i = 0 To listBox.Items.Count - 1
            Dim tipoSector = New Clases.TipoSector
            tipoSector.Descripcion = listBox.Items(i).Text
            tipoSector.Identificador = listBox.Items(i).Value
            list.Add(tipoSector)
        Next
        listBox.DataSource = list.OrderBy(Function(s) s.Descripcion)
        listBox.DataTextField = "Descripcion"
        listBox.DataValueField = "Identificador"
        listBox.DataBind()
    End Sub

    Private Sub imgBtnTiposSectoresOrigenQuitaTodos_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresOrigenQuitaTodos.Click
        Try
            If lstTiposSectoresOrigenSeleccionados.Items.Count > 0 Then
                Dim objListItem As ListItem
                While lstTiposSectoresOrigenSeleccionados.Items.Count > 0
                    objListItem = lstTiposSectoresOrigenSeleccionados.Items(0)
                    lstTiposSectoresOrigenSeleccionados.Items.Remove(objListItem)
                    lstTiposSectoresOrigenReferencia.Items.Add(objListItem)
                End While
                OrdenarListBoxTipoSector(lstTiposSectoresOrigenReferencia)
                lstTiposSectoresOrigenReferencia.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub imgBtnTiposSectoresOrigenSeleccionaTodos_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresOrigenSeleccionaTodos.Click
        Try
            If lstTiposSectoresOrigenReferencia.Items.Count > 0 Then
                Dim objListItem As ListItem
                While lstTiposSectoresOrigenReferencia.Items.Count > 0
                    objListItem = lstTiposSectoresOrigenReferencia.Items(0)
                    lstTiposSectoresOrigenReferencia.Items.Remove(objListItem)
                    lstTiposSectoresOrigenSeleccionados.Items.Add(objListItem)
                End While
                OrdenarListBoxTipoSector(lstTiposSectoresOrigenSeleccionados)
                lstTiposSectoresOrigenSeleccionados.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub imgBtnTiposSectoresOrigenSeleccionaUno_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresOrigenSeleccionaUno.Click
        Try
            If lstTiposSectoresOrigenReferencia.Items.Count > 0 Then
                While lstTiposSectoresOrigenReferencia.SelectedItem IsNot Nothing AndAlso lstTiposSectoresOrigenReferencia.SelectedItem.Value <> String.Empty
                    Dim objListItem As ListItem = lstTiposSectoresOrigenReferencia.SelectedItem
                    lstTiposSectoresOrigenReferencia.Items.Remove(objListItem)
                    lstTiposSectoresOrigenSeleccionados.Items.Add(objListItem)
                End While
                OrdenarListBoxTipoSector(lstTiposSectoresOrigenSeleccionados)
                lstTiposSectoresOrigenSeleccionados.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub imgBtnTiposSectoresOrigenQuitaUno_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTiposSectoresOrigenQuitaUno.Click
        Try
            If lstTiposSectoresOrigenSeleccionados.Items.Count > 0 Then
                While lstTiposSectoresOrigenSeleccionados.SelectedItem IsNot Nothing AndAlso lstTiposSectoresOrigenSeleccionados.SelectedItem.Value <> String.Empty
                    Dim objListItem As ListItem = lstTiposSectoresOrigenSeleccionados.SelectedItem
                    lstTiposSectoresOrigenSeleccionados.Items.Remove(objListItem)
                    lstTiposSectoresOrigenReferencia.Items.Add(objListItem)
                End While
                OrdenarListBoxTipoSector(lstTiposSectoresOrigenReferencia)
                lstTiposSectoresOrigenReferencia.SelectedValue = Nothing
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Protected Sub rbGestionContenedores_CheckedChanged(sender As Object, e As System.EventArgs)

        If rbGestionContenedoresAltas.Checked Then
            dvGestionContenedoresPackModular.Style("display") = "block"
        Else
            dvGestionContenedoresPackModular.Style("display") = "none"
            chkGestionContenedoresPackModular.Checked = False
        End If

        If rbGestionContenedoresBajas.Checked OrElse rbGestionContenedoresReenvios.Checked Then
            dvGestionContenedoresExcluirSectoresHijos.Style("display") = "block"
        Else
            dvGestionContenedoresExcluirSectoresHijos.Style("display") = "none"
            chkGestionContenedoresExcluirSectoresHijos.Checked = False
        End If

    End Sub

#End Region

#Region "EVENTOS WIZARD"

    Protected Sub wMain_ActiveStepChanged(sender As Object, e As System.EventArgs) Handles wMain.ActiveStepChanged

        ' executa as validações e configurações para a mudança de um passo
        EjecutarNavegacion()

    End Sub
    Protected Sub wMain_CancelButtonClick(sender As Object, e As System.EventArgs) Handles wMain.CancelButtonClick

        ' executa as ações para cancelar a inclusão ou edição da página
        Cancelar()

    End Sub
    Protected Sub wMain_FinishButtonClick(sender As Object, e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles wMain.FinishButtonClick

        ' executa a gravação das alterações feitas nos dados do Wizard
        Grabar()

    End Sub
    Protected Sub wMain_Init(sender As Object, e As System.EventArgs) Handles wMain.Init

        ' configura o controle ao inicializar
        ConfiguraControleWizard()

    End Sub
    Protected Sub wMain_NextButtonClick(sender As Object, e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles wMain.NextButtonClick

        Try

            ' faz a mudança para o passo seguinte
            Dim avancar = EjecutaPasoSiguiente()
            If Not avancar Then
                e.Cancel = True
            End If

            HabilitaAccionesContable()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub
    Protected Sub wMain_PreviousButtonClick(sender As Object, e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles wMain.PreviousButtonClick

        ' faz a mudança para o passo anterior
        'EjecutaPasoAnterior()

    End Sub

#End Region

#Region "METODOS"

    Private Sub CargaAccionContable(identificadorAccionContable As String)
        'Estado e Accions Contabeis
        If Not String.IsNullOrEmpty(identificadorAccionContable) Then
            ddlAccionContableSeleccionar.SelectedValue = identificadorAccionContable
            Dim acciones = LogicaNegocio.GenesisSaldos.EstadoAccionContable.ObtenerEstadosAccionContable(identificadorAccionContable)
            If acciones IsNot Nothing AndAlso acciones.Count > 0 Then
                For Each accion In acciones
                    If accion.Codigo.Equals(Enumeradores.EstadoDocumento.Confirmado.RecuperarValor()) Then

                        ddlAccionContableEstadoConfirmadoDisponibleOrigen.SelectedValue = accion.OrigemDisponible
                        ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.SelectedValue = accion.OrigemNoDisponible
                        ddlAccionContableEstadoConfirmadoDisponibleDestino.SelectedValue = accion.DestinoDisponible
                        ddlAccionContableEstadoConfirmadoNoDisponibleDestino.SelectedValue = accion.DestinoNoDisponible
                        ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.SelectedValue = accion.OrigenDisponibleBloqueado
                        ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.SelectedValue = accion.DestinoDisponibleBloqueado

                    ElseIf accion.Codigo.Equals(Enumeradores.EstadoDocumento.Aceptado.RecuperarValor()) Then

                        ddlAccionContableEstadoAceptadoDisponibleOrigen.SelectedValue = accion.OrigemDisponible
                        ddlAccionContableEstadoAceptadoNoDisponibleOrigen.SelectedValue = accion.OrigemNoDisponible
                        ddlAccionContableEstadoAceptadoDisponibleDestino.SelectedValue = accion.DestinoDisponible
                        ddlAccionContableEstadoAceptadoNoDisponibleDestino.SelectedValue = accion.DestinoNoDisponible
                        ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.SelectedValue = accion.OrigenDisponibleBloqueado
                        ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.SelectedValue = accion.DestinoDisponibleBloqueado

                    ElseIf accion.Codigo.Equals(Enumeradores.EstadoDocumento.Rechazado.RecuperarValor()) Then

                        ddlAccionContableEstadoRechazadoDisponibleOrigen.SelectedValue = accion.OrigemDisponible
                        ddlAccionContableEstadoRechazadoNoDisponibleOrigen.SelectedValue = accion.OrigemNoDisponible
                        ddlAccionContableEstadoRechazadoDisponibleDestino.SelectedValue = accion.DestinoDisponible
                        ddlAccionContableEstadoRechazadoNoDisponibleDestino.SelectedValue = accion.DestinoNoDisponible
                        ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.SelectedValue = accion.OrigenDisponibleBloqueado
                        ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.SelectedValue = accion.DestinoDisponibleBloqueado

                    End If
                Next
            Else
                PonerEstadosAccionesContablesVacio()
            End If
        End If
    End Sub

    Private Sub HabilitaAccionesContable()

        If (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse
             (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse
             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) OrElse
             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosClasificacion.Checked) OrElse
             ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso
              (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) AndAlso
              chkSolicitacionFondos.Checked)) Then

            pnlWs6AccionContable.Visible = False

            ddlAccionContableSeleccionar.Enabled = False
            txtAccionContableNuevo.Enabled = False
            txtAccionContableNuevo.Text = String.Empty

            PonerEstadosAccionesContablesVacio()

            ddlAccionContableEstadoConfirmadoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoConfirmadoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoConfirmadoNoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.Enabled = False
            ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.Enabled = False

            ddlAccionContableEstadoAceptadoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoAceptadoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoAceptadoNoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoAceptadoNoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.Enabled = False
            ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.Enabled = False

            ddlAccionContableEstadoRechazadoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoRechazadoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoRechazadoNoDisponibleOrigen.Enabled = False
            ddlAccionContableEstadoRechazadoNoDisponibleDestino.Enabled = False
            ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.Enabled = False
            ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.Enabled = False

        Else

            pnlWs6AccionContable.Visible = True

            If rbAccionContableSeleccionar.Checked Then

                ddlAccionContableSeleccionar.Enabled = True
                txtAccionContableNuevo.Enabled = False
                txtAccionContableNuevo.Text = String.Empty

                ddlAccionContableEstadoConfirmadoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoConfirmadoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoConfirmadoNoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.Enabled = False
                ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.Enabled = False

                ddlAccionContableEstadoAceptadoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoAceptadoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoAceptadoNoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoAceptadoNoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.Enabled = False
                ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.Enabled = False

                ddlAccionContableEstadoRechazadoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoRechazadoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoRechazadoNoDisponibleOrigen.Enabled = False
                ddlAccionContableEstadoRechazadoNoDisponibleDestino.Enabled = False
                ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.Enabled = False
                ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.Enabled = False

            Else

                ddlAccionContableSeleccionar.Enabled = False
                ddlAccionContableSeleccionar.SelectedValue = String.Empty
                txtAccionContableNuevo.Enabled = True

                ddlAccionContableEstadoConfirmadoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoConfirmadoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoConfirmadoNoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.Enabled = True
                ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.Enabled = True

                ddlAccionContableEstadoAceptadoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoAceptadoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoAceptadoNoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoAceptadoNoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.Enabled = True
                ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.Enabled = True

                ddlAccionContableEstadoRechazadoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoRechazadoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoRechazadoNoDisponibleOrigen.Enabled = True
                ddlAccionContableEstadoRechazadoNoDisponibleDestino.Enabled = True
                ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.Enabled = True
                ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.Enabled = True

            End If

        End If

    End Sub

    Public Sub PonerEstadosAccionesContablesVacio()

        If ddlAccionContableEstadoConfirmadoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoConfirmadoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoConfirmadoNoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoNoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoNoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.Items.Count > 0 Then ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.SelectedIndex = 0

        If ddlAccionContableEstadoAceptadoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoAceptadoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoAceptadoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoAceptadoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoAceptadoNoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoNoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoAceptadoNoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoAceptadoNoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoNoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoAceptadoNoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.Items.Count > 0 Then ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.Items.Count > 0 Then ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.SelectedIndex = 0

        If ddlAccionContableEstadoRechazadoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoRechazadoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoRechazadoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoRechazadoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoRechazadoNoDisponibleOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoNoDisponibleOrigen.Items.Count > 0 Then ddlAccionContableEstadoRechazadoNoDisponibleOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoRechazadoNoDisponibleDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoNoDisponibleDestino.Items.Count > 0 Then ddlAccionContableEstadoRechazadoNoDisponibleDestino.SelectedIndex = 0
        If ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.Items.Count > 0 Then ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.SelectedIndex = 0
        If ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.Items IsNot Nothing AndAlso ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.Items.Count > 0 Then ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.SelectedIndex = 0

    End Sub

    Public Sub AdicionaConfirmacaoCancelar()
        'Seta mensagem de confirmação ao cancelar
        Dim cancelButtonStart As Button
        Dim cancelButtonSteps As Button
        Dim cancelButtonFinish As Button

        cancelButtonStart = TryCast(wMain.FindControl("StartNavigationTemplateContainerID").FindControl("CancelButton"), Button)
        cancelButtonSteps = TryCast(wMain.FindControl("StepNavigationTemplateContainerID").FindControl("CancelButton"), Button)
        cancelButtonFinish = TryCast(wMain.FindControl("FinishNavigationTemplateContainerID").FindControl("CancelButton"), Button)

        If cancelButtonStart IsNot Nothing Then
            cancelButtonStart.OnClientClick = "return confirm('" & Traduzir("045_Confirm_Cancelar") & "')"
        End If
        If cancelButtonSteps IsNot Nothing Then
            cancelButtonSteps.OnClientClick = "return confirm('" & Traduzir("045_Confirm_Cancelar") & "')"
        End If
        If cancelButtonFinish IsNot Nothing Then
            cancelButtonFinish.OnClientClick = "return confirm('" & Traduzir("045_Confirm_Cancelar") & "')"
        End If
    End Sub

    Public Sub CarregaDropDownTipoDocumentos()
        Try
            Dim selectedValue As String = ddlTipoDocumento.SelectedValue
            ddlTipoDocumento.Items.Clear()
            ddlTipoDocumento.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))

            For Each item In LogicaNegocio.GenesisSaldos.MaestroDocumentos.RecuperaTiposDocumentos()
                If (item.EsExhibidoCertificado) Then
                    ddlTipoDocumento.Items.Add(New ListItem(item.Descripcion & String.Format(" ({0})", Traduzir("045_Tipo_Documento_Certificable")), item.Identificador))
                Else
                    ddlTipoDocumento.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                End If
            Next

            If ddlTipoDocumento.Items Is Nothing OrElse ddlTipoDocumento.Items.Count = 0 Then
                ddlTipoDocumento.Enabled = False
            ElseIf ddlTipoDocumento.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlTipoDocumento.SelectedValue = selectedValue
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub CarregaDropDownTiposBultos()
        Try
            chlGestionRemesasTipoBulto.Items.Clear()
            chlGestionBultosTipoBulto.Items.Clear()
            Dim tiposBultos = LogicaNegocio.Genesis.TipoBulto.ObtenerTiposBultos

            If tiposBultos IsNot Nothing AndAlso tiposBultos.Count > 0 Then
                chlGestionRemesasTipoBulto.DataTextField = "Descripcion"
                chlGestionRemesasTipoBulto.DataValueField = "Identificador"
                chlGestionRemesasTipoBulto.DataSource = tiposBultos
                chlGestionRemesasTipoBulto.DataBind()

                chlGestionBultosTipoBulto.DataTextField = "Descripcion"
                chlGestionBultosTipoBulto.DataValueField = "Identificador"
                chlGestionBultosTipoBulto.DataSource = tiposBultos
                chlGestionBultosTipoBulto.DataBind()

            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub CarregaFiltrosFormulario()
        Try
            ddlGestionRemesasBajasFiltro.Items.Clear()
            ddlGestionBultosBajasFiltro.Items.Clear()
            ddlGestionRemesasReenviosFiltro.Items.Clear()
            ddlGestionBultosReenviosFiltro.Items.Clear()
            ddlGestionRemesasActasFiltro.Items.Clear()
            ddlGestionBultosActasFiltro.Items.Clear()

            ddlGestionRemesasBajasFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlGestionBultosBajasFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlGestionRemesasReenviosFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlGestionBultosReenviosFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlGestionRemesasActasFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlGestionBultosActasFiltro.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))


            For Each item In LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFiltrosFormulario
                ddlGestionRemesasBajasFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlGestionBultosBajasFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlGestionRemesasReenviosFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlGestionBultosReenviosFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlGestionRemesasActasFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlGestionBultosActasFiltro.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

            If ddlGestionRemesasBajasFiltro.Items Is Nothing OrElse ddlGestionRemesasBajasFiltro.Items.Count = 0 Then
                ddlGestionRemesasBajasFiltro.Enabled = False
            End If
            If ddlGestionBultosBajasFiltro.Items Is Nothing OrElse ddlGestionBultosBajasFiltro.Items.Count = 0 Then
                ddlGestionBultosBajasFiltro.Enabled = False
            End If
            If ddlGestionRemesasReenviosFiltro.Items Is Nothing OrElse ddlGestionRemesasReenviosFiltro.Items.Count = 0 Then
                ddlGestionRemesasReenviosFiltro.Enabled = False
            End If
            If ddlGestionBultosReenviosFiltro.Items Is Nothing OrElse ddlGestionBultosReenviosFiltro.Items.Count = 0 Then
                ddlGestionBultosReenviosFiltro.Enabled = False
            End If
            If ddlGestionRemesasActasFiltro.Items Is Nothing OrElse ddlGestionRemesasActasFiltro.Items.Count = 0 Then
                ddlGestionRemesasActasFiltro.Enabled = False
            End If
            If ddlGestionBultosActasFiltro.Items Is Nothing OrElse ddlGestionBultosActasFiltro.Items.Count = 0 Then
                ddlGestionBultosActasFiltro.Enabled = False
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Public Sub CarregaListaTipoSectores()
        Try
            lstTiposSectoresOrigenReferencia.Items.Clear()
            lstTiposSectoresDestinoReferencia.Items.Clear()
            Dim tiposSectores = LogicaNegocio.Genesis.TipoSector.ObtenerTiposSectores

            If tiposSectores IsNot Nothing AndAlso tiposSectores.Count > 0 Then
                lstTiposSectoresOrigenReferencia.DataTextField = "Descripcion"
                lstTiposSectoresOrigenReferencia.DataValueField = "Identificador"
                lstTiposSectoresOrigenReferencia.DataSource = tiposSectores.OrderBy(Function(s) s.Descripcion)
                lstTiposSectoresOrigenReferencia.DataBind()

                lstTiposSectoresDestinoReferencia.DataTextField = "Descripcion"
                lstTiposSectoresDestinoReferencia.DataValueField = "Identificador"
                lstTiposSectoresDestinoReferencia.DataSource = tiposSectores.OrderBy(Function(s) s.Descripcion)
                lstTiposSectoresDestinoReferencia.DataBind()

            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub CarregaDropDownAccionesContabeis(Optional soloPositivo As Boolean? = Nothing)
        Try

            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoNoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoNoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino, soloPositivo)

            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoNoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoNoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino, soloPositivo)

            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoNoDisponibleOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoNoDisponibleDestino, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen, soloPositivo)
            CarregaDropDownAccionesContabeis(ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino, soloPositivo)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Public Sub CarregaDropDownIAC()
        Try
            ddlIAC.Items.Clear()
            ddlIAC.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            ddlIACGrupo.Items.Clear()
            ddlIACGrupo.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))
            For Each item In LogicaNegocio.Genesis.InformacaoAdicionalCliente.ObtenerInformacionesAdicionais
                ddlIAC.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                ddlIACGrupo.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Public Sub CarregaDropDownAccionesContables(Optional somenteAcoesComConfiguracoesParaEstadoAceptado As Boolean = False)
        Try

            ddlAccionContableSeleccionar.Items.Clear()

            ddlAccionContableSeleccionar.Items.Add(New ListItem(Traduzir("gen_selecione"), String.Empty))

            Dim acoes As List(Of Comon.Clases.AccionContable)

            If somenteAcoesComConfiguracoesParaEstadoAceptado Then
                acoes = LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionesContablesSoloConEstadoAceptado
            Else
                acoes = LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionesContables
            End If

            If rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso
                rbGestionContenedoresReenvios.Checked AndAlso
                rbGestionContenedoresReenviosEntreClientes.Checked AndAlso (Me.chkDocumentoIndividual.Checked OrElse chkDocumentoGrupo.Checked) Then

                Dim codigosAciones As New List(Of String)
                codigosAciones.Add("TRANS_ENTRE_CLIENTES_GENERAL")
                codigosAciones.Add("TRANS_ENTRE_CLIENTES_PROSEGUR")
                For Each item In acoes.Where(Function(a) codigosAciones.Contains(a.Codigo))

                    Dim _Egreso = item.Acciones.FindAll(Function(x) x.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso)
                    Dim _Ingreso = item.Acciones.FindAll(Function(x) x.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso)

                    If rbTipoAjustesSoloPositivos.Checked AndAlso (_Egreso Is Nothing OrElse _Egreso.Count = 0) Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    ElseIf rbTipoAjustesSoloNegativos.Checked AndAlso (_Ingreso Is Nothing OrElse _Ingreso.Count = 0) Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    ElseIf rbTipoAjustesCualquier.Checked Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    End If
                Next

                rbAccionContableNuevo.Enabled = False
            Else
                rbAccionContableNuevo.Enabled = True

                For Each item In acoes

                    Dim _Egreso = item.Acciones.FindAll(Function(x) x.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso)
                    Dim _Ingreso = item.Acciones.FindAll(Function(x) x.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso)

                    If rbTipoAjustesSoloPositivos.Checked AndAlso (_Egreso Is Nothing OrElse _Egreso.Count = 0) Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    ElseIf rbTipoAjustesSoloNegativos.Checked AndAlso (_Ingreso Is Nothing OrElse _Ingreso.Count = 0) Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    ElseIf rbTipoAjustesCualquier.Checked Then
                        ddlAccionContableSeleccionar.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                    End If

                Next
            End If

            If Not String.IsNullOrEmpty(hidAccionContable.Value) AndAlso ddlAccionContableSeleccionar.Items.FindByValue(hidAccionContable.Value) IsNot Nothing Then
                ddlAccionContableSeleccionar.SelectedValue = hidAccionContable.Value
                CargaAccionContable(hidAccionContable.Value)
            Else
                PonerEstadosAccionesContablesVacio()
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub CarregaDropDownAccionesContabeis(ByRef ddl As System.Web.UI.WebControls.DropDownList, Optional soloPositivo As Boolean? = Nothing)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(String.Empty, "0"))
        If soloPositivo IsNot Nothing AndAlso soloPositivo Then
            ddl.Items.Add(New ListItem(Traduzir("045_Accion_Contable_Suma"), "+"))
        ElseIf soloPositivo IsNot Nothing AndAlso Not soloPositivo Then
            ddl.Items.Add(New ListItem(Traduzir("045_Accion_Contable_Resta"), "-"))
        Else
            ddl.Items.Add(New ListItem(Traduzir("045_Accion_Contable_Suma"), "+"))
            ddl.Items.Add(New ListItem(Traduzir("045_Accion_Contable_Resta"), "-"))
        End If
    End Sub

    Protected Function VerificaCaracteristicaExiste(formulario As Clases.Formulario, caracteristica As Enumeradores.CaracteristicaFormulario) As Boolean
        If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing Then
            Return formulario.Caracteristicas.Where(Function(c) c.Equals(caracteristica)).Count > 0
        End If
    End Function

    Protected Sub CargaCaracteristicaPrincipal(formulario As Clases.Formulario)
        If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 Then

            If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                rbCaracteristicaPrincipalGestionRemesa.Checked = True
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                rbCaracteristicaPrincipalGestionBultos.Checked = True
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                rbCaracteristicaPrincipalGestionFondos.Checked = True
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then
                rbCaracteristicaPrincipalGestionContenedores.Checked = True
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Cierres) Then
                rbCaracteristicaPrincipalCierres.Checked = True
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.OtrosMovimientos) Then
                rbCaracteristicaPrincipalOtrosMovimientos.Checked = True
            End If

        End If
    End Sub
    ''' <summary>
    ''' Carrega os radios buttons do passo 3
    ''' rbGestionRemesasAltas, rbGestionRemesasReenvio, rbGestioBultosActas...
    ''' <paramref></paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CargaCaracteristicaRadioButtonsPaso3(formulario As Clases.Formulario)

        If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 Then

            If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then

                'Gestión Remesas
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Altas) Then
                    rbGestionRemesasAltas.Checked = True
                    rbGestionRemesasBajas.Checked = False
                    rbGestionRemesasReenvios.Checked = False
                    rbGestionRemesasActas.Checked = False
                    rbGestionRemesasSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then
                    rbGestionRemesasAltas.Checked = False
                    rbGestionRemesasBajas.Checked = True
                    rbGestionRemesasReenvios.Checked = False
                    rbGestionRemesasActas.Checked = False
                    rbGestionRemesasSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then
                    rbGestionRemesasAltas.Checked = False
                    rbGestionRemesasBajas.Checked = False
                    rbGestionRemesasReenvios.Checked = True
                    rbGestionRemesasActas.Checked = False
                    rbGestionRemesasSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Actas) Then
                    rbGestionRemesasAltas.Checked = False
                    rbGestionRemesasBajas.Checked = False
                    rbGestionRemesasReenvios.Checked = False
                    rbGestionRemesasActas.Checked = True
                    rbGestionRemesasSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    rbGestionRemesasAltas.Checked = False
                    rbGestionRemesasBajas.Checked = False
                    rbGestionRemesasReenvios.Checked = False
                    rbGestionRemesasActas.Checked = False
                    rbGestionRemesasSustitucion.Checked = True
                End If
                'CargaCaracteristicaGestionsCheckBox
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then

                'Gestión Bultos
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Altas) Then
                    rbGestionBultosAltas.Checked = True
                    rbGestionBultosBajas.Checked = False
                    rbGestionBultosReenvios.Checked = False
                    rbGestionBultosActas.Checked = False
                    rbGestionBultosSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then
                    rbGestionBultosAltas.Checked = False
                    rbGestionBultosBajas.Checked = True
                    rbGestionBultosReenvios.Checked = False
                    rbGestionBultosActas.Checked = False
                    rbGestionBultosSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then
                    rbGestionBultosAltas.Checked = False
                    rbGestionBultosBajas.Checked = False
                    rbGestionBultosReenvios.Checked = True
                    rbGestionBultosActas.Checked = False
                    rbGestionBultosSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Actas) Then
                    rbGestionBultosAltas.Checked = False
                    rbGestionBultosBajas.Checked = False
                    rbGestionBultosReenvios.Checked = False
                    rbGestionBultosActas.Checked = True
                    rbGestionBultosSustitucion.Checked = False
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    rbGestionBultosAltas.Checked = False
                    rbGestionBultosBajas.Checked = False
                    rbGestionBultosReenvios.Checked = False
                    rbGestionBultosActas.Checked = False
                    rbGestionBultosSustitucion.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then

                'Gestión de Fundos
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.MovimientodeFondos) Then
                    rbGestionFondosMovimientoFondos.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Ajustes) Then
                    rbGestionFondosAjustes.Checked = True
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesNegativos) Then
                        rbTipoAjustesSoloNegativos.Checked = True
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesPositivos) Then
                        rbTipoAjustesSoloPositivos.Checked = True
                    Else
                        rbTipoAjustesCualquier.Checked = True
                    End If
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    rbGestionFondosSustitucion.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) Then
                    rbGestionFondosSolicitacion.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Classificacion) Then
                    rbGestionFondosClasificacion.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then

                rbGestionContenedoresAltas.Checked = False
                rbGestionContenedoresBajas.Checked = False
                rbGestionContenedoresReenvios.Checked = False

                'Gestión de Contenedores
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Altas) Then
                    rbGestionContenedoresAltas.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then
                    rbGestionContenedoresBajas.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then
                    rbGestionContenedoresReenvios.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Cierres) Then

                'Gestión de Cierres
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.CierreDeTesoro) Then
                    rbCierresTesoro.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.CierreDeCaja) Then
                    rbCierresCaja.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.CuadreFisico) Then
                    rbCierresCuadreFisico.Checked = True
                End If

            End If

        End If

    End Sub
    ''' <summary>
    ''' Carrega os checkbox do passo 3
    ''' chkGestionRemesasLectorPrecintos, chkGestionRemesasPermiteLlegarSaldoNegativo....
    ''' <paramref></paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CargaCaracteristicaCheckBoxPaso3(formulario As Clases.Formulario)

        If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 Then
            Dim tiposBultos = LogicaNegocio.Genesis.TipoBulto.ObtenerTiposBultos(formulario.Identificador)

            If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then

                For Each tb In tiposBultos
                    If chlGestionRemesasTipoBulto.Items.FindByValue(tb.Identificador) IsNot Nothing Then
                        chlGestionRemesasTipoBulto.Items.FindByValue(tb.Identificador).Selected = True
                    End If
                Next

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo) Then
                    chkGestionRemesasPermiteLlegarSaldoNegativo.Checked = True
                End If

                If rbGestionRemesasAltas.Checked Then
                    dvGestionRemesasExcluirSectoresHijos.Style("display") = "none"
                    chkGestionRemesasExcluirSectoresHijos.Checked = False
                Else
                    dvGestionRemesasExcluirSectoresHijos.Style("display") = "block"
                End If

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos) Then
                    chkGestionRemesasExcluirSectoresHijos.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then

                For Each tb In tiposBultos
                    If chlGestionRemesasTipoBulto.Items.FindByValue(tb.Identificador) IsNot Nothing Then
                        chlGestionBultosTipoBulto.Items.FindByValue(tb.Identificador).Selected = True
                    End If
                Next

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo) Then
                    chkGestionBultosPermiteLlegarSaldoNegativo.Checked = True
                End If

                If rbGestionBultosAltas.Checked Then
                    dvGestionBultosExcluirSectoresHijos.Style("display") = "none"
                    chkGestionBultosExcluirSectoresHijos.Checked = False
                Else
                    dvGestionBultosExcluirSectoresHijos.Style("display") = "block"
                End If

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos) Then
                    chkGestionBultosExcluirSectoresHijos.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo) Then
                    chkGestionContenedoresPermiteLlegarSaldoNegativo.Checked = True
                End If

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PackModular) Then
                    chkGestionContenedoresPackModular.Checked = True
                End If

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos) Then
                    chkGestionContenedoresExcluirSectoresHijos.Checked = True
                End If

                If rbGestionContenedoresAltas.Checked Then
                    dvGestionContenedoresPackModular.Style("display") = "block"
                Else
                    dvGestionContenedoresPackModular.Style("display") = "none"
                    chkGestionContenedoresPackModular.Checked = False
                End If

                If rbGestionContenedoresBajas.Checked OrElse rbGestionContenedoresReenvios.Checked Then
                    dvGestionContenedoresExcluirSectoresHijos.Style("display") = "block"
                Else
                    dvGestionContenedoresExcluirSectoresHijos.Style("display") = "none"
                    chkGestionContenedoresExcluirSectoresHijos.Checked = False
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo) Then
                    chkGestionFondosPermiteLlegarSaldoNegativo.Checked = True
                End If

            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.CierreDeCaja) Then

                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo) Then
                    chkCierreCajaPermiteLlegarSaldoNegativo.Checked = True
                End If

                If rbCierresCuadreFisico.Checked Then
                    dvGestionCierreExcluirSectoresHijos.Style("display") = "none"
                    chkGestionCierreExcluirSectoresHijos.Checked = False
                Else
                    dvGestionCierreExcluirSectoresHijos.Style("display") = "block"
                End If
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos) Then
                    chkGestionCierreExcluirSectoresHijos.Checked = True
                End If
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.AlConfirmarElDocumentoSeImprime) Then
                    chkImprime.Checked = True
                    chkImprime_CheckedChanged(Me, Nothing)
                End If

            End If

        End If

    End Sub
    ''' <summary>
    ''' Carrega os radios buttons e o drop down do passo 4
    ''' rbGestionRemesasBajasBajaElementos, rbGestionRemesasBajasSalidasRecorrido, rbGestionRemesasReenviosEntreSectores...
    ''' <paramref></paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CargaCaracteristicaPaso4(formulario As Clases.Formulario)

        If formulario IsNot Nothing AndAlso formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 Then

            'Gestion de Remessa
            If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then

                'Bajas
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then

                    'BajaElementos
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.BajaElementos) Then
                        rbGestionRemesasBajasBajaElementos.Checked = True

                        'SalidasRecorrido
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SalidasRecorrido) Then
                        rbGestionRemesasBajasSalidasRecorrido.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionRemesasBajasFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionRemesasBajasFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If

                    'Reenvios
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then

                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then
                        rbGestionRemesasReenviosEntreSectores.Checked = True

                        'Reenvio entre Plantas
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then
                        rbGestionRemesasReenviosEntrePlantas.Checked = True
                    End If

                    'Checkbox reenvio automatico
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ReenvioAutomatico) Then
                        chkGestionRemesasReenviosReenvioAutomatico.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionRemesasReenviosFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionRemesasReenviosFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If

                    'Actas
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Actas) Then

                    'Acta de Recuento
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeRecuento) Then
                        rbGestionRemesasActasActaRecuento.Checked = True

                        'Acta de classificado
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeClasificado) Then
                        rbGestionRemesasActasActaClasificado.Checked = True

                        'Acta de Embolsado
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeEmbolsado) Then
                        rbGestionRemesasActasActaEmbolsado.Checked = True
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado) Then
                        rbGestionRemesasActasActaDesembolsado.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionRemesasActasFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionRemesasActasFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If

                End If

                'Gestion de Bultos
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then

                'Bajas
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then

                    'BajaElementos
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.BajaElementos) Then
                        rbGestionBultosBajasBajaElementos.Checked = True

                        'SalidasRecorrido
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SalidasRecorrido) Then
                        rbGestionBultosBajasSalidasRecorrido.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionBultosBajasFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionBultosBajasFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If

                    'Reenvios
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then

                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then
                        rbGestionBultosReenviosEntreSectores.Checked = True

                        'Reenvio entre Plantas
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then
                        rbGestionBultosReenviosEntrePlantas.Checked = True
                    End If

                    'Checkbox reenvio automatico
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ReenvioAutomatico) Then
                        chkGestionBultosReenviosReenvioAutomatico.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionBultosReenviosFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionBultosReenviosFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If


                    'Actas
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Actas) Then

                    'Acta de Recuento
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeRecuento) Then
                        rbGestionBultosActasActaRecuento.Checked = True

                        'Acta de classificado
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeClasificado) Then
                        rbGestionBultosActasActaClasificado.Checked = True

                        'Acta de Embolsado
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeEmbolsado) Then
                        rbGestionBultosActasActaEmbolsado.Checked = True

                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado) Then
                        rbGestionBultosActasActaDesembolsado.Checked = True
                    End If

                    'Drop Down Filtro
                    If formulario.FiltroFormulario IsNot Nothing AndAlso ddlGestionBultosActasFiltro.Items.FindByValue(formulario.FiltroFormulario.Identificador) IsNot Nothing Then
                        ddlGestionBultosActasFiltro.SelectedValue = formulario.FiltroFormulario.Identificador
                    End If

                End If

                'Gestion de Fondos
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then

                'Movimentos de Fundos
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.MovimientodeFondos) Then

                    'Movimento entre Sectores
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then
                        rbGestionFondosMovimientoFondosEntreSectores.Checked = True
                        rbGestionFondosMovimientoFondosEntrePlantas.Checked = False
                        rbGestionFondosMovimientoFondosEntreCanales.Checked = False
                        rbGestionFondosMovimientoFondosEntreClientes.Checked = False

                        'Movimento entre Plantas
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then
                        rbGestionFondosMovimientoFondosEntreSectores.Checked = False
                        rbGestionFondosMovimientoFondosEntrePlantas.Checked = True
                        rbGestionFondosMovimientoFondosEntreCanales.Checked = False
                        rbGestionFondosMovimientoFondosEntreClientes.Checked = False

                        'Movimento entre Canales
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreCanales) Then
                        rbGestionFondosMovimientoFondosEntreSectores.Checked = False
                        rbGestionFondosMovimientoFondosEntrePlantas.Checked = False
                        rbGestionFondosMovimientoFondosEntreCanales.Checked = True
                        rbGestionFondosMovimientoFondosEntreClientes.Checked = False

                        'Movimento entre Clientes
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreClientes) Then
                        rbGestionFondosMovimientoFondosEntreSectores.Checked = False
                        rbGestionFondosMovimientoFondosEntrePlantas.Checked = False
                        rbGestionFondosMovimientoFondosEntreCanales.Checked = False
                        rbGestionFondosMovimientoFondosEntreClientes.Checked = True
                    End If

                End If
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then
                'Bajas
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Bajas) Then

                    'Desarmar el Contenedor
                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.DesarmarContenedor) Then
                        rbGestionContenedoresBajasDesarmar.Checked = True

                        'SalidasRecorrido
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SalidasRecorrido) Then
                        rbGestionContenedoresBajasSalida.Checked = True
                    End If

                    'Reenvios
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Reenvios) Then

                    If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then
                        rbGestionContenedoresReenviosEntreSectores.Checked = True
                        'Reenvio entre Plantas
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then
                        rbGestionContenedoresReenviosEntrePlantas.Checked = True
                    ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreClientes) Then
                        rbGestionContenedoresReenviosEntreClientes.Checked = True
                    End If
                End If

                'Actas
            ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.Cierres) Then
                If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then
                    rbGestionCierreEntreSectores.Checked = True
                ElseIf VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then
                    rbGestionCierreEntrePlantas.Checked = True
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Carrega os listbox do passo 5
    ''' lstTiposSectoresOrigenReferencia e lstTiposSectoresOrigenSeleccionados
    ''' <paramref></paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CargaCaracteristicaListBoxPaso5(identificadorFormulario As String)

        Dim tiposSectoresFormularioOrigen = LogicaNegocio.Genesis.TipoSector.ObtenerTiposSectores(identificadorFormulario, "O").OrderBy(Function(s) s.Descripcion)

        'Origen
        If tiposSectoresFormularioOrigen IsNot Nothing AndAlso tiposSectoresFormularioOrigen.Count > 0 Then

            For Each ts In tiposSectoresFormularioOrigen

                lstTiposSectoresOrigenSeleccionados.Items.Add(New ListItem(ts.Descripcion, ts.Identificador))

                Dim itemRemove = lstTiposSectoresOrigenReferencia.Items.FindByValue(ts.Identificador)
                If itemRemove IsNot Nothing Then
                    lstTiposSectoresOrigenReferencia.Items.Remove(itemRemove)
                End If
            Next

        End If

        'Destino
        Dim tiposSectoresFormularioDestino = LogicaNegocio.Genesis.TipoSector.ObtenerTiposSectores(identificadorFormulario, "D")

        If tiposSectoresFormularioDestino IsNot Nothing AndAlso tiposSectoresFormularioDestino.Count > 0 Then

            For Each ts In tiposSectoresFormularioDestino

                lstTiposSectoresDestinoSeleccionados.Items.Add(New ListItem(ts.Descripcion, ts.Identificador))

                Dim itemRemove = lstTiposSectoresDestinoReferencia.Items.FindByValue(ts.Identificador)
                If itemRemove IsNot Nothing Then
                    lstTiposSectoresDestinoReferencia.Items.Remove(itemRemove)
                End If
            Next

        End If

    End Sub
    ''' <summary>
    ''' Carrega os dados do passo 6
    ''' dropdowns, checkbox...
    ''' <paramref></paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CargaCaracteristicasPaso6(formulario As Clases.Formulario)

        If formulario.AccionContable IsNot Nothing Then
            hidAccionContable.Value = formulario.AccionContable.Identificador
            CargaAccionContable(formulario.AccionContable.Identificador)
        End If

        Dim copias = LogicaNegocio.GenesisSaldos.Copia.ObtenerCopiasFormulario(formulario.Identificador)

        If copias IsNot Nothing AndAlso copias.Count > 0 Then

            For Each c In copias
                lstImprimeDestinos.Items.Add(New ListItem(c.Destino, c.Identificador))
            Next

        End If

        'IAC
        If formulario.GrupoTerminosIACIndividual IsNot Nothing Then
            Dim iac = LogicaNegocio.Genesis.InformacaoAdicionalCliente.ObtenerInformacionAdicional(formulario.GrupoTerminosIACIndividual.Identificador)
            If iac IsNot Nothing Then
                ddlIAC.SelectedValue = iac.Identificador
            End If
        End If

        'IAC Grupo
        If formulario.GrupoTerminosIACGrupo IsNot Nothing Then
            Dim iac = LogicaNegocio.Genesis.InformacaoAdicionalCliente.ObtenerInformacionAdicional(formulario.GrupoTerminosIACGrupo.Identificador)
            If iac IsNot Nothing Then
                ddlIACGrupo.SelectedValue = iac.Identificador
            End If
        End If

        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.CodigoExternoObligatorio) Then
            chkCodigoExternoObligatorio.Checked = True
        End If

        'Se o formulário é utilizado para integração com o Salidas
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionSalidas) Then
            dvIntegracionSalidas.Visible = True
            chkIntegracionSalidas.Checked = True
        End If

        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.MovimientodeAceptacionAutomatica) Then
            dvMovimientoAceptacionAutomatica.Visible = True
            chkMovimientoAceptacionAutomatica.Checked = True
        End If

        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ModificarTerminos) Then
            dvModificarTermino.Visible = True
            chkModificarTermino.Checked = True
        End If

        'Se o formulário é utilizado para integração com o Recepção e Envio
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio) Then
            chkIntegracionRecepcionEnvio.Visible = True
            chkIntegracionRecepcionEnvio.Checked = True
        End If

        'Se o formulário é utilizado para integração o o Legado
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionLegado) Then
            dvIntegracionLegado.Visible = True
            chkIntegracionLegado.Checked = True
        End If

        'Se o formulário é utilizado para integração com o Conteo
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.IntegracionConteo) Then
            dvIntegracionConteo.Visible = True
            chkIntegracionConteo.Checked = True
        End If

        'Se o formulário é utilizado para solicitação de fundos
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos) Then
            pnlWs7SolicitacionFondos.Visible = True
            chkSolicitacionFondos.Checked = True
        End If

        'Se o formulário é utilizado para contestação de fundos
        If VerificaCaracteristicaExiste(formulario, Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos) Then
            pnlWs7ContestarSolicitacionFondos.Visible = True
            chkContestarSolicitacionFondos.Checked = True
        End If

    End Sub

    Private Sub PreencherIdentificacion(ByRef formulario As Clases.Formulario)

        If hidIdendificador.Value.Trim.Length > 0 AndAlso Modo = Enumeradores.Modo.Modificacion Then
            formulario.Identificador = FormularioGrabado.Identificador
            formulario.FechaHoraModificacion = DateTime.UtcNow
            formulario.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            formulario.UsuarioCreacion = FormularioGrabado.UsuarioCreacion
            formulario.FechaHoraCreacion = FormularioGrabado.FechaHoraCreacion
        Else
            formulario.Identificador = System.Guid.NewGuid.ToString
            formulario.FechaHoraCreacion = DateTime.UtcNow
            formulario.UsuarioCreacion = Parametros.Permisos.Usuario.Login
            formulario.FechaHoraModificacion = DateTime.UtcNow
            formulario.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        End If

        formulario.Codigo = txtCodigo.Text
        formulario.Descripcion = txtDescripcion.Text
        formulario.Color = System.Drawing.Color.FromName(txtColor.Text)
        If IconeParaSalvar IsNot Nothing AndAlso IconeParaSalvar.Length > 0 Then
            formulario.Icono = IconeParaSalvar
        End If
        formulario.TipoDocumento = New Clases.TipoDocumento
        formulario.TipoDocumento.Identificador = ddlTipoDocumento.SelectedValue
        formulario.Caracteristicas = New List(Of Enumeradores.CaracteristicaFormulario)
        If chkDocumentoIndividual.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual)
        End If
        'Formularios de Substituição, Solicitação de Fundos e Contestar Fundos não podem ser de grupos
        If chkDocumentoGrupo.Checked AndAlso Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked)) Then

            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo)

        End If

        formulario.EstaActivo = chkActivo.Checked

    End Sub

    Private Sub PreencheCaracteriscaPrincial(ByRef formulario As Clases.Formulario)

        If rbCaracteristicaPrincipalGestionRemesa.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.GestiondeRemesas)
        ElseIf rbCaracteristicaPrincipalGestionBultos.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.GestiondeBultos)
        ElseIf rbCaracteristicaPrincipalGestionFondos.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.GestiondeFondos)
        ElseIf rbCaracteristicaPrincipalGestionContenedores.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.GestiondeContenedores)
        ElseIf rbCaracteristicaPrincipalCierres.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Cierres)
        ElseIf rbCaracteristicaPrincipalOtrosMovimientos.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.OtrosMovimientos)
        End If

    End Sub

    Private Sub PreencheCaracteriscasPaso3(ByRef formulario As Clases.Formulario, ByRef tiposBultos As List(Of Clases.TipoBulto))


        If rbCaracteristicaPrincipalGestionRemesa.Checked Then

            'Radios Buttons
            If rbGestionRemesasAltas.Checked = True Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Altas)
            ElseIf rbGestionRemesasBajas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Bajas)
            ElseIf rbGestionRemesasReenvios.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Reenvios)
            ElseIf rbGestionRemesasActas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Actas)
            ElseIf rbGestionRemesasSustitucion.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Sustitucion)
            End If

            'Tipos de Bultos selecionados no list box
            For value As Integer = 0 To chlGestionRemesasTipoBulto.Items.Count - 1
                If chlGestionRemesasTipoBulto.Items(value).Selected Then
                    Dim tipoBulto As New Clases.TipoBulto
                    'Oid do tipo bulto
                    tipoBulto.Identificador = chlGestionRemesasTipoBulto.Items(value).Value
                    tipoBulto.FechaHoraCreacion = formulario.FechaHoraCreacion
                    tipoBulto.UsuarioCreacion = formulario.UsuarioCreacion
                    tipoBulto.FechaHoraModificacion = formulario.FechaHoraModificacion
                    tipoBulto.UsuarioModificacion = formulario.UsuarioModificacion

                    tiposBultos.Add(tipoBulto)
                End If
            Next

            'CheckBox Saldo Negativo
            If chkGestionRemesasPermiteLlegarSaldoNegativo.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            End If

            If rbGestionRemesasAltas.Checked Then
                dvGestionRemesasExcluirSectoresHijos.Style("display") = "none"
                chkGestionRemesasExcluirSectoresHijos.Checked = False
            Else
                dvGestionRemesasExcluirSectoresHijos.Style("display") = "block"
            End If

            'CheckBox Excluir Sectores Hijos
            If chkGestionRemesasExcluirSectoresHijos.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos)
            End If

        ElseIf rbCaracteristicaPrincipalGestionBultos.Checked Then

            'Radios Buttons
            If rbGestionBultosAltas.Checked = True Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Altas)
            ElseIf rbGestionBultosBajas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Bajas)
            ElseIf rbGestionBultosReenvios.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Reenvios)
            ElseIf rbGestionBultosActas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Actas)
            ElseIf rbGestionBultosSustitucion.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Sustitucion)
            End If

            'Tipos de Bultos selecionados no list box
            For value As Integer = 0 To chlGestionBultosTipoBulto.Items.Count - 1
                If chlGestionBultosTipoBulto.Items(value).Selected Then
                    Dim tipoBulto As New Clases.TipoBulto
                    'Oid do tipo bulto
                    tipoBulto.Identificador = chlGestionBultosTipoBulto.Items(value).Value
                    tipoBulto.FechaHoraCreacion = formulario.FechaHoraCreacion
                    tipoBulto.UsuarioCreacion = formulario.UsuarioCreacion
                    tipoBulto.FechaHoraModificacion = formulario.FechaHoraModificacion
                    tipoBulto.UsuarioModificacion = formulario.UsuarioModificacion

                    tiposBultos.Add(tipoBulto)
                End If
            Next

            'CheckBox Saldo Negativo
            If chkGestionBultosPermiteLlegarSaldoNegativo.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            End If

            If rbGestionBultosAltas.Checked Then
                dvGestionBultosExcluirSectoresHijos.Style("display") = "none"
                chkGestionBultosExcluirSectoresHijos.Checked = False
            Else
                dvGestionBultosExcluirSectoresHijos.Style("display") = "block"
            End If

            'CheckBox Excluir Sectores Hijos
            If chkGestionBultosExcluirSectoresHijos.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos)
            End If

        ElseIf rbCaracteristicaPrincipalGestionFondos.Checked Then

            'Radios Buttons
            If rbGestionFondosMovimientoFondos.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.MovimientodeFondos)
            ElseIf rbGestionFondosAjustes.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Ajustes)

                If rbTipoAjustesSoloPositivos.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesPositivos)
                ElseIf rbTipoAjustesSoloNegativos.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SoloPermitirAjustesNegativos)
                End If

            ElseIf rbGestionFondosSustitucion.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Sustitucion)
            ElseIf rbGestionFondosSolicitacion.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos)
            ElseIf rbGestionFondosClasificacion.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Classificacion)
            End If

            'CheckBox Saldo Negativo
            If chkGestionFondosPermiteLlegarSaldoNegativo.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            End If

        ElseIf rbCaracteristicaPrincipalGestionContenedores.Checked Then

            'Radios Buttons
            If rbGestionContenedoresAltas.Checked = True Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Altas)
            ElseIf rbGestionContenedoresBajas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Bajas)
            ElseIf rbGestionContenedoresReenvios.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.Reenvios)
            End If

            'CheckBox Saldo Negativo
            If chkGestionContenedoresPermiteLlegarSaldoNegativo.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
            End If

            'CheckBox Pack Modular
            If chkGestionContenedoresPackModular.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PackModular)
            End If

            'CheckBox Excluir Setores Filhos
            If chkGestionContenedoresExcluirSectoresHijos.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos)
            End If

        ElseIf rbCaracteristicaPrincipalCierres.Checked Then

            'Radios Buttons
            If rbCierresTesoro.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.CierreDeTesoro)
            ElseIf rbCierresCaja.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.CierreDeCaja)

                If chkCierreCajaPermiteLlegarSaldoNegativo.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)
                End If

            ElseIf rbCierresCuadreFisico.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.CuadreFisico)
            End If

            If rbCierresCuadreFisico.Checked Then
                dvGestionCierreExcluirSectoresHijos.Style("display") = "none"
                chkGestionCierreExcluirSectoresHijos.Checked = False
            Else
                dvGestionCierreExcluirSectoresHijos.Style("display") = "block"
            End If

            'CheckBox Excluir Sectores Hijos
            If chkGestionCierreExcluirSectoresHijos.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos)
            End If

            If chkImprime.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.AlConfirmarElDocumentoSeImprime)
            End If

        End If

    End Sub

    Private Sub PreencheCaracteriscasPaso4(ByRef formulario As Clases.Formulario)

        If rbCaracteristicaPrincipalGestionRemesa.Checked Then

            If rbGestionRemesasBajas.Checked Then
                'Radios Buttons
                If rbGestionRemesasBajasBajaElementos.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.BajaElementos)
                ElseIf rbGestionRemesasBajasSalidasRecorrido.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SalidasRecorrido)
                End If

                'Drop Down de Filtro
                If ddlGestionRemesasBajasFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionRemesasBajasFiltro.SelectedValue
                End If

            ElseIf rbGestionRemesasReenvios.Checked Then
                'Radios Buttons
                If rbGestionRemesasReenviosEntreSectores.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
                ElseIf rbGestionRemesasReenviosEntrePlantas.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
                End If

                'Reenvio Automatico
                If chkGestionRemesasReenviosReenvioAutomatico.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ReenvioAutomatico)
                End If

                'Drop Down de Filtro
                If ddlGestionRemesasReenviosFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionRemesasReenviosFiltro.SelectedValue
                End If

            ElseIf rbGestionRemesasActas.Checked Then

                If rbGestionRemesasActasActaRecuento.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeRecuento)
                ElseIf rbGestionRemesasActasActaClasificado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeClasificado)
                ElseIf rbGestionRemesasActasActaEmbolsado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeEmbolsado)
                ElseIf rbGestionRemesasActasActaDesembolsado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado)
                End If

                'Drop Down de Filtro
                If ddlGestionRemesasActasFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionRemesasActasFiltro.SelectedValue
                End If

            End If

        ElseIf rbCaracteristicaPrincipalGestionBultos.Checked Then

            If rbGestionBultosBajas.Checked Then
                'Radios Buttons
                If rbGestionBultosBajasBajaElementos.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.BajaElementos)
                ElseIf rbGestionBultosBajasSalidasRecorrido.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SalidasRecorrido)
                End If

                'Drop Down de Filtro
                If ddlGestionBultosBajasFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionBultosBajasFiltro.SelectedValue
                End If

            ElseIf rbGestionBultosReenvios.Checked Then
                'Radios Buttons
                If rbGestionBultosReenviosEntreSectores.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
                ElseIf rbGestionBultosReenviosEntrePlantas.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
                End If

                'Reenvio Automatico
                If chkGestionBultosReenviosReenvioAutomatico.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ReenvioAutomatico)
                End If

                'Drop Down de Filtro
                If ddlGestionBultosReenviosFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionBultosReenviosFiltro.SelectedValue
                End If

            ElseIf rbGestionBultosActas.Checked Then

                If rbGestionBultosActasActaRecuento.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeRecuento)
                ElseIf rbGestionBultosActasActaClasificado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeClasificado)
                ElseIf rbGestionBultosActasActaEmbolsado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeEmbolsado)
                ElseIf rbGestionBultosActasActaDesembolsado.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado)
                End If

                'Drop Down de Filtro
                If ddlGestionBultosActasFiltro.SelectedValue <> String.Empty Then
                    formulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                    formulario.FiltroFormulario.Identificador = ddlGestionBultosActasFiltro.SelectedValue
                End If

            End If

        ElseIf rbCaracteristicaPrincipalGestionFondos.Checked Then

            If rbGestionFondosMovimientoFondos.Checked Then

                If rbGestionFondosMovimientoFondosEntreSectores.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
                ElseIf rbGestionFondosMovimientoFondosEntrePlantas.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
                ElseIf rbGestionFondosMovimientoFondosEntreCanales.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreCanales)
                ElseIf rbGestionFondosMovimientoFondosEntreClientes.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreClientes)
                End If

            End If

        ElseIf rbCaracteristicaPrincipalCierres.Checked Then

            'Radios Buttons
            If rbGestionCierreEntreSectores.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
            ElseIf rbGestionCierreEntrePlantas.Checked Then
                formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
            End If

        ElseIf rbCaracteristicaPrincipalGestionContenedores.Checked Then
            If rbGestionContenedoresBajas.Checked Then

                'Radios Buttons
                If rbGestionContenedoresBajasDesarmar.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.DesarmarContenedor)
                ElseIf rbGestionContenedoresBajasSalida.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SalidasRecorrido)
                End If

            ElseIf rbGestionContenedoresReenvios.Checked Then

                'Radios Buttons
                If rbGestionContenedoresReenviosEntreSectores.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)
                ElseIf rbGestionContenedoresReenviosEntrePlantas.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)
                ElseIf rbGestionContenedoresReenviosEntreClientes.Checked Then
                    formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.EntreClientes)
                End If

            End If
        End If

    End Sub

    Private Sub PreencheCaracteriscasPaso5(formulario As Clases.Formulario, ByRef tiposSectoresOrigen As List(Of Clases.TipoSector), ByRef tiposSectoresDestino As List(Of Clases.TipoSector))

        'Tipos de Sectores origem Selecionados
        For value As Integer = 0 To lstTiposSectoresOrigenSeleccionados.Items.Count - 1
            Dim tipoSectorOri As New Clases.TipoSector

            'Oid do tipo bulto
            tipoSectorOri.Identificador = lstTiposSectoresOrigenSeleccionados.Items(value).Value
            tipoSectorOri.FechaHoraCreacion = formulario.FechaHoraCreacion
            tipoSectorOri.UsuarioCreacion = formulario.UsuarioCreacion
            tipoSectorOri.FechaHoraModificacion = formulario.FechaHoraModificacion
            tipoSectorOri.UsuarioModificacion = formulario.UsuarioModificacion

            tiposSectoresOrigen.Add(tipoSectorOri)
        Next

        'O Novo Saldos deve permitir configurar o setor destino quando o formulário é:
        'Gestión de Elementos – Alta 
        '   Características: CARACTERISTICA_PRINCIPAL_GESTION_REMESAS/CARACTERISTICA_PRINCIPAL_GESTION_BULTOS, ACCION_ALTAS

        'Gestión de Fondos – Movimiento entre sectores (de misma planta/planta distinta) ou Solicitación de Fondos
        ' Características: CARACTERISTICA_PRINCIPAL_GESTION_FONDOS, ENTRE_SECTORES_MISMA_PLANTA/ENTRE_SECTORES_PLANTAS_DIFERENTES 
        ' ou(CARACTERISTICA_PRINCIPAL_GESTION_FONDOS, ACCION_SOLICITACION_FONDOS)

        'Cierre de caja
        'Característica: CIERRE_CAJA

        If ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso (rbGestionBultosReenvios.Checked OrElse rbGestionBultosAltas.Checked)) OrElse _
            (rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso (rbGestionRemesasReenvios.Checked OrElse rbGestionRemesasAltas.Checked)) OrElse _
            (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked AndAlso Not rbGestionFondosMovimientoFondosEntreClientes.Checked) OrElse _
            (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSolicitacion.Checked) OrElse
            (rbCaracteristicaPrincipalCierres.Checked AndAlso rbCierresCaja.Checked) OrElse _
            (rbCaracteristicaPrincipalGestionContenedores.Checked AndAlso rbGestionContenedoresReenvios.Checked)) Then

            'Tipos de Sectores Destino Selecionados
            For value As Integer = 0 To lstTiposSectoresDestinoSeleccionados.Items.Count - 1
                Dim tipoSectorDest As New Clases.TipoSector

                'Oid do tipo bulto
                tipoSectorDest.Identificador = lstTiposSectoresDestinoSeleccionados.Items(value).Value
                tipoSectorDest.FechaHoraCreacion = formulario.FechaHoraCreacion
                tipoSectorDest.UsuarioCreacion = formulario.UsuarioCreacion
                tipoSectorDest.FechaHoraModificacion = formulario.FechaHoraModificacion
                tipoSectorDest.UsuarioModificacion = formulario.UsuarioModificacion

                tiposSectoresDestino.Add(tipoSectorDest)
            Next
        Else

            'Se os setores de destino não estiverem visiveis
            'os setores de destino são os mesmo de origem

            For value As Integer = 0 To lstTiposSectoresOrigenSeleccionados.Items.Count - 1
                Dim tipoSectorOri As New Clases.TipoSector

                'Oid do tipo bulto
                tipoSectorOri.Identificador = lstTiposSectoresOrigenSeleccionados.Items(value).Value
                tipoSectorOri.FechaHoraCreacion = formulario.FechaHoraCreacion
                tipoSectorOri.UsuarioCreacion = formulario.UsuarioCreacion
                tipoSectorOri.FechaHoraModificacion = formulario.FechaHoraModificacion
                tipoSectorOri.UsuarioModificacion = formulario.UsuarioModificacion

                tiposSectoresDestino.Add(tipoSectorOri)
            Next

        End If

    End Sub

    Private Sub PreencheCaracteriscasPaso6(ByRef formulario As Clases.Formulario, ByRef estadosAccionesContales As List(Of Clases.EstadoAccionContable), _
                                           ByRef copias As List(Of Clases.Copia))

        ' documentos de substituição ou solicitação de fundos ou classificação não permitem configurações de ações contábeis
        If Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse
                 (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse
             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) OrElse
             (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosClasificacion.Checked) OrElse
             ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso
              (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) AndAlso
              chkSolicitacionFondos.Checked)) Then

            'Ação Contábil
            formulario.AccionContable = New Clases.AccionContable
            Dim estado As Clases.EstadoAccionContable
            If rbAccionContableSeleccionar.Checked Then
                formulario.AccionContable = LogicaNegocio.GenesisSaldos.AccionContable.ObtenerAccionContable(ddlAccionContableSeleccionar.SelectedValue)
            Else
                formulario.AccionContable.Identificador = System.Guid.NewGuid.ToString
                formulario.AccionContable.Codigo = "COD_" & System.Guid.NewGuid.ToString
                formulario.AccionContable.Codigo = formulario.AccionContable.Codigo.Substring(0, 15)
                formulario.AccionContable.Descripcion = txtAccionContableNuevo.Text

                formulario.AccionContable.EstaActivo = True
                formulario.AccionContable.FechaHoraCreacion = formulario.FechaHoraCreacion
                formulario.AccionContable.FechaHoraModificacion = formulario.FechaHoraModificacion
                formulario.AccionContable.UsuarioCreacion = formulario.UsuarioCreacion
                formulario.AccionContable.UsuarioModificacion = formulario.UsuarioModificacion
            End If

            'Estados da Ação Contábil

            'Confirmado
            estado = New Clases.EstadoAccionContable
            estado.Identificador = System.Guid.NewGuid.ToString
            estado.IdentificadorAccionContable = formulario.AccionContable.Identificador
            estado.Codigo = Enumeradores.EstadoDocumento.Confirmado.RecuperarValor()
            estado.OrigemDisponible = ddlAccionContableEstadoConfirmadoDisponibleOrigen.SelectedValue
            estado.OrigemNoDisponible = ddlAccionContableEstadoConfirmadoNoDisponibleOrigen.SelectedValue
            estado.DestinoDisponible = ddlAccionContableEstadoConfirmadoDisponibleDestino.SelectedValue
            estado.DestinoNoDisponible = ddlAccionContableEstadoConfirmadoNoDisponibleDestino.SelectedValue
            estado.DestinoDisponibleBloqueado = ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino.SelectedValue
            estado.OrigenDisponibleBloqueado = ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen.SelectedValue
            estado.FechaHoraCreacion = formulario.FechaHoraCreacion
            estado.FechaHoraModificacion = formulario.FechaHoraModificacion
            estado.UsuarioCreacion = formulario.UsuarioCreacion
            estado.UsuarioModificacion = formulario.UsuarioModificacion
            estadosAccionesContales.Add(estado)

            'Aceptado
            estado = New Clases.EstadoAccionContable
            estado.Identificador = System.Guid.NewGuid.ToString
            estado.IdentificadorAccionContable = formulario.AccionContable.Identificador
            estado.Codigo = Enumeradores.EstadoDocumento.Aceptado.RecuperarValor()
            estado.OrigemDisponible = ddlAccionContableEstadoAceptadoDisponibleOrigen.SelectedValue
            estado.OrigemNoDisponible = ddlAccionContableEstadoAceptadoNoDisponibleOrigen.SelectedValue
            estado.DestinoDisponible = ddlAccionContableEstadoAceptadoDisponibleDestino.SelectedValue
            estado.DestinoNoDisponible = ddlAccionContableEstadoAceptadoNoDisponibleDestino.SelectedValue
            estado.DestinoDisponibleBloqueado = ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino.SelectedValue
            estado.OrigenDisponibleBloqueado = ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen.SelectedValue
            estado.FechaHoraCreacion = formulario.FechaHoraCreacion
            estado.FechaHoraModificacion = formulario.FechaHoraModificacion
            estado.UsuarioCreacion = formulario.UsuarioCreacion
            estado.UsuarioModificacion = formulario.UsuarioModificacion
            estadosAccionesContales.Add(estado)

            'Rechazado
            estado = New Clases.EstadoAccionContable
            estado.Identificador = System.Guid.NewGuid.ToString
            estado.IdentificadorAccionContable = formulario.AccionContable.Identificador
            estado.Codigo = Enumeradores.EstadoDocumento.Rechazado.RecuperarValor()
            estado.OrigemDisponible = ddlAccionContableEstadoRechazadoDisponibleOrigen.SelectedValue
            estado.OrigemNoDisponible = ddlAccionContableEstadoRechazadoNoDisponibleOrigen.SelectedValue
            estado.DestinoDisponible = ddlAccionContableEstadoRechazadoDisponibleDestino.SelectedValue
            estado.DestinoNoDisponible = ddlAccionContableEstadoRechazadoNoDisponibleDestino.SelectedValue
            estado.DestinoDisponibleBloqueado = ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino.SelectedValue
            estado.OrigenDisponibleBloqueado = ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen.SelectedValue
            estado.FechaHoraCreacion = formulario.FechaHoraCreacion
            estado.FechaHoraModificacion = formulario.FechaHoraModificacion
            estado.UsuarioCreacion = formulario.UsuarioCreacion
            estado.UsuarioModificacion = formulario.UsuarioModificacion
            estadosAccionesContales.Add(estado)

        End If

        'Cópias
        If lstImprimeDestinos.Items.Count > 0 Then
            For i As Integer = 0 To lstImprimeDestinos.Items.Count - 1
                Dim copia As New Clases.Copia

                copia.Identificador = System.Guid.NewGuid.ToString
                copia.IdentificadorFormulario = formulario.Identificador
                copia.Destino = lstImprimeDestinos.Items(i).Text
                copia.Cantidad = i + 1
                copia.FechaHoraCreacion = formulario.FechaHoraCreacion
                copia.FechaHoraModificacion = formulario.FechaHoraModificacion
                copia.UsuarioCreacion = formulario.UsuarioCreacion
                copia.UsuarioModificacion = formulario.UsuarioModificacion

                copias.Add(copia)
            Next
        End If

        'IAC Individual
        formulario.GrupoTerminosIACIndividual = New Clases.GrupoTerminosIAC
        If ddlIAC.SelectedValue <> String.Empty Then
            formulario.GrupoTerminosIACIndividual.Identificador = ddlIAC.SelectedValue
        End If

        ' documentos de substituição, solicitação de fundos ou contestar fundos não permitem configurações IAC's para grupos (uma vez que não é possível criar um documento de substituição de grupo)
        If Not (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasSustitucion.Checked) OrElse (rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosSustitucion.Checked)) OrElse (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosSustitucion.Checked) _
            OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
                                                AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
                                                AndAlso chkSolicitacionFondos.Checked) _
                                            OrElse ((((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) AndAlso chkContestarSolicitacionFondos.Checked)) Then
            'IAC Grupo
            formulario.GrupoTerminosIACGrupo = New Clases.GrupoTerminosIAC
            If ddlIACGrupo.SelectedValue <> String.Empty Then
                formulario.GrupoTerminosIACGrupo.Identificador = ddlIACGrupo.SelectedValue
            End If

        End If

        'Código Externo
        If chkCodigoExternoObligatorio.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.CodigoExternoObligatorio)
        End If

        ' Movimientode Aceptacion Automatica
        If chkMovimientoAceptacionAutomatica.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.MovimientodeAceptacionAutomatica)
        End If

        ' Modificar Término
        If chkModificarTermino.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ModificarTerminos)
        End If

        'Integração Salidas
        'CheckBox Integração Salidas
        If chkIntegracionSalidas.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.IntegracionSalidas)
        End If

        'Integração Recepción y Envío
        'CheckBox Integração Recipción y Envío
        If chkIntegracionRecepcionEnvio.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio)
        End If

        'Integração Legado
        'CheckBox Integração Legado
        If chkIntegracionLegado.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.IntegracionLegado)
        End If

        'Integração Conteo
        'CheckBox Integração Conteo
        If chkIntegracionConteo.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.IntegracionConteo)
        End If

        'Solicitação de fundos - CheckBox Integração Salidas
        If (rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) _
            AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked) _
            AndAlso chkSolicitacionFondos.Checked Then
            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.SolicitaciondeFondos)
        End If

        'Contestar fundos
        If (((rbCaracteristicaPrincipalGestionRemesa.Checked AndAlso rbGestionRemesasReenvios.Checked) AndAlso (rbGestionRemesasReenviosEntreSectores.Checked OrElse rbGestionRemesasReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionBultos.Checked AndAlso rbGestionBultosReenvios.Checked) AndAlso (rbGestionBultosReenviosEntreSectores.Checked OrElse rbGestionBultosReenviosEntrePlantas.Checked)) _
                                                    OrElse ((rbCaracteristicaPrincipalGestionFondos.Checked AndAlso rbGestionFondosMovimientoFondos.Checked) AndAlso (rbGestionFondosMovimientoFondosEntreSectores.Checked OrElse rbGestionFondosMovimientoFondosEntrePlantas.Checked))) _
            AndAlso chkContestarSolicitacionFondos.Checked Then

            formulario.Caracteristicas.Add(Enumeradores.CaracteristicaFormulario.ContestarSolicitacionDeFondos)
        End If

    End Sub
    Sub ValidasTipoSectorOrigen(source As System.Object, args As System.Web.UI.WebControls.ServerValidateEventArgs)
        If Me.lstTiposSectoresOrigenSeleccionados.Items.Count = 0 Then
            args.IsValid = False
        End If
    End Sub
    Sub ValidasTipoSectorDestino(source As System.Object, args As System.Web.UI.WebControls.ServerValidateEventArgs)
        If Me.lstTiposSectoresDestinoSeleccionados.Items.Count = 0 Then
            args.IsValid = False
        End If
    End Sub

#End Region

End Class