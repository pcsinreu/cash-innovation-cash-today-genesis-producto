SELECT IDPS,
       Descripcion
FROM PD_CentroProceso
WHERE IDPS Like :IdsPlanta || '%'
   Or IDPS Like ' ' ||  :IdsPlanta || '%'
ORDER BY IDPS, Descripcion