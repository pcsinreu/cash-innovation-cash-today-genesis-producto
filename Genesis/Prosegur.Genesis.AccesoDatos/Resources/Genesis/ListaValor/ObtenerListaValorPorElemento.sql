﻿SELECT
	LV.OID_LISTA_VALOR,
	LV.COD_VALOR,
	LV.DES_VALOR,
	LV.BOL_DEFECTO
FROM
	GEPR_TLISTA_VALOR LV
	INNER JOIN SAPR_TLISTA_VALORXELEMENTO VE ON VE.OID_LISTA_VALOR = LV.OID_LISTA_VALOR
	INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = VE.OID_LISTA_TIPO
WHERE
	LT.COD_TIPO = []COD_TIPO AND 
	{0}
