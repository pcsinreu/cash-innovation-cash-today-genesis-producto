SELECT NumSobre, ConDiferencia, IdMoneda, Importe
  FROM PD_Sobre
 where IdDocumento = :IdDocumento
 order by NumSobre