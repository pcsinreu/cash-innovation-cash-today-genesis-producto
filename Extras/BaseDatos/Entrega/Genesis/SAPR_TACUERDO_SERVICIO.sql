 DECLARE
    OWNER_OBJECTS VARCHAR2(30) := '';
    ExisteTablaAcServicio NUMBER;
  
 BEGIN
    SELECT sys_context( 'userenv', 'current_schema' ) INTO OWNER_OBJECTS FROM DUAL;
    SELECT COUNT(1) INTO ExisteTablaAcServicio FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TACUERDO_SERVICIO';

    IF ExisteTablaAcServicio = 0 THEN
        /*==============================================================*/
        /* Table: SAPR_TACUERDO_SERVICIO                                          */
        /*==============================================================*/
        DBMS_OUTPUT.PUT_LINE('CREATE SAPR_TACUERDO_SERVICIO');
        EXECUTE IMMEDIATE q'[
        create table SAPR_TACUERDO_SERVICIO 
        (
            OID_ACUERDO_SERVICIO      VARCHAR2(36)        NOT NULL,
            DES_CONTRACT_ID           VARCHAR2(255),
            DES_SERVICE_ORDER_ID      VARCHAR2(255),
            DES_SERVICE_ORDER_CODE    VARCHAR2(255),
            DES_PRODUCT_ID            VARCHAR2(255),
            DES_PRODUCT_CODE          VARCHAR2(255),
            OID_CLIENTE               VARCHAR2(36)        NOT NULL,
            OID_SUBCLIENTE            VARCHAR2(36)        NOT NULL,
            OID_PTO_SERVICIO          VARCHAR2(36)        NOT NULL,
            OID_PAIS                  VARCHAR2(36)        NOT NULL,
            BOL_VIGENTE               NUMBER(1)           NOT NULL,
            GMT_CREACION              TIMESTAMP(6) WITH TIME ZONE NOT NULL,
            DES_USUARIO_CREACION      VARCHAR2(50 BYTE) NOT NULL,
            GMT_MODIFICACION          TIMESTAMP(6) WITH TIME ZONE NOT NULL,
            DES_USUARIO_MODIFICACION  VARCHAR2(50 BYTE) NOT NULL,
            constraint PK_SAPR_TAC_SERV primary key (OID_ACUERDO_SERVICIO),
            constraint FK_SAPR_TAC_SERV_CLI foreign key (OID_CLIENTE) references GEPR_TCLIENTE (OID_CLIENTE),
            constraint FK_SAPR_TAC_SERV_SUBCLI foreign key (OID_SUBCLIENTE) references GEPR_TSUBCLIENTE (OID_SUBCLIENTE),
            constraint FK_SAPR_TAC_SERV_PTO foreign key (OID_PTO_SERVICIO) references GEPR_TPUNTO_SERVICIO (OID_PTO_SERVICIO),
            constraint FK_SAPR_TAC_SERV_PAIS foreign key (OID_PAIS) references GEPR_TPAIS (OID_PAIS)
        )]';
    END IF;

EXCEPTION
   WHEN OTHERS THEN raise_application_error( -20001, 'Arquivo: Genesis.sql Script: SAPR_TACUERDO_SERVICIO - ' || sqlerrm);
    RAISE;
END;
/