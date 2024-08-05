SELECT Descripcion,
       DistinguirPorVistaDestinatario,
       DistinguirPorFecha,
       FechaDesdeHoyDiasD,
       FechaHastaHoyDiasD,
       DistinguirPorDisponibilidad,
       Disponible,
       DistinguirPorReenvio,
       Reenviado,
       VistaDestinatario,
       DistinguirPorBultos,
       ConBultos,
       DistinguirPorValores,
       ConValores,
       ConTomados,
       DistinguirPorSustitucion,
       Sustituido
  FROM PD_Reporte
 WHERE IdReporte = :IdReporte