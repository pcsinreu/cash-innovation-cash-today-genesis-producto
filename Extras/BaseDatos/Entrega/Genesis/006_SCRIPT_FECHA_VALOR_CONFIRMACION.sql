-- Crear un nuevo tipo de planificación: Fecha Valor con Confirmación
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$cod_tipo_planificacion  VARCHAR2(20)  := 'FECHA_VALOR_CONFIR';
  var$des_tipo_planificacion  VARCHAR2(100) := 'Fecha Valor con Confirmación';
  var$gmt_zero                VARCHAR2(80)  := sys_extract_utc(current_timestamp) || ' +00:00';
  var$usuario                 VARCHAR2(50)  := 'GENESIS_PRODUCTO';
  var$existe                  NUMBER;
BEGIN
  -- Busco si existe la columna COD_TIPO_PLANIFICACION en la tabla "SAPR_TTIPO_PLANIFICACION" con un tamaño mínimo de 20 caracteres
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TTIPO_PLANIFICACION' AND COLUMN_NAME = 'COD_TIPO_PLANIFICACION' AND DATA_LENGTH >= 20;
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TTIPO_PLANIFICACION MODIFY COD_TIPO_PLANIFICACION VARCHAR2(20)';
  END IF;

  /* Buscamos si existe el tipo de planificación */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TTIPO_PLANIFICACION WHERE COD_TIPO_PLANIFICACION = var$cod_tipo_planificacion;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TTIPO_PLANIFICACION (OID_TIPO_PLANIFICACION, COD_TIPO_PLANIFICACION, DES_TIPO_PLANIFICACION, OBS_TIPO_PLANIFICACION, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_tipo_planificacion, var$des_tipo_planificacion, var$des_tipo_planificacion, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Crear un nuevo tipo de planificación: Fecha Valor con Confirmación - ' ||
                            sqlerrm);
END;
/
-- Agregar un nuevo campo "COD_TIPO_CONFIRMACION" en la tabla "SAPR_TPLANIFICACION"
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  -- Busco si existe la columna COD_TIPO_CONFIRMACION en la tabla "SAPR_TPLANIFICACION"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPLANIFICACION' AND COLUMN_NAME = 'COD_TIPO_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANIFICACION ADD COD_TIPO_CONFIRMACION VARCHAR2(80)';
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar un nuevo campo "COD_TIPO_CONFIRMACION" en la tabla "SAPR_TPLANIFICACION" - ' ||
                            sqlerrm);
