SELECT IdCentroProceso, IdInventario, FechaInventario
  FROM InventarioCabecera
 WHERE IdCentroproceso = :IdCentroProceso
   AND FechaInventario > :FechaIni
   AND FechaInventario < :FechaFin
 ORDER BY FechaInventario DESC