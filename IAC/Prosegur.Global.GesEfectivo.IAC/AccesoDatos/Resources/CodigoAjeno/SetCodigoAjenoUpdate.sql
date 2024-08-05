UPDATE GEPR_TCODIGO_AJENO
   SET OID_TABLA_GENESIS = []OID_TABLA_GENESIS,
       COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS,
       COD_IDENTIFICADOR = []COD_IDENTIFICADOR,
       COD_AJENO = []COD_AJENO,
       DES_AJENO = []DES_AJENO,
       BOL_ACTIVO = []BOL_ACTIVO,
       BOL_DEFECTO = []BOL_DEFECTO,
       GMT_CREACION = CASE WHEN TO_CHAR([]GMT_CREACION, 'YYYY') = '0001' THEN SYSTIMESTAMP ELSE []GMT_CREACION END,
       DES_USUARIO_CREACION = []DES_USUARIO_CREACION,
       GMT_MODIFICACION = CASE WHEN TO_CHAR([]GMT_MODIFICACION, 'YYYY') = '0001' THEN SYSTIMESTAMP ELSE []GMT_MODIFICACION END,
       DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION
 WHERE OID_CODIGO_AJENO = []OID_CODIGO_AJENO
 AND (BOL_MIGRADO <> 1 OR (BOL_MIGRADO = 1 AND COD_IDENTIFICADOR = []COD_IDENTIFICADOR))