END;
/
-- Agregar campos en la tabla "SAPR_TPERIODO"
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  -- Busco si existe la columna OID_TIPO_PERIODO en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'OID_TIPO_PERIODO';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD OID_TIPO_PERIODO VARCHAR2(36)';
  END IF;

  -- Busco si existe la columna FYH_ACREDITACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'FYH_ACREDITACION';
  IF var$existe = 1 THEN
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'FYH_CONFIRMACION';
    IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO RENAME COLUMN FYH_ACREDITACION TO FYH_CONFIRMACION';
    ELSE
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO DROP COLUMN FYH_ACREDITACION';
    END IF;
  ELSE
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'FYH_CONFIRMACION';
    IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD FYH_CONFIRMACION TIMESTAMP(6) WITH TIME ZONE';
    END IF;
  END IF;

  -- Busco si existe la columna COD_CONFIRMACION_ACREDITACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_CONFIRMACION_ACREDITACION';
  IF var$existe = 1 THEN
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_CONFIRMACION';
    IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO RENAME COLUMN COD_CONFIRMACION_ACREDITACION TO COD_CONFIRMACION';
    ELSE
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO DROP COLUMN COD_CONFIRMACION_ACREDITACION';
    END IF;
  ELSE
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_CONFIRMACION';
    IF var$existe = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD COD_CONFIRMACION VARCHAR2(80)';
    END IF;
  END IF;

  -- Busco si existe la columna COD_ERROR_CONFIRMACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_ERROR_CONFIRMACION';
  IF var$existe = 1 THEN
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_MENSAJE_CONFIRMACION';
    IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO RENAME COLUMN COD_ERROR_CONFIRMACION TO COD_MENSAJE_CONFIRMACION';
    ELSE
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO DROP COLUMN COD_ERROR_CONFIRMACION';
    END IF;
  ELSE
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_MENSAJE_CONFIRMACION';
    IF var$existe = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD COD_MENSAJE_CONFIRMACION VARCHAR2(80)';
    END IF;
  END IF;

  -- Busco si existe la columna BOL_CONFIRMACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'BOL_CONFIRMACION';
  IF var$existe = 1 THEN
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'BOL_CONFIRMADO';
    IF var$existe = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO RENAME COLUMN BOL_CONFIRMACION TO BOL_CONFIRMADO';
    ELSE
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO DROP COLUMN BOL_CONFIRMACION';
    END IF;
  ELSE
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'BOL_CONFIRMADO';
    IF var$existe = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD (BOL_CONFIRMADO NUMBER(1) DEFAULT 0 NOT NULL,
                            constraint CK_SAPR_TPERIODO_01 check (BOL_CONFIRMADO IN (0,1)))';
    END IF;
  END IF;

  -- Busco si existe la columna COD_PERIODO_CONFIRMACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_PERIODO_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD COD_PERIODO_CONFIRMACION VARCHAR2(15)';
  END IF;

  -- Busco si existe la columna NEL_INTENTO_CONFIRMACION en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'NEL_INTENTO_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD NEL_INTENTO_CONFIRMACION NUMBER(10) DEFAULT 0';
  END IF;

  -- Busco si existe la columna FYH_FIN en la tabla "SAPR_TPERIODO" que sea Nulleable
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'FYH_FIN' AND NULLABLE = 'Y';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO MODIFY FYH_FIN NULL';
  END IF;

  -- Busco si existe la columna FYH_ENVIO_BANCO en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'FYH_ENVIO_BANCO';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD FYH_ENVIO_BANCO TIMESTAMP(6) WITH TIME ZONE';
  END IF;

  /*==============================================================*/
  /* Index: IX_SAPR_TPERIODO_02                                   */
  /*==============================================================*/
  SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPERIODO' AND C.INDEX_NAME = 'IX_SAPR_TPERIODO_02';
  IF var$existe = 0 THEN  
    EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPERIODO_02 ON SAPR_TPERIODO (OID_MAQUINA, OID_PLANIFICACION, OID_TIPO_PERIODO) ONLINE';
  END IF;

  /*==============================================================*/
  /* Index: IX_SAPR_TPERIODO_03                                   */
  /*==============================================================*/
  SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPERIODO' AND C.INDEX_NAME = 'IX_SAPR_TPERIODO_03';
  IF var$existe = 0 THEN  
    EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPERIODO_03 ON SAPR_TPERIODO (OID_MAQUINA, COD_PERIODO_CONFIRMACION, OID_ESTADO_PERIODO) ONLINE';
  END IF;


EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar campos en la tabla "SAPR_TPERIODO" - ' ||
                            sqlerrm);
END;
/
/*Cambiar el trigger "TRG_SAPR_TPERIODO_ACREDITACION" de la tabla "SAPR_TPERIODO" de modo a incluir el siguiente regla: 
si el período esté relacionado a una planificación que sea del tipo "Fecha Valor con Confirmación", 
no deberá actualizar ninguno dato en la tabla de documentos.*/
create or replace TRIGGER TRG_SAPR_TPERIODO_ACREDITACION
AFTER UPDATE OF OID_ACREDITACION ON SAPR_TPERIODO FOR EACH ROW
DECLARE
V_FYH_ACREDITACION DATE;
var$cod_tipo_planificacion  VARCHAR2(20);
BEGIN
  IF :NEW.OID_ACREDITACION IS NOT NULL THEN
    SELECT ACRE.FYH_ACREDITACION INTO V_FYH_ACREDITACION FROM SAPR_TACREDITACION ACRE WHERE ACRE.OID_ACREDITACION = :NEW.OID_ACREDITACION;

    SELECT TPLAN.COD_TIPO_PLANIFICACION 
    INTO var$cod_tipo_planificacion
    FROM SAPR_TPLANIFICACION PLAN
    INNER JOIN SAPR_TTIPO_PLANIFICACION TPLAN ON PLAN.OID_TIPO_PLANIFICACION = TPLAN.OID_TIPO_PLANIFICACION
    WHERE PLAN.OID_PLANIFICACION = :NEW.OID_PLANIFICACION;
    
    IF var$cod_tipo_planificacion <> 'FECHA_VALOR_CONFIR' THEN
      UPDATE SAPR_TDOCUMENTO DOCU
      SET DOCU.BOL_ACREDITADO = '1',
          DOCU.FYH_ACREDITACION = V_FYH_ACREDITACION
      WHERE DOCU.OID_DOCUMENTO IN (SELECT PEDO.OID_DOCUMENTO FROM SAPR_TPERIODOXDOCUMENTO PEDO WHERE PEDO.OID_PERIODO = :OLD.OID_PERIODO);
    END IF;
  END IF;
