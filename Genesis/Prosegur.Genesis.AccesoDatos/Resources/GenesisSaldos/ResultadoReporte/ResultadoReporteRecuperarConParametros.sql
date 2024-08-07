﻿SELECT RP.OID_RESULTADO_REPORTE,
      RP.OID_ENTIDAD_REPORTE,
      RP.COD_SITUACION_REPORTE,
      RP.DES_ERROR_EJECUCION,
      RP.DES_NOMBRE_ARCHIVO,
      C.NEL_TIPO_REPORTE
FROM SAPR_TRESULTADO_REPORTE RP
INNER JOIN SAPR_TCONFIG_REPORTE C ON C.OID_CONFIG_REPORTE = RP.OID_CONFIG_REPORTE
{0}
WHERE RP.OID_CONFIG_REPORTE = []OID_CONFIG_REPORTE
{1}