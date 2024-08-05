select distinct 
	F.IdFormulario, 
	F.descripcion
From 
	PD_Formulario  F
LEFT JOIN 
	PD_FormularioReporteCondicion PRC 
		on	PRC.IdFormulario = F.IdFormulario And PRC.Reporte = :Reporte
Where 
	PRC.IdFormulario IS NULL
order by 2