﻿WITH DATOS AS (
    SELECT
      DATB.OID_DATO_BANCARIO,
      DATB.OID_BANCO,
      BAN.COD_CLIENTE COD_BANCO,
      BAN.DES_CLIENTE DES_BANCO,
      DATB.OID_CLIENTE,
      DATB.OID_SUBCLIENTE,
      DATB.OID_PTO_SERVICIO,
      DATB.OID_DIVISA,
      DIV.DES_DIVISA,
      DIV.COD_ISO_DIVISA,
      DATB.COD_TIPO_CUENTA_BANCARIA,
      DATB.COD_CUENTA_BANCARIA,
      DATB.COD_DOCUMENTO,
      DATB.DES_TITULARIDAD,
      DATB.DES_OBSERVACIONES,
      DATB.BOL_DEFECTO,
      DATB.COD_AGENCIA,
      DATB.DES_CAMPO_ADICIONAL_1,
      DATB.DES_CAMPO_ADICIONAL_2,
      DATB.DES_CAMPO_ADICIONAL_3,
      DATB.DES_CAMPO_ADICIONAL_4,
      DATB.DES_CAMPO_ADICIONAL_5,
      DATB.DES_CAMPO_ADICIONAL_6,
      DATB.DES_CAMPO_ADICIONAL_7,
      DATB.DES_CAMPO_ADICIONAL_8
    FROM SAPR_TDATO_BANCARIO DATB
      INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = DATB.OID_DIVISA
      INNER JOIN GEPR_TCLIENTE BAN ON BAN.OID_CLIENTE = DATB.OID_BANCO
    WHERE DATB.BOL_ACTIVO = 1 ),
CAMBIO AS (
    SELECT DBC.OID_DATO_BANCARIO, COUNT(1) PENDIENTE
    FROM SAPR_TDATO_BANCARIO_CAMBIO DBC
    INNER JOIN DATOS D ON D.OID_DATO_BANCARIO = DBC.OID_DATO_BANCARIO
    WHERE COD_ESTADO='PD' AND BOL_ACTIVO = 1 
    GROUP BY DBC.OID_DATO_BANCARIO
)
SELECT DATB.OID_DATO_BANCARIO,
      DATB.OID_BANCO,
      DATB.COD_BANCO,
      DATB.DES_BANCO,
      DATB.OID_CLIENTE,
      DATB.OID_SUBCLIENTE,
      DATB.OID_PTO_SERVICIO,
      DATB.OID_DIVISA,
      DATB.DES_DIVISA,
      DATB.COD_ISO_DIVISA,
      DATB.COD_TIPO_CUENTA_BANCARIA,
      DATB.COD_CUENTA_BANCARIA,
      DATB.COD_DOCUMENTO,
      DATB.DES_TITULARIDAD,
      DATB.DES_OBSERVACIONES,
      DATB.BOL_DEFECTO,
      DATB.COD_AGENCIA,
      DATB.DES_CAMPO_ADICIONAL_1,
      DATB.DES_CAMPO_ADICIONAL_2,
      DATB.DES_CAMPO_ADICIONAL_3,
      DATB.DES_CAMPO_ADICIONAL_4,
      DATB.DES_CAMPO_ADICIONAL_5,
      DATB.DES_CAMPO_ADICIONAL_6,
      DATB.DES_CAMPO_ADICIONAL_7,
      DATB.DES_CAMPO_ADICIONAL_8,
      CASE WHEN  C.PENDIENTE > 0 THEN 1 ELSE 0 END  PENDIENTE  
      FROM DATOS DATB LEFT JOIN CAMBIO C ON DATB.OID_DATO_BANCARIO = C.OID_DATO_BANCARIO
      WHERE 1=1