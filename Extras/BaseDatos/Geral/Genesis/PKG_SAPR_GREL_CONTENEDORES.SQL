create or replace package SAPR_GREL_CONTENEDORES_###VERSION### as
  /*Version: ###VERSION_COMP###*/

  procedure SP_CONTENEDORES(par$codTipoContenedor    varchar2,
                            par$codEstadoContenedor  varchar2,
                            par$fechaHoraArmadoDesde date,
                            par$fechaHoraArmadoHasta date,
                            par$packModular          number,
                            par$mayorNivel           number,
                            par$codPrecinto          varchar2,
                            par$codSector            varchar2,
                            par$codCliente           varchar2,
                            par$codSubCliente        varchar2,
                            par$codPuntoDeServicio   varchar2,
                            par$codCanal             varchar2,
                            par$SubCodCanal          varchar2,
                            par$cursor               out sys_refcursor);
							 
  procedure SP_CONTENEDORES_VENCIDOS(par$codTipoContenedor  varchar2,
                                     par$fechaVencimentoDesde    date,
                                     par$fechaVencimentoHasta    date,
                                     par$mayorNivel         number,
                                     par$codSector          varchar2,
                                     par$codCliente         varchar2,
                                     par$codSubCliente      varchar2,
                                     par$codPuntoDeServicio varchar2,
                                     par$codCanal           varchar2,
                                     par$SubCodCanal        varchar2,
                                     par$cursor             out sys_refcursor);

  procedure SP_SEGUIMIENTO_ELEMENTO(par$TipoElemento varchar2,
                                    par$CodBusca     varchar2,
                                    par$cursor       out sys_refcursor);

  procedure SP_CONTENEDORESXCLIENTES(par$codEstadoContenedor varchar2,
                                     par$packModular         number,
                                     par$mayorNivel          number,
                                     par$codSector           varchar2,
                                     par$codCliente          varchar2,
                                     par$codSubCliente       varchar2,
                                     par$codPuntoDeServicio  varchar2,
                                     par$codCanal            varchar2,
                                     par$SubCodCanal         varchar2,
                                     par$divisas             varchar2,
                                     par$cursor              out sys_refcursor);

  procedure SP_INVENTARIO_CONTENEDOR(par$codInventario varchar2,
                                     par$cursor        out sys_refcursor);

end SAPR_GREL_CONTENEDORES_###VERSION###;
/
create or replace package body SAPR_GREL_CONTENEDORES_###VERSION### as
    procedure SP_CONTENEDORES(par$codTipoContenedor    varchar2,
                            par$codEstadoContenedor  varchar2,
                            par$fechaHoraArmadoDesde date,
                            par$fechaHoraArmadoHasta date,
                            par$packModular          number,
                            par$mayorNivel           number,
                            par$codPrecinto          varchar2,
                            par$codSector            varchar2,
                            par$codCliente           varchar2,
                            par$codSubCliente        varchar2,
                            par$codPuntoDeServicio   varchar2,
                            par$codCanal             varchar2,
                            par$SubCodCanal          varchar2,
                            par$cursor               out sys_refcursor) as
  
  begin
    open par$cursor for
      WITH CONTENEDOR AS (
            SELECT DISTINCT (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = CO.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS,
                      CO.COD_ESTADO AS ESTADO,
                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = CO.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS CONTENEDOR_PADRE,
                      (CASE
                        WHEN CU.DES_CLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE
                        WHEN CU.DES_SUBCLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE || ' | ' ||
                         CU.DES_SUBCLIENTE
                        WHEN CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE ||
                             ' | ' || CU.DES_SUBCLIENTE || ' | ' ||
                             CU.DES_PTO_SERVICIO IS NOT NULL THEN
                         ' | ' || CU.DES_PTO_SERVICIO
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NULL THEN
                         NULL
                      END) AS CLIENTE,
                      CU.COD_CANAL || ' ' || CU.DES_CANAL || ' | ' ||
                      CU.DES_SUBCANAL AS CANAL,
                      FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION, CO.GMT_CREACION) AS FECHA_ARMADO,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      PSC.COD_POSICION AS POSICION,
                      D.COD_SIMBOLO AS DIVISA,
                      DE.NUM_VALOR AS VALOR_DIVISA,
                      EF.NUM_IMPORTE AS IMPORTE,
                      CASE WHEN TC.NEC_CANTIDAD IS NULL THEN 1 ELSE 0 END ES_PICO
        FROM SAPR_TCONTENEDOR CO
        LEFT JOIN SAPR_TPRECINTOXCONTENEDOR SPRE
          ON SPRE.OID_CONTENEDOR = CO.OID_CONTENEDOR
        INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON CO.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        INNER JOIN GEPR_TSECTOR SEC
          ON SEC.OID_SECTOR = CO.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLA
          ON SEC.OID_PLANTA = PLA.OID_PLANTA
        LEFT JOIN SAPR_TPOSICION_SECTOR PSC
          ON PSC.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_VCUENTA CU
          ON CO.OID_CUENTA_MOVIMIENTO = CU.OID_CUENTA
        LEFT JOIN SAPR_TEFECTIVOXCONTENEDOR EF
          ON EF.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA D
          ON D.OID_DIVISA = EF.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DE
          ON DE.OID_DENOMINACION = EF.OID_DENOMINACION
         AND DE.OID_DIVISA = EF.OID_DIVISA
       WHERE (CO.OID_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ','))) OR
             (par$mayorNivel = 1 AND
             CO.OID_SECTOR IN
             (SELECT OID_SECTOR
                  FROM GEPR_TSECTOR
                 WHERE OID_SECTOR_PADRE = par$codSector)))
         AND (par$codCliente IS NULL OR CU.OID_CLIENTE = par$codCliente)
         AND (par$codSubCliente IS NULL OR
             CU.OID_SUBCLIENTE = par$codSubCliente)
         AND (par$codPuntoDeServicio IS NULL OR
             CU.OID_PTO_SERVICIO = par$codPuntoDeServicio)
         AND (par$codCanal IS NULL OR CU.OID_CANAL = par$codCanal)
         AND (par$SubCodCanal IS NULL OR CU.OID_SUBCANAL = par$SubCodCanal)
         AND (par$codTipoContenedor IS NULL OR
             CO.OID_TIPO_CONTENEDOR = par$codTipoContenedor)
         AND (CO.BOL_PACK_MODULAR = par$packModular)
         AND (CO.GMT_CREACION >= par$fechaHoraArmadoDesde AND
             CO.GMT_CREACION <= par$fechaHoraArmadoHasta)
         AND (par$codEstadoContenedor IS NULL OR
             CO.COD_ESTADO IN (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codEstadoContenedor, ','))))
         AND (par$codPrecinto IS NULL OR
             SPRE.COD_PRECINTO IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codPrecinto, ',')))))
         SELECT PRECINTOS,
                ESTADO,
                CONTENEDOR_PADRE,
                CLIENTE,
                CANAL,
                FECHA_ARMADO,
                TIPO_CONTENEDOR,
                POSICION,
                DIVISA,
                VALOR_DIVISA,
                IMPORTE
         FROM CONTENEDOR 
         WHERE ES_PICO = 0
         UNION
          SELECT PRECINTOS,
                ESTADO,
                CONTENEDOR_PADRE,
                CLIENTE,
                CANAL,
                FECHA_ARMADO,
                TIPO_CONTENEDOR,
                POSICION,
                DIVISA,
                NULL AS VALOR_DIVISA,
                SUM(IMPORTE)
         FROM CONTENEDOR 
         WHERE ES_PICO = 1
         GROUP BY PRECINTOS,
                ESTADO,
                CONTENEDOR_PADRE,
                CLIENTE,
                CANAL,
                FECHA_ARMADO,
                TIPO_CONTENEDOR,
                POSICION,
                DIVISA;
  
  end SP_CONTENEDORES;

  procedure SP_CONTENEDORES_VENCIDOS(par$codTipoContenedor  varchar2,
                                     par$fechaVencimentoDesde    date,
                                     par$fechaVencimentoHasta    date,
                                     par$mayorNivel         number,
                                     par$codSector          varchar2,
                                     par$codCliente         varchar2,
                                     par$codSubCliente      varchar2,
                                     par$codPuntoDeServicio varchar2,
                                     par$codCanal           varchar2,
                                     par$SubCodCanal        varchar2,
                                     par$cursor             out sys_refcursor) as

  begin

    open par$cursor for
