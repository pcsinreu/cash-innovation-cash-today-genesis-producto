SELECT IdFormulario,
       rtrim(RutaDeTrabajo) as RutaDeTrabajo,
       IdUsuario,
       ArchivosPorTurno,
       Exportador,
       Descripcion,
       IdEstadoAExportar,
       rtrim(RutaDeCadena) as RutaDeCadena,
       FormatoExportacion,
       DiasAProcesar,
       Grupo,
       Orden
  FROM PD_Automata
 WHERE IdAutomata = :IdAutomata