SELECT PUE.OID_PUESTO
FROM GEPR_TPUESTO PUE
WHERE PUE.COD_PUESTO = :COD_PUESTO
AND PUE.COD_DELEGACION = :COD_DELEGACION