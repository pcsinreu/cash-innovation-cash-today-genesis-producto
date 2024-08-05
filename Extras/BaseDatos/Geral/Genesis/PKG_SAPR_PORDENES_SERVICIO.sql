create or replace PACKAGE SAPR_PORDENES_SERVICIO_###VERSION### AS
  /* Version: ###VERSION_COMP### */
  const$vacio CONSTANT               gepr_pcomon_###VERSION###.tipo$desc_ := '###VACIO###';

/* Devuelve la información almacenada en SAPR_TDATO_BANCARIO_CAMBIO */
PROCEDURE srecuperar_ordenes(
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /* ---- FILTROS ---- */
                            /*Clientes*/
                            par$aoid_cliente            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_subcliente         IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_pto_servicio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Contrato*/
                            par$contrato          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Orden Servicio*/
                            par$orden_servicio          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Productos*/
                            par$acod_producto              IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Fecha inicio*/
                            par$fecha_inicio             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Fecha fin*/            
                            par$fecha_fin            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Estado*/
                            par$estado             IN gepr_pcomon_###VERSION###.tipo$nel_,
                            /* ---- FIN FILTROS ---- */
                            par$rc_ordenes                OUT sys_refcursor);
  PROCEDURE srecuperar_detalles (
    par$oid_acuerdo_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$product_code            IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$rc_detalles  OUT sys_refcursor);
  
  PROCEDURE srecuperar_notificaciones (
    par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_notificaciones  OUT sys_refcursor);

  PROCEDURE srecuperar_notificaciones_det (
    par$oid_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_notificaciones_det  OUT sys_refcursor);

END SAPR_PORDENES_SERVICIO_###VERSION###;
/
create or replace PACKAGE BODY SAPR_PORDENES_SERVICIO_###VERSION### AS
  
