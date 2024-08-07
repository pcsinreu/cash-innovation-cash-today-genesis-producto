﻿SELECT GEPR_TPARAMETRO.OID_PARAMETRO,
	   GEPR_TPARAMETRO_VALOR.OID_PARAMETRO_VALOR,
       GEPR_TPARAMETRO_VALOR.DES_VALOR_PARAMETRO,
	   GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO,
	   GEPR_TPARAMETRO.COD_PARAMETRO
FROM GEPR_TPARAMETRO 
INNER JOIN GEPR_TNIVEL_PARAMETRO ON
  GEPR_TNIVEL_PARAMETRO.OID_NIVEL_PARAMETRO = GEPR_TPARAMETRO.OID_NIVEL_PARAMETRO
INNER JOIN GEPR_TAPLICACION ON 
  GEPR_TAPLICACION.OID_APLICACION = GEPR_TPARAMETRO.OID_APLICACION
LEFT JOIN GEPR_TPARAMETRO_VALOR ON
  GEPR_TPARAMETRO_VALOR.OID_PARAMETRO = GEPR_TPARAMETRO.OID_PARAMETRO
WHERE
GEPR_TAPLICACION.COD_APLICACION = []COD_APLICACION 
AND
(
	(
		(TRIM([]COD_ID_PAIS) IS NOT NULL AND TRIM([]COD_ID_DELEGACION) IS NOT NULL)
		AND 
		(
			(GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO = []COD_NIVEL_PAIS
			AND GEPR_TPARAMETRO_VALOR.COD_IDENTIFICADOR_NIVEL =  []COD_ID_PAIS)
			OR
			(GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO = []COD_NIVEL_DELEGACION
			AND GEPR_TPARAMETRO_VALOR.COD_IDENTIFICADOR_NIVEL =  []COD_ID_DELEGACION)
		)
	)
	OR
	(TRIM([]COD_ID_PLANTA) IS NOT NULL
	AND GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO = []COD_NIVEL_PLANTA
	AND GEPR_TPARAMETRO_VALOR.COD_IDENTIFICADOR_NIVEL =  []COD_ID_PLANTA)
	OR
	(TRIM([]COD_ID_PUESTO) IS NOT NULL 
	AND GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO = []COD_NIVEL_PUESTO
	AND GEPR_TPARAMETRO_VALOR.COD_IDENTIFICADOR_NIVEL =  []COD_ID_PUESTO)
)