SELECT GEPR_TPUESTO.OID_PUESTO FROM GEPR_TPUESTO 
INNER JOIN GEPR_TAPLICACION ON 
	GEPR_TAPLICACION.OID_APLICACION = GEPR_TPUESTO.OID_APLICACION
INNER JOIN GEPR_TDELEGACION ON
  GEPR_TDELEGACION.OID_DELEGACION = GEPR_TPUESTO.OID_DELEGACION
WHERE  trim(COD_PUESTO) = trim(upper([]COD_PUESTO))
AND COD_APLICACION = []COD_APLICACION
AND GEPR_TDELEGACION.COD_DELEGACION = []COD_DELEGACION