UPDATE PD_DocumentoCabecera
   SET IdEstadoComprobante    = :IdEstadoComprobanteRechazado,
       IdUsuarioResuelve      = :IdUsuarioResuelve,
       FechaResuelve          = sysdate,
       IdCentroProcesoDestino = :IdCentroProcesoDestino
       {0}
 WHERE IdDocumento = :IdDocumento