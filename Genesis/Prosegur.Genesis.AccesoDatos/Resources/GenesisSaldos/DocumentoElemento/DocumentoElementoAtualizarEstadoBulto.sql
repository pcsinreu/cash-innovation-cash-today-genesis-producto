﻿UPDATE SAPR_TDOCUMENTOXELEMENTO
   SET GMT_MODIFICACION = FN_GMT_ZERO_###VERSION###
	  ,DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION
	  ,COD_ESTADO_DOCXELEMENTO = []COD_ESTADO_DOCXELEMENTO
WHERE OID_DOCUMENTO	=[]OID_DOCUMENTO
  AND OID_BULTO = []OID_BULTO
