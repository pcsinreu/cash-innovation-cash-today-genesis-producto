﻿SELECT CTSTS.OID_TIPO_SECTOR, CTS.COD_CARACT_TIPOSECTOR
FROM GEPR_TCARACT_TIPOSECTOR CTS
INNER JOIN GEPR_TCARACTTIPOSECTORXTIPOSEC CTSTS ON CTSTS.OID_CARACT_TIPOSECTOR = CTS.OID_CARACT_TIPOSECTOR
WHERE 1 = 1
{0}
