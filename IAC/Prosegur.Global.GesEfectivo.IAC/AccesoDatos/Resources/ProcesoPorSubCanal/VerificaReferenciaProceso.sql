SELECT PRSUB.OID_PROCESO_SUBCANAL, PRSUB.OID_PROCESO_POR_PSERVICIO
FROM GEPR_TPROCESO_SUBCANAL PRSUB
INNER JOIN GEPR_TSUBCANAL SUB ON PRSUB.OID_SUBCANAL = SUB.OID_SUBCANAL
