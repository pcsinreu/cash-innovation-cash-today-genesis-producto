﻿SELECT TSETOR.OID_TIPO_SECTOR, TSETOR.COD_TIPO_SECTOR, TSETOR.DES_TIPO_SECTOR,  
TSETOR.BOL_ACTIVO, TSETOR.GMT_CREACION, TSETOR.DES_USUARIO_CREACION, TSETOR.GMT_MODIFICACION, TSETOR.DES_USUARIO_MODIFICACION,
TC.OID_CARACTTIPOSECTORXTIPOSEC, TCT.COD_CARACT_TIPOSECTOR, TCT.DES_CARACT_TIPOSECTOR
FROM GEPR_TTIPO_SECTOR TSETOR
LEFT JOIN GEPR_TCARACTTIPOSECTORXTIPOSEC TC ON TC.OID_TIPO_SECTOR = TSETOR.OID_TIPO_SECTOR
LEFT JOIN GEPR_TCARACT_TIPOSECTOR TCT ON TCT.OID_CARACT_TIPOSECTOR = TC.OID_CARACT_TIPOSECTOR