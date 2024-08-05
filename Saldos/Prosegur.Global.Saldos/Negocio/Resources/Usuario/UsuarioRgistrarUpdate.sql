UPDATE PD_Usuario
   SET Nombre         = :Nombre,
       ApellidoNombre = :ApellidoNombre,
       SeleccionCP    = :ElijeCP,
       Caduca         = :Caduca,
       Caduco         = :Caduco,
       DiasDeValidez  = :DiasDeValidez
       {0}
 WHERE IdUsuario = :IdUsuario