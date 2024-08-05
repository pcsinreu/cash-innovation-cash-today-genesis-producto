INSERT INTO
	PD_HISTORICOINVENTARIO
	(	
		IdInventario,
		IdCentroProceso,
		FechaEmision,
		ArchivoPDF,
		ArchivoExcel
	)
VALUES 
(
	PD_HISTORICOINVENTARIO_SEQ.NextVal,
	:IdCentroProceso,
	SYSDATE,
	:ArchivoPDF,
	:ArchivoExcel
)