/* Devuelve la información almacenada en SAPR_TDATO_BANCARIO_CAMBIO */
PROCEDURE srecuperar_ordenes(
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /* ---- FILTROS ---- */
                            /*Clientes*/
                            par$aoid_cliente            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_subcliente         IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_pto_servicio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Contrato*/
                            par$contrato          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Orden Servicio*/
                            par$orden_servicio          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Productos*/
                            par$acod_producto              IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Fecha inicio*/
                            par$fecha_inicio             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Fecha fin*/            
                            par$fecha_fin            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Estado*/
                            par$estado             IN gepr_pcomon_###VERSION###.tipo$nel_,
                            /* ---- FIN FILTROS ---- */
                            par$rc_ordenes                OUT sys_refcursor) IS
    const$new_line                    CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
    const$comilla_simple              CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);

    var$bol_filtra_cliente gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_subcliente gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_punto gepr_pcomon_###VERSION###.tipo$nel_;

    var$bol_filtra_contrato gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_ordenservicio gepr_pcomon_###VERSION###.tipo$nel_;

    var$bol_filtra_fechainicio gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_fechafin gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_producto gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_estado gepr_pcomon_###VERSION###.tipo$nel_;

    var$qry_select gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_where gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_final gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_joins gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_insert gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_filtro_cliente      gepr_pcomon_###VERSION###.tipo$obs_;

  BEGIN     
    var$bol_filtra_cliente := 0;
    var$bol_filtra_subcliente := 0;
    var$bol_filtra_punto := 0;
    var$bol_filtra_contrato := 0;
    var$bol_filtra_ordenservicio := 0;
    var$bol_filtra_fechainicio := 0;
    var$bol_filtra_fechafin := 0;
    var$bol_filtra_producto := 0;
    var$bol_filtra_estado := 0;

    var$qry_select := '';
    var$qry_where := '';
    var$qry_final := '';
    var$qry_joins := '';
    var$qry_insert := '';
    var$qry_filtro_cliente := '';

    DELETE  FROM SAPR_GTT_TAUXILIAR
    WHERE 
        COD_CALIFICADOR IN 
        (
            'ORDENES_SERVICIO',
            'OS_OID_CLIENTE',
            'OS_OID_SUBCLIENTE',
            'OS_OID_PTO_SERVICIO',
            'OS_OID_PRODUCTOS'
        );

    DBMS_OUTPUT.PUT_LINE('Recupera Ordenes Servicio - Filtro Cliente');
    /*Cargo en la tabla auxiliar los datos de filtro de clientes*/
    IF par$aoid_cliente IS NOT NULL AND par$aoid_cliente.COUNT > 0 THEN
        FOR idx in par$aoid_cliente.first .. par$aoid_cliente.last LOOP
            IF par$aoid_cliente(idx) is not null OR par$aoid_cliente(idx) <> '' THEN
                var$bol_filtra_cliente := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                VALUES (par$aoid_cliente(idx), 'OS_OID_CLIENTE');
            END IF;
        END LOOP;
    END IF;
    DBMS_OUTPUT.PUT_LINE('Recupera Ordenes Servicio - Filtro SubCliente');

    IF par$aoid_subcliente IS NOT NULL AND par$aoid_subcliente.COUNT > 0 THEN             
        FOR idx in par$aoid_subcliente.first .. par$aoid_subcliente.last LOOP
            IF par$aoid_subcliente(idx) is not null OR par$aoid_subcliente(idx) <> '' THEN
                    var$bol_filtra_subcliente:=1;
                    INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (par$aoid_subcliente(idx), 'OS_OID_SUBCLIENTE');
            END IF;
        END LOOP;
    END IF;            

    DBMS_OUTPUT.PUT_LINE('Recupera Ordenes Servicio - Filtro Punto Servicio');
    IF par$aoid_pto_servicio IS NOT NULL AND par$aoid_pto_servicio.COUNT > 0 THEN    
        FOR idx in par$aoid_pto_servicio.first .. par$aoid_pto_servicio.last LOOP
            IF par$aoid_pto_servicio(idx) is not null or par$aoid_pto_servicio(idx) <> '' THEN
                    var$bol_filtra_punto := 1;
                    INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (par$aoid_pto_servicio(idx), 'OS_OID_PTO_SERVICIO');
                END IF;
        END LOOP;
    END IF;

    DBMS_OUTPUT.PUT_LINE('Recupera Ordenes Servicio - Filtro Productos');
    IF par$acod_producto  IS NOT NULL AND par$acod_producto.COUNT > 0 THEN
        FOR idx IN par$acod_producto.first .. par$acod_producto.last  LOOP
            IF par$acod_producto(idx) is not null OR par$acod_producto(idx) <> '' THEN
                var$bol_filtra_producto := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                VALUES (par$acod_producto(idx), 'OS_OID_PRODUCTOS');
            END IF;          
        END LOOP;
    END IF;


    /* Query INSERT */
    var$qry_insert := 'INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1,OID_CAMPO7,COD_CALIFICADOR)' || const$new_line;
    DBMS_OUTPUT.PUT_LINE('Recupera Ordenes Servicio - Filtros Aplicados');
    /* Query principal */
    var$qry_select := 'SELECT DISTINCT' || const$new_line;
    var$qry_select := var$qry_select || ' ASE.OID_ACUERDO_SERVICIO, SAR.OID_SALDO_ACUERDO_REF, ' || const$comilla_simple || 'ORDENES_SERVICIO' || const$comilla_simple || const$new_line;
    var$qry_select := var$qry_select || ' FROM' || const$new_line;
    var$qry_select := var$qry_select || '   SAPR_TACUERDO_SERVICIO ASE ' || const$new_line;

    /* -------- Inicio query joins -------- */

    var$qry_joins := var$qry_joins || 'LEFT JOIN SAPR_TSALDO_ACUERDO_REF SAR ON SAR.OID_ACUERDO_SERVICIO = ASE.OID_ACUERDO_SERVICIO' || const$new_line;

    IF var$bol_filtra_producto = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_PRODUCTO ON FILTRO_PRODUCTO.OID_CAMPO1 = ASE.DES_PRODUCT_CODE AND FILTRO_PRODUCTO.COD_CALIFICADOR = ' || const$comilla_simple || 'OS_OID_PRODUCTOS' ||  const$comilla_simple || const$new_line;
    END IF;

    IF var$bol_filtra_cliente = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_CLIENTE ON FILTRO_CLIENTE.OID_CAMPO1 = ASE.OID_CLIENTE AND FILTRO_CLIENTE.COD_CALIFICADOR = ' || const$comilla_simple || 'OS_OID_CLIENTE' ||  const$comilla_simple || const$new_line;
    END IF;

    IF var$bol_filtra_subcliente = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_SUBCLI ON FILTRO_SUBCLI.OID_CAMPO1 = ASE.OID_SUBCLIENTE AND FILTRO_SUBCLI.COD_CALIFICADOR = ' || const$comilla_simple || 'OS_OID_SUBCLIENTE' || const$comilla_simple || const$new_line;
    END IF; 

    IF var$bol_filtra_punto = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_PUNTO ON FILTRO_PUNTO.OID_CAMPO1 = ASE.OID_PTO_SERVICIO AND FILTRO_PUNTO.COD_CALIFICADOR = ' || const$comilla_simple || 'OS_OID_PTO_SERVICIO' || const$comilla_simple || const$new_line;
    END IF;

    /* -------- Fin query joins -------- */

    /* Query WHERE */
    var$qry_where := var$qry_where || 'WHERE' || const$new_line;
    var$qry_where := var$qry_where || ' 1 = 1' || const$new_line;
    var$qry_where := var$qry_where || ' AND SAR.BOL_CALCULADO = '|| TO_CHAR(par$estado) || ' ' || const$new_line;
    IF par$contrato IS NOT NULL THEN
        var$qry_where := var$qry_where || ' AND ASE.DES_CONTRACT_ID = '  ||  const$comilla_simple || par$contrato || const$comilla_simple || const$new_line;
    END IF;
    IF par$orden_servicio IS NOT NULL THEN
        var$qry_where := var$qry_where || ' AND ASE.DES_SERVICE_ORDER_CODE = '  ||  const$comilla_simple || par$orden_servicio || const$comilla_simple || const$new_line;
    END IF;

    /*Filtros de fecha - INICIO*/
    IF par$fecha_inicio IS NOT NULL OR par$fecha_fin IS NOT NULL THEN
        /*filtro por Fecha de Inicio*/
        IF par$fecha_inicio IS NOT NULL AND par$fecha_fin IS NULL THEN
            var$qry_where := var$qry_where || ' AND SAR.FEC_SALDO >= TO_DATE('  ||  const$comilla_simple || SUBSTR(TO_CHAR(par$fecha_inicio, 'DD/MM/YYYY HH24:MI:SS'),0,10) || const$comilla_simple || ', ''DD/MM/YYYY'') ';
        END IF;
    /*filtro por Fecha de Fin*/
        IF par$fecha_inicio IS NULL AND par$fecha_fin IS NOT NULL THEN
            /*Filtro fecha hasta*/
                var$qry_where := var$qry_where || ' AND SAR.FEC_SALDO <= TO_DATE(' || const$comilla_simple || SUBSTR(TO_CHAR(par$fecha_fin, 'DD/MM/YYYY HH24:MI:SS'),0,10)  || const$comilla_simple || ', ''DD/MM/YYYY'') ';
        END IF;

       /*Tipo de filtro por Fecha de Inicio y Fin*/
        IF par$fecha_inicio IS NOT NULL AND par$fecha_fin IS NOT NULL THEN
            /*Filtro fecha desde*/
            var$qry_where := var$qry_where || ' AND (SAR.FEC_SALDO >= TO_DATE('  ||  const$comilla_simple || SUBSTR(TO_CHAR(par$fecha_inicio, 'DD/MM/YYYY HH24:MI:SS'),0,10) || const$comilla_simple || ', ''DD/MM/YYYY'') ';

            var$qry_where := var$qry_where || ' AND SAR.FEC_SALDO <= TO_DATE(' || const$comilla_simple || SUBSTR(TO_CHAR(par$fecha_fin, 'DD/MM/YYYY HH24:MI:SS'),0,10)  || const$comilla_simple || ', ''DD/MM/YYYY'') )';
        END IF;
    END IF;
    /*Filtros de fecha - FIN*/


    IF var$bol_filtra_cliente = 1 THEN
        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ ASE.OID_CLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'OS_OID_CLIENTE' ) ]' || const$new_line;
    END IF;


    IF var$bol_filtra_subcliente = 1 THEN
        IF var$qry_filtro_cliente IS NOT NULL THEN
            var$qry_filtro_cliente := var$qry_filtro_cliente || ' OR '  ;
        END IF; 

        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ ASE.OID_SUBCLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'OS_OID_SUBCLIENTE' ) ]' || const$new_line;        
    END IF;


    IF var$bol_filtra_punto = 1 THEN
        IF var$qry_filtro_cliente IS NOT NULL THEN
            var$qry_filtro_cliente := var$qry_filtro_cliente || ' OR '  ;
        END IF; 

        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ ASE.OID_PTO_SERVICIO IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'OS_OID_PTO_SERVICIO' ) ]' || const$new_line;        
    END IF;


    IF var$qry_filtro_cliente IS NOT NULL THEN
        var$qry_filtro_cliente := ' AND ( '  || var$qry_filtro_cliente || ' ) '  ;
        var$qry_where := var$qry_where ||  var$qry_filtro_cliente;
    END IF; 

    var$qry_final := var$qry_insert || var$qry_select || var$qry_joins || var$qry_where;

    dbms_output.put_line('Recupera Ordenes Servicio - Imprimo query final:');
    dbms_output.put_line(var$qry_final);

    /*Ejecutamos la query final */
    EXECUTE IMMEDIATE var$qry_final;

