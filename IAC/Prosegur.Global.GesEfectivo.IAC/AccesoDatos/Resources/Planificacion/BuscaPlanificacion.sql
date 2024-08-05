﻿SELECT 
          P.OID_PLANIFICACION,
          P.COD_PLANIFICACION,
          P.DES_PLANIFICACION,
          P.FYH_VIGENCIA_INICIO,
          P.FYH_VIGENCIA_FIN,
          P.BOL_ACTIVO,
          P.GMT_CREACION,
          P.DES_USUARIO_CREACION,
          P.GMT_MODIFICACION,
          P.DES_USUARIO_MODIFICACION,
          C.OID_CLIENTE,
          C.COD_CLIENTE,
          C.DES_CLIENTE,  
          PG.OID_PLANXPROGRAMACION,
          PG.NEC_DIA_INICIO,
          PG.FYH_HORA_INICIO,            
          PG.NEC_DIA_FIN,
          PG.FYH_HORA_FIN,
          PG.GMT_CREACION PROG_GMT_CREACION,
          PG.DES_USUARIO_CREACION PROG_DES_USUARIO_CREACION,
          PG.GMT_MODIFICACION PROG_GMT_MODIFICACION,
          PG.DES_USUARIO_MODIFICACION PROG_DES_USUARIO_MODIFICACION,
		  TT.OID_TIPO_PLANIFICACION,
		  TT.DES_TIPO_PLANIFICACION
FROM SAPR_TPLANIFICACION P
  INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = P.OID_CLIENTE
  INNER JOIN SAPR_TTIPO_PLANIFICACION TT ON P.OID_TIPO_PLANIFICACION = TT.OID_TIPO_PLANIFICACION
  INNER JOIN SAPR_TPLANXPROGRAMACION PG ON PG.OID_PLANIFICACION = P.OID_PLANIFICACION