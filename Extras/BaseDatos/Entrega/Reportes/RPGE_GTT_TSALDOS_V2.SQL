DECLARE
  OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv', 'current_schema');
  EXISTE        NUMBER;
BEGIN

   SELECT COUNT(1) INTO EXISTE FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2';

   IF EXISTE = 0 THEN

         EXECUTE IMMEDIATE 'CREATE GLOBAL TEMPORARY TABLE RPGE_GTT_TSALDOS_V2
                            (
                              DES_TITULO                   VARCHAR2(200),
                              DES_DIVISA                   VARCHAR2(200),
                              DES_CLIENTE                  VARCHAR2(200),
                              COD_NIVEL_DETALLE            VARCHAR2(200),
                              DES_DISP                     VARCHAR2(100),
                              DES_NO_DISP                  VARCHAR2(100),
                              DES_SECTOR                   VARCHAR2(200),
                              DES_SUBCLIENTE               VARCHAR2(200),
                              DES_PTO_SERVICIO             VARCHAR2(200),
                              DES_CANAL                    VARCHAR2(200),
                              DES_SUBCANAL                 VARCHAR2(200),
                              NUM_VALOR_DENOMINACION       FLOAT,
                              DES_UNIDAD_MEDIDA_MEDIO_PAGO VARCHAR2(200),
                              DES_TOTAL                    VARCHAR2(200),
                              NUM_IMPORTE_DISPONIBLE       FLOAT,
                              NUM_IMPORTE_NODISPONIBLE     FLOAT,
                              NEL_CANTIDAD_DISPONIBLE      INTEGER,
                              NEL_CANTIDAD_NODISPONIBLE    INTEGER,
                              DES_DELEGACION               VARCHAR2(200),
                              COD_PLANTA                   VARCHAR2(200),
                              DES_CALIDAD_DISPONIBLE       VARCHAR2(200),
                              DES_CALIDAD_NODISPONIBLE     VARCHAR2(200),
                              OID_DIVISA                   VARCHAR2(200),
                              BOL_BILLETE                  VARCHAR2(200),
                              DES_DENOMINACION             VARCHAR2(200)
                            )
                            ON COMMIT PRESERVE ROWS';

   ELSE

      /* Verificar columna: DES_DELEGACION */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'DES_DELEGACION';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD DES_DELEGACION VARCHAR2(200)';
      END IF;

      /* Verificar columna: COD_PLANTA */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'COD_PLANTA';

      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD COD_PLANTA VARCHAR2(200)';
      END IF;
      
      /* Verificar columna: DES_CALIDAD_DISPONIBLE */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'DES_CALIDAD_DISPONIBLE';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD DES_CALIDAD_DISPONIBLE VARCHAR2(200)';
      END IF;
      
      /* Verificar columna: DES_CALIDAD_NODISPONIBLE */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'DES_CALIDAD_NODISPONIBLE';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD DES_CALIDAD_NODISPONIBLE VARCHAR2(200)';
      END IF;

      /* Verificar columna: OID_DIVISA */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'OID_DIVISA';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD OID_DIVISA VARCHAR2(200)';
      END IF;
      
      /* Verificar columna: BOL_BILLETE */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'BOL_BILLETE';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD BOL_BILLETE VARCHAR2(200)';
      END IF;
      
      /* Verificar columna: DES_DENOMINACION */
      SELECT COUNT(1)
        INTO EXISTE
        FROM ALL_TAB_COLUMNS C
      WHERE UPPER(C.OWNER) = UPPER(OWNER_OBJECTS)
        AND UPPER(C.TABLE_NAME) = 'RPGE_GTT_TSALDOS_V2'
        AND UPPER(C.COLUMN_NAME) = 'DES_DENOMINACION';
      
      IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE RPGE_GTT_TSALDOS_V2 ADD DES_DENOMINACION VARCHAR2(200)';
      END IF;
   
   END IF;
  
EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: RPGE_GTT_TSALDOS_V2.sql Error: ' || sqlerrm);
    RAISE;
  
END;
/
