select mon.isogenesis
           ,esp.idgenesis
           ,det.cantidad
           ,det.importe
       	from pd_documentodetalle det 
inner join pd_especie esp on det.idespecie = esp.idespecie
inner join pd_moneda mon on esp.idmoneda = mon.idmoneda
	where esp.idgenesis is not null and 
		det.iddocumento = :ID_DOCUMENTO
