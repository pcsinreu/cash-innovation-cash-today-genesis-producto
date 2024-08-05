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
	PD_Cliente C INNER JOIN PD_ClientePlanta CP ON C.IdCliente = CP.IdCliente AND CP.IdPlanta = :IdPlanta
WHERE
	EsBanco = 0
	AND(
		(
			length(:IdPS) IS NOT NULL 
			and (
				(
					IdPS = :IdPS and C.SaldosPorSucursal = 0
				)
				or (	
					IdPS LIKE :IdPS || '-%'	
				)
				or (	
       instr(:IdPS, '*') = length(:IdPS) and  IdPS || '*' = :IdPS and C.SaldosPorSucursal = 1
				) 
			)
		)

		OR ( 
		 	length(:IdPS) IS NULL
			and length(:Descripcion) IS NOT NULL
     			and ( 
				(
					Descripcion like '%' || :Descripcion || '%' 
					and ( 
						C.IdMatriz is not null 
						or(					 
							C.IdMatriz is null 
							and C.SaldosPorSucursal = 0
						)
					)
				)
			)
		)
		OR (
			length(:IdPS) IS NULL AND length(:Descripcion) IS NULL
		)		
	)
ORDER BY C.IdPS