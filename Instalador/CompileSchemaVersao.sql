DECLARE
   OWNER_OBJECTS VARCHAR2(30) := sys_context('userenv','current_schema');
   OBJ_NOME VARCHAR2(30) := '';
   OBJ_TIPO VARCHAR2(30) := '';
BEGIN
  FOR cur_rec IN (SELECT owner,
                         object_name,
                         object_type,
                         DECODE(object_type, 'PACKAGE', 1,
                                             'PACKAGE BODY', 2, 2) AS recompile_order
                  FROM   all_objects
                  WHERE  object_type IN ('PACKAGE', 'PACKAGE BODY', 'PROCEDURE', 'FUNCTION', 'TRIGGER')
                  AND    status != 'VALID'
                  AND owner = OWNER_OBJECTS
				   AND object_name LIKE '%0209%')
  LOOP
    BEGIN
      OBJ_NOME := cur_rec.object_name;
      OBJ_TIPO := cur_rec.object_type;
      
      IF cur_rec.object_type = 'PACKAGE' THEN
        EXECUTE IMMEDIATE 'ALTER ' || cur_rec.object_type || 
            ' "' || cur_rec.owner || '"."' || cur_rec.object_name || '" COMPILE';
      END IF;
      IF cur_rec.object_type = 'PACKAGE BODY' THEN
        EXECUTE IMMEDIATE 'ALTER PACKAGE "' || cur_rec.owner || 
            '"."' || cur_rec.object_name || '" COMPILE BODY';
      END IF;
      IF cur_rec.object_type = 'PROCEDURE' THEN
        EXECUTE IMMEDIATE 'ALTER PROCEDURE "' || cur_rec.owner || 
            '"."' || cur_rec.object_name || '" COMPILE';
      END IF;
      IF cur_rec.object_type = 'FUNCTION' THEN
        EXECUTE IMMEDIATE 'ALTER FUNCTION "' || cur_rec.owner || 
            '"."' || cur_rec.object_name || '" COMPILE';
      END IF;
      IF cur_rec.object_type = 'TRIGGER' THEN
        EXECUTE IMMEDIATE 'ALTER TRIGGER "' || cur_rec.owner || 
            '"."' || cur_rec.object_name || '" COMPILE';
      END IF;
      
    EXCEPTION
      WHEN OTHERS THEN
        IF sqlcode <> -24344 THEN
          RAISE;
       END IF;
    END;
  END LOOP;
EXCEPTION
  WHEN OTHERS THEN raise_application_error( -20001, 'Arquivo: CompileSchema.sql Script: COMPILE_SCHEMA Owner: ' || OWNER_OBJECTS || ' Objeto: ' || OBJ_NOME || ' Tipo: ' || OBJ_TIPO || ' ' || sqlerrm);
  RAISE;
END;
/