SELECT
        FORM.OID_FORMULARIO
FROM
        SAPR_TFORMULARIO FORM
WHERE  
        FORM.OID_FORMULARIO IN (SELECT 
                                        CAFO.OID_FORMULARIO 
                                FROM 
                                        SAPR_TCARACTFORMXFORMULARIO CAFO
                                        INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
                                WHERE
                                        -- GESTIÓN FONDOS, MOVIMIENTO DE FONDOS, ENTRE SECTORES MISMA PLANTA E INTEGRACION SALIDAS
                                        {0}
                                GROUP BY
                                        CAFO.OID_FORMULARIO
                                HAVING
                                        COUNT(CAFO.OID_CARACT_FORMULARIO) = []CANTIDAD_CARACTERISTICAS -- CANTIDAD DE CARACTERISTICAS DEL FILTRO
                                ) -- SOMENTE FORMULÁRIOS COM AS DEVIDAS CARACTERÍSTICAS FILTRADAS
        AND FORM.BOL_ACTIVO = 1 -- SOMENTE SE O FORMULÁRIO ESTIVER ATIVO    
ORDER BY
        FORM.GMT_CREACION DESC