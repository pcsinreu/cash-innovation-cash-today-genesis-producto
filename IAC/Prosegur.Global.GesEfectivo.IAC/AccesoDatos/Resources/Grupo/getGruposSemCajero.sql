select gepr_tgrupo.oid_grupo,
       gepr_tgrupo.cod_grupo,
       gepr_tgrupo.des_grupo
  from gepr_tgrupo 
 inner join gepr_tcajero on gepr_tgrupo.oid_grupo = gepr_tcajero.oid_grupo(+)
 where gepr_tcajero.oid_grupo is null
 order by des_grupo