END TRG_SAPR_TPERIODO_ACREDITACION;
/
-- Agregar campos en la tabla "SAPR_TDOCUMENTO"
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  -- Busco si existe la columna COD_CONFIRMACION_ACREDITACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'COD_CONFIRMACION_ACREDITACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO ADD COD_CONFIRMACION_ACREDITACION VARCHAR2(80)';
  END IF;

  -- Busco si existe la columna COD_ERROR_CONFIRMACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'COD_ERROR_CONFIRMACION';
  IF var$existe = 1 THEN
    SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'COD_MENSAJE_CONFIRMACION';
    IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO RENAME COLUMN COD_ERROR_CONFIRMACION TO COD_MENSAJE_CONFIRMACION';
    ELSE
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO DROP COLUMN COD_ERROR_CONFIRMACION';
    END IF;
  END IF;
  -- Busco si existe la columna COD_MENSAJE_CONFIRMACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'COD_MENSAJE_CONFIRMACION';
  IF var$existe = 0 THEN
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO ADD COD_MENSAJE_CONFIRMACION VARCHAR2(80)';
  END IF;

  -- Busco si existe la columna COD_MOVIMIENTO_CONFIRMACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'COD_MOVIMIENTO_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO ADD COD_MOVIMIENTO_CONFIRMACION VARCHAR2(15)';
  END IF;

  -- Busco si existe la columna NEL_INTENTO_CONFIRMACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'NEL_INTENTO_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO ADD NEL_INTENTO_CONFIRMACION NUMBER(10) DEFAULT 0';
  END IF;

  -- Busco si existe la columna BOL_CONFIRMACION en la tabla "SAPR_TDOCUMENTO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TDOCUMENTO' AND COLUMN_NAME = 'BOL_CONFIRMACION';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TDOCUMENTO ADD (BOL_CONFIRMACION NUMBER(1) DEFAULT 0 NOT NULL, 
                      constraint CK_SAPR_TDOCUMENTO_01 check (BOL_CONFIRMACION IN (0,1)))';  
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar campos en la tabla "SAPR_TDOCUMENTO" - ' ||
                            sqlerrm);
END;
/
-- Agregar nuevos estados de períodos en la tabla "SAPR_TESTADO_PERIODO"
DECLARE
  var$cod_estado_periodo  VARCHAR2(2);
  var$des_estado_periodo  VARCHAR2(100);
  var$obs_estado_periodo  VARCHAR2(500);
  var$gmt_zero            VARCHAR2(80)  := sys_extract_utc(current_timestamp) || ' +00:00';
  var$usuario             VARCHAR2(50)  := 'GENESIS_PRODUCTO';
  var$existe              NUMBER;
