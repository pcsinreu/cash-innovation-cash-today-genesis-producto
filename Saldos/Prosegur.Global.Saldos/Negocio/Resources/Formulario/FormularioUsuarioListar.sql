select distinct 
	F.IdFormulario, 
	F.descripcion
From 
	PD_Formulario  F
	Inner Join PD_FormularioTipoCentroProceso PDFTCP	on F.IdFormulario = PDFTCP.IdFormulario 
	Inner Join PD_CentroProceso               PDCP		on PDFTCP.IdTipoCentroProceso = PDCP.IdTipoCentroProceso
	Inner Join PD_UsuarioCentroProceso        PDUCP		on PDUCP.IdCentroProceso = PDCP.IdCentroProceso
Where 
	PDUCP.IdUsuario = :IdUsuario
	And PDUCP.IdCentroProceso = PDCP.IdCentroProceso
order by 2