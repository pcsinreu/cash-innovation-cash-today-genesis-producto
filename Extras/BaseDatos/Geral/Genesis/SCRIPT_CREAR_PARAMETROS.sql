DECLARE
var$oid_grupo gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_app gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_nivel gepr_pcomon_###VERSION###.tipo$oid_;
BEGIN
     /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_app
            from    gepr_Taplicacion
            where   cod_aplicacion = 'Genesis';
        exception
            when no_data_found then
            var$oid_app := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| 'Genesis');
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = '1';
        exception
            when no_data_found then
            var$oid_nivel := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| '1');
        end;

      begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_app
            and     oid_nivel_parametro = var$oid_nivel
            and     DES_DESCRIPCION_CORTO = 'Configuración Switch';
        exception
            when no_data_found then
              var$oid_grupo:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo, var$oid_nivel, var$oid_app, 'Configuración Switch', 'Configuración Switch', 1, 'GENESIS_INSTALL', SYSDATE, 'PARAMETROS_CONFIGURAR_GENESIS', 'CFAP_GNS_IAC');
        END;
        UPDATE GEPR_TPARAMETRO SET OID_GRUPO_PARAMETRO = var$oid_grupo WHERE
        COD_PARAMETRO IN ('URLReintentosEnviosFVOaSwitch', 'ReintentosMaximosEnviosFVOaSwitch', 'EnviarDatosSwitchDocumento',
        'EnviarDatosSwitchSinFechaValor', 'EnviarDatosSwitchTipoPlanificacion') 
        AND OID_GRUPO_PARAMETRO <> var$oid_grupo;
END;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRecuperarSaldosAcuerdo';
    var$des_corta_param := 'Loguea recuperar saldos acuerdo.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea recuperar saldos acuerdo.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRelacionarMovimientosPeriodos';
    var$des_corta_param := 'Loguea relacionar movimientos periodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea relacionar movimientos periodos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioActualizarPeriodos';
    var$des_corta_param := 'Loguea Actualizar Periodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea Actualizar Periodos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioConfirmarPeriodos';
    var$des_corta_param := 'Loguea confirmar períodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea confirmar períodos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioReconfirmarPeriodos';
    var$des_corta_param := 'Loguea reconfirmar períodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea reconfirmar períodos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRecuperarSaldosPeriodos';
    var$des_corta_param := 'Loguea recuperar saldos por periodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea recuperar saldos por periodos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 14;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioOnlineNotificationEventOp';
    var$des_corta_param := 'Loguea servicio de notificaciones de eventos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de notificaciones de eventos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 15;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NiloCredencial';
    var$des_corta_param := 'Credencial de Nilo.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Credencial de Nilo.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 4;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración Nilo';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NiloURLAutenticacion';
    var$des_corta_param := 'URL de autentificacion con nilo.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL de autentificacion con nilo.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 4;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración Nilo';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NiloURLConsultaAcuerdosServicio';
    var$des_corta_param := 'URL para consulta de acuerdos servicio.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL para consulta de acuerdos servicio.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 4;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración Nilo';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioActualizarFechaCalculos';
    var$des_corta_param := 'Loguea servicio de Actualizar Fecha Calculos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de Actualizar Fecha Calculos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioActualizarMovimientos';
    var$des_corta_param := 'Loguea servicio de actualizar movimientos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de actualizar movimientos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioMarcarMovimientos';
    var$des_corta_param := 'Loguea servicio de marcar movimientos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de marcar movimientos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioActualizarSaldosAcuerdos';
    var$des_corta_param := 'Loguea servicio de actualizar saldos acuerdo.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de actualizar saldos acuerdo.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioActualizarSaldosHistorico';
    var$des_corta_param := 'Loguea servicio de actualizar saldos historico.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de actualizar saldos historico.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioConfigurarAcuerdosServicio';
    var$des_corta_param := 'Loguea servicio de configurar acuerdos servicio.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de configurar acuerdos servicio.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRecuperarSaldosHistorico';
    var$des_corta_param := 'Loguea servicio de recuperar saldos historico.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de recuperar saldos historico.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioConfigurarClientes';
    var$des_corta_param := 'Loguea servicio de configurar clientes.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de configurar clientes';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioIntegracionEnvioMovimientos';
    var$des_corta_param := 'Loguea Integración de EnvioMovimientos a Switch.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea Integración de EnvioMovimientos a Switch';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioIntegracionNotificacion';
    var$des_corta_param := 'Loguea Integración de Notificaciones.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea Integración de Notificaciones enviadas al Middleware de Notificaciones (API Comercial Global)';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioConfigurarMAE';
    var$des_corta_param := 'Loguea servicio de Configurar MAE.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de Configurar MAE.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioModificarMovimientos';
    var$des_corta_param := 'Loguea servicio de Modificar Movimientos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de Modificar Movimientos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioEnviarDocumentos';
    var$des_corta_param := 'Loguea servicio de enviar documentos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de enviar documentos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalURLNotificacion';
    var$des_corta_param := 'URL notificación APIGlobalURLNotificacion.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL notificación APIGlobalURLNotificacion.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_cantidad   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es numerico*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalCantidadMaximaIntentos';
    var$des_corta_param := 'Máxima cantidad de intentos APIGlobalCantidadMaximaIntentos.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Máxima cantidad de intentos APIGlobalCantidadMaximaIntentos.';
    var$valor_cantidad := '5';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 2;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_cantidad, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalURLAutenticacion';
    var$des_corta_param := 'URL Autenticacion de APIGlobal.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL Autenticacion de APIGlobal.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 3;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalURLAutenticacionScope';
    var$des_corta_param := 'Scope de APIGlobal.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Scope de APIGlobal.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 4;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalURLAutenticacionClientSecret';
    var$des_corta_param := 'ClientSecret de APIGlobal.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'ClientSecret de APIGlobal.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 5;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'APIGlobalURLAutenticacionClientId';
    var$des_corta_param := 'ClientId de APIGlobal.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'ClientId de APIGlobal.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 6;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración API Global';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
