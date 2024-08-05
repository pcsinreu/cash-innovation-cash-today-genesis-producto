SELECT ec.descripcion,
       ec.codigo,
	   ec.IdEstadoComprobante Id
FROM pd_estadocomprobante ec
INNER JOIN pd_documentocabecera doc
        ON doc.IDESTADOCOMPROBANTE = ec.IDESTADOCOMPROBANTE
WHERE doc.iddocumento = :iddocumento