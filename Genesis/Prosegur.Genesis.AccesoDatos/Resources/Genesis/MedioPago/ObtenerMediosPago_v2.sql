﻿SELECT 
 DIV.COD_ISO_DIVISA
  
,MP.OID_DIVISA
,MP.OID_MEDIO_PAGO
,MP.COD_MEDIO_PAGO
,MP.DES_MEDIO_PAGO
,MP.OBS_MEDIO_PAGO
,MP.BOL_VIGENTE
,MP.COD_USUARIO
,MP.FYH_ACTUALIZACION
,MP.COD_TIPO_MEDIO_PAGO

,T.OID_TERMINO AS T_OID_TERMINO
,T.COD_TERMINO AS T_COD_TERMINO
,T.DES_TERMINO AS T_DES_TERMINO
,T.OBS_TERMINO AS T_OBS_TERMINO
,T.DES_VALOR_INICIAL AS T_DES_VALOR_INICIAL
,T.NEC_LONGITUD AS T_NEC_LONGITUD
,T.OID_FORMATO AS T_OID_FORMATO
,T.OID_MASCARA AS T_OID_MASCARA
,T.OID_ALGORITMO_VALIDACION AS T_OID_ALGORITMO_VALIDACION
,T.BOL_MOSTRAR_CODIGO AS T_BOL_MOSTRAR_CODIGO
,T.NEC_ORDEN AS T_NEC_ORDEN
,T.BOL_VIGENTE AS T_BOL_VIGENTE
,T.COD_USUARIO AS T_COD_USUARIO
,T.FYH_ACTUALIZACION AS T_FYH_ACTUALIZACION
  
,FORM.OID_FORMATO
,FORM.COD_FORMATO
,FORM.DES_FORMATO

,AV.OID_ALGORITMO_VALIDACION
,AV.COD_ALGORITMO_VALIDACION
,AV.DES_ALGORITMO_VALIDACION
,AV.OBS_ALGORITMO_VALIDACION
,AV.BOL_APLICA_TERM_MEDIO_PAGO AS AV_BOL_APLICA_TERM_MEDIO_PAGO
,AV.BOL_APLICA_TERM_IAC AS AV_BOL_APLICA_TERM_IAC

,M.OID_MASCARA
,M.COD_MASCARA
,M.DES_MASCARA
,M.DES_EXP_REGULAR
,M.BOL_APLICA_TERM_MEDIO_PAGO AS M_BOL_APLICA_TERM_MEDIO_PAGO
,M.BOL_APLICA_TERM_IAC AS M_BOL_APLICA_TERM_IAC

,VT.OID_VALOR
,VT.COD_VALOR
,VT.DES_VALOR
,VT.BOL_VIGENTE AS VT_BOL_VIGENTE
       
FROM GEPR_TMEDIO_PAGO MP
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = MP.OID_DIVISA
LEFT OUTER JOIN GEPR_TTERMINO_MEDIO_PAGO T ON MP.OID_MEDIO_PAGO = T.OID_MEDIO_PAGO
LEFT OUTER JOIN GEPR_TFORMATO FORM ON FORM.OID_FORMATO = T.OID_FORMATO
LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
LEFT OUTER JOIN GEPR_TMASCARA M ON M.OID_MASCARA = T.OID_MASCARA
LEFT OUTER JOIN GEPR_TVALOR_TERMINO_MEDIO_PAGO VT ON VT.OID_TERMINO = T.OID_TERMINO
WHERE 1 = 1 
 {0}