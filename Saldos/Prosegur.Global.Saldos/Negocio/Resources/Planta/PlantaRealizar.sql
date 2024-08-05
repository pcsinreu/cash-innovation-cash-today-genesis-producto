SELECT IdPlanta,
	   Descripcion,
       IdPS,
       IdPS || '-' || Descripcion as IdPSDescripcion,
       CodDelegacionGenesis
  FROM PD_Planta
 WHERE IdPlanta = :IdPlanta