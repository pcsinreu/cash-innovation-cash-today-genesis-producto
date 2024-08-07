﻿SELECT MP.COD_MEDIO_PAGO,
       TMP.COD_TIPO_MEDIO_PAGO,
       TMP.NUM_IMPORTE,
       TMP.NEL_CANTIDAD,
       TMP.COD_NIVEL_DETALLE,
       TMP.BOL_DISPONIBLE,
       TMP.COD_ESTADO_DOCUMENTO,
       TMP.COD_TIPO_MOVIMIENTO,
	   TMP.OID_TRANSACCION_MEDIO_PAGO,
	   TMP.OID_DIVISA,
	   TMP.FYH_PLAN_CERTIFICACION
FROM SAPR_TTRANSACCION_MEDIO_PAGO TMP
LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = TMP.OID_MEDIO_PAGO
WHERE OID_TRANSACCION_MEDIO_PAGO = []OID_TRANSACCION_MEDIO_PAGO