DECLARE
  var$existe NUMBER;
BEGIN

  SELECT COUNT(1) INTO var$existe
  FROM GEPR_TGRUPO_PARAMETRO
  WHERE DES_DESCRIPCION_CORTO = 'Configuración Nilo';

  IF var$existe = 0 THEN
    UPDATE GEPR_TGRUPO_PARAMETRO SET DES_DESCRIPCION_CORTO = 'Configuración Nilo' , DES_DESCRIPCION_LARGA = 'Configuración Nilo'
    WHERE DES_DESCRIPCION_CORTO = 'Datos Integración';
  END IF;

  SELECT COUNT(1) INTO var$existe
  FROM GEPR_TGRUPO_PARAMETRO
  WHERE DES_DESCRIPCION_CORTO = 'Configuración API Global';

  IF var$existe = 0 THEN
    UPDATE GEPR_TGRUPO_PARAMETRO SET DES_DESCRIPCION_CORTO = 'Configuración API Global' , DES_DESCRIPCION_LARGA = 'Configuración API Global'
    WHERE DES_DESCRIPCION_CORTO = 'Configuración Middleware';
  END IF;

  SELECT COUNT(1) INTO var$existe
  FROM GEPR_TGRUPO_PARAMETRO
  WHERE DES_DESCRIPCION_CORTO = 'Configuración API Global';

  IF var$existe = 1 THEN  
    UPDATE GEPR_TPARAMETRO SET OID_GRUPO_PARAMETRO = (SELECT OID_GRUPO_PARAMETRO FROM GEPR_TGRUPO_PARAMETRO WHERE DES_DESCRIPCION_CORTO = 'Configuración API Global') 
    WHERE  COD_PARAMETRO IN ('APIGlobalURLNotificacion','APIGlobalCantidadMaximaIntentos');
  END IF;
 

  DELETE FROM GEPR_TPARAMETRO_VALOR WHERE OID_PARAMETRO IN (SELECT OID_PARAMETRO FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO IN ('CodigoProcesoSincronizacionCodigoAjeno','CodigoProcesoSincronizacionCertificado'));
  DELETE FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO IN ('CodigoProcesoSincronizacionCodigoAjeno','CodigoProcesoSincronizacionCertificado');
END;
/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRecuperarMAEsPlanificadas';
    var$des_corta_param := 'Loguea servicio de recuperar MAEs planificadas.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de recuperar MAEs planificadas.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'cantidadDeDiasFacturacion';
    var$des_corta_param := 'Cantidad de días que busca referencias de acuerdos/fechas'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Cantidad de días que busca referencias de acuerdos/fechas desde hoy. Por defecto se considera un mes.';
    var$valor_defecto := '30';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1; /*Es texto*/
    var$des_grupo := 'Configuración Facturación';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$num_reintento_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

   var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es numerico*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'FechaValorConfirmacionMaxReintentosPeriodos';
    var$des_corta_param := 'Máximo de reintentos de envíos de confirmación por períodos.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Máximo de reintentos de envíos de confirmación por períodos.';
    var$num_reintento_defecto := 5;
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Fecha Valor'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$num_reintento_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;

