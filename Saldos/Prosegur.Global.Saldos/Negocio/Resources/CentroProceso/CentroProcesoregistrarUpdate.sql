UPDATE PD_CentroProceso
   SET IdPlanta              = :IdPlanta,
       Descripcion           = :Descripcion,
       IdCentroProcesoMatriz = :IdCentroProcesoMatriz,
       IdTipoCentroProceso   = :IdTipoCentroProceso,
       SeDispone             = :SeDispone,
       EsTesoro              = :EsTesoro,
       IdPs                  = :IdPs,
	   EsConteo              = :EsConteo{0}
 WHERE IdCentroProceso = :IdCentroProceso