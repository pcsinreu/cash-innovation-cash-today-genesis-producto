﻿DELETE FROM 
	GEPR_TVALOR_TERMINO_IAC
 WHERE 
	OID_CLIENTE = (SELECT OID_CLIENTE
                   FROM GEPR_TCLIENTE
                   WHERE COD_CLIENTE = []COD_CLIENTE)
	AND OID_TERMINO = (SELECT OID_TERMINO
                       FROM GEPR_TTERMINO
                       WHERE COD_TERMINO = []COD_TERMINO)