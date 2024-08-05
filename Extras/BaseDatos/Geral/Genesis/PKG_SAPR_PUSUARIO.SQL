CREATE OR REPLACE PACKAGE SAPR_PUSUARIO_###VERSION### AS
  /*Version: ###VERSION_COMP###*/
	
    FUNCTION frecuperar_identificador(par$desLogin IN gepr_pcomon_###VERSION###.tipo$cod_)
        RETURN gepr_pcomon_###VERSION###.tipo$oid_;

    /* Procedure que devuelve los permisos*/
    PROCEDURE srecuperar_permisos(par$oid_aplicacion    IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$rc_permisos         OUT sys_refcursor
                                );

    /* Procedure que devuelve las aplicaciones*/
    PROCEDURE srecuperar_aplicaciones(par$cod_aplicacion  IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_aplicacion      IN  gepr_pcomon_###VERSION###.tipo$desc_,
                            par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                            par$rc_aplicaciones     OUT sys_refcursor
                        );

    /* Procedure que devuelve los roles*/
    PROCEDURE srecuperar_role(par$cod_role          IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$oid_aplicacion      IN  gepr_pcomon_###VERSION###.tipo$oid_,
                            par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                            par$modo_detallado      IN  gepr_pcomon_###VERSION###.tipo$nbol_,      
                            par$rc_roles            OUT sys_refcursor,
                            par$rc_permisos         OUT sys_refcursor
                        );

    /* Procedure que se encarga del ABM de roles junto con los permisos asociados al rol */
    PROCEDURE sconfigurar_role(
        /*Rol*/
        par$cod_accion              IN  gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_role                IN  gepr_pcomon_###VERSION###.tipo$cod_,
        par$des_role                IN  gepr_pcomon_###VERSION###.tipo$desc_,
        par$bol_activo              IN  gepr_pcomon_###VERSION###.tipo$nbol_,
        /*Permisos*/
        par$aoid_permiso            IN  gepr_pcomon_###VERSION###.tipo$oids_,
        par$cod_cultura             IN  gepr_pcomon_###VERSION###.tipo$cod_,
		par$cod_usuario             IN  gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor);

    /*Valida los datos ingresados en el procedure sconfigurar_role */
    PROCEDURE svalidar_role(par$cod_accion  IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_role    IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_role    IN  gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_cultura IN  gepr_pcomon_###VERSION###.tipo$cod_);

    /* Procedure que devuelve el usuario con los datos necesarios despues de hacer el login*/
    PROCEDURE srecuperar_usuario_login(par$desLogin             IN  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_pais        IN  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$rc_datos_login  OUT sys_refcursor
                                      );  

    /* Procedure que guarda el token de acceso generado al hacer el login*/
    PROCEDURE sgrabar_token_acceso(par$oid_token_acceso         IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_usuario               IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_aplicacion            IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_token_acceso          IN  gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$des_permisos_acceso       IN  BLOB,
                                  par$des_configuraciones       IN  BLOB
                                  );

    /* Procedure que recupera el token de acceso generado al hacer el login*/
    PROCEDURE sobtener_token_acceso(par$oid_token_acceso      IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$rc_datos              OUT sys_refcursor
                                  );

    /* Procedure que elimina el token de acceso generado al hacer el login*/
    PROCEDURE sborrar_token_acceso(par$oid_token_acceso      IN  gepr_pcomon_###VERSION###.tipo$oid_
                                  );

    /* Procedure que se encarga del ABM de usuarios junto con los roles asociados al usuario */
    PROCEDURE sconfigurar_usuario(
                        par$cod_accion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$aoid_role               IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_usuario            IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$ades_login              IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_nombre             IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_apellido           IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_idioma             IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$aoid_pais               IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$abol_activo             IN gepr_pcomon_###VERSION###.tipo$nbols_,
                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$rc_validaciones         OUT sys_refcursor);

    /*Valida los datos ingresados en el procedure sconfigurar_usuario */
    PROCEDURE svalidar_usuarios(
                                par$cod_accion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aoid_usuario            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ades_login              IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$ades_nombre             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$ades_apellido           IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$aoid_pais               IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$aoid_role               IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$cod_cultura IN  gepr_pcomon_###VERSION###.tipo$cod_);
    
     /* Procedure que devuelve los usuarios*/
    PROCEDURE srecuperar_usuario(par$cod_pais IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_role IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$des_login IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$modo_detallado      IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$rc_usuarios OUT sys_refcursor);

     /*Función que corrobora si hay error para el procedure sconfigurar_usuario*/
    FUNCTION fhay_error_user RETURN BOOLEAN ;
    /*Función que corrobora si hay error para el procedure sconfigurar_role*/
    FUNCTION fhay_error RETURN BOOLEAN;

END SAPR_PUSUARIO_###VERSION###;
/

create or replace PACKAGE BODY SAPR_PUSUARIO_###VERSION###
    AS

	FUNCTION frecuperar_identificador(par$desLogin IN gepr_pcomon_###VERSION###.tipo$cod_) 
        RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
        var$oid_usuario gepr_pcomon_###VERSION###.tipo$oid_;
    BEGIN
        BEGIN
            SELECT D.OID_USUARIO
                INTO var$oid_usuario
            FROM SAPR_TUSUARIO D
            WHERE UPPER(D.DES_LOGIN) = UPPER(par$desLogin);
        EXCEPTION
            WHEN NO_DATA_FOUND THEN var$oid_usuario := NULL;
        END;
        RETURN var$oid_usuario;
    END frecuperar_identificador;



    /* Procedure que devuelve los permisos*/
    PROCEDURE srecuperar_permisos(par$oid_aplicacion    IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$rc_permisos         OUT sys_refcursor
                                ) IS  
    BEGIN
        OPEN par$rc_permisos FOR
        SELECT 
            A.OID_APLICACION, A.COD_APLICACION, A.DES_APLICACION, 
            P.OID_PERMISO, P.COD_PERMISO, P.DES_PERMISO, P.BOL_ACTIVO
            FROM SAPR_TPERMISO P
            INNER JOIN SAPR_TAPLICACION A ON A.OID_APLICACION = P.OID_APLICACION
            WHERE 
                (P.OID_APLICACION = par$oid_aplicacion OR par$oid_aplicacion IS NULL)
                AND P.BOL_ACTIVO = par$bol_activo;
    END srecuperar_permisos;


    /* Procedure que devuelve las aplicaciones*/
    PROCEDURE srecuperar_aplicaciones(par$cod_aplicacion  IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_aplicacion      IN  gepr_pcomon_###VERSION###.tipo$desc_,
                            par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                            par$rc_aplicaciones     OUT sys_refcursor
                        ) IS  
    BEGIN
        OPEN par$rc_aplicaciones FOR
            SELECT OID_APLICACION, COD_APLICACION, DES_APLICACION, BOL_ACTIVO
            FROM SAPR_TAPLICACION
            WHERE 
                (COD_APLICACION = par$cod_aplicacion OR par$cod_aplicacion IS NULL)
                AND (UPPER(DES_APLICACION) LIKE '%' || UPPER(par$des_aplicacion) || '%' OR par$des_aplicacion IS NULL)
                AND BOL_ACTIVO = par$bol_activo;

    END srecuperar_aplicaciones;


    /* Procedure que devuelve los roles*/
    PROCEDURE srecuperar_role(par$cod_role          IN  gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_aplicacion      IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                par$bol_activo          IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$modo_detallado      IN  gepr_pcomon_###VERSION###.tipo$nbol_,      
                                par$rc_roles            OUT sys_refcursor,
                                par$rc_permisos         OUT sys_refcursor
                            ) IS
    BEGIN
        /*Inicializamos los cursores */
        OPEN par$rc_roles FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        OPEN par$rc_permisos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

        IF par$modo_detallado IS NULL OR par$modo_detallado = 0 THEN
            /* Modo no detallado: Roles junto con su aplicación */
            OPEN par$rc_roles FOR
            SELECT DISTINCT R.OID_ROLE, R.COD_ROLE, R.DES_ROLE, R.BOL_ACTIVO, A.OID_APLICACION, A.COD_APLICACION, A.DES_APLICACION
            FROM SAPR_TROLE R
            LEFT JOIN SAPR_TPERMISOXROLE PR ON PR.OID_ROLE = R.OID_ROLE AND PR.BOL_ACTIVO = 1
            LEFT JOIN SAPR_TPERMISO P ON P.OID_PERMISO = PR.OID_PERMISO
            LEFT JOIN SAPR_TAPLICACION A ON A.OID_APLICACION = P.OID_APLICACION
            WHERE
                UPPER(R.COD_ROLE) LIKE'%' || UPPER(par$cod_role) || '%'
                AND R.BOL_ACTIVO = par$bol_activo
                AND (A.OID_APLICACION = par$oid_aplicacion OR par$oid_aplicacion IS NULL);
        ELSE
            /* Modo detallado: Roles junto con los permisos con su aplicación */
            OPEN par$rc_roles FOR
                SELECT R.OID_ROLE, R.COD_ROLE, R.DES_ROLE, R.BOL_ACTIVO 
                FROM SAPR_TROLE R
                WHERE R.COD_ROLE = par$cod_role;

            OPEN par$rc_permisos FOR
                SELECT 
                    R.OID_ROLE, R.COD_ROLE, R.DES_ROLE, R.BOL_ACTIVO, 
                    A.OID_APLICACION, A.COD_APLICACION, A.DES_APLICACION, 
                    P.OID_PERMISO, P.COD_PERMISO, P.DES_PERMISO
                FROM SAPR_TPERMISOXROLE PR
                INNER JOIN SAPR_TROLE R ON R.OID_ROLE = PR.OID_ROLE AND R.COD_ROLE = par$cod_role
                INNER JOIN SAPR_TPERMISO P ON P.OID_PERMISO = PR.OID_PERMISO
                INNER JOIN SAPR_TAPLICACION A ON A.OID_APLICACION = P.OID_APLICACION
                WHERE PR.BOL_ACTIVO = 1;
        END IF;

    END srecuperar_role;



    /* Procedure que se encarga del ABM de roles junto con los permisos asociados al rol */
    PROCEDURE sconfigurar_role(
        /*Rol*/
        par$cod_accion              IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_role                IN  gepr_pcomon_###VERSION###.tipo$cod_,
        par$des_role                IN  gepr_pcomon_###VERSION###.tipo$desc_,
        par$bol_activo              IN  gepr_pcomon_###VERSION###.tipo$nbol_,
        /*Permisos*/
        par$aoid_permiso            IN  gepr_pcomon_###VERSION###.tipo$oids_,
        par$cod_cultura             IN  gepr_pcomon_###VERSION###.tipo$cod_,
		par$cod_usuario             IN  gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor) IS

        var$oid_role            gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_permisoxrole    gepr_pcomon_###VERSION###.tipo$oid_;
        var$gmt                 VARCHAR2(80);
        var$mensaje             gepr_pcomon_###VERSION###.tipo$desc_;  
        v_code  NUMBER;
        v_errm  VARCHAR2(4000);

    BEGIN
        DELETE SAPR_GTT_TAUXILIAR;
        COMMIT;

        OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

        svalidar_role(par$cod_accion,
                    par$cod_role,
                    par$des_role,
                    par$cod_cultura);

        IF NOT fhay_error THEN
            /*Recuperamos el Id del rol almacenado durante la validación*/
            BEGIN
                SELECT OID_CAMPO1
                    INTO var$oid_role
                    FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_ROLE';
            EXCEPTION
                WHEN no_data_found THEN
                    var$oid_role := NULL;
            END;

            var$gmt := current_timestamp;

            IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL THEN
                IF var$oid_role IS NULL THEN
                    var$oid_role := SYS_GUID();
                    /* Realizo el insert del rol*/
                    INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, 
                        GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                    VALUES (var$oid_role, par$cod_role, par$des_role, par$bol_activo,
                        var$gmt, par$cod_usuario, var$gmt, par$cod_usuario);

                    /* Recorro el array de permisos y realizo el insert */
                    IF par$aoid_permiso IS NOT NULL AND par$aoid_permiso.COUNT > 0 THEN
                        FOR idx IN par$aoid_permiso.first .. par$aoid_permiso.last LOOP
                            INSERT INTO SAPR_TPERMISOXROLE(OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO,
                                GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                            VALUES(SYS_GUID(), par$aoid_permiso(idx), var$oid_role, 1,
                                var$gmt, par$cod_usuario, var$gmt, par$cod_usuario);
                        END LOOP;
                    END IF;
                END IF;
            ELSIF par$cod_accion = 'MODIFICAR' THEN
                /* Doy de baja los permisos actuales asociados al rol*/
                UPDATE SAPR_TPERMISOXROLE
                    SET BOL_ACTIVO = 0,
                        GMT_MODIFICACION = var$gmt, 
                        DES_USUARIO_MODIFICACION = par$cod_usuario
                WHERE OID_ROLE = var$oid_role;

                /* Volver a habilitar (bol_activo = 1) los permisos actuales que se enviaron por parametro*/
                /* Insertar los nuevos parametros que no existen en los permisos actuales */
                IF par$aoid_permiso IS NOT NULL AND par$aoid_permiso.COUNT > 0 THEN
                    FOR idx IN par$aoid_permiso.first .. par$aoid_permiso.last LOOP
                        /*Buscamos si el rol ya posee el permiso */
                        BEGIN
                            SELECT OID_PERMISOXROLE 
                            INTO var$oid_permisoxrole
                            FROM SAPR_TPERMISOXROLE
                            WHERE OID_ROLE = var$oid_role AND OID_PERMISO = par$aoid_permiso(idx) AND ROWNUM = 1;
                        EXCEPTION
                            WHEN no_data_found THEN
                                var$oid_permisoxrole := NULL;
                        END;
                        IF var$oid_permisoxrole IS NULL THEN
                            /* Inserto el permiso para el rol*/
                            INSERT INTO SAPR_TPERMISOXROLE(OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO,
                                GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                            VALUES(SYS_GUID(), par$aoid_permiso(idx), var$oid_role, 1,
                                var$gmt, par$cod_usuario, var$gmt, par$cod_usuario);
                        ELSE
                            /* Vuelvo a habilitar el permiso para el rol */
                            UPDATE SAPR_TPERMISOXROLE
                                SET BOL_ACTIVO = 1,
                                    GMT_MODIFICACION = var$gmt, 
                                    DES_USUARIO_MODIFICACION = par$cod_usuario
                            WHERE OID_PERMISOXROLE = var$oid_permisoxrole;
                        END IF;
                    END LOOP;
                END IF;

                /* Actualizo los datos del ROL */
                UPDATE SAPR_TROLE
                    SET BOL_ACTIVO = par$bol_activo,
                        GMT_MODIFICACION = var$gmt, 
                        DES_USUARIO_MODIFICACION = par$cod_usuario,
                        DES_ROLE = par$des_role
                WHERE OID_ROLE = var$oid_role;

            ELSIF par$cod_accion = 'BAJA' THEN
                /* Damos de baja el rol */
                UPDATE SAPR_TROLE
                    SET BOL_ACTIVO = 0, 
                        GMT_MODIFICACION = var$gmt, 
                        DES_USUARIO_MODIFICACION = par$cod_usuario
                WHERE OID_ROLE = var$oid_role;

                /* Damos de baja los permisos del rol */
                UPDATE SAPR_TPERMISOXROLE
                    SET BOL_ACTIVO = 0,
                        GMT_MODIFICACION = var$gmt, 
                        DES_USUARIO_MODIFICACION = par$cod_usuario
                WHERE OID_ROLE = var$oid_role;
            END IF;

            /* Insertamos mensaje de éxito*/
            /*msgExito - Registro actualizado con éxito.*/
            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgExito',
            gepr_pcomon_###VERSION###.const$CodFunConfigurarRoles,
            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
            NULL,
            0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgExito',
                var$mensaje,
                'VALIDACIONES',
                gepr_pcomon_###VERSION###.const$codTipoExito
                ); 
        END IF;


        /* Poblamos el cursor de validaciones*/
        OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO,
                COD_CAMPO2 AS DESCRIPCION,
                COD_CALIFICADOR AS CALIFICADOR,
                NEL_CAMPO5 AS TIPO_MENSAJE
            FROM SAPR_GTT_TAUXILIAR AUX
            WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

        COMMIT;

    EXCEPTION WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK');
        ROLLBACK;
        v_code := SQLCODE;
        v_errm := SQLERRM;

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgRollback',
                'Error code: ' || v_code || ' - Message: ' ||  v_errm,
                'VALIDACIONES',
                gepr_pcomon_###VERSION###.const$codTipoErrorAplicacion
                ); 

        /* Poblamos el cursor de validaciones*/
        OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO,
                COD_CAMPO2 AS DESCRIPCION,
                COD_CALIFICADOR AS CALIFICADOR,
                NEL_CAMPO5 AS TIPO_MENSAJE
            FROM SAPR_GTT_TAUXILIAR AUX
            WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

    END sconfigurar_role;

    /*Valida los datos ingresados en el procedure sconfigurar_role */
    PROCEDURE svalidar_role(par$cod_accion  IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_role    IN  gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_role    IN  gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_cultura IN  gepr_pcomon_###VERSION###.tipo$cod_) IS
        var$oid_role    gepr_pcomon_###VERSION###.tipo$oid_;
        var$mensaje     gepr_pcomon_###VERSION###.tipo$desc_;     
    BEGIN
        IF par$cod_role IS NULL THEN
            /*msgCodigoRolObligatorio - Es obligatorio informar el código del Rol.*/
            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgCodigoRolObligatorio',
            gepr_pcomon_###VERSION###.const$CodFunConfigurarRoles,
            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
            NULL,
            0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgCodigoRolObligatorio',
                var$mensaje,
                'VALIDACIONES',
                gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                ); 
        ELSE
            /* Buscamos el Id del rol por el codigo enviado*/
            BEGIN
                SELECT OID_ROLE
                    INTO var$oid_role
                    FROM SAPR_TROLE
                WHERE COD_ROLE = par$cod_role;
            EXCEPTION
                WHEN no_data_found THEN
                    var$oid_role := NULL;
            END;
            IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL THEN
                /* En caso de tratarse de un alta y el Código ya existe */
                IF var$oid_role IS NOT NULL THEN
                    /*msgRolExistente - Ya existe un rol con el código informado: {0}.*/
                    var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgRolExistente',
                    gepr_pcomon_###VERSION###.const$CodFunConfigurarRoles,
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    par$cod_role,
                    0);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                    VALUES (
                        'msgRolExistente',
                        var$mensaje,
                        'VALIDACIONES',
                        gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                        );
                END IF;
            ELSE 
                /* En caso de que exista lo almacenamos en la tabla temproral*/
                IF var$oid_role IS NOT NULL THEN
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_role, 'OID_ROLE'); 
                ELSE
                    /*msgRolInexistente - No existe un rol con el código informado: {0}.*/
                    var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgRolInexistente',
                    gepr_pcomon_###VERSION###.const$CodFunConfigurarRoles,
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    par$cod_role,
                    0);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                    VALUES (
                        'msgRolInexistente',
                        var$mensaje,
                        'VALIDACIONES',
                        gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                        );
                END IF;
            END IF;
        END IF;

        IF par$cod_accion <> 'BAJA' AND par$des_role IS NULL THEN
            /*msgDescripcionRolObligatorio - Es obligatorio informar la descripción del Rol.*/
            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgDescripcionRolObligatorio',
            gepr_pcomon_###VERSION###.const$CodFunConfigurarRoles,
            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
            NULL,
            0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgDescripcionRolObligatorio',
                var$mensaje,
                'VALIDACIONES',
                gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                ); 
        END IF;
    END svalidar_role;

    /* Procedure que devuelve el usuario con los datos necesarios despues de hacer el login*/
    PROCEDURE srecuperar_usuario_login(par$desLogin             IN  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_pais        IN  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$rc_datos_login  OUT sys_refcursor
                                      ) IS  
    BEGIN

        OPEN par$rc_datos_login FOR
          SELECT U.OID_USUARIO, U.DES_LOGIN, U.DES_NOMBRE, U.DES_APELLIDO, U.DES_IDIOMA_PREFERIDO,
          PAIS.OID_PAIS, PAIS.COD_PAIS, PAIS.DES_PAIS, 
          DE.OID_DELEGACION, DE.COD_DELEGACION, DE.DES_DELEGACION, DE.NEC_GMT_MINUTOS, DE.FYH_VERANO_INICIO ,DE.FYH_VERANO_FIN ,DE.NEC_VERANO_AJUSTE ,DE.DES_ZONA ,
          PLA.OID_PLANTA, PLA.COD_PLANTA, PLA.DES_PLANTA,
          R.OID_ROLE, R.COD_ROLE,
          PER.OID_PERMISO, PER.COD_PERMISO, PER.DES_PERMISO,
          APL.OID_APLICACION, APL.COD_APLICACION, DES_APLICACION,
          CASE APL.COD_APLICACION
          WHEN 'GenesisSaldos' THEN 'Prosegur.Genesis.NuevoSaldos'
          WHEN 'IAC' THEN 'Prosegur.Genesis.IAC'
          WHEN 'Reportes' THEN 'Prosegur.Genesis.Listado.Conteo'
          END AS URL_APLICACION,
          'Prosegur.Genesis.Servicio' AS URL_SERVICIO
          FROM SAPR_TUSUARIO U
          INNER JOIN SAPR_TROLEXUSUARIO TRO ON (TRO.OID_USUARIO = U.OID_USUARIO AND TRO.BOL_ACTIVO = 1)
          INNER JOIN GEPR_TPAIS PAIS ON PAIS.OID_PAIS = TRO.OID_PAIS
          INNER JOIN GEPR_TDELEGACION DE ON DE.OID_DELEGACION = 
              (SELECT OID_DELEGACION FROM GEPR_TDELEGACION WHERE OID_PAIS = PAIS.OID_PAIS AND BOL_DEFAULT = 1 AND ROWNUM = 1)
          INNER JOIN SAPR_TROLE R ON R.OID_ROLE = TRO.OID_ROLE
          INNER JOIN SAPR_TPERMISOXROLE PR ON PR.OID_ROLE = R.OID_ROLE 
          INNER JOIN SAPR_TPERMISO PER ON PER.OID_PERMISO = PR.OID_PERMISO
          INNER JOIN SAPR_TAPLICACION APL ON APL.OID_APLICACION = PER.OID_APLICACION
          INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_DELEGACION = DE.OID_DELEGACION AND PLA.COD_PLANTA = DE.COD_DELEGACION
          WHERE
              UPPER(U.DES_LOGIN) = UPPER(par$desLogin)AND
              PAIS.COD_PAIS = par$cod_pais AND
              U.BOL_ACTIVO = 1 AND
              R.BOL_ACTIVO = 1 AND
              PER.BOL_ACTIVO = 1
          ORDER BY APL.COD_APLICACION, R.COD_ROLE, PER.COD_PERMISO;
    END srecuperar_usuario_login;


    /* Procedure que guarda el token de acceso generado al hacer el login*/
    PROCEDURE sgrabar_token_acceso(par$oid_token_acceso         IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_usuario               IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_aplicacion            IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_token_acceso          IN  gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$des_permisos_acceso       IN  BLOB,
                                  par$des_configuraciones       IN  BLOB
                                  ) IS  
    BEGIN
      INSERT INTO sapr_ttoken_acceso (oid_token_acceso, oid_usuario, oid_aplicacion, fyh_token_acceso, des_permisos_acceso, des_configuraciones)
        VALUES (par$oid_token_acceso, par$oid_usuario, par$oid_aplicacion, par$fyh_token_acceso, par$des_permisos_acceso, par$des_configuraciones);
      COMMIT;
    EXCEPTION
      WHEN others THEN
        ROLLBACK;
        raise_application_error(-20001, sqlerrm);
    END sgrabar_token_acceso;

    /* Procedure que recupera el token de acceso generado al hacer el login*/
    PROCEDURE sobtener_token_acceso(par$oid_token_acceso      IN  gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$rc_datos              OUT sys_refcursor
                                  ) IS  
    BEGIN
      OPEN par$rc_datos FOR
        SELECT OID_TOKEN_ACCESO,
        OID_USUARIO,
        OID_APLICACION,
        FYH_TOKEN_ACCESO,
        DES_PERMISOS_ACCESO,
	      DES_CONFIGURACIONES
        FROM SAPR_TTOKEN_ACCESO
        WHERE OID_TOKEN_ACCESO = par$oid_token_acceso;
    EXCEPTION
      WHEN others THEN
        raise_application_error(-20001, sqlerrm);
    END sobtener_token_acceso;

    /* Procedure que elimina el token de acceso generado al hacer el login*/
    PROCEDURE sborrar_token_acceso(par$oid_token_acceso         IN  gepr_pcomon_###VERSION###.tipo$oid_
                                  ) IS  
    BEGIN
      DELETE FROM SAPR_TTOKEN_ACCESO WHERE OID_TOKEN_ACCESO = par$oid_token_acceso;
      COMMIT;
    EXCEPTION
      WHEN others THEN
        ROLLBACK;
        raise_application_error(-20001, sqlerrm);
    END sborrar_token_acceso;


    /* Procedure que se encarga del ABM de usuarios junto con los roles asociados al usuario */
    PROCEDURE sconfigurar_usuario(
                            par$cod_accion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$aoid_role               IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_usuario            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$ades_login              IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$ades_nombre             IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$ades_apellido           IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$ades_idioma             IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$aoid_pais               IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$abol_activo             IN gepr_pcomon_###VERSION###.tipo$nbols_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$rc_validaciones         OUT sys_refcursor) IS

    var$gmt_zero            gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$mensaje             gepr_pcomon_###VERSION###.tipo$desc_;  
    var$aoid_usuario        gepr_pcomon_###VERSION###.tipo$oids_;
    v_code  NUMBER;
    v_errm  VARCHAR2(4000);
    var$existe_usuario      gepr_pcomon_###VERSION###.tipo$nel_;
    var$existe_rolxusr      gepr_pcomon_###VERSION###.tipo$nel_;
    BEGIN
        DELETE SAPR_GTT_TAUXILIAR;
        COMMIT;

        OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

        svalidar_usuarios(
            par$cod_accion      => par$cod_accion,
            par$aoid_usuario    => par$aoid_usuario,
            par$ades_login      => par$ades_login,
            par$ades_nombre     => par$ades_nombre,
            par$ades_apellido   => par$ades_apellido,
            par$aoid_pais       => par$aoid_pais,
            par$aoid_role       => par$aoid_role,
            par$cod_cultura     => par$cod_cultura

        );

        IF not fhay_error_user() THEN
            IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL OR par$cod_accion = 'MODIFICAR' THEN

                /* Recorro el array de usuarios y realizo el insert en SAPR_TUSUARIO*/
                IF par$ades_login IS NOT NULL AND par$ades_login.COUNT > 0 THEN
                    FOR idx IN par$ades_login.first .. par$ades_login.last LOOP

                        IF par$ades_login(idx) is not null then
                             IF par$cod_accion = 'MODIFICAR' THEN
                                begin
                                    SELECT COUNT (*)
                                    INTO var$existe_usuario
                                    FROM   SAPR_TUSUARIO
                                    WHERE
                                        upper(DES_LOGIN) = upper(par$ades_login(idx))
                                        AND OID_USUARIO = par$aoid_usuario(idx)
                                        ;
                                exception
                                    when no_data_found then
                                    var$existe_usuario := 0;
                                end;
                             END IF;
                             IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL THEN
                                begin
                                    SELECT COUNT (*)
                                    INTO var$existe_usuario
                                    FROM   SAPR_TUSUARIO
                                    WHERE
                                        upper(DES_LOGIN) = upper(par$ades_login(idx))
                                        ;
                                exception
                                    when no_data_found then
                                    var$existe_usuario := 0;
                                end;
                             END IF;


                            IF var$existe_usuario = 0 THEN
                                var$aoid_usuario(idx) := SYS_GUID();
                                INSERT INTO SAPR_TUSUARIO (OID_USUARIO, DES_LOGIN, DES_NOMBRE, DES_APELLIDO, DES_IDIOMA_PREFERIDO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                                    VALUES (var$aoid_usuario(idx), par$ades_login(idx), par$ades_nombre(idx), par$ades_apellido(idx), par$ades_idioma(idx),  1 ,var$gmt_zero ,par$cod_usuario,CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE) ,par$cod_usuario);
                            ELSE
                                begin
                                    SELECT OID_USUARIO
                                    INTO var$aoid_usuario(idx)
                                    FROM   SAPR_TUSUARIO
                                    WHERE
                                        upper(DES_LOGIN) = upper(par$ades_login(idx))
                                        ;
                                exception
                                    when no_data_found then
                                        var$aoid_usuario(idx) := NULL;
                                end;

                                IF var$aoid_usuario(idx) is not null THEN
                                    UPDATE SAPR_TUSUARIO
                                    SET
                                        DES_LOGIN = TRIM(UPPER(par$ades_login(idx))),
                                        DES_NOMBRE = par$ades_nombre(idx), 
                                        DES_APELLIDO= par$ades_apellido(idx), 
                                        DES_IDIOMA_PREFERIDO = par$ades_idioma(idx), 
                                        GMT_MODIFICACION = CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), 
                                        DES_USUARIO_MODIFICACION  = par$cod_usuario,
                                        BOL_ACTIVO = par$abol_activo(idx)
                                    WHERE
                                        OID_USUARIO = var$aoid_usuario(idx)
                                    ;
                                END IF;
                            END IF;
                        END IF;

                    END LOOP;
                END IF;
                /*Solo para las altas*/
                IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL THEN
                    /* Recorro el array de usuarios y realizo el insert en SAPR_TROLEXUSUARIO*/
                    IF par$ades_login IS NOT NULL AND par$ades_login.COUNT > 0 THEN
                        FOR idx IN par$ades_login.first .. par$ades_login.last LOOP
                            IF par$ades_login(idx) is not null and par$aoid_role(idx) is not null THEN
                                INSERT INTO SAPR_TROLEXUSUARIO (OID_ROLEXUSUARIO, OID_ROLE, OID_USUARIO, OID_PAIS, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                                    VALUES (SYS_GUID(), par$aoid_role(idx), var$aoid_usuario(idx), par$aoid_pais(idx), 1 ,var$gmt_zero ,par$cod_usuario,CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE) ,par$cod_usuario);
                            END IF;
                        END LOOP;
                    END IF;
                ELSE
                    /* Cuando es una actualización primero inactivamos los rolesxusuarios existentes */
                    IF par$aoid_usuario IS NOT NULL AND par$aoid_usuario.COUNT > 0 THEN
                        FOR idx IN par$aoid_usuario.first .. par$aoid_usuario.last LOOP
                            IF par$aoid_usuario(idx) is not null then
                                UPDATE SAPR_TROLEXUSUARIO
                                    SET BOL_ACTIVO = 0, 
                                        GMT_MODIFICACION = var$gmt_zero, 
                                        DES_USUARIO_MODIFICACION = par$cod_usuario
                                WHERE OID_USUARIO = par$aoid_usuario(idx);
                            END IF;
                        END LOOP;
                    END IF;

                    /* Ahora actualizamos o insertamos */
                    IF par$aoid_usuario IS NOT NULL AND par$aoid_usuario.COUNT > 0 THEN
                        FOR idx IN par$aoid_usuario.first .. par$aoid_usuario.last LOOP
                            IF par$aoid_usuario(idx) is not null AND par$aoid_role(idx) is not null then

                                BEGIN
                                    SELECT COUNT(1)
                                    INTO    var$existe_rolxusr
                                    FROM    SAPR_TROLEXUSUARIO
                                    WHERE
                                        OID_USUARIO = par$aoid_usuario(idx)
                                        AND OID_ROLE = par$aoid_role(idx)
                                        AND OID_PAIS = par$aoid_pais(idx)
                                    ;
                                EXCEPTION
                                    WHEN no_data_found THEN
                                    var$existe_rolxusr := 0
                                    ;
                                END;

                                IF var$existe_rolxusr = 0 THEN
                                    INSERT INTO SAPR_TROLEXUSUARIO 
                                    (OID_ROLEXUSUARIO, OID_ROLE, OID_USUARIO, OID_PAIS, BOL_ACTIVO, 
                                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                                    VALUES (SYS_GUID(), par$aoid_role(idx), var$aoid_usuario(idx), par$aoid_pais(idx), 1 ,
                                    var$gmt_zero ,par$cod_usuario,CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE) ,par$cod_usuario);
                                ELSE
                                    UPDATE SAPR_TROLEXUSUARIO
                                    SET BOL_ACTIVO = 1,
                                    GMT_MODIFICACION = CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
                                    DES_USUARIO_MODIFICACION = par$cod_usuario
                                    WHERE
                                        OID_USUARIO = par$aoid_usuario(idx)
                                        AND OID_ROLE = par$aoid_role(idx)
                                        AND OID_PAIS = par$aoid_pais(idx)
                                    ;
                                END IF;
                            END IF;
                        END LOOP;
                    END IF;
                END IF;

            ELSIF par$cod_accion = 'BAJA' THEN
                /* Damos de baja el ususario y los roles por usuario*/
                IF par$aoid_usuario IS NOT NULL AND par$aoid_usuario.COUNT > 0 THEN
                    FOR idx IN par$aoid_usuario.first .. par$aoid_usuario.last LOOP
                        IF par$aoid_usuario(idx) is not null then
                            UPDATE SAPR_TUSUARIO
                                SET BOL_ACTIVO = 0, 
                                    GMT_MODIFICACION = var$gmt_zero, 
                                    DES_USUARIO_MODIFICACION = par$cod_usuario
                            WHERE OID_USUARIO = par$aoid_usuario(idx);

                            UPDATE SAPR_TROLEXUSUARIO
                                SET BOL_ACTIVO = 0,
                                    GMT_MODIFICACION = var$gmt_zero, 
                                    DES_USUARIO_MODIFICACION = par$cod_usuario
                            WHERE OID_USUARIO = par$aoid_usuario(idx);

                        END IF;
                    END LOOP;
                END IF;
            END IF;

            /* Insertamos mensaje de éxito*/
            /*msgExito - Registro actualizado con éxito.*/
            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgExito',
            gepr_pcomon_###VERSION###.const$CodFunConfigurarUsers,
            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
            NULL,
            0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgExito',
                var$mensaje,
                'VALIDACIONES_USER',
                gepr_pcomon_###VERSION###.const$codTipoExito
                ); 

            COMMIT;
        END IF;


        /* Poblamos el cursor de validaciones*/
        OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO,
                COD_CAMPO2 AS DESCRIPCION,
                COD_CALIFICADOR AS CALIFICADOR,
                NEL_CAMPO5 AS TIPO_MENSAJE
            FROM SAPR_GTT_TAUXILIAR AUX
            WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES_USER';


    EXCEPTION WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK');
        ROLLBACK;
        v_code := SQLCODE;
        v_errm := SQLERRM;

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
            VALUES (
                'msgRollback',
                'Error code: ' || v_code || ' - Message: ' ||  v_errm,
                'VALIDACIONES_USER',
                gepr_pcomon_###VERSION###.const$codTipoErrorAplicacion
                ); 

        /* Poblamos el cursor de validaciones*/
        OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO,
                COD_CAMPO2 AS DESCRIPCION,
                COD_CALIFICADOR AS CALIFICADOR,
                NEL_CAMPO5 AS TIPO_MENSAJE
            FROM SAPR_GTT_TAUXILIAR AUX
            WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES_USER';
    END sconfigurar_usuario;

    /*Valida los datos ingresados en el procedure sconfigurar_usuario */
    PROCEDURE svalidar_usuarios( 
                                par$cod_accion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aoid_usuario            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ades_login              IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$ades_nombre             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$ades_apellido           IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$aoid_pais               IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$aoid_role               IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$cod_cultura IN  gepr_pcomon_###VERSION###.tipo$cod_) IS

                                var$mensaje     gepr_pcomon_###VERSION###.tipo$desc_;
                                var$existe      gepr_pcomon_###VERSION###.tipo$nel_;

    BEGIN
        IF par$cod_accion = 'ALTA' OR par$cod_accion IS NULL THEN
            IF par$ades_login IS NULL THEN
                /*msgDesLoginSinValores - No hay usuarios para dar de alta.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgDesLoginSinValores',
                'ALTA_USUARIO',
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                VALUES (
                    'msgDesLoginSinValores',
                    var$mensaje,
                    'VALIDACIONES_USER',
                    gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                    );

            ELSE
                IF par$ades_login IS NOT NULL AND par$ades_login.COUNT > 0 THEN
                    FOR idx IN par$ades_login.first .. par$ades_login.last LOOP
                        IF par$ades_login (idx) is not null THEN
                            BEGIN
                                SELECT COUNT(1)
                                INTO var$existe
                                FROM SAPR_TUSUARIO
                                WHERE trim(upper(DES_LOGIN)) = trim(upper(par$ades_login(idx)));
                            EXCEPTION
                                WHEN OTHERS THEN
                                    var$existe := 0;
                            END;
                            IF var$existe <> 0 THEN

                                /*msgDesLoginExistente - El login {0} del usuario ya existe en la base de datos.*/
                                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgDesLoginExistente',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarUsers,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                par$ades_login(idx),
                                0);

                                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                                VALUES (
                                    'msgDesLoginExistente',
                                    var$mensaje,
                                    'VALIDACIONES_USER',
                                    gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                                );
                            END IF;
                        END IF;
                    END LOOP;
                END IF;
            END IF;
        ELSIF par$cod_accion = 'MODIFICAR' then
            IF par$ades_login IS NOT NULL AND par$ades_login.COUNT > 0 THEN
                FOR idx IN par$ades_login.first .. par$ades_login.last LOOP
                    IF par$ades_login (idx) is not null THEN
                        BEGIN
                            SELECT COUNT(1)
                            INTO var$existe
                            FROM SAPR_TUSUARIO
                            WHERE trim(upper(DES_LOGIN)) = trim(upper(par$ades_login(idx)))
                            AND OID_USUARIO <> par$aoid_usuario(idx);
                        EXCEPTION
                            WHEN OTHERS THEN
                                var$existe := 0;
                        END;
                        IF var$existe <> 0 THEN
                            /*msgDesLoginExistente - El login {0} del usuario ya existe en la base de datos.*/
                            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgDesLoginExistente',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarUsers,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            par$ades_login(idx),
                            0);

                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                                'msgDesLoginExistente',
                                var$mensaje,
                                'VALIDACIONES_USER',
                                gepr_pcomon_###VERSION###.const$codTipoErrorNegocio
                            );
                        END IF;
                    END IF;
                END LOOP;
            END IF;          
        END IF;
    END svalidar_usuarios;

