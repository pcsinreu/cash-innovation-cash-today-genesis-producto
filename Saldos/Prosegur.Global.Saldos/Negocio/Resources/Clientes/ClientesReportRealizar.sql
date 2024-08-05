select Distinct CL.IdCliente, CL.IDPS || '-' || P.Descripcion || '-' || CL.Descripcion as Descripcion
From PD_Planta P
INNER JOIN PD_ClientePlanta CLPL ON CLPL.IdPlanta = P.IdPlanta
INNER JOIN PD_Cliente CL ON CLPL.IDCLIENTE = CL.IDCLIENTE
Where p.idplanta in	
(
	select idplanta 
	from pd_centroproceso where idcentroproceso in ({0})
)
order by 2
 