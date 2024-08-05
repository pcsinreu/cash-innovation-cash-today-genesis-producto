﻿SELECT 
	PSER.OID_PROCESO_POR_PSERVICIO, 
	CLI.OID_CLIENTE, 
	CLI.COD_CLIENTE, 
	SCLI.COD_SUBCLIENTE, 
	PTSER.COD_PTO_SERVICIO, 
	PSER.COD_DELEGACION, 
	CLIFAT.COD_CLIENTE AS COD_CLIENTE_FACTURACION, 
	IAC.OID_IAC, 
	IAC.COD_IAC, 
	IAC.DES_IAC, 
	IAC.OBS_IAC, 
	IAC.BOL_VIGENTE 
FROM 
	GEPR_TPROCESO_POR_PSERVICIO PSER 
	LEFT OUTER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = PSER.OID_CLIENTE 
	LEFT OUTER JOIN GEPR_TCLIENTE CLIFAT ON CLIFAT.OID_CLIENTE = PSER.OID_CLIENTE_FACTURACION 
	LEFT OUTER JOIN GEPR_TSUBCLIENTE SCLI ON SCLI.OID_SUBCLIENTE = PSER.OID_SUBCLIENTE 
	LEFT OUTER JOIN GEPR_TPUNTO_SERVICIO PTSER ON PTSER.OID_PTO_SERVICIO = PSER.OID_PTO_SERVICIO 
	LEFT OUTER JOIN GEPR_TINFORM_ADICIONAL_CLIENTE IAC ON IAC.OID_IAC = PSER.OID_IAC 
WHERE 
	PSER.OID_PROCESO = []OID_PROCESO