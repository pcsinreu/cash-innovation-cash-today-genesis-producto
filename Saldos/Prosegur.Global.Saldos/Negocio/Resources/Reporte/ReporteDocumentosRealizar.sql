SELECT DISTINCT TOTAL.IdDocumento,
                TOTAL.Fecha,
                TOTAL.NumComprobante,
                TOTAL.IdCentroProcesoOrigen,
                TOTAL.CentroProcesoOrigenDesc,
                TOTAL.IdCentroProcesoDestino,
                TOTAL.CentroProcesoDestinoDesc,
                TOTAL.IdClienteOrigen,
                TOTAL.ClienteOrigenDesc,
                TOTAL.IdClienteDestino,
                TOTAL.ClienteDestinoDesc,
                TOTAL.IdBanco,
                TOTAL.BancoDesc,
                TOTAL.IdBancoDeposito,
                TOTAL.BancoDepositoDesc,
                TOTAL.IdEstadoComprobante,
                TOTAL.IdUsuario,
                TOTAL.IdFormulario,
                TOTAL.IdBancoDeposito,
                TOTAL.IdUsuarioResuelve,
                TOTAL.FechaResuelve,
                TOTAL.FechaGestion,
                TOTAL.NumExterno,
                TOTAL.Agrupado,
                TOTAL.EsGrupo,
                TOTAL.IdGrupo,
                TOTAL.IdOrigen,
                TOTAL.Reenviado,
                TOTAL.Disponible,
                TOTAL.IdUsuarioDispone,
                TOTAL.FechaDispone,
                TOTAL.Sustituido,
                TOTAL.EsSustituto,
                TOTAL.IdDocDetalles,
                TOTAL.Descripcion,
                TOTAL.ConBultos,
                TOTAL.ConValores
  FROM (SELECT DISTINCT DC.IdDocumento,
                        DC.Fecha,
                        DC.NumComprobante,
                        DC.IdCentroProcesoOrigen,
                        CPO.Descripcion AS CentroProcesoOrigenDesc,
                        DC.IdCentroProcesoDestino,
                        CPD.Descripcion AS CentroProcesoDestinoDesc,
                        DC.IdClienteOrigen,
                        CO.IdPS || '-' || CO.Descripcion AS ClienteOrigenDesc,
                        DC.IdClienteDestino,
                        CD.IdPS || '-' || CD.Descripcion AS ClienteDestinoDesc,
                        DC.IdBanco,
                        BC.IdPS || '-' || BC.Descripcion AS BancoDesc,
                        DC.IdBancoDeposito,
                        BDC.IdPS || '-' || BDC.Descripcion AS BancoDepositoDesc,
                        DC.IdEstadoComprobante,
                        DC.IdUsuario,
                        DC.IdFormulario,
                        DC.IdUsuarioResuelve,
                        DC.FechaResuelve,
                        DC.FechaGestion,
                        DC.NumExterno,
                        NVL(DC.Agrupado, 0) AS Agrupado,
                        NVL(DC.EsGrupo, 0) AS EsGrupo,
                        NVL(DC.IdGrupo, 0) AS IdGrupo,
                        NVL(DC.IdOrigen, 0) AS IdOrigen,
                        NVL(DC.Reenviado, 0) AS Reenviado,
                        NVL(DC.Disponible, 1) AS Disponible,
                        DC.IdUsuarioDispone,
                        DC.FechaDispone,
                        NVL(DC.Sustituido, 0) AS Sustituido,
                        NVL(DC.EsSustituto, 0) AS EsSustituto,
                        NVL(DC.IdDocDetalles, 0) AS IdDocDetalles,
                        BU.NUMPRECINTO,
                        BU.CODBOLSA,
                        F.CONBULTOS,
                        F.CONVALORES,
                        F.DESCRIPCION
          FROM PD_Bulto BU
         RIGHT JOIN(PD_Cliente BDC
         RIGHT JOIN(PD_Banco BD
         RIGHT JOIN(PD_Cliente BC
         RIGHT JOIN(PD_Banco B
         RIGHT JOIN(PD_Cliente CD
         RIGHT JOIN(PD_Cliente CO
         RIGHT JOIN(PD_CentroProceso CPD
         RIGHT JOIN(PD_CentroProceso CPO
         INNER JOIN(PD_DocumentoCabecera DC
         INNER JOIN(PD_Formulario F
          LEFT JOIN PD_FormularioUsuario FU ON (F.IdFormulario =
                                               FU.IdFormulario AND
                                               (FU.IdUsuario IS NULL OR
                                               FU.IdUsuario = :IdUsuario OR :IdUsuario = 0))) ON DC.IdFormulario = F.IdFormulario) ON DC.IdCentroProcesoOrigen = CPO.IdCentroProceso) ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso) ON DC.IdClienteOrigen = CO.IdCliente) ON DC.IdClienteDestino = CD.IdCliente) ON DC.IdBanco = B.IdBanco) ON B.IdBanco = BC.IdCliente) ON DC.IdBancoDeposito = BD.IdBanco) ON BD.IdBanco = BDC.IdCliente) ON BU.IdDocumento = DC.IdDocumento
         WHERE (DC.Fecha >= :FechaDesde OR :FechaDesde IS NULL)
           AND (DC.Fecha < :FechaHasta Or :FechaHasta IS NULL)
        UNION ALL
        SELECT DISTINCT DC.IdDocumento,
                        DC.Fecha,
                        DC.NumComprobante,
                        DC.IdCentroProcesoOrigen,
                        CPO.Descripcion AS CentroProcesoOrigenDesc,
                        DC.IdCentroProcesoDestino,
                        CPD.Descripcion AS CentroProcesoDestinoDesc,
                        DC.IdClienteOrigen,
                        CO.IdPS || '-' || CO.Descripcion AS ClienteOrigenDesc,
                        DC.IdClienteDestino,
                        CD.IdPS || '-' || CD.Descripcion AS ClienteDestinoDesc,
                        DC.IdBanco,
                        BC.IdPS || '-' || BC.Descripcion AS BancoDesc,
                        DC.IdBancoDeposito,
                        BDC.IdPS || '-' || BDC.Descripcion AS BancoDepositoDesc,
                        DC.IdEstadoComprobante,
                        DC.IdUsuario,
                        DC.IdFormulario,
                        DC.IdUsuarioResuelve,
                        DC.FechaResuelve,
                        DC.FechaGestion,
                        DC.NumExterno,
                        NVL(DC.Agrupado, 0) AS Agrupado,
                        NVL(DC.EsGrupo, 0) AS EsGrupo,
                        NVL(DC.IdGrupo, 0) AS IdGrupo,
                        NVL(DC.IdOrigen, 0) AS IdOrigen,
                        NVL(DC.Reenviado, 0) AS Reenviado,
                        NVL(DC.Disponible, 1) AS Disponible,
                        DC.IdUsuarioDispone,
                        DC.FechaDispone,
                        NVL(DC.Sustituido, 0) AS Sustituido,
                        NVL(DC.EsSustituto, 0) AS EsSustituto,
                        NVL(DC.IdDocDetalles, 0) AS IdDocDetalles,
                        BU.NUMPRECINTO,
                        BU.CODBOLSA,
                        F.CONBULTOS,
                        F.CONVALORES,
                        F.DESCRIPCION
          FROM PD_Bulto BU
         RIGHT JOIN(PD_Cliente BDC
         RIGHT JOIN(PD_Banco BD
         RIGHT JOIN(PD_Cliente BC
         RIGHT JOIN(PD_Banco B
         RIGHT JOIN(PD_Cliente CD
         RIGHT JOIN(PD_Cliente CO
         RIGHT JOIN(PD_CentroProceso CPD
         RIGHT JOIN(PD_CentroProceso CPO
         INNER JOIN(PD_DocumentoCabecera DC
         INNER JOIN(PD_Formulario F
          LEFT JOIN PD_FormularioUsuario FU ON (F.IdFormulario =
                                               FU.IdFormulario AND
                                               (FU.IdUsuario IS NULL OR
                                               FU.IdUsuario = :IdUsuario OR
                                               :IdUsuario = 0))) ON DC.IdFormulario = F.IdFormulario) ON DC.IdCentroProcesoOrigen = CPO.IdCentroProceso) ON DC.IdCentroProcesoDestino = CPD.IdCentroProceso) ON DC.IdClienteOrigen = CO.IdCliente) ON DC.IdClienteDestino = CD.IdCliente) ON DC.IdBanco = B.IdBanco) ON B.IdBanco = BC.IdCliente) ON DC.IdBancoDeposito = BD.IdBanco) ON BD.IdBanco = BDC.IdCliente) ON BU.IdDocumento = DC.IdDocumento
         WHERE DC.Fecha IS NULL) TOTAL
 WHERE (TOTAL.NumComprobante >= :NumComprobanteDesde OR
       LENGTH(:NumComprobanteDesde) IS NULL)
   AND (TOTAL.NumComprobante <= :NumComprobanteHasta OR
       LENGTH(:NumComprobanteHasta) IS NULL)
   AND (TOTAL.Fecha >= :FechaDesde OR :FechaDesde IS NULL OR
       TOTAL.Fecha IS NULL)
   AND (TOTAL.Fecha < :FechaHasta OR :FechaHasta IS NULL OR
       TOTAL.Fecha IS NULL)
   AND ((:DistinguirPorVistaDestinatario = 0 AND
       ((TOTAL.IdCentroProcesoOrigen = :IdCentroProceso AND
       INSTR(:IdsEstadosComprobanteEmitido,
                 '|' || TOTAL.IdEstadoComprobante || '|') > 0) OR
       (NVL(TOTAL.IdCentroProcesoDestino, TOTAL.IdCentroProcesoOrigen) =
       :IdCentroProceso AND
       INSTR(:IdsEstadosComprobanteRecibido,
                 '|' || TOTAL.IdEstadoComprobante || '|') > 0))) OR
       (:DistinguirPorVistaDestinatario = 1 AND
       ((:VistaDestinatario = 0 AND
       TOTAL.IdCentroProcesoOrigen = :IdCentroProceso AND
       INSTR(:IdsEstadosComprobanteEmitido,
                 '|' || TOTAL.IdEstadoComprobante || '|') > 0) OR
       (:VistaDestinatario = 1 AND
       NVL(TOTAL.IdCentroProcesoDestino, TOTAL.IdCentroProcesoOrigen) =
       :IdCentroProceso AND
       INSTR(:IdsEstadosComprobanteRecibido,
                 '|' || TOTAL.IdEstadoComprobante || '|') > 0))))
   AND EsGrupo = 0
   AND (INSTR(:IdsFormulariosRestriccion, '|' || TOTAL.IdFormulario || '|') > 0 OR
       LENGTH(:IdsFormulariosRestriccion) IS NULL)
   AND (:DistinguirPorReenvio = 0 OR
       (:DistinguirPorReenvio = 1 AND TOTAL.Reenviado = :Reenviado))
   AND (:DistinguirPorDisponibilidad = 0 OR (:DistinguirPorDisponibilidad = 1 AND
       TOTAL.Disponible = :Disponible))
   AND (:DistinguirPorBultos = 0 OR
       (:DistinguirPorBultos = 1 AND TOTAL.ConBultos = :ConBultos))
   AND (:DistinguirPorValores = 0 OR
       (:DistinguirPorValores = 1 AND TOTAL.ConValores = :ConValores))
   AND ((:ConTomados = 0 AND
       (NOT (SELECT COUNT(*)
                 FROM PD_DocumentoCabecera IDC
                WHERE IDC.IdOrigen = TOTAL.IdDocumento) > 0)) OR
       (:ConTomados = 1))
   AND (:DistinguirPorSustitucion = 0 OR
       (:DistinguirPorSustitucion = 1 AND TOTAL.Sustituido = :Sustituido))
   AND (LENGTH(:TipoCodigo) IS NULL OR
       (:TipoCodigo = 'NC' AND
       INSTR(:ListaCodigos, '|' || RTRIM(TOTAL.NumComprobante) || '|') > 0) OR
       (:TipoCodigo = 'NE' AND
       INSTR(:ListaCodigos, '|' || RTRIM(TOTAL.NumExterno) || '|') > 0) OR
       (:TipoCodigo = 'CB' AND
       INSTR(:ListaCodigos, '|' || RTRIM(TOTAL.CodBolsa) || '|') > 0) OR
       (:TipoCodigo = 'NP' AND
       INSTR(:ListaCodigos, '|' || RTRIM(TOTAL.NumPrecinto) || '|') > 0))
 ORDER BY TOTAL.NumExterno, TOTAL.NumComprobante

