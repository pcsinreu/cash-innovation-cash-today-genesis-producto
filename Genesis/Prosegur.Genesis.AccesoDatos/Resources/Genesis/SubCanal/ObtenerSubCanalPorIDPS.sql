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
 WHERE S.OID_SUBCANAL IN 
       (SELECT C.OID_TABLA_GENESIS
          FROM GEPR_TCODIGO_AJENO C
         WHERE C.COD_AJENO = []COD_AJENO
           AND COD_IDENTIFICADOR = 'IDPS'
           AND COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL')