DECLARE
    PERMISOS_OWNER      varchar2(50);
BEGIN

    PERMISOS_OWNER := 'YYYY';

	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TAPLICACION FOR ' || PERMISOS_OWNER || '.ADPR_TAPLICACION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TAPLICACION_VERSION FOR ' || PERMISOS_OWNER || '.ADPR_TAPLICACION_VERSION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TCONVERSION_LEGADO FOR ' || PERMISOS_OWNER || '.ADPR_TCONVERSION_LEGADO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TDELE_TIPO_ROLE_USR FOR ' || PERMISOS_OWNER || '.ADPR_TDELE_TIPO_ROLE_USR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TDELEGACION FOR ' || PERMISOS_OWNER || '.ADPR_TDELEGACION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TDELEGACION_TIPO_SECTOR FOR ' || PERMISOS_OWNER || '.ADPR_TDELEGACION_TIPO_SECTOR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TDELEGACION_VERSION FOR ' || PERMISOS_OWNER || '.ADPR_TDELEGACION_VERSION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TGRUPO_USUARIO FOR ' || PERMISOS_OWNER || '.ADPR_TGRUPO_USUARIO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TGRUPO_VERSION FOR ' || PERMISOS_OWNER || '.ADPR_TGRUPO_VERSION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TNEGOCIO FOR ' || PERMISOS_OWNER || '.ADPR_TNEGOCIO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TPAIS FOR ' || PERMISOS_OWNER || '.ADPR_TPAIS';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TPERMISO FOR ' || PERMISOS_OWNER || '.ADPR_TPERMISO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TPLANTA FOR ' || PERMISOS_OWNER || '.ADPR_TPLANTA';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TPLANTA_TIPO_SECTOR FOR ' || PERMISOS_OWNER || '.ADPR_TPLANTA_TIPO_SECTOR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TROLE FOR ' || PERMISOS_OWNER || '.ADPR_TROLE';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TROLEXDELEGACIONXSECTOR FOR ' || PERMISOS_OWNER || '.ADPR_TROLEXDELEGACIONXSECTOR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TROLEXPERMISO FOR ' || PERMISOS_OWNER || '.ADPR_TROLEXPERMISO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TSECTOR FOR ' || PERMISOS_OWNER || '.ADPR_TSECTOR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TSERVIDOR_DELEGACION FOR ' || PERMISOS_OWNER || '.ADPR_TSERVIDOR_DELEGACION';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TTIPO_SECTOR FOR ' || PERMISOS_OWNER || '.ADPR_TTIPO_SECTOR';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TTOKEN_ACCESO FOR ' || PERMISOS_OWNER || '.ADPR_TTOKEN_ACCESO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TUSUARIO FOR ' || PERMISOS_OWNER || '.ADPR_TUSUARIO';
	execute immediate 'CREATE OR REPLACE SYNONYM ADPR_TUSUARIO_VERSION FOR ' || PERMISOS_OWNER || '.ADPR_TUSUARIO_VERSION';
	execute immediate 'CREATE OR REPLACE SYNONYM CPPR_TLOG FOR ' || PERMISOS_OWNER || '.CPPR_TLOG';
	execute immediate 'CREATE OR REPLACE SYNONYM CPPR_TLOG_ERROR FOR ' || PERMISOS_OWNER || '.CPPR_TLOG_ERROR';
	execute immediate 'CREATE OR REPLACE SYNONYM CPPR_TSESION FOR ' || PERMISOS_OWNER || '.CPPR_TSESION';




EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: SCRIPT_SYNONYMS_GENESIS_IAC.sql Script: GRANTS SYNONYMS GENESIS IAC' ||
                            sqlerrm);
    RAISE;
