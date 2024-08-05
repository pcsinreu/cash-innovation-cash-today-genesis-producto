		SELECT
				   MPG.OID_MEDIO_PAGO,
                   MPG.COD_MEDIO_PAGO,
                   MPG.DES_MEDIO_PAGO,
                   MPG.OBS_MEDIO_PAGO,
				   MPG.BOL_MERCANCIA,
                   MPG.BOL_VIGENTE,
                   MPG.COD_TIPO_MEDIO_PAGO,
				   MPG.COD_ACCESO,
                   DIV.COD_ISO_DIVISA,
                   DIV.DES_DIVISA
        FROM 
                   GEPR_TMEDIO_PAGO MPG
        INNER JOIN GEPR_TDIVISA DIV ON
                   DIV.oid_divisa=MPG.oid_divisa