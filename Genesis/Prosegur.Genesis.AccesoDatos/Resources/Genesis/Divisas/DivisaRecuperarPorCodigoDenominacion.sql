﻿SELECT 
	D.OID_DIVISA,
	D.COD_ISO_DIVISA,
	D.DES_DIVISA,
	D.COD_SIMBOLO,
	D.BOL_VIGENTE,
	D.COD_USUARIO,
	D.FYH_ACTUALIZACION,
	D.COD_ACCESO,
	D.COD_COLOR
FROM 
	GEPR_TDIVISA D
	INNER JOIN GEPR_TDENOMINACION DN
	ON D.OID_DIVISA = DN.OID_DIVISA
WHERE
	DN.COD_DENOMINACION = []COD_DENOMINACION