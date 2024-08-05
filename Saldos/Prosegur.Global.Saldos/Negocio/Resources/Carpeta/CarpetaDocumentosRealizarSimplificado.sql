SELECT DISTINCT DC.IdDocumento,
                DC.Fecha,
                DC.NumComprobante,
                DC.IdCentroProcesoOrigen,
                F.Descripcion,
                F.Color,
                CPO.Descripcion as CentroProcesoOrigenDesc,
                DC.IdCentroProcesoDestino,
                CPD.Descripcion as CentroProcesoDestinoDesc,
                0 as IdClienteOrigen,
                ' ' as ClienteOrigenDesc,
                0 as IdClienteDestino,
                ' ' as ClienteDestinoDesc,
                0 as Idbanco,
                ' ' as BancoDesc,
                0 as IdBancoDeposito,
                ' ' as BancoDepositoDesc,
                DC.IdEstadoComprobante,
                U.IdUsuario,
                U.ApellidoNombre as Nombre,
                DC.IdFormulario,
                DC.IdBancoDeposito as BD,
                UR.IdUsuario AS IdUsuarioResuelve,
                UR.ApellidoNombre AS NombreResuelve,
				UD.IdUsuario AS IdUsuarioDispone,
                UD.ApellidoNombre AS NombreDispone,
                DC.FechaResuelve,
                DC.FechaGestion,
                DC.NumExterno,
                NVL(DC.Agrupado, 0) as Agrupado,
                NVL(DC.EsGrupo, 0) as EsGrupo,
                NVL(DC.IdGrupo, 0) as IdGrupo,
                NVL(DC.IdOrigen, 0) as IdOrigen,
                NVL(DC.Reenviado, 0) as Reenviado,
                NVL(DC.Disponible, 1) as Disponible,                
                DC.FechaDispone,
                NVL(DC.Sustituido, 0) as Sustituido
  FROM PD_DOCUMENTOCABECERA DC INNER JOIN PD_FORMULARIO F ON DC.IdFormulario = F.IdFormulario
  INNER JOIN PD_Usuario U ON DC.IdUsuario = U.IdUsuario
  INNER JOIN PD_Usuario UR ON DC.IdUsuario = UR.IdUsuario
  INNER JOIN PD_Usuario UD ON DC.IdUsuario = UD.IdUsuario
  LEFT JOIN PD_CentroProceso CPD ON CPD.IDCENTROPROCESO = DC.IdCentroProcesoDestino
  LEFT JOIN PD_CentroProceso CPO ON CPO.IDCENTROPROCESO = DC.Idcentroprocesoorigen
 WHERE ((:VistaDestinatario = 0 AND DC.IdCentroProcesoOrigen = :IdCentroProceso) OR
       (:VistaDestinatario = 1 AND DC.IdCentroProcesoDestino = :IdCentroProceso AND DC.IdCentroProcesoOrigen <> DC.IdCentroProcesoDestino))
   AND DC.IdEstadoComprobante = :IdEstadoComprobante
   AND DC.IdGrupo IS NULL 