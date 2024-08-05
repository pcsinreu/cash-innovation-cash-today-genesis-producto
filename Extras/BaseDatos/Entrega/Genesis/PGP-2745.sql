/*==============================================================*/
/* Index: IX_SAPR_TPLANXMAQUINA_02                              */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPLANXMAQUINA' AND C.INDEX_NAME = 'IX_SAPR_TPLANXMAQUINA_02';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPLANXMAQUINA_02 ON SAPR_TPLANXMAQUINA (OID_MAQUINA, BOL_ACTIVO)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TPLANXMAQUINA" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TPLANXMAQUINA_03                              */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TPLANXMAQUINA' AND C.INDEX_NAME = 'IX_SAPR_TPLANXMAQUINA_03';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TPLANXMAQUINA_03 ON SAPR_TPLANXMAQUINA (OID_PLANIFICACION, BOL_ACTIVO)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TPLANXMAQUINA" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TMODELO_2                                    */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TMODELO' AND C.INDEX_NAME = 'IX_SAPR_TMODELO_2';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TMODELO_2 ON SAPR_TMODELO (OID_FABRICANTE)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TMODELO" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_GEPR_TSECTOR_3                                    */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'GEPR_TSECTOR' AND C.INDEX_NAME = 'IX_GEPR_TSECTOR_3';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_GEPR_TSECTOR_3 ON GEPR_TSECTOR (OID_PLANTA)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "GEPR_TSECTOR" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_GEPR_TPUNTO_SERVICIO_6                             */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'GEPR_TPUNTO_SERVICIO' AND C.INDEX_NAME = 'IX_GEPR_TPUNTO_SERVICIO_6';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_GEPR_TPUNTO_SERVICIO_6 ON GEPR_TPUNTO_SERVICIO (OID_SUBCLIENTE)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "GEPR_TPUNTO_SERVICIO" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TLIMITE_01                                    */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TLIMITE' AND C.INDEX_NAME = 'IX_SAPR_TLIMITE_01';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TLIMITE_01 ON SAPR_TLIMITE (OID_MAQUINA)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TLIMITE" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TLIMITE_02                                    */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TLIMITE' AND C.INDEX_NAME = 'IX_SAPR_TLIMITE_02';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TLIMITE_02 ON SAPR_TLIMITE (OID_DIVISA)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TLIMITE" - ' ||
                            sqlerrm);
END;
/

/*==============================================================*/
/* Index: IX_SAPR_TLIMITE_03                                    */
/*==============================================================*/
DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  var$existe NUMBER;
BEGIN

    SELECT COUNT(1) INTO var$existe FROM ALL_INDEXES C WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)  AND UPPER(C.TABLE_NAME) = 'SAPR_TLIMITE' AND C.INDEX_NAME = 'IX_SAPR_TLIMITE_03';
    IF var$existe = 0 THEN  
      EXECUTE IMMEDIATE 'CREATE INDEX IX_SAPR_TLIMITE_03 ON SAPR_TLIMITE (OID_PTO_SERVICIO)';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: PGP-2745.sql Script: Agregar índice en la tabla "SAPR_TLIMITE" - ' ||
                            sqlerrm);
END;
/