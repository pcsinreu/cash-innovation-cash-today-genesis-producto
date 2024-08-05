CREATE OR REPLACE PACKAGE SAPR_PBORRAR AS
  PROCEDURE sborrar_documento(par$oid_documento  IN VARCHAR2);
END SAPR_PBORRAR;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PBORRAR AS
  
  PROCEDURE sborrar_documento(par$oid_documento  IN VARCHAR2) IS

    CURSOR cur$terminos IS
        SELECT OID_TERMINO, COD_TERMINO FROM GEPR_TTERMINO
        WHERE COD_TERMINO IN ('OID_FORMULARIO_ORIGINAL', 'COD_EXTERNO_ORIGINAL', 'COD_ACTUAL_ID_ORIGINAL', 'COD_COLLECTION_ID_ORIGINAL');

    var$oid_formulario_deleted      VARCHAR2(36);
    var$cod_externo_deleted         VARCHAR2(50);

    var$oid_formulario_original     VARCHAR2(36);
    var$cod_externo_original        VARCHAR2(80);
    var$cod_actual_id_original      VARCHAR2(200);
    var$cod_collection_id_original  VARCHAR2(200);

    var$oid_movimiento              VARCHAR2(36);

    var$existe  NUMBER;

  BEGIN
    BEGIN
      SELECT OID_FORMULARIO, COD_EXTERNO, NVL(COD_ACTUAL_ID, ''), NVL(COD_COLLECTION_ID, '')
        INTO var$oid_formulario_original, var$cod_externo_original, var$cod_actual_id_original, var$cod_collection_id_original
      FROM SAPR_TDOCUMENTO
      WHERE OID_DOCUMENTO = par$oid_documento;

      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_formulario_original := NULL;
    END;

    BEGIN
      SELECT OID_FORMULARIO, DES_COD_EXTERNO
        INTO var$oid_formulario_deleted, var$cod_externo_deleted
      FROM SAPR_TFORMULARIO 
      WHERE COD_FORMULARIO = 'MAEBOR';

      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_formulario_deleted := NULL;
    END;

   
    IF var$oid_formulario_original IS NOT NULL AND var$oid_formulario_deleted IS NOT NULL THEN
      -- Alta de términos para el documento
      FOR reg$terminos IN cur$terminos LOOP
        SELECT COUNT(1) INTO var$existe
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO
        WHERE OID_TERMINO = reg$terminos.OID_TERMINO AND OID_DOCUMENTO = par$oid_documento;

        IF var$existe = 0 THEN
          IF reg$terminos.COD_TERMINO = 'OID_FORMULARIO_ORIGINAL' THEN
            INSERT INTO SAPR_TVALOR_TERMINOXDOCUMENTO
              (oid_valor_terminoxdocumento, oid_documento, oid_termino, des_valor, gmt_creacion, des_usuario_creacion, gmt_modificacion, des_usuario_modificacion)
            VALUES
              (SYS_GUID(), par$oid_documento, reg$terminos.OID_TERMINO, var$oid_formulario_original, SYSTIMESTAMP, 'SBORRAR_DOCUMENTO', SYSTIMESTAMP, 'SBORRAR_DOCUMENTO');
          END IF;

          IF reg$terminos.COD_TERMINO = 'COD_EXTERNO_ORIGINAL' THEN
            INSERT INTO SAPR_TVALOR_TERMINOXDOCUMENTO
              (oid_valor_terminoxdocumento, oid_documento, oid_termino, des_valor, gmt_creacion, des_usuario_creacion, gmt_modificacion, des_usuario_modificacion)
            VALUES
              (SYS_GUID(), par$oid_documento, reg$terminos.OID_TERMINO, var$cod_externo_original, SYSTIMESTAMP, 'SBORRAR_DOCUMENTO', SYSTIMESTAMP, 'SBORRAR_DOCUMENTO');
          END IF;

          IF reg$terminos.COD_TERMINO = 'COD_ACTUAL_ID_ORIGINAL' AND var$cod_actual_id_original IS NOT NULL THEN
            INSERT INTO SAPR_TVALOR_TERMINOXDOCUMENTO
              (oid_valor_terminoxdocumento, oid_documento, oid_termino, des_valor, gmt_creacion, des_usuario_creacion, gmt_modificacion, des_usuario_modificacion)
            VALUES
              (SYS_GUID(), par$oid_documento, reg$terminos.OID_TERMINO, var$cod_actual_id_original, SYSTIMESTAMP, 'SBORRAR_DOCUMENTO', SYSTIMESTAMP, 'SBORRAR_DOCUMENTO');
          END IF;

          IF reg$terminos.COD_TERMINO = 'COD_COLLECTION_ID_ORIGINAL' AND var$cod_collection_id_original IS NOT NULL THEN
            INSERT INTO SAPR_TVALOR_TERMINOXDOCUMENTO
              (oid_valor_terminoxdocumento, oid_documento, oid_termino, des_valor, gmt_creacion, des_usuario_creacion, gmt_modificacion, des_usuario_modificacion)
            VALUES
              (SYS_GUID(), par$oid_documento, reg$terminos.OID_TERMINO, var$cod_collection_id_original, SYSTIMESTAMP, 'SBORRAR_DOCUMENTO', SYSTIMESTAMP, 'SBORRAR_DOCUMENTO');
          END IF;
        END IF;
      END LOOP;

      -- Actualización de documento
      UPDATE SAPR_TDOCUMENTO 
          SET OID_FORMULARIO = var$oid_formulario_deleted,
          COD_EXTERNO = COD_EXTERNO || '_' || var$cod_externo_deleted,
          COD_ACTUAL_ID = CASE WHEN COD_ACTUAL_ID IS NOT NULL THEN COD_ACTUAL_ID || '_' || var$cod_externo_deleted ELSE COD_ACTUAL_ID END,
          COD_COLLECTION_ID = CASE WHEN COD_COLLECTION_ID IS NOT NULL THEN COD_COLLECTION_ID || '_' || var$cod_externo_deleted ELSE COD_COLLECTION_ID END,
          GMT_MODIFICACION = SYSTIMESTAMP,
          DES_USUARIO_MODIFICACION = 'SBORRAR_DOCUMENTO'
        WHERE OID_DOCUMENTO = par$oid_documento AND COD_EXTERNO NOT LIKE '%' || var$cod_externo_deleted;

      -- Borrado de tablas SAPR_TMOVIMIENTO
      BEGIN
        SELECT OID_MOVIMIENTO
          INTO var$oid_movimiento
        FROM SAPR_TMOVIMIENTO
        WHERE OID_DOCUMENTO = par$oid_documento;
      EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_movimiento := NULL;
      END;

      IF var$oid_movimiento IS NOT NULL THEN
        DELETE FROM SAPR_TMOVIMIENTO_CAMPO_EXTRA
        WHERE OID_MOVIMIENTO = var$oid_movimiento;

        DELETE FROM SAPR_TMOVIMIENTO_DETALLE
        WHERE OID_MOVIMIENTO = var$oid_movimiento;

        DELETE FROM SAPR_TMOVIMIENTO
        WHERE OID_MOVIMIENTO = var$oid_movimiento;
      END IF;
      
      COMMIT;
    END IF;

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RAISE;

  END sborrar_documento;

END SAPR_PBORRAR;
/