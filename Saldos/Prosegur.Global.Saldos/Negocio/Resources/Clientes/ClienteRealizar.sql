SELECT Descripcion,
       IdPS,
       DescCorta,
       IdMatriz,
       SaldosPorSucursal,
       CodClienteGenesis,
       CodSubClienteGenesis,
       CodPuntoServicioGenesis,
       CodNivelSaldo
  FROM PD_Cliente
 WHERE IdCliente = :IdCliente