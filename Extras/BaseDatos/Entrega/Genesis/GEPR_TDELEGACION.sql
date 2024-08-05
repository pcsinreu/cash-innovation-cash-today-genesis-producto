DECLARE
   OWNER_OBJECTS VARCHAR2(30) := '';
   EXISTE NUMBER;
BEGIN

   SELECT sys_context( 'userenv', 'current_schema' ) INTO OWNER_OBJECTS FROM DUAL;
    
   SELECT COUNT(1) INTO EXISTE FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'GEPR_TDELEGACION';

   IF EXISTE = 1 THEN
   
	  /* BOL_DEFAULT */
      SELECT COUNT(1) INTO EXISTE FROM ALL_TAB_COLUMNS WHERE OWNER = OWNER_OBJECTS AND TABLE_NAME = 'GEPR_TDELEGACION' AND COLUMN_NAME = 'BOL_DEFAULT';  
      
      IF EXISTE = 0 THEN
      
         EXECUTE IMMEDIATE 'ALTER TABLE GEPR_TDELEGACION ADD BOL_DEFAULT NUMBER(1) DEFAULT NULL';
		 EXECUTE IMMEDIATE q'[COMMENT ON COLUMN GEPR_TDELEGACION.BOL_DEFAULT IS 'Indica si la delegación será seleccionada para ingresar a la aplicación.']';
    
      END IF;
   END IF;

   
   EXCEPTION
      WHEN OTHERS THEN raise_application_error( -20001, 'Arquivo: Genesis.sql Script: GEPR_TDELEGACION - ' || sqlerrm);
      RAISE;
END;  
/