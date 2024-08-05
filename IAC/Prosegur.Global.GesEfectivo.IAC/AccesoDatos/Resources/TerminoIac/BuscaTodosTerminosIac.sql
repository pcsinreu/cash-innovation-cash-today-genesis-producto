SELECT TER.COD_TERMINO, TER.DES_TERMINO, TERIAC.BOL_BUSQUEDA_PARCIAL, TERIAC.BOL_CAMPO_CLAVE, TERIAC.NEC_ORDEN
FROM GEPR_TTERMINO_POR_IAC TERIAC INNER JOIN GEPR_TTERMINO TER ON TERIAC.OID_TERMINO = TER.OID_TERMINO
WHERE OID_IAC IN (SELECT IAC.OID_IAC 
FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC INNER JOIN GEPR_TTERMINO_POR_IAC TERIAC ON IAC.OID_IAC = TERIAC.OID_IAC
WHERE IAC.COD_IAC = []COD_IAC)