SELECT M.Accion
  FROM PD_Motivo M
 INNER JOIN PD_Formulario F ON F.IdMotivo = M.IdMotivo
                           AND F.IdFormulario = :IdFormulario