dbms_output.put_line('Recupera Datos - Ejecutó query final');

      OPEN par$rc_ordenes FOR
            SELECT 
            ASE.OID_ACUERDO_SERVICIO AS OID_ACUERDO_SERVICIO,
            CLIE.COD_CLIENTE || ' - ' ||  CLIE.DES_CLIENTE AS CLIENTE,
            SCLIE.COD_SUBCLIENTE || ' - ' ||  SCLIE.DES_SUBCLIENTE AS SUBCLIENTE,
            PTO.COD_PTO_SERVICIO || ' - ' ||  PTO.DES_PTO_SERVICIO AS PUNTO,
            ASE.DES_CONTRACT_ID AS CONTRATO,
            ASE.DES_SERVICE_ORDER_CODE AS ORDENSERVICIO,
            ASE.DES_PRODUCT_CODE, 
            CASE ASE.DES_PRODUCT_CODE 
                WHEN 'PR00117'
                THEN 'PR00117 - Fecha Valor'
                WHEN 'PR00160'
                THEN 'PR00160 - Transacciones'
            END PRODUCTO,
            SAR.FEC_SALDO AS FECHAREFERENCIA,
            CASE SAR.BOL_CALCULADO
                WHEN 1
                THEN SAR.GMT_MODIFICACION
                ELSE NULL
              END FECHACALCULO,
            CASE SAR.BOL_CALCULADO
                WHEN 1
                THEN 'Calculado'
                ELSE 'No Calculado'
              END ESTADO,
             SAR.OID_SALDO_ACUERDO_REF AS OID_SALDO_ACUERDO_REF 
            FROM SAPR_GTT_TAUXILIAR AUX
            INNER JOIN SAPR_TACUERDO_SERVICIO ASE ON ASE.OID_ACUERDO_SERVICIO = AUX.OID_CAMPO1 
            LEFT JOIN GEPR_TCLIENTE CLIE ON CLIE.OID_CLIENTE = ASE.OID_CLIENTE
            LEFT JOIN GEPR_TSUBCLIENTE SCLIE ON SCLIE.OID_SUBCLIENTE = ASE.OID_SUBCLIENTE
            LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = ASE.OID_PTO_SERVICIO
            LEFT JOIN SAPR_TSALDO_ACUERDO_REF SAR ON SAR.OID_ACUERDO_SERVICIO = ASE.OID_ACUERDO_SERVICIO
            AND SAR.OID_SALDO_ACUERDO_REF = AUX.OID_CAMPO7
            WHERE AUX.COD_CALIFICADOR='ORDENES_SERVICIO'
            ORDER BY SAR.FEC_SALDO;

  END srecuperar_ordenes;

 PROCEDURE srecuperar_detalles
