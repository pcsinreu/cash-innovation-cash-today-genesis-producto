select 
	H.IdInventario
	,H.FechaEmision
from 
	PD_HISTORICOINVENTARIO H
where 
	H.IDCENTROPROCESO = :IdCentroProceso 
    and (:DataIni is null or (:DataIni is not null and H.FECHAEMISION >= :DataIni))
    and (:DataFim is null or (:DataFim is not null and H.FECHAEMISION <= :DataFim))
order by 
	H.FECHAEMISION desc
