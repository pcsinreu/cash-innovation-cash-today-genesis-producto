SELECT 
	C.IdCliente as Id, 
	C.Descripcion, 
	C.IdPS, 
	C.DescCorta, 
	C.IdMatriz,
	C.CodClienteGenesis,
	C.CodSubClienteGenesis,
	C.CodPuntoServicioGenesis,
	C.IdClienteSaldo,
	C.CodNivelSaldo,
	C.SaldosPorSucursal
FROM 
	PD_Cliente C
WHERE 
	ROWNUM <= 25
	AND EsBanco = 0
	AND IdPS = :IdPS
ORDER BY C.IdPS