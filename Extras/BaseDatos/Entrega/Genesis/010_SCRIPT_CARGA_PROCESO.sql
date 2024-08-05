DECLARE
var$existeValor NUMBER;
BEGIN
    begin
        SELECT COUNT(*) INTO var$existeValor FROM SAPR_TPROCESO WHERE COD_PROCESO = 'BBVA_PERU';
      exception
        when no_data_found then
           var$existeValor := 0;
    end;
    IF var$existeValor = 0 THEN
        INSERT INTO SAPR_TPROCESO VALUES (SYS_GUID(), 'BBVA_PERU',  'BBVA Perú', SYSTIMESTAMP, 'SCRIPT_CARGA', SYSTIMESTAMP, 'SCRIPT_CARGA');
        COMMIT;
    END IF;
END;
/