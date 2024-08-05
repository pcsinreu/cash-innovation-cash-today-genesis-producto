select 
     DOC.IdDocumento as IdDocumento
     ,CLO.Descripcion as ClienteOrigen
     ,BOR.Descripcion as BancoOrigen
     ,trunc(DOC.FechaGestion) as FechaGestion
     ,DES.Descripcion as Destino
     ,DOC.NumExterno as NumeroExterno
     ,BUL.NumPrecinto as NumeroPrecinto
     ,BUL.CodBolsa as CodigoBolsa
     ,ESC.Codigo as EstadoComprobante
     ,MON.Descripcion as Moneda
     ,MON.Simbolo as Simbolo
     ,SUM(DET.importe) as Importe
from pd_DocumentoCabecera DOC
     INNER JOIN pd_EstadoComprobante ESC ON DOC.IdEstadoComprobante = ESC.IdEstadoComprobante
     INNER JOIN pd_Bulto BUL ON BUL.IdDocumento = NVL(DOC.IdDocDetalles, DOC.IdDocumento)
     LEFT JOIN pd_DocumentoDetalle DET ON DET.IdDocumento = NVL(DOC.IdDocDetalles, DOC.IdDocumento)
     LEFT JOIN PD_Destino DES ON BUL.IdDestino = DES.IdDestino
     LEFT JOIN pd_Especie ESP ON DET.IdEspecie = ESP.IdEspecie
     LEFT JOIN pd_Moneda MON ON ESP.IdMoneda = MON.IdMoneda
     LEFT JOIN pd_Cliente CLO ON DOC.IdClienteOrigen = CLO.IdCliente
     LEFT JOIN pd_Cliente BOR ON DOC.IdBanco = BOR.IdCliente,
     (select 
           max(fechaemision) as fechaemision
      from 
           pd_historicoinventario
      where 
           idcentroproceso = :IdCentroProceso) HV
where 
     (
           (DOC.IdEstadoComprobante = 3 and DOC.IdCentroProcesoDestino = :IdCentroProceso) or
           (DOC.IdEstadoComprobante = 4 and DOC.IdCentroProcesoOrigen = :IdCentroProceso)
     )
     and DOC.IdFormulario not in (select 
                                       FRC.IdFormulario 
                                  from 
                                       PD_FormularioReporteCondicion FRC 
                                  where 
                                       FRC.Reporte = 3)
     and DOC.NumExterno is not null
     and DOC.Reenviado = 0
     and DOC.Sustituido = 0
     and DOC.Disponible = 0
     and (HV.fechaemision is null or (HV.fechaemision is not null and DOC.FechaResuelve > HV.fechaemision))
 group by 
     DOC.IdDocumento,
     CLO.Descripcion,
     BOR.Descripcion,
     trunc(DOC.FechaGestion),
     DES.Descripcion,
     DOC.NumExterno,
     BUL.NumPrecinto,
     BUL.CodBolsa,
     ESC.Codigo,
     MON.Descripcion,
     MON.Simbolo
 order by 
     DOC.NumExterno