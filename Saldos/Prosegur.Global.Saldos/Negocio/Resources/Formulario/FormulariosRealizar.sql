SELECT 
	DISTINCT 
	F.IdFormulario as Id, 
	F.Descripcion, 
	F.SoloEnGrupo, 
	F.ConValores, 
	F.ConBultos, 
	F.ConLector, 
	F.BasadoEnReporte, 
	nvl(F.IdReporte, 0) as IdReporte, 
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
	INNER JOIN PD_MotivoCentroProceso MCP on M.IdMotivo = MCP.IdMotivo 
	LEFT JOIN PD_FormularioUsuario FU ON F.IdFormulario = FU.IdFormulario 
WHERE 
	(MCP.IdCentroProceso = :IdCentroProceso OR :IdCentroProceso = 0) 
	AND (FU.IdUsuario IS NULL OR FU.IdUsuario = :IdUsuario OR :IdUsuario = 0) 
ORDER BY 
	F.Descripcion