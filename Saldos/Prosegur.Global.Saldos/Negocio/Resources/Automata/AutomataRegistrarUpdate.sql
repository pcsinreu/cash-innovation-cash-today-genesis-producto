UPDATE PD_Automata
   SET IdFormulario       = :IdFormulario,
       RutaDeTrabajo      = :RutaDeTrabajo,
       IdUsuario          = :IdUsuario,
       ArchivosPorTurno   = :ArchivosPorTurno,
       Exportador         = :Exportador,
       Descripcion        = :Descripcion,
       IdEstadoAExportar  = :IdEstadoAExportar,
       RutaDeCadena       = :RutaDeCadena,
       FormatoExportacion = :FormatoExportacion,
       DiasAProcesar      = :DiasAProcesar,
       Grupo              = :Grupo,
       Orden              = :Orden
 WHERE IdAutomata = :IdAutomata