WITH Q AS(SELECT DISTINCT (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = CO.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS,
                        CO.OID_CONTENEDOR,
                      SE.DES_SECTOR AS SECTOR,
                      (CASE
                        WHEN CU.DES_CLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE
                        WHEN CU.DES_SUBCLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE || ' | ' ||
                         CU.DES_SUBCLIENTE
                        WHEN CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE ||
                             ' | ' || CU.DES_SUBCLIENTE || ' | ' ||
                             CU.DES_PTO_SERVICIO IS NOT NULL THEN
                         ' | ' || CU.DES_PTO_SERVICIO
                      END) AS CLIENTE,
                      CU.COD_CANAL || ' ' || CU.DES_CANAL || ' | ' ||
                      CU.DES_SUBCANAL AS CANAL,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      D.COD_ISO_DIVISA AS DIVISA,
                      EF.NUM_IMPORTE AS IMPORTE,
                      PSC.COD_POSICION AS POSICION,
                      FN_GMT_DELEGACION_###VERSION###(CU.OID_DELEGACION, CO.GMT_CREACION) AS FECHA_ARMADO,
                      (SELECT to_number(to_char(to_date('1', 'J') +
                                                (SYSDATE - CO.GMT_CREACION),
                                                'J') - 1)
                         FROM DUAL) AS DIAS_ARMADO,
                      (SELECT COUNT(*)
                         FROM SAPR_TALERTA_VENC_CONTENEDOR CAL
                        WHERE CAL.OID_CONTENEDOR = CO.OID_CONTENEDOR) AS CANT_ALERTA,
                      (CASE WHEN SAL.GMT_CREACION IS NOT NULL THEN FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION, SAL.GMT_CREACION) ELSE NULL END) AS FECHA_ALERTA,
                      NVL2(SAL.GMT_CREACION,
                           CASE
                             WHEN SAL.GMT_CREACION <= SYSDATE THEN
                              (SELECT to_number(to_char(to_date('1', 'J') +
                                                        (SYSDATE -
                                                         SAL.GMT_CREACION),
                                                        'J') - 1)
                                 FROM DUAL)
                           END,
                           NULL) AS DIAS_ALERTA,
                      FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION,
                                        CO.FEC_VENCIMIENTO) AS FECHA_VENCIMIENTO,
                      NVL2(CO.FEC_VENCIMIENTO,
                           CASE
                             WHEN CO.FEC_VENCIMIENTO <= SYSDATE THEN
                              (SELECT to_number(to_char(to_date('1', 'J') +
                                                        (SYSDATE -
                                                         CO.FEC_VENCIMIENTO),
                                                        'J') - 1)
                                 FROM DUAL)
                           END,
                           NULL) AS DIAS_VENCIDO

        FROM SAPR_TCONTENEDOR CO
        INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON CO.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        INNER JOIN GEPR_TSECTOR SE
          ON SE.OID_SECTOR = CO.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLA
          ON SE.OID_PLANTA = PLA.OID_PLANTA
        LEFT JOIN SAPR_TPOSICION_SECTOR PSC
          ON PSC.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_VCUENTA CU
          ON CO.OID_CUENTA_MOVIMIENTO = CU.OID_CUENTA
        INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EF
          ON EF.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_TALERTA_VENC_CONTENEDOR SAL
          ON SAL.OID_CONTENEDOR = CO.OID_CONTENEDOR
        INNER JOIN GEPR_TDIVISA D
          ON D.OID_DIVISA = EF.OID_DIVISA
        WHERE (CO.OID_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ','))) OR
             (par$mayorNivel = 1 AND
              CO.OID_SECTOR IN
             (SELECT OID_SECTOR
                  FROM GEPR_TSECTOR
                 WHERE OID_SECTOR_PADRE = par$codSector)))
         AND (par$codCliente IS NULL OR CU.OID_CLIENTE = par$codCliente)
         AND (par$codSubCliente IS NULL OR
             CU.OID_SUBCLIENTE = par$codSubCliente)
         AND (par$codPuntoDeServicio IS NULL OR
             CU.OID_PTO_SERVICIO = par$codPuntoDeServicio)
         AND (par$codCanal IS NULL OR CU.OID_CANAL = par$codCanal)
         AND (par$SubCodCanal IS NULL OR CU.OID_SUBCANAL = par$SubCodCanal)
         AND (par$codTipoContenedor IS NULL OR
             CO.OID_TIPO_CONTENEDOR = par$codTipoContenedor)
         AND (par$fechaVencimentoDesde IS NULL OR TO_DATE(CO.FEC_VENCIMIENTO, 'DD/MM/YYYY') >= TO_DATE(par$fechaVencimentoDesde, 'DD/MM/YYYY'))
         AND (par$fechaVencimentoHasta IS NULL OR TO_DATE(CO.FEC_VENCIMIENTO, 'DD/MM/YYYY') <= TO_DATE(par$fechaVencimentoHasta, 'DD/MM/YYYY')))
