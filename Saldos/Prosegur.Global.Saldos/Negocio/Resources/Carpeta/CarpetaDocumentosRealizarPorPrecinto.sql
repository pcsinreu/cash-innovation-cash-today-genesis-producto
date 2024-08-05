SELECT DISTINCT DC.IdDocumento,
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
  FROM (PD_Bulto BU right
        JOIN(PD_CentroProceso CPD RIGHT
             JOIN(PD_CentroProceso CPO INNER JOIN PD_DocumentoCabecera DC ON
                  DC.IdCentroProcesoOrigen = CPO.IdCentroProceso) ON
             DC.IdCentroProcesoDestino = CPD.IdCentroProceso) ON
        BU.IdDocumento = DC.IdDocumento)
 WHERE (DC.NumComprobante >= :NumComprobanteDesde or
       length(:NumComprobanteDesde) IS NULL)
   and (DC.NumComprobante <= :NumComprobanteHasta or
       length(:NumComprobanteHasta) IS NULL)
   and (Dc.iddocumento in
       (select c.idgrupo
           From -- Recupera a quantidade de Precintos encontrados no documento
                 (select d.idgrupo, count(b.numprecinto) QtdPrecintoEcontrados
                    from pd_bulto b
                   inner join pd_documentocabecera d on b.iddocumento =
                                                        d.idprimordial
                   where instr(:ListaCodigos,
                               '|' || rtrim(b.NumPrecinto) || '|') > 0
                   group by d.idgrupo) a,
                -- Recupera a quantidade de Precintos existentes no documento   
                (select d.idgrupo, count(b.numprecinto) QtdPrecintoExistentes
                   from pd_bulto b
                  inner join pd_documentocabecera d on b.iddocumento =
                                                       d.idprimordial
                  where d.idgrupo in
                        (select distinct d1.idgrupo
                           from pd_bulto b1
                          inner join pd_documentocabecera d1 on b1.iddocumento =
                                                                d1.idprimordial
                          where instr(:ListaCodigos,
                                      '|' || rtrim(b1.NumPrecinto) || '|') > 0)
                  group by d.idgrupo) b,
                pd_documentocabecera c
          where b.QtdPrecintoExistentes = a.QtdPrecintoEcontrados
            and a.idgrupo = c.idgrupo
          group by c.idgrupo))
   and (Fecha <= :FechaHasta or :FechaHasta is null or Fecha is null)
   and (Fecha >= :FechaDesde or :FechaDesde is null or Fecha is null)
   and ((:VistaDestinatario = 0 and
       IdCentroProcesoOrigen = :IdCentroProceso) or
       (:VistaDestinatario = 1 and
       IdCentroProcesoDestino = :IdCentroProceso))
   and IdEstadoComprobante = :IdEstadoComprobante
   and DC.IdGrupo is Null
