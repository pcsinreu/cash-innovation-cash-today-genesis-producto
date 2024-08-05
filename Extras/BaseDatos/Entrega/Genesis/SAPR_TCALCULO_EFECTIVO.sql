 DECLARE
    OWNER_OBJECTS VARCHAR2(30) := '';
    var$existe NUMBER;
  
 BEGIN
    SELECT sys_context( 'userenv', 'current_schema' ) INTO OWNER_OBJECTS FROM DUAL;
    
    /*==============================================================*/
    /* Index: IX_SAPR_TCALCULO_EFECTIVO_2                           */
    /*==============================================================*/
    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES WHERE UPPER(OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(TABLE_NAME) = 'SAPR_TCALCULO_EFECTIVO' AND INDEX_NAME = 'IX_SAPR_TCALCULO_EFECTIVO_2';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TCALCULO_EFECTIVO_2 ON SAPR_TCALCULO_EFECTIVO (OID_PERIODO) ONLINE';
    END IF;

    /*==============================================================*/
    /* Index: IX_SAPR_TCALCULO_EFECTIVO_3                           */
    /*==============================================================*/
    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES WHERE UPPER(OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(TABLE_NAME) = 'SAPR_TCALCULO_EFECTIVO' AND INDEX_NAME = 'IX_SAPR_TCALCULO_EFECTIVO_3';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE q'[
                CREATE INDEX IX_SAPR_TCALCULO_EFECTIVO_3
                  ON SAPR_TCALCULO_EFECTIVO (
                      OID_PERIODO,
                      OID_DIVISA,
                      NVL(OID_DENOMINACION, '-'),
                      NVL(COD_NIVEL_DETALLE, '-'),
                      BOL_BLOQUEADO,
                      BOL_DISPONIBLE,
                      NVL(OID_CALIDAD, '-'),
                      NVL(COD_TIPO_EFECTIVO_TOTAL, '-'),
                      NVL(OID_UNIDAD_MEDIDA, '-'),
                      NVL(OID_TIPO_CALCULO, '-'),
                      NVL(OID_SUBCANAL, '-'),
                      NVL(OID_PTO_SERVICIO, '-'),
                      NVL(FEC_CONTABLE, TO_DATE('0001-01-01', 'YYYY-MM-DD'))
                ) ONLINE
              ]';
    END IF;


EXCEPTION
   WHEN OTHERS THEN raise_application_error( -20001, 'Arquivo: Genesis.sql Script: SAPR_TCALCULO_EFECTIVO - ' || sqlerrm);
    RAISE;
END;
/