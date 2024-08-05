/* Nueva query para verificar si existe alguno documento con el codigo externo Documento_ValidarSiExisteCodigoExterno.sql */
SELECT COUNT(1)
FROM SAPR_TDOCUMENTO D
WHERE D.COD_EXTERNO = []COD_EXTERNO