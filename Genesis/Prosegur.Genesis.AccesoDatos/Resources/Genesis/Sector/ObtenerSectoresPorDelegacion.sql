SELECT 
  SECT.DES_SECTOR
FROM 
  GEPR_TSECTOR SECT
  INNER JOIN GEPR_TPLANTA PLANT ON SECT.OID_PLANTA = PLANT.OID_PLANTA
  INNER JOIN GEPR_TDELEGACION DELE ON PLANT.OID_DELEGACION = DELE.OID_DELEGACION
WHERE DELE.COD_DELEGACION = :COD_DELEGACION
