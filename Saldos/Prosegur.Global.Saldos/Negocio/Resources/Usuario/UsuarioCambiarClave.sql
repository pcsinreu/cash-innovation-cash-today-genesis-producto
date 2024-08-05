UPDATE PD_Usuario
   SET Clave = :ClaveNueva, FechaCambioClave = sysdate, Caduco = 0
 WHERE IdUsuario = :IdUsuario