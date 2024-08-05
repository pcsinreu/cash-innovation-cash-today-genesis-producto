SELECT DC.IdDocumento,
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
       B.IdBanco,
       BC.Descripcion Banco,
       DC.IdEstadoComprobante,
       DC.IdUsuario,
       DC.IdFormulario,
       BD.IdBanco IdBancoDeposito,
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
 FROM PD_Cliente BDC
 RIGHT JOIN(PD_Banco BD
 RIGHT JOIN(PD_Cliente BC
 RIGHT JOIN(PD_Banco B
 RIGHT JOIN(PD_Cliente CD
 RIGHT JOIN(PD_Cliente CO
 RIGHT JOIN(PD_CentroProceso CPD
 RIGHT JOIN(PD_CentroProceso CPO
 INNER JOIN(PD_Remesa_Legado RL
 RIGHT JOIN(PD_DocumentoCabecera DC 
 INNER JOIN (PD_Motivo Mo
 INNER JOIN PD_Formulario F 
        ON Mo.IdMotivo = F.IdMotivo)
        ON DC.IdFormulario = F.IdFormulario) 
        ON nvl(dc.IdPrimordial, dc.IdDocumento) = RL.IdDocumento) 
        ON DC.IdCentroProcesoOrigen = CPO.IdCentroProceso) 
        ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso) 
        ON DC.IdClienteOrigen = CO.IdCliente) 
        ON DC.IdClienteDestino = CD.IdCliente) 
        ON DC.IdBanco = B.IdBanco) 
        ON B.IdBanco = BC.IdCliente) 
        ON DC.IdBancoDeposito = BD.IdBanco) 
        ON BD.IdBanco = BDC.IdCliente
 WHERE CPD.EsConteo = 1
   AND DC.EsGrupo = 0
   AND DC.Sustituido = 0
   AND DC.Reenviado = 0
   AND DC.Exportado_Conteo = 0
   AND DC.IdEstadoComprobante = 3
   AND F.EsActaProceso = 0  
   AND Mo.Exportable = 1
   AND (RL.FechaGeneracion = (SELECT MAX(RL2.FechaGeneracion) FROM Pd_Remesa_Legado RL2 WHERE RL2.IdDocumento = RL.IdDocumento) OR DC.LEGADO = 0)
   AND DC.IdDocumento In ({0})
 ORDER BY NumExterno