UPDATE PD_DocumentoCabecera
   SET Fecha = :Fecha, 
       IdEstadoComprobante = :IdEstado
 WHERE IdDocumento = :IdDocumento
 
 