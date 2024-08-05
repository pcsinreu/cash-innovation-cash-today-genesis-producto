SELECT 
	IdEspecie,
	Descripcion,
	IdMoneda,
	Uniforme,
	Orden,
	EnActaProceso,
	IdRBO,
	IdSIGII,
	Calidad,
	EsDefaultGenesis,
	IdGenesis,
	Bol_Billete,
	Bol_Moneda
FROM 
	PD_Especie
WHERE IdEspecie = :IdEspecie