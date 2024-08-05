SELECT E.IdEspecie as Id,
       E.Descripcion,
       nvl(E.Calidad, '') as Calidad,
       M.IdMoneda as IdMoneda,
       M.Descripcion as DescMoneda,
       M.Simbolo as SimboloMoneda,
       E.Uniforme as Uniforme,
       E.Orden as Orden,
       E.EnActaProceso as EnActaProceso,
       E.IdRBO as IdRBO,
       E.IdSIGII as IdSIGII
  FROM PD_Especie E
 INNER JOIN PD_Moneda M ON E.IdMoneda = M.IdMoneda
  ORDER BY M.IdMoneda, E.Orden