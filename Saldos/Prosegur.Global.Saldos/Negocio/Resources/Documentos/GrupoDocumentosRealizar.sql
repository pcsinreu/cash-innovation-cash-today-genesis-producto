SELECT 
	DC.IdDocumento,
	DC.Fecha,
	DC.NumComprobante,
	DC.IdCentroProcesoOrigen,
	CPO.Descripcion as CentroProcesoOrigenDes,
	DC.IdCentroProcesoDestino,
	CPD.Descripcion as CentroProcesoDestinoDes,
	DC.IdClienteOrigen,
	CO.IdPS || '-' || CO.Descripcion as ClienteOrigenDes,
	DC.IdClienteDestino,
	CD.IdPS || '-' || CD.Descripcion as ClienteDestinoDes,
	DC.IdBanco,
	BC.IdPS || '-' || BC.Descripcion as BancoDes,
	DC.IdBancoDeposito,
	BDC.IdPS || '-' || BDC.Descripcion as BancoDepositoDes,
	DC.IdEstadoComprobante,
	DC.IdUsuario,
	DC.IdFormulario,
	DC.IdBancoDeposito,
	DC.IdUsuarioResuelve,
	DC.FechaResuelve,
	DC.FechaGestion,
	DC.NumExterno,
	nvl(DC.Agrupado, 0) as Agrupado,
	nvl(DC.EsGrupo, 0) as EsGrupo,
	nvl(DC.IdGrupo, 0) as IdGrupo,
	nvl(DC.IdOrigen, 0) as IdOrigen,
	nvl(DC.Reenviado, 0) as Reenviado,
	nvl(DC.Disponible, 1) as Disponible,
	DC.IdUsuarioDispone,
	DC.FechaDispone,
	nvl(DC.Sustituido, 0) as Sustituido,
	nvl(DC.IdDocDetalles, 0) as IdDocDetalles,
	DC.reintentos_conteo
FROM 
	PD_Cliente BDC
	RIGHT JOIN(PD_Banco BD
	RIGHT JOIN(PD_Cliente BC
	RIGHT JOIN(PD_Banco B
	RIGHT JOIN(PD_Cliente CD
	RIGHT JOIN(PD_Cliente CO
	RIGHT JOIN(PD_CentroProceso CPD
	RIGHT JOIN(PD_CentroProceso CPO
	INNER JOIN(PD_DocumentoCabecera DC
	INNER JOIN PD_Formulario F 
	
	ON DC.IdFormulario = F.IdFormulario) 
	ON DC.IdCentroProcesoOrigen = CPO.IdCentroProceso) 
	ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso) 
	ON DC.IdClienteOrigen = CO.IdCliente) 
	ON DC.IdClienteDestino = CD.IdCliente) 
	ON DC.IdBanco = B.IdBanco) 
	ON B.IdBanco = BC.IdCliente) 
	ON DC.IdBancoDeposito = BD.IdBanco) 
	ON BD.IdBanco = BDC.IdCliente
	
WHERE 
	DC.IdGrupo = :IdGrupo
ORDER BY NumExterno