(
    par$oid_acuerdo_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$product_code        IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$rc_detalles  OUT sys_refcursor
)
IS

BEGIN

IF  par$product_code = 'PR00160' THEN 
      OPEN par$rc_detalles FOR
        SELECT
            --oid_saldo_acuerdo,
            'PR00160-CASHIN' AS TIPO,
            nel_cant_cashin AS CANTIDAD,
            DIV.cod_iso_divisa AS DIVISA,
            CASE SAC.cod_tipo_mercancia
                WHEN '0'
                THEN 'EFECTIVO'
                WHEN '1'
                THEN 'BILLETE'
                WHEN '2'
                THEN 'MONEDA'
            END AS TIPOMERCANCIA,
            num_total_cashin AS TOTAL
        FROM 
            sapr_tsaldo_acuerdo SAC
            inner JOIN GEPR_TDIVISA DIV on DIV.oid_divisa = SAC.oid_divisa
        WHERE SAC.oid_acuerdo_servicio = par$oid_acuerdo_servicio
        and SAC.oid_saldo_acuerdo_ref = par$oid_saldo_acuerdo_ref
        UNION
        SELECT
            --oid_saldo_acuerdo,
            'PR00160-SHIPOUT' AS TIPO,
            nel_cant_shipout AS CANTIDAD,
            DIV.cod_iso_divisa AS DIVISA,
            CASE SAC.cod_tipo_mercancia
                WHEN '0'
                THEN 'EFECTIVO'
                WHEN '1'
                THEN 'BILLETE'
                WHEN '2'
                THEN 'MONEDA'
            END AS TIPOMERCANCIA,
            num_total_shipout AS TOTAL
        FROM
            sapr_tsaldo_acuerdo SAC
            inner JOIN GEPR_TDIVISA DIV on DIV.oid_divisa = SAC.oid_divisa
        WHERE SAC.oid_acuerdo_servicio = par$oid_acuerdo_servicio
        and SAC.oid_saldo_acuerdo_ref = par$oid_saldo_acuerdo_ref;
