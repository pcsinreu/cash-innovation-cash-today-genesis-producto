SELECT

 CP.IdCentroProceso as Id,
 CP.IdPlanta,
 CP.Descripcion,
 CP.IdCentroProcesoMatriz,
 CP.SeDispone,
 CP.IdPs,
 P.descripcion as DescPlanta
 FROM PD_CentroProceso CP
 INNER JOIN PD_Planta P ON CP.IdPlanta = P.IdPlanta
  LEFT JOIN PD_CentroProceso CPM ON CP.IdCentroProcesoMatriz =
                                    CPM.IdCentroProceso
 INNER JOIN PD_TipoCentroProceso TCP ON CP.IdTipoCentroProceso =
                                        TCP.IdTipoCentroProceso
 INNER JOIN PD_UsuarioCentroProceso UCP ON CP.IdCentroProceso =
                                           UCP.IdCentroProceso
 WHERE UCP.IdUsuario = :IdUsuario
   AND (CP.IdCentroProcesoMatriz = :IdCentroProcesoMatriz or
       :IdCentroProcesoMatriz = 0)
   AND (:SoloTesoros = 0 or (:SoloTesoros = 1 AND CP.EsTesoro = 1))

 ORDER BY CP.Descripcion