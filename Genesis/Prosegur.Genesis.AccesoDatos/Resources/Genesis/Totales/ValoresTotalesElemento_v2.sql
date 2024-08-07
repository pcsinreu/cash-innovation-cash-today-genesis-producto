﻿-- SAPR_TDECLARADO_EFECTIVO
SELECT
 'DECLARADO_EFECTIVO' AS TIPO
,DECEF.OID_REMESA
,DECEF.OID_BULTO
,DECEF.OID_PARCIAL
,DECEF.OID_DIVISA
,DECEF.COD_NIVEL_DETALLE
,NULL AS OID_DEC_CONT_MEDIO_PAGO
,NULL AS COD_TIPO_MEDIO_PAGO
,SUM(DECEF.NUM_IMPORTE) AS IMPORTE
,SUM(DECEF.NEL_CANTIDAD) AS CANTIDAD
     
      FROM SAPR_TDECLARADO_EFECTIVO DECEF
 LEFT JOIN GEPR_TDENOMINACION D ON D.OID_DENOMINACION = DECEF.OID_DENOMINACION
     WHERE DECEF.COD_NIVEL_DETALLE <> 'D' {0}
  GROUP BY DECEF.OID_REMESA, DECEF.OID_BULTO, DECEF.OID_PARCIAL, DECEF.OID_DIVISA, DECEF.COD_NIVEL_DETALLE
  
UNION

-- SAPR_TDIFERENCIA_EFECTIVO
SELECT
 'DIFERENCIA_EFECTIVO' AS TIPO
,DIFEF.OID_REMESA
,DIFEF.OID_BULTO
,DIFEF.OID_PARCIAL
,DIFEF.OID_DIVISA
,DIFEF.COD_NIVEL_DETALLE
,NULL AS OID_DEC_CONT_MEDIO_PAGO
,NULL AS COD_TIPO_MEDIO_PAGO
,SUM(DIFEF.NUM_IMPORTE) AS IMPORTE
,SUM(DIFEF.NEL_CANTIDAD) AS CANTIDAD

      FROM SAPR_TDIFERENCIA_EFECTIVO DIFEF
 LEFT JOIN GEPR_TDENOMINACION D ON D.OID_DENOMINACION = DIFEF.OID_DENOMINACION
     WHERE DIFEF.COD_NIVEL_DETALLE <> 'D' {0}
  GROUP BY DIFEF.OID_REMESA, DIFEF.OID_BULTO, DIFEF.OID_PARCIAL, DIFEF.OID_DIVISA, DIFEF.COD_NIVEL_DETALLE
  
UNION

-- SAPR_TDECLARADO_MEDIO_PAGO
SELECT
 'DECLARADO_MEDIO_PAGO' AS TIPO
,DECMP.OID_REMESA
,DECMP.OID_BULTO
,DECMP.OID_PARCIAL
,DECMP.OID_DIVISA
,DECMP.COD_NIVEL_DETALLE
,DECMP.OID_DECLARADO_MEDIO_PAGO AS OID_DEC_CONT_MEDIO_PAGO
,DECMP.COD_TIPO_MEDIO_PAGO
,SUM(DECMP.NUM_IMPORTE) AS IMPORTE
,SUM(DECMP.NEL_CANTIDAD) AS CANTIDAD

      FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
 LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
     WHERE DECMP.COD_NIVEL_DETALLE <> 'D' {0}
  GROUP BY DECMP.OID_REMESA, DECMP.OID_BULTO, DECMP.OID_PARCIAL, DECMP.OID_DIVISA,
           DECMP.COD_NIVEL_DETALLE, DECMP.OID_DECLARADO_MEDIO_PAGO, DECMP.COD_TIPO_MEDIO_PAGO

UNION

-- SAPR_TDIFERENCIA_MEDIO_PAGO
SELECT
 'DIFERENCIA_MEDIO_PAGO' AS TIPO
,DIFMP.OID_REMESA
,DIFMP.OID_BULTO
,DIFMP.OID_PARCIAL
,DIFMP.OID_DIVISA
,DIFMP.COD_NIVEL_DETALLE
,NULL AS OID_DEC_CONT_MEDIO_PAGO
,DIFMP.COD_TIPO_MEDIO_PAGO
,SUM(DIFMP.NUM_IMPORTE) AS IMPORTE
,SUM(DIFMP.NEL_CANTIDAD) AS CANTIDAD

      FROM SAPR_TDIFERENCIA_MEDIO_PAGO DIFMP
 LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = DIFMP.OID_MEDIO_PAGO
     WHERE DIFMP.COD_NIVEL_DETALLE <> 'D' {0}
  GROUP BY DIFMP.OID_REMESA, DIFMP.OID_BULTO, DIFMP.OID_PARCIAL, DIFMP.OID_DIVISA,
           DIFMP.COD_NIVEL_DETALLE, DIFMP.COD_TIPO_MEDIO_PAGO
  
  