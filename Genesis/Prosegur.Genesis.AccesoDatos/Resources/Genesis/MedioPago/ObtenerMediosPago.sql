﻿SELECT 
  MP.OID_MEDIO_PAGO,
  MP.COD_MEDIO_PAGO,
  MP.DES_MEDIO_PAGO,
  MP.OBS_MEDIO_PAGO,
  MP.BOL_VIGENTE,
  MP.COD_USUARIO,
  MP.FYH_ACTUALIZACION,
  MP.COD_TIPO_MEDIO_PAGO,
  T.OID_TERMINO T_OID_TERMINO, 
  T.COD_TERMINO T_COD_TERMINO, 
  T.DES_TERMINO T_DES_TERMINO, 
  T.OBS_TERMINO T_OBS_TERMINO, 
  T.DES_VALOR_INICIAL T_DES_VALOR_INICIAL, 
  T.NEC_LONGITUD T_NEC_LONGITUD, 
  T.OID_FORMATO T_OID_FORMATO, 
  T.OID_MASCARA T_OID_MASCARA, 
  T.OID_ALGORITMO_VALIDACION T_OID_ALGORITMO_VALIDACION, 
  T.BOL_MOSTRAR_CODIGO T_BOL_MOSTRAR_CODIGO, 
  T.NEC_ORDEN T_NEC_ORDEN, 
  T.BOL_VIGENTE T_BOL_VIGENTE, 
  T.COD_USUARIO T_COD_USUARIO, 
  T.FYH_ACTUALIZACION T_FYH_ACTUALIZACION
FROM 
  GEPR_TMEDIO_PAGO MP
LEFT JOIN GEPR_TTERMINO_MEDIO_PAGO T ON MP.OID_MEDIO_PAGO = T.OID_MEDIO_PAGO
