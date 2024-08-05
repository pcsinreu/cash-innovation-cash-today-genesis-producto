SELECT RF.idFormulario as Id, Descripcion
  FROM PD_ReporteFormulario RF
 INNER JOIN PD_Formulario F ON RF.IdFormulario = F.IdFormulario
 WHERE RF.IdReporte = :IdReporte