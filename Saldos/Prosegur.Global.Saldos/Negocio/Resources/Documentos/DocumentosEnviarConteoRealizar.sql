SELECT 
  DC.IdDocumento,
  nvl(dc.IdPrimordial, dc.IdDocumento) as IdDocumentoLegado,
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
  DC.IdUsuario,
  DC.IdFormulario,
  DC.IdBancoDeposito IdBancoDeposito,
  BDC.Descripcion BancoDeposito,
  nvl(dc.IdUsuarioResuelve, 0) as IdUsuarioResolutor,
  dc.FechaResuelve as FechaResolucion,
  dc.FechaGestion as FechaGestion,
  DC.NumExterno,
  nvl(dc.IdGrupo, 0) as IdGrupo,
  DC.Agrupado,
  DC.EsGrupo,
  nvl(dc.IdOrigen, 0) as IdOrigen,
  DC.Reenviado,
  nvl(dc.IdUsuarioDispone, 0) as IdUsuarioDispone,
  dc.FechaDispone as FechaDispone,
  DC.Disponible,
  DC.Sustituido,
  DC.EsSustituto,
  nvl(dc.IdSustituto, 0) as IdSustituto,
  DC.Importado,
  DC.Exportado,
  nvl(dc.IdDocDetalles, 0) as IdDocDetalles,
  nvl(dc.IdDocBultos, 0) as IdDocBultos,
  nvl(dc.IdDocCamposExtra, 0) as IdDocCamposExtra,
  nvl(dc.IdPrimordial, 0) as IdPrimordial,
  RL.ArchivoRemesaLegado,
  DC.reintentos_conteo    
 FROM 
  PD_DocumentoCabecera DC 
  LEFT JOIN PD_REMESA_LEGADO RL ON (nvl(dc.IdPrimordial, dc.IdDocumento) = RL.IdDocumento) 
  INNER JOIN PD_Cliente CO ON (DC.IdClienteOrigen = CO.IdCliente)  
  INNER JOIN PD_Cliente CD ON (DC.IdClienteDestino = CD.IdCliente)
  INNER JOIN PD_Cliente BC ON (DC.IdBanco = BC.IdCliente) 
  INNER JOIN PD_Cliente BDC ON (DC.IdBancoDeposito = BDC.IdCliente)
  INNER JOIN PD_CentroProceso CPO ON (DC.IDCENTROPROCESOORIGEN = CPO.IDCENTROPROCESO)
  LEFT JOIN PD_CentroProceso CPD ON (DC.IDCENTROPROCESODESTINO = CPD.IDCENTROPROCESO)     
  INNER JOIN PD_Formulario F ON (F.IDFORMULARIO = DC.IDFORMULARIO)
  INNER JOIN PD_Motivo MO ON (Mo.IdMotivo = F.IdMotivo)  
 WHERE CPD.EsConteo = 1
   AND DC.EsGrupo = 0
   AND DC.Sustituido = 0
   AND DC.Reenviado = 0
   AND DC.Exportado_Conteo = 0
   AND DC.IdEstadoComprobante = 3
      AND F.EsActaProceso = 0  
   AND Mo.Exportable = 1
   AND (RL.FechaGeneracion = (SELECT MAX(RL2.FechaGeneracion) FROM Pd_Remesa_Legado RL2 WHERE RL2.IdDocumento = RL.IdDocumento) OR DC.LEGADO = 0)
   {0}
   AND DC.IdFormulario in (SELECT F.IdFormulario FROM PD_FORMULARIO F INNER JOIN PD_MOTIVO M ON (F.IdMotivo = M.IdMotivo))   
   AND DC.reintentos_conteo <= :reintentos_conteo 
ORDER BY 
	DC.Fecha 