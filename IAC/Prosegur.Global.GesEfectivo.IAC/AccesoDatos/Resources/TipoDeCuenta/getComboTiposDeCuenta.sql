SELECT DISTINCT
	cod_tipo_cuenta_bancaria 
FROM
	sapr_tdato_bancario
WHERE
	bol_activo = 1 
ORDER BY cod_tipo_cuenta_bancaria ASC