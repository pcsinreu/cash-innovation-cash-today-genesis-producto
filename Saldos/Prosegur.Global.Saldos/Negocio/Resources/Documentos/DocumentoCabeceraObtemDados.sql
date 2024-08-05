Select IdEstadoComprobante, IdFormulario, nvl(IdOrigen, 0), nvl(EsGrupo, 0)
  FROM PD_DocumentoCabecera
 WHERE IdDocumento = :IdDocumento