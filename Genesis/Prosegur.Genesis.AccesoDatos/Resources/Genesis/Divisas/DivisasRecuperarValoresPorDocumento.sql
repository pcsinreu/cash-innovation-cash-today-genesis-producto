﻿SELECT OID_DIVISA,
       OID_DENOMINACION,
       NULL AS OID_MEDIO_PAGO,
       NULL AS COD_TIPO_MEDIO_PAGO,
       COD_NIVEL_DETALLE,
       NULL AS BOL_DISPONIBLE,
	   OID_UNIDAD_MEDIDA,	
	   OID_CALIDAD,	
       SUM(NUM_IMPORTE) AS IMPORTE,
       SUM(NEL_CANTIDAD) AS CANTIDAD
  FROM SAPR_TEFECTIVOXDOCUMENTO
 WHERE OID_DOCUMENTO = []OID_DOCUMENTO
 GROUP BY OID_DIVISA, OID_DENOMINACION, COD_NIVEL_DETALLE, OID_UNIDAD_MEDIDA, OID_CALIDAD
UNION
SELECT OID_DIVISA,
       NULL AS OID_DENOMINACION,
       OID_MEDIO_PAGO,
       COD_TIPO_MEDIO_PAGO,
       COD_NIVEL_DETALLE,
       NULL AS BOL_DISPONIBLE,
	   NULL AS OID_UNIDAD_MEDIDA,	
	   NULL AS OID_CALIDAD,	
       SUM(NUM_IMPORTE) AS IMPORTE,
       SUM(NEL_CANTIDAD) AS CANTIDAD
  FROM SAPR_TMEDIO_PAGOXDOCUMENTO
 WHERE OID_DOCUMENTO = []OID_DOCUMENTO
 GROUP BY OID_DIVISA,
          COD_TIPO_MEDIO_PAGO,
          OID_MEDIO_PAGO,
          COD_NIVEL_DETALLE