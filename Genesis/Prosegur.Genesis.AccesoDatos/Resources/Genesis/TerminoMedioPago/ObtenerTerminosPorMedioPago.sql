﻿SELECT m.OID_TERMINO, 
	   m.COD_TERMINO, 
	   m.DES_TERMINO, 
	   m.OBS_TERMINO, 
	   m.DES_VALOR_INICIAL, 
	   m.NEC_LONGITUD, 
	   m.OID_FORMATO, 
	   m.OID_MASCARA, 
	   m.OID_ALGORITMO_VALIDACION, 
	   m.BOL_MOSTRAR_CODIGO, 
	   m.NEC_ORDEN, 
	   m.BOL_VIGENTE, 
	   m.COD_USUARIO, 
	   m.FYH_ACTUALIZACION 
FROM GEPR_TTERMINO_MEDIO_PAGO m
WHERE m.OID_MEDIO_PAGO = []OID_MEDIO_PAGO
ORDER BY m.DES_TERMINO