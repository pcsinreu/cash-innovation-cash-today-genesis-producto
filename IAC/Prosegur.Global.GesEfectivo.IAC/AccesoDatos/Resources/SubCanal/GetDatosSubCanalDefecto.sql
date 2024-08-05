select oid_subcanal,
       cod_subcanal,
       oid_canal,
       des_subcanal,
       obs_subcanal,
       bol_vigente,
       cod_usuario,
       fyh_actualizacion,
       bol_por_defecto
  from gepr_tsubcanal
 where BOL_POR_DEFECTO = 1