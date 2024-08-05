SELECT
	   DC.IdDocumento,	 
	   DC.Fecha,
       DC.NumComprobante,
       DC.IdCentroProcesoOrigen,
       CPO.Descripcion CentroProcesoOrigen,
       DC.IdCentroProcesoDestino,
       CPD.Descripcion CentroProcesoDestino,
       IdClienteOrigen,
       CO.Descripcion ClienteOrigen,
       DC.IdClienteDestino,
       CD.Descripcion ClienteDestino,
       B.IdBanco,
       BC.Descripcion Banco,
       DC.IdEstadoComprobante,
       DC.IdUsuario,
       DC.IdFormulario,
       BD.IdBanco IdBancoDeposito,
       BDC.Descripcion BancoDeposito,
	   nvl(dc.IdUsuarioResuelve,0) as IdUsuarioResolutor,
	   DC.FechaResuelve as FechaResolucion,
	   DC.FechaGestion as FechaGestion, 
	   DC.NumExterno,
	   nvl(dc.IdGrupo,0) as IdGrupo,
       DC.Agrupado,
       DC.EsGrupo,
	   nvl(dc.IdOrigen,0) as IdOrigen,
       DC.Reenviado,
	   nvl(dc.IdUsuarioDispone,0) as IdUsuarioDispone,
	   DC.FechaDispone as FechaDispone,
	   DC.Disponible,
	   DC.Sustituido,
	   DC.EsSustituto,
	   nvl(dc.IdSustituto,0)as IdSustituto,
	   DC.Importado,
	   DC.Exportado,
	   nvl(dc.IdDocDetalles,0) as IdDocDetalles,
	   nvl(dc.IdDocBultos,0) as IdDocBultos,
	   nvl(dc.IdDocCamposExtra,0) as IdDocCamposExtra,
	   nvl(dc.IdPrimordial,0) as IdPrimordial,
	   DC.Reintentos_Conteo,
	   DC.Legado,
	   DC.Exportado_Conteo
  FROM PD_Cliente BDC
 right JOIN(PD_Banco BD
 right JOIN(PD_Cliente BC
 right JOIN(PD_Banco B
 right JOIN(PD_Cliente CD
 right JOIN(PD_Cliente CO
 right JOIN(PD_CentroProceso CPD
 right JOIN(PD_DocumentoCabecera DC
 LEFT JOIN PD_CentroProceso CPO ON DC.IdCentroProcesoOrigen =
                                CPO.IdCentroProceso) ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso) 
                                ON DC.IdClienteOrigen = CO.IdCliente) ON DC.IdClienteDestino = CD.IdCliente) 
                                ON DC.IdBanco = B.IdBanco) ON B.IdBanco = BC.IdCliente) ON DC.IdBancoDeposito = BD.IdBanco) 
                                ON BD.IdBanco = BDC.IdCliente                                
 WHERE DC.IdMovimentacionFondo = :IdMovimentacionFondo