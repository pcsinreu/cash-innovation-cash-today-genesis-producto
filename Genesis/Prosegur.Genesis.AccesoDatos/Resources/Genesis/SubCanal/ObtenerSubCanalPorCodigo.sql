﻿SELECT OID_SUBCANAL,
       COD_SUBCANAL,
       OID_CANAL,
       DES_SUBCANAL,
       OBS_SUBCANAL,
       BOL_VIGENTE,
       COD_USUARIO,
       FYH_ACTUALIZACION,
       BOL_POR_DEFECTO,
       COD_MIGRACION
  FROM GEPR_TSUBCANAL S
 WHERE S.COD_SUBCANAL = :COD_SUBCANAL