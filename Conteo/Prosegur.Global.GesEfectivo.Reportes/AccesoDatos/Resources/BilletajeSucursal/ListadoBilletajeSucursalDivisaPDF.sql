﻿SELECT
  SUM(
	CASE 
      WHEN TREGMOV.NUM_IMPORTE_EFECTIVO IS NOT NULL OR TREGMOV.NUM_IMPORTE_EFECTIVO <> 0 THEN
       TREGMOV.NUM_IMPORTE_EFECTIVO
			WHEN	GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO=0
	OR	GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO IS NULL THEN
     (TDECL.num_importe_efectivo)
			ELSE
     (GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO)
 END)  AS IMPORTE_DECLARADO_EFETIVO,
	SUM(
		CASE 
		  WHEN TREGMOV.Num_Importe_Cheque IS NOT NULL OR TREGMOV.Num_Importe_Cheque <> 0 
			 THEN TREGMOV.Num_Importe_Cheque
		  ELSE TDECL.num_importe_cheque
		END) IMPORTE_DECLARADO_CHEQUE,
	SUM(
		CASE 
		  WHEN TREGMOV.Num_Importe_Ticket IS NOT NULL OR TREGMOV.Num_Importe_Ticket <> 0 
			 THEN TREGMOV.Num_Importe_Ticket
		  ELSE TDECL.num_importe_ticket
		END) IMPORTE_DECLARADO_TICKET,
	SUM(
		CASE 
		  WHEN TREGMOV.Num_Importe_Otro_Valor IS NOT NULL OR TREGMOV.Num_Importe_Otro_Valor <> 0 
			 THEN TREGMOV.Num_Importe_Otro_Valor
		  ELSE TDECL.num_importe_otro_valor
		END) IMPORTE_DECLARADO_OTRO_VALOR,
	TREM.COD_PUNTO_SERVICIO ESTACION,
	TREM.DES_PUNTO_SERVICIO DES_ESTACION,
	TREM.COD_ISO_DIVISA 
FROM
	(           
		SELECT DISTINCT 
		   TBULT.COD_PUNTO_SERVICIO,
		   TBULT.DES_PUNTO_SERVICIO,
		   TDIVI.COD_ISO_DIVISA,
		   TREM.OID_REMESA
		FROM  
			GEPR_TREMESA TREM 
			INNER JOIN GEPR_TBULTO TBULT ON TREM.OID_REMESA = TBULT.OID_REMESA
			INNER JOIN GEPR_TDIVISA_POSIBLE TDIVP ON TBULT.OID_BULTO = TDIVP.OID_BULTO
            INNER JOIN GEPR_TDIVISA TDIVI ON TDIVP.COD_ISO_DIVISA = TDIVI.COD_ISO_DIVISA
		WHERE
			TBULT.COD_ESTADO <> 'RZ' AND
			TBULT.COD_CLIENTE = []COD_CLIENTE AND
			TBULT.COD_DELEGACION = []COD_DELEGACION 
			[FEC_TRANSPORTE] 
			[FEC_PROCESO] 
			[FYH_FIN_CONTEO] 
			[FILTRO_PROCESO] 
	)  TREM
	LEFT OUTER JOIN 
        (
              select R.OID_REMESA,
           T.NUM_IMPORTE_EFECTIVO,
           T.Num_Importe_Cheque,
           T.Num_Importe_Ticket,
           T.Num_Importe_Otro_Valor,
           T.COD_ISO_DIVISA,
           r.cod_tipo_movimiento from gepr_tregistro_movimiento r
              inner join gepr_tdeclarado_total t on r.oid_registro_movimiento = t.oid_registro_movimiento
              where r.cod_tipo_movimiento='D' and r.cod_tipo_contenedor='R'
        ) TREGMOV
        ON TREM.OID_REMESA=TREGMOV.OID_REMESA AND TREM.COD_ISO_DIVISA = TREGMOV.COD_ISO_DIVISA
        LEFT OUTER JOIN   GEPR_TDECLARADO_REMESA TDECL  ON TREM.OID_REMESA=TDECL.OID_REMESA 
        AND  TREM.COD_ISO_DIVISA=TDECL.COD_ISO_DIVISA   
        LEFT OUTER JOIN
        (
            SELECT  
				TDECL.OID_REMESA OID_REMESA,
				TDIVI.COD_ISO_DIVISA COD_ISO_DIVISA, 
				SUM(TDENO.NUM_VALOR*TDECL.NEL_UNIDADES)   NUM_IMPORTE_EFECTIVO_DENO
			FROM  
				GEPR_TREMESA TREM INNER JOIN GEPR_TBULTO TBULT 
				ON TREM.OID_REMESA = TBULT.OID_REMESA INNER JOIN   GEPR_TDECLARADO_REMESA_DET TDECL  
				ON TREM.OID_REMESA=TDECL.OID_REMESA  INNER JOIN GEPR_TDENOMINACION TDENO
				ON TDECL.COD_DENOMINACION=TDENO.COD_DENOMINACION  INNER JOIN  GEPR_TDIVISA  TDIVI
				ON TDENO.OID_DIVISA=TDIVI.OID_DIVISA 
            WHERE
				TBULT.COD_ESTADO <> 'RZ' AND
				TBULT.COD_CLIENTE = []COD_CLIENTE AND
				TBULT.COD_DELEGACION = []COD_DELEGACION 
				[FEC_TRANSPORTE] 
				[FEC_PROCESO] 
				[FYH_FIN_CONTEO] 
				[FILTRO_PROCESO] 				
            GROUP BY 
				TDECL.OID_REMESA,
				TDIVI.COD_ISO_DIVISA
        )   GEPR_TDECL_TDENO01 ON TREM.OID_REMESA=GEPR_TDECL_TDENO01.OID_REMESA
        AND TREM.COD_ISO_DIVISA=GEPR_TDECL_TDENO01.COD_ISO_DIVISA
GROUP BY 
    TREM.COD_PUNTO_SERVICIO,
    TREM.DES_PUNTO_SERVICIO,
    TREM.COD_ISO_DIVISA
ORDER BY
    ESTACION