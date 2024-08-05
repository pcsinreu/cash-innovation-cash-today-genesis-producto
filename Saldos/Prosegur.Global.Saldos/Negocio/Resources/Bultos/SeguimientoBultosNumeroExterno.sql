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
	pd_Bulto BUL 
    inner join pd_DocumentoCabecera DOC ON BUL.IdDocumento = NVL(DOC.IdDocBultos, DOC.IdDocumento)
    inner join pd_Formulario FRM ON DOC.IdFormulario = FRM.IdFormulario
    inner join pd_EstadoComprobante EST ON DOC.IdEstadoComprobante = EST.IdEstadoComprobante
    left join pd_Cliente BOR ON DOC.IdBanco = BOR.IdCliente 
    left join pd_Cliente CLO ON DOC.IdClienteOrigen = CLO.IdCliente
    left join pd_Cliente BDE ON DOC.IdBancoDeposito = BDE.IdCliente
    left join pd_Cliente CLD ON DOC.IdclienteDestino = CLD.idcliente
    left join pd_Destino DES ON BUL.IdDestino = DES.IdDestino
    left join pd_CentroProceso CPO ON DOC.IdCentroProcesoOrigen = CPO.IdCentroProceso
    left join pd_CentroProceso CPD ON DOC.IdCentroProcesoDestino = CPD.IdCentroProceso
where 
	DOC.Sustituido = 0
    and DOC.NumExterno = :NumExterno
    and (
			(
				DOC.IdCentroProcesoOrigen in 
											(
												select 
													UCP.IdCentroProceso
                                                from 
													PD_UsuarioCentroProceso UCP
                                                where 
													UCP.IdUsuario = :IdUsuario
											)
			) 
            or 
            (
				DOC.IdCentroProcesoDestino in 
											(
												select 
													UCP.IdCentroProceso
                                                from 
													PD_UsuarioCentroProceso UCP
                                                where 
													UCP.IdUsuario = :IdUsuario
											)
			)
		)
order by  
	DOC.FechaResuelve desc
    ,DOC.NumExterno
    ,BUL.CodBolsa
    ,BUL.NumPrecinto