SELECT DISTINCT
   DC.IdDocumento IdOrigen,
   DC.IdDocDetalles,
   DC.IdDocCamposExtra,
   DC.IdDocBultos,
   DC.IdPrimordial
FROM
   PD_DocumentoCabecera DC 
WHERE 
   DC.NumExterno = :NumExterno	AND
   (
      ( 	
      DC.IdCentroProcesoOrigen = :IdCentroProceso AND
      DC.IdEstadoComprobante = 4 /* Estado rechazado*/
      )
      OR 
      (
      DC.IdCentroProcesoDestino = :IdCentroProceso AND
      DC.IdEstadoComprobante = 3 /* Estado aceptado*/
      )
   ) AND
   DC.EsGrupo = 0 AND 
   DC.Reenviado = 0 AND 
   DC.Disponible = 0 AND 
   not (Select count(1) from PD_DocumentoCabecera where IdOrigen = DC.IdDocumento) > 0 AND 
   DC.Sustituido = 0