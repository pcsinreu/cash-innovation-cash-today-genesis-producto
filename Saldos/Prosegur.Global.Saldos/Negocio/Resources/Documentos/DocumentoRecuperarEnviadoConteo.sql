select 
	nvl(dc.IdDocumento,0) IdDocumento
from 
	pd_documentocabecera dc
where 
	dc.legado = 0 and
	dc.exportado_conteo = 1 and
	dc.idprimordial = :iddocumento
