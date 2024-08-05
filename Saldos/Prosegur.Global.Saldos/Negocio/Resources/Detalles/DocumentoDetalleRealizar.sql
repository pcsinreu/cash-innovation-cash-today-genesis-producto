SELECT DISTINCT Importe,
                Cantidad,
                M.IdMoneda as IdMoneda,
                M.Descripcion as DescMoneda,
                M.Simbolo as SimboloMoneda,
                E.IdEspecie as IdEspecie,
                E.Descripcion as DescEspecie,
                E.Uniforme,
                M.IdMoneda,
                E.Orden,
                E.Calidad
  FROM PD_DocumentoDetalle DD
 INNER JOIN PD_Especie E ON DD.IdEspecie = E.IdEspecie
 INNER JOIN PD_Moneda M ON E.IdMoneda = M.IdMoneda
 WHERE IdDocumento = :IdDocumento
 AND (M.IdMoneda = :IdMoneda OR :IdMoneda IS NULL)
 ORDER BY M.IdMoneda, E.Orden