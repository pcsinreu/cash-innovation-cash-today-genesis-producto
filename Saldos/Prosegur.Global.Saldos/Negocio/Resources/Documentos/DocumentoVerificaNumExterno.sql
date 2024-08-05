SELECT count(*)
FROM PD_DocumentoCabecera
WHERE NumExterno = :NumExterno
   and IdDocumento <> :IdDocumento
   and ((IdDocumento <> :IdOrigen and Reenviado = 0) or :IdOrigen is null)
   and Sustituido <> 1
   and Reenviado <> 1