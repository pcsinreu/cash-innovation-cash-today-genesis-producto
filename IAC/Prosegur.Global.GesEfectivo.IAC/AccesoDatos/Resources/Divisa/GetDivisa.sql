﻿SELECT 
	OID_DIVISA,
	COD_ISO_DIVISA,
	DES_DIVISA,
	COD_SIMBOLO,
	BOL_VIGENTE,
	COD_COLOR,
	COD_ACCESO
FROM 
	GEPR_TDIVISA
WHERE
	BOL_VIGENTE = []BOL_VIGENTE