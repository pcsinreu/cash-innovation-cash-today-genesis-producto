SELECT B.IdBanco AS Id, 
	C.Descripcion, 
	C.IdPS, 
	C.codclientegenesis, 
	C.codsubclientegenesis, 
	C.codpuntoserviciogenesis
  FROM PD_Cliente C
 INNER JOIN PD_Banco B
 INNER JOIN PD_ClienteBanco CB ON B.IdBanco = CB.IdBanco ON
 C.IdCliente = B.IdBanco
WHERE CB.IdCliente = :IdCliente