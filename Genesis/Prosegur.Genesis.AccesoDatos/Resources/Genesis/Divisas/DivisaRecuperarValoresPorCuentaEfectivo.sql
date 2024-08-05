﻿SELECT CU.OID_CUENTA,
	SF.OID_DIVISA,
    SF.OID_DENOMINACION,
    NULL AS OID_MEDIO_PAGO,
    NULL AS COD_TIPO_MEDIO_PAGO,
    SF.COD_NIVEL_DETALLE,
	SF.BOL_DISPONIBLE,
	SF.OID_UNIDAD_MEDIDA,	
	SF.OID_CALIDAD,	
    SUM(SF.NUM_IMPORTE) AS IMPORTE,
    SUM(SF.NEL_CANTIDAD) AS CANTIDAD
	{3}
FROM SAPR_TSALDO_EFECTIVO SF
INNER JOIN SAPR_TCUENTA CU ON CU.OID_CUENTA = SF.OID_CUENTA_SALDO
INNER JOIN GEPR_TCLIENTE CL ON CL.OID_CLIENTE = CU.OID_CLIENTE
INNER JOIN GEPR_TSUBCANAL SBC ON SBC.OID_SUBCANAL = CU.OID_SUBCANAL
INNER JOIN GEPR_TCANAL CAN ON CAN.OID_CANAL = SBC.OID_CANAL
{0}
INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR
INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = SE.OID_PLANTA
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
LEFT  JOIN GEPR_TSUBCLIENTE SCL ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
LEFT  JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO

{1} {2}
GROUP BY CU.OID_CUENTA, SF.OID_DIVISA, SF.OID_DENOMINACION, SF.COD_NIVEL_DETALLE, SF.BOL_DISPONIBLE,  SF.OID_UNIDAD_MEDIDA, SF.OID_CALIDAD
{3}