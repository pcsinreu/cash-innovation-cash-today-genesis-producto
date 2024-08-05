SELECT COUNT(F.Idformulario) as qtd
  FROM PD_Formulario F
 INNER JOIN PD_Motivo M ON F.IdMotivo = M.IdMotivo
 INNER JOIN PD_MotivoCentroProceso MCP ON M.IdMotivo = MCP.IdMotivo
  LEFT JOIN PD_FormularioUsuario FU ON F.IdFormulario = FU.IdFormulario
 WHERE F.IdFormulario = :IdFormulario
   AND MCP.IdCentroProceso = :IdCentroProceso
   AND (FU.IdUsuario IS NULL OR FU.IdUsuario = :IdUsuario)