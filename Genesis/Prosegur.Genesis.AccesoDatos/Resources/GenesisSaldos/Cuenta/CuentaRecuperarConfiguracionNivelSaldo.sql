﻿SELECT
	OID_CONFIG_NIVEL_SALDO,
	OID_CLIENTE,
	OID_SUBCLIENTE,
	OID_PTO_SERVICIO
FROM
	SAPR_TCONFIG_NIVEL_SALDO
WHERE
	OID_CLIENTE = []OID_CLIENTE
	####FILTROS####