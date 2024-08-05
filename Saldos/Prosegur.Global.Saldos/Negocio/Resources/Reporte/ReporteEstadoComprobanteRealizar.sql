SELECT E.IdEstadoComprobante, E.Descripcion, E.Codigo, E.ORDEN
  FROM PD_ReporteEstadoComprobante RE
 INNER JOIN PD_EstadoComprobante E ON RE.IdEstadoComprobante = E.IdEstadoComprobante
 WHERE IdReporte = :IdReporte
   AND VistaDestinatario = :VistaDestinatario
 ORDER BY E.ORDEN