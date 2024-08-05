﻿SELECT 
      RR.OID_RESULTADO_REPORTE,
      RR.OID_ENTIDAD_REPORTE,
      RR.COD_SITUACION_REPORTE,
      RR.DES_ERROR_EJECUCION,
      RR.DES_NOMBRE_ARCHIVO,
      C.NEL_TIPO_REPORTE
FROM SAPR_TRESULTADO_REPORTE RR
INNER JOIN SAPR_TCONFIG_REPORTE C ON C.OID_CONFIG_REPORTE = RR.OID_CONFIG_REPORTE
WHERE 0 = 0 