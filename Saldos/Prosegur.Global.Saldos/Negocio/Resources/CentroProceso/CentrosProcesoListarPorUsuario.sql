select 
	UCP.IdCentroProceso
    ,CP.Descripcion
from 
	PD_CentroProceso CP
    ,PD_UsuarioCentroProceso UCP
where 
	UCP.IdCentroProceso = CP.IdCentroProceso	
	and UCP.IdUsuario = :IdUsuario