insert into gepr_tlog_error
  (oid_log_error, des_error, fyh_error, cod_usuario)
values
  (:oid_log_error, :des_error, sysdate, :cod_usuario)