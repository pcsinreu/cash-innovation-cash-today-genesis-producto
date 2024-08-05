SELECT F.IdFormulario as Id,
       F.Descripcion,
       F.SoloEnGrupo,
       F.ConValores,
       F.ConBultos,
       F.BasadoEnReporte,
       nvl(F.IdReporte, 0) as IdReporte,
       F.BasadoEnSaldos,
       F.SeImprime,
       F.Interplantas,
       nvl(F.BasadoEnExtracto, 0) as BasadoEnExtracto,
       F.TotalCero
  FROM PD_FormularioUsuario FU
 INNER JOIN PD_Formulario F ON FU.IdFormulario = F.IdFormulario
