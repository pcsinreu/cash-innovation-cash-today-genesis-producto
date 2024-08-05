﻿SELECT 
	TER.COD_TERMINO, 
	TER.DES_TERMINO, 
	TER.OBS_TERMINO, 
	FORM.COD_FORMATO, 
	FORM.DES_FORMATO, 
	TER.NEC_LONGITUD, 
	MAS.COD_MASCARA, 
	MAS.DES_MASCARA, 
	MAS.DES_EXP_REGULAR,
	AVAL.COD_ALGORITMO_VALIDACION, 
	AVAL.DES_ALGORITMO_VALIDACION, 
	TER.BOL_MOSTRAR_CODIGO, 
	TIAC.BOL_BUSQUEDA_PARCIAL, 
	TIAC.BOL_CAMPO_CLAVE, 
	TIAC.BOL_ES_OBLIGATORIO, 
	TIAC.NEC_ORDEN, 
	TIAC.BOL_ES_PROTEGIDO,
	TER.BOL_VIGENTE, 
	VTER.COD_VALOR, 
	VTER.DES_VALOR, 
	VTER.BOL_VIGENTE 
FROM 
	GEPR_TTERMINO_POR_IAC TIAC 
	LEFT JOIN GEPR_TTERMINO TER ON TER.OID_TERMINO = TIAC.OID_TERMINO 
	LEFT JOIN GEPR_TFORMATO FORM ON FORM.OID_FORMATO = TER.OID_FORMATO 
	LEFT JOIN GEPR_TMASCARA MAS ON MAS.OID_MASCARA = TER.OID_MASCARA 
	LEFT JOIN GEPR_TALGORITMO_VALIDACION AVAL ON AVAL.OID_ALGORITMO_VALIDACION = TER.OID_ALGORITMO_VALIDACION 
	LEFT JOIN GEPR_TVALOR_TERMINO_IAC VTER ON VTER.OID_TERMINO = TER.OID_TERMINO 
	INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = VTER.OID_CLIENTE 
	AND CLI.OID_CLIENTE = []OID_CLIENTE 
WHERE 
	TIAC.OID_IAC = []OID_IAC 
ORDER BY 
	TER.COD_TERMINO