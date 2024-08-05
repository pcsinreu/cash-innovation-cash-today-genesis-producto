SELECT
	oid_valor
FROM 
	gepr_tvalor_termino_iac
WHERE 
	  ( ([]oid_cliente IS NULL AND OID_CLIENTE IS NULL) OR (OID_CLIENTE = []oid_cliente) )
  AND ( ([]oid_subcliente IS NULL AND OID_SUBCLIENTE IS NULL) OR (OID_SUBCLIENTE = []oid_subcliente) )
  AND ( ([]oid_pto_servicio IS NULL AND OID_PTO_SERVICIO IS NULL) OR (OID_PTO_SERVICIO = []oid_pto_servicio) )
  AND oid_termino = []oid_termino
  AND cod_valor = []cod_valor