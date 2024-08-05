SELECT 
	COD_CLIENTE, 
	COD_SUBCLIENTE,
	COD_PTO_SERVICIO, 
	COD_SUBCANAL, 
	DES_SUBCANAL, 
	COD_DELEGACION, 
	DES_PROCESO, 
	OBS_PROCESO_SUBCANAL, 
	COD_CLIENTE AS COD_CLIENTE_FACTURACION, 
	BOL_CONTAR_CHEQUES_TOTAL, 
	BOL_CONTAR_TICKETS_TOTAL,
	BOL_CONTAR_OTROS_TOTAL, 
	BOL_CONTAR_TARJETAS_TOTAL,
	BOL_VIGENTE,
	OID_PRODUCTO,
	OID_PROCESO,
	COD_PRODUCTO,
	DES_PRODUCTO,
	DES_CLASE_BILLETE,
	NUM_FACTOR_CORRECCION,
	BOL_MANUAL,
	OID_TIPO_PROCESADO, 
	COD_TIPO_PROCESADO,
	DES_TIPO_PROCESADO,
	OID_IAC, 
	OID_IAC_BULTO, 
	OID_IAC_REMESA, 
	OID_CLIENTE, 
	BOL_MEDIOS_PAGO, 
	OID_PROCESO_SUBCANAL, 
	OID_SUBCANAL, 
	OID_SUBCLIENTE, 
	OID_PTO_SERVICIO 
