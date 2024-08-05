SELECT count(*)
FROM 
	pd_documentocabecera
WHERE 
	idcentroprocesoorigen = :idcentroprocesoorigen
	AND iddocumento = :iddocumento