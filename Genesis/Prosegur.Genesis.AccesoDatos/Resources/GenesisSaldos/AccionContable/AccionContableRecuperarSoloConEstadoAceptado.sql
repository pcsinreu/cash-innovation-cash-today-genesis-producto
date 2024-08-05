﻿SELECT
      A.OID_ACCION_CONTABLE,
      A.COD_ACCION_CONTABLE,
      A.DES_ACCION_CONTABLE,
      A.BOL_ACTIVO,
      A.GMT_CREACION,
      A.DES_USUARIO_CREACION,
      A.GMT_MODIFICACION,
      A.DES_USUARIO_MODIFICACION
FROM SAPR_TACCION_CONTABLE A
LEFT OUTER JOIN SAPR_TESTADOXACCION_CONTABLE B ON A.OID_ACCION_CONTABLE = B.OID_ACCION_CONTABLE
WHERE A.BOL_ACTIVO = 1
GROUP BY
      A.OID_ACCION_CONTABLE,
      A.COD_ACCION_CONTABLE,
      A.DES_ACCION_CONTABLE,
      A.BOL_ACTIVO,
      A.GMT_CREACION,
      A.DES_USUARIO_CREACION,
      A.GMT_MODIFICACION,
      A.DES_USUARIO_MODIFICACION
HAVING
      SUM(CASE B.COD_ESTADO
        WHEN 'AC' THEN
          0
        WHEN 'CF' THEN
          CASE WHEN B.COD_ACCION_ORIGEN_DISPONIBLE = '0' AND B.COD_ACCION_ORIGEN_NODISP = '0' AND B.COD_ACCION_DESTINO_DISPONIBLE = '0' AND B.COD_ACCION_DESTINO_NODISP = '0' THEN
            0
          ELSE
            1
          END
        WHEN 'RC' THEN
          CASE WHEN B.COD_ACCION_ORIGEN_DISPONIBLE = '0' AND B.COD_ACCION_ORIGEN_NODISP = '0' AND B.COD_ACCION_DESTINO_DISPONIBLE = '0' AND B.COD_ACCION_DESTINO_NODISP = '0' THEN
            0
          ELSE
            1
          END 
        ELSE
          0        
      END) = 0
ORDER BY A.DES_ACCION_CONTABLE