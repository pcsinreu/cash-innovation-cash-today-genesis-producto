SELECT distinct
	  DC.IdDocumento,
	  NVL(DC.IdDocDetalles, DC.IdDocumento) IdDocDetalles,
	  E.IdEspecie,
	  E.Descripcion,
	  E.Orden,
	  CO.Idps,
	  CPO.IDPS,
	  E.IdGenesis,
	  E.Bol_Billete,
	  E.Bol_Moneda,
	  nvl(DD.Importe, 0) Importe,
	  nvl(DD.Cantidad ,0) Cantidad,
	  MN.IdMoneda,
	  MN.Idps MonedaIdps,
	  MN.Descripcion Moneda,
	  MN.Isogenesis,
	  MN.Isosaldos
  FROM PD_DocumentoCabecera DC
  INNER JOIN PD_DocumentoDetalle DD on DD.IdDocumento = NVL(DC.IdDocDetalles, DC.IdDocumento)
  INNER JOIN PD_Especie E on E.IdEspecie = DD.IdEspecie
  LEFT JOIN PD_Moneda MN on E.Idmoneda = MN.Idmoneda
  INNER JOIN PD_Cliente CO ON (DC.IdClienteOrigen = CO.IdCliente)
  INNER JOIN PD_Cliente CD ON (DC.IdClienteDestino = CD.IdCliente)
  INNER JOIN PD_Cliente BC ON (DC.IdBanco = BC.IdCliente) 
  INNER JOIN PD_Cliente BDC ON (DC.IdBancoDeposito = BDC.IdCliente)
  INNER JOIN PD_CentroProceso CPO ON (DC.IdCentroProcesoOrigen = CPO.IdCentroProceso)
  LEFT JOIN PD_CentroProceso CPD ON (DC.IdCentroProcesoDestino = CPD.IdCentroProceso)
 WHERE (DC.FECHAGESTION >= :FECHAHORADESDE AND DC.FECHAGESTION <=:FECHAHORAHASTA) 
 AND (CPO.IdPlanta = :IDPLANTA OR :IDPLANTA IS NULL)
 AND ((DC.IdCentroProcesoOrigen = :IDCENTROPROCESO OR DC.IdCentroProcesoDestino = :IDCENTROPROCESO) OR :IDCENTROPROCESO IS NULL)
 AND ((DC.IdBanco = :IDCANAL OR DC.IdBancoDeposito = :IDCANAL) OR :IDCANAL IS NULL)
 AND ((DC.IdClienteOrigen = :IDCLIENTE OR DC.IdClienteDestino = :IDCLIENTE) OR :IDCLIENTE IS NULL)
 AND (E.IdMoneda = :IDMONEDA OR :IDMONEDA IS NULL)
 