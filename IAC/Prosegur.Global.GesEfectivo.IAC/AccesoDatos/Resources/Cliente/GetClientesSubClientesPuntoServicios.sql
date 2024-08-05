select oid_cliente,
       cod_cliente,
       des_cliente,
       cliente_enviado,
       oid_subcliente,
       cod_subcliente,
       des_subcliente,
       subcliente_enviado,
       oid_pto_servicio,
       cod_pto_servicio,
       des_pto_servicio,
       pto_enviado
from (select c.oid_cliente,
       c.cod_cliente,
       c.des_cliente,
       c.bol_enviado_saldos cliente_enviado,
       sc.oid_subcliente,
       sc.cod_subcliente,
       sc.des_subcliente,
       sc.bol_enviado_saldos subcliente_enviado,
       pto.oid_pto_servicio,
       pto.cod_pto_servicio,
       pto.des_pto_servicio,
       pto.bol_enviado_saldos pto_enviado
  from gepr_tpunto_servicio pto
  inner join gepr_tsubcliente sc on pto.oid_subcliente = sc.oid_subcliente 
  inner join gepr_tcliente c on c.oid_cliente = sc.oid_cliente 
where pto.bol_enviado_saldos =  {0}
Union
select c.oid_cliente,
       c.cod_cliente,
       c.des_cliente,
       c.bol_enviado_saldos cliente_enviado,
       sc.oid_subcliente,
       sc.cod_subcliente,
       sc.des_subcliente,
       sc.bol_enviado_saldos subcliente_enviado,
       null oid_pto_servicio,
       null cod_pto_servicio,
       null des_pto_servicio,
       null pto_enviado
  from gepr_tsubcliente sc  
  inner join gepr_tcliente c on c.oid_cliente = sc.oid_cliente 
where sc.bol_enviado_saldos = {0}
Union
select c.oid_cliente,
       c.cod_cliente,
       c.des_cliente,
       c.bol_enviado_saldos cliente_enviado,
       null oid_subcliente,
       null cod_subcliente,
       null des_subcliente,
       null subcliente_enviado,
       null oid_pto_servicio,
       null cod_pto_servicio,
       null des_pto_servicio,
       null pto_enviado
  from gepr_tcliente c  
where c.bol_enviado_saldos = 0)
where rownum <= []num_linhas
