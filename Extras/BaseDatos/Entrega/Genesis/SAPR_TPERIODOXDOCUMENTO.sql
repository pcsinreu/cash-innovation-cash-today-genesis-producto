  
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN
  /*==============================================================*/
  /* Index: IX_SAPR_TPERIODOXDOCUMENTO_01                                   */
  /*==============================================================*/
  SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPERIODOXDOCUMENTO' AND C.INDEX_NAME = 'IX_SAPR_TPERIODOXDOCUMENTO_01';
  IF var$existe = 0 THEN  
    EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPERIODOXDOCUMENTO_01 ON SAPR_TPERIODOXDOCUMENTO (OID_DOCUMENTO) ONLINE';
  END IF;

EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: SAPR_TPERIODOXDOCUMENTO: Agregar indice en la tabla "SAPR_TPERIODOXDOCUMENTO" - ' ||
                            sqlerrm);
END;
/