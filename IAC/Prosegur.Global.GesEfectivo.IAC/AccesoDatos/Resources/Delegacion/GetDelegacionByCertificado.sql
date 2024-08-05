﻿SELECT D.COD_DELEGACION,CD.OID_DELEGACION, D.DES_DELEGACION, P.OID_PLANTA, P.COD_PLANTA, P.DES_PLANTA
FROM SAPR_TCERTIFICADOXDELEGACION CD
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = CD.OID_DELEGACION
INNER JOIN SAPR_TCERTIFICADO C ON C.OID_CERTIFICADO = CD.OID_CERTIFICADO
INNER JOIN GEPR_TPLANTA P ON P.OID_DELEGACION = D.OID_DELEGACION
WHERE C.COD_CERTIFICADO = []COD_CERTIFICADO