SELECT ACP.IdCentroProceso as IdCentroProceso,
       CP.Descripcion      as Descripcion
  FROM PD_AutomataCentroProceso ACP
 INNER JOIN PD_CentroProceso CP ON ACP.IdCentroProceso = CP.IdCentroProceso
 WHERE IdAutomata = :IdAutomata