ELSIF par$product_code = 'PR00117' THEN
      OPEN par$rc_detalles FOR
        SELECT
            --oid_saldo_acuerdo,
            'PR00117-ACREDITACION' AS TIPO,
            nel_cant_acreditacion AS CANTIDAD,
            DIV.cod_iso_divisa AS DIVISA,
            CASE SAC.cod_tipo_mercancia
                WHEN '0'
                THEN 'EFECTIVO'
                WHEN '1'
                THEN 'BILLETE'
                WHEN '2'
                THEN 'MONEDA'
            END AS TIPOMERCANCIA,
            num_total_acreditacion AS TOTAL
        FROM 
            sapr_tsaldo_acuerdo SAC
            inner JOIN GEPR_TDIVISA DIV on DIV.oid_divisa = SAC.oid_divisa
        WHERE SAC.oid_acuerdo_servicio = par$oid_acuerdo_servicio
        and SAC.oid_saldo_acuerdo_ref = par$oid_saldo_acuerdo_ref
        UNION
        SELECT
            --oid_saldo_acuerdo,
            'PR00117-CASHIN' AS TIPO,
            nel_cant_cashin_acred AS CANTIDAD,
            DIV.cod_iso_divisa AS DIVISA,
            CASE SAC.cod_tipo_mercancia
                WHEN '0'
                THEN 'EFECTIVO'
                WHEN '1'
                THEN 'BILLETE'
                WHEN '2'
                THEN 'MONEDA'
            END AS TIPOMERCANCIA,
            num_total_cashin_acred AS TOTAL
        FROM
            sapr_tsaldo_acuerdo SAC
            inner JOIN GEPR_TDIVISA DIV on DIV.oid_divisa = SAC.oid_divisa
        WHERE SAC.oid_acuerdo_servicio = par$oid_acuerdo_servicio
        and SAC.oid_saldo_acuerdo_ref = par$oid_saldo_acuerdo_ref
        UNION
        SELECT
            --oid_saldo_acuerdo,
            'PR00117-COMISIÓN' AS TIPO,
            nel_cant_acreditacion AS CANTIDAD,
            DIV.cod_iso_divisa AS DIVISA,
            CASE SAC.cod_tipo_mercancia
                WHEN '0'
                THEN 'EFECTIVO'
                WHEN '1'
                THEN 'BILLETE'
                WHEN '2'
                THEN 'MONEDA'
            END AS TIPOMERCANCIA,
            num_total_comision AS TOTAL
        FROM
            sapr_tsaldo_acuerdo SAC
            inner JOIN GEPR_TDIVISA DIV on DIV.oid_divisa = SAC.oid_divisa
        WHERE SAC.oid_acuerdo_servicio = par$oid_acuerdo_servicio
        and SAC.oid_saldo_acuerdo_ref = par$oid_saldo_acuerdo_ref;
