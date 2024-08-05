SELECT
	DC.IdDocumento,  
	DC.Fecha,
	DC.NumComprobante,
	DC.IdCentroProcesoOrigen,
	CPO.Descripcion CentroProcesoOrigen,
	DC.IdCentroProcesoDestino,
	CPD.Descripcion CentroProcesoDestino,
	DC.IdClienteOrigen,
	CO.Descripcion ClienteOrigen,
	DC.IdClienteDestino,
	CD.Descripcion ClienteDestino,
	DC.IdBanco,
	BC.Descripcion Banco,
	DC.IdEstadoComprobante,
	EC.Descripcion DescripcionEstadoComprobante,
	DC.IdUsuario,
	DC.IdFormulario,
	DC.IdBancoDeposito IdBancoDeposito,
	BDC.Descripcion BancoDeposito,
	nvl(DC.IdUsuarioResuelve, 0) as IdUsuarioResolutor,
	dc.FechaResuelve as FechaResolucion,
	dc.FechaGestion as FechaGestion,
	DC.NumExterno,
	nvl(dc.IdGrupo, 0) as IdGrupo,
	DC.Agrupado,
	DC.EsGrupo,
	nvl(DC.IdOrigen, 0) as IdOrigen,
	DC.Reenviado,
	nvl(DC.IdUsuarioDispone, 0) as IdUsuarioDispone,
	DC.FechaDispone as FechaDispone,
	DC.Disponible,
	DC.Sustituido,
	DC.EsSustituto,
	nvl(DC.IdSustituto, 0) as IdSustituto,
	DC.Importado,
	DC.Exportado,
	nvl(DC.IdDocDetalles, DC.IdDocumento) as IdDocDetalles,
	nvl(DC.IdDocBultos, DC.IdDocumento) as IdDocBultos,
	nvl(DC.IdDocCamposExtra, DC.IdDocumento) as IdDocCamposExtra,
	nvl(DC.IdPrimordial, DC.IdDocumento) as IdPrimordial,
	DC.Reintentos_Conteo,
	DC.Legado,
	DC.Exportado_Conteo,
	DC.IdMovimentacionFondo,
	Saldo_Disponible_###VERSION###(M.ACCION, EC.CODIGO) SaldoDisponible
  FROM PD_DocumentoCabecera DC
  INNER JOIN PD_DocumentoDetalle DD on DD.IdDocumento = NVL(DC.IdDocDetalles, DC.IdDocumento)
  INNER JOIN PD_Especie E on E.IdEspecie = DD.IdEspecie
  INNER JOIN PD_Formulario F ON DC.IdFormulario = F.IdFormulario
  INNER JOIN PD_EstadoComprobante EC ON DC.IdEstadoComprobante = EC.IdEstadoComprobante
  INNER JOIN PD_Motivo M ON M.IdMotivo = F.IdMotivo
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