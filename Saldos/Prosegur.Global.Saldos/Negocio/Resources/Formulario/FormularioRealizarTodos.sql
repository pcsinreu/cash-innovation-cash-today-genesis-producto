SELECT 
	DISTINCT 
	F.IdFormulario AS Id, 
	F.Descripcion, 
	F.SoloEnGrupo, 
	F.ConValores, 
	F.ConBultos, 
	F.ConLector, 
	F.BasadoEnReporte, 
	nvl(F.IdReporte, 0) AS IdReporte, 
	F.BasadoEnSaldos, 
	F.SoloSaldoDisponible, 
	F.SeImprime, 
	F.Interplantas, 
	F.DistinguirPorNivel, 
	F.Matrices, 
	F.SoloIndividual, 
	F.EsActaProceso, 
	F.Color, 
	F.BasadoEnExtracto, 
	F.TotalCero, 
	F.BOL_VALIDAR_NUM_EXT_EXISTENTE 
FROM 
	PD_Formulario F 
	INNER JOIN PD_Motivo M ON F.IdMotivo = M.IdMotivo 
	INNER JOIN PD_MotivoCentroProceso MCP ON M.IdMotivo = MCP.IdMotivo 
ORDER BY 
	F.Descripcion