SELECT PRECINTOS,SECTOR,SUM(IMPORTE) IMPORTE,CLIENTE,CANAL,TIPO_CONTENEDOR,DIVISA,POSICION,FECHA_ARMADO,DIAS_ARMADO,
       CANT_ALERTA,FECHA_ALERTA,DIAS_ALERTA,FECHA_VENCIMIENTO,DIAS_VENCIDO
FROM Q
GROUP BY PRECINTOS,SECTOR,CLIENTE,CANAL,TIPO_CONTENEDOR,DIVISA,POSICION,FECHA_ARMADO,DIAS_ARMADO,
CANT_ALERTA,FECHA_ALERTA,DIAS_ALERTA,FECHA_VENCIMIENTO,DIAS_VENCIDO;

  end SP_CONTENEDORES_VENCIDOS;

  procedure SP_SEGUIMIENTO_ELEMENTO(par$TipoElemento varchar2,
                                    par$CodBusca     varchar2,
                                    par$cursor out sys_refcursor) as

   var$TipoElemento      gepr_pcomon_###VERSION###.tipo$cod_;
   var$CodBusca        gepr_pcomon_###VERSION###.tipo$cod_;

  begin

    var$TipoElemento:=par$TipoElemento;
    var$CodBusca:=par$CodBusca;

    open par$cursor for

      SELECT DISTINCT D.GMT_MODIFICACION AS FECHA_DOCUMENTO,
                      F.COD_FORMULARIO || ' - ' || F.DES_FORMULARIO FORMULARIO,
                      D.COD_ESTADO AS ESTADO,
                      C.COD_ESTADO AS ESTADO_CONTENEDOR,
                      R.COD_ESTADO AS ESTADO_REMESA,
                      B.COD_ESTADO AS ESTADO_BULTO,
                      D.DES_USUARIO_MODIFICACION AS USUARIO_RESPONSIBLE,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      C.BOL_PACK_MODULAR AS PACK_MODULAR,
                      C.BOL_AGRUPA_CONTENEDOR AS AGRUPA_ELEMENTOS,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_CONTENEDOR,
                      R.COD_RUTA AS COD_RUTA,
                      R.COD_EXTERNO AS COD_EXTERNO,
                      FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION,
                                        R.FYH_TRANSPORTE) AS FECHA_TRANSPORTE,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = R.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_REMESA_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_MAYOR_NIVEL,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Incluido en el Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Armado/Ingresado para las Cuentas:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Retirado del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Elemento bajado:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         'Salida a Recorridos del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         'Salida a recorrido del elemento:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Sector:¿:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Cliente:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Procesado'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Sustituido'

                        ELSE
                         NULL
                      END) AS DESCRIPCION_ACCION,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         SEC.DES_SECTOR || ' / ' || CL.DES_CLIENTE || ' | ' ||
                         SCL.DES_SUBCLIENTE || ' | ' || PS.DES_PTO_SERVICIO ||
                         ' / ' || CA.DES_CANAL || ' | ' || SCA.DES_SUBCANAL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR =
                                 C.OID_CONTENEDOR_MAYOR_NIVEL
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         SEC.DES_SECTOR

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         CL.DES_CLIENTE || ' | ' || SCL.DES_SUBCLIENTE ||
                         ' | ' || PS.DES_PTO_SERVICIO

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL
                        ELSE
                         NULL
                      END) AS DETALLE_ACCION

        FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
       INNER JOIN SAPR_TDOCUMENTO D
          ON DOCELE.OID_DOCUMENTO = D.OID_DOCUMENTO
       INNER JOIN SAPR_TFORMULARIO F
          ON D.OID_FORMULARIO = F.OID_FORMULARIO
       INNER JOIN SAPR_TCUENTA CU
          ON D.OID_CUENTA_ORIGEN = CU.OID_CUENTA
       INNER JOIN SAPR_TCUENTA CU2
          ON D.OID_CUENTA_DESTINO = CU2.OID_CUENTA
       INNER JOIN GEPR_TSECTOR SEC
          ON SEC.OID_SECTOR = CU.OID_SECTOR
          OR SEC.OID_SECTOR = CU2.OID_SECTOR
       INNER JOIN GEPR_TPLANTA PLA
          ON PLA.OID_PLANTA = SEC.OID_PLANTA
        LEFT JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
          OR CL.OID_CLIENTE = CU2.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
          OR SCL.OID_SUBCLIENTE = CU2.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PS
          ON PS.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
          OR PS.OID_PTO_SERVICIO = CU2.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TSUBCANAL SCA
          ON SCA.OID_SUBCANAL = CU.OID_SUBCANAL
          OR SCA.OID_SUBCANAL = CU2.OID_SUBCANAL
        LEFT JOIN GEPR_TCANAL CA
          ON CA.OID_CANAL = SCA.OID_CANAL
        LEFT JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DOCELE.OID_REMESA
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DOCELE.OID_BULTO
        LEFT JOIN SAPR_TCONTENEDOR C
          ON DOCELE.OID_CONTENEDOR = C.OID_CONTENEDOR
        LEFT JOIN SAPR_TTIPO_CONTENEDOR TC
          ON TC.OID_TIPO_CONTENEDOR = C.OID_TIPO_CONTENEDOR
