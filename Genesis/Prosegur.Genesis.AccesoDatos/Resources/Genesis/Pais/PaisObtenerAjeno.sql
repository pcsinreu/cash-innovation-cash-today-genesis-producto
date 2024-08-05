﻿SELECT
      P.OID_PAIS,
      P.COD_PAIS,
      P.DES_PAIS,
      P.BOL_ACTIVO,
      P.GMT_CREACION,
      P.DES_USUARIO_CREACION,
      P.GMT_MODIFICACION,
      P.DES_USUARIO_MODIFICACION
FROM GEPR_TPAIS P 
INNER JOIN GEPR_TCODIGO_AJENO CAPA ON CAPA.OID_TABLA_GENESIS = P.OID_PAIS AND CAPA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS'
WHERE CAPA.COD_AJENO = []COD_AJENO AND CAPA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR