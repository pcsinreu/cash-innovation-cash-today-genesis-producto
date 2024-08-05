SELECT ApellidoNombre,
       Nombre,
       Clave,
       nvl(SeleccionCP, 0) as ElijeCP,
       Caduca,
       Caduco,
       DiasdeValidez,
       FechaCambioClave
  FROM PD_Usuario
 WHERE IdUsuario = :IdUsuario