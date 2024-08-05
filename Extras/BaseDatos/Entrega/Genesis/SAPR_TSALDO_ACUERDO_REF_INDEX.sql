/*==============================================================*/
/* Index: IX_SAPR_TSAL_ACU_REF_02                               */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TSALDO_ACUERDO_REF' AND C.INDEX_NAME = 'IX_SAPR_TSAL_ACU_REF_02';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TSAL_ACU_REF_02 ON SAPR_TSALDO_ACUERDO_REF (BOL_CALCULADO, HOR_SALDO, FEC_SALDO) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-1732.sql Script: Agregar índice en la tabla "SAPR_TSALDO_ACUERDO_REF" - ' ||
                            sqlerrm);
END;
/
