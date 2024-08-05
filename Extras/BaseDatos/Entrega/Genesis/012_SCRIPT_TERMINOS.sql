DECLARE
  V_EXISTE NUMBER := 0;
BEGIN  

  -- CODIGO_CONFIRMACION_PERIODO
  SELECT COUNT(1) INTO V_EXISTE FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'CODIGO_CONFIRMACION_PERIODO';
  IF (V_EXISTE = 0) THEN
    INSERT INTO GEPR_TTERMINO (OID_TERMINO, COD_TERMINO, DES_TERMINO, OID_FORMATO, NEC_LONGITUD, BOL_MOSTRAR_CODIGO, BOL_VALORES_POSIBLES, BOL_VIGENTE, COD_USUARIO, FYH_ACTUALIZACION, BOL_ACEPTAR_DIGITACION, BOL_ESPECIFICO_DE_SALDOS) VALUES
                              (SYS_GUID(), 'CODIGO_CONFIRMACION_PERIODO', 'Código de Confirmación de Período', (SELECT MAX(FORM.OID_FORMATO) FROM GEPR_TFORMATO FORM WHERE FORM.COD_FORMATO = '1'), 15, 0, 0, 1, 'SCRIPT_CARGA', SYSDATE, 1, 0);

    INSERT INTO GEPR_TTERMINO_POR_IAC (OID_TERMINO_IAC, OID_IAC, OID_TERMINO, BOL_BUSQUEDA_PARCIAL, BOL_CAMPO_CLAVE, NEC_ORDEN, BOL_ES_OBLIGATORIO, COD_USUARIO, FYH_ACTUALIZACION, BOL_TERMINO_COPIA, BOL_ES_PROTEGIDO, BOL_ES_INVISIBLEREPORTE, BOL_ES_IDMECANIZADO) VALUES
                                      (SYS_GUID(), (SELECT MAX(IAC.OID_IAC) FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC WHERE IAC.COD_IAC = 'IACMAE1'), (SELECT MAX(TERM.OID_TERMINO) FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'CODIGO_CONFIRMACION_PERIODO'), 0, 0, (SELECT MAX(TEIA.NEC_ORDEN) + 1 FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC INNER JOIN GEPR_TTERMINO_POR_IAC TEIA ON IAC.OID_IAC = TEIA.OID_IAC WHERE IAC.COD_IAC = 'IACMAE1'), 0, 'SCRIPT_CARGA', SYSDATE, 0, 0, 0, 0);
  END IF;

  -- OID_FORMULARIO_ORIGINAL
  SELECT COUNT(1) INTO V_EXISTE FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'OID_FORMULARIO_ORIGINAL';
  IF (V_EXISTE = 0) THEN

    INSERT INTO GEPR_TTERMINO (OID_TERMINO, COD_TERMINO, DES_TERMINO, OID_FORMATO, NEC_LONGITUD, BOL_MOSTRAR_CODIGO, BOL_VALORES_POSIBLES, BOL_VIGENTE, COD_USUARIO, FYH_ACTUALIZACION, BOL_ACEPTAR_DIGITACION, BOL_ESPECIFICO_DE_SALDOS) VALUES
                              (SYS_GUID(), 'OID_FORMULARIO_ORIGINAL', 'Identificador de formulario original', (SELECT MAX(FORM.OID_FORMATO) FROM GEPR_TFORMATO FORM WHERE FORM.COD_FORMATO = '1'), 36, 0, 0, 1, 'SCRIPT_CARGA', SYSDATE, 1, 0);
  END IF;

  -- COD_EXTERNO_ORIGINAL
  SELECT COUNT(1) INTO V_EXISTE FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'COD_EXTERNO_ORIGINAL';
  IF (V_EXISTE = 0) THEN

    INSERT INTO GEPR_TTERMINO (OID_TERMINO, COD_TERMINO, DES_TERMINO, OID_FORMATO, NEC_LONGITUD, BOL_MOSTRAR_CODIGO, BOL_VALORES_POSIBLES, BOL_VIGENTE, COD_USUARIO, FYH_ACTUALIZACION, BOL_ACEPTAR_DIGITACION, BOL_ESPECIFICO_DE_SALDOS) VALUES
                              (SYS_GUID(), 'COD_EXTERNO_ORIGINAL', 'Código externo original', (SELECT MAX(FORM.OID_FORMATO) FROM GEPR_TFORMATO FORM WHERE FORM.COD_FORMATO = '1'), 80, 0, 0, 1, 'SCRIPT_CARGA', SYSDATE, 1, 0);
  END IF;

  -- COD_ACTUAL_ID_ORIGINAL
  SELECT COUNT(1) INTO V_EXISTE FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'COD_ACTUAL_ID_ORIGINAL';
  IF (V_EXISTE = 0) THEN

    INSERT INTO GEPR_TTERMINO (OID_TERMINO, COD_TERMINO, DES_TERMINO, OID_FORMATO, NEC_LONGITUD, BOL_MOSTRAR_CODIGO, BOL_VALORES_POSIBLES, BOL_VIGENTE, COD_USUARIO, FYH_ACTUALIZACION, BOL_ACEPTAR_DIGITACION, BOL_ESPECIFICO_DE_SALDOS) VALUES
                              (SYS_GUID(), 'COD_ACTUAL_ID_ORIGINAL', 'Código ActualId original', (SELECT MAX(FORM.OID_FORMATO) FROM GEPR_TFORMATO FORM WHERE FORM.COD_FORMATO = '1'), 200, 0, 0, 1, 'SCRIPT_CARGA', SYSDATE, 1, 0);
  END IF;

  -- COD_COLLECTION_ID_ORIGINAL
  SELECT COUNT(1) INTO V_EXISTE FROM GEPR_TTERMINO TERM WHERE TERM.COD_TERMINO = 'COD_COLLECTION_ID_ORIGINAL';
  IF (V_EXISTE = 0) THEN

    INSERT INTO GEPR_TTERMINO (OID_TERMINO, COD_TERMINO, DES_TERMINO, OID_FORMATO, NEC_LONGITUD, BOL_MOSTRAR_CODIGO, BOL_VALORES_POSIBLES, BOL_VIGENTE, COD_USUARIO, FYH_ACTUALIZACION, BOL_ACEPTAR_DIGITACION, BOL_ESPECIFICO_DE_SALDOS) VALUES
                              (SYS_GUID(), 'COD_COLLECTION_ID_ORIGINAL', 'Código CollectionId original', (SELECT MAX(FORM.OID_FORMATO) FROM GEPR_TFORMATO FORM WHERE FORM.COD_FORMATO = '1'), 200, 0, 0, 1, 'SCRIPT_CARGA', SYSDATE, 1, 0);
  END IF;

  COMMIT; 

EXCEPTION
WHEN OTHERS THEN
  ROLLBACK;
  RAISE;
END;
/