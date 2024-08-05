SELECT idplanta,
       descripcion,
       idps,       
       coddelegaciongenesis
FROM pd_planta
WHERE coddelegaciongenesis = :CodDelegacion