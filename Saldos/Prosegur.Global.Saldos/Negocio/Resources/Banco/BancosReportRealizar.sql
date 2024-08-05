SELECT PDC.IdCliente,
       PDC.IdPS,
       PDC.Descripcion,
       PDC.CodClienteGenesis,
       PDC.CodSubClienteGenesis,
       PDC.CodPuntoServicioGenesis
  FROM PD_Cliente PDC
 INNER JOIN PD_Banco PDB ON PDC.IdCliente = PDB.IdBanco
 ORDER BY PDC.IdPS