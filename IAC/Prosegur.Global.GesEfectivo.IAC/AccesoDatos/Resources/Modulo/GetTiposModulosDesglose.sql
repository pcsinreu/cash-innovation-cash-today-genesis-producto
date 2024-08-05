﻿SELECT 
LV.COD_VALOR COD_EMBALAJE,
TM.OID_MODULO,
TM.COD_MODULO,
TM.DES_MODULO,
TM.BOL_ACTIVO,
TM.GMT_CREACION,
TM.DES_USUARIO_CREACION,
TM.GMT_MODIFICACION,
TM.DES_USUARIO_MODIFICACION,
CLI.COD_CLIENTE,
DIV.COD_ISO_DIVISA,
DE.COD_DENOMINACION,
DE.DES_DENOMINACION,
DE.BOL_BILLETE,
DE.NUM_VALOR,
TMD.OID_MODULO_DESGLOSE,
TMD.NEL_UNIDADES,
TMD.GMT_CREACION GMT_CREACION_D,
TMD.DES_USUARIO_CREACION DES_USUARIO_CREACION_D,
TMD.GMT_MODIFICACION GMT_MODIFICACION_D,
TMD.DES_USUARIO_MODIFICACION DES_USUARIO_MODIFICACION_D
FROM GEPR_TMODULO TM
INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = TM.OID_LISTA_VALOR
INNER JOIN GEPR_TMODULO_DESGLOSE TMD ON TMD.OID_MODULO = TM.OID_MODULO
INNER JOIN GEPR_TDENOMINACION DE ON DE.OID_DENOMINACION = TMD.OID_DENOMINACION
INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = DE.OID_DIVISA
LEFT JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = TM.OID_CLIENTE
WHERE 0 = 0 