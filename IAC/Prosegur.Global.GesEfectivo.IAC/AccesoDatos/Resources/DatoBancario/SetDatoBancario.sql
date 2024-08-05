﻿MERGE INTO SAPR_TDATO_BANCARIO
USING DUAL ON
(
	  OID_DATO_BANCARIO = []OID_DATO_BANCARIO
)
WHEN MATCHED THEN
    UPDATE SET 
	OID_BANCO = []OID_BANCO
	,OID_DIVISA = []OID_DIVISA
	,COD_TIPO_CUENTA_BANCARIA = []COD_TIPO_CUENTA_BANCARIA
	,COD_CUENTA_BANCARIA = []COD_CUENTA_BANCARIA
	,COD_DOCUMENTO = []COD_DOCUMENTO
	,DES_TITULARIDAD = []DES_TITULARIDAD
	,DES_OBSERVACIONES = []DES_OBSERVACIONES
	,BOL_DEFECTO = []BOL_DEFECTO
	,BOL_ACTIVO = 1	
	
	,COD_AGENCIA 			= []COD_AGENCIA 
	,DES_CAMPO_ADICIONAL_1 	= []DES_CAMPO_ADICIONAL_1
	,DES_CAMPO_ADICIONAL_2 	= []DES_CAMPO_ADICIONAL_2
	,DES_CAMPO_ADICIONAL_3 	= []DES_CAMPO_ADICIONAL_3
	,DES_CAMPO_ADICIONAL_4 	= []DES_CAMPO_ADICIONAL_4
	,DES_CAMPO_ADICIONAL_5 	= []DES_CAMPO_ADICIONAL_5
	,DES_CAMPO_ADICIONAL_6 	= []DES_CAMPO_ADICIONAL_6
	,DES_CAMPO_ADICIONAL_7 	= []DES_CAMPO_ADICIONAL_7
	,DES_CAMPO_ADICIONAL_8	= []DES_CAMPO_ADICIONAL_8
	,GMT_MODIFICACION = []GMT_MODIFICACION
	,DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION
WHEN NOT MATCHED THEN
    INSERT (
     OID_DATO_BANCARIO
	,OID_BANCO
	,OID_CLIENTE
	,OID_SUBCLIENTE
	,OID_PTO_SERVICIO
	,OID_DIVISA
	,COD_TIPO_CUENTA_BANCARIA
	,COD_CUENTA_BANCARIA
	,COD_DOCUMENTO
	,DES_TITULARIDAD
	,DES_OBSERVACIONES
	,BOL_DEFECTO
	,BOL_ACTIVO
	,COD_AGENCIA 
	,DES_CAMPO_ADICIONAL_1 
	,DES_CAMPO_ADICIONAL_2 
	,DES_CAMPO_ADICIONAL_3 
	,DES_CAMPO_ADICIONAL_4 
	,DES_CAMPO_ADICIONAL_5 
	,DES_CAMPO_ADICIONAL_6 
	,DES_CAMPO_ADICIONAL_7 
	,DES_CAMPO_ADICIONAL_8
	,GMT_CREACION
	,DES_USUARIO_CREACION
	,GMT_MODIFICACION
	,DES_USUARIO_MODIFICACION)
	VALUES
	([]OID_DATO_BANCARIO
	,[]OID_BANCO
	,[]OID_CLIENTE
	,[]OID_SUBCLIENTE
	,[]OID_PTO_SERVICIO
	,[]OID_DIVISA
	,[]COD_TIPO_CUENTA_BANCARIA
	,[]COD_CUENTA_BANCARIA
	,[]COD_DOCUMENTO
	,[]DES_TITULARIDAD
	,[]DES_OBSERVACIONES
	,[]BOL_DEFECTO
	,[]BOL_ACTIVO	
	,[]COD_AGENCIA 
	,[]DES_CAMPO_ADICIONAL_1 
	,[]DES_CAMPO_ADICIONAL_2 
	,[]DES_CAMPO_ADICIONAL_3 
	,[]DES_CAMPO_ADICIONAL_4 
	,[]DES_CAMPO_ADICIONAL_5 
	,[]DES_CAMPO_ADICIONAL_6 
	,[]DES_CAMPO_ADICIONAL_7 
	,[]DES_CAMPO_ADICIONAL_8
	,[]GMT_CREACION
	,[]DES_USUARIO_CREACION
	,[]GMT_MODIFICACION
	,[]DES_USUARIO_MODIFICACION
	)