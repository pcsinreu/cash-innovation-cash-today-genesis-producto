SELECT 
	CP.IdCentroProceso as Id,
    CP.IdPlanta,
    CP.Descripcion,
    CP.IdCentroProcesoMatriz,
    CP.SeDispone,
    CP.EsTesoro,
    CP.IdPs,
	P.descripcion as DescPlanta
FROM 
	PD_CentroProceso CP LEFT JOIN PD_Planta P ON CP.IDPLANTA = p.idplanta
WHERE 
	CP.IdPs = :IdPS