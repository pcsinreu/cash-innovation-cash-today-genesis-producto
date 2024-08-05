CREATE OR REPLACE PACKAGE SAPR_PBULTO_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pbulto_###VERSION###';

  /* sins_tbulto */
  PROCEDURE sins_tbulto(par$oid_bulto           IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_remesa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_externo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_iac             IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_iac_parciales   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_precinto        IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_bolsa           IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_banco_ingreso   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$bol_banco_ing_madre IN gepr_pcomon_###VERSION###.tipo$nbol_,
                        par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$est_,
                        par$usuario_resp        IN gepr_pcomon_###VERSION###.tipo$usr_,
                        par$puesto_resp         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$nel_cant_parciales  IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$fyh_conteo_inicio   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$fyh_conteo_fin      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$fyh_proceso_leg     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$oid_bulto_padre     IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_bulto_sub       IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_nivel_detalle   IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$bol_cuadrado        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                        par$oid_cuenta_saldo    IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$bol_gestion_bulto   IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                        par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sgrabar_bulto */
  PROCEDURE sgrabar_bulto(par$oid_bulto           IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_remesa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_externo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_iac             IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_iac_parciales   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_cuenta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto        IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_bolsa           IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$oid_banco_ingreso   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$bol_banco_ing_madre IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$est_,
                          par$usuario_resp        IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$puesto_resp         IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$nel_cant_parciales  IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$fyh_conteo_inicio   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_conteo_fin      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_proceso_leg     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_bulto_padre     IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto_sub       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_nivel_detalle   IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_cuadrado        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$oid_cuenta_saldo    IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_formato    IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_servicio   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$bol_gestion_bulto   IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_bultos */
  PROCEDURE scargar_tt_oids_bultos(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  FUNCTION fhay_docs_sectores_distintos(par$abulrem_oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$abulrem_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$cod_delegacion     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_planta         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  FUNCTION fvalidar_romper_precinto(par$abulrem_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abulrem_cod_precint_nuevo IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$abulrem_cod_sector        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* fregistro_existe */
  FUNCTION fregistro_existe(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* fvalidar_bulto */
  FUNCTION fvalidar_bulto(par$oid_cuenta   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$usuario      IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$cod_cultura  IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* Valida o estado do bulto */
  FUNCTION fvalidar_estado_bulto(par$oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_estado IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  FUNCTION frecupera_lista_valor(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo  IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  PROCEDURE sexcluir_bulto(par$abul_oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$abul_oid_documento        IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$abul_oid_tipo_formato     IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$abul_oid_tipo_servicio    IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_parcial          IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_oid_tipo_formato IN gepr_pcomon_###VERSION###.tipo$oids_);

  PROCEDURE sdel_tbulto(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE supd_tbulto(par$oid_bulto        in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                        par$updates          out gepr_pcomon_###VERSION###.tipo$nel_);

  procedure supd_tbulto_rowver(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$rowver   IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_funcionalidad in gepr_pcomon_###VERSION###.tipo$desc_);

END SAPR_PBULTO_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PBULTO_###VERSION### AS

  /* sins_tbulto */
  PROCEDURE sins_tbulto(par$oid_bulto           IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_remesa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_externo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_iac             IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_iac_parciales   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_precinto        IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_bolsa           IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_banco_ingreso   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$bol_banco_ing_madre IN gepr_pcomon_###VERSION###.tipo$nbol_,
                        par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$est_,
                        par$usuario_resp        IN gepr_pcomon_###VERSION###.tipo$usr_,
                        par$puesto_resp         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$nel_cant_parciales  IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$fyh_conteo_inicio   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$fyh_conteo_fin      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$fyh_proceso_leg     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$oid_bulto_padre     IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_bulto_sub       IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_nivel_detalle   IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$bol_cuadrado        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                        par$oid_cuenta_saldo    IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$bol_gestion_bulto   IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                        par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    INSERT INTO SAPR_TBULTO
      (OID_BULTO,
       OID_EXTERNO,
       OID_REMESA,
       OID_IAC,
       OID_IAC_PARCIALES,
       OID_DOCUMENTO,
       OID_BANCO_INGRESO,
       BOL_BANCO_INGRESO_ES_MADRE,
       COD_PRECINTO,
       COD_ESTADO,
       COD_USUARIO_RESPONSABLE,
       COD_PUESTO_RESPONSABLE,
       COD_BOLSA,
       NEL_CANTIDAD_PARCIALES,
       FYH_CONTEO_INICIO,
       FYH_CONTEO_FIN,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       OID_CUENTA,
       DES_USUARIO_MODIFICACION,
       OID_BULTO_PADRE,
       OID_BULTO_SUSTITUTO,
       FYH_PROCESO_LEGADO,
       BOL_CUADRADO,
       COD_NIVEL_DETALLE,
       OID_CUENTA_SALDO,
       COD_ESTADO_ABONO,
       BOL_GESTION_BULTO)
    VALUES
      (par$oid_bulto,
       par$oid_externo,
       par$oid_remesa,
       par$oid_iac,
       par$oid_iac_parciales,
       par$oid_documento,
       par$oid_banco_ingreso,
       par$bol_banco_ing_madre,
       par$cod_precinto,
       par$cod_estado,
       par$usuario_resp,
       par$puesto_resp,
       par$cod_bolsa,
       par$nel_cant_parciales,
       par$fyh_conteo_inicio,
       par$fyh_conteo_fin,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$oid_cuenta,
       par$usuario,
       par$oid_bulto_padre,
       par$oid_bulto_sub,
       par$fyh_proceso_leg,
       par$bol_cuadrado,
       par$cod_nivel_detalle,
       par$oid_cuenta_saldo,
       gepr_pcomon_###VERSION###.const$codEstAbonoENoAbonado,
       par$bol_gestion_bulto);
    par$inserts := sql%rowcount;
  END sins_tbulto;

  /* sgrabar_bulto */
  PROCEDURE sgrabar_bulto(par$oid_bulto           IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_remesa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_externo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_iac             IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_iac_parciales   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_cuenta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto        IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_bolsa           IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$oid_banco_ingreso   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$bol_banco_ing_madre IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$est_,
                          par$usuario_resp        IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$puesto_resp         IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$nel_cant_parciales  IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$fyh_conteo_inicio   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_conteo_fin      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_proceso_leg     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_bulto_padre     IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto_sub       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_nivel_detalle   IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_cuadrado        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$oid_cuenta_saldo    IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_formato    IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_servicio   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$bol_gestion_bulto   IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$oid_bulto gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
    par$inserts := 0;
  
    IF fvalidar_bulto(par$oid_cuenta,
                      par$cod_precinto,
                      par$usuario,
                      par$cod_cultura) THEN
    
      var$oid_bulto := par$oid_bulto;
      IF par$oid_bulto IS NULL THEN
        SELECT SYS_GUID() INTO var$oid_bulto FROM DUAL;
      END IF;
    
      --Insere Bulto
      sins_tbulto(var$oid_bulto,
                  par$oid_remesa,
                  par$oid_externo,
                  par$oid_iac,
                  par$oid_iac_parciales,
                  par$oid_documento,
                  par$oid_cuenta,
                  par$cod_precinto,
                  par$cod_bolsa,
                  par$oid_banco_ingreso,
                  par$bol_banco_ing_madre,
                  par$cod_estado,
                  par$usuario_resp,
                  par$puesto_resp,
                  par$nel_cant_parciales,
                  par$fyh_conteo_inicio,
                  par$fyh_conteo_fin,
                  par$fyh_proceso_leg,
                  par$oid_bulto_padre,
                  par$oid_bulto_sub,
                  par$cod_nivel_detalle,
                  par$bol_cuadrado,
                  par$oid_cuenta_saldo,
                  par$bol_gestion_bulto,
                  par$usuario,
                  par$inserts);
      par$inserts := par$inserts + sql%rowcount;
    
      /* Insere o formato do bulto */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                        par$oid_remesa      => par$oid_remesa,
                                                        par$oid_bulto       => par$oid_bulto,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => par$oid_tipo_formato,
                                                        par$cod_lista_valor => null,
                                                        par$cod_lista_tipo  => null,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => par$inserts);
      par$inserts := par$inserts + sql%rowcount;
    
      /* Insere o servicio do bulto */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                        par$oid_remesa      => par$oid_remesa,
                                                        par$oid_bulto       => par$oid_bulto,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => par$oid_tipo_servicio,
                                                        par$cod_lista_valor => null,
                                                        par$cod_lista_tipo  => null,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => par$inserts);
      par$inserts := par$inserts + sql%rowcount;
    
    END IF;
  END sgrabar_bulto;

  /* scargar_tt_oids_bultos */
  PROCEDURE scargar_tt_oids_bultos(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_DOCUMENTO
          FROM SAPR_GTT_TOIDS t
         WHERE COD_CALIFICADOR = 'DOCUMENTO')
      SELECT DISTINCT DE.OID_BULTO, 'BULTO'
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       INNER JOIN q
          ON q.OID_DOCUMENTO = DE.OID_DOCUMENTO
       WHERE DE.OID_BULTO IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_bultos;

  /* fregistro_existe */
  FUNCTION fregistro_existe(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
    var$cant INT;
  BEGIN
    SELECT COUNT(1)
      INTO var$cant
      FROM sapr_tbulto
     WHERE oid_bulto = par$oid_bulto;
    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
  END fregistro_existe;

  FUNCTION fhay_docs_sectores_distintos(par$abulrem_oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$abulrem_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$cod_delegacion     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_planta         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
  
    var$count_sec_dist gepr_pcomon_###VERSION###.tipo$cant_ := 0;
    var$respuesta      gepr_pcomon_###VERSION###.tipo$nbol_;
  
  BEGIN
  
    FOR idx IN par$abulrem_oid_bulto.first .. par$abulrem_oid_bulto.last LOOP
      BEGIN
      
        SELECT COUNT(1)
          INTO var$count_sec_dist
          FROM GEPR_TSECTOR S
         WHERE S.COD_SECTOR = par$abulrem_cod_sector(idx)
           AND S.OID_SECTOR_PADRE IS NOT NULL;
      
        par$selects := par$selects + sql%rowcount;
      
        IF var$count_sec_dist > 0 THEN
        
          var$count_sec_dist := 0;
        
          SELECT COUNT(1) EXISTE
            INTO var$count_sec_dist
            FROM (SELECT COUNT(1) EXISTE
                    FROM SAPR_TREMESA R
                   INNER JOIN SAPR_TBULTO B
                      ON B.OID_REMESA = R.OID_REMESA
                   INNER JOIN SAPR_TCUENTA C
                      ON C.OID_CUENTA = B.OID_CUENTA
                   INNER JOIN GEPR_TSECTOR S
                      ON S.OID_SECTOR = C.OID_SECTOR
                   WHERE B.OID_EXTERNO = par$abulrem_oid_bulto(idx)
                     AND S.OID_SECTOR_PADRE =
                         (SELECT SP.OID_SECTOR
                            FROM GEPR_TSECTOR S
                           INNER JOIN GEPR_TPLANTA PLA
                              ON PLA.OID_PLANTA = S.OID_PLANTA
                           INNER JOIN GEPR_TDELEGACION DEL
                              ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
                            LEFT JOIN GEPR_TSECTOR SP
                              ON S.OID_SECTOR_PADRE = SP.OID_SECTOR
                           WHERE DEL.COD_DELEGACION = par$cod_delegacion
                             AND PLA.COD_PLANTA = par$cod_planta
                             AND S.COD_SECTOR = par$abulrem_cod_sector(idx))
                  UNION
                  SELECT COUNT(1) EXISTE
                    FROM SAPR_TREMESA R
                   INNER JOIN SAPR_TBULTO B
                      ON B.OID_REMESA = R.OID_REMESA
                   INNER JOIN SAPR_TCUENTA C
                      ON C.OID_CUENTA = B.OID_CUENTA
                   INNER JOIN GEPR_TSECTOR S
                      ON S.OID_SECTOR = C.OID_SECTOR
                   WHERE B.OID_EXTERNO = par$abulrem_oid_bulto(idx)
                     AND S.OID_SECTOR_PADRE IS NULL
                     AND S.OID_SECTOR =
                         (SELECT S.OID_SECTOR
                            FROM GEPR_TSECTOR S
                           INNER JOIN GEPR_TPLANTA PLA
                              ON PLA.OID_PLANTA = S.OID_PLANTA
                           INNER JOIN GEPR_TDELEGACION DEL
                              ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
                           WHERE DEL.COD_DELEGACION = par$cod_delegacion
                             AND PLA.COD_PLANTA = par$cod_planta
                             AND S.COD_SECTOR = par$abulrem_cod_sector(idx)
                             AND S.OID_SECTOR_PADRE IS NULL))
           WHERE EXISTE > 0;
        
          par$selects := par$selects + sql%rowcount;
        
          IF var$count_sec_dist > 0 THEN
            var$respuesta := 0;
          ELSE
            var$respuesta := 1;
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$codcultura        => par$cod_cultura,
                                                                        par$codchave          => gepr_pcomon_###VERSION###.const$codExpBultoSecDistin,
                                                                        par$codfuncionalidad  => gepr_pcomon_###VERSION###.const$CodFunActualizarPrecint,
                                                                        par$codaplicacion     => gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        par$colparametros     => null,
                                                                        par$bolmarcarinitexto => 1));
            EXIT;
          END IF;
        
        ELSE
        
          /* estão no mesmo setor */
          var$respuesta := 0;
          EXIT;
        
        END IF;
      
      END;
    
    END LOOP;
  
    RETURN var$respuesta;
  
  END fhay_docs_sectores_distintos;

  FUNCTION fvalidar_romper_precinto(par$abulrem_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abulrem_cod_precint_nuevo IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$abulrem_cod_sector        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
  
    -- armazena as mensagens de validacao
    var$mensajes_validacion gepr_pcomon_###VERSION###.tipo$obs_;
  
  BEGIN
  
    var$mensajes_validacion := '#';
  
    IF gepr_putilidades_###VERSION###.farray_vacio_oid(par$abulrem_oid_bulto) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Lista de bultos',
                                                                     0) ||
                                 chr(10);
    END IF;
  
    IF gepr_putilidades_###VERSION###.farray_vacio_cod(par$abulrem_cod_precint_nuevo) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Lista de nuevos precintos',
                                                                     0) ||
                                 chr(10);
    
    END IF;
  
    IF gepr_putilidades_###VERSION###.farray_vacio_cod(par$abulrem_cod_sector) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Lista de sectores',
                                                                     0) ||
                                 chr(10);
    
    END IF;
  
    var$mensajes_validacion := var$mensajes_validacion || '#';
  
    -- se houver alguma mensagem na vari?vel, dispara o RAISE
    IF (length(var$mensajes_validacion) > 2) THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              var$mensajes_validacion);
      RETURN FALSE;
    END IF;
  
    RETURN TRUE;
  
  END fvalidar_romper_precinto;

  /* fvalidar_bulto */
  FUNCTION fvalidar_bulto(par$oid_cuenta   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$usuario      IN gepr_pcomon_###VERSION###.tipo$usr_
                          
                          /* cultura/idioma */,
                          par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
  BEGIN
    IF par$oid_cuenta IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Cuenta',
                                                                  0));
      RETURN False;
    END IF;
  
    IF par$cod_precinto IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Precinto',
                                                                  0));
      RETURN False;
    END IF;
  
    IF par$usuario IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Usuario',
                                                                  0));
      RETURN False;
    END IF;
  
    RETURN True;
  END fvalidar_bulto;

  /* Valida o estado do bulto */
  FUNCTION fvalidar_estado_bulto(par$oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_estado IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$retorno gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN
  
    BEGIN
    
      SELECT 1
        INTO var$retorno
        FROM SAPR_TBULTO B
       WHERE B.OID_BULTO = par$oid_bulto
         and B.COD_ESTADO = par$cod_estado;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$retorno := NULL;
    END;
  
    return var$retorno;
  end fvalidar_estado_bulto;

  FUNCTION frecupera_lista_valor(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo  IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$retorno gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    BEGIN
    
      SELECT LV.OID_LISTA_VALOR
        INTO var$retorno
        FROM SAPR_TLISTA_VALORXELEMENTO VE
       INNER JOIN GEPR_TLISTA_VALOR LV
          ON VE.OID_LISTA_VALOR = LV.OID_LISTA_VALOR
       INNER JOIN GEPR_TLISTA_TIPO LT
          ON LT.OID_LISTA_TIPO = VE.OID_LISTA_TIPO
       WHERE LT.COD_TIPO = par$cod_tipo
         AND VE.OID_BULTO = par$oid_bulto
         AND VE.OID_PARCIAL IS NULL;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$retorno := NULL;
    END;
  
    return var$retorno;
  end frecupera_lista_valor;

  PROCEDURE sexcluir_bulto(par$abul_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$abul_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                           
                           par$abul_oid_tipo_formato     IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$abul_oid_tipo_servicio    IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_parcial          IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$apar_oid_oid_tipo_formato IN gepr_pcomon_###VERSION###.tipo$oids_) IS
    par$deletes gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN
  
    -- Excluir as parciais
    -- TO DO
    /*
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$apar_oid_parcial) THEN
    
    END IF;
    */
  
    FOR idx IN par$abul_oid_bulto.first .. par$abul_oid_bulto.last LOOP
      -- Exclui o formato do bulto
      -- TO DO
    
      -- Exclui o Servicio do bulto
      -- TO DO
    
      -- Exclui o bulto
      sdel_tbulto(NULL, par$abul_oid_bulto(idx), par$deletes);
    END LOOP;
    RETURN;
  END sexcluir_bulto;

  PROCEDURE sdel_tbulto(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE FROM SAPR_TBULTO WHERE OID_BULTO = par$oid_bulto;
  
    IF par$oid_bulto IS NOT NULL THEN
      DELETE SAPR_TBULTO WHERE OID_BULTO = par$oid_bulto;
      par$deletes := sql%rowcount;
    ELSE
      DELETE SAPR_TBULTO WHERE OID_REMESA = par$oid_remesa;
      par$deletes := sql%rowcount;
    END IF;
  
  END sdel_tbulto;

  PROCEDURE supd_tbulto(par$oid_bulto        in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                        par$updates          out gepr_pcomon_###VERSION###.tipo$nel_) as
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    update sapr_tbulto b
       set b.cod_estado               = nvl(par$cod_estado, b.cod_estado),
           b.oid_documento            = nvl(par$oid_documento,
                                            b.oid_documento),
           b.oid_cuenta               = nvl(par$oid_cuenta_mov, b.oid_cuenta),
           b.oid_cuenta_saldo         = nvl(par$oid_cuenta_saldo,
                                            b.oid_cuenta_saldo),
           b.gmt_modificacion         = var$gmt_zero,
           b.des_usuario_modificacion = par$cod_usuario
     where b.oid_bulto = par$oid_bulto;
    par$updates := sql%rowcount;
  END supd_tbulto;

  procedure supd_tbulto_rowver(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$rowver   IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_funcionalidad in gepr_pcomon_###VERSION###.tipo$desc_)as
   PRAGMA AUTONOMOUS_TRANSACTION;                                        
   var$rowver gepr_pcomon_###VERSION###.tipo$nel_ := NVL(par$rowver,0) + 1;
   var$cod_precinto varchar2(100);
  begin
    
    update sapr_tbulto b
    set b.rowver = b.rowver
    where b.oid_bulto =par$oid_bulto
    and NVL(b.rowver,0) = NVL(par$rowver,0);
    
    if sql%rowcount >0 then
      par$rowver :=var$rowver;
    else
      begin
        select b.cod_precinto
        into var$cod_precinto
        from sapr_tbulto b where b.oid_bulto =par$oid_bulto;
        
        /*recuperar o precinto para exibir na mensagem*/
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                              'bulto_modificado',
                                                              'SAPR_PBULTO.supd_tbulto_rowver.'||par$cod_funcionalidad,
                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                              var$cod_precinto,
                                                              1));
       exception   
         when no_data_found then
           null;
       end;                                                     
    end if;  
    
    commit; 
  end supd_tbulto_rowver;  

END SAPR_PBULTO_###VERSION###;
/
