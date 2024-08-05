SELECT DISTINCT FC.OID_CONFIG_REPORTE, FC.COD_CONFIG_REPORTE, FC.DES_CONFIG_REPORTE, FC.DES_DIRECCION
FROM SAPR_TCONFIG_REPORTE FC
LEFT join sapr_tconfigrpxtipocli CTC on FC.oid_config_reporte = CTC.oid_config_reporte
LEFT join SAPR_TCONFIGRPXCLIENTE C on FC.oid_config_reporte = C.oid_config_reporte
WHERE 1=1
{0}