/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$num_reintento_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

   var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es numerico*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'FechaValorConfirmacionMaxReintentosMovimientos';
    var$des_corta_param := 'Máximo de reintentos de envíos por movimientos.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Máximo de reintentos de envíos de confirmación por movimientos.';
    var$num_reintento_defecto := 5;
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Fecha Valor'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$num_reintento_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;

/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$num_reintento_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

   var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es numerico*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'FechaValorConfirmacionMaxHorasConfirmacion';
    var$des_corta_param := 'Máximo de horas para confirmación de períodos o movimientos.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Máximo de horas para confirmación de períodos o movimientos.';
    var$num_reintento_defecto := 48;
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Fecha Valor'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$num_reintento_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;

/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

   var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'FechaValorConfirmacionListaCorreos';
    var$des_corta_param := 'Lista de correos que recibirán los errores .'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Lista de correos (separadas por punto-coma) que recibirán los errores en el proceso de confirmación de acreditación por el banco.';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Fecha Valor'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Fecha Valor', 'Fecha Valor', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;

/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_url   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NiloURLMensajesEntregados';
    var$des_corta_param := 'URL de confirmacion con nilo.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL de confirmacion con nilo.';
    var$valor_url := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 4;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$des_grupo := 'Configuración Nilo';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_url, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'urlApiNotificacionOS';
    var$des_corta_param := 'URL de API permite ver el Json de la notificación de la OS'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'URL de la API que permite visualizar el Json de la notificación de la OS';
    var$valor_defecto := 'https://genesis-producto-api-sit.inth-prosegur.com/api/billing';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 2;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1; /*Es texto*/
    var$des_grupo := 'Configuración Facturación';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                        
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
BEGIN
    /*PGP-1471 - Evolutivo pantalla parametros Cuentas Bancarias*/

    UPDATE GEPR_TPARAMETRO SET DES_DESCRIPCION_LARGA = 'Lista de e-mails a quienes se enviará el correo electronico (separados por coma)'
    WHERE COD_PARAMETRO = 'AprobacionDatosBancariosMailListaDestinatarios';

    UPDATE GEPR_TPARAMETRO SET DES_DESCRIPCION_LARGA = 'Selección de los campos que deberán ser aprobados por modificación (separados por punto y coma).'
    WHERE COD_PARAMETRO = 'CamposAprobacionRequeridaCuentasBancarias';

    COMMIT;
END;
/
/* PGP-2130 */
DECLARE
    var$existe NUMBER;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
BEGIN
    /* ACTUALIZO EL GRUPO DE PARAMETRO */
    UPDATE GEPR_TGRUPO_PARAMETRO SET DES_DESCRIPCION_CORTO = 'Mail',
    DES_DESCRIPCION_LARGA = 'Mail',
    COD_GRUPO_PARAMETRO ='MAIL',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE DES_DESCRIPCION_CORTO='Configuración Mail';
    
    /* ACTUALIZO LOS PARAMETROS */    
    UPDATE GEPR_TPARAMETRO SET COD_PARAMETRO = 'MailURLEnvio',
    DES_DESCRIPCION_CORTO = 'URL de envios de correos.',
    DES_DESCRIPCION_LARGA = 'URL de envios de correos.',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE COD_PARAMETRO='MailServidor';
    
    UPDATE GEPR_TPARAMETRO SET COD_PARAMETRO = 'MailSendgridTempladeId',
    DES_DESCRIPCION_CORTO = 'Identificador del Template en Sendgrid.',
    DES_DESCRIPCION_LARGA = 'Identificador del Template en Sendgrid.',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE COD_PARAMETRO='MailUsuario';
    
    UPDATE GEPR_TPARAMETRO SET COD_PARAMETRO = 'MailAutenticacionURL',
    DES_DESCRIPCION_CORTO = 'URL Autenticacion de envios.',
    DES_DESCRIPCION_LARGA = 'URL Autenticacion de envios.',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE COD_PARAMETRO='MailPuerto';
    
    UPDATE GEPR_TPARAMETRO SET COD_PARAMETRO = 'MailAutenticacionClientId',
    DES_DESCRIPCION_CORTO = 'Client Id para autenticacion de envios.',
    DES_DESCRIPCION_LARGA = 'Client Id para autenticacion de envios.',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE COD_PARAMETRO='MailRemitente';
    
    UPDATE GEPR_TPARAMETRO SET COD_PARAMETRO = 'MailSendgridAPIKey',
    DES_DESCRIPCION_CORTO = 'API Key de envío a Sendgrid.',
    DES_DESCRIPCION_LARGA = 'API Key de envío a Sendgrid.',
    FYH_ACTUALIZACION =SYSDATE()
    WHERE COD_PARAMETRO='MailPassword';
    
    /* ELIMINO LOS PARAMETROS QUE YA NO SERAN UTILIZADOS */
    DELETE FROM gepr_tparametro_valor
    WHERE OID_PARAMETRO IN ( SELECT OID_PARAMETRO FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO = 'UsaSSL');
    DELETE FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO = 'UsaSSL';
    
    COMMIT;