/*         WHERE ('B' = 'B' AND B.COD_PRECINTO = ',15558709')

      OR ('B' = 'C' AND EXISTS
          (SELECT *
             FROM SAPR_TPRECINTOXCONTENEDOR SPRE
            WHERE SPRE.OID_CONTENEDOR = C.OID_CONTENEDOR AND SPRE.COD_PRECINTO IN
                  (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (',15558709', '|')))))
      OR ('B' = 'R' AND R.COD_EXTERNO = ',15558709')*/
       WHERE (var$TipoElemento = 'B' AND B.COD_PRECINTO = var$CodBusca)
       UNION
             SELECT DISTINCT D.GMT_MODIFICACION AS FECHA_DOCUMENTO,
                      F.COD_FORMULARIO || ' - ' || F.DES_FORMULARIO FORMULARIO,
                      D.COD_ESTADO AS ESTADO,
                      C.COD_ESTADO AS ESTADO_CONTENEDOR,
                      R.COD_ESTADO AS ESTADO_REMESA,
                      B.COD_ESTADO AS ESTADO_BULTO,
                      D.DES_USUARIO_MODIFICACION AS USUARIO_RESPONSIBLE,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      C.BOL_PACK_MODULAR AS PACK_MODULAR,
                      C.BOL_AGRUPA_CONTENEDOR AS AGRUPA_ELEMENTOS,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_CONTENEDOR,
                      R.COD_RUTA AS COD_RUTA,
                      R.COD_EXTERNO AS COD_EXTERNO,
                      FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION,
                                        R.FYH_TRANSPORTE) AS FECHA_TRANSPORTE,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = R.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_REMESA_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_MAYOR_NIVEL,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Incluido en el Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Armado/Ingresado para las Cuentas:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Retirado del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Elemento bajado:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         'Salida a Recorridos del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         'Salida a recorrido del elemento:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Sector:¿:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Cliente:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Procesado'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Sustituido'

                        ELSE
                         NULL
                      END) AS DESCRIPCION_ACCION,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         SEC.DES_SECTOR || ' / ' || CL.DES_CLIENTE || ' | ' ||
                         SCL.DES_SUBCLIENTE || ' | ' || PS.DES_PTO_SERVICIO ||
                         ' / ' || CA.DES_CANAL || ' | ' || SCA.DES_SUBCANAL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR =
                                 C.OID_CONTENEDOR_MAYOR_NIVEL
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         SEC.DES_SECTOR

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         CL.DES_CLIENTE || ' | ' || SCL.DES_SUBCLIENTE ||
                         ' | ' || PS.DES_PTO_SERVICIO

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL
                        ELSE
                         NULL
                      END) AS DETALLE_ACCION

        FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
       INNER JOIN SAPR_TDOCUMENTO D
          ON DOCELE.OID_DOCUMENTO = D.OID_DOCUMENTO
       INNER JOIN SAPR_TFORMULARIO F
          ON D.OID_FORMULARIO = F.OID_FORMULARIO
       INNER JOIN SAPR_TCUENTA CU
          ON D.OID_CUENTA_ORIGEN = CU.OID_CUENTA
       INNER JOIN SAPR_TCUENTA CU2
          ON D.OID_CUENTA_DESTINO = CU2.OID_CUENTA
       INNER JOIN GEPR_TSECTOR SEC
          ON SEC.OID_SECTOR = CU.OID_SECTOR
          OR SEC.OID_SECTOR = CU2.OID_SECTOR
       INNER JOIN GEPR_TPLANTA PLA
          ON PLA.OID_PLANTA = SEC.OID_PLANTA
        LEFT JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
          OR CL.OID_CLIENTE = CU2.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
          OR SCL.OID_SUBCLIENTE = CU2.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PS
          ON PS.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
          OR PS.OID_PTO_SERVICIO = CU2.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TSUBCANAL SCA
          ON SCA.OID_SUBCANAL = CU.OID_SUBCANAL
          OR SCA.OID_SUBCANAL = CU2.OID_SUBCANAL
        LEFT JOIN GEPR_TCANAL CA
          ON CA.OID_CANAL = SCA.OID_CANAL
        LEFT JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DOCELE.OID_REMESA
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DOCELE.OID_BULTO
        LEFT JOIN SAPR_TCONTENEDOR C
          ON DOCELE.OID_CONTENEDOR = C.OID_CONTENEDOR
        LEFT JOIN SAPR_TTIPO_CONTENEDOR TC
          ON TC.OID_TIPO_CONTENEDOR = C.OID_TIPO_CONTENEDOR
