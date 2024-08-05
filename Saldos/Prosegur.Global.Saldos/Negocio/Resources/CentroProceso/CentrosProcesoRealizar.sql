 SELECT CP.IdCentroProceso as Id,
        CP.IdPlanta,
        CP.Descripcion,
        CP.IdCentroProcesoMatriz,
        CP.SeDispone,
        CP.EsTesoro,
        CP.IdPs,
		P.descripcion as DescPlanta        
 FROM PD_CentroProceso CP

 INNER JOIN PD_Planta P ON CP.IdPlanta = P.IdPlanta
 INNER JOIN PD_TipoCentroProceso TCP ON CP.IdTipoCentroProceso = TCP.IdTipoCentroProceso
 LEFT JOIN PD_CentroProceso CPM ON CP.IdCentroProcesoMatriz = CPM.IdCentroProceso
 
 WHERE ((:SoloTesoros = 0 AND CP.IdCentroProceso <> :IdCentroProcesoActual) OR
       (:SoloTesoros = 1))
   AND (:Interplantas = 1 or
       (:Interplantas = 0 AND CP.IdPlanta = (SELECT IdPlanta FROM PD_CentroProceso WHERE IdCentroProceso = :IdCentroProcesoActual)))
   AND (:DistinguirPorNivel = 0 OR
       (:DistinguirPorNivel = 1 AND
       (:Matrices = 1 AND CP.IdCentroProcesoMatriz is null) OR
       (:Matrices = 0 AND not CP.IdCentroProcesoMatriz is null)))
   AND (CP.IdCentroProcesoMatriz = :IdCentroProcesoMatriz or
       :IdCentroProcesoMatriz = 0)
   AND (CP.IdPlanta = :IdPlanta or :IdPlanta = 0)
   AND (:SoloTesoros = 0 or (:SoloTesoros = 1 AND CP.EsTesoro = 1))
   AND (instr(:idTiposCentroProceso, '|' || TCP.IdTipoCentroProceso || '|') > 0 OR :idTiposCentroProceso = '0')
   
 ORDER BY CP.Descripcion, TCP.IdPS