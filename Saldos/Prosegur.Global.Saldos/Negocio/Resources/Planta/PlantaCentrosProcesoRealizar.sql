SELECT 
	IdCentroProceso as Id,
	Descripcion
FROM 
	PD_CentroProceso
WHERE 
	IdPlanta = :IdPlanta 