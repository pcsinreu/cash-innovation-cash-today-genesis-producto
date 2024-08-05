SELECT count(*)
FROM 
	pd_documentocabecera dc
    inner join pd_centroproceso cp on dc.idcentroprocesoorigen = cp.idcentroproceso
WHERE 
	cp.idps = :idpscentroprocesoorigen
	AND dc.iddocumento = :iddocumento