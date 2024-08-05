CREATE OR REPLACE PACKAGE sapr_pnotificacion_###VERSION### IS
  /*Version: ###VERSION_COMP###*/

  const$ID_SOBRE             VARCHAR2(20) := 'ID_SOBRE';
  const$NUMERO_DEPOSITO      VARCHAR2(20) := 'NUMERO_DEPOSITO';
  const$ID_OT         VARCHAR2(20) := 'ID_OT';
  const$ID_REMESA         VARCHAR2(20) := 'ID_REMESA';
  const$COD_SECTOR         VARCHAR2(20) := 'COD_SECTOR';
  const$COD_DELEGACION         VARCHAR2(20) := 'COD_DELEGACION';
  const$TP_MENS_SOBRE_RIPLEY gepr_pcomon_###VERSION###.tipo$desc_ := 'SobreRipley';
  const$TP_MENS_DEPOS_RIPLEY gepr_pcomon_###VERSION###.tipo$desc_ := 'DepositoRipley';
  const$TP_MENS_SAL_ENV_REM_LV gepr_pcomon_###VERSION###.tipo$desc_ := 'Salidas_Enviar_Remesa_LV';

  PROCEDURE srecibir_mensaje_externo(par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_mensaje              IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$mensaje                  IN gepr_pcomon_###VERSION###.tipo$obss_,
                                     par$tipo_mensaje             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$oid_msg_criterio         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$cod_criterio             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$val_criterio             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_notificaciones_ok    OUT SYS_REFCURSOR,
                                     par$oid_notificacion_error  OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$tipo_notificacion_error OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$notificacion_error      OUT gepr_pcomon_###VERSION###.tipo$obs_);

  FUNCTION fverifica_criterio(par$cod_criterio    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$lista_criterios IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$lista_valores   IN gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN gepr_pcomon_###VERSION###.tipo$desc_;

  PROCEDURE sins_notificacion(par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_tipo_notificacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$obs_notificacion     IN gepr_pcomon_###VERSION###.tipo$obs_,
                              par$obs_parametros       IN gepr_pcomon_###VERSION###.tipo$obs_,
                              par$cod_tipo_destino      IN gepr_pcomon_###VERSION###.tipo$obss_,
                              par$oid_destino          IN gepr_pcomon_###VERSION###.tipo$obss_,
                              par$usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$oid_notificacion     OUT gepr_pcomon_###VERSION###.tipo$oid_);

  PROCEDURE sgrabar_notificacion_leido(par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$oid_destino_notificacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$leido                    IN INTEGER,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_);

END sapr_pnotificacion_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pnotificacion_###VERSION### IS

  PROCEDURE srecibir_mensaje_externo(par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_mensaje              IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$mensaje                  IN gepr_pcomon_###VERSION###.tipo$obss_,
                                     par$tipo_mensaje             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$oid_msg_criterio         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$cod_criterio             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$val_criterio             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_notificaciones_ok    OUT SYS_REFCURSOR,
                                     par$oid_notificacion_error  OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$tipo_notificacion_error OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$notificacion_error      OUT gepr_pcomon_###VERSION###.tipo$obs_) IS

    var$oid_remesa                 gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parcial                gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_precinto               gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_registro_mov           gepr_pcomon_###VERSION###.tipo$oid_;
    var$valor_criterio             gepr_pcomon_###VERSION###.tipo$desc_;
    var$rc_datos_sobre_ripley      SYS_REFCURSOR;
    var$obs_parametros             gepr_pcomon_###VERSION###.tipo$obs_;
    var$cod_tipo_destino           gepr_pcomon_###VERSION###.tipo$obss_;
    var$oid_destino                gepr_pcomon_###VERSION###.tipo$obss_;
    var$oid_documento              gepr_pcomon_###VERSION###.tipo$oid_;
    var$rc_dt_deposito_ripley      SYS_REFCURSOR;
    var$cod_delegacion             gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_delegacion_dest        gepr_pcomon_###VERSION###.tipo$cod_;
    var$ParcialConteoTipoDestNotif gepr_pcomon_###VERSION###.tipo$desc_;
    var$ParcialConteoIdDestNotif   gepr_pcomon_###VERSION###.tipo$desc_;
    var$RemesaSalidasLVTipoDestN gepr_pcomon_###VERSION###.tipo$desc_;
    var$RemesaSalidasLVIdentDestN gepr_pcomon_###VERSION###.tipo$desc_;
    var$DocumentoTipoDestNotif     gepr_pcomon_###VERSION###.tipo$desc_;
    var$DocumentoIdDestNotif       gepr_pcomon_###VERSION###.tipo$desc_;
    var$identificador              gepr_pcomon_###VERSION###.tipo$oid_;
    var$rc_identificadores         SYS_REFCURSOR;
    var$indice                     INTEGER;
    var$cod_criterio               gepr_pcomon_###VERSION###.tipo$cods_;
    var$val_criterio               gepr_pcomon_###VERSION###.tipo$descs_;
    var$oid_notificacion_ok        gepr_pcomon_###VERSION###.tipo$oid_;
    err_code                       NUMBER;
    err_msg                        VARCHAR2(4000);
    var$oid_notificacion_error     VARCHAR2(32767);
    var$tipo_notificacion_error    VARCHAR2(32767);
    var$notificacion_error         VARCHAR2(32767);
    var$oid_ot                 gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_sector                 gepr_pcomon_###VERSION###.tipo$cod_;

  BEGIN

    --Validacoes
    IF par$usuario IS NULL THEN
      RAISE_APPLICATION_ERROR(-20008,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MSG_VALIDAR_USUARIO',
                                                                    'spRecibirMensajeExterno',
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    0));
    END IF;
    IF gepr_putilidades_###VERSION###.farray_vacio_cod(par$cod_criterio) THEN
      RAISE_APPLICATION_ERROR(-20003,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MSG_VALIDAR_CRITERIO',
                                                                    'spRecibirMensajeExterno',
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    0));
    END IF;
    IF gepr_putilidades_###VERSION###.farray_vacio_desc(par$val_criterio) THEN
      RAISE_APPLICATION_ERROR(-20004,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MSG_VALIDAR_CRITERIO',
                                                                    'spRecibirMensajeExterno',
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    0));
    END IF;

    DELETE SAPR_GTT_TOIDS;

    for idx in par$mensaje.FIRST .. par$mensaje.LAST LOOP
      BEGIN
        --Validacoes
        IF par$mensaje(idx) IS NULL THEN
          RAISE_APPLICATION_ERROR(-20001,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'MSG_VALIDAR_MENSAJE',
                                                                        'spRecibirMensajeExterno',
                                                                        'GenesisSaldos',
                                                                        NULL,
                                                                        0));
        END IF;
        IF par$tipo_mensaje(idx) IS NULL THEN
          RAISE_APPLICATION_ERROR(-20002,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'MSG_VALIDAR_TP_MENSAJE',
                                                                        'spRecibirMensajeExterno',
                                                                        'GenesisSaldos',
                                                                        NULL,
                                                                        0));
        END IF;

        var$cod_criterio := gepr_putilidades_###VERSION###.fObtenerItemEntidadPadreCods(par$oid_msg_criterio,
                                                                                    par$cod_criterio,
                                                                                    par$oid_mensaje(idx));
        var$val_criterio := gepr_putilidades_###VERSION###.fObtenerItemEntPadreDescs(par$oid_msg_criterio,
                                                                                 par$val_criterio,
                                                                                 par$oid_mensaje(idx));

        var$oid_notificacion_ok := NULL;
        --Sobre Ripley
        IF par$tipo_mensaje(idx) = const$TP_MENS_SOBRE_RIPLEY THEN

          --Verifica criterio ID_SOBRE
          var$valor_criterio := fverifica_criterio(const$ID_SOBRE,
                                                   var$cod_criterio,
                                                   var$val_criterio);
          IF var$valor_criterio IS NULL THEN
            RAISE_APPLICATION_ERROR(-20005,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                          'MSG_VALIDAR_ID_SOBRE',
                                                                          'spRecibirMensajeExterno',
                                                                          'GenesisSaldos',
                                                                          NULL,
                                                                          0));
          END IF;

          --Valida se encontrou
          IF var$oid_registro_mov IS NULL THEN
            RAISE_APPLICATION_ERROR(-20010,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                          'MSG_VALIDAR_ID_SOBRE_NOTFOUND',
                                                                          'spRecibirMensajeExterno',
                                                                          'GenesisSaldos',
                                                                          NULL,
                                                                          0));
          END IF;

          var$obs_parametros := var$oid_remesa || '|' || var$cod_precinto || '|' ||
                               var$oid_parcial;

          var$ParcialConteoTipoDestNotif := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                           null,
                                                                                           'ParcialConteoTipoDestNotif',
                                                                                           'Genesis');
          var$ParcialConteoIdDestNotif   := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                           null,
                                                                                           'ParcialConteoIdentificadorDestNotif',
                                                                                           'Genesis');

          --Separa os identificadores e coloca num cursor
          var$rc_identificadores := gepr_putilidades_###VERSION###.fSplit(var$ParcialConteoIdDestNotif, ',');
          var$indice             := 0;

          LOOP
            FETCH var$rc_identificadores
              INTO var$identificador;
            EXIT WHEN var$rc_identificadores%NOTFOUND;

            IF var$ParcialConteoTipoDestNotif = 'U' THEN
              var$cod_tipo_destino(var$indice) := 'USUARIO';
              var$oid_destino(var$indice) := SAPR_PUSUARIO_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'USUARIO'));
              END IF;
            END IF;
            IF var$ParcialConteoTipoDestNotif = 'S' THEN
              var$cod_tipo_destino(var$indice) := 'SECTOR';
              var$oid_destino(var$indice) := SAPR_PSECTOR_###VERSION###.frecuperar_identificador(var$identificador,
                                                                                            var$cod_delegacion);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'SECTOR'));
              END IF;
            END IF;
            IF var$ParcialConteoTipoDestNotif = 'TSXP' THEN
              var$cod_tipo_destino(var$indice) := 'TIPO_SECTOR';
              var$oid_destino(var$indice) := var$identificador;
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'TIPO_SECTOR'));
              END IF;
            END IF;
            IF var$ParcialConteoTipoDestNotif = 'P' THEN
              var$cod_tipo_destino(var$indice) := 'PLANTA';
              var$oid_destino(var$indice) := SAPR_PPLANTA_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'PLANTA'));
              END IF;
            END IF;
            IF var$ParcialConteoTipoDestNotif = 'D' THEN
              var$cod_tipo_destino(var$indice) := 'DELEGACION';
              var$oid_destino(var$indice) := SAPR_PDELEGACION_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'DELEGACION'));
              END IF;
            END IF;

            var$indice := var$indice + 1;
          END LOOP;
          CLOSE var$rc_identificadores;

          --Insere Notificacao
          sins_notificacion(par$cod_cultura,
                                  'PARCIAL_CONTEO',
                                  REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_NOTIF_SOBRE_RIPLEY',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0),
                                          '{0}',
                                          var$valor_criterio),
                                  var$obs_parametros,
                                  var$cod_tipo_destino,
                                  var$oid_destino,
                                  par$usuario,
                                  var$oid_notificacion_ok);

          -- Registra o ID inserido
          INSERT INTO SAPR_GTT_TOIDS
            (OID, COD_CALIFICADOR)
          VALUES
            (var$oid_notificacion_ok, par$tipo_mensaje(idx));

        END IF;

        --Deposito Ripley
        IF par$tipo_mensaje(idx) = const$TP_MENS_DEPOS_RIPLEY THEN

          --Verifica criterio NUMERO_DEPOSITO
          var$valor_criterio := fverifica_criterio(const$NUMERO_DEPOSITO,
                                                   var$cod_criterio,
                                                   var$val_criterio);
          IF var$valor_criterio IS NULL THEN
            RAISE_APPLICATION_ERROR(-20007,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                          'MSG_VALIDAR_NUMERO_DEPOSITO',
                                                                          'spRecibirMensajeExterno',
                                                                          'GenesisSaldos',
                                                                          NULL,
                                                                          0));
          END IF;

          --Recupera dados deposito ripley
          sapr_pdocumento_###VERSION###.srecupera_dt_deposito_ripley(par$cod_cultura,
                                                                var$valor_criterio,
                                                                var$rc_dt_deposito_ripley);

          LOOP
            FETCH var$rc_dt_deposito_ripley
              INTO var$oid_documento,
                   var$cod_delegacion,
                   var$cod_delegacion_dest;
            EXIT WHEN var$rc_dt_deposito_ripley%NOTFOUND;

            --Valida se encontrou mais de 1 registro
            IF var$rc_dt_deposito_ripley%ROWCOUNT > 1 THEN
              RAISE_APPLICATION_ERROR(-20009,
                                      gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                            'MSG_VALIDAR_ID_DEPOSITO_VARIOS',
                                                                            'spRecibirMensajeExterno',
                                                                            'GenesisSaldos',
                                                                            NULL,
                                                                            0));
            END IF;

            --Atualiza campo DES_MENSAJE_EXTERNO na tabela GEPR_TDOCUMENTO
            sapr_pdocumento_###VERSION###.sactualiza_desc_mensaje(par$cod_cultura,
                                                             var$oid_documento,
                                                             par$mensaje(idx));

          END LOOP;
          CLOSE var$rc_dt_deposito_ripley;

          --Valida se encontrou
          IF var$oid_documento IS NULL THEN
            RAISE_APPLICATION_ERROR(-20011,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                          'MSG_VALIDAR_ID_DEPOSITO_NOTFOUND',
                                                                          'spRecibirMensajeExterno',
                                                                          'GenesisSaldos',
                                                                          NULL,
                                                                          0));
          END IF;

          var$obs_parametros := var$oid_documento;

          --Envia Notificação para a delegação de origem
          var$DocumentoTipoDestNotif := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                       null,
                                                                                       'DocumentoTipoDestNotif',
                                                                                       'Genesis');
          var$DocumentoIdDestNotif   := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                       null,
                                                                                       'DocumentoIdentificadorDestNotif',
                                                                                       'Genesis');

          --Separa os identificadores e coloca num cursor
          var$rc_identificadores := gepr_putilidades_###VERSION###.fsplit(var$DocumentoIdDestNotif, ',');
          var$indice            := 0;

          LOOP
            FETCH var$rc_identificadores
              INTO var$identificador;
            EXIT WHEN var$rc_identificadores%NOTFOUND;

            IF var$DocumentoTipoDestNotif = 'U' THEN
              var$cod_tipo_destino(var$indice) := 'USUARIO';
              var$oid_destino(var$indice) := SAPR_PUSUARIO_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'USUARIO'));
              END IF;
            END IF;
            IF var$DocumentoTipoDestNotif = 'S' THEN
              var$cod_tipo_destino(var$indice) := 'SECTOR';
              var$oid_destino(var$indice) := SAPR_PSECTOR_###VERSION###.frecuperar_identificador(var$identificador,
                                                                                           var$cod_delegacion);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'SECTOR'));
              END IF;
            END IF;
            IF var$DocumentoTipoDestNotif = 'TSXP' THEN
              var$cod_tipo_destino(var$indice) := 'TIPO_SECTOR';
              var$oid_destino(var$indice) := var$identificador;
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'TIPO_SECTOR'));
              END IF;
            END IF;
            IF var$DocumentoTipoDestNotif = 'P' THEN
              var$cod_tipo_destino(var$indice) := 'PLANTA';
              var$oid_destino(var$indice) := SAPR_PPLANTA_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'PLANTA'));
              END IF;
            END IF;
            IF var$DocumentoTipoDestNotif = 'D' THEN
              var$cod_tipo_destino(var$indice) := 'DELEGACION';
              var$oid_destino(var$indice) := SAPR_PDELEGACION_###VERSION###.frecuperar_identificador(var$identificador);
              IF var$oid_destino(var$indice) IS NULL THEN
                RAISE_APPLICATION_ERROR(-20012,
                                        REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'MSG_DESTINO_N_ENC',
                                                                                              'spRecibirMensajeExterno',
                                                                                              'GenesisSaldos',
                                                                                              NULL,
                                                                                              0),
                                                        '{0}',
                                                        var$identificador),
                                                '{1}',
                                                'DELEGACION'));
              END IF;
            END IF;

            var$indice := var$indice + 1;
          END LOOP;
          CLOSE var$rc_identificadores;

          --Insere Notificacao
          sins_notificacion(par$cod_cultura,
                                  'DOCUMENTO',
                                  REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_NOTIF_DEPOSITO_RIPLEY',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0),
                                          '{0}',
                                          var$valor_criterio),
                                  var$obs_parametros,
                                  var$cod_tipo_destino,
                                  var$oid_destino,
                                  par$usuario,
                                  var$oid_notificacion_ok);
          -- Registra o ID inserido
          INSERT INTO SAPR_GTT_TOIDS
            (OID, COD_CALIFICADOR)
          VALUES
            (var$oid_notificacion_ok, par$tipo_mensaje(idx));

          --Envia Notificação para a delegação de destino  se for diferente
          IF var$cod_delegacion <> var$cod_delegacion_dest THEN

            var$DocumentoTipoDestNotif := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                         null,
                                                                                         'DocumentoTipoDestNotif',
                                                                                         'Genesis');
            var$DocumentoIdDestNotif   := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                         null,
                                                                                         'DocumentoIdentificadorDestNotif',
                                                                                         'Genesis');

            --Separa os identificadores e coloca num cursor
            var$rc_identificadores := gepr_putilidades_###VERSION###.fsplit(var$DocumentoIdDestNotif,
                                                                       ',');
            var$indice            := 0;

            LOOP
              FETCH var$rc_identificadores
                INTO var$identificador;
              EXIT WHEN var$rc_identificadores%NOTFOUND;

              IF var$DocumentoTipoDestNotif = 'U' THEN
                var$cod_tipo_destino(var$indice) := 'USUARIO';
                var$oid_destino(var$indice) := SAPR_PUSUARIO_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'USUARIO'));
                END IF;
              END IF;
              IF var$DocumentoTipoDestNotif = 'S' THEN
                var$cod_tipo_destino(var$indice) := 'SECTOR';
                var$oid_destino(var$indice) := SAPR_PSECTOR_###VERSION###.frecuperar_identificador(var$identificador,
                                                                                             var$cod_delegacion);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'SECTOR'));
                END IF;
              END IF;
              IF var$DocumentoTipoDestNotif = 'TSXP' THEN
                var$cod_tipo_destino(var$indice) := 'TIPO_SECTOR';
                var$oid_destino(var$indice) := var$identificador;
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'TIPO_SECTOR'));
                END IF;
              END IF;
              IF var$DocumentoTipoDestNotif = 'P' THEN
                var$cod_tipo_destino(var$indice) := 'PLANTA';
                var$oid_destino(var$indice) := SAPR_PPLANTA_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'PLANTA'));
                END IF;
              END IF;
              IF var$DocumentoTipoDestNotif = 'D' THEN
                var$cod_tipo_destino(var$indice) := 'DELEGACION';
                var$oid_destino(var$indice) := SAPR_PDELEGACION_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'DELEGACION'));
                END IF;
              END IF;

              var$indice := var$indice + 1;
            END LOOP;
            CLOSE var$rc_identificadores;

            --Insere Notificacao
            sins_notificacion(par$cod_cultura,
                                    'DOCUMENTO',
                                    REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                  'MSG_NOTIF_DEPOSITO_RIPLEY',
                                                                                  'spRecibirMensajeExterno',
                                                                                  'GenesisSaldos',
                                                                                  NULL,
                                                                                  0),
                                            '{0}',
                                            var$valor_criterio),
                                    var$obs_parametros,
                                    var$cod_tipo_destino,
                                    var$oid_destino,
                                    par$usuario,
                                    var$oid_notificacion_ok);

            -- Registra o ID inserido
            INSERT INTO SAPR_GTT_TOIDS
              (OID, COD_CALIFICADOR)
            VALUES
              (var$oid_notificacion_ok, par$tipo_mensaje(idx));

          END IF;
        END IF;
               
        --Salidas_Enviar_Remesa_LV
        IF par$tipo_mensaje(idx) = const$TP_MENS_SAL_ENV_REM_LV THEN
           
            --Verifica criterio ID_OT
            var$oid_ot := fverifica_criterio(const$ID_OT,
                                                       var$cod_criterio,
                                                       var$val_criterio);
            IF var$oid_ot IS NULL THEN
              RAISE_APPLICATION_ERROR(-20007,
                                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_VALIDAR_ID_OT',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0));
            END IF;
              
            --Verifica criterio ID_REMESA
            var$oid_remesa := fverifica_criterio(const$ID_REMESA,
                                                       var$cod_criterio,
                                                       var$val_criterio);
            IF var$oid_remesa IS NULL THEN
              RAISE_APPLICATION_ERROR(-20007,
                                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_VALIDAR_ID_REMESA',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0));
            END IF;
              
            --Verifica criterio COD_SECTOR
            var$cod_sector := fverifica_criterio(const$COD_SECTOR,
                                                       var$cod_criterio,
                                                       var$val_criterio);
            IF var$cod_sector IS NULL THEN
              RAISE_APPLICATION_ERROR(-20007,
                                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_VALIDAR_COD_SECTOR',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0));	
             END IF;
             
             --Verifica criterio COD_DELEGACION
            var$cod_delegacion := fverifica_criterio(const$COD_DELEGACION,
                                                       var$cod_criterio,
                                                       var$val_criterio);
            IF var$cod_delegacion IS NULL THEN
              RAISE_APPLICATION_ERROR(-20007,
                                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                'MSG_VALIDAR_COD_DELEGACION',
                                                                                'spRecibirMensajeExterno',
                                                                                'GenesisSaldos',
                                                                                NULL,
                                                                                0));	
             END IF;

          var$obs_parametros := var$oid_ot || '|' || var$oid_remesa;

          var$RemesaSalidasLVTipoDestN := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                           null,
                                                                                           'RemesaSalidasLVTipoDestNotif',
                                                                                           'Genesis');
          var$RemesaSalidasLVIdentDestN   := gepr_putilidades_###VERSION###.frecuperar_parametro(var$cod_delegacion,
                                                                                           null,
                                                                                           'RemesaSalidasLVIdentDestNotif',
                                                                                           'Genesis');
                                                                                             
          IF var$RemesaSalidasLVTipoDestN IS NOT NULL AND var$RemesaSalidasLVIdentDestN IS NOT NULL THEN                                                                       
            --Separa os identificadores e coloca num cursor
            var$rc_identificadores := gepr_putilidades_###VERSION###.fSplit(var$RemesaSalidasLVIdentDestN, ',');
            var$indice             := 0;

            LOOP
              FETCH var$rc_identificadores
                INTO var$identificador;
              EXIT WHEN var$rc_identificadores%NOTFOUND;

              IF var$RemesaSalidasLVTipoDestN = 'U' THEN
                var$cod_tipo_destino(var$indice) := 'USUARIO';
                var$oid_destino(var$indice) := SAPR_PUSUARIO_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'USUARIO'));
                END IF;
              END IF;
              IF var$RemesaSalidasLVTipoDestN = 'S' THEN
                var$cod_tipo_destino(var$indice) := 'SECTOR';
                var$oid_destino(var$indice) := SAPR_PSECTOR_###VERSION###.frecuperar_identificador(var$identificador,
                                                                                              var$cod_delegacion);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'SECTOR'));
                END IF;
              END IF;
              IF var$RemesaSalidasLVTipoDestN = 'TSXP' THEN
                var$cod_tipo_destino(var$indice) := 'TIPO_SECTOR';
                var$oid_destino(var$indice) := var$identificador;
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'TIPO_SECTOR'));
                END IF;
              END IF;
              IF var$RemesaSalidasLVTipoDestN = 'P' THEN
                var$cod_tipo_destino(var$indice) := 'PLANTA';
                var$oid_destino(var$indice) := SAPR_PPLANTA_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'PLANTA'));
                END IF;
              END IF;
              IF var$RemesaSalidasLVTipoDestN = 'D' THEN
                var$cod_tipo_destino(var$indice) := 'DELEGACION';
                var$oid_destino(var$indice) := SAPR_PDELEGACION_###VERSION###.frecuperar_identificador(var$identificador);
                IF var$oid_destino(var$indice) IS NULL THEN
                  RAISE_APPLICATION_ERROR(-20012,
                                          REPLACE(REPLACE(gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                'MSG_DESTINO_N_ENC',
                                                                                                'spRecibirMensajeExterno',
                                                                                                'GenesisSaldos',
                                                                                                NULL,
                                                                                                0),
                                                          '{0}',
                                                          var$identificador),
                                                  '{1}',
                                                  'DELEGACION'));
          END IF;
        END IF;

              var$indice := var$indice + 1;
            END LOOP;
            CLOSE var$rc_identificadores;
          ELSE
            var$cod_tipo_destino(0) := 'SECTOR';
            var$oid_destino(0) := SAPR_PSECTOR_###VERSION###.frecuperar_identificador(var$cod_sector,
                                                                                 var$cod_delegacion);          END IF;

          --Insere Notificacao
          sins_notificacion(par$cod_cultura,
                                  'ENV_REMESA_LV',
                                  par$mensaje(idx),
                                  var$obs_parametros,
                                  var$cod_tipo_destino,
                                  var$oid_destino,
                                  par$usuario,
                                  var$oid_notificacion_ok);

          -- Registra o ID inserido
          INSERT INTO SAPR_GTT_TOIDS
            (OID, COD_CALIFICADOR)
          VALUES
            (var$oid_notificacion_ok, par$tipo_mensaje(idx));
              
      END IF;
    
      EXCEPTION
        WHEN OTHERS THEN
          err_code := SQLCODE;
          err_msg  := SUBSTR(SQLERRM, 1, 4000);

          IF var$oid_notificacion_error IS NULL THEN
            var$oid_notificacion_error := var$oid_notificacion_ok;
          ELSE
            var$oid_notificacion_error := var$oid_notificacion_error || ';' ||
                                       var$oid_notificacion_ok;
          END IF;

          IF var$tipo_notificacion_error IS NULL THEN
            var$tipo_notificacion_error := par$tipo_mensaje(idx);
          ELSE
            var$tipo_notificacion_error := var$tipo_notificacion_error || ';' ||
                                         par$tipo_mensaje(idx);
          END IF;

          IF var$notificacion_error IS NULL THEN
            var$notificacion_error := err_msg;
          ELSE
            var$notificacion_error := var$notificacion_error || ';' ||
                                     err_msg;
          END IF;
          
          ROLLBACK;

      END;

    END LOOP;

    OPEN par$rc_notificaciones_ok FOR
      SELECT TOID.OID AS ID, TOID.COD_CALIFICADOR AS TIPO
        FROM SAPR_GTT_TOIDS TOID;

    par$oid_notificacion_error   := var$oid_notificacion_error;
    par$tipo_notificacion_error := var$tipo_notificacion_error;
    par$notificacion_error     := var$notificacion_error;

  END srecibir_mensaje_externo;

  FUNCTION fverifica_criterio(par$cod_criterio    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$lista_criterios IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$lista_valores   IN gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN gepr_pcomon_###VERSION###.tipo$desc_ IS
  BEGIN
    for idx in par$lista_criterios.FIRST .. par$lista_criterios.LAST LOOP
      if par$lista_criterios(idx) = par$cod_criterio Then
        RETURN par$lista_valores(idx);
      end if;
    END LOOP;

    RETURN NULL;
  END fverifica_criterio;

  PROCEDURE sins_notificacion(par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_tipo_notificacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$obs_notificacion     IN gepr_pcomon_###VERSION###.tipo$obs_,
                              par$obs_parametros       IN gepr_pcomon_###VERSION###.tipo$obs_,
                              par$cod_tipo_destino      IN gepr_pcomon_###VERSION###.tipo$obss_,
                              par$oid_destino          IN gepr_pcomon_###VERSION###.tipo$obss_,
                              par$usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$oid_notificacion     OUT gepr_pcomon_###VERSION###.tipo$oid_) IS

    var$oid_tipo_notificacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$existe               NUMBER;
    var$oid_notificacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_usuario           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_tipo_sectorxplanta gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planta            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_delegacion        gepr_pcomon_###VERSION###.tipo$oid_;
    err_code                       NUMBER;
    err_msg                        VARCHAR2(4000);

  BEGIN
  BEGIN
      --Validacoes
      IF par$cod_tipo_notificacion IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MSG_VALIDAR_COD_TIPO_NOTIFICACION',
                                                                      'sins_notificacion',
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      0));
      ELSE
        --Verifica se o tipo da notificacao existe
        SELECT COUNT(0)
          INTO var$existe
          FROM GEPR_TTIPO_NOTIFICACION TN
         WHERE TN.COD_TIPO_NOTIFICACION = par$cod_tipo_notificacion AND TN.BOL_ACTIVO = 1;

        IF var$existe = 0 THEN
          RAISE_APPLICATION_ERROR(-20006,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'MSG_VALIDAR_COD_TIPO_NOTIFICACION',
                                                                        'sins_notificacion',
                                                                        'GenesisSaldos',
                                                                        NULL,
                                                                        0));
        ELSE
          SELECT TN.OID_TIPO_NOTIFICACION
            INTO var$oid_tipo_notificacion
            FROM GEPR_TTIPO_NOTIFICACION TN
           WHERE TN.COD_TIPO_NOTIFICACION = par$cod_tipo_notificacion;
        END IF;

        --valida parametro
        IF par$obs_parametros IS NOT NULL THEN
          SELECT COUNT(0)
            INTO var$existe
            FROM GEPR_TTIPO_NOTIFICACION TN
           WHERE TN.OID_TIPO_NOTIFICACION = var$oid_tipo_notificacion
             AND TN.BOL_EVENTO_RELACIONADO = 1;

          IF var$existe = 0 THEN
            RAISE_APPLICATION_ERROR(-20007,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                          'MSG_VALIDAR_PARAMETRO',
                                                                          'sins_notificacion',
                                                                          'GenesisSaldos',
                                                                          NULL,
                                                                          0));
          END IF;
        END IF;

      END IF;
      IF par$obs_notificacion IS NULL THEN
        RAISE_APPLICATION_ERROR(-20002,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MSG_VALIDAR_OBS_NOTIFICACION',
                                                                      'sins_notificacion',
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      0));
      END IF;
      IF gepr_putilidades_###VERSION###.farray_vacio_obs(par$cod_tipo_destino) THEN
        RAISE_APPLICATION_ERROR(-20004,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MSG_VALIDAR_DESTINO',
                                                                      'sins_notificacion',
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      0));
      END IF;
      IF gepr_putilidades_###VERSION###.farray_vacio_obs(par$oid_destino) THEN
        RAISE_APPLICATION_ERROR(-20005,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MSG_VALIDAR_DESTINO',
                                                                      'sins_notificacion',
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      0));
      END IF;

      var$oid_notificacion := SYS_GUID();

      INSERT INTO GEPR_TNOTIFICACION
        (oid_notificacion,
         oid_tipo_notificacion,
         obs_notificacion,
         obs_parametros,
         bol_activo,
         gmt_creacion,
         des_usuario_creacion,
         gmt_modificacion,
         des_usuario_modificacion)
      VALUES
        (var$oid_notificacion,
         var$oid_tipo_notificacion,
         par$obs_notificacion,
         par$obs_parametros,
         1,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         FN_GMT_ZERO_###VERSION###,
         par$usuario);

      for idx in par$cod_tipo_destino.FIRST .. par$cod_tipo_destino.LAST LOOP
        if par$cod_tipo_destino(idx) is not null Then
          var$oid_usuario           := NULL;
          var$oid_sector            := NULL;
          var$oid_tipo_sectorxplanta := NULL;
          var$oid_planta            := NULL;
          var$oid_delegacion        := NULL;

          IF par$cod_tipo_destino(idx) = 'USUARIO' THEN
            var$oid_usuario := par$oid_destino(idx);
          END IF;

          IF par$cod_tipo_destino(idx) = 'SECTOR' THEN
            var$oid_sector := par$oid_destino(idx);
          END IF;

          IF par$cod_tipo_destino(idx) = 'TIPO_SECTOR' THEN
            var$oid_tipo_sectorxplanta := par$oid_destino(idx);
          END IF;

          IF par$cod_tipo_destino(idx) = 'PLANTA' THEN
            var$oid_planta := par$oid_destino(idx);
          END IF;

          IF par$cod_tipo_destino(idx) = 'DELEGACION' THEN
            var$oid_delegacion := par$oid_destino(idx);
          END IF;

          INSERT INTO GEPR_TDESTINO_NOTIFICACION
            (oid_destino_notificacion,
             oid_notificacion,
             oid_usuario,
             oid_sector,
             oid_tipo_sectorxplanta,
             oid_planta,
             oid_delegacion,
             gmt_creacion,
             des_usuario_creacion,
             gmt_modificacion,
             des_usuario_modificacion,
             bol_leida)
          VALUES
            (SYS_GUID(),
             var$oid_notificacion,
             var$oid_usuario,
             var$oid_sector,
             var$oid_tipo_sectorxplanta,
             var$oid_planta,
             var$oid_delegacion,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             0);

        end if;
      END LOOP;

      par$oid_notificacion := var$oid_notificacion;
   EXCEPTION
        WHEN OTHERS THEN
          par$oid_notificacion := NULL;
          err_code := SQLCODE;
          err_msg  := SUBSTR(SQLERRM, 1, 4000);
          
          ROLLBACK;
          RAISE_APPLICATION_ERROR(-20008,err_msg);
   END;      
  END sins_notificacion;

  PROCEDURE sgrabar_notificacion_leido(par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$oid_destino_notificacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$leido                    IN INTEGER,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_) IS

  BEGIN

    IF gepr_putilidades_###VERSION###.farray_vacio_oid(par$oid_destino_notificacion) THEN
      RAISE_APPLICATION_ERROR(-20005,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MSG_VALIDAR_ID_DESTINO',
                                                                    'sp_grabarNotificacionLeido',
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    0));
    END IF;

    for idx in par$oid_destino_notificacion.FIRST .. par$oid_destino_notificacion.LAST LOOP
      if par$oid_destino_notificacion(idx) IS NOT NULL Then
        UPDATE GEPR_TDESTINO_NOTIFICACION
           SET BOL_LEIDA                = par$leido,
               DES_USUARIO_MODIFICACION = par$usuario,
               GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###
         WHERE OID_DESTINO_NOTIFICACION = par$oid_destino_notificacion(idx);

        UPDATE GEPR_TNOTIFICACION N
           SET N.DES_USUARIO_MODIFICACION = par$usuario,
         GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###
         WHERE N.OID_NOTIFICACION =
               (SELECT D.OID_NOTIFICACION
                  FROM GEPR_TDESTINO_NOTIFICACION D
                 WHERE D.OID_DESTINO_NOTIFICACION =
                       par$oid_destino_notificacion(idx)
                   AND ROWNUM = 1);

      end if;
    END LOOP;

  END sgrabar_notificacion_leido;

END sapr_pnotificacion_###VERSION###;
/
