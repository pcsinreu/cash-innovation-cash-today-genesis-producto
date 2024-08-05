﻿SELECT DIV.OID_DIVISA
      ,SEA.COD_NIVEL_DETALLE    
      ,SEA.COD_TIPO_EFECTIVO_TOTAL
      ,DENO.OID_DENOMINACION
      ,U.OID_UNIDAD_MEDIDA
      ,CAL.OID_CALIDAD
  FROM SAPR_TEFECTIVO_ANTERIORXDOC SEA INNER JOIN SAPR_TDOCUMENTO DOC ON DOC.OID_DOCUMENTO = SEA.OID_DOCUMENTO
                                       INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = SEA.OID_DIVISA
                                       LEFT JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = SEA.OID_DENOMINACION
                                       LEFT JOIN GEPR_TUNIDAD_MEDIDA U  ON U.OID_UNIDAD_MEDIDA = SEA.OID_UNIDAD_MEDIDA    
                                       LEFT JOIN GEPR_TCALIDAD CAL  ON CAL.OID_CALIDAD = SEA.OID_CALIDAD  
WHERE SEA.OID_DOCUMENTO = []OID_DOCUMENTO
ORDER BY 1