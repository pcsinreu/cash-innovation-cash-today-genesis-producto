     SELECT COUNT(*) AS QTDE
          FROM (SELECT P.OID_PERIODO
                  FROM SAPR_TPERIODO P
                 INNER JOIN SAPR_TESTADO_PERIODO E
                    ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
                 INNER JOIN SAPR_TCALCULO_EFECTIVO C
                    ON P.OID_PERIODO = C.OID_PERIODO
                 WHERE E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL {0}
                
                UNION ALL
                SELECT P.OID_PERIODO
                  FROM SAPR_TPERIODO P
                 INNER JOIN SAPR_TESTADO_PERIODO E
                    ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
                 INNER JOIN SAPR_TCALCULO_MEDIO_PAGO C
                    ON P.OID_PERIODO = C.OID_PERIODO
                 WHERE E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL  {0})