﻿DELETE SAPR_TABONO_ELEMENTO AEL
WHERE AEL.OID_ABONO_VALOR IN (SELECT AV.OID_ABONO_VALOR
                              FROM SAPR_TABONO_VALOR AV 
                              WHERE AV.OID_ABONO = []OID_ABONO)