BEGIN
  var$cod_estado_periodo  := 'CO';
  var$des_estado_periodo  := 'A Confirmar';
  var$obs_estado_periodo  := 'Estado en el cual el período se encuentra hasta que el cliente confirme si está acreditado.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'RE';
  var$des_estado_periodo  := 'Reprocesar';
  var$obs_estado_periodo  := 'Estado en el cual el cliente indicó que existía un error en la acreditación y que será necesario enviarlo nuevamente para confirmación.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'NA';
  var$des_estado_periodo  := 'No Acreditado';
  var$obs_estado_periodo  := 'Estado final a cual el período se encuentra después de fallar en se acreditar.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'AP';
  var$des_estado_periodo  := 'Acreditado Parcial';
  var$obs_estado_periodo  := 'Estado final a cual el período se encuentra cuando ni todas las transacciones relacionadas a él fueron acreditadas.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'EC';
  var$des_estado_periodo  := 'En Creación';
  var$obs_estado_periodo  := 'Estado del período en creación.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'CF';
  var$des_estado_periodo  := 'Confirmado';
  var$obs_estado_periodo  := 'Estado del período confirmado.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'NC';
  var$des_estado_periodo  := 'No confirmado';
  var$obs_estado_periodo  := 'Estado del período no confirmado.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  var$cod_estado_periodo  := 'NP';
  var$des_estado_periodo  := 'No Procesar';
  var$obs_estado_periodo  := 'Estado final a cual el período no será considerado en los procesos de acreditación, etc.';
  
  /* Buscamos si existe el estado de período */
  BEGIN
    SELECT COUNT(1) INTO var$existe FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = var$cod_estado_periodo;
  EXCEPTION WHEN no_data_found THEN
    var$existe := 0;
  END;
  IF var$existe = 0 THEN
    INSERT INTO SAPR_TESTADO_PERIODO (OID_ESTADO_PERIODO, COD_ESTADO_PERIODO, DES_ESTADO_PERIODO, OBS_ESTADO_PERIODO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
    values (SYS_GUID(), var$cod_estado_periodo, var$des_estado_periodo, var$obs_estado_periodo, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
  END IF;

  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar nuevos estados de períodos en la tabla "SAPR_TESTADO_PERIODO" - ' ||
                            sqlerrm);
END;
/
-- Agregar nueva tabla SAPR_TPLANXMENSAJE


DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPLANXMENSAJE                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPLANXMENSAJE';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPLANXMENSAJE');
    EXECUTE IMMEDIATE q'[create table SAPR_TPLANXMENSAJE 
    (
      OID_PLANXMENSAJE            VARCHAR2(36)                NOT NULL,
      OID_PLANIFICACION           VARCHAR2(36)                NOT NULL,
      COD_MENSAJE                 VARCHAR2(80)                NOT NULL,
      DES_MENSAJE                 VARCHAR2(255)               NOT NULL,
      COD_TIPO_MENSAJE            VARCHAR2(5)                 NOT NULL,
      BOL_SIN_REINTENTOS          NUMBER(1) DEFAULT 0         NOT NULL,
      GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_CREACION        VARCHAR2(50)                NOT NULL,
      GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_MODIFICACION    VARCHAR2(50)                NOT NULL,
      constraint PK_SAPR_TPLANXMENSAJE primary key (OID_PLANXMENSAJE),
      constraint FK_SAPR_TPLANXMENSAJE_01 foreign key (OID_PLANIFICACION)
            references SAPR_TPLANIFICACION (OID_PLANIFICACION),
      constraint CK_SAPR_TPLANXMENSAJE_01 check (BOL_SIN_REINTENTOS IN (0,1)),
      constraint CK_SAPR_TPLANXMENSAJE_02 check (COD_TIPO_MENSAJE in ('OK', 'KO', 'EC'))
    )]';

    /*==============================================================*/
    /* Index: IX_SAPR_TPLANXMENSAJE_01                        */
    /*==============================================================*/
    EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPLANXMENSAJE_01 ON SAPR_TPLANXMENSAJE (OID_PLANIFICACION, COD_MENSAJE)';

  ELSE
    --Eliminamos la constraint
    EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPLANXMENSAJE DROP CONSTRAINT CK_SAPR_TPLANXMENSAJE_02]';

    --Agregamos nuevamente la constraint agregando el tipo EC
    EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPLANXMENSAJE ADD CONSTRAINT CK_SAPR_TPLANXMENSAJE_02 check (COD_TIPO_MENSAJE in ('OK', 'KO', 'EC'))]';

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPLANXMENSAJE' AND C.INDEX_NAME = 'IX_SAPR_TPLANXMENSAJE_01';
	  IF var$existe = 0 THEN  
		  EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPLANXMENSAJE_01 ON SAPR_TPLANXMENSAJE (OID_PLANIFICACION, COD_MENSAJE) ONLINE';
    END IF;

  END IF;
EXCEPTION

  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar nueva tabla SAPR_TPLANXMENSAJE - ' ||
                            sqlerrm);
END;
/
-- Agregar sequence SAPR_QCOD_PER_CON_SEQ
DECLARE
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Sequence: SAPR_QCOD_PER_CON_SEQ                                    */
  /*==============================================================*/
  /* Busco si ya existe la secuencia*/
  SELECT COUNT(1) INTO var$existe FROM ALL_SEQUENCES WHERE sequence_name = 'SAPR_QCOD_PER_CON_SEQ';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_QCOD_PER_CON_SEQ');
    EXECUTE IMMEDIATE q'[CREATE SEQUENCE SAPR_QCOD_PER_CON_SEQ
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 99999999999999
    ORDER]';
  END IF;
 
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar secuencia SAPR_QCOD_PER_CON_SEQ - ' ||
                            sqlerrm);
