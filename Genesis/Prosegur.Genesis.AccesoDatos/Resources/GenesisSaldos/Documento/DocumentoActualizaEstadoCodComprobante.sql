﻿UPDATE SAPR_TDOCUMENTO
SET COD_ESTADO = []COD_ESTADO
    ,COD_COMPROBANTE = []COD_COMPROBANTE
	,DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION
	,GMT_MODIFICACION = FN_GMT_ZERO_###VERSION###
WHERE OID_DOCUMENTO = []OID_DOCUMENTO