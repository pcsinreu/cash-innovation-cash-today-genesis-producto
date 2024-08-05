SELECT IdMoneda
  FROM PD_MONEDA
 WHERE IsoGenesis = :IsoGenesis
   AND (IsoSaldos = :IsoSaldos OR IdGenesis = :IdGenesis)