UPDATE gepr_tpunto_servicio P SET P.Oid_Maquina = null
where P.{0}
and P.OID_PTO_SERVICIO not in (
  select max(pto.oid_pto_servicio)  from gepr_tpunto_servicio pto
  where pto.{0}
  group by pto.oid_maquina 
)