select 	RL.ArchivoRemesaLegado
from PD_DocumentoCabecera DC 
     LEFT JOIN PD_REMESA_LEGADO RL ON (nvl(dc.IdPrimordial, dc.IdDocumento) = RL.IdDocumento) 
where dc.legado = 0 and dc.exportado_conteo = 1 and nvl(dc.IdPrimordial, dc.IdDocumento) = :iddocumento.