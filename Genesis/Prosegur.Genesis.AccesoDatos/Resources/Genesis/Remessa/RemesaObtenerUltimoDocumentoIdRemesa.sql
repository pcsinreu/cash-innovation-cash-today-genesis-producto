﻿SELECT DISTINCT DE.OID_DOCUMENTO FROM SAPR_TDOCUMENTOXELEMENTO DE
   INNER JOIN (
      SELECT 
         R.OID_REMESA
      FROM
         SAPR_TREMESA R
      START WITH R.OID_REMESA = []OID_REMESA
      CONNECT BY NOCYCLE PRIOR R.OID_REMESA = R.OID_REMESA_PADRE
   ) R ON DE.OID_REMESA = R.OID_REMESA
WHERE DE.COD_ESTADO_DOCXELEMENTO = []COD_ESTADO_DOCXELEMENTO