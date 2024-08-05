SELECT IdUsuario,
	   Bloqueado
  FROM PD_Usuario
 WHERE Nombre = :Nombre
   and Clave = :Clave