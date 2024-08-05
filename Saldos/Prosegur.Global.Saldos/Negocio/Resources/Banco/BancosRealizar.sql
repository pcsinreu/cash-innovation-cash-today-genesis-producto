SELECT C.IdCliente as Id,
       C.Descripcion,
       C.IdPS,
       C.CodClienteGenesis,
       C.CodSubClienteGenesis,
       C.CodPuntoServicioGenesis
  FROM PD_Banco B
 INNER JOIN PD_Cliente C ON B.IdBanco = C.IdCliente