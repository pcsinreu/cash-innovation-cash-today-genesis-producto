Select	PDCP.Descripcion
From	Prosedocs.dbo.PD_CentroProceso PDCP
Where	charindex('|' + convert(varchar,PDCP.IdCentroProceso) + '|',:IdCentroProcesoVar) > 0