END;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'MailAutenticacionClientSecret';
    var$des_corta_param := 'Client secret para autenticacion de envios.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Client secret para autenticacion de envios.';
    var$valor_defecto := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 2;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1; /*Es texto*/
    var$des_grupo := 'Mail';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'MAIL');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'MAIL')
                        ;
                        
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'MailAutenticacionScope';
    var$des_corta_param := 'Scope para autenticacion de envios.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Scope para autenticacion de envios.';
    var$valor_defecto := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 2;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1; /*Es texto*/
    var$des_grupo := 'Mail';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'MAIL');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'MAIL')
                        ;
                        
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioEnviarCorreos';
    var$des_corta_param := 'Loguea servicio enviar correos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio enviar correos.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    

    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogSALDOSConsultaTransacciones';
    var$des_corta_param := 'Loguea evento de la pantalla Transacciones, Nuevo Saldos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea evento de la pantalla Transacciones, Nuevo Saldos.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogIACBusquedaPuntoServicio';
    var$des_corta_param := 'Loguea evento de la pantalla Búsqueda Punto Servicio.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea evento de la pantalla Búsqueda Punto Servicio.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogIACBusquedaSubCliente';
    var$des_corta_param := 'Loguea evento de la pantalla Busqueda Sub Clientes.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea evento de la pantalla Busqueda Sub Clientes.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_cantidad   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 1; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es numerico*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'DepuracionLogsNumeroDias';
    var$des_corta_param := 'Cantidad de días a mantener Logs en la BBDD.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Cantidad de días a mantener Logs en la BBDD.';
    var$valor_cantidad := '30';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$cod_grupo_parametro := 'DEPURACION';
    var$des_grupo := 'Depuracion';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_cantidad, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioDepuracion';
    var$des_corta_param := 'Loguea servicio de Depuración.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de Depuración.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$cod_grupo_parametro := 'CFAP_GNS_IAC';
    var$des_grupo := 'Log';
    var$nec_tipo_componte := 3; /*Es check*/

    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioCrearDocumentosFondos';
    var$des_corta_param := 'Loguea servicio Crear Documentos Fondos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio Crear Documentos Fondos.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$cod_grupo_parametro := 'CFAP_GNS_IAC';
    var$des_grupo := 'Log';
    var$nec_tipo_componte := 3; /*Es check*/

    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NotificacionListaCorreosMovimientosNoAcreditados';
    var$des_corta_param := 'Lista de Correos para Recibir  Movimientos No Acreditados.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Lista de Correos para Recibir  Movimientos No Acreditados.';
    var$valor_defecto := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$cod_grupo_parametro := 'NOTIFICACION';
    var$des_grupo := 'Notificacion';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'NotificacionHorasLimiteAcreditacionFVO';
    var$des_corta_param := 'Diferencia Horas Acreditacion de FVO.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Diferencia Horas Acreditacion de FVO.';
    var$valor_defecto := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$cod_grupo_parametro := 'NOTIFICACION';
    var$des_grupo := 'Notificacion';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioNotificarMovimientosNoAcreditados';
    var$des_corta_param := 'Loguea servicio NotificarMovimientosQueNoSeranAcreditados.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio NotificarMovimientosQueNoSeranAcreditados.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$cod_grupo_parametro := 'CFAP_GNS_IAC';
    var$des_grupo := 'Log';
    var$nec_tipo_componte := 3; /*Es check*/

    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel país
    var$bol_obligatorio  := 0; /* Lo considero como obligatorio */
    var$NEC_TIPO_DATO := 1; /*Es texto*/
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'ConfigurarEntornoServidor';
    var$des_corta_param := 'Indica el entorno del sevidor.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Indica el entorno del sevidor.';
    var$valor_defecto := '';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 1;
    var$cod_grupo_parametro := 'Configuración Aplicación';
    var$des_grupo := 'Configuración Aplicación';
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
      
