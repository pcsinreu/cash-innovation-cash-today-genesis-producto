﻿WITH SALDO_ANTERIOR AS
 (SELECT SALDO_ANTERIOR.OID_DIVISA,
         NVL(SALDO_ANTERIOR.OID_DENOMINACION, '-') AS OID_DENOMINACION,
         NVL(SALDO_ANTERIOR.OID_UNIDAD_MEDIDA, '-') AS OID_UNIDAD_MEDIDA,
         NVL(SALDO_ANTERIOR.OID_CALIDAD, '-') AS OID_CALIDAD,
         NVL(SALDO_ANTERIOR.COD_NIVEL_DETALLE, '-') AS COD_NIVEL_DETALLE,
         SUM(SALDO_ANTERIOR.NUM_IMPORTE) AS NUM_IMPORTE
    FROM SAPR_TEFECTIVO_ANTERIORXDOC SALDO_ANTERIOR
   WHERE SALDO_ANTERIOR.OID_DOCUMENTO =[]OID_DOCUMENTO         
   GROUP BY SALDO_ANTERIOR.OID_DIVISA,
            SALDO_ANTERIOR.OID_DENOMINACION,
            SALDO_ANTERIOR.OID_UNIDAD_MEDIDA,
            SALDO_ANTERIOR.OID_CALIDAD,
            SALDO_ANTERIOR.COD_NIVEL_DETALLE),
SALDO_ACTUAL AS
 (SELECT SALDO_ACTUAL.OID_DIVISA,
         NVL(SALDO_ACTUAL.OID_DENOMINACION, '-') AS OID_DENOMINACION,
         NVL(SALDO_ACTUAL.OID_UNIDAD_MEDIDA, '-') AS OID_UNIDAD_MEDIDA,
         NVL(SALDO_ACTUAL.OID_CALIDAD, '-') AS OID_CALIDAD,
         NVL(SALDO_ACTUAL.COD_NIVEL_DETALLE, '-') AS COD_NIVEL_DETALLE,
         SUM(SALDO_ACTUAL.NUM_IMPORTE) AS NUM_IMPORTE
    FROM SALDO_ANTERIOR
   INNER JOIN SAPR_TSALDO_EFECTIVO SALDO_ACTUAL
      ON SALDO_ACTUAL.OID_DIVISA = SALDO_ANTERIOR.OID_DIVISA
     AND NVL(SALDO_ACTUAL.OID_DENOMINACION, '-') =
         NVL(SALDO_ANTERIOR.OID_DENOMINACION, '-')
     AND NVL(SALDO_ACTUAL.OID_UNIDAD_MEDIDA, '-') =
         NVL(SALDO_ANTERIOR.OID_UNIDAD_MEDIDA, '-')
     AND NVL(SALDO_ACTUAL.OID_CALIDAD, '-') =
         NVL(SALDO_ANTERIOR.OID_CALIDAD, '-')
     AND NVL(SALDO_ACTUAL.COD_NIVEL_DETALLE, '-') =
         NVL(SALDO_ANTERIOR.COD_NIVEL_DETALLE, '-')
     AND SALDO_ACTUAL.OID_CUENTA_SALDO =[]OID_CUENTA_SALDO
     AND SALDO_ACTUAL.BOL_DISPONIBLE = 1
     AND SALDO_ACTUAL.NUM_IMPORTE <> 0
   GROUP BY SALDO_ACTUAL.OID_DIVISA,
            SALDO_ACTUAL.OID_DENOMINACION,
            SALDO_ACTUAL.OID_UNIDAD_MEDIDA,
            SALDO_ACTUAL.OID_CALIDAD,
            SALDO_ACTUAL.COD_NIVEL_DETALLE)
SELECT DISTINCT DIV.DES_DIVISA,
                CASE
                  WHEN SALDO_ANTERIOR.NUM_IMPORTE <>
                       SALDO_ACTUAL.NUM_IMPORTE THEN
                   1
                  ELSE
                   0
                END BOL_DIFERENCIA
  FROM SALDO_ANTERIOR
 INNER JOIN SALDO_ACTUAL
    ON SALDO_ACTUAL.OID_DIVISA = SALDO_ANTERIOR.OID_DIVISA
   AND SALDO_ACTUAL.OID_DENOMINACION = SALDO_ANTERIOR.OID_DENOMINACION
   AND SALDO_ACTUAL.OID_UNIDAD_MEDIDA = SALDO_ANTERIOR.OID_UNIDAD_MEDIDA
   AND SALDO_ACTUAL.OID_CALIDAD = SALDO_ANTERIOR.OID_CALIDAD
   AND SALDO_ACTUAL.COD_NIVEL_DETALLE = SALDO_ANTERIOR.COD_NIVEL_DETALLE
 INNER JOIN GEPR_TDIVISA DIV
    ON DIV.OID_DIVISA = SALDO_ANTERIOR.OID_DIVISA