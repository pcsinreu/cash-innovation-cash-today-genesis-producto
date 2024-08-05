BEGIN

	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TACREDITACION TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TCALIDAD TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TCANAL TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCERT_SALDO_EFECTIVO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCERTIFICADO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCERTIFICADOXDELEGACION TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCERTIFICADOXSUBCANAL TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TCLIENTE TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TCODIGO_AJENO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCONFIG_NIVEL_SALDO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCONTENEDOR TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TCUENTA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TDATO_BANCARIO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TDELEGACION TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TDENOMINACION TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TDIVISA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TDOCUMENTO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TFORMULARIO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TMAQUINA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TMEDIO_PAGO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TMODELO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TNIVEL_PARAMETRO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TPAIS TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TPARAMETRO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TPARAMETRO_VALOR TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TPERIODO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TPERIODOXDOCUMENTO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TPLANIFICACION TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TPLANTA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TPLANXMAQUINA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TPRECINTOXCONTENEDOR TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TPUNTO_SERVICIO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TSALDO_EFECTIVO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TSALDO_EFECTIVO_HISTORICO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TSALDO_MEDIO_PAGO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TSALDO_MEDIO_PAGO_HIST TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TSECTOR TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TSUBCANAL TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TSUBCLIENTE TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TTIPO_CLIENTE TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TTIPO_PUNTO_SERVICIO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TTIPO_SECTOR TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TTIPO_SUBCLIENTE TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_TTRANSACCION_EFECTIVO TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.GEPR_TUNIDAD_MEDIDA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_VCUENTA TO {REPORTES_OWNER}';
	execute immediate 'GRANT SELECT ON {GENESIS_OWNER}.SAPR_VFILTROXCERTIFICADO TO {REPORTES_OWNER}';

EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: Genesis_GeS.sql Script: Grants_Synonyms' ||
                            sqlerrm);
    RAISE;
END;
/