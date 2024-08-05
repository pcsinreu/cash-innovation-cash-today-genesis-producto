Select P.Descripcion Planta,
       C.IdPS IdPS,
       C.Descripcion Cliente,
       CASE WHEN C.esbanco = 1 THEN 'Banco' ELSE 'Cliente' END Tipo,
       CC.Descripcion Canal,
       M.Descripcion Moneda,
       M.IdMoneda IdMoneda,
       Sum(Importe) Importe
  From PD_Moneda M Inner Join
  (PD_Cliente C Inner Join  
  (PD_Banco B Inner Join
  (PD_Cliente CC Inner Join
  (PD_Planta P Inner Join
  (Pd_SaldobasicoCabecera SBC Inner Join PD_CentroProceso CP 
       ON SBC.IdCentroProceso = CP.IdCentroProceso) 
       ON CP.IdPlanta = P.IdPlanta)         
       ON SBC.IdBanco = CC.IdCliente) 
       ON CC.IdCliente = B.IdBanco)
       ON SBC.IdCliente = C.IdCliente) 
       ON SBC.IdMoneda = M.IdMoneda
 Where (InStr(C.IDPS, '-') - 1) > 0 And P.ZonaG <> 'NO'
 Group by P.Descripcion,
          C.IdPS,
          C.Descripcion,
          C.esbanco,
          CC.Descripcion,
          M.Descripcion,
          M.IdMoneda
 Having Sum(Importe) <> 0
