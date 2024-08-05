UPDATE PD_DocumentoCabecera
   SET IdCentroProcesoOrigen  = :IdCentroProcesoOrigen,
       IdCentroProcesoDestino = :IdCentroProcesoDestino
 WHERE IdGrupo = :IdGrupo