END;
/

 DECLARE
    OWNER_OBJECTS VARCHAR2(30) := '';
    ExisteTabla NUMBER;
  
 BEGIN
    SELECT sys_context( 'userenv', 'current_schema' ) INTO OWNER_OBJECTS FROM DUAL;
    
    /*==============================================================*/
    /* Table: SAPR_TAPLICACION                                      */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TAPLICACION';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TAPLICACION');
        EXECUTE IMMEDIATE q'[create table SAPR_TAPLICACION 
        (
            OID_APLICACION              VARCHAR2(36)                not null,
            COD_APLICACION              VARCHAR2(20)                not null,
            DES_APLICACION              VARCHAR2(50)                not null,
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TAPLICACION primary key (OID_APLICACION),
            constraint AK_SAPR_TAPLICACION unique (COD_APLICACION)
        )]';
    END IF;

    /*==============================================================*/
    /* Table: SAPR_TPERMISO                                         */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPERMISO';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPERMISO');
        EXECUTE IMMEDIATE q'[create table SAPR_TPERMISO 
        (
            OID_PERMISO                 VARCHAR2(36)                not null,
            OID_APLICACION              VARCHAR2(36)                not null,
            COD_PERMISO                 VARCHAR2(100)               not null,
            DES_PERMISO                 VARCHAR2(255)               not null,
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TPERMISO primary key (OID_PERMISO),
            constraint AK_SAPR_TPERMISO unique (COD_PERMISO,OID_APLICACION),
            constraint FK_SAPR_TPERMISO_01 foreign key (OID_APLICACION)
                references SAPR_TAPLICACION (OID_APLICACION)
        )]';
    END IF;

    /*==============================================================*/
    /* Table: SAPR_TROLE                                            */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TROLE';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TROLE');
        EXECUTE IMMEDIATE q'[create table SAPR_TROLE 
        (
            OID_ROLE                    VARCHAR2(36)                not null,
            COD_ROLE                    VARCHAR2(50)                not null,
            DES_ROLE                    VARCHAR2(255)               not null,
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TROLE primary key (OID_ROLE),
            constraint AK_SAPR_TROLE unique (COD_ROLE)
        )]';
    END IF;
  
    /*==============================================================*/
    /* Table: SAPR_TPERMISOXROLE                                    */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPERMISOXROLE';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TPERMISOXROLE');
        EXECUTE IMMEDIATE q'[create table SAPR_TPERMISOXROLE
        (
            OID_PERMISOXROLE            VARCHAR2(36)                not null,
            OID_PERMISO                 VARCHAR2(36)                not null,
            OID_ROLE                    VARCHAR2(36)                not null,
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TPERMISOXROLE primary key (OID_PERMISOXROLE),
            constraint FK_SAPR_TPERMISOXROLE_01 foreign key (OID_PERMISO)
                references SAPR_TPERMISO (OID_PERMISO),
            constraint FK_SAPR_TPERMISOXROLE_02 foreign key (OID_ROLE)
                references SAPR_TROLE (OID_ROLE)
        )]';
    END IF;

    /*==============================================================*/
    /* Table: SAPR_TUSUARIO                                         */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TUSUARIO';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TUSUARIO');
        EXECUTE IMMEDIATE q'[create table SAPR_TUSUARIO 
        (
            OID_USUARIO                 VARCHAR2(36)                not null,
            DES_LOGIN                   VARCHAR2(20)                not null,
            DES_NOMBRE                  VARCHAR2(50)                not null,
            DES_APELLIDO                VARCHAR2(50),
            DES_IDIOMA_PREFERIDO        VARCHAR2(5),
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TUSUARIO primary key (OID_USUARIO),
            constraint AK_SAPR_TUSUARIO unique (DES_LOGIN)
        )]';
    END IF;

    /*==============================================================*/
    /* Table: SAPR_TROLEXUSUARIO                                    */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TROLEXUSUARIO';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TROLEXUSUARIO');
        EXECUTE IMMEDIATE q'[create table SAPR_TROLEXUSUARIO
        (
            OID_ROLEXUSUARIO            VARCHAR2(36)                not null,
            OID_ROLE                    VARCHAR2(36)                not null,
            OID_USUARIO                 VARCHAR2(36)                not null,
            OID_PAIS                    VARCHAR2(36)                not null,
            BOL_ACTIVO                  NUMBER(1,0)                 not null,
            GMT_CREACION                TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_CREACION        VARCHAR2(50)                not null,
            GMT_MODIFICACION            TIMESTAMP(6) WITH TIME ZONE not null,
            DES_USUARIO_MODIFICACION    VARCHAR2(50)                not null,
            constraint PK_SAPR_TROLEXUSUARIO primary key (OID_ROLEXUSUARIO),
            constraint FK_SAPR_TROLEXUSUARIO_01 foreign key (OID_ROLE)
                references SAPR_TROLE (OID_ROLE),
            constraint FK_SAPR_TROLEXUSUARIO_02 foreign key (OID_USUARIO)
                references SAPR_TUSUARIO (OID_USUARIO),
            constraint FK_SAPR_TROLEXUSUARIO_03 foreign key (OID_PAIS)
                references GEPR_TPAIS (OID_PAIS)
        )]';
    END IF;

    /*==============================================================*/
    /* Table: SAPR_TTOKEN_ACCESO                                    */
    /*==============================================================*/
    /* Busco si ya existe la tabla*/
    SELECT COUNT(1) INTO ExisteTabla FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TTOKEN_ACCESO';

    IF ExisteTabla = 0 THEN
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TTOKEN_ACCESO');
        EXECUTE IMMEDIATE q'[create table SAPR_TTOKEN_ACCESO
        (
          OID_TOKEN_ACCESO            VARCHAR2(36)                not null,
          OID_USUARIO                 VARCHAR2(36)                not null,
          OID_APLICACION              VARCHAR2(36)                not null,
          FYH_TOKEN_ACCESO            DATE                        not null,
          DES_PERMISOS_ACCESO         BLOB                        not null,
          DES_CONFIGURACIONES         BLOB                        not null,
          constraint PK_SAPR_TTOKEN_ACCESO primary key (OID_TOKEN_ACCESO),
          constraint FK_SAPR_TTOKEN_ACCESO_01 foreign key (OID_USUARIO)
                references SAPR_TUSUARIO (OID_USUARIO),
          constraint FK_SAPR_TTOKEN_ACCESO_02 foreign key (OID_APLICACION)
                references SAPR_TAPLICACION (OID_APLICACION)
        )]';
    END IF;
EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: SCRIPT_TABLAS_PERMISOS.sql Script: SCRIPT_TABLAS_PERMISOS' ||
                            sqlerrm);
    RAISE;
END;
/

DECLARE
    var$cod_role    VARCHAR2(50);
    var$des_role    VARCHAR2(255);
    var$gmt_zero    VARCHAR2(80);
    var$usuario     VARCHAR2(50);
    var$existe      NUMBER;
