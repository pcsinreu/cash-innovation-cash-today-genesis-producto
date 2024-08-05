SELECT CE.IDCampoExtra as Id,
       CE.Nombre,
       TCE.IdTipoCampoExtra as IdTipoCampoExtra,
       TCE.Descripcion as TCEDescripcion,
       TCE.Codigo as TCECodigo,
       '' as Valor,
       CE.SeValida as SeValida
  FROM PD_CampoExtra CE
 INNER JOIN PD_TipoCampoExtra TCE ON CE.IdTipoCampoExtra = TCE.IdTipoCampoExtra
 INNER JOIN PD_CampoExtraFormulario CEF ON CE.IdCampoExtra = CEF.IdCampoExtra
 WHERE CEF.IdFormulario = :IdFormulario