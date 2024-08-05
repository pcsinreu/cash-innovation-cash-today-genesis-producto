SELECT DISTINCT SUM(DD.Importe) as Importe,               
                M.IdMoneda as IdMoneda,
                M.Simbolo as SimboloMoneda
  FROM PD_DocumentoDetalle DD
 INNER JOIN PD_Especie E ON DD.IdEspecie = E.IdEspecie
 INNER JOIN PD_Moneda M ON E.IdMoneda = M.IdMoneda
 WHERE DD.IdDocumento = :IdDocumento
 GROUP BY M.IdMoneda, M.Simbolo
 ORDER BY M.IdMoneda
