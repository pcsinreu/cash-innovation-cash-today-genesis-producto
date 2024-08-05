﻿SELECT /*CE.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       D.OID_DENOMINACION,
       NULL AS OID_MEDIO_PAGO,
	   NULL AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
       NULL AS T_Diferencia,
       NULL AS T_Declarado,
       NULL AS G_Diferencia,
       NULL AS G_Declarado,
	   NULL AS COD_TIPO_MEDIO_PAGO,
	   NULL As MP_Diferencia,
	   NULL As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TCONTADO_EFECTIVO CE ON CE.OID_REMESA = R.OID_REMESA AND CE.OID_BULTO IS NULL AND CE.OID_PARCIAL IS NULL
INNER JOIN GEPR_TDENOMINACION D ON D.OID_DENOMINACION = CE.OID_DENOMINACION
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = D.OID_DIVISA
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (CE.OID_DOCUMENTO IS NULL OR CE.OID_DOCUMENTO = []OID_DOCUMENTO)*/
UNION
SELECT /*DE.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       D.OID_DENOMINACION,
       NULL AS OID_MEDIO_PAGO,
	   NULL AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
       NULL AS T_Diferencia,
                (SELECT DE.NUM_IMPORTE
                        FROM  SAPR_TDECLARADO_EFECTIVO DE
                        WHERE DE.OID_DIVISA = DIV.OID_DIVISA  AND rownum = 1 
                        AND DE.COD_NIVEL_DETALLE = 'T'
                        AND DE.OID_DENOMINACION IS NULL
                        AND DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL) T_Declarado,
       NULL AS G_Diferencia,
                (SELECT DE.NUM_IMPORTE
                        FROM  SAPR_TDECLARADO_EFECTIVO DE
                        WHERE DE.OID_DIVISA = DIV.OID_DIVISA AND rownum = 1 
                        AND DE.COD_NIVEL_DETALLE = 'G'
                        AND DE.OID_DENOMINACION IS NULL
                        AND DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL) G_Declarado,
	   NULL AS COD_TIPO_MEDIO_PAGO,
	   NULL As MP_Diferencia,
	   NULL As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TDECLARADO_EFECTIVO DE ON DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL
INNER JOIN GEPR_TDIVISA DIV ON DE.OID_DIVISA = DIV.OID_DIVISA
LEFT JOIN GEPR_TDENOMINACION D ON D.OID_DENOMINACION = DE.OID_DENOMINACION
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (DE.OID_DOCUMENTO IS NULL OR DE.OID_DOCUMENTO = []OID_DOCUMENTO)*/
UNION
SELECT /*DE.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       D.OID_DENOMINACION,
       NULL AS OID_MEDIO_PAGO,
	   NULL AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
                (SELECT DE.NUM_IMPORTE
                        FROM  SAPR_TDIFERENCIA_EFECTIVO DE
                        WHERE DE.OID_DIVISA = DIV.OID_DIVISA AND rownum = 1 
                        AND DE.COD_NIVEL_DETALLE = 'T'
                        AND DE.OID_DENOMINACION IS NULL
                        AND DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL) T_Diferencia,
                NULL AS T_Declarado,
                (SELECT DE.NUM_IMPORTE
                        FROM  SAPR_TDIFERENCIA_EFECTIVO DE
                        WHERE DE.OID_DIVISA = DIV.OID_DIVISA AND rownum = 1 
                        AND DE.COD_NIVEL_DETALLE = 'G'
                        AND DE.OID_DENOMINACION IS NULL
                        AND DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL) G_Diferencia,
                NULL AS G_Declarado,
	   NULL AS COD_TIPO_MEDIO_PAGO,
	   NULL As MP_Diferencia,
	   NULL As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TDIFERENCIA_EFECTIVO DE ON DE.OID_REMESA = R.OID_REMESA AND DE.OID_BULTO IS NULL AND DE.OID_PARCIAL IS NULL
INNER JOIN GEPR_TDIVISA DIV ON DE.OID_DIVISA = DIV.OID_DIVISA
LEFT JOIN GEPR_TDENOMINACION D ON D.OID_DENOMINACION = DE.OID_DENOMINACION
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (DE.OID_DOCUMENTO IS NULL OR DE.OID_DOCUMENTO = []OID_DOCUMENTO)*/
UNION
SELECT /*CPP.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       NULL AS OID_DENOMINACION,
       MP.OID_MEDIO_PAGO,
	   CPP.OID_CONTADO_MEDIO_PAGO AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
       NULL AS T_Diferencia,
       NULL AS T_Declarado,
       NULL AS G_Diferencia,
       NULL AS G_Declarado,
	   NULL AS COD_TIPO_MEDIO_PAGO,
	   NULL As MP_Diferencia,
	   NULL As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TCONTADO_MEDIO_PAGO CPP ON CPP.OID_REMESA = R.OID_REMESA AND CPP.OID_BULTO IS NULL AND CPP.OID_PARCIAL IS NULL
INNER JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = CPP.OID_MEDIO_PAGO
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = MP.OID_DIVISA
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (CPP.OID_DOCUMENTO IS NULL OR CPP.OID_DOCUMENTO = []OID_DOCUMENTO)*/
UNION
SELECT /*DMP.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       NULL AS OID_DENOMINACION,
       MP.OID_MEDIO_PAGO,
	   DMP.OID_DECLARADO_MEDIO_PAGO AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
       NULL AS T_Diferencia,
       NULL AS T_Declarado,
       NULL AS G_Diferencia,
       NULL AS G_Declarado,
	   DMP.COD_TIPO_MEDIO_PAGO,
	   NULL As MP_Diferencia,
	    (SELECT DMP1.NUM_IMPORTE
			FROM  SAPR_TDECLARADO_MEDIO_PAGO DMP1
			WHERE DMP1.OID_DIVISA = DIV.OID_DIVISA AND DMP1.OID_MEDIO_PAGO IS NULL AND DMP1.COD_TIPO_MEDIO_PAGO = DMP.COD_TIPO_MEDIO_PAGO
			AND DMP1.OID_REMESA = R.OID_REMESA AND DMP1.OID_BULTO IS NULL AND DMP1.OID_PARCIAL IS NULL AND rownum = 1) As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TDECLARADO_MEDIO_PAGO DMP ON DMP.OID_REMESA = R.OID_REMESA AND DMP.OID_BULTO IS NULL AND DMP.OID_PARCIAL IS NULL
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = DMP.OID_DIVISA
LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = DMP.OID_MEDIO_PAGO
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (DMP.OID_DOCUMENTO IS NULL OR DMP.OID_DOCUMENTO = []OID_DOCUMENTO)*/
UNION
SELECT /*DMP.OID_DOCUMENTO,*/
	   DIV.COD_ISO_DIVISA,
       DIV.OID_DIVISA,
       DIV.DES_DIVISA,
       DIV.BOL_VIGENTE AS DIVISA_ACTIVA,
       DIV.COD_SIMBOLO,
       DIV.COD_USUARIO,
       DIV.FYH_ACTUALIZACION,
       NULL AS OID_DENOMINACION,
       MP.OID_MEDIO_PAGO,
	   NULL AS OID_DEC_CONT_MEDIO_PAGO,
	   DIV.COD_COLOR,
       NULL AS T_Diferencia,
       NULL AS T_Declarado,
       NULL AS G_Diferencia,
       NULL AS G_Declarado,
	   DMP.COD_TIPO_MEDIO_PAGO,
				(SELECT DMP1.NUM_IMPORTE
						FROM  SAPR_TDIFERENCIA_MEDIO_PAGO DMP1
						WHERE DMP1.OID_DIVISA = DIV.OID_DIVISA AND DMP1.OID_MEDIO_PAGO IS NULL AND DMP1.COD_TIPO_MEDIO_PAGO = DMP.COD_TIPO_MEDIO_PAGO
						AND DMP1.OID_REMESA = R.OID_REMESA AND DMP1.OID_BULTO IS NULL AND DMP1.OID_PARCIAL IS NULL  AND rownum = 1) As MP_Diferencia,
	   NULL As MP_Declarado
FROM SAPR_TREMESA R
INNER JOIN SAPR_TDIFERENCIA_MEDIO_PAGO DMP ON DMP.OID_REMESA = R.OID_REMESA AND DMP.OID_BULTO IS NULL AND DMP.OID_PARCIAL IS NULL
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = DMP.OID_DIVISA
LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = DMP.OID_MEDIO_PAGO
WHERE R.OID_REMESA = []OID_ELEMENTO /*AND (DMP.OID_DOCUMENTO IS NULL OR DMP.OID_DOCUMENTO = []OID_DOCUMENTO)*/