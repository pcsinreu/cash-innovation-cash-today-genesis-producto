UPDATE PD_Moneda
   SET Descripcion = :Descripcion, 
	   Simbolo = :Simbolo, 
	   Visible = :Visible,
	   IsoGenesis = :IsoGenesis,
	   IdGenesis = :IdGenesis,
	   IsoSaldos = :IsoSaldos	   
 WHERE IdMoneda = :IdMoneda