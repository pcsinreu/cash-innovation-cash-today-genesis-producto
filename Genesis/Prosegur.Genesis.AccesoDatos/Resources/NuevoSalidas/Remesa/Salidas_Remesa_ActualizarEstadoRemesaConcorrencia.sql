UPDATE GEPR_TREMESA R 
SET R.COD_ESTADO_REMESA = []COD_ESTADO_REMESA 
	,R.COD_USUARIO = []COD_USUARIO 
	,R.FYH_ACTUALIZACION = []FYH_ACTUALIZACION_NUEVA
	--R.COD_USUARIO_BLOQUEO = []COD_USUARIO_BLOQUEO
	{0}
WHERE R.OID_REMESA = []OID_REMESA
AND R.FYH_ACTUALIZACION = []FYH_ACTUALIZACION 