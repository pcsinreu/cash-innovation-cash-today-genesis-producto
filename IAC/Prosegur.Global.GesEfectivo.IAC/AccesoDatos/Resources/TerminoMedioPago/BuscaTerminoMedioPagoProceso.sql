SELECT 
        TMP.COD_TERMINO, 
        TMP.DES_TERMINO,
        TMPPR.BOL_ES_OBLIGATORIO
FROM    
        GEPR_TTERMINO_MEDIO_PAGO TMP
        INNER JOIN GEPR_TTERMINO_MED_PAGO_POR_PRO TMPPR ON TMP.OID_TERMINO = TMPPR.OID_TERMINO
WHERE   TMP.OID_MEDIO_PAGO = []OID_MEDIO_PAGO AND
        TMPPR.OID_PROCESO_SUBCANAL = []OID_PROCESO_SUBCANAL
