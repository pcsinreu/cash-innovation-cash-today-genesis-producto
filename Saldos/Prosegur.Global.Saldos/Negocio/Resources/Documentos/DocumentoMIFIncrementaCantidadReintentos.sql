update pd_documentocabecera set 
DES_ERR_ENVIO = NULL,
nel_intentos = nel_intentos + 1 
where iddocumento = :IdDocumento