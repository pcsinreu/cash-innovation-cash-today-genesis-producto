﻿SELECT 
 MP.OID_MEDIO_PAGO
,MP.COD_MEDIO_PAGO
,MP.COD_TIPO_MEDIO_PAGO
,D.COD_ISO_DIVISA
      FROM GEPR_TMEDIO_PAGO MP
INNER JOIN GEPR_TDIVISA D ON D.OID_DIVISA = MP.OID_DIVISA
{0}
     WHERE MP.BOL_VIGENTE = 1
	 {1}
