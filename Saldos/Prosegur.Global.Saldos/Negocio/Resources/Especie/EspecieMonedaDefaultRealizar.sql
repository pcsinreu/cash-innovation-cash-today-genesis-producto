SELECT IdEspecie
  FROM PD_ESPECIE
 WHERE IdMoneda = :IdMoneda
 AND Uniforme = 0
 AND EsDefaultGenesis = 1