﻿SELECT
OID_DOCUMENTO
,OID_FORMULARIO
,OID_CUENTA_ORIGEN
,OID_CUENTA_DESTINO
,OID_CUENTA_SALDO_ORIGEN
,OID_CUENTA_SALDO_DESTINO
,OID_DOCUMENTO_PADRE
,OID_DOCUMENTO_SUSTITUTO
,OID_TIPO_DOCUMENTO
,OID_SECTOR_ORIGEN
,OID_SECTOR_DESTINO
,FYH_PLAN_CERTIFICACION
,BOL_CERTIFICADO
,FYH_CIERRE_GESTION
,COD_EXTERNO
,COD_ESTADO
,BOL_IMPORTADO_LEGADO
,BOL_EXPORTADO_CONTEO
,BOL_EXPORTADO_SALIDAS
,GMT_CREACION
,DES_USUARIO_CREACION
,GMT_MODIFICACION
,DES_USUARIO_MODIFICACION
FROM SAPR_TDOCUMENTO
WHERE OID_DOCUMENTO = []OID_DOCUMENTO