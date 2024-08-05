SELECT
                      NULL OID_DOCUMENTO,
                      DOCU.OID_FORMULARIO,
                      NULL OID_CUENTA_ORIGEN,
                      NULL OID_CUENTA_DESTINO,
                      NULL OID_CUENTA_SALDO_ORIGEN,
                      NULL OID_CUENTA_SALDO_DESTINO,
                      NULL OID_DOCUMENTO_PADRE,
                      NULL OID_DOCUMENTO_SUSTITUTO,
                      NULL OID_TIPO_DOCUMENTO,
                      DOCU.OID_SECTOR_ORIGEN,
                      DOCU.OID_SECTOR_DESTINO,
                      NULL OID_MOVIMENTACION_FONDO,
                      DOCU.OID_GRUPO_DOCUMENTO,
                      NULL FYH_PLAN_CERTIFICACION,
                      NULL FYH_GESTION,
                      NULL BOL_CERTIFICADO,
                      NULL COD_EXTERNO,
                      DOCU.COD_ESTADO,
                      DOCU.GMT_CREACION,
                      DOCU.DES_USUARIO_CREACION,
                      DOCU.GMT_MODIFICACION,
                      DOCU.DES_USUARIO_MODIFICACION,
                      DOCU.COD_COMPROBANTE,
                      DOCU.OID_GRUPO_DOCUMENTO
              FROM
                      SAPR_TGRUPO_DOCUMENTO DOCU                      
                      INNER JOIN GEPR_TSECTOR SEOR ON DOCU.OID_SECTOR_ORIGEN = SEOR.OID_SECTOR
                      INNER JOIN GEPR_TPLANTA PLOR ON SEOR.OID_PLANTA = PLOR.OID_PLANTA
                      INNER JOIN GEPR_TDELEGACION DELOR ON DELOR.OID_DELEGACION = PLOR.OID_DELEGACION
                      INNER JOIN GEPR_TSECTOR SEDE ON DOCU.OID_SECTOR_DESTINO = SEDE.OID_SECTOR        
                      INNER JOIN GEPR_TPLANTA PLDE ON SEDE.OID_PLANTA = PLDE.OID_PLANTA
                      INNER JOIN GEPR_TDELEGACION DELDE ON DELDE.OID_DELEGACION = PLDE.OID_DELEGACION                
              WHERE
					  1 = 1
                      {0} 
                      AND (
                      (
                        0 = 0
                        {1}             
                      )
                      OR
                      ( 
                        0 = 0      
                        {2}             
                      )
                    )  