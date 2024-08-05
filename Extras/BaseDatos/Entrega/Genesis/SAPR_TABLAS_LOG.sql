 DECLARE
    OWNER_OBJECTS VARCHAR2(30) := '';
    ExisteDetalle NUMBER;
    ExisteLlamada NUMBER;
  
 BEGIN
    SELECT sys_context( 'userenv', 'current_schema' ) INTO OWNER_OBJECTS FROM DUAL;
    

    SELECT COUNT(1) INTO ExisteDetalle FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TLOG_API_DETALLE';
    SELECT COUNT(1) INTO ExisteLlamada FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TLOG_API_LLAMADA';
    
    IF ExisteDetalle = 1 THEN
        DBMS_OUTPUT.PUT_LINE('DROP SAPR_TLOG_API_DETALLE');
        EXECUTE IMMEDIATE q'[DROP TABLE SAPR_TLOG_API_DETALLE]';
    END IF;
    IF ExisteLlamada = 1 THEN
        DBMS_OUTPUT.PUT_LINE('DROP SAPR_TLOG_API_LLAMADA');
        EXECUTE IMMEDIATE q'[DROP TABLE SAPR_TLOG_API_LLAMADA]';
    END IF;   

    /*==============================================================*/
    /* Table: SAPR_TLOG_API_LLAMADA                                 */
    /*==============================================================*/
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TLOG_API_LLAMADA');
    EXECUTE IMMEDIATE q'[create table SAPR_TLOG_API_LLAMADA 
    (
    OID_LOG_API_LLAMADA  VARCHAR2(36)              NOT NULL,
    COD_PAIS             VARCHAR2(15)              NOT NULL,
    DES_RECURSO          VARCHAR2(200)             NOT NULL,
    DES_VERSION          VARCHAR2(20)              NOT NULL,
    DES_DATOS_ENTRADA    CLOB                      NOT NULL,
    DES_DATOS_SALIDA     CLOB,
    COD_HASH_ENTRADA     VARCHAR2(32)              NOT NULL,
    COD_HASH_SALIDA      VARCHAR2(32),
    FEC_LOG	             DATE                      NOT NULL,
    FYH_LLAMADA_INICIO   TIMESTAMP WITH TIME ZONE  NOT NULL,
    FYH_LLAMADA_FIN      TIMESTAMP WITH TIME ZONE,
    COD_RESULTADO        VARCHAR2(20),
    DES_RESULTADO        VARCHAR2(4000),
    DES_HOST             VARCHAR2(50),
    DES_HOST_IP          VARCHAR2(50),
    constraint PK_SAPR_TLOG_API_LLAMADA primary key (OID_LOG_API_LLAMADA)
    )
    PARTITION BY RANGE (FEC_LOG) INTERVAL (NUMTODSINTERVAL(1, 'DAY'))
        ( 
            PARTITION LESS_2023 VALUES LESS THAN (TO_DATE('01-01-2023','dd-MM-yyyy')) 
        )
    ]';

    /*==============================================================*/
    /* Index: IX_SAPR_TLOG_API_LLAMADA_01                           */
    /*==============================================================*/
    EXECUTE IMMEDIATE q'[create index IX_SAPR_TLOG_API_LLAMADA_01 on SAPR_TLOG_API_LLAMADA (
    FYH_LLAMADA_INICIO ASC,
    FYH_LLAMADA_FIN ASC
    )]';
    
   
    /*==============================================================*/
    /* Table: SAPR_TLOG_API_DETALLE                                 */
    /*==============================================================*/
    DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TLOG_API_DETALLE');
    EXECUTE IMMEDIATE q'[create table SAPR_TLOG_API_DETALLE 
    (
    OID_LOG_API_DETALLE  VARCHAR2(36)                NOT NULL,
    OID_LOG_API_LLAMADA  VARCHAR2(36)                NOT NULL,
    DES_ORIGEN           VARCHAR2(200)               NOT NULL,
    DES_VERSION          VARCHAR2(20)                NOT NULL,
    DES_DETALLE          VARCHAR2(4000)              NOT NULL,
    FEC_LOG	             DATE                        NOT NULL,
    FYH_DETALLE          TIMESTAMP WITH TIME ZONE    NOT NULL,
    COD_IDENTIFICADOR    VARCHAR2(80),
    constraint PK_SAPR_TLOG_API_DETALLE primary key (OID_LOG_API_DETALLE),
    constraint FK_SAPR_TLOG_API_DETALLE_01 foreign key (OID_LOG_API_LLAMADA)
        references SAPR_TLOG_API_LLAMADA (OID_LOG_API_LLAMADA)
    )
    PARTITION BY RANGE (FEC_LOG) INTERVAL (NUMTODSINTERVAL(1, 'DAY'))
        ( 
            PARTITION LESS_2023 VALUES LESS THAN (TO_DATE('01-01-2023','dd-MM-yyyy')) 
        )
    ]';

    /*==============================================================*/
    /* Index: IX_SAPR_TLOG_API_DETALLE_01                           */
    /*==============================================================*/
    EXECUTE IMMEDIATE q'[create index IX_SAPR_TLOG_API_DETALLE_01 on SAPR_TLOG_API_DETALLE (OID_LOG_API_LLAMADA)]';
   
END;
/