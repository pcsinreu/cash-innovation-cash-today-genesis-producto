INSERT INTO PD_Extracto(
		IdExtracto,
		FechaDesde,
		FechaHasta,
		IdPlanta,
		IdMoneda,
		IdCliente,
		IdBanco,
		IdDocumento,
		IdAnterior,
		VistaCliente) 
    
    VALUES(
		:IdExtracto,
		:FechaDesde ,
		case 
		when (:FechaHasta < sysdate) then :FechaHasta
		else sysDate
		end ,
		:IdPlanta ,
		:IdMoneda,
		:IdCliente,
		:IdBanco ,
		:IdDocumento,
		:IdAnterior,
		:VistaCliente)