﻿UPDATE SAPR_TBULTO B SET B.COD_PRECINTO = []PRECINTO,
B.COD_BOLSA = []COD_BOLSA,
B.DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION,
B.GMT_MODIFICACION = FN_GMT_ZERO_###VERSION###
WHERE B.OID_EXTERNO = []OID_EXTERNO