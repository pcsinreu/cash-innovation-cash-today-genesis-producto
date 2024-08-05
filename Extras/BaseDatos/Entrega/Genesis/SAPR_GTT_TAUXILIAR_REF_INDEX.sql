/*==============================================================*/
/* Index: IX_SAPR_GTT_TAUXILIAR_1                               */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_GTT_TAUXILIAR' AND C.INDEX_NAME = 'IX_SAPR_GTT_TAUXILIAR_1';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_GTT_TAUXILIAR_1 ON SAPR_GTT_TAUXILIAR (COD_CALIFICADOR, OID_CAMPO1)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2393.sql Script: Agregar índice en la tabla "SAPR_GTT_TAUXILIAR" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_GTT_TAUXILIAR_2                               */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_GTT_TAUXILIAR' AND C.INDEX_NAME = 'IX_SAPR_GTT_TAUXILIAR_2';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_GTT_TAUXILIAR_2 ON SAPR_GTT_TAUXILIAR (COD_CALIFICADOR, OID_CAMPO1, OID_CAMPO7)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2393.sql Script: Agregar índice en la tabla "SAPR_GTT_TAUXILIAR" - ' ||
                            sqlerrm);
END;
/
