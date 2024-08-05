
SELECT
	
	 P.OID_PTO_SERVICIO  AS IDENTIFICADOR
	,P.OID_SUBCLIENTE  AS IDENTIFICADOR_PAI
	,P.COD_PTO_SERVICIO AS CODIGO 
	,P.DES_PTO_SERVICIO AS DESCRICAO
	,P.BOL_VIGENTE AS VIGENTE
	,P.COD_USUARIO
	,P.FYH_ACTUALIZACION
	,P.BOL_ENVIADO_SALDOS
	,P.OID_TIPO_PUNTO_SERVICIO
	,P.COD_MIGRACION
	,P.BOL_TOTALIZADOR_SALDO
	
FROM
	GEPR_TPUNTO_SERVICIO P