/* Nueva query para obterner Terminos de un GrupoTerminosIAC del Formulario ObtenerTerminos_v2.sql */

SELECT 
 TI.OID_IAC
,TI.OID_TERMINO
,TI.BOL_BUSQUEDA_PARCIAL
,TI.BOL_CAMPO_CLAVE
,TI.NEC_ORDEN
,TI.BOL_ES_OBLIGATORIO
,TI.BOL_TERMINO_COPIA
,TI.BOL_ES_PROTEGIDO

,T.COD_TERMINO
,T.DES_TERMINO
,T.OBS_TERMINO
,T.NEC_LONGITUD
,T.BOL_MOSTRAR_CODIGO
,T.BOL_VALORES_POSIBLES
,T.BOL_ACEPTAR_DIGITACION
,T.BOL_VIGENTE
,T.BOL_ESPECIFICO_DE_SALDOS

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

FROM GEPR_TTERMINO_POR_IAC TI
INNER JOIN GEPR_TTERMINO T ON T.OID_TERMINO = TI.OID_TERMINO
INNER JOIN GEPR_TFORMATO FORM ON FORM.OID_FORMATO = T.OID_FORMATO
INNER JOIN SAPR_TFORMULARIO F ON F.OID_IAC_INDIVIDUAL = TI.OID_IAC
LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
LEFT OUTER JOIN GEPR_TMASCARA M ON M.OID_MASCARA = T.OID_MASCARA
WHERE 1 = 1 {0}

UNION

SELECT 
 TI.OID_IAC
,TI.OID_TERMINO
,TI.BOL_BUSQUEDA_PARCIAL
,TI.BOL_CAMPO_CLAVE
,TI.NEC_ORDEN
,TI.BOL_ES_OBLIGATORIO
,TI.BOL_TERMINO_COPIA
,TI.BOL_ES_PROTEGIDO

,T.COD_TERMINO
,T.DES_TERMINO
,T.OBS_TERMINO
,T.NEC_LONGITUD
,T.BOL_MOSTRAR_CODIGO
,T.BOL_VALORES_POSIBLES
,T.BOL_ACEPTAR_DIGITACION
,T.BOL_VIGENTE
,T.BOL_ESPECIFICO_DE_SALDOS

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

FROM GEPR_TTERMINO_POR_IAC TI
INNER JOIN GEPR_TTERMINO T ON T.OID_TERMINO = TI.OID_TERMINO
INNER JOIN GEPR_TFORMATO FORM ON FORM.OID_FORMATO = T.OID_FORMATO
INNER JOIN SAPR_TFORMULARIO F ON F.OID_IAC_GRUPO = TI.OID_IAC
LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
LEFT OUTER JOIN GEPR_TMASCARA M ON M.OID_MASCARA = T.OID_MASCARA
WHERE 1 = 1 {0}
