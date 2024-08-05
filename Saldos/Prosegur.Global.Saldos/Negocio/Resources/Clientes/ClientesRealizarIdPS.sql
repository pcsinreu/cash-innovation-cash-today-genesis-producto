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
	AND 
	(
		(	
			Length(:IdPS) IS NOT NULL 
			AND 
			(
				(
					IdPS = :IdPS and C.SaldosPorSucursal = 0
				)
				OR 
				(	
					IdPS LIKE :IdPS || '-%'	
				)
				OR 
				(	
					instr(:IdPS, '*') = length(:IdPS) and  IdPS || '*' = :IdPS and C.SaldosPorSucursal = 1
				) 
			)
		)	
		OR 
		( 
		 	Length(:IdPS) IS NULL 
			AND Length(:Descripcion) IS NOT NULL
			AND 
			( 
				(
					Descripcion like '%' || :Descripcion || '%' 
					AND 
					( 
						not C.IdMatriz is null 
						or
						(
							C.IdMatriz is null 
							and C.SaldosPorSucursal = 0
						)
					)
				)
				or 
				(
					Descripcion  like '%' || substr(:Descripcion, 0, length(:Descripcion) - 1 ) || '%' 
					and instr(:Descripcion, '*' ) = length(:Descripcion)
					and C.IdMatriz is null 
					and C.SaldosPorSucursal = 1
				)
			)
		)
	)
	ORDER BY C.IdPS