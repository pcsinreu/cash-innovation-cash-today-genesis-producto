﻿SELECT R.OID_REMESA
FROM GEPR_TREMESA R
INNER JOIN GEPR_TBULTO B ON B.OID_REMESA = R.OID_REMESA
WHERE B.COD_PRECINTO_BULTO = []COD_PRECINTO_BULTO