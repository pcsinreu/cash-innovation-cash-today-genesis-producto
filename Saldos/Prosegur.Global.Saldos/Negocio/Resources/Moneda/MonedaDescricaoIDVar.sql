Select Descripcion
From	PD_Moneda
Where INSTR(:IdMonedaVar, '|' || IdMoneda || '|') > 0