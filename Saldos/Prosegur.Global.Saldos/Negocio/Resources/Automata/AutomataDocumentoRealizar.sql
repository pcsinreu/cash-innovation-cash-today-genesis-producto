SELECT 
	IdDocumento  as Id, 
	IdEstadoComprobante 
FROM  
	PD_DocumentoCabecera 
WHERE 
	IdFormulario = :IdFormulario
	AND (EsGrupo = 0)
	AND (Sustituido = 0)
	AND (Exportado = 0)
	AND IdEstadoComprobante = :IdEstadoAExportar
	AND FechaResuelve >= (sysdate - :DiasAProcesar)