﻿SELECT 
      C.OID_COPIA,
      C.OID_FORMULARIO,
      C.DES_DESTINO_COPIA,
      C.NEL_CANTIDAD_COPIA,
      C.COD_MIGRACION,
      C.GMT_CREACION,
      C.DES_USUARIO_CREACION,
      C.GMT_MODIFICACION,
      C.DES_USUARIO_MODIFICACION                        
from SAPR_TCOPIA C
WHERE C.OID_FORMULARIO = []OID_FORMULARIO