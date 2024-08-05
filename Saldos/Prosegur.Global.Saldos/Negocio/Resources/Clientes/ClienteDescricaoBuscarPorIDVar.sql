Select	PDC.Descripcion
From	Prosedocs.dbo.Pd_Cliente PDC
Where	charindex('|' + convert(varchar,PDC.IdCliente) + '|',:IdClienteVar) > 0