/* Procedure que devuelve los usuarios*/
    PROCEDURE srecuperar_usuario(par$cod_pais IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_role IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$des_login IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$modo_detallado      IN  gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$rc_usuarios OUT sys_refcursor) IS
    BEGIN
       /*Inicializamos los cursores */
        OPEN par$rc_usuarios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

        IF par$modo_detallado IS NULL OR par$modo_detallado = 0 THEN
            /* Modo no detallado */
            OPEN par$rc_usuarios FOR
                SELECT
                    DISTINCT
                    U.OID_USUARIO, U.DES_LOGIN, U.DES_NOMBRE, U.DES_APELLIDO, nvl(U.DES_IDIOMA_PREFERIDO, 'es') DES_IDIOMA_PREFERIDO, U.BOL_ACTIVO BOL_ACTIVO_USUARIO,
                    P.OID_PAIS, P.DES_PAIS, P.COD_PAIS, 
                    R.OID_ROLE, R.COD_ROLE, R.DES_ROLE
                FROM SAPR_TUSUARIO U
                LEFT JOIN SAPR_TROLEXUSUARIO TRO ON (TRO.OID_USUARIO = U.OID_USUARIO AND TRO.BOL_ACTIVO = 1)
                LEFT JOIN GEPR_TPAIS P ON P.OID_PAIS = TRO.OID_PAIS
                LEFT JOIN SAPR_TROLE R ON R.OID_ROLE = TRO.OID_ROLE
                WHERE
                    UPPER(U.DES_LOGIN) LIKE'%' || UPPER(par$des_login) || '%' AND
                    (UPPER(R.COD_ROLE) LIKE'%' || UPPER(par$cod_role) || '%' OR R.COD_ROLE IS NULL) AND
                    (UPPER(P.COD_PAIS) LIKE'%' || UPPER(par$cod_pais) || '%' OR P.COD_PAIS IS NULL)
                    --AND U.BOL_ACTIVO = 1
                    ;
        ELSE
            /* Modo detallado: Usuarios junto con los roles */
            OPEN par$rc_usuarios FOR
                SELECT DISTINCT
                    U.OID_USUARIO, U.DES_LOGIN, U.DES_NOMBRE, U.DES_APELLIDO, nvl(U.DES_IDIOMA_PREFERIDO, 'es') DES_IDIOMA_PREFERIDO,U.BOL_ACTIVO BOL_ACTIVO_USUARIO,
                    P.OID_PAIS, P.DES_PAIS, P.COD_PAIS,
                    R.OID_ROLE, R.COD_ROLE, R.DES_ROLE,
                    TRO.OID_ROLEXUSUARIO, TRO.BOL_ACTIVO BOL_ACTIVO_ROLEXUSR,
                    A.OID_APLICACION, A.COD_APLICACION, A.DES_APLICACION, A.BOL_ACTIVO BOL_ACTIVO_APLICACION
                FROM SAPR_TUSUARIO U
                    LEFT JOIN SAPR_TROLEXUSUARIO TRO ON (TRO.OID_USUARIO = U.OID_USUARIO AND TRO.BOL_ACTIVO = 1)
                    LEFT JOIN GEPR_TPAIS P ON P.OID_PAIS = TRO.OID_PAIS
                    LEFT JOIN SAPR_TROLE R ON R.OID_ROLE = TRO.OID_ROLE AND R.BOL_ACTIVO = 1
                    LEFT JOIN SAPR_TPERMISOXROLE PERXROL ON PERXROL.OID_ROLE = R.OID_ROLE
                    LEFT JOIN SAPR_TPERMISO PER ON PER.OID_PERMISO = PERXROL.OID_PERMISO
                    LEFT JOIN SAPR_TAPLICACION A ON A.OID_APLICACION = PER.OID_APLICACION
                WHERE
                    UPPER(U.DES_LOGIN) = UPPER(par$des_login)
                    --AND TRO.BOL_ACTIVO = 1       
            ;
        END IF;
    END srecuperar_usuario;

   /*Función que corrobora si hay error para el procedure sconfigurar_usuario*/
    FUNCTION fhay_error_user RETURN BOOLEAN IS
        var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    BEGIN

        BEGIN
            SELECT COUNT(1)
                INTO var$existe
                FROM SAPR_GTT_TAUXILIAR A
            WHERE A.COD_CALIFICADOR = 'VALIDACIONES_USER' and A.NEL_CAMPO5 <> gepr_pcomon_###VERSION###.const$codTipoExito;
        EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
        END;

        RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

    END fhay_error_user;

    /*Función que corrobora si hay error para el procedure sconfigurar_role*/
    FUNCTION fhay_error RETURN BOOLEAN IS
        var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    BEGIN

        BEGIN
            SELECT COUNT(1)
                INTO var$existe
                FROM SAPR_GTT_TAUXILIAR A
            WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and A.NEL_CAMPO5 <> gepr_pcomon_###VERSION###.const$codTipoExito;
        EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
        END;

        RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

    END fhay_error;

END SAPR_PUSUARIO_###VERSION###;
/