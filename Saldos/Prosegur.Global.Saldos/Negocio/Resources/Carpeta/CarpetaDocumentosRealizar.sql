SELECT 
    DISTINCT 
    DC.IdDocumento,
    DC.Fecha,
    DC.NumComprobante,
    DC.IdCentroProcesoOrigen,
    CPO.Descripcion as CentroProcesoOrigenDesc,
    DC.IdCentroProcesoDestino,
    CPD.Descripcion as CentroProcesoDestinoDesc,    
    0 as IdClienteOrigen,    
    ' ' as ClienteOrigenDesc,    
    0 as IdClienteDestino,    
    ' ' as ClienteDestinoDesc,    
    0 as Idbanco,    
    ' ' as BancoDesc,    
    0 as IdBancoDeposito,    
    ' ' as BancoDepositoDesc,
    DC.IdEstadoComprobante,
    DC.IdUsuario,
    DC.IdFormulario,
    DC.IdBancoDeposito as BD,
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
    nvl(DC.Sustituido, 0) as Sustituido
FROM (PD_Bulto BU
right JOIN(PD_CentroProceso CPD
RIGHT JOIN(PD_CentroProceso CPO
INNER JOIN PD_DocumentoCabecera DC
                                  ON DC.IdCentroProcesoOrigen = CPO.IdCentroProceso)
                                  ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso)
                                  ON BU.IdDocumento = DC.IdDocumento)
WHERE
    (DC.NumComprobante >= :NumComprobanteDesde or length(:NumComprobanteDesde) IS NULL)
    and (DC.NumComprobante <= :NumComprobanteHasta or length(:NumComprobanteHasta) IS NULL)           
    and (length(:TipoCodigo) IS NULL 
                             or (:TipoCodigo = 'NC' and INSTR(:ListaCodigos, '|' || trim(DC.NumComprobante) || '|') > 0) 
                             or (:TipoCodigo = 'NE' and INSTR(:ListaCodigos, '|' || trim(DC.NumExterno) || '|') > 0)
                             or (:TipoCodigo = 'NC' and 0 < (SELECT COUNT(*) 
                                                             FROM PD_DOCUMENTOCABECERA SQDC 
                                                             WHERE SQDC.IdGrupo = DC.IdDocumento
                                                             and INSTR(:ListaCodigos, '|' || trim(SQDC.NumComprobante) || '|') > 0)) 
                             or (:TipoCodigo = 'NE' and 0 < (SELECT count(*) 
                                                             FROM PD_DOCUMENTOCABECERA SQDC
                                                             WHERE SQDC.IdGrupo = DC.IdDocumento
                                                             and INSTR(:ListaCodigos, '|' || trim(SQDC.NumExterno) || '|') > 0))
                            or (:TipoCodigo = 'NP' and instr(:ListaCodigos, '|' || rtrim(BU.NumPrecinto) || '|') > 0)
                            or (:TipoCodigo = 'NP' and 0 < (select count(*) from pd_Bulto bu where bu.iddocumento in
                                                            (select idprimordial from pd_documentocabecera where idgrupo = dc.iddocumento)
                                                            and INSTR(:ListaCodigos, '|' || trim(BU.NumPrecinto) || '|') > 0))
         )
    and (Fecha <= :FechaHasta or :FechaHasta is null or Fecha is null)
	and (Fecha >= :FechaDesde or :FechaDesde is null or Fecha is null)
	and ( (:VistaDestinatario = 0 and IdCentroProcesoOrigen = :IdCentroProceso) or 
	      (:VistaDestinatario = 1 and IdCentroProcesoDestino = :IdCentroProceso) )		
	and	IdEstadoComprobante = :IdEstadoComprobante
	and DC.IdGrupo is Null