﻿MERGE INTO SAPR_THIST_MOV_DOCUMENTO H
USING DUAL
ON (H.OID_DOCUMENTO = []OID_DOCUMENTO  AND H.COD_ESTADO  =[]COD_ESTADO)
WHEN MATCHED THEN
  UPDATE 
	SET H.GMT_MODIFICACION =FN_GMT_ZERO_###VERSION###
		,H.DES_USUARIO_MODIFICACION	=[]DES_USUARIO_MODIFICACION	
WHEN NOT MATCHED THEN
   INSERT (H.OID_HIST_MOV_DOCUMENTO
	,H.OID_DOCUMENTO
	,H.COD_ESTADO
	,H.GMT_CREACION
	,H.DES_USUARIO_CREACION
	,H.GMT_MODIFICACION
	,H.DES_USUARIO_MODIFICACION)
	VALUES
	([]OID_HIST_MOV_DOCUMENTO
	,[]OID_DOCUMENTO
	,[]COD_ESTADO
	,FN_GMT_ZERO_###VERSION###
	,[]DES_USUARIO_CREACION
	,FN_GMT_ZERO_###VERSION###
	,[]DES_USUARIO_MODIFICACION)