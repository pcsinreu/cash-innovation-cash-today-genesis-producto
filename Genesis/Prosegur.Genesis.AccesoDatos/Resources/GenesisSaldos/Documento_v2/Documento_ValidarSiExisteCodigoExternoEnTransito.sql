﻿SELECT DISTINCT 
          D.COD_EXTERNO
         ,DE.COD_ESTADO_DOCXELEMENTO
		 ,R.COD_ESTADO
      FROM SAPR_TDOCUMENTO D
INNER JOIN SAPR_TDOCUMENTOXELEMENTO DE ON DE.OID_DOCUMENTO = D.OID_DOCUMENTO
INNER JOIN SAPR_TREMESA R ON R.OID_REMESA = DE.OID_REMESA
     WHERE 1 = 1
	 {0}