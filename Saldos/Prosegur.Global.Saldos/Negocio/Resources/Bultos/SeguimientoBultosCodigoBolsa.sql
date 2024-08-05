select 	
	DOC.FechaResuelve as Fecha
	,DOC.numexterno as NumExterno
	,FRM.Descripcion as Formulario
	,BUL.codbolsa as Bulto
	,BUL.numprecinto as Precinto
	,DES.descripcion as Destino
	,Substr(EST.Descripcion,0,1) as Est
    ,Decode (DOC.Exportado, 1, 'S', 'N') Exp	
	,CPO.Descripcion as CentroProcesoOrigen
	,BOR.Descripcion as CanalOrigen
	,CLO.Descripcion as ClienteOrigen
	,CPD.Descripcion as CentroProcesoDestino
	,BDE.Descripcion as CanalDestino
	,CLD.Descripcion as ClienteDestino
	,DOC.IdDocumento as Documento
from	
	pd_destino DES right JOIN (
		pd_bulto BUL right JOIN (
			pd_cliente BOR right JOIN(
				pd_cliente CLD right JOIN (
					pd_cliente BDE right JOIN (							
						pd_cliente CLO right JOIN (	
							pd_CentroProceso CPO Left JOIN (
								pd_CentroProceso CPD Left JOIN (
									pd_EstadoComprobante EST Left JOIN (		
										(select	
											DOC.IdDocumento,
											DOC.IdCentroProcesoOrigen,
											DOC.IdCentroProcesoDestino,
											DOC.IdClienteOrigen,
											DOC.IdClienteDestino,
											DOC.IdBanco,
											DOC.IdEstadoComprobante,
											DOC.IdFormulario,
											DOC.IdBancoDeposito,
											DOC.FechaResuelve,
											DOC.NumExterno,
											DOC.Sustituido,
											DOC.Exportado,
											DOC.IdDocBultos
										from	
											(Select	
											IdDocumento
										from	
											PD_Bulto
										Where	
											CodBolsa = :CodBolsa
										UNION ALL
										select 	
											DC.iddocumento IdDocumento 
										from	
											 pd_documentocabecera DC
										where 	
											DC.IdDocBultos IN (Select	
															IdDocumento
														from	
															PD_Bulto
														Where	
															CodBolsa = :CodBolsa) ) T,
											 pd_documentocabecera  DOC
										where	
											T.IdDocumento = DOC.IdDocumento
										) DOC 
										Left JOIN pd_Formulario FRM
										ON DOC.sustituido = 0 and DOC.IdFormulario = FRM.idFormulario
									)ON DOC.IdEstadoComprobante = EST.IdEstadoComprobante
								)ON DOC.IdCentroProcesoDestino = CPD.IdCentroproceso
							)ON DOC.IdCentroProcesoOrigen = CPO.IdCentroproceso
						)ON DOC.IdClienteOrigen = CLO.IdCliente
					)ON DOC.IdBancoDeposito = BDE.IdCliente
				)ON DOC.IdclienteDestino  = CLD.idcliente		
			)ON DOC.Idbanco = BOR.idcliente
		)ON NVL(DOC.IdDocBultos,DOC.IDDOCUMENTO) = bul.iddocumento
	)ON DES.IdDestino = BUL.Iddestino	
Where	
	(
	doc.idcentroprocesodestino in ( select	a.idcentroproceso 
					from	PD_UsuarioCentroProceso a
					Where	a.IdUsuario = :IdUsuario
					)    
       )
	OR
	(
	doc.idcentroprocesoorigen in (	 select	a.idcentroproceso 
					from	PD_UsuarioCentroProceso a
					Where	a.IdUsuario = :IdUsuario
					) 
	)										      
Order by
	DOC.FechaResuelve desc,
	DOC.NumExterno, 
	BUL.codbolsa,
	BUL.numprecinto 