CREATE OR REPLACE TRIGGER TRG_SAPR_TPERIODOXDOC_AFTER
AFTER INSERT OR DELETE ON SAPR_TPERIODOXDOCUMENTO FOR EACH ROW
BEGIN
  IF INSERTING THEN 
    SAPR_PMOVIMIENTO_###VERSION###.supd_por_relacion_periodo(
                              par$oid_documento    => :NEW.OID_DOCUMENTO,
                              par$oid_periodo      => :NEW.OID_PERIODO,
                              par$cod_usuario      => :NEW.DES_USUARIO_CREACION);
  ELSIF DELETING THEN
    SAPR_PMOVIMIENTO_###VERSION###.supd_por_desrelacion_periodo(
                              par$oid_documento    => :OLD.OID_DOCUMENTO,
                              par$oid_periodo      => :OLD.OID_PERIODO,
                              par$cod_usuario      => 'TRG_SAPR_TPERIODOXDOC_AFTER');
  END IF;
END;