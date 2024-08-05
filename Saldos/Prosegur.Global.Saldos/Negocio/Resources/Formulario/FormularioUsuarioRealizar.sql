SELECT U.IdUsuario as Id, U.Nombre, U.ApellidoNombre, U.Bloqueado
  FROM PD_FormularioUsuario FU
 INNER JOIN PD_Usuario U ON FU.IdUsuario = U.IdUsuario
 WHERE FU.IdFormulario = :IdFormulario