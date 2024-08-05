update pd_documentocabecera set 
DES_ERR_ENVIO = NULL,
reintentos_conteo = reintentos_conteo + 1 
where iddocumento = :iddocumento