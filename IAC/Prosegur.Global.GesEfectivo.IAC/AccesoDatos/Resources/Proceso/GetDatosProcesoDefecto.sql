select oid_proceso,
       cod_checksum_inf_gnral,
       cod_checksum_inf_tolerancias,
       cod_delegacion,
       oid_tipo_procesado,
       oid_producto,
       bol_medios_pago,
       bol_contar_cheques_total,
       bol_contar_tickets_total,
       bol_contar_otros_total,
       des_proceso,
       bol_vigente,
       cod_usuario,
       fyh_actualizacion,
       bol_por_defecto
  from gepr_tproceso
 where BOL_POR_DEFECTO = 1