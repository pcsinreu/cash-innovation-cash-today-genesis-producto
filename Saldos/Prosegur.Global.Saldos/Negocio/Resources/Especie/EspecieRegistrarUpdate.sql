UPDATE PD_Especie
   SET Descripcion      = :Descripcion,
       Uniforme         = :Uniforme,
       Orden            = :Orden,
       EnActaProceso    = :EnActaProceso,
       IdRBO            = :IdRBO,
       IdSIGII          = :IdSIGII,
       Calidad          = :Calidad,
       EsDefaultGenesis = :EsDefaultGenesis,
       IdGenesis        = :IdGenesis,
	   Bol_Billete      = :EsBillete,
	   Bol_Moneda       = :EsMoneda
 WHERE IdEspecie = :IdEspecie