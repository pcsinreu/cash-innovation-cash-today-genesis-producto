﻿SELECT DISTINCT
       CR.OID_CONFIG_REPORTE,
       CR.COD_CONFIG_REPORTE,
       CR.DES_CONFIG_REPORTE,
       CR.DES_DIRECCION,
       CT.OID_CERTIFICADO,
       CT.COD_CERTIFICADO,
       CT.COD_ESTADO,
       CT.COD_EXTERNO,
       CT.FYH_CERTIFICADO,
	   CL.OID_CLIENTE,
       CL.COD_CLIENTE,
	   CL.DES_CLIENTE,
       SC.OID_SUBCANAL,
       SC.COD_SUBCANAL,
       SC.DES_SUBCANAL,
       D.COD_DELEGACION,
	   D.OID_DELEGACION,
	   RR.COD_SITUACION_REPORTE,
	   RR.FYH_INICIO_EJECUCION,
	   RR.FYH_FIN_EJECUCION,
	   RR.DES_ERROR_EJECUCION
FROM SAPR_TCERTIFICADO CT
INNER JOIN SAPR_TCERTIFICADOXDELEGACION CTD ON CTD.OID_CERTIFICADO = CT.OID_CERTIFICADO
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = CTD.OID_DELEGACION
INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CTSC ON CTSC.OID_CERTIFICADO = CT.OID_CERTIFICADO
INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = CTSC.OID_SUBCANAL
INNER JOIN SAPR_TCONFIG_NIVEL_SALDO NS ON NS.OID_CONFIG_NIVEL_SALDO = CT.OID_CONFIG_NIVEL_SALDO AND NS.OID_SUBCLIENTE IS NULL AND NS.OID_PTO_SERVICIO IS NULL
INNER JOIN GEPR_TCLIENTE CL ON CL.OID_CLIENTE = NS.OID_CLIENTE
INNER JOIN SAPR_TCONFIGRPXCLIENTE CRC ON CRC.OID_CLIENTE = CL.OID_CLIENTE
INNER JOIN SAPR_TCONFIG_REPORTE CR ON CR.OID_CONFIG_REPORTE = CRC.OID_CONFIG_REPORTE AND CR.NEL_TIPO_REPORTE = 1
LEFT JOIN SAPR_TRESULTADO_REPORTE RR ON RR.OID_CONFIG_REPORTE = CR.OID_CONFIG_REPORTE AND 
                                        RR.OID_ENTIDAD_REPORTE = CT.OID_CERTIFICADO AND
										RR.OID_SUBCANAL = SC.OID_SUBCANAL
WHERE  {0}

UNION

SELECT DISTINCT
       CR.OID_CONFIG_REPORTE,
       CR.COD_CONFIG_REPORTE,
       CR.DES_CONFIG_REPORTE,
       CR.DES_DIRECCION,
       CT.OID_CERTIFICADO,
       CT.COD_CERTIFICADO,
       CT.COD_ESTADO,
       CT.COD_EXTERNO,
       CT.FYH_CERTIFICADO,
       CL.OID_CLIENTE,
       CL.COD_CLIENTE,
	   CL.DES_CLIENTE,
       SC.OID_SUBCANAL,
       SC.COD_SUBCANAL,
       SC.DES_SUBCANAL,
       D.COD_DELEGACION,
	   D.OID_DELEGACION,
       RR.COD_SITUACION_REPORTE,
       RR.FYH_INICIO_EJECUCION,
       RR.FYH_FIN_EJECUCION,
       RR.DES_ERROR_EJECUCION
FROM SAPR_TCERTIFICADO CT
INNER JOIN SAPR_TCERTIFICADOXDELEGACION CTD ON CTD.OID_CERTIFICADO = CT.OID_CERTIFICADO
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = CTD.OID_DELEGACION
INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CTSC ON CTSC.OID_CERTIFICADO = CT.OID_CERTIFICADO
INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = CTSC.OID_SUBCANAL
INNER JOIN SAPR_TCONFIG_NIVEL_SALDO NS ON NS.OID_CONFIG_NIVEL_SALDO = CT.OID_CONFIG_NIVEL_SALDO AND NS.OID_SUBCLIENTE IS NULL AND NS.OID_PTO_SERVICIO IS NULL
INNER JOIN GEPR_TCLIENTE CL ON CL.OID_CLIENTE = NS.OID_CLIENTE
INNER JOIN SAPR_TCONFIGRPXTIPOCLI CRTC ON CRTC.OID_TIPO_CLIENTE = CL.OID_TIPO_CLIENTE
INNER JOIN SAPR_TCONFIG_REPORTE CR ON CR.OID_CONFIG_REPORTE = CRTC.OID_CONFIG_REPORTE AND CR.NEL_TIPO_REPORTE = 1
LEFT JOIN SAPR_TRESULTADO_REPORTE RR ON RR.OID_CONFIG_REPORTE = CR.OID_CONFIG_REPORTE AND 
                                        RR.OID_ENTIDAD_REPORTE = CT.OID_CERTIFICADO AND
										RR.OID_SUBCANAL = SC.OID_SUBCANAL
WHERE  {0}