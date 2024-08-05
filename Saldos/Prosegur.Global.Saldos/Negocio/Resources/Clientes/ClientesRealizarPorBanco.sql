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
	PD_Cliente C INNER JOIN  PD_ClienteBanco CB ON C.IdCliente = CB.IdCliente
WHERE 
	CB.IdBanco = :IdBanco 
	and( 
		(IdPS = :IdPS or length(:idPS) IS NULL
	 		and ( (Descripcion like '%' || :Descripcion || '%' and length(:IdPS) IS NULL) or length(:Descripcion) IS NULL or length(:IdPS) IS NOT NULL)
	)
	and C.EsBanco = 0
ORDER BY C.IdPS