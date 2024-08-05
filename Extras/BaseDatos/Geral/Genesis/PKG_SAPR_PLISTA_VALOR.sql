CREATE OR REPLACE PACKAGE SAPR_PLISTA_VALOR_###VERSION###
  AS
  /*Version: ###VERSION_COMP###*/

  PROCEDURE sins_tlista_valor_elem(par$oid_contenedor  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_remesa      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_lista_valor IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_lista_valor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_lista_tipo  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE sdel_tlista_valor_elem(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_
                                ,par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_
                                ,par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_
                                ,par$oid_lista_valor IN gepr_pcomon_###VERSION###.tipo$oid_
                                ,par$eliminar_hijos IN BOOLEAN
                                ,par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

END SAPR_PLISTA_VALOR_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PLISTA_VALOR_###VERSION### AS

  PROCEDURE sins_tlista_valor_elem(par$oid_contenedor  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_remesa      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_lista_valor IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_lista_valor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_lista_tipo  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_lista_tipo  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_lista_valor gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
    
    IF par$oid_lista_valor IS NULL THEN
      BEGIN

        SELECT LT.OID_LISTA_TIPO, LV.OID_LISTA_VALOR
          INTO var$oid_lista_tipo, var$oid_lista_valor
          FROM GEPR_TLISTA_TIPO LT
         INNER JOIN GEPR_TLISTA_VALOR LV
            ON LV.OID_LISTA_TIPO = LT.OID_LISTA_TIPO
         WHERE LT.COD_TIPO = par$cod_lista_tipo
           AND LV.COD_VALOR = par$cod_lista_valor;

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_lista_tipo := NULL;
      END;
    ELSE
      var$oid_lista_valor := par$oid_lista_valor;
      BEGIN
        SELECT LV.OID_LISTA_TIPO
            INTO var$oid_lista_tipo
            FROM GEPR_TLISTA_VALOR LV
           WHERE LV.OID_LISTA_VALOR = var$oid_lista_valor;

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_lista_tipo := NULL;
      END;
    END IF;

    IF var$oid_lista_tipo IS NOT NULL THEN

      INSERT INTO SAPR_TLISTA_VALORXELEMENTO
          (OID_LISTA_VALORXELEMENTO,
           OID_CONTENEDOR,
           OID_REMESA,
           OID_BULTO,
           OID_PARCIAL,
           OID_LISTA_TIPO,
           OID_LISTA_VALOR,
           GMT_CREACION,
           DES_USUARIO_CREACION,
           GMT_MODIFICACION,
           DES_USUARIO_MODIFICACION)
        VALUES
          (sys_guid(),
           par$oid_contenedor,
           par$oid_remesa,
           par$oid_bulto,
           par$oid_parcial,
           var$oid_lista_tipo,
           var$oid_lista_valor,
           var$gmt_zero,
           par$cod_usuario,
           var$gmt_zero,
           par$cod_usuario);           
      par$inserts := sql%rowcount;        
     
    ELSE
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_lista_tipo_invalida',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDoc,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  '',
                                                                  1));
    END IF;
    
  END sins_tlista_valor_elem;

  PROCEDURE sdel_tlista_valor_elem(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_
                                  ,par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_
                                  ,par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_
                                  ,par$oid_lista_valor IN gepr_pcomon_###VERSION###.tipo$oid_
                                  ,par$eliminar_hijos IN BOOLEAN
                                  ,par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    IF par$eliminar_hijos THEN

      IF par$oid_parcial IS NOT NULL THEN
         DELETE SAPR_TLISTA_VALORXELEMENTO WHERE (par$oid_lista_valor IS NOT NULL AND OID_LISTA_VALOR = par$oid_lista_valor OR par$oid_lista_valor IS NULL) AND OID_REMESA = par$oid_remesa AND OID_BULTO = par$oid_bulto AND OID_PARCIAL = par$oid_parcial;
         par$deletes := sql%rowcount;
      ELSIF par$oid_bulto IS NOT NULL THEN
         DELETE SAPR_TLISTA_VALORXELEMENTO WHERE (par$oid_lista_valor IS NOT NULL AND OID_LISTA_VALOR = par$oid_lista_valor OR par$oid_lista_valor IS NULL) AND OID_REMESA = par$oid_remesa AND OID_BULTO = par$oid_bulto;
         par$deletes := sql%rowcount;
      ELSE
         DELETE SAPR_TLISTA_VALORXELEMENTO WHERE (par$oid_lista_valor IS NOT NULL AND OID_LISTA_VALOR = par$oid_lista_valor OR par$oid_lista_valor IS NULL) AND OID_REMESA = par$oid_remesa;
         par$deletes := sql%rowcount;
      END IF;

    ELSE
      DELETE SAPR_TLISTA_VALORXELEMENTO
       WHERE OID_LISTA_VALOR = par$oid_lista_valor
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR par$oid_remesa IS NULL)
         AND (par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto OR
             par$oid_bulto IS NULL)
         AND (par$oid_parcial IS NOT NULL AND OID_PARCIAL = par$oid_parcial OR
             par$oid_parcial IS NULL);
      par$deletes := sql%rowcount;
    END IF;

  END sdel_tlista_valor_elem;

END SAPR_PLISTA_VALOR_###VERSION###;
/
