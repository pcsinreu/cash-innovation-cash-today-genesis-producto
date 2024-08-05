DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPROCESO                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPROCESO';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPROCESO');
    EXECUTE IMMEDIATE q'[create table SAPR_TPROCESO 
    (
      OID_PROCESO          VARCHAR2(36)         not null,
      COD_PROCESO          VARCHAR2(15)         not null,
      DES_PROCESO          VARCHAR2(100)        not null,
      GMT_CREACION         TIMESTAMP WITH TIME ZONE not null,
      DES_USUARIO_CREACION VARCHAR2(50)         not null,
      GMT_MODIFICACION     TIMESTAMP WITH TIME ZONE not null,
      DES_USUARIO_MODIFICACION VARCHAR2(50)         not null,
      constraint PK_SAPR_TPROCESO primary key (OID_PROCESO),
      constraint AK_KEY_2_SAPR_TPR unique (COD_PROCESO)
    )]';
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Agregar nueva tabla SAPR_TPROCESO - ' ||
                            sqlerrm);
END;
/

DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPROCESOXPLANIFICACION                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPROCESOXPLANIFICACION';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPROCESOXPLANIFICACION');
    EXECUTE IMMEDIATE q'[create table SAPR_TPROCESOXPLANIFICACION 
    (
      OID_PROCESOXPLANIFICACION VARCHAR2(36)         not null,
      OID_PROCESO          VARCHAR2(36)         not null,
      OID_PLANIFICACION    VARCHAR2(36)         not null,
      GMT_CREACION         TIMESTAMP WITH TIME ZONE not null,
      DES_USUARIO_CREACION VARCHAR2(50)         not null,
      GMT_MODIFICACION     TIMESTAMP WITH TIME ZONE not null,
      DES_USUARIO_MODIFICACION VARCHAR2(50)         not null,
      constraint PK_SAPR_TPROCESOXPLANIFICACION primary key (OID_PROCESOXPLANIFICACION),
      constraint FK_SAPR_TPR_REFERENCE_SAPR_TPR foreign key (OID_PROCESO)
                        references SAPR_TPROCESO (OID_PROCESO),
      constraint FK_SAPR_TPR_REFERENCE_SAPR_TPL foreign key (OID_PLANIFICACION)
                        references SAPR_TPLANIFICACION (OID_PLANIFICACION)
    )]';
 
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Agregar nueva tabla SAPR_TPROCESOXPLANIFICACION - ' ||
                            sqlerrm);
END;
/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TTIPO_PERIODO                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TTIPO_PERIODO';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TTIPO_PERIODO');
    EXECUTE IMMEDIATE q'[create table SAPR_TTIPO_PERIODO 
    (
       OID_TIPO_PERIODO     VARCHAR2(36)         not null,
       COD_TIPO_PERIODO     VARCHAR2(2)          not null,
       DES_TIPO_PERIODO     VARCHAR2(100)        not null,
       GMT_CREACION         TIMESTAMP WITH TIME ZONE not null,
       DES_USUARIO_CREACION VARCHAR2(50)         not null,
       GMT_MODIFICACION     TIMESTAMP WITH TIME ZONE not null,
       DES_USUARIO_MODIFICACION VARCHAR2(50)         not null,
       constraint PK_SAPR_TTIPO_PERIODO primary key (OID_TIPO_PERIODO),
       constraint AK_KEY_2_SAPR_TTI unique (COD_TIPO_PERIODO)
    )]';
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Agregar nueva tabla SAPR_TTIPO_PERIODO - ' ||
                            sqlerrm);
