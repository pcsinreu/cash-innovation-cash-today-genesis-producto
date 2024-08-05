DECLARE
   OWNER_OBJECTS VARCHAR2(30) := '';
   EXISTE NUMBER;
BEGIN
    SELECT COUNT(1) INTO EXISTE FROM ALL_TABLES A WHERE UPPER(A.OWNER) = UPPER(OWNER_OBJECTS) AND UPPER(A.TABLE_NAME) = 'SAPR_TPLANXENTIDAD';
    
    IF EXISTE = 0 THEN
        EXECUTE IMMEDIATE 'CREATE TABLE SAPR_TPLANXENTIDAD
                            (
                                oid_planxentidad        VARCHAR2(36) not null,
                                oid_planificacion       VARCHAR2(36) not null,
                                oid_maquina             VARCHAR2(36),
                                oid_pto_servicio        VARCHAR2(36),
                                oid_canal               VARCHAR2(36),
                                oid_subcanal            VARCHAR2(36),
                                bol_activo               NUMBER(1) default 0 not null,
                                gmt_creacion             TIMESTAMP(6) WITH TIME ZONE,
                                des_usuario_creacion     VARCHAR2(50),
                                gmt_modificacion         TIMESTAMP(6) WITH TIME ZONE,
                                des_usuario_modificacion VARCHAR2(50) not null
                            )
        ';
        /*PRIMARY KEY*/  
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT PK_SAPR_TPLANXENTIDAD PRIMARY KEY (oid_planxentidad)';
        
        /*FOREING KEY*/
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT FK_TPLANXENTIDAD_TPLAN FOREIGN KEY (oid_planificacion) REFERENCES SAPR_TPLANIFICACION (oid_planificacion)';
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT FK_TPLANXENTIDAD_TMAQ FOREIGN KEY (oid_maquina) REFERENCES SAPR_TMAQUINA (oid_maquina)';
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT FK_TPLANXENTIDAD_TPS FOREIGN KEY (oid_pto_servicio) REFERENCES GEPR_TPUNTO_SERVICIO (oid_pto_servicio)';
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT FK_TPLANXENTIDAD_TC FOREIGN KEY (oid_canal) REFERENCES GEPR_TCANAL (oid_canal)';
        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXENTIDAD ADD CONSTRAINT FK_TPLANXENTIDAD_TSUBC FOREIGN KEY (oid_subcanal) REFERENCES GEPR_TSUBCANAL (oid_subcanal)';        

    END IF;

    EXCEPTION
      WHEN OTHERS THEN raise_application_error( -20001, 'Arquivo: Genesis.sql Script: SAPR_TPLANXENTIDAD - ' || sqlerrm);
    RAISE;

 END;