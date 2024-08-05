/* Nueva query para obterner HistoricoMovimientacion HistoricoMovimentacionDocumentosObtener.sql */
SELECT 
 OID_DOCUMENTO
,COD_ESTADO
,GMT_MODIFICACION
,DES_USUARIO_MODIFICACION
FROM SAPR_THIST_MOV_DOCUMENTO 
WHERE 1 = 1
