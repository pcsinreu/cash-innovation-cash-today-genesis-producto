﻿SELECT DISTINCT

	-- Documento
	DER.OID_DOCUMENTO,
    DER.COD_ESTADO_DOCXELEMENTO,

    -- Cuenta
    CU.OID_CUENTA,
    
    -- Remesas
	R.OID_REMESA,

    -- Bultos
    B.OID_BULTO,

    -- Parciales
    P.OID_PARCIAL

FROM SAPR_TREMESA R
     LEFT  JOIN SAPR_TBULTO B ON B.OID_REMESA = R.OID_REMESA
     LEFT  JOIN SAPR_TPARCIAL P ON P.OID_BULTO = B.OID_BULTO
     
     -- Documentos
	 INNER JOIN SAPR_TDOCUMENTOXELEMENTO DER ON DER.OID_REMESA = R.OID_REMESA
	 AND (DER.OID_BULTO = B.OID_BULTO OR DER.OID_BULTO IS NULL)
	 INNER JOIN SAPR_TDOCUMENTO DOCR ON DER.OID_DOCUMENTO = DOCR.OID_DOCUMENTO

     -- Cuenta
     INNER JOIN SAPR_TCUENTA CU ON CU.OID_CUENTA = B.OID_CUENTA
     INNER JOIN GEPR_TCLIENTE CL ON CL.OID_CLIENTE = CU.OID_CLIENTE
     INNER JOIN GEPR_TSUBCANAL SBC ON SBC.OID_SUBCANAL = CU.OID_SUBCANAL
     INNER JOIN GEPR_TCANAL CAN ON CAN.OID_CANAL = SBC.OID_CANAL
     INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR
     INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR
     INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA
     INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION
     LEFT  JOIN GEPR_TSUBCLIENTE SCL ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
     LEFT  JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
	{0}
    {1}
