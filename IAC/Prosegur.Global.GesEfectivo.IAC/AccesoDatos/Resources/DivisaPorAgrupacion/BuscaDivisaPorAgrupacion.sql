﻿SELECT DIV.COD_ISO_DIVISA, DPA.BOL_VIGENTE
FROM GEPR_TDIVISA DIV
INNER JOIN GEPR_TDIVISA_POR_AGRUPACION DPA ON DPA.OID_DIVISA = DIV.OID_DIVISA
WHERE DPA.OID_AGRUPACION = []OID_AGRUPACION