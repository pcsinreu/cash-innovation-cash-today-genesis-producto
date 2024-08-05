SELECT DISTINCT C.IdCampo as Id,
                C.Nombre,
                C.Clase,
                C.Coleccion,
                C.Etiqueta,
                C.Tipo,
                C.Orden
  FROM PD_FormularioCampo FC
 RIGHT JOIN PD_Campo C ON FC.IdCampo = C.IdCampo