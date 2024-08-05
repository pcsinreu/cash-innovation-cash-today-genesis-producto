SELECT IdPlanta as Id,
       Descripcion,
       IdPS,
       IdPS || '-' || Descripcion as IdPSDescripcion,
       CodDelegacionGenesis
  FROM PD_Planta
 order by IdPS