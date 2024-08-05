﻿SELECT DISTINCT LV.COD_VALOR
FROM GEPR_TMODULO M
INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = M.OID_LISTA_VALOR
INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LV.OID_LISTA_TIPO
WHERE LT.COD_TIPO = []COD_TIPO AND M.BOL_ACTIVO = 1