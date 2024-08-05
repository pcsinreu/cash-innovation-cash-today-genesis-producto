SELECT CP.IdPlanta as IdPlanta,
       CP.Descripcion as Descripcion,
       nvl(CP.IdCentroProcesoMatriz, 0) as IdCentroProcesoMatriz,
       CP.IdTipoCentroProceso as IdTipoCentroProceso,
       P.Descripcion as DescPlanta,
       nvl(CPM.Descripcion, '') as DescCentroProcesoMatriz,
       TCP.Descripcion as DescTipoCentroProceso,
       CP.SeDispone as SeDispone,
       CP.EsTesoro as EsTesoro,
       CP.EsConteo as EsConteo,
	   {0}
       CP.IdPs as IdPs
  FROM PD_CentroProceso CP
 INNER JOIN PD_Planta P ON CP.IdPlanta = P.IdPlanta
 INNER JOIN PD_TipoCentroProceso TCP ON CP.IdTipoCentroProceso = TCP.IdTipoCentroProceso
  LEFT JOIN PD_CentroProceso CPM ON CP.IdCentroProcesoMatriz = CPM.IdCentroProceso
 WHERE CP.IdCentroProceso = :IdCentroProceso