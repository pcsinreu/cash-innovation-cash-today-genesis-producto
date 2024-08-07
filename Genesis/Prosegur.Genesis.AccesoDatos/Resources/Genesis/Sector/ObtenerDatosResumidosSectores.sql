SELECT 
    SEC.COD_SECTOR,
    SEC.DES_SECTOR,
    PLA.COD_PLANTA,
    DEL.COD_DELEGACION
FROM
    GEPR_TSECTOR SEC
    INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA
    INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PLA.OID_DELEGACION    
WHERE PLA.COD_PLANTA = []COD_PLANTA
	AND DEL.COD_DELEGACION = []COD_DELEGACION