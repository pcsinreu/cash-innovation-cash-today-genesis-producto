SELECT DIVI.COD_ISO_DIVISA, DIVI.DES_DIVISA FROM SAPR_TPLANXDIVISA PXD
            INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = PXD.OID_DIVISA
            WHERE
              PXD.BOL_ACTIVO = 1 AND PXD.OID_PLANIFICACION = []OID_PLANIFICACION