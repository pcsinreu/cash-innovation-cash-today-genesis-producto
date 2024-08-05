SELECT
 IdAutomata as id,
 IdFormulario,
 rtrim(RutaDeTrabajo) as RutaDeTrabajo,
 IdUsuario,
 ArchivosPorTurno,
 Exportador,
 Descripcion,
 IdEstadoAExportar,
 rtrim(RutaDeCadena) as RutaDeCadena,
 FormatoExportacion,
 DiasAProcesar
FROM PD_Automata