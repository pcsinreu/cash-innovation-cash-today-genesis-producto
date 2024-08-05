﻿SELECT 
       EB.OID_BULTO,
       EB.OID_MODULO_BULTO,
	   D.COD_ISO_DIVISA,
       D.DES_DIVISA,
       D.OID_DIVISA,
       DEN.OID_DENOMINACION,
       DEN.COD_DENOMINACION,
       DEN.DES_DENOMINACION,
	   DEN.BOL_BILLETE,
	   DEN.NUM_VALOR_FACIAL,
       SUM(EB.NEL_CANTIDAD) NEL_CANTIDAD,
       SUM(EB.NEL_CANTIDAD * DEN.NUM_VALOR_FACIAL) NEL_IMPORTE_EFECTIVO
FROM GEPR_TDIVISA D
INNER JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DIVISA = D.OID_DIVISA
INNER JOIN GEPR_TEFECTIVO_BULTO EB ON EB.OID_DENOMINACION = DEN.OID_DENOMINACION
{0}
GROUP BY EB.OID_BULTO,EB.OID_MODULO_BULTO, D.COD_ISO_DIVISA, D.DES_DIVISA, D.OID_DIVISA, DEN.OID_DENOMINACION,DEN.COD_DENOMINACION, DEN.DES_DENOMINACION, DEN.BOL_BILLETE, DEN.NUM_VALOR_FACIAL