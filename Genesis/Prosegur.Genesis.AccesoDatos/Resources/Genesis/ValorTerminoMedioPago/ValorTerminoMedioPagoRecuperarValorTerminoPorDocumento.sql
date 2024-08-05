﻿SELECT P.DES_VALOR, P.NEC_INDICE_GRUPO
  FROM SAPR_TVALOR_TERMINO_MEDIO_PAGO P
 INNER JOIN SAPR_TMEDIO_PAGOXDOCUMENTO PD
    ON (P.OID_MEDIO_PAGOXDOCUMENTO = PD.OID_MEDIO_PAGOXDOCUMENTO)
 WHERE P.OID_TERMINO_MEDIO_PAGO = []pOID_TERMINO_MEDIO_PAGO
   AND PD.OID_DOCUMENTO = []pOID_DOCUMENTO
ORDER BY P.NEC_INDICE_GRUPO