END;
/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPERIODO_RELACION                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPERIODO_RELACION';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPERIODO_RELACION');
    EXECUTE IMMEDIATE q'[create table SAPR_TPERIODO_RELACION 
    (
       OID_PERIODO_RELACION VARCHAR2(36)         not null,
       OID_PERIODO          VARCHAR2(36)         not null,
       OID_PERIODO_RELACIONADO VARCHAR2(36)         not null,
       GMT_CREACION         TIMESTAMP WITH TIME ZONE not null,
       DES_USUARIO_CREACION VARCHAR2(50)         not null,
       GMT_MODIFICACION     TIMESTAMP WITH TIME ZONE not null,
       DES_USUARIO_MODIFICACION VARCHAR2(50)         not null,
       constraint PK_SAPR_TPERIODO_RELACION primary key (OID_PERIODO_RELACION),
       constraint FK_SAPR_TPE_REFERENCE_SAPR_TPE foreign key (OID_PERIODO_RELACIONADO)
                        references SAPR_TPERIODO (OID_PERIODO),
       constraint FK_SAPR_TPE_REFERENCE_SAPR_TP2 foreign key (OID_PERIODO)
                        references SAPR_TPERIODO (OID_PERIODO)
    )]';
 
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Agregar nueva tabla SAPR_TPERIODO_RELACION - ' ||
                            sqlerrm);
END;
/
-- Agregar tipos de periodo a SAPR_TTIPO_PERIODO
DECLARE
  var$cod_tipo_periodo  VARCHAR2(2);
  var$des_tipo_periodo  VARCHAR2(100);
  var$gmt_zero            VARCHAR2(80)  := sys_extract_utc(current_timestamp) || ' +00:00';
  var$usuario             VARCHAR2(50)  := 'GENESIS_PRODUCTO';
  var$existe              NUMBER;
BEGIN
  var$cod_tipo_periodo  := 'AC';
  var$des_tipo_periodo  := 'Acreditacion';
  
  /* Buscamos si existe el tipo de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = var$cod_tipo_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TTIPO_PERIODO (OID_TIPO_PERIODO, COD_TIPO_PERIODO, DES_TIPO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_tipo_periodo, var$des_tipo_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_tipo_periodo  := 'RE';
  var$des_tipo_periodo  := 'Recojo';
  
  /* Buscamos si existe el tipo de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = var$cod_tipo_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TTIPO_PERIODO (OID_TIPO_PERIODO, COD_TIPO_PERIODO, DES_TIPO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_tipo_periodo, var$des_tipo_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_tipo_periodo  := 'BO';
  var$des_tipo_periodo  := 'Boveda';
  
  /* Buscamos si existe el tipo de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = var$cod_tipo_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TTIPO_PERIODO (OID_TIPO_PERIODO, COD_TIPO_PERIODO, DES_TIPO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_tipo_periodo, var$des_tipo_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Agregar nuevos tipos de periodo en SAPR_TTIPO_PERIODO" - ' ||
                            sqlerrm);
END;
/
/*Reemplazo el trigger AK_SAPR_TPERIODOXDOCUMENTO_1 de SAPR_TPERIODOXDOCUMENTO */
DECLARE
var$existe              NUMBER;
BEGIN
        SELECT COUNT(1) INTO var$existe FROM ALL_CONSTRAINTS WHERE constraint_name = 'AK_SAPR_TPERIODOXDOCUMENTO_1';
        IF var$existe = 1 THEN 
            EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPERIODOXDOCUMENTO drop constraint AK_SAPR_TPERIODOXDOCUMENTO_1]';

        END IF;
        EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPERIODOXDOCUMENTO add constraint AK_SAPR_TPERIODOXDOCUMENTO_1 unique (OID_PERIODO, OID_DOCUMENTO)]';
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Archivo: 007_SCRIPT_FECHA_VALOR_TIPOS.sql Script: Reemplazo el trigger AK_SAPR_TPERIODOXDOCUMENTO_1 de SAPR_TPERIODOXDOCUMENTO" - ' ||
                            sqlerrm);
END;
/

/*Reemplazo el trigger TRG_SAPR_TPERIODOXDOC_LIMITE*/
create or replace TRIGGER TRG_SAPR_TPERIODOXDOC_LIMITE
 AFTER INSERT ON SAPR_TPERIODOXDOCUMENTO FOR EACH ROW
