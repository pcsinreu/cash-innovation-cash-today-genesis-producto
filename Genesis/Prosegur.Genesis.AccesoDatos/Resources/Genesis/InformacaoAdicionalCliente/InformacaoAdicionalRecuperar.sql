﻿SELECT 
      A.OID_IAC,
      A.COD_IAC,
      A.DES_IAC,
      A.OBS_IAC,
      A.BOL_VIGENTE,
      A.COD_USUARIO,
      A.FYH_ACTUALIZACION,
      A.BOL_COPIA_DECLARADOS,
      A.BOL_INVISIBLE,
      A.BOL_ESPECIFICO_DE_SALDOS
FROM GEPR_TINFORM_ADICIONAL_CLIENTE A 
WHERE BOL_VIGENTE = 1 AND BOL_ESPECIFICO_DE_SALDOS = 1 AND OID_IAC = []OID_IAC