FROM 
	(
		--PEGA O NIVEL MAIS ALTO DA DEPENDENCIA DO PROCESSO COM O CLIENTE. 
		SELECT 
			3 AS NIVELA, 
			CASE WHEN PSERV.COD_DELEGACION = []COD_DELEGACION THEN '1' 
			WHEN PSERV.COD_DELEGACION = []DELEGACIONCENTRAL THEN '2' END AS NIVELB, 
			[]COD_CLIENTE AS COD_CLIENTE, 
			[]COD_SUBCLIENTE AS COD_SUBCLIENTE, 
			[]COD_PTO_SERVICIO AS COD_PTO_SERVICIO, 
			SUB.COD_SUBCANAL, 
			SUB.DES_SUBCANAL, 
			PSERV.COD_DELEGACION, 
			PROC.DES_PROCESO,
			PSUB.OBS_PROCESO_SUBCANAL,
			CLIFAT.COD_CLIENTE AS COD_CLIENTE_FACTURACION,
			PROC.BOL_CONTAR_CHEQUES_TOTAL,
			PROC.BOL_CONTAR_TICKETS_TOTAL,
			PROC.BOL_CONTAR_OTROS_TOTAL,
			PROC.BOL_CONTAR_TARJETAS_TOTAL,
			PROC.BOL_VIGENTE,
			PROC.OID_PRODUCTO,
			PROC.OID_PROCESO,
			PROD.COD_PRODUCTO,
			PROD.DES_PRODUCTO,
			PROD.DES_CLASE_BILLETE,
			PROD.NUM_FACTOR_CORRECCION,
			PROD.BOL_MANUAL,
			TPPROC.OID_TIPO_PROCESADO, 
			TPPROC.COD_TIPO_PROCESADO,
			TPPROC.DES_TIPO_PROCESADO, 
			PSERV.OID_IAC,
			PSERV.OID_IAC_BULTO,
			PSERV.OID_IAC_REMESA,
			CLI.OID_CLIENTE,
			PROC.BOL_MEDIOS_PAGO,
			PSUB.OID_PROCESO_SUBCANAL,
			PSUB.OID_SUBCANAL,
			PSERV.OID_SUBCLIENTE,
			PSERV.OID_PTO_SERVICIO 
		FROM 
			GEPR_TPROCESO_POR_PSERVICIO PSERV 
			INNER JOIN GEPR_TCLIENTE CLI ON PSERV.OID_CLIENTE = CLI.OID_CLIENTE 
			INNER JOIN GEPR_TPROCESO_SUBCANAL PSUB ON PSERV.OID_PROCESO_POR_PSERVICIO = PSUB.OID_PROCESO_POR_PSERVICIO 
			INNER JOIN GEPR_TSUBCANAL SUB ON PSUB.OID_SUBCANAL = SUB.OID_SUBCANAL 
			INNER JOIN GEPR_TPROCESO PROC ON PSERV.OID_PROCESO = PROC.OID_PROCESO 
			INNER JOIN GEPR_TPRODUCTO PROD ON PROC.OID_PRODUCTO = PROD.OID_PRODUCTO 
			INNER JOIN GEPR_TTIPO_PROCESADO TPPROC ON PROC.OID_TIPO_PROCESADO = TPPROC.OID_TIPO_PROCESADO 
			LEFT JOIN GEPR_TCLIENTE CLIFAT ON PSERV.OID_CLIENTE_FACTURACION = CLIFAT.OID_CLIENTE 
		WHERE 
			CLI.COD_CLIENTE = []COD_CLIENTE 
			AND PSERV.OID_SUBCLIENTE IS NULL 
			AND PSERV.OID_PTO_SERVICIO IS NULL 
			AND PSERV.COD_DELEGACION IN ([]COD_DELEGACION, []DELEGACIONCENTRAL) 
			AND SUB.COD_SUBCANAL = []COD_SUBCANAL 
			AND PSERV.BOL_VIGENTE = 1 
			AND PSUB.BOL_VIGENTE = 1 
			{0} -- Parametro OID_PROCESO opcional: AND PROC.OID_PROCESO = []OID_PROCESO 
		UNION 
		--PEGA O 2 NIVEL DA DEPENDENCIA DO PROCESSO COM O CLIENTE. 
		SELECT 
			2 AS NIVELA, 
			CASE WHEN PSERV.COD_DELEGACION = []COD_DELEGACION THEN '1' 
			WHEN PSERV.COD_DELEGACION = []DELEGACIONCENTRAL THEN '2' END AS NIVELB, 
			[]COD_CLIENTE AS COD_CLIENTE, 
			[]COD_SUBCLIENTE AS COD_SUBCLIENTE, 
			[]COD_PTO_SERVICIO AS COD_PTO_SERVICIO, 
			SUB.COD_SUBCANAL, 
			SUB.DES_SUBCANAL, 
			PSERV.COD_DELEGACION, 
			PROC.DES_PROCESO, 
			PSUB.OBS_PROCESO_SUBCANAL, 
			CLIFAT.COD_CLIENTE AS COD_CLIENTE_FACTURACION, 
			PROC.BOL_CONTAR_CHEQUES_TOTAL, 
			PROC.BOL_CONTAR_TICKETS_TOTAL, 
			PROC.BOL_CONTAR_OTROS_TOTAL, 
			PROC.BOL_CONTAR_TARJETAS_TOTAL, 
			PROC.BOL_VIGENTE, 
			PROC.OID_PRODUCTO, 
			PROC.OID_PROCESO, 
			PROD.COD_PRODUCTO, 
			PROD.DES_PRODUCTO, 
			PROD.DES_CLASE_BILLETE, 
			PROD.NUM_FACTOR_CORRECCION, 
			PROD.BOL_MANUAL, 
			TPPROC.OID_TIPO_PROCESADO, 
			TPPROC.COD_TIPO_PROCESADO, 
			TPPROC.DES_TIPO_PROCESADO, 
			PSERV.OID_IAC, 
			PSERV.OID_IAC_BULTO, 
			PSERV.OID_IAC_REMESA, 
			CLI.OID_CLIENTE, 
			PROC.BOL_MEDIOS_PAGO, 
			PSUB.OID_PROCESO_SUBCANAL, 
			PSUB.OID_SUBCANAL, 
			PSERV.OID_SUBCLIENTE, 
			PSERV.OID_PTO_SERVICIO 
		FROM 
			GEPR_TPROCESO_POR_PSERVICIO PSERV 
			INNER JOIN GEPR_TPROCESO_SUBCANAL PSUB ON PSERV.OID_PROCESO_POR_PSERVICIO = PSUB.OID_PROCESO_POR_PSERVICIO 
			INNER JOIN GEPR_TSUBCANAL SUB ON PSUB.OID_SUBCANAL = SUB.OID_SUBCANAL 
			INNER JOIN GEPR_TCLIENTE CLI ON PSERV.OID_CLIENTE = CLI.OID_CLIENTE 
			INNER JOIN GEPR_TSUBCLIENTE SUBCLI ON PSERV.OID_SUBCLIENTE = SUBCLI.OID_SUBCLIENTE 
			INNER JOIN GEPR_TPROCESO PROC ON PSERV.OID_PROCESO = PROC.OID_PROCESO 
			INNER JOIN GEPR_TPRODUCTO PROD ON PROC.OID_PRODUCTO = PROD.OID_PRODUCTO 
			INNER JOIN GEPR_TTIPO_PROCESADO TPPROC ON PROC.OID_TIPO_PROCESADO = TPPROC.OID_TIPO_PROCESADO 
			LEFT JOIN GEPR_TCLIENTE CLIFAT ON PSERV.OID_CLIENTE_FACTURACION = CLIFAT.OID_CLIENTE 
		WHERE 
			CLI.COD_CLIENTE = []COD_CLIENTE 
			AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE 
			AND PSERV.OID_PTO_SERVICIO IS NULL 
			AND PSERV.COD_DELEGACION IN ([]COD_DELEGACION, []DELEGACIONCENTRAL) 
			AND SUB.COD_SUBCANAL = []COD_SUBCANAL 
			AND PSERV.BOL_VIGENTE = 1 
			AND PSUB.BOL_VIGENTE = 1 
			{0} -- Parametro OID_PROCESO opcional: AND PROC.OID_PROCESO = []OID_PROCESO 
		UNION 
		--PEGA O NIVEL MAIS BAIXO DA DEPENDENCIA DO PROCESSO COM O CLIENTE. 
		SELECT 
			1 AS NIVELA, 
			CASE WHEN PSERV.COD_DELEGACION = []COD_DELEGACION THEN '1' 
			WHEN PSERV.COD_DELEGACION = []DELEGACIONCENTRAL THEN '2' END AS NIVELB, 
			[]COD_CLIENTE AS COD_CLIENTE,
			[]COD_SUBCLIENTE AS COD_SUBCLIENTE, 
			[]COD_PTO_SERVICIO AS COD_PTO_SERVICIO, 
			SUB.COD_SUBCANAL, 
			SUB.DES_SUBCANAL, 
			PSERV.COD_DELEGACION, 
			PROC.DES_PROCESO, 
			PSUB.OBS_PROCESO_SUBCANAL, 
			CLIFAT.COD_CLIENTE AS COD_CLIENTE_FACTURACION, 
			PROC.BOL_CONTAR_CHEQUES_TOTAL, 
			PROC.BOL_CONTAR_TICKETS_TOTAL, 
			PROC.BOL_CONTAR_OTROS_TOTAL, 
			PROC.BOL_CONTAR_TARJETAS_TOTAL, 
			PSERV.BOL_VIGENTE, 
			PROC.OID_PRODUCTO, 
			PROC.OID_PROCESO, 
			PROD.COD_PRODUCTO, 
			PROD.DES_PRODUCTO, 
			PROD.DES_CLASE_BILLETE, 
			PROD.NUM_FACTOR_CORRECCION, 
			PROD.BOL_MANUAL, 
			TPPROC.OID_TIPO_PROCESADO, 
			TPPROC.COD_TIPO_PROCESADO, 
			TPPROC.DES_TIPO_PROCESADO, 
			PSERV.OID_IAC, 
			PSERV.OID_IAC_BULTO, 
			PSERV.OID_IAC_REMESA, 
			CLI.OID_CLIENTE, 
			PROC.BOL_MEDIOS_PAGO, 
			PSUB.OID_PROCESO_SUBCANAL, 
			PSUB.OID_SUBCANAL, 
			PSERV.OID_SUBCLIENTE, 
			PSERV.OID_PTO_SERVICIO 
		FROM 
			GEPR_TPROCESO_POR_PSERVICIO PSERV 
			INNER JOIN GEPR_TPROCESO_SUBCANAL PSUB ON PSERV.OID_PROCESO_POR_PSERVICIO = PSUB.OID_PROCESO_POR_PSERVICIO 
			INNER JOIN GEPR_TSUBCANAL SUB ON PSUB.OID_SUBCANAL = SUB.OID_SUBCANAL 
			INNER JOIN GEPR_TCLIENTE CLI ON PSERV.OID_CLIENTE = CLI.OID_CLIENTE 
			INNER JOIN GEPR_TSUBCLIENTE SUBCLI ON PSERV.OID_SUBCLIENTE = SUBCLI.OID_SUBCLIENTE 
			INNER JOIN GEPR_TPUNTO_SERVICIO PTO ON PSERV.OID_PTO_SERVICIO = PTO.OID_PTO_SERVICIO 
			INNER JOIN GEPR_TPROCESO PROC ON PSERV.OID_PROCESO = PROC.OID_PROCESO 
			INNER JOIN GEPR_TPRODUCTO PROD ON PROC.OID_PRODUCTO = PROD.OID_PRODUCTO 
			INNER JOIN GEPR_TTIPO_PROCESADO TPPROC ON PROC.OID_TIPO_PROCESADO = TPPROC.OID_TIPO_PROCESADO 
			LEFT JOIN GEPR_TCLIENTE CLIFAT ON PSERV.OID_CLIENTE_FACTURACION = CLIFAT.OID_CLIENTE 
		WHERE 
			CLI.COD_CLIENTE = []COD_CLIENTE 
			AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE 
			AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO 
			AND PSERV.COD_DELEGACION IN ([]COD_DELEGACION, []DELEGACIONCENTRAL) 
			AND SUB.COD_SUBCANAL = []COD_SUBCANAL 
			AND PSERV.BOL_VIGENTE = 1 
			AND PSUB.BOL_VIGENTE = 1 
			{0} -- Parametro OID_PROCESO opcional: AND PROC.OID_PROCESO = []OID_PROCESO 
	) PROCESO 
WHERE 
	ROWNUM = 1 
ORDER BY 
	NIVELA, NIVELB ASC