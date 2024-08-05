﻿SELECT 
DOC.COD_COMPROBANTE
,F.DES_FORMULARIO
,SO.DES_SECTOR AS SETOR_ORIGEM
,SD.DES_SECTOR AS SETOR_DESTINO
FROM SAPR_TDOCUMENTO DOC
INNER JOIN SAPR_TFORMULARIO F ON F.OID_FORMULARIO = DOC.OID_FORMULARIO
INNER JOIN SAPR_TCUENTA CO ON CO.OID_CUENTA =DOC.OID_CUENTA_ORIGEN 
INNER JOIN SAPR_TCUENTA CD ON CD.OID_CUENTA = DOC.OID_CUENTA_DESTINO
INNER JOIN GEPR_TSECTOR SO ON SO.OID_SECTOR = CO.OID_SECTOR
INNER JOIN GEPR_TSECTOR SD ON SD.OID_SECTOR = CD.OID_SECTOR
WHERE DOC.OID_DOCUMENTO =[]OID_DOCUMENTO