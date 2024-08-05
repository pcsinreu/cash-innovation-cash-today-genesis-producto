SELECT C.Descripcion,
       C.IdPS,
       C.CodClienteGenesis,
       C.CodSubClienteGenesis,
       C.CodPuntoServicioGenesis
  FROM PD_Cliente C
 INNER JOIN PD_Banco B ON C.IdCliente = B.IdBanco
 WHERE C.CodClienteGenesis = :CodClienteGenesis
 AND C.CodSubClienteGenesis = :CodSubClienteGenesis