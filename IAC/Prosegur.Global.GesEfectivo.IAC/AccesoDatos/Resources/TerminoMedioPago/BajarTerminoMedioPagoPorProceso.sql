DELETE 
FROM GEPR_TTERMINO_MED_PAGO_POR_PRO TMPPPRC
WHERE TMPPPRC.OID_TERMINO_PROCESO IN (SELECT TMPPPRC.OID_TERMINO_PROCESO
FROM GEPR_TTERMINO_MED_PAGO_POR_PRO TMPPPRC
INNER JOIN GEPR_TPROCESO_SUBCANAL PRSUB ON TMPPPRC.OID_PROCESO_SUBCANAL = PRSUB.OID_PROCESO_SUBCANAL
INNER JOIN GEPR_TSUBCANAL SUB ON PRSUB.OID_SUBCANAL = SUB.OID_SUBCANAL
WHERE SUB.COD_SUBCANAL = []COD_SUBCANAL AND PRSUB.OID_PROCESO_POR_PSERVICIO = []OID_PROCESO_POR_PSERVICIO)