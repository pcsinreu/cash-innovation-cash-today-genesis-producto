create or replace package SAPR_PCONSULTAS_ATM_###VERSION### is
  /*Version: ###VERSION_COMP###*/

   PROCEDURE SOBTENEBANCOSXPAGINA(PAR$NUMPAGINA     IN INTEGER,
                                 PAR$MAXPORPAGINA  IN INTEGER,
                                 PAR$INCLUIRPAGINA IN INTEGER,
                                 PAR$FECHADESDE    IN DATE,
                                 PAR$FECHAHASTA    IN DATE,
                                 PAR$CODCLIENTE    IN VARCHAR2,
                                 PAR$DESCLIENTE    IN VARCHAR2,
                                 PAR$CODDELEGACION IN VARCHAR2,
                                 PAR$BANCOS        OUT SYS_REFCURSOR);

  PROCEDURE SOBTENEDETALLEBANCOSXPAGINA(PAR$NUMPAGINA     IN INTEGER,
                                        PAR$MAXPORPAGINA  IN INTEGER,
                                        PAR$INCLUIRPAGINA IN INTEGER,
                                        PAR$COD_CLIENTE   IN INTEGER,
                                        PAR$FECHADESDE    IN DATE,
                                        PAR$FECHAHASTA    IN DATE,
                                        PAR$CODDELEGACION IN VARCHAR2,
                                        PAR$DETALLES      OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERDETALLEACREDPORCLIENTE(PAR$NUMPAGINA       IN INTEGER,
                                           PAR$MAXPORPAGINA    IN INTEGER,
                                           PAR$INCLUIRPAGINA   IN INTEGER,
                                           PAR$FYHACREDITACION IN DATE,
                                           PAR$CODCLIENTE      IN VARCHAR2,
                                           PAR$CODDELEGACION   IN VARCHAR2,
                                           PAR$DETALLESACRED   OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERMAES(PAR$NUMPAGINA       IN INTEGER,
                         PAR$MAXPORPAGINA    IN INTEGER,
                         PAR$INCLUIRPAGINA   IN INTEGER,
                         PAR$FYHACREDITACION IN DATE,
                         PAR$CODCLIENTE      IN VARCHAR2,
                         PAR$CODDELEGACION   IN VARCHAR2,
                         PAR$MAES            OUT SYS_REFCURSOR);
                         
  PROCEDURE SOBTENERMAESDELEG(PAR$NUMPAGINA       IN INTEGER,
                              PAR$MAXPORPAGINA    IN INTEGER,
                              PAR$INCLUIRPAGINA   IN INTEGER,
                              PAR$FYHACREDITACION IN DATE,
                              PAR$CODCLIENTE      IN VARCHAR2,
                              PAR$CODDELEGACION   IN VARCHAR2,
                              PAR$OIDPLANTA       IN VARCHAR2,
                              PAR$MAES            OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERTRADUCCIONES(par$codFuncionalidad IN VARCHAR2,
                                 par$codIdioma        IN VARCHAR2,
                                 par$resTraducciones  OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERBANCOSFECHAVALOR(PAR$NUMPAGINA     IN INTEGER,
                                     PAR$MAXPORPAGINA  IN INTEGER,
                                     PAR$INCLUIRPAGINA IN INTEGER,
                                     PAR$CODCLIENTE    IN VARCHAR2,
                                     PAR$DESCRIPCION   IN VARCHAR2,
                                     PAR$RESBANCOS     OUT SYS_REFCURSOR);

  PROCEDURE STORTAMAESXCLIENTE(PAR$OIDPLANIFICACION IN VARCHAR2,
                               PAR$OIDCLIENTE       IN VARCHAR2,
                               PAR$MAES             OUT SYS_REFCURSOR,
                               PAR$MAESDELEG        OUT SYS_REFCURSOR);

  PROCEDURE STORTAMAESXDELEGACION(PAR$OIDPLANIFICACION IN VARCHAR2,
                                  PAR$OIDCLIENTE       IN VARCHAR2,
                                  PAR$MAES             OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERSALDOSDESCLI(PAR$OIDCLIENTES      IN VARCHAR2,
                                 PAR$OIDPLANIFICACION IN VARCHAR2,
                                 PAR$SALDOSDESCLI     OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERSALDOSDESDELEG(PAR$OIDDELEGACIONES  IN VARCHAR2,
                                   PAR$OIDPLANIFICACION IN VARCHAR2,
                                   PAR$SALDOSDESDEL     OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERPLANIFICACIONES(PAR$OIDCLIENTE      IN VARCHAR2,
                                    PAR$PLANIFICACIONES OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERDETACREDMAE(PAR$OIDACREDITACION IN VARCHAR2,
                                PAR$OIDMAQUINA      IN VARCHAR2,
                                PAR$NUMPAGINA       IN INTEGER,
                                PAR$MAXPORPAGINA    IN INTEGER,
                                PAR$INCLUIRPAGINA   IN INTEGER,
                                PAR$ACREDMAES       OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERSALDOSDESP(PAR$OIDPLANIFICACION IN VARCHAR2,
                               PAR$SALDOSDESP       OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERSALDOSAACREDITAR(PAR$OIDPLANIFICACION IN VARCHAR2,
                                     PAR$SALDOSACRED      OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERCLIPLANIFICACION(PAR$NUMPAGINA     IN INTEGER,
                                     PAR$MAXPORPAGINA  IN INTEGER,
                                     PAR$INCLUIRPAGINA IN INTEGER,
                                     PAR$CLIENTES      OUT SYS_REFCURSOR);

  PROCEDURE SOBTENERDETALLEACREDPORDELEG(PAR$NUMPAGINA       IN INTEGER,
                                         PAR$MAXPORPAGINA    IN INTEGER,
                                         PAR$INCLUIRPAGINA   IN INTEGER,
                                         PAR$OIDACREDITACION IN VARCHAR,
                                         PAR$CODCLIENTE      IN VARCHAR2,
                                         PAR$CODDELEGACION   IN VARCHAR2,
                                         PAR$DETALLESACRED   OUT SYS_REFCURSOR);

end sapr_pconsultas_atm_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pconsultas_atm_###VERSION### IS

  PROCEDURE SOBTENEBANCOSXPAGINA(PAR$NUMPAGINA     IN INTEGER,
                                 PAR$MAXPORPAGINA  IN INTEGER,
                                 PAR$INCLUIRPAGINA IN INTEGER,
                                 PAR$FECHADESDE    IN DATE,
                                 PAR$FECHAHASTA    IN DATE,
                                 PAR$CODCLIENTE    IN VARCHAR2,
                                 PAR$DESCLIENTE    IN VARCHAR2,
                                 PAR$CODDELEGACION IN VARCHAR2,
                                 PAR$BANCOS        OUT SYS_REFCURSOR) IS
  BEGIN
  
    OPEN PAR$BANCOS FOR
      WITH BANKS AS
       (SELECT BA.COD_CLIENTE,
               BA.DES_CLIENTE,
               CM.DES_LOGO_PATH,
               CM.NEC_ORDEN,
               CM.BOL_MOSTRAR,
               BA.OID_CLIENTE
          FROM SAPR_TACREDITACION ACR
         INNER JOIN GEPR_TCLIENTE BA
            ON ACR.OID_CLIENTE = BA.OID_CLIENTE
         INNER JOIN SAPR_TACREDITACION_EFECTIVO EFE
            ON EFE.OID_ACREDITACION = ACR.OID_ACREDITACION
         INNER JOIN GEPR_TDIVISA DIV
            ON DIV.OID_DIVISA = EFE.OID_DIVISA
          LEFT OUTER JOIN SAPR_TCONFIG_CONSULTAS_MAE CM
            ON CM.OID_CLIENTE = BA.OID_CLIENTE
         WHERE 1 = 1
           AND ((PAR$FECHADESDE IS NULL AND PAR$FECHAHASTA IS NULL OR
               (TRIM(PAR$CODDELEGACION) IS NULL)) OR
               ACR.FYH_ACREDITACION BETWEEN
               FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FECHADESDE) AND
               FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FECHAHASTA))
           AND ((TRIM(PAR$CODCLIENTE) IS NULL) OR
               (BA.COD_CLIENTE = PAR$CODCLIENTE))
           AND ((TRIM(PAR$DESCLIENTE) IS NULL) OR
               (UPPER(BA.DES_CLIENTE) LIKE
               '%' || UPPER(PAR$DESCLIENTE) || '%'))
           AND CM.BOL_MOSTRAR = 1
         GROUP BY BA.COD_CLIENTE,
                  BA.DES_CLIENTE,
                  CM.DES_LOGO_PATH,
                  CM.NEC_ORDEN,
                  CM.BOL_MOSTRAR,
                  BA.OID_CLIENTE)
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM BANKS) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH,
             NULL NEC_ORDEN,
             NULL OID_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'TP' CQUE,
             CEIL((SELECT COUNT(*) FROM BANKS) / PAR$MAXPORPAGINA) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH,
             NULL NEC_ORDEN,
             NULL OID_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH,
             NULL NEC_ORDEN,
             NULL OID_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_CLIENTE,
             C2.DES_CLIENTE,
             C2.DES_LOGO_PATH,
             C2.NEC_ORDEN,
             C2.OID_CLIENTE
        FROM (SELECT ROWNUM NUM,
                     COD_CLIENTE,
                     DES_CLIENTE,
                     DES_LOGO_PATH,
                     NEC_ORDEN,
                     OID_CLIENTE
                FROM (SELECT BANKS.COD_CLIENTE,
                             BANKS.DES_CLIENTE,
                             BANKS.DES_LOGO_PATH,
                             BANKS.NEC_ORDEN,
                             BANKS.OID_CLIENTE
                        FROM BANKS
                       ORDER BY BANKS.NEC_ORDEN ASC, DES_CLIENTE ASC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENEBANCOSXPAGINA;

  PROCEDURE SOBTENEDETALLEBANCOSXPAGINA(PAR$NUMPAGINA     IN INTEGER,
                                        PAR$MAXPORPAGINA  IN INTEGER,
                                        PAR$INCLUIRPAGINA IN INTEGER,
                                        PAR$COD_CLIENTE   IN INTEGER,
                                        PAR$FECHADESDE    IN DATE,
                                        PAR$FECHAHASTA    IN DATE,
                                        PAR$CODDELEGACION IN VARCHAR2,
                                        PAR$DETALLES      OUT SYS_REFCURSOR) IS
  BEGIN
  
    OPEN PAR$DETALLES FOR
      WITH DETALLES AS
       (SELECT *
          FROM (SELECT BA.COD_CLIENTE,
                       BA.DES_CLIENTE,
                       ACR.FYH_ACREDITACION FYH_ACREDITACION,
                       UPPER(DIV.DES_DIVISA) DES_DIVISA,
                       ACR.OID_ACREDITACION,
                       SUM(EFE.NUM_IMPORTE) NUM_IMPORTE
                  FROM SAPR_TACREDITACION ACR
                 INNER JOIN GEPR_TCLIENTE BA
                    ON ACR.OID_CLIENTE = BA.OID_CLIENTE
                 INNER JOIN SAPR_TACREDITACION_EFECTIVO EFE
                    ON EFE.OID_ACREDITACION = ACR.OID_ACREDITACION
                 INNER JOIN GEPR_TDIVISA DIV
                    ON DIV.OID_DIVISA = EFE.OID_DIVISA
                 WHERE BA.COD_CLIENTE = PAR$COD_CLIENTE
                   AND ((PAR$FECHADESDE IS NULL AND PAR$FECHAHASTA IS NULL OR
                       (TRIM(PAR$CODDELEGACION) IS NULL)) OR
                       ACR.FYH_ACREDITACION BETWEEN
                       FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FECHADESDE) AND
                       FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FECHAHASTA))
                 GROUP BY BA.COD_CLIENTE,
                          BA.DES_CLIENTE,
                          FYH_ACREDITACION,
                          ACR.OID_ACREDITACION,
                          DIV.DES_DIVISA) PIVOT(SUM(NUM_IMPORTE) FOR DES_DIVISA IN('REAL' AS
                                                                                   REALES,
                                                                                   'DOLAR' AS
                                                                                   DOLARES,
                                                                                   'EURO' AS
                                                                                   EUROS)))
      
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM DETALLES) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL OID_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             /*  FLOOR((SELECT COUNT(*) FROM DETALLES) / PAR$MAXPORPAGINA) NUM,*/
             CEIL((SELECT COUNT(*) FROM DETALLES) / PAR$MAXPORPAGINA) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL OID_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL OID_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_CLIENTE,
             C2.DES_CLIENTE,
             C2.FYH_ACREDITACION,
             C2.OID_ACREDITACION,
             C2.REALES,
             C2.DOLARES,
             C2.EUROS
        FROM (SELECT ROWNUM NUM,
                     COD_CLIENTE,
                     DES_CLIENTE,
                     FYH_ACREDITACION,
                     OID_ACREDITACION,
                     REALES,
                     DOLARES,
                     EUROS
                FROM (SELECT DETALLES.COD_CLIENTE,
                             DETALLES.DES_CLIENTE,
                             DETALLES.FYH_ACREDITACION,
                             DETALLES.OID_ACREDITACION,
                             DETALLES.REALES,
                             DETALLES.DOLARES,
                             DETALLES.EUROS
                        FROM DETALLES
                       ORDER BY FYH_ACREDITACION DESC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENEDETALLEBANCOSXPAGINA;

  PROCEDURE SOBTENERDETALLEACREDPORCLIENTE(PAR$NUMPAGINA       IN INTEGER,
                                           PAR$MAXPORPAGINA    IN INTEGER,
                                           PAR$INCLUIRPAGINA   IN INTEGER,
                                           PAR$FYHACREDITACION IN DATE,
                                           PAR$CODCLIENTE      IN VARCHAR2,
                                           PAR$CODDELEGACION   IN VARCHAR2,
                                           PAR$DETALLESACRED   OUT SYS_REFCURSOR) IS
  BEGIN
  
    OPEN PAR$DETALLESACRED FOR
      WITH CLIENTES AS
       (SELECT *
          FROM (SELECT ACR.FYH_ACREDITACION FYH_ACREDITACION,
                       CLI.COD_CLIENTE,
                       CLI.DES_CLIENTE,
                       -- CONTABLE
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                              UPPER(DIV.DES_DIVISA)
                             ELSE
                              ''
                           END) AS DES_DIVISA_C,
                       MAX(CASE
                             WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                              'CONTABLE'
                             ELSE
                              ''
                           END) AS COD_TIPO_CALCULO_C,
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                              'CONTABLE'
                             ELSE
                              ''
                           END) AS DES_TIPO_CALCULO_C,
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                              DIV.COD_ISO_DIVISA
                             ELSE
                              ''
                           END) AS COD_DIVISA_C,
                       SUM(CASE
                             WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                              CAL.NUM_IMPORTE
                             ELSE
                              0
                           END) AS IMPORTE_C,
                       -- FISICO
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                              DIV.DES_DIVISA
                             ELSE
                              ''
                           END) AS DES_DIVISA_F,
                       MAX(CASE
                             WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                              'FISICO'
                             ELSE
                              ''
                           END) AS COD_TIPO_CALCULO_F,
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                              'FISICO'
                             ELSE
                              ''
                           END) AS DES_TIPO_CALCULO_F,
                       MAX(CASE
                             WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                              DIV.COD_ISO_DIVISA
                             ELSE
                              ''
                           END) AS COD_DIVISA_F,
                       SUM(CASE
                             WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                              CAL.NUM_IMPORTE
                             ELSE
                              0
                           END) AS IMPORTE_F
                  FROM SAPR_TACREDITACION ACR
                 INNER JOIN SAPR_TPERIODO PER
                    ON ACR.OID_ACREDITACION = PER.OID_ACREDITACION
                 INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                    ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                 INNER JOIN SAPR_TMAQUINA MAE
                    ON PER.OID_MAQUINA = MAE.OID_MAQUINA
                 INNER JOIN GEPR_TPUNTO_SERVICIO SER
                    ON MAE.OID_MAQUINA = SER.OID_MAQUINA
                 INNER JOIN GEPR_TSUBCLIENTE SUB
                    ON SUB.OID_SUBCLIENTE = SER.OID_SUBCLIENTE
                 INNER JOIN GEPR_TCLIENTE CLI
                    ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                 INNER JOIN GEPR_TSECTOR SEC
                    ON SEC.OID_SECTOR = MAE.OID_SECTOR
                 INNER JOIN SAPR_TCALCULO_EFECTIVO CAL
                    ON CAL.OID_PERIODO = PER.OID_PERIODO
                 INNER JOIN SAPR_TTIPO_CALCULO TICAL
                    ON TICAL.OID_TIPO_CALCULO = CAL.OID_TIPO_CALCULO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON DIV.OID_DIVISA = CAL.OID_DIVISA
                 WHERE ACR.FYH_ACREDITACION =
                       FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION,
                                         PAR$FYHACREDITACION) --TO_DATE('08/08/2017')
                   AND ACR.OID_CLIENTE = PAR$CODCLIENTE
                 GROUP BY ACR.FYH_ACREDITACION,
                          CLI.COD_CLIENTE,
                          CLI.DES_CLIENTE,
                          ACR.OID_CLIENTE
                 ORDER BY 1, 2, 4) PIVOT(SUM(IMPORTE_C) FOR DES_DIVISA_C IN('REAL' AS
                                                                            REALES,
                                                                            'DOLAR' AS
                                                                            DOLARES,
                                                                            'EURO' AS
                                                                            EUROS)))
      
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM CLIENTES) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             /* FLOOR((SELECT COUNT(*) FROM CLIENTES) / PAR$MAXPORPAGINA) NUM,*/
             CEIL((SELECT COUNT(*) FROM CLIENTES) / PAR$MAXPORPAGINA) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_CLIENTE,
             C2.DES_CLIENTE,
             C2.FYH_ACREDITACION,
             C2.REALES,
             C2.DOLARES,
             C2.EUROS
        FROM (SELECT ROWNUM NUM,
                     COD_CLIENTE,
                     DES_CLIENTE,
                     FYH_ACREDITACION,
                     REALES,
                     DOLARES,
                     EUROS
                FROM (SELECT CLIENTES.COD_CLIENTE,
                             CLIENTES.DES_CLIENTE,
                             CLIENTES.FYH_ACREDITACION,
                             CLIENTES.REALES,
                             CLIENTES.DOLARES,
                             CLIENTES.EUROS
                        FROM CLIENTES
                       ORDER BY DES_CLIENTE ASC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERDETALLEACREDPORCLIENTE;

  PROCEDURE SOBTENERMAES(PAR$NUMPAGINA       IN INTEGER,
                         PAR$MAXPORPAGINA    IN INTEGER,
                         PAR$INCLUIRPAGINA   IN INTEGER,
                         PAR$FYHACREDITACION IN DATE,
                         PAR$CODCLIENTE      IN VARCHAR2,
                         PAR$CODDELEGACION   IN VARCHAR2,
                         PAR$MAES            OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$MAES FOR
      WITH MAES AS
       (SELECT CLI.COD_CLIENTE,
               ACR.FYH_ACREDITACION,
               MAE.COD_IDENTIFICACION,
               SEC.DES_SECTOR,
               MAE.OID_MAQUINA,
               ACR.OID_ACREDITACION,
               -- CONTABLE
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      UPPER(DIV.DES_DIVISA)
                     ELSE
                      ''
                   END) AS DES_DIVISA_C,
               MAX(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                      'CONTABLE'
                     ELSE
                      ''
                   END) AS COD_TIPO_CALCULO_C,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      'CONTABLE'
                     ELSE
                      ''
                   END) AS DES_TIPO_CALCULO_C,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      DIV.COD_ISO_DIVISA
                     ELSE
                      ''
                   END) AS COD_DIVISA_C,
               SUM(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                      CAL.NUM_IMPORTE
                     ELSE
                      0
                   END) AS IMPORTE_C,
               -- FISICO
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      DIV.DES_DIVISA
                     ELSE
                      ''
                   END) AS DES_DIVISA_F,
               MAX(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                      'FISICO'
                     ELSE
                      ''
                   END) AS COD_TIPO_CALCULO_F,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      'FISICO'
                     ELSE
                      ''
                   END) AS DES_TIPO_CALCULO_F,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      DIV.COD_ISO_DIVISA
                     ELSE
                      ''
                   END) AS COD_DIVISA_F,
               SUM(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                      CAL.NUM_IMPORTE
                     ELSE
                      0
                   END) AS IMPORTE_F
          FROM SAPR_TACREDITACION ACR
         INNER JOIN SAPR_TPERIODO PER
            ON ACR.OID_ACREDITACION = PER.OID_ACREDITACION
         INNER JOIN SAPR_TTIPO_PERIODO TIPE 
            ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
         INNER JOIN SAPR_TMAQUINA MAE
            ON PER.OID_MAQUINA = MAE.OID_MAQUINA
         INNER JOIN GEPR_TSECTOR SEC
            ON SEC.OID_SECTOR = MAE.OID_SECTOR
         INNER JOIN SAPR_TCALCULO_EFECTIVO CAL
            ON CAL.OID_PERIODO = PER.OID_PERIODO
         INNER JOIN SAPR_TTIPO_CALCULO TICAL
            ON TICAL.OID_TIPO_CALCULO = CAL.OID_TIPO_CALCULO
         INNER JOIN GEPR_TDIVISA DIV
            ON DIV.OID_DIVISA = CAL.OID_DIVISA
         INNER JOIN GEPR_TPUNTO_SERVICIO PS
            ON MAE.OID_MAQUINA = PS.OID_MAQUINA
         INNER JOIN GEPR_TSUBCLIENTE SUBCLI
            ON PS.OID_SUBCLIENTE = SUBCLI.OID_SUBCLIENTE
         INNER JOIN GEPR_TCLIENTE CLI
            ON SUBCLI.OID_CLIENTE = CLI.OID_CLIENTE
         WHERE ACR.FYH_ACREDITACION =
               FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FYHACREDITACION) --TO_DATE('08/08/2017')
           AND CLI.COD_CLIENTE = PAR$CODCLIENTE
         GROUP BY CLI.COD_CLIENTE,
                  ACR.FYH_ACREDITACION,
                  MAE.COD_IDENTIFICACION,
                  SEC.DES_SECTOR,
                  MAE.OID_MAQUINA,
                  ACR.OID_ACREDITACION
         ORDER BY 1, 2, 4)
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM MAES) NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             /* FLOOR((SELECT COUNT(*) FROM MAES) / PAR$MAXPORPAGINA) NUM,*/
             CEIL((SELECT COUNT(*) FROM MAES) / PAR$MAXPORPAGINA) NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_IDENTIFICACION,
             C2.DES_SECTOR,
             C2.OID_MAQUINA,
             C2.OID_ACREDITACION,
             C2.FYH_ACREDITACION,
             C2.COD_DIVISA_C,
             C2.COD_DIVISA_F,
             C2.IMPORTE_C,
             C2.IMPORTE_F
        FROM (SELECT ROWNUM NUM,
                     COD_IDENTIFICACION,
                     DES_SECTOR,
                     OID_MAQUINA,
                     OID_ACREDITACION,
                     FYH_ACREDITACION,
                     COD_DIVISA_C,
                     COD_DIVISA_F,
                     IMPORTE_C,
                     IMPORTE_F
                FROM (SELECT MAES.COD_IDENTIFICACION,
                             MAES.DES_SECTOR,
                             MAES.OID_MAQUINA,
                             MAES.OID_ACREDITACION,
                             MAES.FYH_ACREDITACION,
                             MAES.COD_DIVISA_C,
                             MAES.COD_DIVISA_F,
                             MAES.IMPORTE_C,
                             MAES.IMPORTE_F
                        FROM MAES
                       ORDER BY DES_SECTOR ASC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERMAES;

  PROCEDURE SOBTENERMAESDELEG(PAR$NUMPAGINA       IN INTEGER,
                              PAR$MAXPORPAGINA    IN INTEGER,
                              PAR$INCLUIRPAGINA   IN INTEGER,
                              PAR$FYHACREDITACION IN DATE,
                              PAR$CODCLIENTE      IN VARCHAR2,
                              PAR$CODDELEGACION   IN VARCHAR2,
                              PAR$OIDPLANTA       IN VARCHAR2,
                              PAR$MAES            OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$MAES FOR
      WITH MAES AS
       (SELECT CLI.COD_CLIENTE,
               ACR.FYH_ACREDITACION,
               MAE.COD_IDENTIFICACION,
               SEC.DES_SECTOR,
               MAE.OID_MAQUINA,
               ACR.OID_ACREDITACION,
               -- CONTABLE
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      UPPER(DIV.DES_DIVISA)
                     ELSE
                      ''
                   END) AS DES_DIVISA_C,
               MAX(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                      'CONTABLE'
                     ELSE
                      ''
                   END) AS COD_TIPO_CALCULO_C,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      'CONTABLE'
                     ELSE
                      ''
                   END) AS DES_TIPO_CALCULO_C,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'CONTABLE' THEN
                      DIV.COD_ISO_DIVISA
                     ELSE
                      ''
                   END) AS COD_DIVISA_C,
               SUM(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'CONTABLE' THEN
                      CAL.NUM_IMPORTE
                     ELSE
                      0
                   END) AS IMPORTE_C,
               -- FISICO
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      DIV.DES_DIVISA
                     ELSE
                      ''
                   END) AS DES_DIVISA_F,
               MAX(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                      'FISICO'
                     ELSE
                      ''
                   END) AS COD_TIPO_CALCULO_F,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      'FISICO'
                     ELSE
                      ''
                   END) AS DES_TIPO_CALCULO_F,
               MAX(CASE
                     WHEN UPPER(TICAL.DES_TIPO_CALCULO) = 'FISICO' THEN
                      DIV.COD_ISO_DIVISA
                     ELSE
                      ''
                   END) AS COD_DIVISA_F,
               SUM(CASE
                     WHEN UPPER(TICAL.COD_TIPO_CALCULO) = 'FISICO' THEN
                      CAL.NUM_IMPORTE
                     ELSE
                      0
                   END) AS IMPORTE_F
          FROM SAPR_TACREDITACION ACR
         INNER JOIN SAPR_TPERIODO PER
            ON ACR.OID_ACREDITACION = PER.OID_ACREDITACION
         INNER JOIN SAPR_TTIPO_PERIODO TIPE 
            ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
         INNER JOIN SAPR_TMAQUINA MAE
            ON PER.OID_MAQUINA = MAE.OID_MAQUINA
         INNER JOIN GEPR_TSECTOR SEC
            ON SEC.OID_SECTOR = MAE.OID_SECTOR
         INNER JOIN SAPR_TCALCULO_EFECTIVO CAL
            ON CAL.OID_PERIODO = PER.OID_PERIODO
         INNER JOIN SAPR_TTIPO_CALCULO TICAL
            ON TICAL.OID_TIPO_CALCULO = CAL.OID_TIPO_CALCULO
         INNER JOIN GEPR_TDIVISA DIV
            ON DIV.OID_DIVISA = CAL.OID_DIVISA
         INNER JOIN GEPR_TPUNTO_SERVICIO PS
            ON MAE.OID_MAQUINA = PS.OID_MAQUINA
         INNER JOIN GEPR_TSUBCLIENTE SUBCLI
            ON PS.OID_SUBCLIENTE = SUBCLI.OID_SUBCLIENTE
         INNER JOIN GEPR_TCLIENTE CLI
            ON SUBCLI.OID_CLIENTE = CLI.OID_CLIENTE
         INNER JOIN GEPR_TPLANTA PL
            ON SEC.OID_PLANTA = PL.OID_PLANTA
         WHERE ACR.FYH_ACREDITACION =
               FN_GMT_DELEGACION_###VERSION###(PAR$CODDELEGACION, PAR$FYHACREDITACION) --TO_DATE('08/08/2017')
          -- AND CLI.COD_CLIENTE = PAR$CODCLIENTE
           AND PL.OID_PLANTA = PAR$OIDPLANTA -- PLANTA/ DELEGACION SELECCIONADA
         GROUP BY CLI.COD_CLIENTE,
                  ACR.FYH_ACREDITACION,
                  MAE.COD_IDENTIFICACION,
                  SEC.DES_SECTOR,
                  MAE.OID_MAQUINA,
                  ACR.OID_ACREDITACION
         ORDER BY 1, 2, 4)
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM MAES) NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             /* FLOOR((SELECT COUNT(*) FROM MAES) / PAR$MAXPORPAGINA) NUM,*/
             CEIL((SELECT COUNT(*) FROM MAES) / PAR$MAXPORPAGINA) NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_IDENTIFICACION,
             NULL DES_SECTOR,
             NULL OID_MAQUINA,
             NULL OID_ACREDITACION,
             NULL FYH_ACREDITACION,
             NULL COD_DIVISA_C,
             NULL COD_DIVISA_F,
             NULL IMPORTE_C,
             NULL IMPORTE_F
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_IDENTIFICACION,
             C2.DES_SECTOR,
             C2.OID_MAQUINA,
             C2.OID_ACREDITACION,
             C2.FYH_ACREDITACION,
             C2.COD_DIVISA_C,
             C2.COD_DIVISA_F,
             C2.IMPORTE_C,
             C2.IMPORTE_F
        FROM (SELECT ROWNUM NUM,
                     COD_IDENTIFICACION,
                     DES_SECTOR,
                     OID_MAQUINA,
                     OID_ACREDITACION,
                     FYH_ACREDITACION,
                     COD_DIVISA_C,
                     COD_DIVISA_F,
                     IMPORTE_C,
                     IMPORTE_F
                FROM (SELECT MAES.COD_IDENTIFICACION,
                             MAES.DES_SECTOR,
                             MAES.OID_MAQUINA,
                             MAES.OID_ACREDITACION,
                             MAES.FYH_ACREDITACION,
                             MAES.COD_DIVISA_C,
                             MAES.COD_DIVISA_F,
                             MAES.IMPORTE_C,
                             MAES.IMPORTE_F
                        FROM MAES
                       ORDER BY DES_SECTOR ASC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERMAESDELEG;

  PROCEDURE SOBTENERTRADUCCIONES(PAR$CODFUNCIONALIDAD IN VARCHAR2,
                                 PAR$CODIDIOMA        IN VARCHAR2,
                                 PAR$RESTRADUCCIONES  OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$RESTRADUCCIONES FOR
      SELECT DICC.COD_EXPRESION, DICC.VALOR
        FROM GEPR_TDICCIONARIO DICC
       WHERE UPPER(DICC.COD_IDIOMA) = UPPER(PAR$CODIDIOMA)
         AND UPPER(DICC.COD_FUNCIONALIDAD) = UPPER(PAR$CODFUNCIONALIDAD);
  
  END SOBTENERTRADUCCIONES;

  PROCEDURE SOBTENERBANCOSFECHAVALOR(PAR$NUMPAGINA     IN INTEGER,
                                     PAR$MAXPORPAGINA  IN INTEGER,
                                     PAR$INCLUIRPAGINA IN INTEGER,
                                     PAR$CODCLIENTE    IN VARCHAR2,
                                     PAR$DESCRIPCION   IN VARCHAR2,
                                     PAR$RESBANCOS     OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$RESBANCOS FOR
      WITH BANKS AS
       (SELECT ROWNUM NUM, COD_CLIENTE, DES_CLIENTE
          FROM GEPR_TCLIENTE
         WHERE OID_CLIENTE IN
               (SELECT DISTINCT (OID_CLIENTE) FROM SAPR_TACREDITACION)
           AND ((TRIM(PAR$CODCLIENTE) IS NULL) OR
               (COD_CLIENTE = PAR$CODCLIENTE))
           AND ((TRIM(PAR$DESCRIPCION) IS NULL) OR
               (UPPER(DES_CLIENTE) LIKE
               '%' || UPPER(PAR$DESCRIPCION) || '%')))
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM BANKS) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             CEIL((SELECT COUNT(*) FROM BANKS) / PAR$MAXPORPAGINA) NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE, C2.NUM, C2.COD_CLIENTE, C2.DES_CLIENTE
        FROM (SELECT * FROM BANKS) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERBANCOSFECHAVALOR;

  -- GRAFICO DE MAES 1
  PROCEDURE STORTAMAESXCLIENTE(PAR$OIDPLANIFICACION IN VARCHAR2,
                               PAR$OIDCLIENTE       IN VARCHAR2,
                               PAR$MAES             OUT SYS_REFCURSOR,
                               PAR$MAESDELEG        OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$MAES FOR
      SELECT CODIGO_CLIENTE, DESCRIPCION, CANTIDAD_MAES
        FROM (SELECT MAES.*, ROWNUM NUM
                FROM (SELECT CLI.COD_CLIENTE AS CODIGO_CLIENTE,
                             CLI.DES_CLIENTE AS DESCRIPCION,
                             COUNT(1) AS CANTIDAD_MAES
                        from SAPR_TPLANXMAQUINA PLXMQ
                       INNER JOIN GEPR_TPUNTO_SERVICIO SER
                          ON PLXMQ.OID_MAQUINA = SER.OID_MAQUINA
                       INNER JOIN GEPR_TSUBCLIENTE SUB
                          ON SER.OID_SUBCLIENTE = SUB.OID_SUBCLIENTE
                       INNER JOIN GEPR_TCLIENTE CLI
                          ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                       WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION
                       GROUP BY CLI.COD_CLIENTE, CLI.DES_CLIENTE
                       ORDER BY 3 DESC) MAES)
       WHERE NUM <= NVL((SELECT CM.NEC_DELEGACIONES_DISTRIBUCION
                          FROM SAPR_TCONFIG_CONSULTAS_MAE CM
                         WHERE CM.OID_CLIENTE = PAR$OIDCLIENTE),
                        5)
      UNION ALL
      SELECT NULL CODIGO_CLIENTE, 'OTROS' DESCRIPCION, SUM(CANTIDAD_MAES)
        FROM (SELECT MAES.*, ROWNUM NUM
                FROM (SELECT CLI.COD_CLIENTE AS CODIGO_CLIENTE,
                             CLI.DES_CLIENTE AS DESCRIPCION,
                             COUNT(1) AS CANTIDAD_MAES
                        from SAPR_TPLANXMAQUINA PLXMQ
                       INNER JOIN GEPR_TPUNTO_SERVICIO SER
                          ON PLXMQ.OID_MAQUINA = SER.OID_MAQUINA
                       INNER JOIN GEPR_TSUBCLIENTE SUB
                          ON SER.OID_SUBCLIENTE = SUB.OID_SUBCLIENTE
                       INNER JOIN GEPR_TCLIENTE CLI
                          ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                       WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION
                       GROUP BY CLI.COD_CLIENTE, CLI.DES_CLIENTE
                       ORDER BY 3 DESC) MAES)
       WHERE NUM > NVL((SELECT CM.NEC_DELEGACIONES_DISTRIBUCION
                         FROM SAPR_TCONFIG_CONSULTAS_MAE CM
                        WHERE CM.OID_CLIENTE = PAR$OIDCLIENTE),
                       5);
  
    OPEN PAR$MAESDELEG FOR
      SELECT CODIGO_DELEGACION, DESCRIPCION, CANTIDAD_MAES
        FROM (SELECT MAES.*, ROWNUM NUM
                FROM (SELECT DE.COD_DELEGACION AS CODIGO_DELEGACION,
                             DE.DES_DELEGACION AS DESCRIPCION,
                             COUNT(1) AS CANTIDAD_MAES
                        from SAPR_TPLANXMAQUINA PLXMQ
                       INNER JOIN SAPR_TMAQUINA MQ
                          ON PLXMQ.OID_MAQUINA = MQ.OID_MAQUINA
                       INNER JOIN GEPR_TSECTOR SEC
                          ON MQ.OID_SECTOR = SEC.OID_SECTOR
                       INNER JOIN GEPR_TPLANTA PL
                          ON SEC.OID_PLANTA = PL.OID_PLANTA
                       INNER JOIN GEPR_TDELEGACION DE
                          ON PL.OID_DELEGACION = DE.OID_DELEGACION
                       WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION
                       GROUP BY DE.COD_DELEGACION, DE.DES_DELEGACION
                       ORDER BY 3 DESC) MAES)
       WHERE NUM <= NVL((SELECT CM.NEC_DELEGACIONES_DISTRIBUCION
                          FROM SAPR_TCONFIG_CONSULTAS_MAE CM
                         WHERE CM.OID_CLIENTE = PAR$OIDCLIENTE),
                        5)
      UNION ALL
      SELECT NULL CODIGO_DELEGACION,
             'OTROS' DESCRIPCION,
             SUM(CANTIDAD_MAES)
        FROM (SELECT MAES.*, ROWNUM NUM
                FROM (SELECT DE.COD_DELEGACION AS CODIGO_DELEGACION,
                             DE.DES_DELEGACION AS DESCRIPCION,
                             COUNT(1) AS CANTIDAD_MAES
                        from SAPR_TPLANXMAQUINA PLXMQ
                       INNER JOIN SAPR_TMAQUINA MQ
                          ON PLXMQ.OID_MAQUINA = MQ.OID_MAQUINA
                       INNER JOIN GEPR_TSECTOR SEC
                          ON MQ.OID_SECTOR = SEC.OID_SECTOR
                       INNER JOIN GEPR_TPLANTA PL
                          ON SEC.OID_PLANTA = PL.OID_PLANTA
                       INNER JOIN GEPR_TDELEGACION DE
                          ON PL.OID_DELEGACION = DE.OID_DELEGACION
                       WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION
                       GROUP BY DE.COD_DELEGACION, DE.DES_DELEGACION
                       ORDER BY 3 DESC) MAES)
       WHERE NUM > NVL((SELECT CM.NEC_DELEGACIONES_DISTRIBUCION
                         FROM SAPR_TCONFIG_CONSULTAS_MAE CM
                        WHERE CM.OID_CLIENTE = PAR$OIDCLIENTE),
                       5);
  
  END STORTAMAESXCLIENTE;

  -- GRAFICO DE MAES 2
  -- 55EEE08F4AD6301EE0535A00530AEF7E
  --237
  PROCEDURE STORTAMAESXDELEGACION(PAR$OIDPLANIFICACION IN VARCHAR2,
                                  PAR$OIDCLIENTE       IN VARCHAR2,
                                  PAR$MAES             OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$MAES FOR
      SELECT *
        FROM (SELECT DE.COD_DELEGACION AS CODIGO_DELEGACION,
                     DE.DES_DELEGACION AS DESCRIPCION,
                     COUNT(1) AS CANTIDAD_MAES
                from SAPR_TPLANXMAQUINA PLXMQ
               INNER JOIN SAPR_TMAQUINA MQ
                  ON PLXMQ.OID_MAQUINA = MQ.OID_MAQUINA
               INNER JOIN GEPR_TSECTOR SEC
                  ON MQ.OID_SECTOR = SEC.OID_SECTOR
               INNER JOIN GEPR_TPLANTA PL
                  ON SEC.OID_PLANTA = PL.OID_PLANTA
               INNER JOIN GEPR_TDELEGACION DE
                  ON PL.OID_DELEGACION = DE.OID_DELEGACION
               WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION
               GROUP BY DE.COD_DELEGACION, DE.DES_DELEGACION
               ORDER BY 3 DESC) MAES
       WHERE ROWNUM <= NVL((SELECT CM.NEC_DELEGACIONES_DISTRIBUCION
                             FROM SAPR_TCONFIG_CONSULTAS_MAE CM
                            WHERE CM.OID_CLIENTE = PAR$OIDCLIENTE),
                           5);
  
  END STORTAMAESXDELEGACION;

  --MODAL DESDE EL GRAFICO DE MAES X CLIENTE
  PROCEDURE SOBTENERSALDOSDESCLI(PAR$OIDCLIENTES      IN VARCHAR2,
                                 PAR$OIDPLANIFICACION IN VARCHAR2,
                                 PAR$SALDOSDESCLI     OUT SYS_REFCURSOR) AS
  
  BEGIN
  
    OPEN PAR$SALDOSDESCLI FOR
      WITH VALIDADOS AS
       (SELECT *
          FROM (SELECT CLI.COD_CLIENTE AS CODIGO_CLIENTE,
                       CLI.DES_CLIENTE AS DESCRIPCION,
                       DIV.COD_ISO_DIVISA AS DIVISA,
                       SUM(SAL.NUM_IMPORTE) AS SALDO
                  from SAPR_TPLANXMAQUINA PLXMQ
                 INNER JOIN GEPR_TPUNTO_SERVICIO SER
                    ON PLXMQ.OID_MAQUINA = SER.OID_MAQUINA
                 INNER JOIN GEPR_TSUBCLIENTE SUB
                    ON SER.OID_SUBCLIENTE = SUB.OID_SUBCLIENTE
                 INNER JOIN GEPR_TCLIENTE CLI
                    ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                 INNER JOIN SAPR_VCUENTA CUE
                    ON CLI.OID_CLIENTE = CUE.OID_CLIENTE
                 INNER JOIN SAPR_TSALDO_EFECTIVO SAL
                    ON CUE.OID_CUENTA = SAL.OID_CUENTA_SALDO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON SAL.OID_DIVISA = DIV.OID_DIVISA
                -- WHERE CLI.COD_CLIENTE IN () -- LISTA DE CLIENTES QUE SE VISUALIZARON EN LAS TORTAS
                 WHERE INSTR(PAR$OIDCLIENTES, ',' || CLI.COD_CLIENTE || ',') > 0
                   AND CUE.COD_CANAL IN ('ING', 'DEB') -- VALORES FIJOS - NO SE DEBEN MODIFICAR                 
                   AND SAL.BOL_DISPONIBLE = 1
                 GROUP BY CLI.COD_CLIENTE,
                          CLI.DES_CLIENTE,
                          SAL.BOL_DISPONIBLE,
                          DIV.COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                             REALES,
                                                                             'DOL' AS
                                                                             DOLARES,
                                                                             'EUR' AS
                                                                             EUROS))),
      VALORESACREDITAR AS
       (SELECT *
          FROM (SELECT COD_CLIENTE AS CODIGO_CLIENTE,
                       COD_ISO_DIVISA AS DIVISA,
                       MAX(IMPORTE) AS SALDO
                  FROM (select TCAL.COD_TIPO_CALCULO,
                               CLI.COD_CLIENTE,
                               des_cliente,
                               DIV.COD_ISO_DIVISA,
                               sum(EFE.NUM_IMPORTE) AS IMPORTE
                          from SAPR_TCALCULO_EFECTIVO EFE
                         INNER JOIN GEPR_TDIVISA DIV
                            ON EFE.OID_DIVISA = DIV.OID_DIVISA
                         INNER JOIN SAPR_TPERIODO PE
                            ON EFE.OID_PERIODO = PE.OID_PERIODO
                         INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                            ON TIPE.OID_TIPO_PERIODO = PE.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                         INNER JOIN GEPR_TPUNTO_SERVICIO SER
                            ON PE.OID_MAQUINA = SER.OID_MAQUINA
                         INNER JOIN SAPR_TESTADO_PERIODO ESPE
                            ON PE.OID_ESTADO_PERIODO = PE.OID_ESTADO_PERIODO
                         INNER JOIN SAPR_TTIPO_CALCULO TCAL
                            ON TCAL.OID_TIPO_CALCULO = EFE.OID_TIPO_CALCULO
                         INNER JOIN GEPR_TSUBCLIENTE SUB
                            ON SER.OID_SUBCLIENTE = SUB.OID_SUBCLIENTE
                         INNER JOIN GEPR_TCLIENTE CLI
                            ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                         WHERE 1 = 1
                           AND PE.OID_PLANIFICACION = PAR$OIDPLANIFICACION -- Planificacion seleccionada
                           AND EFE.BOL_DISPONIBLE = '1'
                           AND ESPE.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PE.oid_acreditacion IS NULL
                           AND INSTR(PAR$OIDCLIENTES,
                                     ',' || CLI.COD_CLIENTE || ',') > 0 -- LISTA DE CLIENTES QUE SE VISUALIZARON EN LAS TORTAS
                         group by TCAL.COD_TIPO_CALCULO,
                                  CLI.COD_CLIENTE,
                                  des_cliente,
                                  DIV.COD_ISO_DIVISA)
                 GROUP BY COD_CLIENTE, des_cliente, COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                                                   ACRED_REALES,
                                                                                                   'DOL' AS
                                                                                                   ACRED_DOLARES,
                                                                                                   'EUR' AS
                                                                                                   ACRED_EUROS))),
      DEPOSITOS AS
       (SELECT *
          FROM (SELECT CLI.COD_CLIENTE AS CODIGO_CLIENTE,
                       DIV.COD_ISO_DIVISA AS DIVISA,
                       SUM(SAL.NUM_IMPORTE) AS SALDO
                  from SAPR_TPLANXMAQUINA PLXMQ
                 INNER JOIN GEPR_TPUNTO_SERVICIO SER
                    ON PLXMQ.OID_MAQUINA = SER.OID_MAQUINA
                 INNER JOIN GEPR_TSUBCLIENTE SUB
                    ON SER.OID_SUBCLIENTE = SUB.OID_SUBCLIENTE
                 INNER JOIN GEPR_TCLIENTE CLI
                    ON SUB.OID_CLIENTE = CLI.OID_CLIENTE
                 INNER JOIN SAPR_VCUENTA CUE
                    ON CLI.OID_CLIENTE = CUE.OID_CLIENTE
                 INNER JOIN SAPR_TSALDO_EFECTIVO SAL
                    ON CUE.OID_CUENTA = SAL.OID_CUENTA_SALDO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON SAL.OID_DIVISA = DIV.OID_DIVISA
                --WHERE CLI.COD_CLIENTE IN (PAR$OIDCLIENTES) -- LISTA DE CLIENTES QUE SE VISUALIZARON EN LAS TORTAS
                 WHERE INSTR(PAR$OIDCLIENTES, ',' || CLI.COD_CLIENTE || ',') > 0
                   AND CUE.COD_CANAL IN ('ING', 'DEB') -- VALORES FIJOS - NO SE DEBEN MODIFICAR  
                   AND SAL.BOL_DISPONIBLE = 0
                 GROUP BY CLI.COD_CLIENTE,
                          CLI.DES_CLIENTE,
                          SAL.BOL_DISPONIBLE,
                          DIV.COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                             DEP_REALES,
                                                                             'DOL' AS
                                                                             DEP_DOLARES,
                                                                             'EUR' AS
                                                                             DEP_EUROS)))
      SELECT VAL.*,
             DEP.DEP_REALES,
             DEP.DEP_DOLARES,
             DEP.DEP_EUROS,
             VAAC.ACRED_REALES,
             VAAC.ACRED_DOLARES,
             VAAC.ACRED_EUROS
        FROM VALIDADOS VAL
        LEFT OUTER JOIN DEPOSITOS DEP
          ON VAL.CODIGO_CLIENTE = DEP.CODIGO_CLIENTE
        LEFT OUTER JOIN VALORESACREDITAR VAAC
          ON VAL.CODIGO_CLIENTE = VAAC.CODIGO_CLIENTE;
  
  END SOBTENERSALDOSDESCLI;

  -- MODAL DESDE EL GRAFICO DE MAES X DELEGACION
  PROCEDURE SOBTENERSALDOSDESDELEG(PAR$OIDDELEGACIONES  IN VARCHAR2,
                                   PAR$OIDPLANIFICACION IN VARCHAR2,
                                   PAR$SALDOSDESDEL     OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$SALDOSDESDEL FOR
      WITH VALIDADOS AS
       (SELECT *
          FROM (SELECT DE.COD_DELEGACION AS CODIGO_DELEGACION,
                       DE.DES_DELEGACION AS DESCRIPCION,
                       --SAL.BOL_DISPONIBLE as VALIDADO,
                       DIV.COD_ISO_DIVISA AS DIVISA,
                       SUM(SAL.NUM_IMPORTE) AS SALDO
                  from SAPR_TPLANXMAQUINA PLXMQ
                 INNER JOIN SAPR_TMAQUINA MQ
                    ON PLXMQ.OID_MAQUINA = MQ.OID_MAQUINA
                 INNER JOIN GEPR_TSECTOR SEC
                    ON MQ.OID_SECTOR = SEC.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PL
                    ON SEC.OID_PLANTA = PL.OID_PLANTA
                 INNER JOIN GEPR_TDELEGACION DE
                    ON PL.OID_DELEGACION = DE.OID_DELEGACION
                 INNER JOIN SAPR_VCUENTA CUE
                    ON SEC.OID_SECTOR = CUE.OID_SECTOR
                 INNER JOIN SAPR_TSALDO_EFECTIVO SAL
                    ON CUE.OID_CUENTA = SAL.OID_CUENTA_SALDO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON SAL.OID_DIVISA = DIV.OID_DIVISA
                --WHERE DE.COD_DELEGACION IN (PAR$OIDDELEGACIONES) -- LISTA DE DELEGACIONES QUE SE VISUALIZARON EN LAS TORTAS
                 WHERE INSTR(PAR$OIDDELEGACIONES,
                             ',' || DE.COD_DELEGACION || ',') > 0
                   AND CUE.COD_CANAL IN ('ING', 'DEB') -- VALORES FIJOS - NO SE DEBEN MODIFICAR
                   AND SAL.BOL_DISPONIBLE = 1
                 GROUP BY DE.COD_DELEGACION,
                          DE.DES_DELEGACION,
                          SAL.BOL_DISPONIBLE,
                          DIV.COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                             REALES,
                                                                             'DOL' AS
                                                                             DOLARES,
                                                                             'EUR' AS
                                                                             EUROS))),
      VALORESACREDITAR AS
       (SELECT *
          FROM (SELECT COD_DELEGACION as CODIGO_DELEGACION,
                       DES_DELEGACION AS DESCRIPCION,
                       COD_ISO_DIVISA AS DIVISA,
                       MAX(IMPORTE) AS SALDO
                  FROM (select TCAL.COD_TIPO_CALCULO,
                               DELE.COD_DELEGACION,
                               DELE.DES_DELEGACION,
                               DIV.COD_ISO_DIVISA,
                               sum(EFE.NUM_IMPORTE) AS IMPORTE
                          from SAPR_TCALCULO_EFECTIVO EFE
                         INNER JOIN GEPR_TDIVISA DIV
                            ON EFE.OID_DIVISA = DIV.OID_DIVISA
                         INNER JOIN SAPR_TPERIODO PE
                            ON EFE.OID_PERIODO = PE.OID_PERIODO
                         INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                            ON TIPE.OID_TIPO_PERIODO = PE.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                         INNER JOIN SAPR_TMAQUINA MQ
                            ON PE.OID_MAQUINA = MQ.OID_MAQUINA
                         INNER JOIN SAPR_TESTADO_PERIODO ESPE
                            ON PE.OID_ESTADO_PERIODO = PE.OID_ESTADO_PERIODO
                         INNER JOIN SAPR_TTIPO_CALCULO TCAL
                            ON TCAL.OID_TIPO_CALCULO = EFE.OID_TIPO_CALCULO
                         INNER JOIN GEPR_TSECTOR SEC
                            ON MQ.OID_SECTOR = SEC.OID_SECTOR
                         inner join GEPR_TPLANTA PLA
                            ON SEC.OID_PLANTA = PLA.OID_PLANTA
                         inner join GEPR_TDELEGACION DELE
                            ON PLA.OID_DELEGACION = DELE.OID_DELEGACION
                         WHERE 1 = 1
                           AND PE.OID_PLANIFICACION = PAR$OIDPLANIFICACION -- Planificacion seleccionada
                           AND EFE.BOL_DISPONIBLE = '1'
                           AND ESPE.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PE.oid_acreditacion IS NULL
                           AND INSTR(PAR$OIDDELEGACIONES,
                                     ',' || DELE.COD_DELEGACION || ',') > 0 -- LISTA DE DELEGACIONES QUE SE VISUALIZARON EN LAS TORTAS
                         group by TCAL.COD_TIPO_CALCULO,
                                  DELE.COD_DELEGACION,
                                  DELE.DES_DELEGACION,
                                  DIV.COD_ISO_DIVISA)
                 GROUP BY COD_DELEGACION, DES_DELEGACION, COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                                                         ACRED_REALES,
                                                                                                         'DOL' AS
                                                                                                         ACRED_DOLARES,
                                                                                                         'EUR' AS
                                                                                                         ACRED_EUROS))),
      DEPOSITOS AS
       (SELECT *
          FROM (SELECT DE.COD_DELEGACION AS CODIGO_DELEGACION,
                       DE.DES_DELEGACION AS DESCRIPCION,
                       --SAL.BOL_DISPONIBLE as VALIDADO,
                       DIV.COD_ISO_DIVISA AS DIVISA,
                       SUM(SAL.NUM_IMPORTE) AS SALDO
                  from SAPR_TPLANXMAQUINA PLXMQ
                 INNER JOIN SAPR_TMAQUINA MQ
                    ON PLXMQ.OID_MAQUINA = MQ.OID_MAQUINA
                 INNER JOIN GEPR_TSECTOR SEC
                    ON MQ.OID_SECTOR = SEC.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PL
                    ON SEC.OID_PLANTA = PL.OID_PLANTA
                 INNER JOIN GEPR_TDELEGACION DE
                    ON PL.OID_DELEGACION = DE.OID_DELEGACION
                 INNER JOIN SAPR_VCUENTA CUE
                    ON SEC.OID_SECTOR = CUE.OID_SECTOR
                 INNER JOIN SAPR_TSALDO_EFECTIVO SAL
                    ON CUE.OID_CUENTA = SAL.OID_CUENTA_SALDO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON SAL.OID_DIVISA = DIV.OID_DIVISA
                --WHERE DE.COD_DELEGACION IN (PAR$OIDDELEGACIONES) -- LISTA DE DELEGACIONES QUE SE VISUALIZARON EN LAS TORTAS
                 WHERE INSTR(PAR$OIDDELEGACIONES,
                             ',' || DE.COD_DELEGACION || ',') > 0
                   AND CUE.COD_CANAL IN ('ING', 'DEB') -- VALORES FIJOS - NO SE DEBEN MODIFICAR
                   AND SAL.BOL_DISPONIBLE = 0
                 GROUP BY DE.COD_DELEGACION,
                          DE.DES_DELEGACION,
                          SAL.BOL_DISPONIBLE,
                          DIV.COD_ISO_DIVISA) PIVOT(SUM(SALDO) FOR DIVISA IN('REA' AS
                                                                             DEP_REALES,
                                                                             'DOL' AS
                                                                             DEP_DOLARES,
                                                                             'EUR' AS
                                                                             DEP_EUROS)))
      SELECT VAL.*,
             DEP.DEP_REALES,
             DEP.DEP_DOLARES,
             DEP.DEP_EUROS,
             VAAC.ACRED_REALES,
             VAAC.ACRED_DOLARES,
             VAAC.ACRED_EUROS
        FROM VALIDADOS VAL
        LEFT OUTER JOIN DEPOSITOS DEP
          ON VAL.CODIGO_DELEGACION = DEP.CODIGO_DELEGACION
        LEFT OUTER JOIN VALORESACREDITAR VAAC
          ON VAL.CODIGO_DELEGACION = VAAC.CODIGO_DELEGACION;
  END SOBTENERSALDOSDESDELEG;

  --combo box--
  PROCEDURE SOBTENERPLANIFICACIONES(PAR$OIDCLIENTE      IN VARCHAR2,
                                    PAR$PLANIFICACIONES OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$PLANIFICACIONES FOR
      SELECT PL.OID_PLANIFICACION,
             DECODE(CM.OID_PLANIFICACION_DEFAULT, NULL, 0, 1) ESDEFAULT,
             PL.DES_PLANIFICACION
        FROM SAPR_TPLANIFICACION PL, SAPR_TCONFIG_CONSULTAS_MAE CM
       WHERE CM.OID_CLIENTE(+) = PL.OID_CLIENTE
         AND PL.OID_CLIENTE = PAR$OIDCLIENTE
         AND CM.OID_PLANIFICACION_DEFAULT(+) = PL.OID_PLANIFICACION
       ORDER BY PL.DES_PLANIFICACION ASC;
  
  END SOBTENERPLANIFICACIONES;

  --OID_ACREDITACION:"5DDA7F0F75464EADE0535B00530ACD9C"
  --OID_MAQUINA:"54D6F4777CB94808E0535B00530A3CD9"
  PROCEDURE SOBTENERDETACREDMAE(PAR$OIDACREDITACION IN VARCHAR2,
                                PAR$OIDMAQUINA      IN VARCHAR2,
                                PAR$NUMPAGINA       IN INTEGER,
                                PAR$MAXPORPAGINA    IN INTEGER,
                                PAR$INCLUIRPAGINA   IN INTEGER,
                                PAR$ACREDMAES       OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$ACREDMAES FOR
      WITH MAESACRED AS
       (SELECT *
          FROM (SELECT DOC.GMT_CREACION,
                       DOC.FYH_GESTION,
                       FO.DES_FORMULARIO,
                       DOC.COD_EXTERNO,
                       DIV.COD_ISO_DIVISA,
                       SUM(EFE.NUM_IMPORTE) AS IMPORTE
                  FROM SAPR_TACREDITACION ACR
                 INNER JOIN SAPR_TPERIODO PER
                    ON ACR.OID_ACREDITACION = PER.OID_ACREDITACION
                 INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                    ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                 INNER JOIN SAPR_TPLANIFICACION PL
                    ON PER.OID_PLANIFICACION = PL.OID_PLANIFICACION
                 INNER JOIN SAPR_TPERIODOXDOCUMENTO PEXDO
                    ON PEXDO.OID_PERIODO = PER.OID_PERIODO
                 INNER JOIN SAPR_TDOCUMENTO DOC
                    ON PEXDO.OID_DOCUMENTO = DOC.OID_DOCUMENTO
                 INNER JOIN SAPR_TFORMULARIO FO
                    ON DOC.OID_FORMULARIO = FO.OID_FORMULARIO
                 INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFE
                    ON PEXDO.OID_DOCUMENTO = EFE.OID_DOCUMENTO
                 INNER JOIN GEPR_TDIVISA DIV
                    ON EFE.OID_DIVISA = DIV.OID_DIVISA
                 WHERE ACR.OID_ACREDITACION = PAR$OIDACREDITACION -- OID acreditacion 
                   AND PER.OID_MAQUINA = PAR$OIDMAQUINA -- OID de la MAE seleccionada
                 GROUP BY DOC.GMT_CREACION,
                          DOC.FYH_GESTION,
                          FO.DES_FORMULARIO,
                          DOC.COD_EXTERNO,
                          DIV.COD_ISO_DIVISA
                 order by 1 asc) PIVOT(SUM(IMPORTE) FOR COD_ISO_DIVISA IN('REA' AS
                                                                          REALES,
                                                                          'DOL' AS
                                                                          DOLARES,
                                                                          'EUR' AS
                                                                          EUROS)))
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM MAESACRED) NUM,
             NULL GMT_CREACION,
             NULL FYH_GESTION,
             NULL DES_FORMULARIO,
             NULL COD_EXTERNO,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'TP' CQUE,
             CEIL((SELECT COUNT(*) FROM MAESACRED) / PAR$MAXPORPAGINA) NUM,
             NULL GMT_CREACION,
             NULL FYH_GESTION,
             NULL DES_FORMULARIO,
             NULL COD_EXTERNO,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL GMT_CREACION,
             NULL FYH_GESTION,
             NULL DES_FORMULARIO,
             NULL COD_EXTERNO,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT NULL CQUE,
             C2.NUM,
             C2.GMT_CREACION,
             C2.FYH_GESTION,
             C2.DES_FORMULARIO,
             C2.COD_EXTERNO,
             C2.REALES,
             C2.DOLARES,
             C2.EUROS
        FROM (SELECT ROWNUM NUM,
                     GMT_CREACION,
                     FYH_GESTION,
                     DES_FORMULARIO,
                     COD_EXTERNO,
                     REALES,
                     DOLARES,
                     EUROS
                FROM (SELECT MAESACRED.GMT_CREACION,
                             MAESACRED.FYH_GESTION,
                             MAESACRED.DES_FORMULARIO,
                             MAESACRED.COD_EXTERNO,
                             MAESACRED.REALES,
                             MAESACRED.DOLARES,
                             MAESACRED.EUROS
                        FROM MAESACRED
                       ORDER BY MAESACRED.GMT_CREACION)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERDETACREDMAE;

  --5E08530F3675FE85E053DF110A0AF95B
  PROCEDURE SOBTENERSALDOSDESP(PAR$OIDPLANIFICACION IN VARCHAR2,
                               PAR$SALDOSDESP       OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$SALDOSDESP FOR
      select DIV.COD_ISO_DIVISA, sum(EFE.NUM_IMPORTE) NUM_IMPORTE
        from SAPR_TSALDO_EFECTIVO EFE
       INNER JOIN SAPR_VCUENTA CUE
          ON EFE.OID_CUENTA_SALDO = CUE.OID_CUENTA
       INNER JOIN GEPR_TDIVISA DIV
          ON EFE.OID_DIVISA = DIV.OID_DIVISA
       INNER JOIN GEPR_TSECTOR SC
          ON SC.OID_SECTOR = CUE.OID_SECTOR
       INNER JOIN SAPR_TMAQUINA MQ
          ON SC.OID_SECTOR = MQ.OID_SECTOR
       INNER JOIN SAPR_TPLANXMAQUINA PLXMQ
          ON MQ.OID_MAQUINA = PLXMQ.OID_MAQUINA
       WHERE PLXMQ.OID_PLANIFICACION = PAR$OIDPLANIFICACION -- Planificacion seleccionada
         AND CUE.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'RCBIL', 'RECMON')
         AND EFE.BOL_DISPONIBLE = '1'
       group by DIV.COD_ISO_DIVISA;
  
  END SOBTENERSALDOSDESP;

  PROCEDURE SOBTENERSALDOSAACREDITAR(PAR$OIDPLANIFICACION IN VARCHAR2,
                                     PAR$SALDOSACRED      OUT SYS_REFCURSOR) AS
  BEGIN
  
    OPEN PAR$SALDOSACRED FOR
      SELECT COD_ISO_DIVISA, MAX(IMPORTE) NUM_IMPORTE
        FROM (select TCAL.COD_TIPO_CALCULO,
                     DIV.COD_ISO_DIVISA,
                     sum(EFE.NUM_IMPORTE) AS IMPORTE
                from SAPR_TCALCULO_EFECTIVO EFE
               INNER JOIN GEPR_TDIVISA DIV
                  ON EFE.OID_DIVISA = DIV.OID_DIVISA
               INNER JOIN SAPR_TPERIODO PE
                  ON EFE.OID_PERIODO = PE.OID_PERIODO
               INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                  ON TIPE.OID_TIPO_PERIODO = PE.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
               INNER JOIN SAPR_TESTADO_PERIODO ESPE
                  ON PE.OID_ESTADO_PERIODO = PE.OID_ESTADO_PERIODO
               INNER JOIN SAPR_TTIPO_CALCULO TCAL
                  ON TCAL.OID_TIPO_CALCULO = EFE.OID_TIPO_CALCULO
               WHERE 1 = 1
                 AND PE.OID_PLANIFICACION = PAR$OIDPLANIFICACION -- Planificacion seleccionada
                 AND EFE.BOL_DISPONIBLE = '1'
                 AND ESPE.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PE.oid_acreditacion IS NULL
               group by TCAL.COD_TIPO_CALCULO, DIV.COD_ISO_DIVISA)
       GROUP BY COD_ISO_DIVISA;
  
  END SOBTENERSALDOSAACREDITAR;

  PROCEDURE SOBTENERCLIPLANIFICACION(PAR$NUMPAGINA     IN INTEGER,
                                     PAR$MAXPORPAGINA  IN INTEGER,
                                     PAR$INCLUIRPAGINA IN INTEGER,
                                     PAR$CLIENTES      OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN PAR$CLIENTES FOR
      WITH CLI AS
       (SELECT PLA.OID_CLIENTE,
               CLI.COD_CLIENTE,
               CLI.DES_CLIENTE,
               CM.DES_LOGO_PATH
          FROM SAPR_TPLANIFICACION PLA
         INNER JOIN GEPR_TCLIENTE CLI
            ON PLA.OID_CLIENTE = CLI.OID_CLIENTE
          LEFT OUTER JOIN SAPR_TCONFIG_CONSULTAS_MAE CM
            ON CM.OID_CLIENTE = CLI.OID_CLIENTE
         GROUP BY PLA.OID_CLIENTE,
                  CLI.COD_CLIENTE,
                  CLI.DES_CLIENTE,
                  CM.DES_LOGO_PATH)
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM CLI) NUM,
             NULL OID_CLIENTE,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'TP' CQUE,
             CEIL((SELECT COUNT(*) FROM CLI) / PAR$MAXPORPAGINA) NUM,
             NULL OID_CLIENTE,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL OID_CLIENTE,
             NULL COD_CLIENTE,
             NULL DES_CLIENTE,
             NULL DES_LOGO_PATH
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION
      SELECT NULL CQUE,
             C2.NUM,
             C2.OID_CLIENTE,
             C2.COD_CLIENTE,
             C2.DES_CLIENTE,
             C2.DES_LOGO_PATH
        FROM (SELECT ROWNUM NUM,
                     OID_CLIENTE,
                     COD_CLIENTE,
                     DES_CLIENTE,
                     DES_LOGO_PATH
                FROM (SELECT CLI.OID_CLIENTE,
                             CLI.COD_CLIENTE,
                             CLI.DES_CLIENTE,
                             CLI.DES_LOGO_PATH
                        FROM CLI
                       ORDER BY CLI.DES_CLIENTE)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERCLIPLANIFICACION;

  PROCEDURE SOBTENERDETALLEACREDPORDELEG(PAR$NUMPAGINA       IN INTEGER,
                                         PAR$MAXPORPAGINA    IN INTEGER,
                                         PAR$INCLUIRPAGINA   IN INTEGER,
                                         PAR$OIDACREDITACION IN VARCHAR,
                                         PAR$CODCLIENTE      IN VARCHAR2,
                                         PAR$CODDELEGACION   IN VARCHAR2,
                                         PAR$DETALLESACRED   OUT SYS_REFCURSOR) IS
  BEGIN
  
    OPEN PAR$DETALLESACRED FOR
      WITH DELEGACIONES AS
      /*  (SELECT '01' AS COD_DELEGACION,
                                 'NAZCA' AS DES_DELEGACION,
                                 '906500' AS REALES,
                                 '9065000' AS DOLARES,
                                 '90165000' AS EUROS
                            FROM DUAL
                          UNION ALL
                          SELECT '01' AS COD_DELEGACION,
                                 'FORMOSA' AS DES_DELEGACION,
                                 '906500' AS REALES,
                                 '9065000' AS DOLARES,
                                 '90165000' AS EUROS
                            FROM DUAL
                          UNION ALL
                          SELECT '01' AS COD_DELEGACION,
                                 'BARRAFUNDA' AS DES_DELEGACION,
                                 '906500' AS REALES,
                                 '9065000' AS DOLARES,
                                 '90165000' AS EUROS
                            FROM DUAL
                          UNION ALL
                          SELECT '01' AS COD_DELEGACION,
                                 'SAO PABLO' AS DES_DELEGACION,
                                 '906500' AS REALES,
                                 '9065000' AS DOLARES,
                                 '90165000' AS EUROS
                            FROM DUAL)*/
       (SELECT *
          FROM (select PL.COD_PLANTA AS COD_DELEGACION, --CODIGO DELEGACION
                       PL.OID_PLANTA AS OID_PLANTA, -- OID_PLANTA
                       PL.DES_PLANTA AS DES_DELEGACION, -- CODIGO PLANTA
                       ACR.FYH_ACREDITACION AS FYH_ACREDITACION,
                       UPPER(DIV.DES_DIVISA) AS DES_DIVISA,
                       SUM(EFE.NUM_IMPORTE) AS IMPORTE
                  from SAPR_TACREDITACION ACR
                 INNER JOIN SAPR_TPERIODO PE
                    ON ACR.OID_ACREDITACION = PE.OID_ACREDITACION
                 INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                    ON TIPE.OID_TIPO_PERIODO = PE.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                 INNER JOIN SAPR_TMAQUINA MQ
                    ON PE.OID_MAQUINA = MQ.OID_MAQUINA
                 INNER JOIN GEPR_TSECTOR SEC
                    ON MQ.OID_SECTOR = SEC.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PL
                    ON PL.OID_PLANTA = SEC.OID_PLANTA
                 INNER JOIN SAPR_TACREDITACION_EFECTIVO EFE
                    ON EFE.OID_ACREDITACION = ACR.OID_ACREDITACION
                 INNER JOIN GEPR_TDIVISA DIV
                    ON DIV.OID_DIVISA = EFE.OID_DIVISA
                 WHERE ACR.OID_ACREDITACION = PAR$OIDACREDITACION -- 5F8125D57E185C5BE0535B00530A9816 acreditacion selecccionada
                 GROUP BY PL.COD_PLANTA,
                          PL.OID_PLANTA,
                          PL.DES_PLANTA,
                          ACR.FYH_ACREDITACION,
                          UPPER(DIV.DES_DIVISA)) PIVOT(SUM(IMPORTE) FOR DES_DIVISA IN('REAL' AS
                                                                                      REALES,
                                                                                      'DOLAR' AS
                                                                                      DOLARES,
                                                                                      'EURO' AS
                                                                                      EUROS)))
      
      SELECT 'TR' CQUE,
             (SELECT COUNT(1) NUM FROM DELEGACIONES) NUM,
             NULL COD_DELEGACION,
             NULL OID_PLANTA,
             NULL DES_DELEGACION,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'TP' CQUE,
             CEIL((SELECT COUNT(*) FROM DELEGACIONES) / PAR$MAXPORPAGINA) NUM,
             NULL COD_DELEGACION,
             NULL OID_PLANTA,
             NULL DES_DELEGACION,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT 'PA' CQUE,
             PAR$NUMPAGINA NUM,
             NULL COD_DELEGACION,
             NULL OID_PLANTA,
             NULL DES_DELEGACION,
             NULL FYH_ACREDITACION,
             NULL REALES,
             NULL DOLARES,
             NULL EUROS
        FROM DUAL
       WHERE PAR$INCLUIRPAGINA = 1
      UNION ALL
      SELECT NULL CQUE,
             C2.NUM,
             C2.COD_DELEGACION,
             C2.OID_PLANTA,
             C2.DES_DELEGACION,
             C2.FYH_ACREDITACION,
             C2.REALES,
             C2.DOLARES,
             C2.EUROS
        FROM (SELECT ROWNUM NUM,
                     COD_DELEGACION,
                     OID_PLANTA,
                     DES_DELEGACION,
                     FYH_ACREDITACION,
                     REALES,
                     DOLARES,
                     EUROS
                FROM (SELECT DELEGACIONES.COD_DELEGACION,
                             DELEGACIONES.OID_PLANTA,
                             DELEGACIONES.DES_DELEGACION,
                             DELEGACIONES.FYH_ACREDITACION,
                             DELEGACIONES.REALES,
                             DELEGACIONES.DOLARES,
                             DELEGACIONES.EUROS
                        FROM DELEGACIONES
                       ORDER BY DES_DELEGACION ASC)) C2
       WHERE C2.NUM BETWEEN (PAR$NUMPAGINA * PAR$MAXPORPAGINA + 1) AND
             ((PAR$NUMPAGINA + 1) * PAR$MAXPORPAGINA);
  
  END SOBTENERDETALLEACREDPORDELEG;

END sapr_pconsultas_atm_###VERSION###;
/
