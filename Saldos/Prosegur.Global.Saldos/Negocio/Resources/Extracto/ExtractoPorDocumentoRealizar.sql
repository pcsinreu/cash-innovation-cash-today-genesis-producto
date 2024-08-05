SELECT 
	IdExtracto,
	FechaDesde,
	FechaHasta,
	IdPlanta,
	IdMoneda,
	IdCliente,
	IdBanco,
	IdAnterior,
	VistaCliente
FROM 
	PD_Extracto
WHERE 
	IdDocumento = :IdDocumento