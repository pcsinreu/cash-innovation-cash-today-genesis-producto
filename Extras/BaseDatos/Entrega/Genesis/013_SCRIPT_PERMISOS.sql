DECLARE
  var$existe NUMBER;
  var$oid_app_iac VARCHAR2(36);
  var$oid_role VARCHAR2(36);
  var$oid_permiso VARCHAR2(36);
BEGIN    

/* Se busca datos comunes */
    BEGIN
        SELECT OID_APLICACION
        INTO var$oid_app_iac 
        FROM SAPR_TAPLICACION 
        WHERE COD_APLICACION = 'IAC';
    EXCEPTION
        WHEN no_data_found THEN
        var$oid_app_iac := NULL;
    END;
    
    BEGIN
        SELECT OID_ROLE
        INTO var$oid_role
        FROM SAPR_TROLE
        WHERE COD_ROLE = 'Administrador';
    EXCEPTION
        WHEN no_data_found THEN
        var$oid_role := NULL;
    END;

    /* Verificación de datos comunes*/
    IF var$oid_app_iac IS NOT NULL AND var$oid_role IS NOT NULL THEN    

    /***************************************************/
    /*         Creación de permisos de ROLES           */
    /***************************************************/

    /* verificación y creación de permiso de CONSULTAR */

        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_ROLES_CONSULTAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_app_iac, 'CONFIGURACION_ROLES_CONSULTAR', 'CONFIGURACION_ROLES_CONSULTAR', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_ROLES_CONSULTAR'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de ALTA */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_ROLES_DAR_ALTA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_ROLES_DAR_ALTA', 'CONFIGURACION_ROLES_DAR_ALTA', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_ROLES_DAR_ALTA'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de MODIFICAR */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_ROLES_MODIFICAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_ROLES_MODIFICAR', 'CONFIGURACION_ROLES_MODIFICAR', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;        
        
        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_ROLES_MODIFICAR'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de BAJA */   
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_ROLES_DAR_BAJA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_ROLES_DAR_BAJA', 'CONFIGURACION_ROLES_DAR_BAJA', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_ROLES_DAR_BAJA'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

        /***************************************************/
        /*         Creación de permisos de USUARIO         */
        /***************************************************/

        /* verificación y creación de permiso de CONSULTAR */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_USUARIOS_CONSULTAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_app_iac, 'CONFIGURACION_USUARIOS_CONSULTAR', 'CONFIGURACION_USUARIOS_CONSULTAR', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_USUARIOS_CONSULTAR'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de ALTA */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_USUARIOS_DAR_ALTA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_USUARIOS_DAR_ALTA', 'CONFIGURACION_USUARIOS_DAR_ALTA', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_USUARIOS_DAR_ALTA'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de MODIFICAR */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_USUARIOS_MODIFICAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_USUARIOS_MODIFICAR', 'CONFIGURACION_USUARIOS_MODIFICAR', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;        
        
        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_USUARIOS_MODIFICAR'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;

    /* verificación y creación de permiso de BAJA */   
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'CONFIGURACION_USUARIOS_DAR_BAJA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'CONFIGURACION_USUARIOS_DAR_BAJA', 'CONFIGURACION_USUARIOS_DAR_BAJA', '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_PERMISO FROM SAPR_TPERMISO WHERE OID_APLICACION = var$oid_app_iac AND COD_PERMISO = 'CONFIGURACION_USUARIOS_DAR_BAJA'), var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS', SYSTIMESTAMP, 'SCRIPT_PERMISOS_ROLES_USUARIOS');
        END IF;


    /***************************************************/
    /*         Creación de permisos de GRUPO_CLIENTE           */
    /***************************************************/

    /* verificación y creación de permiso de CONSULTAR */

        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'GRUPO_CLIENTE_CONSULTAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_app_iac, 'GRUPO_CLIENTE_CONSULTAR', 'GRUPO_CLIENTE_CONSULTAR', '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_permiso, var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE');
        END IF;

    /* verificación y creación de permiso de ALTA */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'GRUPO_CLIENTE_DAR_ALTA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'GRUPO_CLIENTE_DAR_ALTA', 'GRUPO_CLIENTE_DAR_ALTA', '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_permiso, var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE');
        END IF;

    /* verificación y creación de permiso de MODIFICAR */
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'GRUPO_CLIENTE_MODIFICAR' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'GRUPO_CLIENTE_MODIFICAR', 'GRUPO_CLIENTE_MODIFICAR', '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;        
        
        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_permiso, var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE');
        END IF;

    /* verificación y creación de permiso de BAJA */   
        BEGIN
            SELECT OID_PERMISO
            INTO var$oid_permiso
            FROM SAPR_TPERMISO
            WHERE COD_PERMISO = 'GRUPO_CLIENTE_DAR_BAJA' AND OID_APLICACION = var$oid_app_iac;
        EXCEPTION
            WHEN no_data_found THEN
            var$oid_permiso := NULL;
        END;

        IF var$oid_permiso IS NULL THEN        
            INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), (SELECT OID_APLICACION FROM SAPR_TAPLICACION WHERE COD_APLICACION = 'IAC'), 'GRUPO_CLIENTE_DAR_BAJA', 'GRUPO_CLIENTE_DAR_BAJA', '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE')
            RETURNING OID_PERMISO INTO var$oid_permiso;
        END IF;

        BEGIN
            SELECT count(1)
            INTO var$existe
            FROM SAPR_TPERMISOXROLE
            WHERE OID_PERMISO = var$oid_permiso
            AND OID_ROLE = var$oid_role;
        EXCEPTION
            WHEN no_data_found THEN
            var$existe := 0;    
        END;

        IF var$existe = 0 THEN              
            INSERT INTO SAPR_TPERMISOXROLE (OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) 
            VALUES (SYS_GUID(), var$oid_permiso, var$oid_role, '1', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE', SYSTIMESTAMP, 'SCRIPT_GRUPO_CLIENTE');
        END IF;

        
        COMMIT;
    END IF;

EXCEPTION
WHEN OTHERS THEN
  ROLLBACK;
  RAISE;
END;
/
