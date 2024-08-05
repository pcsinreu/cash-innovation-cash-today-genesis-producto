SELECT
  SUM(
  CASE
    WHEN  
    (select 
		1 as Respaldos
     from 
		GEPR_TBULTO B 
		inner join gepr_ttipo_procesado tp on B.oid_bulto = tp.oid_bulto
        inner join gepr_tcarac_por_tipo_procesado ctpr on tp.oid_tipo_procesado = ctpr.oid_tipo_procesado 
        and ctpr.cod_caracteristica = 'CodCarac004' 
     where b.oid_bulto not in (
        Select 
			tp2.oid_bulto
        from 
			gepr_ttipo_procesado tp2 
			inner join gepr_tcarac_por_tipo_procesado ctpr2 on tp2.oid_tipo_procesado = ctpr2.oid_tipo_procesado 
            and ctpr2.cod_caracteristica = 'CodCarac001'
        where 
			tp2.oid_bulto = b.oid_bulto)
			and b.oid_remesa=TBULTLISTA.oid_remesa
			and rownum = 1
        group by b.oid_remesa
    ) is null THEN
    TBULTLISTA.NEC_NUM_PARCIALES_LISTA
    else
      1
    end) AS  NUM_PARCIALES_CON,
  SUM(
      CASE
        WHEN  
		(select 
			1 as Respaldos
		from 
			GEPR_TBULTO B 
			inner join gepr_ttipo_procesado tp on B.oid_bulto = tp.oid_bulto
			inner join gepr_tcarac_por_tipo_procesado ctpr on tp.oid_tipo_procesado = ctpr.oid_tipo_procesado 
			and ctpr.cod_caracteristica = 'CodCarac004' 
		where 
			b.oid_bulto not in (
				Select 
					tp2.oid_bulto
				from 
					gepr_ttipo_procesado tp2 
					inner join gepr_tcarac_por_tipo_procesado ctpr2 on tp2.oid_tipo_procesado = ctpr2.oid_tipo_procesado 
					and ctpr2.cod_caracteristica = 'CodCarac001'
				where 
					tp2.oid_bulto = b.oid_bulto)
			and b.oid_remesa=TBULTLISTA.oid_remesa
			and rownum = 1
		group by 
			b.oid_remesa) is null THEN
			TBULTLISTA.NEC_PARCIALES_DECL
         else
			1
         end) AS  NUM_PARCIALES_DECL,             
      TBULTLISTA.COD_PUNTO_SERVICIO ESTACION,
      TBULTLISTA.DES_PUNTO_SERVICIO DES_ESTACION       
FROM
      (
		SELECT      
			TBULT.OID_BULTO OID_BULTO,
			TBULT.NEC_PARCIALES NEC_PARCIALES_DECL,
			DECODE(NVL(TLISTA.NEC_NUM_PARCIALES,0), 0, 1, TLISTA.NEC_NUM_PARCIALES) NEC_NUM_PARCIALES_LISTA,
			TBULT.COD_PUNTO_SERVICIO,
			TBULT.DES_PUNTO_SERVICIO,
			TREME.oid_remesa
		FROM 
			GEPR_TBULTO  TBULT 
			INNER JOIN GEPR_TREMESA TREME ON TBULT.OID_REMESA=TREME.OID_REMESA 
			INNER JOIN  GEPR_TLISTA_TRABAJO  TLISTA ON      TBULT.OID_BULTO=TLISTA.OID_BULTO  
			WHERE
			TBULT.COD_CLIENTE = []COD_CLIENTE AND
			TBULT.COD_DELEGACION = []COD_DELEGACION 
			[FEC_TRANSPORTE] 
			[FEC_PROCESO] 
			[FYH_FIN_CONTEO] 	
			AND TREME.COD_ESTADO IN('PR','EN','NEN','SA','NSA','MS')                      
			[FILTRO_PROCESO] 
     )    TBULTLISTA
     LEFT OUTER JOIN
      (
		SELECT      
			DECODE(SUM(NVL(TPARC.NEC_NUMERO_PARCIALES,0)), 0, 1, SUM(NVL(TPARC.NEC_NUMERO_PARCIALES,0))) NEC_NUM_PACIALES_TPARC, 
			TPARC.OID_BULTO_PADRE OID_BULTO
		FROM  
			GEPR_TBULTO_PARCIALIZADO  TPARC 
			INNER JOIN  GEPR_TBULTO TBULT ON TPARC.OID_BULTO_PADRE=TBULT.OID_BULTO
			INNER JOIN GEPR_TREMESA TREME ON TBULT.OID_REMESA=TREME.OID_REMESA
		WHERE 
			TBULT.COD_CLIENTE = []COD_CLIENTE AND
			TBULT.COD_DELEGACION = []COD_DELEGACION 
			[FEC_TRANSPORTE] 
			[FEC_PROCESO] 
			[FYH_FIN_CONTEO] 			
			AND TREME.COD_ESTADO IN('PR','EN','NEN','SA','NSA','MS')
			[FILTRO_PROCESO]
		GROUP BY  
			TPARC.OID_BULTO_PADRE
      )   TBULTOPARC    ON  TBULTLISTA.OID_BULTO=TBULTOPARC.OID_BULTO
GROUP BY 
      TBULTLISTA.COD_PUNTO_SERVICIO,
      TBULTLISTA.DES_PUNTO_SERVICIO
ORDER BY
    ESTACION