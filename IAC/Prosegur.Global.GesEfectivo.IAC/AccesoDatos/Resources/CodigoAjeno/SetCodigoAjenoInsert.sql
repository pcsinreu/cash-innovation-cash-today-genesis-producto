INSERT INTO GEPR_TCODIGO_AJENO
  (OID_CODIGO_AJENO, 
  OID_TABLA_GENESIS, 
  COD_TIPO_TABLA_GENESIS, 
  COD_IDENTIFICADOR, 
  COD_AJENO, 
  DES_AJENO, 
  BOL_DEFECTO, 
  BOL_ACTIVO,
  GMT_CREACION, 
  DES_USUARIO_CREACION, 
  GMT_MODIFICACION, 
  DES_USUARIO_MODIFICACION)
VALUES
  ([]OID_CODIGO_AJENO, 
  []OID_TABLA_GENESIS, 
  []COD_TIPO_TABLA_GENESIS, 
  []COD_IDENTIFICADOR, 
  []COD_AJENO, 
  []DES_AJENO, 
  []BOL_DEFECTO, 
  []BOL_ACTIVO, 
  CASE WHEN TO_CHAR([]GMT_CREACION, 'YYYY') = '0001' THEN SYSTIMESTAMP ELSE []GMT_CREACION END, 
  []DES_USUARIO_CREACION, 
  CASE WHEN TO_CHAR([]GMT_MODIFICACION, 'YYYY') = '0001' THEN SYSTIMESTAMP ELSE []GMT_MODIFICACION END, 
  []DES_USUARIO_MODIFICACION)