END;
/

-- Agregar sequence SAPR_QCOD_MOV_CON_SEQ
DECLARE
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Sequence: SAPR_QCOD_MOV_CON_SEQ                                    */
  /*==============================================================*/
  /* Busco si ya existe la secuencia*/
   SELECT COUNT(1) INTO var$existe FROM ALL_SEQUENCES WHERE sequence_name = 'SAPR_QCOD_MOV_CON_SEQ';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_QCOD_MOV_CON_SEQ');
    EXECUTE IMMEDIATE q'[CREATE SEQUENCE SAPR_QCOD_MOV_CON_SEQ
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 99999999999999
    ORDER]';
  END IF;
 
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar secuencia SAPR_QCOD_MOV_CON_SEQ - ' ||
                            sqlerrm);
END;
/
-- Agregar nueva tabla SAPR_TPLANXMOVIMIENTO
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPLANXMOVIMIENTO                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPLANXMOVIMIENTO';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPLANXMOVIMIENTO');
    EXECUTE IMMEDIATE q'[create table SAPR_TPLANXMOVIMIENTO 
    (
      OID_PLANXMOVIMIENTO         VARCHAR2(36)                NOT NULL,
      OID_PLANIFICACION           VARCHAR2(36)                NOT NULL,
      OID_FORMULARIO              VARCHAR2(36)                NOT NULL,
      BOL_CORTE_PERIODO           NUMBER(1) DEFAULT 0         NOT NULL,
      BOL_ACTIVO                  NUMBER(1) DEFAULT 0         NOT NULL,
      GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_CREACION        VARCHAR2(50)                NOT NULL,
      GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_MODIFICACION    VARCHAR2(50)                NOT NULL,
      constraint PK_SAPR_TPLANXMOVIMIENTO primary key (OID_PLANXMOVIMIENTO),
      constraint FK_SAPR_TPLANXMOVIMIENTO_01 foreign key (OID_PLANIFICACION)
            references SAPR_TPLANIFICACION (OID_PLANIFICACION),
      constraint FK_SAPR_TPLANXMOVIMIENTO_02 foreign key (OID_FORMULARIO)
            references SAPR_TFORMULARIO (OID_FORMULARIO),
      constraint CK_SAPR_TPLANXMOVIMIENTO_01 check (BOL_CORTE_PERIODO IN (0,1)),
      constraint CK_SAPR_TPLANXMOVIMIENTO_02 check (BOL_ACTIVO IN (0,1))
    )]';
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: PGP-2060.sql Script: Agregar nueva tabla SAPR_TPLANXMOVIMIENTO - ' ||
                            sqlerrm);
END;
/
-- Agregar nueva tabla SAPR_TPLANXDIVISA
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe    NUMBER;
BEGIN
  /*==============================================================*/
  /* Table: SAPR_TPLANXDIVISA                                    */
  /*==============================================================*/
  /* Busco si ya existe la tabla*/
  SELECT COUNT(1) INTO var$existe FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPLANXDIVISA';

  IF var$existe = 0 THEN
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPLANXDIVISA');
    EXECUTE IMMEDIATE q'[create table SAPR_TPLANXDIVISA 
    (
      OID_PLANXDIVISA             VARCHAR2(36)                NOT NULL,
      OID_PLANIFICACION           VARCHAR2(36)                NOT NULL,
      OID_DIVISA                  VARCHAR2(36)                NOT NULL,
      BOL_ACTIVO                  NUMBER(1) DEFAULT 0         NOT NULL,
      GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_CREACION        VARCHAR2(50)                NOT NULL,
      GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE NOT NULL,
      DES_USUARIO_MODIFICACION    VARCHAR2(50)                NOT NULL,
      constraint PK_SAPR_TPLANXDIVISA primary key (OID_PLANXDIVISA),
      constraint FK_SAPR_TPLANXDIVISA_01 foreign key (OID_PLANIFICACION)
            references SAPR_TPLANIFICACION (OID_PLANIFICACION),
      constraint FK_SAPR_TPLANXDIVISA_02 foreign key (OID_DIVISA)
            references GEPR_TDIVISA (OID_DIVISA),
      constraint CK_SAPR_TPLANXDIVISA_01 check (BOL_ACTIVO IN (0,1))
    )]';
  END IF;
EXCEPTION

  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: PGP-2060.sql Script: Agregar nueva tabla SAPR_TPLANXDIVISA - ' ||
                            sqlerrm);
