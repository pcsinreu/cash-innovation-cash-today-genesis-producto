
SELECT
  
	 S.OID_SECTOR AS IDENTIFICADOR
	,S.OID_PLANTA AS IDENTIFICADOR_PAI
	,S.COD_SECTOR AS CODIGO
	,S.DES_SECTOR AS DESCRICAO
	,S.BOL_ACTIVO AS VIGENTE
	,S.OID_SECTOR_PADRE
	,S.OID_TIPO_SECTOR	
	,S.COD_MIGRACION
	,S.BOL_CENTRO_PROCESO
	,S.BOL_PERMITE_DISPONER_VALOR
	,S.BOL_TESORO
	,S.BOL_CONTEO	
	,S.GMT_CREACION
	,S.DES_USUARIO_CREACION
	,S.GMT_MODIFICACION
	,S.DES_USUARIO_MODIFICACION

FROM
	GEPR_TSECTOR S