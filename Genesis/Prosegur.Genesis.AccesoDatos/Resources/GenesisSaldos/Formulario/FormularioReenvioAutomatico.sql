  SELECT 
  FORM.OID_FORMULARIO
  ,FORM.COD_FORMULARIO
  ,FORM.DES_FORMULARIO
  ,FORM.cod_color
  ,FORM.cod_color
  ,FORM.BIN_ICONO_FORMULARIO
  ,FORM.BOL_ACTIVO
  ,FORM.GMT_CREACION
  ,FORM.GMT_MODIFICACION
  ,FORM.DES_USUARIO_CREACION
  ,FORM.DES_USUARIO_MODIFICACION
  ,FORM.OID_ACCION_CONTABLE
  FROM
    SAPR_TFORMULARIO FORM
  WHERE
          FORM.OID_FORMULARIO IN (SELECT 
                                          CAFO.OID_FORMULARIO 
                                  FROM 
                                          SAPR_TCARACTFORMXFORMULARIO CAFO
                                          INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
                                  WHERE
                                          -- FORMULARIOS DE GESTIÓN Remesas/Bultos - com reenvio automático
                                          CARA.COD_CARACT_FORMULARIO IN ('CARACTERISTICA_PRINCIPAL_GESTION_REMESAS', 'REENVIO_AUTOMATICO')
                                          OR CARA.COD_CARACT_FORMULARIO IN ('CARACTERISTICA_PRINCIPAL_GESTION_BULTOS', 'REENVIO_AUTOMATICO')
                                  GROUP BY
                                          CAFO.OID_FORMULARIO
                                  HAVING
                                          COUNT(CAFO.OID_CARACT_FORMULARIO) = 2 -- CANTIDAD DE CARACTERISTICAS DEL FILTRO
                                  )