END;
/
-- Agregar nuevos campos en la tabla "SAPR_TPLANIFICACION"
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  -- Busco si existe la columna BOL_PERIODO_SUBCANAL en la tabla "SAPR_TPLANIFICACION"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPLANIFICACION' AND COLUMN_NAME = 'BOL_PERIODO_SUBCANAL';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANIFICACION ADD BOL_PERIODO_SUBCANAL NUMBER(1) DEFAULT 0 NOT NULL  
                        constraint CK_SAPR_TPLANIFICACION_01 check (BOL_PERIODO_SUBCANAL IN (0,1))';
  END IF;

  -- Busco si existe la columna BOL_PERIODO_DIVISA en la tabla "SAPR_TPLANIFICACION"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPLANIFICACION' AND COLUMN_NAME = 'BOL_PERIODO_DIVISA';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANIFICACION ADD BOL_PERIODO_DIVISA NUMBER(1) DEFAULT 0 NOT NULL
                        constraint CK_SAPR_TPLANIFICACION_02 check (BOL_PERIODO_DIVISA IN (0,1))';
  END IF;

  -- Busco si existe la columna BOL_PERIODO_PTO_SERVICIO en la tabla "SAPR_TPLANIFICACION"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPLANIFICACION' AND COLUMN_NAME = 'BOL_PERIODO_PTO_SERVICIO';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANIFICACION ADD BOL_PERIODO_PTO_SERVICIO NUMBER(1) DEFAULT 0 NOT NULL
                        constraint CK_SAPR_TPLANIFICACION_03 check (BOL_PERIODO_PTO_SERVICIO IN (0,1))';
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: PGP-2060.sql Script: Agregar nuevos campos en la tabla "SAPR_TPLANIFICACION" - ' ||
                            sqlerrm);
END;
/
-- Agregar nuevos campos en la tabla "SAPR_TPERIODO"
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  -- Busco si existe la columna OID_DIVISA en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'OID_DIVISA';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD (OID_DIVISA VARCHAR2(36),
                        constraint FK_TPERIODO_TDIVISA foreign key (OID_DIVISA)
                          references GEPR_TDIVISA (OID_DIVISA))';
  END IF;

  -- Busco si existe la columna OID_SUBCANAL en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'OID_SUBCANAL';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD (OID_SUBCANAL VARCHAR2(36),
                        constraint FK_TPERIODO_TSUBCANAL foreign key (OID_SUBCANAL)
                          references GEPR_TSUBCANAL (OID_SUBCANAL))';
  END IF;

  -- Busco si existe la columna OID_PTO_SERVICIO en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'OID_PTO_SERVICIO';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD (OID_PTO_SERVICIO VARCHAR2(36),
                        constraint FK_TPERIODO_TPUNTO_SERVICIO foreign key (OID_PTO_SERVICIO)
                          references GEPR_TPUNTO_SERVICIO (OID_PTO_SERVICIO))';
  END IF;

  -- Busco si existe la columna COD_COLLECTION_ID en la tabla "SAPR_TPERIODO"
  SELECT COUNT(1) INTO var$existe FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND COLUMN_NAME = 'COD_COLLECTION_ID';
  IF var$existe = 0 THEN
    EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPERIODO ADD COD_COLLECTION_ID VARCHAR2(200)';
  END IF;

  /*Reemplazo constraint AK_SAPR_TPERIODO_1 de SAPR_TPERIODO */
  SELECT COUNT(1) INTO var$existe FROM ALL_CONSTRAINTS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'SAPR_TPERIODO' AND constraint_name = 'AK_SAPR_TPERIODO_1';
  IF var$existe = 1 THEN 
      EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPERIODO drop constraint AK_SAPR_TPERIODO_1]';
  END IF;
  EXECUTE IMMEDIATE q'[ALTER TABLE SAPR_TPERIODO add constraint AK_SAPR_TPERIODO_1 unique (OID_PLANIFICACION, OID_MAQUINA, OID_ACREDITACION, OID_TIPO_PERIODO, FYH_INICIO, FYH_FIN, OID_DIVISA, OID_SUBCANAL, OID_PTO_SERVICIO, COD_COLLECTION_ID)]';

EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: PGP-2060.sql Script: Agregar nuevos campos en la tabla "SAPR_TPERIODO" - ' ||
                            sqlerrm);
END;
/