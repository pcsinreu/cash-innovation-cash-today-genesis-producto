UPDATE PD_Cliente
   SET Descripcion             = :Descripcion,
       IdPS                    = :IdPS,
       DescCorta               = :DescCorta,
       IdMatriz                = :IdMatriz,
       SaldosPorSucursal       = :SaldosPorSucursal,
       CodClienteGenesis       = :CodClienteGenesis,
       CodSubClienteGenesis    = :CodSubClienteGenesis,
       CodPuntoServicioGenesis = :CodPuntoServicioGenesis,
       CodNivelSaldo		   = :CodNivelSaldo,
       IdClienteSaldo		   = :IdClienteSaldo
 WHERE IdCliente = :IdCliente