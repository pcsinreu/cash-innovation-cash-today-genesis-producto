﻿SELECT C.OID_CAJERO , C.COD_CAJERO , CL.COD_CLIENTE , CL.DES_CLIENTE, SC.COD_SUBCLIENTE , SC.DES_SUBCLIENTE,
PS.COD_PTO_SERVICIO,PS.DES_PTO_SERVICIO, C.BOL_VIGENTE
 FROM GEPR_TCAJERO C
INNER JOIN GEPR_TPUNTO_SERVICIO PS
ON C.OID_PTO_SERVICIO = PS.OID_PTO_SERVICIO
INNER JOIN GEPR_TSUBCLIENTE SC 
ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
INNER JOIN GEPR_TCLIENTE CL
ON CL.OID_CLIENTE = SC.OID_CLIENTE