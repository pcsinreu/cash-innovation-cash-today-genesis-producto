 select MAX(cert.fyh_certificado)
          from sapr_tcertificado cert 
         inner join sapr_tcertificadoxsector cese 
            on cert.oid_certificado = cese.oid_certificado 
         inner join sapr_tcertificadoxsubcanal cesc 
            on cert.oid_certificado = cesc.oid_certificado 
         inner join sapr_tconfig_nivel_saldo cons 
            on cert.oid_config_nivel_saldo = cons.oid_config_nivel_saldo 
         inner join sapr_tcuenta cuen 
            on cons.oid_cliente = cuen.oid_cliente 
           and cese.oid_sector = cuen.oid_sector 
           and cesc.oid_subcanal = cuen.oid_subcanal 
         where 
          cert.cod_estado IN ('PC', 'DE') {0}