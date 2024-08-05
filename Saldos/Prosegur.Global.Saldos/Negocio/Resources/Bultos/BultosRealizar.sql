SELECT NumPrecinto, CodBolsa, IdDestino
  FROM PD_Bulto
 WHERE IdDocumento = :IdDocumento
 ORDER BY NumPrecinto
