﻿SELECT
  OID_SUBCLIENTE
FROM
  GEPR_TSUBCLIENTE
WHERE BOL_TOTALIZADOR_SALDO = 1
AND OID_SUBCLIENTE IN([]OID_SUBCLIENTE)