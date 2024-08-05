SELECT CE.IDCampoExtra as Id,
       CE.Nombre,
       TCE.IdTipoCampoExtra as IdTipoCampoExtra,
       TCE.Descripcion as TCEDescripcion,
       TCE.Codigo as TCECodigo,
       nvl(Valor, '') as Valor,
       CE.SeValida as SeValida
  FROM PD_CampoExtraValor CEV
 RIGHT JOIN(PD_CampoExtraFormulario CEF
 INNER JOIN(PD_CampoExtra CE
 INNER JOIN PD_TipoCampoExtra TCE ON CE.IdTipoCampoExtra =
                                     TCE.IdTipoCampoExtra) ON CE.IdCampoExtra = CEF.IdCampoExtra) 
                                     ON CE.IdCampoExtra = CEV.IdCampoExtra AND CEV.IdDocumento = :IdDocumento
 Where CEF.IdFormulario = :IdFormulario