﻿SELECT COD_PLANTA,
       OID_PLANTA,
       DES_PLANTA,
       COD_MIGRACION,
       BOL_ACTIVO,
       GMT_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_CREACION,
       DES_USUARIO_MODIFICACION
FROM GEPR_TPLANTA
WHERE OID_DELEGACION = []OID_DELEGACION