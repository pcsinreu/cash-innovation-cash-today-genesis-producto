INSERT INTO
  GEPR_TLISTA_TRABAJO
  (OID_LISTA_TRABAJO, OID_REMESA, OID_BULTO, COD_USUARIO, FYH_ACTUALIZACION, OID_PUESTO)
VALUES
  ([]OID_LISTA_TRABAJO, []OID_REMESA, []OID_BULTO, []COD_USUARIO, []FYH_ACTUALIZACION, (SELECT OID_PUESTO FROM GEPR_TPUESTO P WHERE P.COD_PUESTO = []COD_PUESTO AND P.COD_DELEGACION = []COD_DELEGACION))