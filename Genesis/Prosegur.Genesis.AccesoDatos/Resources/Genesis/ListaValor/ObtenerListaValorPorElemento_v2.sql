﻿SELECT
 LT.COD_TIPO
,VE.OID_REMESA
,VE.OID_BULTO
,VE.OID_PARCIAL
,LV.OID_LISTA_VALOR
,LV.COD_VALOR
,LV.DES_VALOR
,LV.BOL_DEFECTO
,B.OID_EXTERNO OID_BULTO_EXTERNO
,M.OID_MODULO
FROM
  GEPR_TLISTA_VALOR LV
  INNER JOIN SAPR_TLISTA_VALORXELEMENTO VE ON VE.OID_LISTA_VALOR = LV.OID_LISTA_VALOR
  INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = VE.OID_LISTA_TIPO
  LEFT JOIN SAPR_TBULTO B ON B.OID_BULTO = VE.OID_BULTO
  LEFT JOIN GEPR_TMODULO M ON LV.OID_LISTA_VALOR = M.OID_LISTA_VALOR
WHERE
  LT.COD_TIPO = []COD_TIPO {0}