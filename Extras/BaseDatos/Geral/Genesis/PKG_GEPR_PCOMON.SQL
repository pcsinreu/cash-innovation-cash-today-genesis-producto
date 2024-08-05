CREATE OR REPLACE PACKAGE gepr_pcomon_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

  SUBTYPE tipo$oid_ IS VARCHAR2(36);
  SUBTYPE tipo$usr_ IS VARCHAR2(50);
  SUBTYPE tipo$est_ IS VARCHAR2(5);
  SUBTYPE tipo$cod_ IS VARCHAR2(80);
  SUBTYPE tipo$nbol_ IS NUMBER(1);
  SUBTYPE tipo$fyh_ IS DATE;
  SUBTYPE tipo$cant_ IS NUMBER(16);
  SUBTYPE tipo$desc_ IS VARCHAR(4000);
  SUBTYPE tipo$imp_ IS NUMBER(20, 2);
  SUBTYPE tipo$nel_ IS NUMBER(38);
  SUBTYPE tipo$gmt_ IS TIMESTAMP(6)
    WITH TIME ZONE;
  SUBTYPE tipo$obs_ IS VARCHAR2(32767);
  SUBTYPE tipo$bol_ IS BOOLEAN;
  SUBTYPE tipo$clob_ IS CLOB;
  SUBTYPE tipo$blob_ IS BLOB;

  TYPE tipo$oids_ IS TABLE OF tipo$oid_ INDEX BY BINARY_INTEGER;
  TYPE tipo$cods_ IS TABLE OF tipo$cod_ INDEX BY BINARY_INTEGER;
  TYPE tipo$descs_ IS TABLE OF tipo$desc_ INDEX BY BINARY_INTEGER;
  TYPE tipo$imps_ IS TABLE OF tipo$imp_ INDEX BY BINARY_INTEGER;
  TYPE tipo$nels_ IS TABLE OF tipo$nel_ INDEX BY BINARY_INTEGER;
  TYPE tipo$ints_ IS TABLE OF INTEGER INDEX BY BINARY_INTEGER;
  TYPE tipo$fyhs_ IS TABLE OF tipo$fyh_ INDEX BY BINARY_INTEGER;
  TYPE tipo$bools_ IS TABLE OF tipo$bol_ INDEX BY BINARY_INTEGER;
  TYPE tipo$obss_ IS TABLE OF tipo$obs_ INDEX BY BINARY_INTEGER;
  TYPE tipo$usrs_ IS TABLE OF tipo$usr_ INDEX BY BINARY_INTEGER;
  TYPE tipo$ests_ IS TABLE OF tipo$est_ INDEX BY BINARY_INTEGER;
  TYPE tipo$nbols_ IS TABLE OF tipo$nbol_ INDEX BY BINARY_INTEGER;
  TYPE tipo$cants_ IS TABLE OF tipo$cant_ INDEX BY BINARY_INTEGER;
  TYPE tipo$gmts_ IS TABLE OF tipo$gmt_ INDEX BY BINARY_INTEGER;
  TYPE tipo$clobs_ IS TABLE OF tipo$clob_ INDEX BY BINARY_INTEGER;
  TYPE tipo$blobs_ IS TABLE OF tipo$blob_ INDEX BY BINARY_INTEGER;

  TYPE tipo$Cuenta IS RECORD(
    oid_cuenta       tipo$oid_,
    oid_cliente      tipo$oid_,
    cod_cliente      tipo$cod_,
    des_cliente      tipo$desc_,
    oid_subcliente   tipo$oid_,
    cod_subcliente   tipo$cod_,
    des_subcliente   tipo$desc_,
    oid_pto_servicio tipo$oid_,
    cod_pto_servicio tipo$cod_,
    des_pto_servicio tipo$desc_,
    oid_canal        tipo$oid_,
    cod_canal        tipo$cod_,
    oid_subcanal     tipo$oid_,
    cod_subcanal     tipo$oid_,
    oid_sector       tipo$oid_,
    cod_sector       tipo$cod_,
    oid_planta       tipo$oid_,
    cod_planta       tipo$oid_,
    oid_delegacion   tipo$oid_,
    cod_delegacion   tipo$oid_,
    cod_pais         tipo$cod_,
    cod_tipo_cuenta  tipo$cod_);

  TYPE tipo$contenedor IS RECORD(
    OID_CONTENEDOR             tipo$oid_,
    OID_CONTENEDOR_PADRE       tipo$oid_,
    OID_CONTENEDOR_MAYOR_NIVEL tipo$oid_,
    OID_TIPO_CONTENEDOR        tipo$oid_,
    OID_SECTOR                 tipo$oid_,
    OID_CUENTA_MOVIMIENTO      tipo$oid_,
    OID_CLIENTE                tipo$oid_,
    OID_SUBCLIENTE             tipo$oid_,
    OID_PTO_SERVICIO           tipo$oid_,
    OID_CANAL                  tipo$oid_,
    OID_SUBCANAL               tipo$oid_,
    OID_DOCUMENTO              tipo$oid_,
    COD_ESTADO                 tipo$est_,
    BOL_PACK_MODULAR           tipo$nbol_,
    FEC_VENCIMIENTO            tipo$fyh_,
    COD_PUESTO                 tipo$cod_,
    COD_TIPO_ELEMENTO          tipo$cod_,
    OID_PLANTA                 tipo$oid_,
    OID_DELEGACION             tipo$oid_,
    OID_CUENTA_SALDO           tipo$oid_,
    OID_GRUPO_DOCUMENTO        tipo$oid_,
    COD_TIPO_CONTENEDOR        tipo$cod_);

  TYPE tipo$efectivo_mp_contenedor IS RECORD(
    OID_CONTENEDOR      tipo$oid_,
    OID_DIVISA          tipo$oid_,
    OID_DENOMINACION    tipo$oid_,
    OID_CALIDAD         tipo$oid_,
    OID_UNIDAD_MEDIDA   tipo$oid_,
    COD_NIVEL_DETALLE   tipo$cod_,
    COD_TIPO_MEDIO_PAGO tipo$cod_,
    OID_MEDIO_PAGO      tipo$oid_,
    DES_MEDIO_PAGO      tipo$desc_,
    NEL_CANTIDAD        tipo$nel_,
    NUM_IMPORTE         tipo$imp_);

  TYPE tipo$Formulario IS RECORD(
    OID_FORMULARIO        tipo$oid_,
    COD_FORMULARIO        tipo$oid_,
    OID_ACCION_CONTABLE   tipo$oid_,
    OID_TIPO_DOCUMENTO    tipo$oid_,
    OID_CARACT_FORMULARIO tipo$oid_,
    COD_CARACT_FORMULARIO tipo$cod_);

  TYPE tipo$ListaValor IS RECORD(
    OID_LISTA_TIPO  tipo$oid_,
    OID_LISTA_VALOR tipo$oid_);

  TYPE tipo$totalizador IS RECORD(
    OID_CONFIG_NIVEL_MOVIMIENTO tipo$oid_,
    BOL_DEFECTO                 tipo$nbol_,
    OID_CLIENTE                 tipo$oid_,
    COD_CLIENTE                 tipo$cod_,
    DES_CLIENTE                 tipo$desc_,
    OID_SUBCLIENTE              tipo$oid_,
    COD_SUBCLIENTE              tipo$cod_,
    DES_SUBCLIENTE              tipo$desc_,
    OID_PTO_SERVICIO            tipo$oid_,
    COD_PTO_SERVICIO            tipo$cod_,
    DES_PTO_SERVICIO            tipo$desc_,
    OID_SUBCANAL                tipo$oid_);

  TYPE tipo$precinto_Contenedor IS RECORD(
    OID_CONTENEDOR tipo$oid_,
    COD_PRECINTO   tipo$cod_);

  TYPE tipo$lst_valor_contenedor IS RECORD(
    OID_CONTENEDOR tipo$oid_,
    COD_TIPO       tipo$cod_,
    COD_VALOR      tipo$cod_,
    DES_VALOR      tipo$desc_);

  TYPE tipo$tipoContenedor IS RECORD(
    OID_TIPO_CONTENEDOR    tipo$oid_,
    COD_TIPO_CONTENEDOR    tipo$cod_,
    DES_TIPO_CONTENEDOR    tipo$desc_,
    NEC_CANTIDAD           tipo$nel_,
    OID_UNIDAD_MEDIDA      tipo$oid_,
    COD_UNIDAD_MEDIDA      tipo$cod_,
    COD_TIPO_UNIDAD_MEDIDA tipo$cod_,
    NUM_VALOR_UNIDAD       tipo$imp_,
    OID_CALIDAD            tipo$oid_,
    COD_CALIDAD            tipo$cod_,
    BOL_PICO               tipo$nbol_);

  TYPE tipo$totalizadores IS TABLE OF tipo$totalizador INDEX BY BINARY_INTEGER;
  TYPE tipo$formularios IS TABLE OF tipo$Formulario INDEX BY BINARY_INTEGER;
  TYPE tipo$contenedores IS TABLE OF tipo$contenedor INDEX BY BINARY_INTEGER;
  TYPE tipo$efec_mp_contenedores IS TABLE OF tipo$efectivo_mp_contenedor INDEX BY BINARY_INTEGER;
  TYPE tipo$precintos_contenedores IS TABLE OF tipo$precinto_Contenedor INDEX BY BINARY_INTEGER;
  TYPE tipo$lst_val_contenedores IS TABLE OF tipo$lst_valor_contenedor INDEX BY BINARY_INTEGER;
  TYPE tipo$tiposContenedores IS TABLE OF tipo$tipoContenedor INDEX BY BINARY_INTEGER;

  /* Codigos Funcionalidades Dicionario */
  const$CodFuncDicGrabarDocCont CONSTANT tipo$desc_ := 'GrabarDocumientoContenedor';
  const$CodFuncDicGrabarDoc     CONSTANT tipo$desc_ := 'GrabarDocumiento';
  const$CodFuncDicGrabarGpDoc   CONSTANT tipo$desc_ := 'GrabarGrupoDocumiento';
  const$CodFuncDicComon         CONSTANT tipo$desc_ := 'Comon';
  const$CodFunDicObtCuentaSaldo CONSTANT tipo$desc_ := 'RecuperarCuentaSaldo';
  const$CodFunDicObtClientes    CONSTANT tipo$desc_ := 'RECUPERARCLIENTES';
  const$CodFunDefCambPosicCont  CONSTANT tipo$desc_ := 'DefinirCambiarPosicionContenedor';
  const$CodFunGravarInventario  CONSTANT tipo$desc_ := 'GravarInventario';
  const$CodFunValidarAtributo   CONSTANT tipo$desc_ := 'ValidarAtributo';
  const$CodFunIntegridadArray   CONSTANT tipo$desc_ := 'IntegridadeArray';
  const$CodFunAbono             CONSTANT tipo$desc_ := 'ABONO';
  const$CodFunActualizarPrecint CONSTANT tipo$desc_ := 'AtualizarPrecintoSaldos';

  const$CodFunGenerico          CONSTANT tipo$desc_ := 'GENERICO';
  const$CodFunGeneralCuenta     CONSTANT tipo$desc_ := 'GENERALCUENTA';
  const$CodFunMarcarMovimientos CONSTANT tipo$desc_ := 'MARCARMOVIMIENTOS';
  const$CodFunRecuperarPlanific CONSTANT tipo$desc_ := 'RECUPERARPLANIFICACIONES';
  const$CodFunRecuperarMaquinas CONSTANT tipo$desc_ := 'RECUPERARMAES';
  const$CodFunRecuperarMovimien CONSTANT tipo$desc_ := 'RECUPERARMOVIMIENTOS';
  const$CodFunRecuperarSaldos   CONSTANT tipo$desc_ := 'RECUPERARSALDOS';
  const$CodFunConfigurarMaquina CONSTANT tipo$desc_ := 'CONFIGURARMAQUINAS';
  const$CodFunConfigurarCliente CONSTANT tipo$desc_ := 'CONFIGURARCLIENTES';
  const$CodFunRelacionarMovPeri CONSTANT tipo$desc_ := 'RELACIONARMOVIMIENTOSPERIODOS';
  const$CodFunMoviAcreditacion  CONSTANT tipo$desc_ := 'ALTAMOVIMIENTOSACREDITACIONES';
  const$CodFunMoviCierreFact    CONSTANT tipo$desc_ := 'ALTAMOVIMIENTOSCIERREFACTURACION';
  const$CodFunMoviShipout       CONSTANT tipo$desc_ := 'ALTAMOVIMIENTOSSHIPOUT';
  const$CodFunMoviRecuento      CONSTANT tipo$desc_ := 'ALTAMOVIMIENTOSRECUENTO';
  const$CodFunMoviProvisionEfec CONSTANT tipo$desc_ := 'ALTAMOVIMIENTOSPROVISIONEFECTIVO';
  const$CodFunGenericoCuentas   CONSTANT tipo$desc_ := 'GENERICOCUENTAS';
  const$CodFunGenericoDivisa    CONSTANT tipo$desc_ := 'GENERICODIVISA';
  const$CodFunGenericoTermino   CONSTANT tipo$desc_ := 'GENERICOTERMINO';
  const$CodFunGenericoFechaHora CONSTANT tipo$desc_ := 'GENERICOFECHAHORA';
  const$CodFunModiMovimientos   CONSTANT tipo$desc_ := 'MODIFICARMOVIMIENTOS';
  const$CodFunConfigurarRoles   CONSTANT tipo$desc_ := 'CONFIGURARROLES';
  const$CodFunConfigurarUsers   CONSTANT tipo$desc_ := 'CONFIGURARUSUARIOS';
  const$CodFunConfigurarAcuerdo CONSTANT tipo$desc_ := 'CONFIGURARACUERDOSSERVICIO';
  const$CodFunRecuperarSalAcuer CONSTANT tipo$desc_ := 'RECUPERARSALDOSACUERDO';
  const$CodFunRecMaesPlanificad CONSTANT tipo$desc_ := 'RECUPERARMAESPLANIFICADAS';


  const$CodCanalSaldoFisico     CONSTANT tipo$desc_ := 'SF';
  const$CodCanalSaldo           CONSTANT tipo$desc_ := 'SAL';
  const$CodSubCanalSaldoFisico  CONSTANT tipo$desc_ := 'SF';
  const$CodSubCanalAcreditacion CONSTANT tipo$desc_ := 'ACR';
  const$CodSubCanalComision     CONSTANT tipo$desc_ := 'COM';
  const$CodSubCanalTesoreria    CONSTANT tipo$desc_ := 'TES';

  const$CodDelegacionCentral    CONSTANT tipo$desc_ := 'CENTRAL';
  const$CodPlantaCentral        CONSTANT tipo$desc_ := 'CENTRAL';
  const$CodSectorBoveda         CONSTANT tipo$desc_ := 'BOVEDA';

  const$CodClienteProsegur      CONSTANT tipo$desc_ := '999999';
  const$CodSubClienteProsegur   CONSTANT tipo$desc_ := '999999-9999';
  const$CodPtoServicioProsegur  CONSTANT tipo$desc_ := '999999-9999-99';

  /* Codigo Aplicacion */
  const$codAplicacionSaldos  CONSTANT tipo$cod_ := 'GenesisSaldos';
  const$codAplicacionGenesis CONSTANT tipo$cod_ := 'Genesis';

  /* Codigo Expresion */
  const$codExpAtributoObrig CONSTANT tipo$desc_ := 'MsgAtributoObrigatorio';
  const$codExpBultoSalidaRec CONSTANT tipo$desc_ := '082_bulto_relacionado_salidas_recorrido';
  const$codExpBultoSecDistin CONSTANT tipo$desc_ := '082_puesto_logrado_dicferente_del_bulto';

  /* Codigo de Error */
  const$codCustomError CONSTANT tipo$nel_ := -20001;

  -- Estado Documento
  const$CodEstadoDocNuevo      CONSTANT tipo$cod_ := 'NU';
  const$CodEstadoDocEnCurso    CONSTANT tipo$cod_ := 'EC';
  const$CodEstadoDocConfirmado CONSTANT tipo$cod_ := 'CF';
  const$CodEstadoDocAnulado    CONSTANT tipo$cod_ := 'AN';
  const$CodEstadoDocAceptado   CONSTANT tipo$cod_ := 'AC';
  const$CodEstadoDocRechazado  CONSTANT tipo$cod_ := 'RC';
  const$CodEstadoDocSustituido CONSTANT tipo$cod_ := 'SU';
  const$CodEstadoDocEliminado  CONSTANT tipo$cod_ := 'EE';

  -- Estado Remesa
  const$codEstadoRemesaPendiente CONSTANT tipo$cod_ := 'PE';
  const$codEstadoRemesaProcesado CONSTANT tipo$cod_ := 'PR';
  const$codEstadoRemesaAnulado   CONSTANT tipo$cod_ := 'AN';
  const$codEstadoRemesaEliminado CONSTANT tipo$cod_ := 'EE';
  const$codEstadoRemesaAsignada  CONSTANT tipo$cod_ := 'AS';
  const$codEstadoRemesaEnCurso   CONSTANT tipo$cod_ := 'EC';
  const$codEstadoRemesaSustituid CONSTANT tipo$cod_ := 'SU';
  const$codEstadoRemesaTransito  CONSTANT tipo$cod_ := 'ET';

  -- Estado Bulto
  const$codEstadoBultoPendiente  CONSTANT tipo$cod_ := 'PE';
  const$codEstadoBultoNuevo      CONSTANT tipo$cod_ := 'NU';
  const$codEstadoBultoCerrado    CONSTANT tipo$cod_ := 'CE';
  const$codEstadoBultoAberto     CONSTANT tipo$cod_ := 'AB';
  const$codEstadoBultoAnulado    CONSTANT tipo$cod_ := 'AN';
  const$codEstadoBultoElminado   CONSTANT tipo$cod_ := 'EE';
  const$codEstadoBultoProcesado  CONSTANT tipo$cod_ := 'PR';
  const$codEstadoBultoEnCurso    CONSTANT tipo$cod_ := 'EC';
  const$codEstadoBultoAsignado   CONSTANT tipo$cod_ := 'AS';
  const$codEstadoBultoSustituido CONSTANT tipo$cod_ := 'SU';
  const$codEstadoBultoTransito   CONSTANT tipo$cod_ := 'ET';
  const$codEstadoBultoSalida     CONSTANT tipo$cod_ := 'SA';

  -- Estado Parcial
  const$codEstadoParcialNuevo    CONSTANT tipo$cod_ := 'NU';
  const$codEstadoParcialPendient CONSTANT tipo$cod_ := 'PE';
  const$codEstadoParcialProcesad CONSTANT tipo$cod_ := 'PR';
  const$codEstadoParcialAnulado  CONSTANT tipo$cod_ := 'AN';
  const$codEstadoParcialEliminad CONSTANT tipo$cod_ := 'EE';
  const$codEstadoParcialSalida   CONSTANT tipo$cod_ := 'SA';
  const$codEstadoParcialSustitui CONSTANT tipo$cod_ := 'SU';

  -- Estado Abono Elemento
  const$codEstAbonoEAbonado      CONSTANT tipo$cod_ := 'A';
  const$codEstAbonoENoAbonado    CONSTANT tipo$cod_ := 'NA';
  const$codEstAbonoEAbononadoDif CONSTANT tipo$cod_ := 'AD';
  const$codEstAbonoEParcAbonado  CONSTANT tipo$cod_ := 'PA';

  --Estado Contenedor
  const$CodEstadoContEnTransito CONSTANT tipo$cod_ := 'ET';
  const$CodEstadoConArmado      CONSTANT tipo$cod_ := 'AR';
  const$CodEstadoContDesarmado  CONSTANT tipo$cod_ := 'DR';
  const$CodEstadoContSalida     CONSTANT tipo$cod_ := 'SA';

  --Estado documento elemento
  const$CodEstadoDocEleTransito  CONSTANT tipo$cod_ := 'T';
  const$CodEstadoDocEleHistorico CONSTANT tipo$cod_ := 'H';
  const$CodEstadoDocEleConcluido CONSTANT tipo$cod_ := 'C';

  --Estado periodo
  const$CodEstadoPeriodoTransito  CONSTANT tipo$cod_ := 'AB';

  -- Codigos Parametros IAC.
  const$CodParamIACCrearConfNS  CONSTANT tipo$cod_ := 'CrearConfiguiracionNivelSaldo';
  const$CodParamIACCualquierTot CONSTANT tipo$cod_ := 'PermitirCualquierTotalizadorSaldoServicio';
  const$CodParamIACCompEnReglas CONSTANT tipo$cod_ := 'CodigoComprovanteBasadoEnReglas';
  const$CodParamIACFrmPaseAbono CONSTANT tipo$cod_ := 'FormularioPasesProcesoAbono';
  const$CodParamIACUsrAceptaDoc CONSTANT tipo$cod_ := 'PermitirUsuarioCreacionAcepteRechazeDoc';

  -- Codigos Tipo Cuneta
  const$codTipoCuentaAmbos CONSTANT tipo$cod_ := 'A';
  const$codTipoCuentaSaldo CONSTANT tipo$cod_ := 'S';
  const$codTipoCuentaMov   CONSTANT tipo$cod_ := 'M';

  -- Codigos Tipo Elemento
  const$codTipoEleContenedor CONSTANT tipo$cod_ := 'C';
  const$codTipoEleRemesa     CONSTANT tipo$cod_ := 'R';
  const$codTipoEleBulto      CONSTANT tipo$cod_ := 'B';
  const$codTipoEleParcial    CONSTANT tipo$cod_ := 'P';

  -- Codigos Lista Tipo
  const$codListaTipoTrabajo CONSTANT tipo$cod_ := '01';
  const$codListaTipoFormato CONSTANT tipo$cod_ := '05';

  -- Codigos tipo Contenedor
  const$codTipoContInd CONSTANT tipo$cod_ := 'INDISTINTO';

  -- Codigos tipo unidade de medida
  const$codTipoUniMedBillete   CONSTANT tipo$nel_ := 0;
  const$codTipoUniMedMoneda    CONSTANT tipo$nel_ := 1;
  const$codTipoUniMedMedioPago CONSTANT tipo$nel_ := 2;

  -- Codigos Calidad
  const$CodCalidadDeteriorado CONSTANT tipo$cod_ := 'D';

  -- Codigos Caracteristicas Formulurio
  const$CodCaracAceptAutomatic   CONSTANT tipo$cod_ := 'MOVIMIENTO_ACEPTACION_AUTOMATICA';
  const$CodCaracAlta             CONSTANT tipo$cod_ := 'ACCION_ALTAS';
  const$CodCaracBaja             CONSTANT tipo$cod_ := 'ACCION_BAJAS';
  const$CodCaracActa             CONSTANT tipo$cod_ := 'ACCION_ACTAS';
  const$CodCaracReenvio          CONSTANT tipo$cod_ := 'ACCION_REENVIOS';
  const$CodCaracReenvioAuto      CONSTANT tipo$cod_ := 'REENVIO_AUTOMATICO';
  const$CodCaracGestionContd     CONSTANT tipo$cod_ := 'CARACTERISTICA_PRINCIPAL_GESTION_CONTENEDORES';
  const$CodCaracDocIndividual    CONSTANT tipo$cod_ := 'DOCUMENTO_INDIVIDUAL';
  const$CodCaracDocGrupo         CONSTANT tipo$cod_ := 'DOCUMENTO_AGRUPADOR';
  const$CodCaracPackModular      CONSTANT tipo$cod_ := 'PACK_MODULAR';
  const$CodCaracCierreCaja       CONSTANT tipo$cod_ := 'CIERRE_CAJA';
  const$CodCaracCierretesoro     CONSTANT tipo$cod_ := 'CIERRE_TESORO';
  const$CodCaracGestionRemes     CONSTANT tipo$cod_ := 'CARACTERISTICA_PRINCIPAL_GESTION_REMESAS';
  const$CodCaracGestionBulto     CONSTANT tipo$cod_ := 'CARACTERISTICA_PRINCIPAL_GESTION_BULTOS';
  const$CodCaracAccionSustit     CONSTANT tipo$cod_ := 'ACCION_SUSTITUCION';
  const$CodCaracPerSaldoNeg      CONSTANT tipo$cod_ := 'PERMITE_LLEGAR_SALDO_NEGATIVO';
  const$CodCaracSalidaRecor      CONSTANT tipo$cod_ := 'SALIDAS_RECORRIDO';
  const$CodCaracIntegrSalid      CONSTANT tipo$cod_ := 'INTEGRACION_SALIDAS';
  const$CodCaracClasificacion    CONSTANT tipo$cod_ := 'ACCION_CLASIFICACION';
  const$CodCaracDesarmarCont     CONSTANT tipo$cod_ := 'DESARMAR_CONTENEDORES';
  const$CodCaracReenvSecMismaPla CONSTANT tipo$cod_ := 'ENTRE_SECTORES_MISMA_PLANTA';
  const$CodCaracReenvSecDistPla  CONSTANT tipo$cod_ := 'ENTRE_SECTORES_PLANTAS_DIFERENTES';
  const$CodCaracReenvioClientes  CONSTANT tipo$cod_ := 'ENTRE_CLIENTES';
  const$CodCaracGestionFondos    CONSTANT tipo$cod_ := 'CARACTERISTICA_PRINCIPAL_GESTION_FONDOS';
  const$CodCaracMovFondos        CONSTANT tipo$cod_ := 'ACCION_MOVIMIENTO_FONDOS';
  const$CodCaracEntreCanales     CONSTANT tipo$cod_ := 'ENTRE_CANALES';

  /*Codigos Formularios*/
  const$cod_form_move_cont_sect constant tipo$desc_ := 'CodFormularioMoverContPMEntreSectores';

  const$CodRelConFormOri  CONSTANT tipo$cod_ := 'O';
  const$CodRelConFormDest CONSTANT tipo$cod_ := 'D';

  /* Enum TipoServicio */
  const$CodTipoServProcesar      CONSTANT tipo$cod_ := '01';
  const$CodTipoServClasificacion CONSTANT tipo$cod_ := '02';
  const$CodTipoServPreparacion   CONSTANT tipo$cod_ := '03';
  const$CodTipoServATM           CONSTANT tipo$cod_ := '04';
  const$CodTipoServAlmacen       CONSTANT tipo$cod_ := '05';
  const$CodTipoServPernoche      CONSTANT tipo$cod_ := '06';

  /* Tipo Nivel Detalle */
  const$cod_nivel_det_detallado CONSTANT tipo$cod_ := 'D';
  const$cod_nivel_det_total     CONSTANT tipo$cod_ := 'T';
  const$cod_nivel_det_geral     CONSTANT tipo$cod_ := 'G';

  /* Origen Busqueda */
  const$codOriBusquedaEnviado   constant tipo$cod_ := 'E';
  const$codOriBusquedaRecebidos constant tipo$cod_ := 'R';
  const$codOriBusquedaAmbos constant tipo$cod_ := 'A';

  /* Termino Recibo Deteriorado */
  const$codTerminoRecDeteriorado   constant tipo$cod_ := 'recDeteriorado';
  const$codTerminoCodDocumento   constant tipo$cod_ := 'codigoDocumento';
  const$codTerminoSerieDocumento   constant tipo$cod_ := 'serieCodigoDocumento';

  const$CodTipoSectorMae CONSTANT tipo$cod_ := 'MAETC';

  const$nomeTableSector CONSTANT tipo$cod_ := 'GEPR_TSECTOR';
  const$identificadorAjenoMAE CONSTANT tipo$cod_ := 'MAE';

  -- Codigos de resultado de mensajes
  const$codTipoExito            CONSTANT tipo$nel_ := 0;
  const$codTipoAlerta           CONSTANT tipo$nel_ := 1;
  const$codTipoErrorNegocio     CONSTANT tipo$nel_ := 2;
  const$codTipoErrorAplicacion  CONSTANT tipo$nel_ := 3;

END gepr_pcomon_###VERSION###;
/
CREATE OR REPLACE PACKAGE body gepr_pcomon_###VERSION### AS

END gepr_pcomon_###VERSION###;
/