BEGIN
    var$usuario := 'GENESIS_PRODUCTO';
    var$gmt_zero := sys_extract_utc(current_timestamp) || ' +00:00';

    /*Rol: Administrador*/
    var$cod_role := 'Administrador';
    var$des_role := 'El rol de administrador tendrá acceso a toda la aplicación, sin restricciones.';
    
    /* Buscamos si existe el rol */
    BEGIN
        SELECT COUNT(1) INTO var$existe FROM SAPR_TROLE WHERE COD_ROLE = var$cod_role;
    EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
    END;
    IF var$existe = 0 THEN
        INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
        values (SYS_GUID(), var$cod_role, var$des_role, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
    END IF;

    /*Rol: General*/
    var$cod_role := 'General';
    var$des_role := 'El rol General tendrá acceso a consulta y mantenimiento de entidades y transacciones.';
    
    /* Buscamos si existe el rol */
    BEGIN
        SELECT COUNT(1) INTO var$existe FROM SAPR_TROLE WHERE COD_ROLE = var$cod_role;
    EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
    END;
    IF var$existe = 0 THEN
        INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
        values (SYS_GUID(), var$cod_role, var$des_role, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
    END IF;

    /*Rol: Consulta Transacciones*/
    var$cod_role := 'Consulta Transacciones';
    var$des_role := 'Este rol tendrá acceso a consultas en el modulo de Nuevos Saldos.';
    
    /* Buscamos si existe el rol */
    BEGIN
        SELECT COUNT(1) INTO var$existe FROM SAPR_TROLE WHERE COD_ROLE = var$cod_role;
    EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
    END;
    IF var$existe = 0 THEN
        INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
        values (SYS_GUID(), var$cod_role, var$des_role, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
    END IF;

    /*Rol: Aprobador Cuentas Bancarias*/
    var$cod_role := 'Aprobador Cuentas Bancarias';
    var$des_role := 'Este rol tendrá acceso a las pantallas de aprobación y podrá aprobar cambios a cuentas bancarias.';
    
    /* Buscamos si existe el rol */
    BEGIN
        SELECT COUNT(1) INTO var$existe FROM SAPR_TROLE WHERE COD_ROLE = var$cod_role;
    EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
    END;
    IF var$existe = 0 THEN
        INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
        values (SYS_GUID(), var$cod_role, var$des_role, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
    END IF;

    /*Rol: Aprobador Limites*/
    var$cod_role := 'Aprobador Limites';
    var$des_role := 'Este rol podrá acceder a la pantalla de periodos y desbloquear períodos bloqueados por haber superado el límite configurado.';
    
    /* Buscamos si existe el rol */
    BEGIN
        SELECT COUNT(1) INTO var$existe FROM SAPR_TROLE WHERE COD_ROLE = var$cod_role;
    EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
    END;
    IF var$existe = 0 THEN
        INSERT INTO SAPR_TROLE (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
        values (SYS_GUID(), var$cod_role, var$des_role, 1, var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
    END IF;

    COMMIT;
EXCEPTION
    
    WHEN OTHERS THEN
        ROLLBACK;
        raise_application_error(-20001,
                                'Arquivo: SCRIPT_CREAR_ROLES.sql Script: SCRIPT_CREAR_ROLES' ||
                                sqlerrm);
        RAISE;
END;
/

DECLARE
    var$existe      NUMBER;
    var$gmt_zero    VARCHAR2(80);
    var$usuario     VARCHAR2(50);
    var$cod_role    VARCHAR2(50);
    var$oid_role    VARCHAR2(36);
    var$oid_aplicacion VARCHAR2(36);
    var$oid_permiso VARCHAR2(36);

    CURSOR rc_aplicaciones  IS SELECT * FROM ADPR_TAPLICACION;
    CURSOR rc_permisos      IS SELECT * FROM ADPR_TPERMISO;
    CURSOR rc_usuarios      IS SELECT * FROM ADPR_TUSUARIO;
    CURSOR rc_per_x_role    IS 
                                SELECT  
                                    rolxper.OID_PERMISO, 
                                    rol.cod_role,
                                    apli.COD_APLICACION,
                                    apli.oid_aplicacion,
                                    PERMISO.COD_PERMISO
                                FROM
                                    adpr_trole rol
                                INNER JOIN  ADPR_TROLEXPERMISO rolxper ON rol.oid_role = rolxper.oid_role
                                INNER JOIN ADPR_TAPLICACION apli ON apli.oid_aplicacion = rol.oid_aplicacion
                                INNER JOIN ADPR_TPERMISO PERMISO ON PERMISO.OID_PERMISO = rolxper.OID_PERMISO;

    const$TesterQA CONSTANT  VARCHAR2(50) := 'TESTER_APROBACION'; /* Código de Permiso */
    const$VerTransac CONSTANT VARCHAR2(50) := 'SALDOS_CONSULTAR_TRANSACCIONES'; /* Código de permiso Consulta transacciones */
    const$apr_cuentas CONSTANT VARCHAR2(50) := 'APROBACION_CUENTAS_BANCARIAS'; /* Código de Permiso */
    const$apr_limites CONSTANT VARCHAR2(50) := 'APROBADOR_LIMITES'; /* Código de Permiso */
BEGIN
    var$usuario := 'GENESIS_PRODUCTO';
    var$gmt_zero := sys_extract_utc(current_timestamp) || ' +00:00';

    /* Aplicaciones */
    FOR app in rc_aplicaciones LOOP
        /* Verificamos si la aplicacion ya existe en la nueva tabla. */ 
        SELECT COUNT(1)
            INTO var$existe
        FROM SAPR_TAPLICACION
        WHERE COD_APLICACION = app.COD_APLICACION;

        /* En caso de que no exista migramos la aplicacion. */ 
        IF var$existe = 0 THEN
              SELECT COUNT(1)
                INTO var$existe
                FROM SAPR_TPERMISO
                WHERE OID_APLICACION = app.OID_APLICACION;
            IF var$existe = 0 THEN
                INSERT INTO SAPR_TAPLICACION (OID_APLICACION, COD_APLICACION, DES_APLICACION, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (app.OID_APLICACION, app.COD_APLICACION, app.DES_APLICACION, 1,
                    var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
              ELSE 
                INSERT INTO SAPR_TAPLICACION (OID_APLICACION, COD_APLICACION, DES_APLICACION, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (sys_guid(), app.COD_APLICACION, app.DES_APLICACION, 1,
                    var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
            END IF; 
        END IF;
    END LOOP;

    /* Permisos */
    FOR per in rc_permisos LOOP
        /* Verificamos si el permiso ya existe en la nueva tabla. */ 
        SELECT COUNT(1)
            INTO var$existe
        FROM SAPR_TPERMISO
        WHERE COD_PERMISO = per.COD_PERMISO AND OID_APLICACION = per.OID_APLICACION;

        /* En caso de que no exista migramos el permiso. */ 
        IF var$existe = 0 THEN
               SELECT COUNT(1)
                INTO var$existe
                FROM SAPR_TPERMISO
                WHERE OID_PERMISO = per.OID_PERMISO;
            IF var$existe = 0 THEN
                INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (per.OID_PERMISO, per.OID_APLICACION, per.COD_PERMISO, per.COD_PERMISO, 1,
                    var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
             ELSE 
                INSERT INTO SAPR_TPERMISO (OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (sys_guid(), per.OID_APLICACION, per.COD_PERMISO, per.COD_PERMISO, 1,
                    var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
            END IF; 
        END IF;
    END LOOP;

    /* Usuarios */
    FOR usr in rc_usuarios LOOP
        /* Verificamos si el usuario ya existe en la nueva tabla. */ 
        SELECT COUNT(1)
            INTO var$existe
        FROM SAPR_TUSUARIO
        WHERE DES_LOGIN = usr.DES_LOGIN;

        /* En caso de que no exista migramos el usuario. */ 
        IF var$existe = 0 THEN

             SELECT COUNT(1)
                INTO var$existe
                FROM SAPR_TUSUARIO
                WHERE OID_USUARIO = usr.OID_USUARIO;
            IF var$existe = 0 THEN
                INSERT INTO SAPR_TUSUARIO (OID_USUARIO, DES_LOGIN, DES_NOMBRE, DES_APELLIDO, DES_IDIOMA_PREFERIDO, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (usr.OID_USUARIO, usr.DES_LOGIN, usr.DES_NOMBRE, usr.DES_APELLIDO, usr.DES_IDIOMA_PREFERIDO, usr.BOL_ACTIVO,
                    usr.FYH_ALTA, usr.COD_USUARIO_ALTA, usr.FYH_ACTUALIZACION, usr.COD_USUARIO_ACTUALIZACION);
            ELSE 
                INSERT INTO SAPR_TUSUARIO (OID_USUARIO, DES_LOGIN, DES_NOMBRE, DES_APELLIDO, DES_IDIOMA_PREFERIDO, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                VALUES (sys_guid(), usr.DES_LOGIN, usr.DES_NOMBRE, usr.DES_APELLIDO, usr.DES_IDIOMA_PREFERIDO, usr.BOL_ACTIVO,
                    usr.FYH_ALTA, usr.COD_USUARIO_ALTA, usr.FYH_ACTUALIZACION, usr.COD_USUARIO_ACTUALIZACION);
            END IF; 

        END IF;
    END LOOP;

    /* Permisos por Roles */
    BEGIN

        /* Rol Administrador */
        BEGIN
            var$cod_role := 'Administrador';
            BEGIN
                SELECT OID_ROLE
                INTO var$oid_role
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN others THEN
                    raise_application_error(-20001,
                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                    'No encontró el rol: ' || var$cod_role  ||
                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
            END;
            
            FOR rolxper IN rc_per_x_role LOOP
                /* Verificamos que sea distinto del permiso de TESTER_APROBACION */
                IF rolxper.COD_ROLE = var$cod_role AND rolxper.cod_permiso <> const$TesterQA THEN
                    BEGIN
                        SELECT OID_APLICACION
                        INTO var$oid_aplicacion
                        FROM SAPR_TAPLICACION
                        WHERE COD_APLICACION = rolxper.COD_APLICACION;
                        EXCEPTION
                        WHEN others THEN
                            raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                            'No encontró la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TAPLICACION' ||
                            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
                    END;

                    BEGIN
                        SELECT count(1)
                        INTO var$existe
                        FROM SAPR_TPERMISOXROLE perxrol
                        INNER JOIN SAPR_TPERMISO permiso ON permiso.oid_permiso = perxrol.oid_permiso
                        INNER JOIN SAPR_TROLE ROL ON ROL.OID_ROLE = perxrol.OID_ROLE
                        WHERE
                            permiso.OID_APLICACION = var$oid_aplicacion
                            AND PERMISO.COD_PERMISO = rolxper.COD_PERMISO
                            AND ROL.COD_ROLE = var$cod_role;
                        EXCEPTION
                          when no_data_found then
                            var$existe := 0;
                    END;

                    /* Creamos el permiso por rol */
                    IF var$existe = 0 THEN
                        /* Buscamos el OID_PERMISO */
                        BEGIN
                            SELECT
                                OID_PERMISO
                            INTO
                                var$oid_permiso
                            FROM
                                SAPR_TPERMISO
                            WHERE
                                COD_PERMISO = rolxper.COD_PERMISO
                                AND OID_APLICACION = var$oid_aplicacion;
                            EXCEPTION
                                WHEN OTHERS THEN
                                    raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                                    'No encontró el permiso: ' || rolxper.COD_PERMISO || ' para la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TPERMISO' ||
                                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);                                
                        END;

                        INSERT INTO SAPR_TPERMISOXROLE
                        (
                            OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, 
                            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                        )
                        VALUES
                        (
                            SYS_GUID(), var$oid_permiso, var$oid_role, 1,
                            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
                        );
                    END IF;


                END IF;
            END LOOP;
        END;

        /* Rol General */
        BEGIN
            var$cod_role := 'General';

            BEGIN
                SELECT OID_ROLE
                INTO var$oid_role
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN others THEN
                    raise_application_error(-20001,
                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                    'No encontró el rol: ' || var$cod_role  ||
                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
            END;

            FOR rolxper IN rc_per_x_role LOOP
                /* Filtramos solo aquellos permisos que pertenezcan al ROL General */
                IF rolxper.COD_ROLE = var$cod_role AND rolxper.cod_permiso <> const$TesterQA THEN
                    BEGIN
                        SELECT OID_APLICACION
                        INTO var$oid_aplicacion
                        FROM SAPR_TAPLICACION
                        WHERE COD_APLICACION = rolxper.COD_APLICACION;
                        EXCEPTION
                        WHEN others THEN
                            raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                            'No encontró la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TAPLICACION' ||
                            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
                    END;

                    BEGIN
                        SELECT count(1)
                        INTO var$existe
                        FROM SAPR_TPERMISOXROLE perxrol
                        INNER JOIN SAPR_TPERMISO permiso ON permiso.oid_permiso = perxrol.oid_permiso
                        INNER JOIN SAPR_TROLE ROL ON ROL.OID_ROLE = perxrol.OID_ROLE
                        WHERE
                            permiso.OID_APLICACION = var$oid_aplicacion
                            AND PERMISO.COD_PERMISO = rolxper.COD_PERMISO
                            AND ROL.COD_ROLE = var$cod_role;
                        EXCEPTION
                          when no_data_found then
                            var$existe := 0;
                    END;

                    /* Creamos el permiso por rol */
                    IF var$existe = 0 THEN
                        /* Buscamos el OID_PERMISO */
                        BEGIN
                            SELECT
                                OID_PERMISO
                            INTO
                                var$oid_permiso
                            FROM
                                SAPR_TPERMISO
                            WHERE
                                COD_PERMISO = rolxper.COD_PERMISO
                                AND OID_APLICACION = var$oid_aplicacion;
                            EXCEPTION
                                WHEN OTHERS THEN
                                    raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                                    'No encontró el permiso: ' || rolxper.COD_PERMISO || ' para la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TPERMISO' ||
                                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);                                
                        END;

                        INSERT INTO SAPR_TPERMISOXROLE
                        (
                            OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, 
                            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                        )
                        VALUES
                        (
                            SYS_GUID(), var$oid_permiso, var$oid_role, 1,
                            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
                        );
                    END IF;


                END IF;
            END LOOP;
        END;

        /* Rol Consulta Transacciones */
        BEGIN
            var$cod_role := 'Consulta Transacciones';
            BEGIN
                SELECT OID_ROLE
                INTO var$oid_role
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN others THEN
                    raise_application_error(-20001,
                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                    'No encontró el rol: ' || var$cod_role  ||
                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
            END;

            FOR rolxper IN rc_per_x_role LOOP
                /* Verificamos que sea del permiso de SALDOS_CONSULTAR_TRANSACCIONES */
                IF rolxper.COD_ROLE = var$cod_role AND rolxper.cod_permiso = const$VerTransac THEN
                    BEGIN
                        SELECT OID_APLICACION
                        INTO var$oid_aplicacion
                        FROM SAPR_TAPLICACION
                        WHERE COD_APLICACION = rolxper.COD_APLICACION;
                        EXCEPTION
                        WHEN others THEN
                            raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                            'No encontró la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TAPLICACION' ||
                            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
                    END;

                    BEGIN
                        SELECT count(1)
                        INTO var$existe
                        FROM SAPR_TPERMISOXROLE perxrol
                        INNER JOIN SAPR_TPERMISO permiso ON permiso.oid_permiso = perxrol.oid_permiso
                        INNER JOIN SAPR_TROLE ROL ON ROL.OID_ROLE = perxrol.OID_ROLE
                        WHERE
                            permiso.OID_APLICACION = var$oid_aplicacion
                            AND PERMISO.COD_PERMISO = rolxper.COD_PERMISO
                            AND ROL.COD_ROLE = var$cod_role;
                        EXCEPTION
                          when no_data_found then
                            var$existe := 0;
                    END;

                    /* Creamos el permiso por rol */
                    IF var$existe = 0 THEN
                        /* Buscamos el OID_PERMISO */
                        BEGIN
                            SELECT
                                OID_PERMISO
                            INTO
                                var$oid_permiso
                            FROM
                                SAPR_TPERMISO
                            WHERE
                                COD_PERMISO = rolxper.COD_PERMISO
                                AND OID_APLICACION = var$oid_aplicacion;
                            EXCEPTION
                                WHEN OTHERS THEN
                                    raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                                    'No encontró el permiso: ' || rolxper.COD_PERMISO || ' para la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TPERMISO' ||
                                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);                                
                        END;

                        INSERT INTO SAPR_TPERMISOXROLE
                        (
                            OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, 
                            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                        )
                        VALUES
                        (
                            SYS_GUID(), var$oid_permiso, var$oid_role, 1,
                            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
                        );
                    END IF;


                END IF;
            END LOOP;
        END;
        
        /* Rol Aprobador Cuentas Bancarias */
        BEGIN
            var$cod_role := 'Aprobador Cuentas Bancarias';
            /* Obtengo el OID_ROLE */
            BEGIN
                SELECT OID_ROLE
                INTO var$oid_role
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN others THEN
                    raise_application_error(-20001,
                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                    'No encontró el rol: ' || var$cod_role  ||
                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
            END;
            
            FOR rolxper IN rc_per_x_role LOOP
                /* Verificamos que sea un permiso de aprobación de cuentas bancarias */
                IF rolxper.COD_ROLE = var$cod_role AND  const$apr_cuentas = substr(rolxper.cod_permiso, 1,LENGTH (const$apr_cuentas)) THEN
                    BEGIN
                        SELECT OID_APLICACION
                        INTO var$oid_aplicacion
                        FROM SAPR_TAPLICACION
                        WHERE COD_APLICACION = rolxper.COD_APLICACION;
                        EXCEPTION
                        WHEN others THEN
                            raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                            'No encontró la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TAPLICACION' ||
                            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
                    END;

                    BEGIN
                        SELECT count(1)
                        INTO var$existe
                        FROM SAPR_TPERMISOXROLE perxrol
                        INNER JOIN SAPR_TPERMISO permiso ON permiso.oid_permiso = perxrol.oid_permiso
                        INNER JOIN SAPR_TROLE ROL ON ROL.OID_ROLE = perxrol.OID_ROLE
                        WHERE
                            permiso.OID_APLICACION = var$oid_aplicacion
                            AND PERMISO.COD_PERMISO = rolxper.COD_PERMISO
                            AND ROL.COD_ROLE = var$cod_role;
                        EXCEPTION
                          when no_data_found then
                            var$existe := 0;
                    END;

                    /* Creamos el permiso por rol */
                    IF var$existe = 0 THEN
                        /* Buscamos el OID_PERMISO */
                        BEGIN
                            SELECT
                                OID_PERMISO
                            INTO
                                var$oid_permiso
                            FROM
                                SAPR_TPERMISO
                            WHERE
                                COD_PERMISO = rolxper.COD_PERMISO
                                AND OID_APLICACION = var$oid_aplicacion;
                            EXCEPTION
                                WHEN OTHERS THEN
                                    raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                                    'No encontró el permiso: ' || rolxper.COD_PERMISO || ' para la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TPERMISO' ||
                                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);                                
                        END;

                        INSERT INTO SAPR_TPERMISOXROLE
                        (
                            OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, 
                            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                        )
                        VALUES
                        (
                            SYS_GUID(), var$oid_permiso, var$oid_role, 1,
                            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
                        );
                    END IF;


                END IF;
            END LOOP;
           
        END;

        /*Rol Aprobador Límites */
        BEGIN
            var$cod_role := 'Aprobador Limites';
            BEGIN
                SELECT OID_ROLE
                INTO var$oid_role
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN others THEN
                    raise_application_error(-20001,
                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                    'No encontró el rol: ' || var$cod_role  ||
                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
            END;

            FOR rolxper IN rc_per_x_role LOOP
                /* Verificamos que sea un permiso de aprobación de límites */
                IF rolxper.COD_ROLE = var$cod_role AND  rolxper.COD_PERMISO = const$apr_limites THEN
                    BEGIN
                        SELECT OID_APLICACION
                        INTO var$oid_aplicacion
                        FROM SAPR_TAPLICACION
                        WHERE COD_APLICACION = rolxper.COD_APLICACION;
                        EXCEPTION
                        WHEN others THEN
                            raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                            'No encontró la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TAPLICACION' ||
                            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
                    END;

                    BEGIN
                        SELECT count(1)
                        INTO var$existe
                        FROM SAPR_TPERMISOXROLE perxrol
                        INNER JOIN SAPR_TPERMISO permiso ON permiso.oid_permiso = perxrol.oid_permiso
                        INNER JOIN SAPR_TROLE ROL ON ROL.OID_ROLE = perxrol.OID_ROLE
                        WHERE
                            permiso.OID_APLICACION = var$oid_aplicacion
                            AND PERMISO.COD_PERMISO = rolxper.COD_PERMISO
                            AND ROL.COD_ROLE = var$cod_role;
                        EXCEPTION
                          when no_data_found then
                            var$existe := 0;
                    END;

                    /* Creamos el permiso por rol */
                    IF var$existe = 0 THEN
                        /* Buscamos el OID_PERMISO */
                        BEGIN
                            SELECT
                                OID_PERMISO
                            INTO
                                var$oid_permiso
                            FROM
                                SAPR_TPERMISO
                            WHERE
                                COD_PERMISO = rolxper.COD_PERMISO
                                AND OID_APLICACION = var$oid_aplicacion;
                            EXCEPTION
                                WHEN OTHERS THEN
                                    raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: Migración Permisos ' ||
                                    'No encontró el permiso: ' || rolxper.COD_PERMISO || ' para la aplicación: ' || rolxper.COD_APLICACION  || ' en SAPR_TPERMISO' ||
                                    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);                                
                        END;

                        INSERT INTO SAPR_TPERMISOXROLE
                        (
                            OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO, 
                            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                        )
                        VALUES
                        (
                            SYS_GUID(), var$oid_permiso, var$oid_role, 1,
                            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
                        );
                    END IF;


                END IF;
            END LOOP;
        END;
    END;



    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(-20001,
                            'Arquivo: SCRIPT_MIGRACION_PERMISOS.sql Script: SCRIPT_MIGRACION_PERMISOS' ||
                             ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
    RAISE;
END;
/

DECLARE
    var$cod_role     varchar2(50);
    var$existe      NUMBER;

    var$gmt_zero    VARCHAR2(80);
    var$usuario     VARCHAR2(50);

    var$oid_role    varchar(36);
    var$oid_usuario    varchar(36);

    CURSOR rc_usuarios      IS SELECT * FROM SAPR_TUSUARIO WHERE BOL_ACTIVO = 1;
    CURSOR rc_permisosporusuario IS 
        WITH RolesxUsuarios AS (  SELECT usu.oid_usuario,
                                                CASE WHEN dnsg.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END NS_GRAL,
                                                CASE WHEN dnsa.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END NS_ADMIN,
                                                CASE WHEN diacg.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END IAC_GRAL,
                                                CASE WHEN diaca.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END IAC_ADMIN
                                        FROM ADPR_TUSUARIO usu
                                                LEFT JOIN ADPR_TDELE_TIPO_ROLE_USR dnsg
                                                    ON usu.oid_usuario = dnsg.oid_usuario
                                                        AND dnsg.oid_role IN (SELECT OID_ROLE
                                                                                    FROM ADPR_TROLE
                                                                                    WHERE cod_role = 'General'
                                                                                            AND oid_aplicacion IN (SELECT oid_aplicacion
                                                                                                                            FROM ADPR_TAPLICACION
                                                                                                                            WHERE cod_aplicacion = 'GenesisSaldos'))
                                                LEFT JOIN ADPR_TDELE_TIPO_ROLE_USR dnsa
                                                    ON usu.oid_usuario = dnsa.oid_usuario
                                                        AND dnsa.oid_role IN (SELECT OID_ROLE
                                                                                    FROM ADPR_TROLE
                                                                                    WHERE cod_role = 'Administrador'
                                                                                            AND oid_aplicacion IN (SELECT oid_aplicacion
                                                                                                                            FROM ADPR_TAPLICACION
                                                                                                                            WHERE cod_aplicacion = 'GenesisSaldos'))
                                                LEFT JOIN ADPR_TDELE_TIPO_ROLE_USR diacg
                                                    ON usu.oid_usuario = diacg.oid_usuario
                                                        AND diacg.oid_role IN (SELECT OID_ROLE
                                                                                        FROM ADPR_TROLE
                                                                                    WHERE cod_role = 'General'
                                                                                            AND oid_aplicacion IN (SELECT oid_aplicacion
                                                                                                                            FROM ADPR_TAPLICACION
                                                                                                                            WHERE cod_aplicacion = 'IAC'))
                                                LEFT JOIN ADPR_TDELE_TIPO_ROLE_USR diaca
                                                    ON usu.oid_usuario = diaca.oid_usuario
                                                        AND diaca.oid_role IN (SELECT OID_ROLE
                                                                                        FROM ADPR_TROLE
                                                                                    WHERE cod_role = 'Administrador'
                                                                                            AND oid_aplicacion IN (SELECT oid_aplicacion
                                                                                                                            FROM ADPR_TAPLICACION
                                                                                                                            WHERE cod_aplicacion = 'IAC'))
                                        WHERE usu.bol_Activo = '1'
                                    GROUP BY usu.oid_usuario,
                                                CASE WHEN dnsg.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END,
                                                CASE WHEN dnsa.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END,
                                                CASE WHEN diacg.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END,
                                                CASE WHEN diaca.OID_DELE_TIPO_ROLE_USR IS NULL THEN 'No' ELSE 'Si' END)
        SELECT usu.des_login,
                CASE
                    WHEN ns_gral = 'Si' AND ns_admin = 'No' AND iac_gral = 'No' AND iac_admin = 'No' THEN 'Consulta Transacciones'
                    WHEN ns_gral = 'Si' AND ns_admin = 'No' AND iac_gral = 'Si' AND iac_admin = 'No' THEN 'General'
                    ELSE 'Administrador'
                END
                    Role,
                pa.cod_pais,
                pa.oid_pais
        FROM     ADPR_TUSUARIO usu
                JOIN
                    RolesxUsuarios usr
                ON usu.oid_usuario = usr.oid_usuario,
                GEPR_TPAIS pa
        ORDER BY usu.des_login;
    
    const$CodAppNuevosSaldos CONSTANT  varchar2(50) := 'GenesisSaldos';
BEGIN

    BEGIN
        var$usuario := 'GENESIS_PRODUCTO';
        var$gmt_zero := sys_extract_utc(current_timestamp) || ' +00:00';

        /* Creamos cada uno de los nuevos tres roles (si es que no existen)*/
        BEGIN
            /* Creamos el rol Consulta Transacciones */
            var$cod_role := 'Consulta Transacciones';
            BEGIN
                 /* Verificamos si el rol ya existe en la nueva tabla. */
                SELECT COUNT(1)
                    INTO var$existe
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;

                /* En caso de que no exista migramos el rol */
                IF var$existe = 0 THEN
                    /* Creamos el ROL para la aplicación*/
                    INSERT INTO SAPR_TROLE  (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                    VALUES 
                        (sys_guid(), var$cod_role, 'Este rol tendrá acceso a consultas en el modulo de Nuevos Saldos.', 1,
                        var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);

                    DBMS_OUTPUT.PUT_LINE('MIG_ROL A - Creamos el rol: ' || var$cod_role);
                END IF;


                /* Obtenemos el OID del rol */
                BEGIN
                    SELECT COUNT(1)
                    INTO var$oid_role
                    FROM SAPR_TROLE
                    WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN no_data_found THEN
                    DBMS_OUTPUT.PUT_LINE('MIG_ROLE | No encontró el rol: ' || var$cod_role);
                    var$oid_role := NULL;
                    ROLLBACK;
                    raise_application_error(-20001,
                        'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles ' || 
                        'MIG_ROLE | No encontró el rol: ' || var$cod_role ||
                        sqlerrm);
                END;             
            END;

            /* Creamos el rol General */
            var$cod_role := 'General';
            BEGIN
                 /* Verificamos si el rol ya existe en la nueva tabla. */
                SELECT COUNT(1)
                    INTO var$existe
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;

                /* En caso de que no exista migramos el rol */
                IF var$existe = 0 THEN
                    /* Creamos el ROL para la aplicación*/
                    INSERT INTO SAPR_TROLE  (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO, 
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                    VALUES 
                        (sys_guid(), var$cod_role, 'El rol General tendrá acceso a consulta y mantenimiento de entidades y transacciones.', 1,
                        var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);

                    DBMS_OUTPUT.PUT_LINE('MIG_ROL A - Creamos el rol: ' || var$cod_role);
                END IF;


                /* Obtenemos el OID del rol */
                BEGIN
                    SELECT COUNT(1)
                    INTO var$oid_role
                    FROM SAPR_TROLE
                    WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN no_data_found THEN
                    DBMS_OUTPUT.PUT_LINE('MIG_ROLE | No encontró el rol: ' || var$cod_role);
                    var$oid_role := NULL;
                    ROLLBACK;
                    raise_application_error(-20001,
                        'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles ' || 
                        'MIG_ROLE | No encontró el rol: ' || var$cod_role ||
                        sqlerrm);
                END;             
            END;

            /* Creamos el rol Administrador */
            var$cod_role := 'Administrador';
            BEGIN
                 /* Verificamos si el rol ya existe en la nueva tabla. */
                SELECT COUNT(1)
                    INTO var$existe
                FROM SAPR_TROLE
                WHERE COD_ROLE = var$cod_role;

                /* En caso de que no exista migramos el rol */
                IF var$existe = 0 THEN
                    /* Creamos el ROL para la aplicación*/
                    INSERT INTO SAPR_TROLE  (OID_ROLE, COD_ROLE, DES_ROLE, BOL_ACTIVO,
                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                    VALUES 
                        (sys_guid(), var$cod_role, 'El rol de administrador tendrá acceso a toda la aplicación, sin restricciones.', 1,
                        var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);

                    DBMS_OUTPUT.PUT_LINE('MIG_ROL A - Creamos el rol: ' || var$cod_role);
                END IF;


                /* Obtenemos el OID del rol */
                BEGIN
                    SELECT COUNT(1)
                    INTO var$oid_role
                    FROM SAPR_TROLE
                    WHERE COD_ROLE = var$cod_role;
                EXCEPTION
                WHEN no_data_found THEN
                    DBMS_OUTPUT.PUT_LINE('MIG_ROLE | No encontró el rol: ' || var$cod_role);
                    var$oid_role := NULL;
                    ROLLBACK;
                    raise_application_error(-20001,
                        'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles ' || 
                        'MIG_ROLE | No encontró el rol: ' || var$cod_role ||
                        sqlerrm);
                END;             
            END;
        EXCEPTION
            WHEN others THEN
                ROLLBACK;
                raise_application_error(-20001,
                                    'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles' ||
                                    sqlerrm);
        END;

        /* Migramos los roles por usuario */
        FOR usu_x_rol in rc_permisosporusuario LOOP
          BEGIN
          --dbms_output.put_line('ENTRO');
            select count(1)
              into var$existe
              from SAPR_TROLEXUSUARIO rxu
              inner join sapr_trole rol on rxu.oid_role = rol.oid_role
              inner join sapr_tusuario usu on rxu.oid_usuario =  usu.oid_usuario
             where 
                usu_x_rol.oid_pais = rxu.oid_pais
                and usu_x_rol.des_login = usu.des_login
                and rol.cod_role = usu_x_rol.role
                and rxu.bol_Activo = 1;
            EXCEPTION
              WHEN others THEN
                var$existe := 0;          
          END;

--dbms_output.put_line('Valor var$existe:'|| var$existe);
          /* Si no existe, creamos un registro en la tabla SAPR_TROLEXUSUARIO*/
          IF var$existe=0 THEN
            /*Obtengo el OID_ROLE*/
            BEGIN
                select oid_role
                  into var$oid_role
                  from sapr_trole
                 where cod_role = usu_x_rol.role;
              EXCEPTION
                WHEN others THEN
                  var$oid_role := null;
            END;
            /*Obtengo el OID_USUARIO*/
            BEGIN
                select oid_usuario
                  into var$oid_usuario
                  from SAPR_TUSUARIO
                 where DES_LOGIN = usu_x_rol.des_login;
              EXCEPTION
                WHEN others THEN
                  var$oid_usuario := NULL;
            END;
            insert into SAPR_TROLEXUSUARIO (OID_ROLEXUSUARIO, oid_role, oid_usuario, oid_pais, bol_activo,
            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
            values (sys_guid(), var$oid_role, var$oid_usuario, usu_x_rol.oid_pais, 1,
            var$gmt_zero, var$usuario, var$gmt_zero, var$usuario);
          END IF;

        END LOOP;



      EXCEPTION
        WHEN others THEN
            ROLLBACK;
            raise_application_error(-20001,
                                'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles' ||
                                sqlerrm);
    END;

    COMMIT;
EXCEPTION
    WHEN others THEN
        ROLLBACK;
        raise_application_error(-20001,
                                'Arquivo: SCRIPT_MIGRACION_ROLES.sql Script: Migración de roles' ||
                                sqlerrm);
END;
/

DECLARE
  var$cod_permiso varchar2(100);
  var$des_permiso varchar2(255);
  const$CodAppIAC CONSTANT  varchar2(50) := 'IAC';
  const$CodRolAdmin CONSTANT  varchar2(50) := 'Administrador';
  var$oid_aplicacion varchar2(36);
  var$oid_role varchar2(36);
  var$gmt_zero    VARCHAR2(80);
  var$usuario     VARCHAR2(50);
  var$existe      number(1);
  var$oid_permiso varchar2(36);

BEGIN
  /* Crea el permiso de ROLES */
  var$cod_permiso := 'CONFIGURACION_ROLES';
  var$des_permiso := 'CONFIGURACION_ROLES';

  var$usuario := 'GENESIS_PRODUCTO';
  var$gmt_zero := sys_extract_utc(current_timestamp) || ' +00:00';

  /* Obtengo el OID de la aplicación const$CodAppIAC */
  BEGIN
    select OID_APLICACION
      into var$oid_aplicacion
      from SAPR_TAPLICACION
     where COD_APLICACION = const$CodAppIAC;
  EXCEPTION
      WHEN no_data_found THEN
        DBMS_OUTPUT.PUT_LINE('Crear Permiso | No encontró la aplicación: ' || const$CodAppIAC);
        var$oid_aplicacion := NULL;
        ROLLBACK;
        raise_application_error(-20001,
            'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || var$cod_permiso ||
            'No encontró la aplicación: ' || const$CodAppIAC  ||
            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM)
            ;
  END;

  /* Busco si existe el permiso var$cod_permiso para la aplicación const$CodAppIAC */
  begin
    select count(1)
      into var$existe
      from SAPR_TPERMISO
     where COD_PERMISO = var$cod_permiso
     AND  OID_APLICACION = var$oid_aplicacion;
    exception
      when others then
        var$existe:=0;
  end;

  /* Creo el nuevo permiso si es que no existe */
  if var$existe = 0 then
    var$oid_permiso := sys_guid();

    INSERT INTO SAPR_TPERMISO
    (
      OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO,
      GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
    )
    VALUES
    (
      var$oid_permiso, var$oid_aplicacion, var$cod_permiso, var$des_permiso, 1,
      var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
    );

  else
    /* Busco el OID del permiso */
      begin
        select OID_PERMISO
          into var$oid_permiso
          from SAPR_TPERMISO
        where COD_PERMISO = var$cod_permiso
        AND  OID_APLICACION = var$oid_aplicacion;
        exception
          when others then
            var$oid_permiso:=0;
      end;    
  end if;

  /* Busco el OID_ROLE de const$CodRolAdmin */
  begin
    select OID_ROLE
      into var$oid_role
      from SAPR_TROLE
     where COD_ROLE = const$CodRolAdmin;
    exception
      when others then
        ROLLBACK;
        raise_application_error(-20001,
            'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || 
            'No encontró el rol: ' || const$CodRolAdmin || ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
  end;

  /* Busco si existe el permiso en el rol */
  begin
    select count(1)
      into var$existe
      from SAPR_TPERMISOXROLE
     where OID_PERMISO = var$oid_permiso
     AND OID_ROLE = var$oid_role;
    exception
        when others then
            ROLLBACK;
            raise_application_error(-20001,
                'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || 
                'No encontró el permiso por rol: ' || const$CodRolAdmin || ' (rol) y ' || var$cod_permiso || ' (permiso)'  || 
                ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
  end;

  /* Creo el permiso por role */
  if var$existe=0 then
    insert into SAPR_TPERMISOXROLE
    (
      OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO,
      GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
    )
    VALUES
    (
      sys_guid(), var$oid_permiso, var$oid_role, 1,
      var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
    );
  else
  
    /* si existe y esta inactivo, lo activamos */  
    update SAPR_TPERMISOXROLE
    set 
      BOL_ACTIVO = 1, 
      GMT_MODIFICACION = var$gmt_zero,
      DES_USUARIO_MODIFICACION = var$usuario
     where OID_PERMISO = var$oid_permiso
     AND OID_ROLE = var$oid_role
     AND BOL_ACTIVO = 0;
  end if;


  COMMIT;

EXCEPTION
  WHEN others THEN
    ROLLBACK;
    raise_application_error(-20001,
    'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || var$cod_permiso ||
    'No encontró la aplicación: ' || const$CodAppIAC || 
    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
END;
/

DECLARE
  var$cod_permiso varchar2(100);
  var$des_permiso varchar2(255);
  const$CodAppIAC CONSTANT  varchar2(50) := 'IAC';
  const$CodRolAdmin CONSTANT  varchar2(50) := 'Administrador';
  var$oid_aplicacion varchar2(36);
  var$oid_role varchar2(36);
  var$gmt_zero    VARCHAR2(80);
  var$usuario     VARCHAR2(50);
  var$existe      number(1);
  var$oid_permiso varchar2(36);

BEGIN
  /* Crea el permiso de USUARIOS */
  var$cod_permiso := 'CONFIGURACION_USUARIOS';
  var$des_permiso := 'CONFIGURACION_USUARIOS';

  var$usuario := 'GENESIS_PRODUCTO';
  var$gmt_zero := sys_extract_utc(current_timestamp) || ' +00:00';

  /* Obtengo el OID de la aplicación const$CodAppIAC */
  BEGIN
    select OID_APLICACION
      into var$oid_aplicacion
      from SAPR_TAPLICACION
     where COD_APLICACION = const$CodAppIAC;
  EXCEPTION
      WHEN no_data_found THEN
        DBMS_OUTPUT.PUT_LINE('Crear Permiso | No encontró la aplicación: ' || const$CodAppIAC);
        var$oid_aplicacion := NULL;
        ROLLBACK;
        raise_application_error(-20001,
            'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || var$cod_permiso ||
            'No encontró la aplicación: ' || const$CodAppIAC  ||
            ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM)
            ;
  END;

  /* Busco si existe el permiso var$cod_permiso para la aplicación const$CodAppIAC */
  begin
    select count(1)
      into var$existe
      from SAPR_TPERMISO
     where COD_PERMISO = var$cod_permiso
     AND  OID_APLICACION = var$oid_aplicacion;
    exception
      when others then
        var$existe:=0;
  end;

  /* Creo el nuevo permiso si es que no existe */
  if var$existe = 0 then
    var$oid_permiso := sys_guid();

    INSERT INTO SAPR_TPERMISO
    (
      OID_PERMISO, OID_APLICACION, COD_PERMISO, DES_PERMISO, BOL_ACTIVO,
      GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
    )
    VALUES
    (
      var$oid_permiso, var$oid_aplicacion, var$cod_permiso, var$des_permiso, 1,
      var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
    );

  else
    /* Busco el OID del permiso */
      begin
        select OID_PERMISO
          into var$oid_permiso
          from SAPR_TPERMISO
        where COD_PERMISO = var$cod_permiso
        AND  OID_APLICACION = var$oid_aplicacion;
        exception
          when others then
            var$oid_permiso:=0;
      end;    
  end if;

  /* Busco el OID_ROLE de const$CodRolAdmin */
  begin
    select OID_ROLE
      into var$oid_role
      from SAPR_TROLE
     where COD_ROLE = const$CodRolAdmin;
    exception
      when others then
        ROLLBACK;
        raise_application_error(-20001,
            'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || 
            'No encontró el rol: ' || const$CodRolAdmin || ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
  end;

  /* Busco si existe el permiso en el rol */
  begin
    select count(1)
      into var$existe
      from SAPR_TPERMISOXROLE
     where OID_PERMISO = var$oid_permiso
     AND OID_ROLE = var$oid_role;
    exception
        when others then
            ROLLBACK;
            raise_application_error(-20001,
                'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || 
                'No encontró el permiso por rol: ' || const$CodRolAdmin || ' (rol) y ' || var$cod_permiso || ' (permiso)'  || 
                ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
  end;

  /* Creo el permiso por role */
  if var$existe=0 then
    insert into SAPR_TPERMISOXROLE
    (
      OID_PERMISOXROLE, OID_PERMISO, OID_ROLE, BOL_ACTIVO,
      GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
    )
    VALUES
    (
      sys_guid(), var$oid_permiso, var$oid_role, 1,
      var$gmt_zero, var$usuario, var$gmt_zero, var$usuario
    );
  else
  
    /* si existe y esta inactivo, lo activamos */  
    update SAPR_TPERMISOXROLE
    set 
      BOL_ACTIVO = 1, 
      GMT_MODIFICACION = var$gmt_zero,
      DES_USUARIO_MODIFICACION = var$usuario
     where OID_PERMISO = var$oid_permiso
     AND OID_ROLE = var$oid_role
     AND BOL_ACTIVO = 0;
  end if;


  COMMIT;

EXCEPTION
  WHEN others THEN
    ROLLBACK;
    raise_application_error(-20001,
    'Arquivo: SCRIPT_CREAR_PERMISO.sql Script: Crear permiso ' || var$cod_permiso ||
    'No encontró la aplicación: ' || const$CodAppIAC || 
    ' SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM);
END;
/