/*         WHERE ('B' = 'B' AND B.COD_PRECINTO = ',15558709')

      OR ('B' = 'C' AND EXISTS
          (SELECT *
             FROM SAPR_TPRECINTOXCONTENEDOR SPRE
            WHERE SPRE.OID_CONTENEDOR = C.OID_CONTENEDOR AND SPRE.COD_PRECINTO IN
                  (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (',15558709', '|')))))
      OR ('B' = 'R' AND R.COD_EXTERNO = ',15558709')*/

          WHERE (var$TipoElemento = 'C' AND EXISTS
              (SELECT *
                 FROM SAPR_TPRECINTOXCONTENEDOR SPRE
                WHERE SPRE.OID_CONTENEDOR = C.OID_CONTENEDOR
                  AND SPRE.COD_PRECINTO IN
                      (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (var$CodBusca, '|')))))
        UNION
              SELECT DISTINCT D.GMT_MODIFICACION AS FECHA_DOCUMENTO,
                      F.COD_FORMULARIO || ' - ' || F.DES_FORMULARIO FORMULARIO,
                      D.COD_ESTADO AS ESTADO,
                      C.COD_ESTADO AS ESTADO_CONTENEDOR,
                      R.COD_ESTADO AS ESTADO_REMESA,
                      B.COD_ESTADO AS ESTADO_BULTO,
                      D.DES_USUARIO_MODIFICACION AS USUARIO_RESPONSIBLE,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      C.BOL_PACK_MODULAR AS PACK_MODULAR,
                      C.BOL_AGRUPA_CONTENEDOR AS AGRUPA_ELEMENTOS,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_CONTENEDOR,
                      R.COD_RUTA AS COD_RUTA,
                      R.COD_EXTERNO AS COD_EXTERNO,
                      FN_GMT_DELEGACION_###VERSION###(PLA.OID_DELEGACION,
                                        R.FYH_TRANSPORTE) AS FECHA_TRANSPORTE,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = R.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_REMESA_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = B.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_BULTO_MAYOR_NIVEL,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS_MAYOR_NIVEL,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Incluido en el Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Armado/Ingresado para las Cuentas:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         'Retirado del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         'Elemento bajado:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         'Salida a Recorridos del Contenedor:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         'Salida a recorrido del elemento:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Sector:¿:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Enviado al Cliente:'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Procesado'

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         'Elemento Sustituido'

                        ELSE
                         NULL
                      END) AS DESCRIPCION_ACCION,

                      (CASE
                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ALTAS'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         SEC.DES_SECTOR || ' / ' || CL.DES_CLIENTE || ' | ' ||
                         SCL.DES_SUBCLIENTE || ' | ' || PS.DES_PTO_SERVICIO ||
                         ' / ' || CA.DES_CANAL || ' | ' || SCA.DES_SUBCANAL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'DESARMAR_CONTENEDORES' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'BAJA_ELEMENTOS')
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_PADRE IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL THEN
                         (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                            FROM SAPR_TPRECINTOXCONTENEDOR
                           WHERE OID_CONTENEDOR =
                                 C.OID_CONTENEDOR_MAYOR_NIVEL
                           GROUP BY OID_CONTENEDOR)

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_BAJAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) AND
                             C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND (CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_MISMA_PLANTA' OR
                                     CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_SECTORES_PLANTAS_DIFERENTES')
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'SALIDAS_RECORRIDO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         SEC.DES_SECTOR

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_REENVIOS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ENTRE_CLIENTES'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         CL.DES_CLIENTE || ' | ' || SCL.DES_SUBCLIENTE ||
                         ' | ' || PS.DES_PTO_SERVICIO

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_ACTAS'
                                 AND CF.COD_CARACT_FORMULARIO =
                                     'ACTA_RECUENTO'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL

                        WHEN EXISTS
                         (SELECT *
                                FROM SAPR_TCARACTFORMXFORMULARIO CFF
                               INNER JOIN SAPR_TCARACT_FORMULARIO CF
                                  ON CF.OID_CARACT_FORMULARIO =
                                     CFF.OID_CARACT_FORMULARIO
                               WHERE CF.COD_CARACT_FORMULARIO =
                                     'ACCION_SUSTITUCION'
                                 AND CFF.OID_FORMULARIO = F.OID_FORMULARIO) THEN
                         NULL
                        ELSE
                         NULL
                      END) AS DETALLE_ACCION

        FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
       INNER JOIN SAPR_TDOCUMENTO D
          ON DOCELE.OID_DOCUMENTO = D.OID_DOCUMENTO
       INNER JOIN SAPR_TFORMULARIO F
          ON D.OID_FORMULARIO = F.OID_FORMULARIO
       INNER JOIN SAPR_TCUENTA CU
          ON D.OID_CUENTA_ORIGEN = CU.OID_CUENTA
       INNER JOIN SAPR_TCUENTA CU2
          ON D.OID_CUENTA_DESTINO = CU2.OID_CUENTA
       INNER JOIN GEPR_TSECTOR SEC
          ON SEC.OID_SECTOR = CU.OID_SECTOR
          OR SEC.OID_SECTOR = CU2.OID_SECTOR
       INNER JOIN GEPR_TPLANTA PLA
          ON PLA.OID_PLANTA = SEC.OID_PLANTA
        LEFT JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
          OR CL.OID_CLIENTE = CU2.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
          OR SCL.OID_SUBCLIENTE = CU2.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PS
          ON PS.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
          OR PS.OID_PTO_SERVICIO = CU2.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TSUBCANAL SCA
          ON SCA.OID_SUBCANAL = CU.OID_SUBCANAL
          OR SCA.OID_SUBCANAL = CU2.OID_SUBCANAL
        LEFT JOIN GEPR_TCANAL CA
          ON CA.OID_CANAL = SCA.OID_CANAL
        LEFT JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DOCELE.OID_REMESA
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DOCELE.OID_BULTO
        LEFT JOIN SAPR_TCONTENEDOR C
          ON DOCELE.OID_CONTENEDOR = C.OID_CONTENEDOR
        LEFT JOIN SAPR_TTIPO_CONTENEDOR TC
          ON TC.OID_TIPO_CONTENEDOR = C.OID_TIPO_CONTENEDOR