DECLARE
  V_LIMITE_SUPERADO NUMBER(1);

BEGIN

    WITH DOCUMENTO_VALOR AS (
    SELECT OID_DIVISA, SUM(NUM_IMPORTE) NUM_IMPORTE FROM SAPR_TTRANSACCION_EFECTIVO TREF 
      WHERE OID_DOCUMENTO = :NEW.OID_DOCUMENTO
      GROUP BY OID_DIVISA
    ),
    DIVISAS_LIMITES AS (
         SELECT
            LIMI.OID_DIVISA,
            LIMI.NUM_LIMITE,
            SUM(CAEF.NUM_IMPORTE) NUM_IMPORTE,
            '1' ORDEN
         FROM
            SAPR_TLIMITE LIMI
         INNER JOIN SAPR_TPERIODO PERI ON LIMI.OID_MAQUINA = PERI.OID_MAQUINA AND
                                          LIMI.BOL_ACTIVO = '1'
         INNER JOIN SAPR_TTIPO_PERIODO TPERI ON TPERI.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
         INNER JOIN SAPR_TCALCULO_EFECTIVO CAEF ON PERI.OID_PERIODO = CAEF.OID_PERIODO AND
                                          LIMI.OID_DIVISA = CAEF.OID_DIVISA
         INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'AB'
         WHERE
               PERI.OID_PERIODO = :NEW.OID_PERIODO
               AND TPERI.COD_TIPO_PERIODO = 'AC'
         GROUP BY
               LIMI.OID_DIVISA,
               LIMI.NUM_LIMITE
         UNION
         SELECT
               LIMI.OID_DIVISA,
               LIMI.NUM_LIMITE,
               SUM(CAEF.NUM_IMPORTE) NUM_IMPORTE,
               '2' ORDEN
         FROM
            SAPR_TLIMITE LIMI
         INNER JOIN SAPR_TPERIODO PERI ON LIMI.OID_PLANIFICACION = PERI.OID_PLANIFICACION AND
               LIMI.BOL_ACTIVO = '1'
         INNER JOIN SAPR_TTIPO_PERIODO TPERI ON TPERI.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
         INNER JOIN SAPR_TCALCULO_EFECTIVO CAEF ON PERI.OID_PERIODO = CAEF.OID_PERIODO AND
               LIMI.OID_DIVISA = CAEF.OID_DIVISA
         INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'AB'
         WHERE
               PERI.OID_PERIODO = :NEW.OID_PERIODO
                AND TPERI.COD_TIPO_PERIODO = 'AC'
         GROUP BY
               LIMI.OID_DIVISA,
               LIMI.NUM_LIMITE
    )
    SELECT
         COUNT(1) LIMITE_SUPERADO
         INTO V_LIMITE_SUPERADO
    FROM
         DIVISAS_LIMITES DILI
         INNER JOIN DOCUMENTO_VALOR DOVA ON DOVA.OID_DIVISA = DILI.OID_DIVISA
    WHERE
         DILI.NUM_IMPORTE + DOVA.NUM_IMPORTE > DILI.NUM_LIMITE AND
         ROWNUM = 1
    ORDER BY
         DILI.ORDEN ASC;

    
    IF V_LIMITE_SUPERADO = 1 THEN
        UPDATE SAPR_TPERIODO SET OID_ESTADO_PERIODO =
        (SELECT OID_ESTADO_PERIODO FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'BL'),
        GMT_MODIFICACION = :NEW.GMT_MODIFICACION,
        DES_USUARIO_MODIFICACION = :NEW.DES_USUARIO_MODIFICACION
         WHERE OID_PERIODO = :NEW.OID_PERIODO;
    END IF;

  

END TRG_SAPR_TPERIODOXDOC_LIMITE;
/