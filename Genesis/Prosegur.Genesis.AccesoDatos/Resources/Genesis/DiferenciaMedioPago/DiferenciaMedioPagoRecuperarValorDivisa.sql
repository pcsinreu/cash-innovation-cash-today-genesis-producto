﻿SELECT SUM(DMP.NUM_IMPORTE) AS IMPORTE,
	   DMP.COD_TIPO_MEDIO_PAGO
FROM  SAPR_TDIFERENCIA_MEDIO_PAGO DMP
WHERE
DMP.OID_DIVISA = []OID_DIVISA AND DMP.OID_MEDIO_PAGO IS NULL {0}
GROUP BY DMP.COD_TIPO_MEDIO_PAGO