﻿SELECT 
	C.COD_CLIENTE, 
	C.DES_CLIENTE,
	C.OID_CLIENTE,
	CA.COD_AJENO, 
	CA.DES_AJENO,
	C.BOL_TOTALIZADOR_SALDO
FROM 
	GEPR_TCLIENTE C 
    LEFT JOIN GEPR_TTIPO_CLIENTE TC ON TC.OID_TIPO_CLIENTE = C.OID_TIPO_CLIENTE
	LEFT JOIN GEPR_TCODIGO_AJENO CA
	ON CA.OID_TABLA_GENESIS = C.OID_CLIENTE AND  CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
	AND CA.BOL_DEFECTO = 1 AND CA.BOL_ACTIVO = 1
 WHERE 1=1