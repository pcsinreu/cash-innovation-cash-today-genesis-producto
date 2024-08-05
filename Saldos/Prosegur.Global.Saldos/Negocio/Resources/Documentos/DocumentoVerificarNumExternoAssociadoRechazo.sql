SELECT 
	COUNT(1) 
FROM 
	pd_documentocabecera p 
WHERE 
	p.numexterno = :NumExterno AND 
	p.iddocumento <> :Iddocumento AND 
	NOT EXISTS (SELECT 1 FROM pd_documentocabecera e WHERE e.numexterno = p.numexterno AND e.idformulario = :IdFormulario) 