/*         WHERE ('B' = 'B' AND B.COD_PRECINTO = ',15558709')

      OR ('B' = 'C' AND EXISTS
          (SELECT *
             FROM SAPR_TPRECINTOXCONTENEDOR SPRE
            WHERE SPRE.OID_CONTENEDOR = C.OID_CONTENEDOR AND SPRE.COD_PRECINTO IN
                  (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (',15558709', '|')))))
      OR ('B' = 'R' AND R.COD_EXTERNO = ',15558709')*/
          WHERE (var$TipoElemento = 'R' AND R.COD_EXTERNO = var$CodBusca);

  end SP_SEGUIMIENTO_ELEMENTO;

 procedure SP_CONTENEDORESXCLIENTES(par$codEstadoContenedor varchar2,
                                     par$packModular         number,
                                     par$mayorNivel          number,
                                     par$codSector           varchar2,
                                     par$codCliente          varchar2,
                                     par$codSubCliente       varchar2,
                                     par$codPuntoDeServicio  varchar2,
                                     par$codCanal            varchar2,
                                     par$SubCodCanal         varchar2,
                                     par$divisas             varchar2,
                                     par$cursor              out sys_refcursor) as

  begin
    open par$cursor for
     WITH CONTENEDORES AS (
      SELECT            CO.OID_CONTENEDOR,
                      (CASE
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE
                        ELSE
                         NULL
                      END) AS CLIENTE,
                      (CASE
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NOT NULL THEN
                         CU.COD_CANAL || ' ' || CU.DES_CANAL || ' | ' ||
                         CU.DES_SUBCANAL
                        ELSE
                         NULL
                      END) AS CANAL,
                      D.COD_SIMBOLO || ' - ' || D.DES_DIVISA AS DIVISA,
                      CU.DES_SECTOR AS SECTOR,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      EF.NUM_IMPORTE AS IMPORTE,
                      CASE WHEN TC.NEC_CANTIDAD IS NULL OR TC.NEC_CANTIDAD = 0 THEN NULL ELSE DE.NUM_VALOR END AS DENOMINACION,
                      NULL AS MEDIO_PAGO,
                      EF.NEL_CANTIDAD AS CANTIDAD,
                      (CASE
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND CAL.COD_CALIDAD <> 'D' THEN
                         'C'
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD <> 'D' THEN
                         'C'
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD = 'D' THEN
                         'CD'
                        WHEN TC.NEC_CANTIDAD IS NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD <> 'D' THEN
                         'P'
                        WHEN TC.NEC_CANTIDAD IS NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD = 'D' THEN
                         'PD'
                        ELSE
                         'I'
                      END) AS TIPO_CONTENEDOR_CODE
        FROM SAPR_TCONTENEDOR CO
        LEFT JOIN SAPR_TPRECINTOXCONTENEDOR SPRE
          ON SPRE.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON CO.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        LEFT JOIN GEPR_TCALIDAD CAL
          ON TC.OID_CALIDAD = CAL.OID_CALIDAD
        LEFT JOIN SAPR_TPOSICION_SECTOR PSC
          ON PSC.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_VCUENTA CU
          ON CO.OID_CUENTA_MOVIMIENTO = CU.OID_CUENTA
        LEFT JOIN SAPR_TEFECTIVOXCONTENEDOR EF
          ON EF.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA D
          ON D.OID_DIVISA = EF.OID_DIVISA
        LEFT JOIN GEPR_TDENOMINACION DE
          ON DE.OID_DENOMINACION = EF.OID_DENOMINACION
         AND DE.OID_DIVISA = EF.OID_DIVISA
       WHERE (par$codSector IS NULL OR
             (CO.OID_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ','))) OR
             (par$mayorNivel = 1 AND
             CO.OID_SECTOR IN
             (SELECT OID_SECTOR
                   FROM GEPR_TSECTOR
                  WHERE OID_SECTOR_PADRE = par$codSector))))
         AND (par$codCliente IS NULL OR CU.OID_CLIENTE = par$codCliente)
         AND (par$codSubCliente IS NULL OR
             CU.OID_SUBCLIENTE = par$codSubCliente)
         AND (par$codPuntoDeServicio IS NULL OR
             CU.OID_PTO_SERVICIO = par$codPuntoDeServicio)
         AND (par$codCanal IS NULL OR CU.OID_CANAL = par$codCanal)
         AND (par$SubCodCanal IS NULL OR CU.OID_SUBCANAL = par$SubCodCanal)
         AND (CO.BOL_PACK_MODULAR = par$packModular)
         AND (par$codEstadoContenedor IS NULL OR
             CO.COD_ESTADO in(SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codEstadoContenedor, ','))))
         AND (par$divisas IS NULL OR
             D.OID_DIVISA IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$divisas, ','))))

      UNION

      SELECT         CO.OID_CONTENEDOR,
                    (CASE
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NOT NULL THEN
                         CL.COD_CLIENTE || ' - ' || CL.DES_CLIENTE
                        ELSE
                         NULL
                      END) AS CLIENTE,
                      (CASE
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NOT NULL THEN
                         CAN.COD_CANAL || ' ' || CAN.DES_CANAL || ' | ' ||
                         SCA.DES_SUBCANAL
                        ELSE
                         NULL
                      END) AS CANAL,
                      D.COD_SIMBOLO || ' - ' || D.DES_DIVISA AS DIVISA,
                      SE.DES_SECTOR AS SECTOR,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      MPC.NUM_IMPORTE AS IMPORTE,
                      NULL AS DENOMINACION,
                      CASE WHEN TC.NEC_CANTIDAD IS NULL OR TC.NEC_CANTIDAD = 0 THEN NULL ELSE MP.DES_MEDIO_PAGO END AS MEDIO_PAGO,
                      MPC.NEL_CANTIDAD AS CANTIDAD,
                      (CASE
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND CAL.COD_CALIDAD <> 'D' THEN
                         'C'
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD <> 'D' THEN
                         'C'
                        WHEN TC.NEC_CANTIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD = 'D' THEN
                         'CD'
                        WHEN TC.NEC_CANTIDAD IS NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD <> 'D' THEN
                         'P'
                        WHEN TC.NEC_CANTIDAD IS NULL AND
                             CAL.COD_CALIDAD IS NOT NULL AND
                             CAL.COD_CALIDAD = 'D' THEN
                         'PD'
                        ELSE
                         'I'
                      END) AS TIPO_CONTENEDOR_CODE
        FROM SAPR_TCONTENEDOR CO
        LEFT JOIN SAPR_TPRECINTOXCONTENEDOR SPRE
          ON SPRE.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON CO.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        LEFT JOIN GEPR_TCALIDAD CAL
          ON TC.OID_CALIDAD = CAL.OID_CALIDAD
        LEFT JOIN SAPR_TPOSICION_SECTOR PSC
          ON PSC.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_TCUENTA CU
          ON CO.OID_CUENTA_MOVIMIENTO = CU.OID_CUENTA
        LEFT JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
        LEFT JOIN GEPR_TSECTOR SE
          ON SE.OID_SECTOR = CU.OID_SECTOR
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PS
          ON PS.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TSUBCANAL SCA
          ON SCA.OID_SUBCANAL = CU.OID_SUBCANAL
        LEFT JOIN GEPR_TCANAL CAN
          ON CAN.OID_CANAL = SCA.OID_CANAL
        LEFT JOIN SAPR_TMEDIO_PAGOXCONTENEDOR MPC
          ON MPC.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA D
          ON D.OID_DIVISA = MPC.OID_DIVISA
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MPC.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO
       WHERE (par$codSector IS NULL OR
             CO.OID_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ','))))
          OR (par$codSector IS NULL OR
             par$mayorNivel = 1 AND
             CO.OID_SECTOR IN
             (SELECT OID_SECTOR
                 FROM GEPR_TSECTOR
                WHERE OID_SECTOR_PADRE = par$codSector))
         AND (par$codCliente IS NULL OR CU.OID_CLIENTE = par$codCliente)
         AND (par$codSubCliente IS NULL OR
             SCL.OID_SUBCLIENTE = par$codSubCliente)
         AND (par$codPuntoDeServicio IS NULL OR
             PS.OID_PTO_SERVICIO = par$codPuntoDeServicio)
         AND (par$codCanal IS NULL OR CAN.OID_CANAL = par$codCanal)
         AND (par$SubCodCanal IS NULL OR SCA.OID_SUBCANAL = par$SubCodCanal)
         AND (CO.BOL_PACK_MODULAR = par$packModular)
         AND (par$codEstadoContenedor IS NULL OR
              CO.COD_ESTADO in(SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codEstadoContenedor, ','))))
         AND (par$divisas IS NULL OR
             D.OID_DIVISA IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$divisas, ',')))))
         SELECT CLIENTE,
                  CANAL,
                  DIVISA,
                  SECTOR,
                  TIPO_CONTENEDOR,
                  SUM(IMPORTE) IMPORTE,
                  DENOMINACION,
                  MEDIO_PAGO,
                  SUM(CANTIDAD) CANTIDAD,
                  TIPO_CONTENEDOR_CODE,
                  COUNT(DISTINCT OID_CONTENEDOR) CANTIDAD_CONTENEDOR
           FROM CONTENEDORES
           GROUP BY CLIENTE, CANAL, DIVISA, SECTOR, TIPO_CONTENEDOR, DENOMINACION, MEDIO_PAGO, TIPO_CONTENEDOR_CODE;

  end SP_CONTENEDORESXCLIENTES;

 procedure SP_INVENTARIO_CONTENEDOR(par$codInventario varchar2,
                                     par$cursor        out sys_refcursor) as

  begin
    open par$cursor for
      SELECT DISTINCT (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = CO.OID_CONTENEDOR
                        GROUP BY OID_CONTENEDOR) AS PRECINTOS,

                      (SELECT LISTAGG(CU1.COD_CLIENTE || ' - ' || CU1.DES_CLIENTE , ' | ') WITHIN GROUP(ORDER BY CO.OID_CONTENEDOR) AS CLIENTES
                         FROM SAPR_VCUENTA CU1
                        WHERE CU1.OID_CUENTA IN(SELECT IC.OID_CUENTA_SALDO FROM SAPR_TINVENTARIOXCONTENEDOR IC
                              INNER JOIN SAPR_TINVENTARIO I ON I.OID_INVENTARIO = IC.OID_INVENTARIO WHERE I.COD_INVENTARIO = par$codInventario)
                        GROUP BY CO.OID_CONTENEDOR) AS CLIENTES,

                      CO.COD_ESTADO AS ESTADO,

                      (SELECT LISTAGG(COD_PRECINTO, ' | ') WITHIN GROUP(ORDER BY OID_CONTENEDOR) AS COD_PRECINTOS
                         FROM SAPR_TPRECINTOXCONTENEDOR
                        WHERE OID_CONTENEDOR = CO.OID_CONTENEDOR_MAYOR_NIVEL
                        GROUP BY OID_CONTENEDOR) AS CONTENEDOR_PADRE,

                      (CASE
                        WHEN CU.DES_CLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE
                        WHEN CU.DES_SUBCLIENTE IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE || ' | ' ||
                         CU.DES_SUBCLIENTE
                        WHEN CU.DES_PTO_SERVICIO IS NOT NULL THEN
                         CU.COD_CLIENTE || ' - ' || CU.DES_CLIENTE || ' | ' ||
                         CU.DES_SUBCLIENTE || ' | ' || CU.DES_PTO_SERVICIO
                      END) AS CLIENTE,

                      (CASE
                        WHEN CO.OID_CUENTA_MOVIMIENTO IS NOT NULL THEN
                         CU.COD_CANAL || ' ' || CU.DES_CANAL || ' | ' ||
                         CU.DES_SUBCANAL
                        ELSE
                         NULL
                      END) AS CANAL,

                      FN_GMT_DELEGACION_###VERSION###(CU.OID_DELEGACION, CO.GMT_CREACION) AS FECHA_ARMADO,
                      TC.DES_TIPO_CONTENEDOR AS TIPO_CONTENEDOR,
                      PSC.COD_POSICION AS POSICION,
                      D.COD_SIMBOLO AS DIVISA,
                      CASE WHEN TC.NEC_CANTIDAD IS NOT NULL THEN DE.NUM_VALOR ELSE NULL END  VALOR_DIVISA,
                      SUM(EF.NUM_IMPORTE) AS IMPORTE,
                      INV.COD_INVENTARIO,
                      CU.COD_SECTOR || ' - ' || CU.DES_SECTOR AS SECTOR,
                      INV.BOL_AUTOMATICO AS GENERADO_AUTOMATICAMENTE,
                      INV.DES_USUARIO_CREACION USUARIO_RESPONSABLE,
                      FN_GMT_DELEGACION_###VERSION###(CU.OID_DELEGACION, INV.GMT_CREACION) AS FECHA_INVENTARIO,

                      (CASE
                        WHEN INC.COD_ESTADO <> 'E' THEN
                         0
                        ELSE
                         1
                      END) BOL_INVENTARIADO,

                      (SELECT Q.NEC_CANTIDAD_INVENTARIADA FROM SAPR_TINVENTARIO Q WHERE Q.COD_INVENTARIO = par$codInventario) AS INVENTARIADO,

                      (SELECT Q.NEC_CANTIDAD_LOGICA FROM SAPR_TINVENTARIO Q WHERE Q.COD_INVENTARIO = par$codInventario) AS CONT_TEORICOS

        FROM SAPR_TCONTENEDOR CO
        LEFT JOIN SAPR_TPRECINTOXCONTENEDOR SPRE
          ON SPRE.OID_CONTENEDOR = CO.OID_CONTENEDOR
        INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON CO.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        INNER JOIN GEPR_TSECTOR SEC
           ON SEC.OID_SECTOR = CO.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLA
           ON SEC.OID_PLANTA = PLA.OID_PLANTA
        LEFT JOIN SAPR_TPOSICION_SECTOR PSC
          ON PSC.OID_CONTENEDOR = CO.OID_CONTENEDOR
        LEFT JOIN SAPR_VCUENTA CU
          ON CO.OID_CUENTA_MOVIMIENTO = CU.OID_CUENTA
        LEFT JOIN SAPR_TEFECTIVOXCONTENEDOR EF
          ON EF.OID_CONTENEDOR = CO.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA D
          ON D.OID_DIVISA = EF.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DE
          ON DE.OID_DENOMINACION = EF.OID_DENOMINACION
         AND DE.OID_DIVISA = EF.OID_DIVISA
       INNER JOIN SAPR_TINVENTARIOXCONTENEDOR INC
         ON INC.OID_CONTENEDOR = CO.OID_CONTENEDOR
       LEFT JOIN SAPR_TINVENTARIO INV
         ON INV.OID_INVENTARIO = INC.OID_INVENTARIO
       WHERE INV.COD_INVENTARIO = par$codInventario
      GROUP BY CO.OID_CONTENEDOR_MAYOR_NIVEL,
              CO.COD_ESTADO,
              INV.COD_INVENTARIO,
              CO.OID_CONTENEDOR,
              INV.GMT_CREACION,
              INV.BOL_AUTOMATICO,
              INV.DES_USUARIO_CREACION,
              INC.COD_ESTADO,
              CU.DES_CLIENTE,
              CU.COD_CLIENTE,
              CU.DES_SUBCLIENTE,
              CU.DES_PTO_SERVICIO,
              CO.OID_CUENTA_MOVIMIENTO,
              CU.COD_CANAL,
              CU.DES_CANAL,
              CU.DES_SUBCANAL,
              CU.OID_DELEGACION,
              CO.GMT_CREACION,
              TC.DES_TIPO_CONTENEDOR,
              PSC.COD_POSICION,
              D.COD_SIMBOLO,
              CU.COD_SECTOR,
              CU.DES_SECTOR,
              CASE WHEN TC.NEC_CANTIDAD IS NOT NULL THEN DE.NUM_VALOR ELSE NULL END;

  end SP_INVENTARIO_CONTENEDOR;

end;
/