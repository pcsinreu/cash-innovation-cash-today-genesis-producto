SELECT Descripcion, IdPS
  FROM PD_TipoCentroProceso
 WHERE IdTipoCentroProceso = :IdTipoCentroProceso
 order by descripcion