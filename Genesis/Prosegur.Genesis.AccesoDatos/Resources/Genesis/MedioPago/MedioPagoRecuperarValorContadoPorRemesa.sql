﻿SELECT SUM(CMP.NUM_IMPORTE)AS NUM_IMPORTE,
	   SUM(CMP.NEL_CANTIDAD)AS CANTIDAD,
       CMP.COD_TIPO_CONTADO,
	   CMP.OID_CONTADO_MEDIO_PAGO
FROM SAPR_TCONTADO_MEDIO_PAGO CMP
WHERE CMP.OID_MEDIO_PAGO = []OID_MEDIO_PAGO AND CMP.OID_REMESA = []OID_ELEMENTO AND CMP.OID_BULTO IS NULL AND CMP.OID_PARCIAL IS NULL
GROUP BY CMP.COD_TIPO_CONTADO, CMP.OID_CONTADO_MEDIO_PAGO
