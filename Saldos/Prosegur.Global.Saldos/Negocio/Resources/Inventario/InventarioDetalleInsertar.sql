INSERT INTO InventarioDetalle
	(IdCentroProceso,
	IdInventario,
	Iddocumento,
	IdMoneda,
	Importe,
	Tipo,
	IdEstadoComprobante,
	IdDocDetalles) 
VALUES 
	(:IdCentroProceso,
	 :IdInventario,
	 :Iddocumento,
	 :IdMoneda,
	 :Importe,
	 :Tipo,
	 :IdEstadoComprobante,
	 :IdDocDetalles)