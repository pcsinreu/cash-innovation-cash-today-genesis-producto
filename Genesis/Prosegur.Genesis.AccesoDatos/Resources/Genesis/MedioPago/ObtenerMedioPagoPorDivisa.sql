﻿SELECT 
    MP.OID_MEDIO_PAGO,
	MP.COD_MEDIO_PAGO,
	MP.DES_MEDIO_PAGO,
	MP.OBS_MEDIO_PAGO,
	MP.BOL_VIGENTE,
	MP.COD_USUARIO,
	MP.FYH_ACTUALIZACION,
	MP.COD_TIPO_MEDIO_PAGO
FROM 
	GEPR_TMEDIO_PAGO MP
WHERE
	MP.OID_DIVISA = []OID_DIVISA
	{0}

ORDER BY MP.COD_MEDIO_PAGO,MP.DES_MEDIO_PAGO