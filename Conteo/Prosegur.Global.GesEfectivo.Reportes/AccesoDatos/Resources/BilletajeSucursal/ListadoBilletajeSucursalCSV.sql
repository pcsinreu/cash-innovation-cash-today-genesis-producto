﻿SELECT 
	TBILLETAJEXSUC.RECUENTO, 
	TBILLETAJEXSUC.FECHA, 
	TBILLETAJEXSUC.LETRA, 
	TBILLETAJEXSUC.F22, 
	TBILLETAJEXSUC.OID_REMESA_ORI, 
	TBILLETAJEXSUC.COD_SUBCLIENTE, 
	TBILLETAJEXSUC.ESTACION, 
	TBILLETAJEXSUC.DES_ESTACION, 
	TBILLETAJEXSUC.MEDIO_PAGO, 
	TBILLETAJEXSUC.DIVISA, 
	TBILLETAJEXSUC.DES_DIVISA, 
	TBILLETAJEXSUC.UNIDAD, 
	TBILLETAJEXSUC.MUTIPLICADOR, 
	TBILLETAJEXSUC.ES_BILLETE, 
	SUM(TBILLETAJEXSUC.CANTIDAD) AS CANTIDAD, 
	SUM(TBILLETAJEXSUC.VALOR) AS VALOR,
	TBILLETAJEXSUC.COD_CALIDAD 
FROM 
	( 
	-- MEDIOS DE PAGO 
	SELECT 
		TBULT.DES_BOOK_PROCESO AS RECUENTO, 
		TBULT.FEC_TRANSPORTE AS FECHA, 
		'A' LETRA, 
		TBULT.COD_TRANSPORTE AS F22, 
		TREGI.OID_REMESA_ORI AS OID_REMESA_ORI, 
		TBULT.COD_SUBCLIENTE AS COD_SUBCLIENTE, 
		TBULT.COD_PUNTO_SERVICIO AS ESTACION, 
		TBULT.DES_PUNTO_SERVICIO AS DES_ESTACION, 
		TMEDI.DES_TIPO_MEDIO_PAGO AS MEDIO_PAGO, 
		TMEDI.COD_ISO_DIVISA AS DIVISA, 
		TDIVI.DES_DIVISA AS DES_DIVISA, 
		CASE WHEN TMEDI.COD_TIPO_MEDIO_PAGO = 'codtipoa' 
		THEN TMEDI.DES_MEDIO_PAGO 
		ELSE TMEDI.DES_TIPO_MEDIO_PAGO 
		END AS UNIDAD, 
		0 AS MUTIPLICADOR, 
		0 AS ES_BILLETE, 
		TMEDI.NUM_UNIDADES AS CANTIDAD, 
		TMEDI.NUM_IMPORTE AS VALOR,
		NULL AS COD_CALIDAD 
	FROM 
		GEPR_TBULTO TBULT 
		INNER JOIN GEPR_TREGISTRO_MOVIMIENTO TREGI ON TBULT.OID_BULTO = TREGI.OID_BULTO 
		INNER JOIN GEPR_TMEDIO_PAGO TMEDI ON TREGI.OID_REGISTRO_MOVIMIENTO = TMEDI.OID_REGISTRO_MOVIMIENTO 
		INNER JOIN GEPR_TDIVISA TDIVI ON TMEDI.COD_ISO_DIVISA=TDIVI.COD_ISO_DIVISA 
	WHERE 
		TBULT.COD_CLIENTE = []COD_CLIENTE 
		AND TBULT.COD_DELEGACION = []COD_DELEGACION 
		[FEC_TRANSPORTE] 
		[FEC_PROCESO] 
		[FYH_FIN_CONTEO] 
		[FILTRO_PROCESO] 
		AND TREGI.COD_TIPO_MOVIMIENTO = []COD_TIPO_MOVIMENTO 
		AND TMEDI.NEC_SECUENCIA = 0 
	UNION ALL 
	--  EFECTIVO 
	SELECT 
		TBULT.DES_BOOK_PROCESO AS RECUENTO, 
		TBULT.FEC_TRANSPORTE AS FECHA, 
		'A' AS LETRA, 
		TBULT.COD_TRANSPORTE AS F22, 
		TREGI.OID_REMESA_ORI AS OID_REMESA_ORI, 
		TBULT.COD_SUBCLIENTE AS COD_SUBCLIENTE, 
		TBULT.COD_PUNTO_SERVICIO AS ESTACION, 
		TBULT.DES_PUNTO_SERVICIO AS DES_ESTACION, 
		'Efectivo' AS MEDIO_PAGO, 
		TEFEC.COD_ISO_DIVISA AS DIVISA, 
		TDIVI.DES_DIVISA AS DES_DIVISA, 
		TDENO.DES_DENOMINACION AS UNIDAD, 
		TDENO.NUM_VALOR AS CANTIDAD, 
		TDENO.BOL_BILLETE AS ES_BILLETE, 
		(TEFEC.NEL_UNIDADES_MANUAL + TEFEC.NEL_UNIDADES_MAQUINA) AS MUTIPLICADOR, 
		(TEFEC.NEL_UNIDADES_MANUAL + TEFEC.NEL_UNIDADES_MAQUINA) * TDENO.NUM_VALOR VALOR,
		CASE WHEN TEFEC.COD_CALIDAD = 'D' THEN TEFEC.COD_CALIDAD ELSE NULL END AS COD_CALIDAD 
	FROM 
		GEPR_TBULTO TBULT 
		INNER JOIN GEPR_TREGISTRO_MOVIMIENTO TREGI ON TBULT.OID_BULTO=TREGI.OID_BULTO 
		INNER JOIN GEPR_TEFECTIVO TEFEC ON TREGI.OID_REGISTRO_MOVIMIENTO=TEFEC.OID_REGISTRO_MOVIMIENTO 
		INNER JOIN GEPR_TDENOMINACION TDENO ON TEFEC.COD_DENOMINACION=TDENO.COD_DENOMINACION 
		INNER JOIN GEPR_TDIVISA TDIVI ON TDENO.OID_DIVISA=TDIVI.OID_DIVISA 
	WHERE 
		TBULT.COD_CLIENTE = []COD_CLIENTE 
		AND TBULT.COD_DELEGACION = []COD_DELEGACION 
		[FEC_TRANSPORTE] 
		[FEC_PROCESO] 
		[FYH_FIN_CONTEO] 
		[FILTRO_PROCESO] 
		AND TREGI.COD_TIPO_MOVIMIENTO = []COD_TIPO_MOVIMENTO
	) TBILLETAJEXSUC 
GROUP BY 
	TBILLETAJEXSUC.RECUENTO, 
	TBILLETAJEXSUC.FECHA, 
	TBILLETAJEXSUC.LETRA, 
	TBILLETAJEXSUC.F22, 
	TBILLETAJEXSUC.OID_REMESA_ORI, 
	TBILLETAJEXSUC.COD_SUBCLIENTE, 
	TBILLETAJEXSUC.ESTACION, 
	TBILLETAJEXSUC.DES_ESTACION, 
	TBILLETAJEXSUC.DIVISA, 
	TBILLETAJEXSUC.DES_DIVISA, 
	TBILLETAJEXSUC.MEDIO_PAGO, 
	TBILLETAJEXSUC.UNIDAD, 
	TBILLETAJEXSUC.COD_CALIDAD,
	TBILLETAJEXSUC.MUTIPLICADOR, 
	TBILLETAJEXSUC.ES_BILLETE 
ORDER BY 
	TBILLETAJEXSUC.F22