end;
/

/*Generar Periodos*/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_grupo_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
begin

    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioGenerarPeriodos';
    var$des_corta_param := 'Loguea servicio GenerarPeriodos.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio GenerarPeriodos.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 1;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$cod_grupo_parametro := 'CFAP_GNS_IAC';
    var$des_grupo := 'Log';
    var$nec_tipo_componte := 3; /*Es check*/

    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo_parametro)
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

                
        end;
        
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;
    
end;
/

declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogServicioRecuperarMAEs';
    var$des_corta_param := 'Loguea servicio de recuperar MAEs.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de recuperar MAEs.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/

    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

        end;

        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;

        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogIACBusquedaPlanificacion';
    var$des_corta_param := 'Loguea evento de la pantalla Búsqueda Planificación.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea evento de la pantalla Búsqueda Planificación.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion);

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogIACBusquedaMAE';
    var$des_corta_param := 'Loguea evento de la pantalla Búsqueda MAES.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea evento de la pantalla Búsqueda MAES.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 13;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    
    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;

    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;

        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;

        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
              /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro);*/
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;

                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;
        end;
        
        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;
        
        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion);

            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;
        
        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogSALDOSLoginUnificado';
    var$des_corta_param := 'Loguea servicio de autenticar usuario y gestionar token.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de autenticar usuario y gestionar token.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/

    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

        end;

        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;

        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogIACLoginUnificado';
    var$des_corta_param := 'Loguea servicio de autenticar usuario y gestionar token.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de autenticar usuario y gestionar token.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/

    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

        end;

        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;

        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'LogREPORTELoginUnificado';
    var$des_corta_param := 'Loguea servicio de autenticar usuario y gestionar token.'; /* Se achica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Loguea servicio de autenticar usuario y gestionar token.';
    var$valor_defecto := '1';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 16;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/

    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC');
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = 'Log'
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, 'Log', 'Log', var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, 'CFAP_GNS_IAC')
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

        end;

        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;

        COMMIT;
    end if;    
end;
/
declare    
    var$cod_aplicacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_nivel_parametro gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_parametro   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_parametro_valor   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_parametro   gepr_pcomon_###VERSION###.tipo$cod_; 
    var$oid_nivel_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_aplicacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_obligatorio gepr_pcomon_###VERSION###.tipo$nbol_;
    var$NEC_TIPO_DATO    gepr_pcomon_###VERSION###.tipo$nel_;
    var$BOL_LISTA_VALORES gepr_pcomon_###VERSION###.tipo$nbol_;
    var$BOL_EXISTE  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_grupo_parametro gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_corta_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_larga_param gepr_pcomon_###VERSION###.tipo$desc_;
    var$valor_defecto   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_usuario gepr_pcomon_###VERSION###.tipo$usr_;
    var$nec_orden   gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_actualizacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$nec_tipo_componte   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_permiso gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_grupo   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_grupo   gepr_pcomon_###VERSION###.tipo$cod_;
begin
    var$cod_aplicacion := 'Genesis'; --> IAC
    var$cod_nivel_parametro :=  '1'; --> Nivel pais
    var$bol_obligatorio  := 0; /* No considero como obligatorio */
    var$NEC_TIPO_DATO := 2; /*Es check */
    var$BOL_LISTA_VALORES :=0; /* No es una lista de valores */
    var$cod_parametro := 'ValidaMaquinaPunto';
    var$des_corta_param := 'Valida relacion de la maquina/punto en entrada movimiento.'; /* Se archica porque el maximo es 60 caracteres */
    var$des_larga_param := 'Valida la relacion de la maquina con el punto de servicio en la entrada de movimiento.';
    var$valor_defecto := '0';
    var$cod_usuario := 'GENESIS_INSTALL';
    var$nec_orden := 5;
    var$fyh_actualizacion := sysdate();
    var$cod_permiso := 'PARAMETROS_CONFIGURAR_GENESIS';
    var$nec_tipo_componte := 3; /*Es check*/
    var$des_grupo := 'Datos Operativos';
    var$cod_grupo := 'DTOP_GNS_PS';

    begin
        select count(*)
        into    var$bol_existe
        from    gepr_tparametro
        where   cod_parametro = var$cod_parametro;
    exception
      when no_data_found then
        var$bol_existe := 0;
    end;
    /* En caso de no existir, se crea. */
    if var$bol_existe = 0 then
        /* Controlo que exista la aplicacion */
        begin
            select  oid_aplicacion
            into    var$oid_aplicacion
            from    gepr_Taplicacion
            where   cod_aplicacion = var$cod_aplicacion;
        exception
            when no_data_found then
            var$oid_aplicacion := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe en GEPR_TAPLICACION para el codigo '|| var$cod_aplicacion);
        end;
        /* Controlo que exista el nivel parametro */
        begin
            select  oid_nivel_parametro
            into    var$oid_nivel_parametro
            from    GEPR_TNIVEL_PARAMETRO
            where   cod_nivel_parametro = var$cod_nivel_parametro;
        exception
            when no_data_found then
            var$oid_nivel_parametro := '';
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'no existe en GEPR_TNIVEL_PARAMETRO para el codigo '|| var$cod_nivel_parametro);
        end;
        /* Busco el OID_GRUPO_PARAMETRO */
        begin
            select OID_GRUPO_PARAMETRO
            into    var$oid_grupo_parametro
            from    GEPR_TGRUPO_PARAMETRO
            where   oid_aplicacion = var$oid_aplicacion
            and     oid_nivel_parametro = var$oid_nivel_parametro;
        exception
            when no_data_found then
              var$oid_grupo_parametro:= sys_guid();
              insert into GEPR_TGRUPO_PARAMETRO
              (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
              values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo);
            when too_many_rows then
                begin
                    select OID_GRUPO_PARAMETRO
                    into    var$oid_grupo_parametro
                    from    GEPR_TGRUPO_PARAMETRO
                    where   oid_aplicacion = var$oid_aplicacion
                    and     oid_nivel_parametro = var$oid_nivel_parametro
                    and     DES_DESCRIPCION_CORTO = var$des_grupo
                    ;
                exception
                  when no_data_found then
                    var$oid_grupo_parametro:= sys_guid();
                    insert into GEPR_TGRUPO_PARAMETRO
                        (OID_GRUPO_PARAMETRO, oid_nivel_parametro, oid_aplicacion, DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, NEC_ORDEN, cod_usuario, FYH_ACTUALIZACION, cod_permiso, cod_grupo_parametro)
                        values (var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$des_grupo, var$des_grupo, var$nec_orden, var$cod_usuario, var$fyh_actualizacion, var$cod_permiso, var$cod_grupo)
                        ;
                  when too_many_rows then
                    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'existe más de un registro para GEPR_TGRUPO_PARAMETRO para el codigo app '|| var$cod_aplicacion || ' y nivel parametro ' || var$cod_nivel_parametro)
                    ;
                end;

        end;

        /* Asigno un NUEVO OID */
        select sys_guid()
        into    var$oid_parametro
        from dual;

        /*CREO EL REGISTRO DE PARAMETRO */
        begin
            Insert into GEPR_TPARAMETRO
            (OID_PARAMETRO, OID_GRUPO_PARAMETRO, OID_NIVEL_PARAMETRO, OID_APLICACION, COD_PARAMETRO,
            DES_DESCRIPCION_CORTO, DES_DESCRIPCION_LARGA, BOL_OBLIGATORIO, BOL_LISTA_VALORES, NEC_ORDEN, 
            NEC_TIPO_DATO, NEC_TIPO_COMPONENTE, COD_USUARIO, FYH_ACTUALIZACION)
            values( var$oid_parametro, var$oid_grupo_parametro, var$oid_nivel_parametro, var$oid_aplicacion, var$cod_parametro, 
            var$des_corta_param, var$des_larga_param, var$bol_obligatorio, var$BOL_LISTA_VALORES, var$nec_orden,
            var$NEC_TIPO_DATO, var$nec_tipo_componte, var$cod_usuario, var$fyh_actualizacion)
            ;
            FOR P IN (SELECT DISTINCT COD_PAIS FROM GEPR_TDELEGACION) LOOP
                var$oid_parametro_valor := sys_guid();
                insert into gepr_tparametro_valor (oid_parametro_valor, oid_parametro, cod_identificador_nivel, des_valor_parametro,
                cod_usuario, fyh_actualizacion)
                values (var$oid_parametro_valor, var$oid_parametro, P.COD_PAIS, var$valor_defecto, var$cod_usuario, var$fyh_actualizacion);
            END LOOP;
        exception
          when others then
            ROLLBACK;
        end;

        COMMIT;
    end if;    
end;
/