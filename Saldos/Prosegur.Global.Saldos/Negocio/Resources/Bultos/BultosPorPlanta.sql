SELECT 
	trunc (D.fecha) as Fecha
    ,P.IdPlanta as IdPlanta
    ,P.Descripcion as DescripcionPlanta
    ,F.IdFormulario as IdFormulario	
    ,F.Descripcion as DescripcionFormulario 
    ,count(B.numprecinto) as QtdeBulto
FROM 
	PD_Bulto B
    ,PD_DocumentoCabecera D 
    ,PD_Formulario F
    ,PD_CentroProceso C
    ,PD_Planta P    
	,PD_FormularioReporteCondicion R
WHERE 
	B.IdDocumento = NVL(D.IdDocBultos,D.idDocumento)
    AND F.IdFormulario = D.IdFormulario
    AND D.IdCentroProcesoDestino = C.IdCentroProceso
    AND P.IdPlanta = C.IdPlanta
    AND C.IdCentroProceso IN (
							SELECT 
								IdCentroProceso
			    			FROM 
			    				PD_UsuarioCentroProceso 
							WHERE 
								IdUsuario = :IdUsr)
	AND D.NumExterno IS NOT NULL
	AND D.IdFormulario = R.IdFormulario
	AND R.Reporte = 1
	AND D.FECHA BETWEEN :DataIni AND :DataFim
GROUP BY 
	trunc (D.fecha), 
	P.IdPlanta, 
	P.Descripcion, 
	F.IdFormulario, 
	F.Descripcion       
ORDER BY 
	P.Descripcion, 
	F.Descripcion, 
	trunc(D.fecha)
