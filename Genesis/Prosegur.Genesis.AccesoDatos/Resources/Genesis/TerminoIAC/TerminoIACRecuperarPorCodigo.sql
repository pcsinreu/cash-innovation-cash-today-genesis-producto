﻿SELECT 
TI.OID_TERMINO
,TI.BOL_BUSQUEDA_PARCIAL
,TI.BOL_CAMPO_CLAVE
,TI.NEC_ORDEN
,TI.BOL_ES_OBLIGATORIO
,TI.COD_USUARIO
,TI.FYH_ACTUALIZACION
,TI.BOL_TERMINO_COPIA
,TI.BOL_ES_PROTEGIDO
,TI.COD_MIGRACION
,T.COD_TERMINO
,T.DES_TERMINO
,T.OBS_TERMINO
,T.NEC_LONGITUD
,T.OID_ALGORITMO_VALIDACION
,T.OID_MASCARA
,T.BOL_MOSTRAR_CODIGO
,T.BOL_VALORES_POSIBLES
,T.BOL_ACEPTAR_DIGITACION
,T.BOL_VIGENTE
,T.BOL_ESPECIFICO_DE_SALDOS
,F.OID_FORMATO
,F.COD_FORMATO
,F.DES_FORMATO
,F.COD_USUARIO AS F_COD_USUARIO
,F.FYH_ACTUALIZACION AS F_FYH_ACTUALIZACION
,AV.OID_ALGORITMO_VALIDACION
,AV.COD_ALGORITMO_VALIDACION
,AV.DES_ALGORITMO_VALIDACION
,AV.OBS_ALGORITMO_VALIDACION
,AV.BOL_APLICA_TERM_MEDIO_PAGO AS AV_BOL_APLICA_TERM_MEDIO_PAGO
,AV.BOL_APLICA_TERM_IAC AS AV_BOL_APLICA_TERM_IAC
,AV.COD_USUARIO AS AV_COD_USUARIO
,AV.FYH_ACTUALIZACION AS AV_FYH_ACTUALIZACION
,M.OID_MASCARA
,M.COD_MASCARA
,M.DES_MASCARA
,M.DES_EXP_REGULAR
,M.BOL_APLICA_TERM_MEDIO_PAGO AS M_BOL_APLICA_TERM_MEDIO_PAGO
,M.BOL_APLICA_TERM_IAC AS M_BOL_APLICA_TERM_IAC
,M.COD_USUARIO AS M_COD_USUARIO
,M.FYH_ACTUALIZACION AS M_FYH_ACTUALIZACION
FROM GEPR_TTERMINO_POR_IAC TI
INNER JOIN GEPR_TTERMINO T ON T.OID_TERMINO = TI.OID_TERMINO
INNER JOIN GEPR_TFORMATO F ON F.OID_FORMATO = T.OID_FORMATO
LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
LEFT OUTER JOIN GEPR_TMASCARA M ON M.OID_MASCARA = T.OID_MASCARA
{0}