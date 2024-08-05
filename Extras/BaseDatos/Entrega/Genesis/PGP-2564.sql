/*==============================================================*/
/* Index: IX_SAPR_TMAQUINA_2                               */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TMAQUINA' AND C.INDEX_NAME = 'IX_SAPR_TMAQUINA_2';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TMAQUINA_2 ON SAPR_TMAQUINA (COD_IDENTIFICACION) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: INDICES.sql Script: Agregar índice en la tabla "SAPR_TMAQUINA" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TMOVIMIENTO_02                               */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TMOVIMIENTO' AND C.INDEX_NAME = 'IX_SAPR_TMOVIMIENTO_02';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TMOVIMIENTO_02 ON SAPR_TMOVIMIENTO (COD_PAIS, COD_DEVICEID, FEC_MOVIMIENTO, HOR_MOVIMIENTO, FYH_ACREDITACION) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: INDICES.sql Script: Agregar índice en la tabla "SAPR_TMOVIMIENTO" - ' ||
                            sqlerrm);
END;
/