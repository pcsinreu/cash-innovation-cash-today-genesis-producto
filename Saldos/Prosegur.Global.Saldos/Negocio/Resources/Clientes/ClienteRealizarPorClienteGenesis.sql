SELECT /*+ index (IX_CLIENTEGENESIS) */
	   IdCliente,
       Descripcion,
       IdPS,
       DescCorta,
       IdMatriz,
       SaldosPorSucursal,
       CodClienteGenesis,
       CodSubClienteGenesis,
       CodPuntoServicioGenesis
  FROM PD_Cliente