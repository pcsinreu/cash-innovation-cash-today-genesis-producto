SELECT 
  FechaHasta
, IdExtracto
, IdDocumento

  FROM PD_Extracto

 WHERE IdPlanta = :IdPlanta
   AND IdMoneda = :IdMoneda
   AND IdCliente = :IdCliente
   AND IdBanco = :IdBanco
   AND FechaHasta = (select max(FechaHasta)
                       from PD_Extracto
                      WHERE IdPlanta = :IdPlanta
                        AND IdMoneda = :IdMoneda
                        AND IdCliente = :IdCliente
                        AND IdBanco = :IdBanco)