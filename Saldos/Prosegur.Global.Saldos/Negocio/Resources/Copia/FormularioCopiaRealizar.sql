SELECT C.IdFormulario   as Id,
       C.Destinatario as Destinatario,
       TC.IdTipoCopia as IdTipoCopia,
       TC.Descripcion as Descripcion
  FROM PD_Copia C
 INNER JOIN PD_TipoCopia TC ON C.IdTipoCopia = TC.IdTipoCopia
 WHERE C.IdFormulario = :IdFormulario