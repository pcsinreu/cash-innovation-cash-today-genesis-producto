SELECT
        FORM.OID_FORMULARIO, FORM.COD_FORMULARIO, FORM.OID_TIPO_DOCUMENTO
FROM
        SAPR_TFORMULARIO FORM
WHERE  
        FORM.OID_FORMULARIO IN (SELECT 
                                        CAFO.OID_FORMULARIO 
                                FROM 
                                        SAPR_TCARACTFORMXFORMULARIO CAFO
                                        INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
                                WHERE
                                        1 = 1
                                        {0}
                                GROUP BY
                                        CAFO.OID_FORMULARIO
                                HAVING
                                        COUNT(CAFO.OID_CARACT_FORMULARIO) = []CANTIDAD_CARACTERISTICAS -- CANTIDAD DE CARACTERISTICAS DEL FILTRO
                                )
        AND FORM.BOL_ACTIVO = 1
ORDER BY
        FORM.GMT_CREACION DESC