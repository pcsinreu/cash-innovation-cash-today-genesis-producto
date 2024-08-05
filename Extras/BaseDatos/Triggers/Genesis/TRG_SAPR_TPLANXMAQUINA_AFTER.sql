CREATE OR REPLACE TRIGGER TRG_SAPR_TPLANXMAQUINA_AFTER
AFTER UPDATE OF BOL_ACTIVO ON SAPR_TPLANXMAQUINA FOR EACH ROW
BEGIN
  IF :NEW.BOL_ACTIVO = 0 THEN 
    SAPR_PMOVIMIENTO_###VERSION###.supd_por_desrelacion_maq_plani(
                                par$oid_maquina         => :NEW.OID_MAQUINA,
                                par$oid_planificacion   => :NEW.OID_PLANIFICACION,
                                par$fyh_vigencia_inicio => :NEW.FYH_VIGENCIA_INICIO,
                                par$cod_usuario         => 'TRG_SAPR_TPLANXMAQUINA_AFTER');
  END IF;
END;