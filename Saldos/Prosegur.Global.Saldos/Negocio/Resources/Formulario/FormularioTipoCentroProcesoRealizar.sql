SELECT TCP.IdTipoCentroProceso as Id, TCP.IdPS, TCP.Descripcion
  FROM PD_FormularioTipoCentroProceso FTCP
 INNER JOIN PD_TipoCentroProceso TCP ON FTCP.IdTipoCentroProceso = TCP.IdTipoCentroProceso
 WHERE FTCP.IdFormulario = :IdFormulario