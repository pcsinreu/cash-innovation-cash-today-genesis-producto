DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TESTADO_PERIODO' AND C.INDEX_NAME = 'IX_SAPR_TEST_PER_01';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TEST_PER_01 ON SAPR_TESTADO_PERIODO (COD_ESTADO_PERIODO, OID_ESTADO_PERIODO) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2344.sql Script: Agregar indices en la tablas "SAPR_TESTADO_PERIODO" - ' ||
                            sqlerrm);
END;
/

DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPLANIFICACION' AND C.INDEX_NAME = 'IX_SAPR_TPLANIFICACION_02';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPLANIFICACION_02 ON SAPR_TPLANIFICACION (OID_DELEGACION) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2344.sql Script: Agregar indices en la tablas "SAPR_TPLANIFICACION" - ' ||
                            sqlerrm);
END;
/

DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TDOCUMENTO' AND C.INDEX_NAME = 'IX_SAPR_TDOCUMENTO_30';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TDOCUMENTO_30 ON SAPR_TDOCUMENTO (COD_ESTADO, OID_FORMULARIO, OID_CUENTA_ORIGEN, OID_SECTOR_ORIGEN) ONLINE';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2344.sql Script: Agregar indices en la tablas "SAPR_TDOCUMENTO" - ' ||
                            sqlerrm);
END;
/