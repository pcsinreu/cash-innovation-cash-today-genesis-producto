UPDATE GEPR_TPROCESO_POR_PSERVICIO
SET BOL_VIGENTE = 
(SELECT COUNT(1) RESULTADO
FROM GEPR_TPROCESO_SUBCANAL PRSUB
WHERE PRSUB.OID_PROCESO_POR_PSERVICIO = []OID_PROCESO_POR_PSERVICIO
AND PRSUB.BOL_VIGENTE = 1
AND ROWNUM = 1),
COD_USUARIO = []COD_USUARIO,
FYH_ACTUALIZACION = []FYH_ACTUALIZACION
WHERE OID_PROCESO_POR_PSERVICIO = []OID_PROCESO_POR_PSERVICIO