﻿SELECT 
      CF.OID_CONFIG_REPORTE, 
      CF.COD_CONFIG_REPORTE, 
      CF.DES_CONFIG_REPORTE, 
      CF.DES_DIRECCION,
      CF.DES_USUARIO_CREACION,
      CF.NEL_TIPO_REPORTE,
      CF.COD_MASCARA_NOMBRE,
      CF.COD_RENDERIZADOR,
      CF.DES_EXTENSION,
      CF.DES_SEPARADOR,
    CTRP.OID_TIPO_REPORTEXPARAMETRO,
      CTRP.COD_PARAMETRO,
      CTRP.DES_PARAMETRO
FROM SAPR_TCONFIG_REPORTE CF
INNER JOIN SAPR_TCONFIGRPXPARAMETRO CRP ON CRP.OID_CONFIG_REPORTE = CF.OID_CONFIG_REPORTE
INNER JOIN SAPR_TTIPO_REPORTEXPARAMETRO CTRP ON CTRP.OID_TIPO_REPORTEXPARAMETRO = CRP.OID_TIPO_REPORTEXPARAMETRO
INNER JOIN SAPR_TCONFIGRPXCLIENTE C ON CF.OID_CONFIG_REPORTE = C.OID_CONFIG_REPORTE
WHERE C.OID_CLIENTE = []OID_CLIENTE {0}
UNION
SELECT 
      CF.OID_CONFIG_REPORTE, 
      CF.COD_CONFIG_REPORTE, 
      CF.DES_CONFIG_REPORTE, 
      CF.DES_DIRECCION,
      CF.DES_USUARIO_CREACION,
      CF.NEL_TIPO_REPORTE,
      CF.COD_MASCARA_NOMBRE,
      CF.COD_RENDERIZADOR,
      CF.DES_EXTENSION,
      CF.DES_SEPARADOR,
    CTRP.OID_TIPO_REPORTEXPARAMETRO,
      CTRP.COD_PARAMETRO,
      CTRP.DES_PARAMETRO
FROM SAPR_TCONFIG_REPORTE CF
INNER JOIN SAPR_TCONFIGRPXTIPOCLI CTC ON CF.OID_CONFIG_REPORTE = CTC.OID_CONFIG_REPORTE
INNER JOIN SAPR_TCONFIGRPXPARAMETRO CRP ON CRP.OID_CONFIG_REPORTE = CF.OID_CONFIG_REPORTE
INNER JOIN SAPR_TTIPO_REPORTEXPARAMETRO CTRP ON CTRP.OID_TIPO_REPORTEXPARAMETRO = CRP.OID_TIPO_REPORTEXPARAMETRO
WHERE 1=1 AND CTC.OID_TIPO_CLIENTE = []OID_TIPO_CLIENTE {0}
