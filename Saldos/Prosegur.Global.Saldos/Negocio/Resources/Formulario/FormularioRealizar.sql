SELECT PF.Descripcion,
       PF.IdMotivo,
       PF.SoloEnGrupo,
       PF.ConValores,
       PF.ConBultos,
       PF.ConLector,
       PF.BasadoEnReporte,
       PF.IdReporte,
       PF.BasadoEnSaldos,
       PF.SeImprime,
       PF.Interplantas,
       PF.DistinguirPorNivel,
       PF.Matrices,
       PF.SoloIndividual,
       PF.EsActaProceso,
       PF.SoloSaldoDisponible,
       PF.Color,
       PF.Sustituible,
       PF.BasadoEnExtracto,
       PF.TotalCero,
       PF.BOL_VALIDAR_NUM_EXT_EXISTENTE,
	   PRC.Reporte
  FROM PD_Formulario PF
  LEFT JOIN PD_FormularioReporteCondicion PRC on 
  PRC.IdFormulario = PF.IdFormulario And PRC.Reporte = :Reporte
 WHERE PF.IdFormulario = :IdFormulario