END IF;


END srecuperar_detalles;

 PROCEDURE srecuperar_notificaciones
(
    par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_notificaciones  OUT sys_refcursor
)
IS

BEGIN

    OPEN par$rc_notificaciones FOR
        WITH INTE AS (
            SELECT
                TINT.oid_integracion AS OIDINTEGRACION,
                TINT.gmt_creacion AS FECHA,
                TINT.cod_estado AS ESTADO,
                TINT.nel_intento_envio AS INTENTOS,
                DET.FYH_CREACION AS FECHA_DETALLE,
                CASE DET.BOL_ERROR
                    WHEN 0
                    THEN ''
                    WHEN 1
                    THEN CAST(DET.DES_COMENTARIO AS VARCHAR2(4000))
                END AS ULTIMOERROR,
                SAR.OID_SALDO_ACUERDO_REF AS OIDSALDOACUERDOREF
            FROM
                gepr_tintegracion TINT
            INNER JOIN SAPR_TSALDO_ACUERDO_REF SAR ON SAR.OID_SALDO_ACUERDO_REF= TINT.OID_TABLA_INTEGRACION
            LEFT JOIN GEPR_TINTEGRACION_DETALLE DET ON DET.oid_integracion = TINT.oid_integracion
            WHERE TINT.COD_TABLA_INTEGRACION = 'SAPR_TSALDO_ACUERDO_REF'
            AND TINT.COD_MODULO_DESTINO = 'API_GLOBAL'
            AND TINT.COD_PROCESO = 'Notificacion'
            AND SAR.OID_SALDO_ACUERDO_REF = par$oid_saldo_acuerdo_ref
            ),
            MAX_FECHA AS (
            SELECT MAX(FECHA_DETALLE) AS FECHA_DETALLE FROM INTE
            )
            SELECT * FROM INTE
            INNER JOIN MAX_FECHA MF ON MF.FECHA_DETALLE = INTE.FECHA_DETALLE;

END srecuperar_notificaciones;

 PROCEDURE srecuperar_notificaciones_det
(
    par$oid_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_notificaciones_det  OUT sys_refcursor
)
IS

BEGIN

      OPEN par$rc_notificaciones_det FOR
        SELECT
            DET.OID_INTEGRACION AS OIDINTEGRACION,
            DET.NEL_REINTENTOS AS NUMERODEINTENTO,
            DET.FYH_CREACION FECHA,
            DET.COD_ESTADO AS ESTADO,
            DET.DES_COMENTARIO AS OBSERVACIONES,
            DET.BOL_ERROR AS ERROR
        FROM GEPR_TINTEGRACION_DETALLE DET
        WHERE DET.OID_INTEGRACION = par$oid_integracion
        ORDER BY DET.FYH_CREACION DESC; 

END srecuperar_notificaciones_det;

END SAPR_PORDENES_SERVICIO_###VERSION###;
/