﻿SELECT
OID_TRANSACCION_MEDIO_PAGO
,OID_DOCUMENTO
,OID_CUENTA
,OID_CUENTA_SALDO
,OID_MEDIO_PAGO
,OID_CERTIFICADO
,COD_TIPO_MEDIO_PAGO
,COD_NIVEL_DETALLE
,BOL_DISPONIBLE
,NUM_IMPORTE
,NEL_CANTIDAD
,COD_ESTADO_DOCUMENTO
,COD_TIPO_MOVIMIENTO
,OBS_TRANSACCION
,COD_MIGRACION
,GMT_CREACION
,DES_USUARIO_CREACION
,GMT_MODIFICACION
,DES_USUARIO_MODIFICACION
,OID_DIVISA
,COD_TIPO_SITIO
,OID_UNIDAD_MEDIDA
,BOL_CONTRA_MOVIMIENTO
,FYH_PLAN_CERTIFICACION
FROM SAPR_TTRANSACCION_MEDIO_PAGO
WHERE BOL_CONTRA_MOVIMIENTO = 0 -- SOMENTE TRANSAÇÕES QUE NÃO FORAM ANULADAS
AND OID_DOCUMENTO			=[]OID_DOCUMENTO
AND COD_ESTADO_DOCUMENTO	=[]COD_ESTADO_DOCUMENTO
ORDER BY GMT_CREACION