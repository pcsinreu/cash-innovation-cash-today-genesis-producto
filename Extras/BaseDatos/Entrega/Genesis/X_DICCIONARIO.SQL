DECLARE
  var$codidioma         gepr_pdiccionario.tipo$codIdioma;
  var$codidiomaVazio    gepr_pdiccionario.tipo$codIdioma;
  var$codvalor          gepr_pdiccionario.tipo$valor;
  var$codvalorVazio     gepr_pdiccionario.tipo$valor;
  var$codCulturaDefecto VARCHAR2(5);
  var$usuario           VARCHAR2(50);
BEGIN

  /* === Inicio: GenesisProducto === */

  /* Cultura patron */
  var$codCulturaDefecto := 'es';

  /* Usuario */
  var$usuario := 'GENESIS_PRODUCTO';

  /*msgCodigoRolObligatorio - Es obligatorio informar el código del Rol. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'É obrigatório informar o código da Rol.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'It is mandatory to inform the code of the Role.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Es obligatorio informar el código del Rol.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il est obligatoire de renseigner le code du rôle.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgCodigoRolObligatorio',
                                              var$codvalor,
                                              var$usuario);

  /*msgDescripcionRolObligatorio - Es obligatorio informar la descripción del Rol. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'É obrigatório informar a descrição da Rol.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'It is mandatory to inform the description of the role.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Es obligatorio informar el código del Rol.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il est obligatoire de renseigner la description du rôle.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgCodigoRolObligatorio',
                                              var$codvalor,
                                              var$usuario);
  
  /*msgRolExistente - Ya existe un rol con el código informado: {0}. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Já existe um rol com o código relatado: {0}.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'There is a role with the reported code: {0}.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Ya existe un rol con el código informado: {0}.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Un rôle avec le code signalé existe déjà: {0}.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgRolExistente',
                                              var$codvalor,
                                              var$usuario);
                                              
  /*msgRolInexistente - No existe un rol con el código informado: {0}. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Não há rol com o código relatado: {0}.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'There is no role with the reported code: {0}.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'No existe un rol con el código informado: {0}.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il n' || CHR(39) || 'y a pas de rôle avec le code signalé: {0}.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgRolInexistente',
                                              var$codvalor,
                                              var$usuario);

  /*msgExito - Registro actualizado con éxito. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Registro atualizado com sucesso.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Registration updated successfully.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Registro actualizado con éxito.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Inscription mise à jour avec succès.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgExito',
                                              var$codvalor,
                                              var$usuario);
  
  /*lblBusquedaDeRoles - Búsqueda de Roles */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Pesquisa de Roles';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Role Search';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Búsqueda de Roles';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Recherche de Rôle';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblBusquedaDeRoles',
                                              var$codvalor,
                                              var$usuario);

  /*lblMantenimientoRoles - Mantenimiento de Roles */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Manutenção de Roles';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Roles Maintenance';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Mantenimiento de Roles';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Maintenance des Rôles';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblMantenimientoRoles',
                                              var$codvalor,
                                              var$usuario);

  /*lblRoles - Roles */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Roles';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Roles';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Roles';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Rôles';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblRoles',
                                              var$codvalor,
                                              var$usuario);

  /*lblNombre - Nombre */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Nome';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Name';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Nombre';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Nom';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblNombre',
                                              var$codvalor,
                                              var$usuario);

  /*lblModulo - Módulo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Módulo';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Module';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Módulo';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Module';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblModulo',
                                              var$codvalor,
                                              var$usuario);

  /*lblVigente - Vigente */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Vigente';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Active';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Vigente';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Actif';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblVigente',
                                              var$codvalor,
                                              var$usuario);

  /*lblDescripcion - Descripcion */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Descrição';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Description';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Descripción';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Description';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblDescripcion',
                                              var$codvalor,
                                              var$usuario);

  /*lblPermisos - Permisos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Permissões';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Permissions';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Permisos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Autorisations';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblPermisos',
                                              var$codvalor,
                                              var$usuario);

  /*lblPermisosDisponibles - Permisos Disponibles */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Permissões Disponíveis';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Available Permissions';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Permisos Disponibles';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Autorisations Disponibles';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblPermisosDisponibles',
                                              var$codvalor,
                                              var$usuario);

  /*lblPermisosAsignados - Permisos Asignados */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Permissões Atribuídas';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Assigned Permissions';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Permisos Asignados';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Autorisations Attribuées';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'lblPermisosAsignados',
                                              var$codvalor,
                                              var$usuario);
                      
  /*msgDescripcionRolObligatorio - Es obligatorio informar la descripción del Rol. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'É obrigatório informar a descrição da Rol.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'It is mandatory to inform the description of the role.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Es obligatorio informar la descripción del Rol.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il est obligatoire de renseigner la description du rôle.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARROLES',
                                              'msgDescripcionRolObligatorio',
                                              var$codvalor,
                                              var$usuario);
  /*msgExito - Registro actualizado con éxito. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Registro atualizado com sucesso.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Registration updated successfully.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Registro actualizado con éxito.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Inscription mise à jour avec succès.';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgExito',
                                              var$codvalor,
                                              var$usuario);

  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Não há usuários para se inscrever';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'No users to sign up';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'No hay usuarios para dar de alta';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Pas d’utilisateurs à s’inscrire';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgDesLoginSinValores',
                                              var$codvalor,
                                              var$usuario);

 var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Não há usuários para cancelar a inscrição';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'No users to unsubscribe';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'No hay usuarios para dar de baja';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Pas d’utilisateurs à désabonner';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgDesUserSinValores',
                                              var$codvalor,
                                              var$usuario);

/*lblBusquedaDeUsuarios - Búsqueda de Usuarios */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Pesquisa do usuário';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'User Search';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Búsqueda de Usuarios';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Recherche d’utilisateurs';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblBusquedaDeUsuarios',
                                              var$codvalor,
                                              var$usuario);
/*lblTituloUsuarios - Usuarios */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Usuários';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Users';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Usuarios';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Utilisateurs';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblTituloUsuarios',
                                              var$codvalor,
                                              var$usuario);

/*lblTituloMantenimientoUsuarios - Mantenimiento de Usuario */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Manutenção do usuário';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'User Maintenance';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Mantenimiento de Usuario';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Maintenance des utilisateurs';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblTituloMantenimientoUsuarios',
                                              var$codvalor,
                                              var$usuario);


  /*lblNombre - Nombre */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Nome';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Name';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Nombre';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Nom et prénom';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblNombre',
                                              var$codvalor,
                                              var$usuario);
  /*lblNombreDesLogin - Usuario de Red */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Usuário de rede';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Network User';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Usuario de Red';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Utilisateur de réseau';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblNombreDesLogin',
                                              var$codvalor,
                                              var$usuario);
  /*lblApellido - Apellido */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Sobrenome';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Surname';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Apellido';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Nom de famille';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblApellido',
                                              var$codvalor,
                                              var$usuario);
  /*lblModulo - Módulo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Módulo';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Module';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Módulo';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Module';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblModulo',
                                              var$codvalor,
                                              var$usuario); 

  /*lblRolesDisponibles - Roles Disponibles */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Roles disponíveis';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Available Roles';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Roles Disponibles';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Rôles disponibles';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblRolesDisponibles',
                                              var$codvalor,
                                              var$usuario); 
  /*lblRolesUsuarios - Roles del Usuario */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Roles do usuário';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'User Roles';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Roles del Usuario';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Rôles de l’utilisateur';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblRolesUsuarios',
                                              var$codvalor,
                                              var$usuario);
  /*lblPais - País */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'País';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Country';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'País';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Pays';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblPais',
                                              var$codvalor,
                                              var$usuario);

 /*lblRole - Rol */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Rol';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Rol';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Rol';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Rol';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblRole',
                                              var$codvalor,
                                              var$usuario);

  /*en-EN - Inglés */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Inglês';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'English';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Inglés';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Anglais';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'en-EN',
                                              var$codvalor,
                                              var$usuario);

  /*es-ES - Español */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Espanhol';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Spanish';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Español';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Espanol';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'es-ES',
                                              var$codvalor,
                                              var$usuario);

  /*de-DE - Alemán */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Alemão';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'German';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Alemán';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Allemand';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'de-DE',
                                              var$codvalor,
                                              var$usuario);

  /*pt-PT - Portugués */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Português';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Portuguese';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Portugués';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Portugais';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'pt-PT',
                                              var$codvalor,
                                              var$usuario);

  /* msgEsObligatorioPais - Es obligatorio seleccionar un país */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'É obrigatório selecionar um país';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'It is mandatory to select a country';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Es obligatorio seleccionar un país';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il est obligatoire de sélectionner un pays';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgEsObligatorioPais',
                                              var$codvalor,
                                              var$usuario);

  /* lblActivo - Activo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Ativo';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Active';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Activo';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Actif';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblActivo',
                                              var$codvalor,
                                              var$usuario);

  /* lblAltaMasiva - Alta masiva */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Carga a granel';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Bulk load';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Alta masiva';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Charge en vrac';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'lblAltaMasiva',
                                              var$codvalor,
                                              var$usuario);

  /* msgCamposObligatorios - Debe completar los campos identificados como obligatorios. */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Você deve preencher os campos identificados como obrigatórios.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'You must complete the fields identified as mandatory.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Debe completar los campos identificados como obligatorios.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Vous devez remplir les champs identifiés comme obligatoires.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgCamposObligatorios',
                                              var$codvalor,
                                              var$usuario);
  /* msgEsObligatorioIdioma - Es obligatorio seleccionar un idioma */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'É obrigatório selecionar um idioma';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'It is mandatory to select a language';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Es obligatorio seleccionar un idioma';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Il est obligatoire de sélectionner une langue';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgEsObligatorioIdioma',
                                              var$codvalor,
                                              var$usuario);

  /* msgDesLoginExistente - El login {0} del usuario ya existe en la base de datos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'O login do usuário {0} já existe no banco de dados';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'The user' || chr(39) || 's login {0} already exists in the database';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'El login {0} del usuario ya existe en la base de datos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Le login de l' || chr(39) || 'utilisateur {0} existe déjà dans la base de données';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgDesLoginExistente',
                                              var$codvalor,
                                              var$usuario);

  /* msgNoTienePermisoParaQuitarRolePais - Usted no es administrador para el país {0}. No puede quitar ni agregar permisos para roles de dicho país */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Você não é um administrador do país {0}. Você não pode remover ou adicionar permissões para funções nesse país.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'You are not an administrator for country {0}. You cannot remove or add permissions for roles in that country.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Usted no es administrador para el país {0}. No puede quitar ni agregar permisos para roles de dicho país';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Vous n' || chr(39) || 'êtes pas administrateur du pays {0}. Vous ne pouvez pas supprimer ou ajouter des autorisations pour des rôles dans ce pays.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'CONFIGURARUSUARIOS',
                                              'msgNoTienePermisoParaQuitarRolePais',
                                              var$codvalor,
                                              var$usuario);


  /*lblPatronConfirmacion - Patrón Confirmación */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Padrão de confirmação';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Confirmation Pattern';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Patrón Confirmación';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Modèle de confirmation';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblPatronConfirmacion',
                                              var$codvalor,
                                              var$usuario);

/*lblFiltroAsociacionMovimientos - Filtros de asociación de movimientos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Filtros de associação de movimento';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Motion association filters';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Filtros de asociación de movimientos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Filtres d’association de mouvement';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblFiltroAsociacionMovimientos',
                                              var$codvalor,
                                              var$usuario);

/*lblDivisionPeriodos - División por periodos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Divisão por períodos';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Division by periods';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'División por periodos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Répartition par périodes';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblDivisionPeriodos',
                                              var$codvalor,
                                              var$usuario);

/*lblDivisionSubcanal - División de periodo por subcanal */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Divisão de período por subcanal';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Period division by subchannel';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'División de periodo por subcanal';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Répartition des périodes par sous-canal';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblDivisionSubcanal',
                                              var$codvalor,
                                              var$usuario);

/*lblDivisionDivisa - División de periodo por divisa */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Divisão de período por moeda';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Period division by currency';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'División de periodo por divisa';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Répartition des périodes par devise';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblDivisionDivisa',
                                              var$codvalor,
                                              var$usuario);

/*btnMensajes - Mensajes */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Mensagens';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Messages';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Mensajes';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Messages';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'btnMensajes',
                                              var$codvalor,
                                              var$usuario);
  
  /*lblCodMensaje - Código */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Código';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Code';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Código';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Code';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblCodMensaje',
                                              var$codvalor,
                                              var$usuario);

/*lblDesMensaje - Mensaje */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Mensagem';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Message';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Mensaje';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Message';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblDesMensaje',
                                              var$codvalor,
                                              var$usuario);

/*lblTipoMensaje - Tipo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Tipo';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Type';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Tipo';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Type';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblTipoMensaje',
                                              var$codvalor,
                                              var$usuario);

/*lblTitulo - Mensajes */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Mensagens';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Messages';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Mensajes';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Messages';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblTitulo',
                                              var$codvalor,
                                              var$usuario);

/*lblSinReintentos - Sin Reintentos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Sem novos julgamentos';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'No Retries';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Sin Reintentos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Pas de tentatives';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblSinReintentos',
                                              var$codvalor,
                                              var$usuario);

/*msgEliminaMensaje - ¿Esta seguro que desea eliminar el mensaje? */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Tem certeza que quer apagar a mensagem?';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Are you sure you want to delete the message?';
  var$codidioma (2) := 'es';
  var$codvalor (2) := '¿Esta seguro que desea eliminar el mensaje?';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Êtes-vous sûr de vouloir supprimer le message?';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'msgEliminaMensaje',
                                              var$codvalor,
                                              var$usuario);
/*msgCodigoExistente - Código de mensaje existente */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Código de mensagem existente';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Existing message code';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Código de mensaje existente';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Code de message existant';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'msgCodigoExistente',
                                              var$codvalor,
                                              var$usuario);

 /*lblAgrupValores - Agrupamento de valores em períodos (Cortes) */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Agrupamento de valores em períodos (Cortes)';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Grouping of Values ​​in Periods (Courts)';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Agrupamiento de Valores en los Períodos (Cortes)';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Regroupement des valeurs en périodes (tribunaux)';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblAgrupValores',
                                              var$codvalor,
                                              var$usuario);

  /*lblFaturacion - Recursos Propios */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Recursos próprios';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Own resources';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Recursos Propios';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Ressources propres';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblFacturacion',
                                              var$codvalor,
                                              var$usuario);

/*lblControlaFacturacionForm - Fecha Valor con Recursos Propios? */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Valor de data com recursos próprios?';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Date Value with Own Resources?';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Fecha Valor con Recursos Propios?';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Valeur de date avec ressources propres ?';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblControlaFacturacionForm',
                                              var$codvalor,
                                              var$usuario);

/*lblTipoPeriodo - Tipo de Periodo*/
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Tipo de Período';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Period Type';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Tipo de Período';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Type de Période';
  gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'MENSAJES_PLANIFICACION',
                                              'lblTipoPeriodo',
                                              var$codvalor,
                                              var$usuario);

  /*lblProcesos - Procesos */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Processos';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Processes';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Procesos';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Processus';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'lblProcesos',
                                              var$codvalor,
                                              var$usuario);

  /*MSG_INFO_DIVISION_POR_SUBCANAL - Al cambiar esta planificación por "División de periodo por subcanal" no será posible deseleccionar */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Ao alterar esta planejamento para "Divisão de período por subcanal" não será possível desmarcar.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'By changing this planning to "Period division by subchannel" it will not be possible to deselect.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Al cambiar esta planificación por "División de periodo por subcanal" no será posible deseleccionar.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'En changeant de planification en "Répartition de la période par sous-canal" il ne sera pas possible de désélectionner.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'MSG_INFO_DIVISION_POR_SUBCANAL',
                                              var$codvalor,
                                              var$usuario);


  /*MSG_INFO_DIVISION_POR_DIVISA - Al cambiar esta planificación por "División de periodo por divisa" no será posible deseleccionar */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Ao alterar esta planejamento para "Divisão de período por moeda" não será possível desmarcar.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'By changing this planning to "Period division by currency" it will not be possible to deselect.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Al cambiar esta planificación por "División de periodo por divisa" no será posible deseleccionar.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'En changeant de planification en "Répartition des périodes par devise" il ne sera pas possible de désélectionner.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'MSG_INFO_DIVISION_POR_DIVISA',
                                              var$codvalor,
                                              var$usuario);


  /*MSG_INFO_SELECCIONAR_DIVISA - Al cambiar esta planificación por "División de periodo por divisa" debe seleccionar al menos una divisa */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Ao alterar esta planejamento para "Divisão de período por moeda" você deve selecionar pelo menos uma moeda.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'By changing this planning to "Period division by currency" you must select at least one currency.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Al cambiar esta planificación por "División de periodo por divisa" debe seleccionar al menos una divisa.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'En changeant de planification en "Répartition des périodes par devise", vous devez sélectionner au moins une devise.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'MSG_INFO_SELECCIONAR_DIVISA',
                                              var$codvalor,
                                              var$usuario);
                     
/*MSG_INFO_CONFIGURACION_DIVISA_VACIO - Al buscar el parámetro "CodigoIsoDivisaDefecto" no existe */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'O valor definido no parâmetro "CodigoMoedaIsoPadrão" está vazio.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'The value set in the parameter "CodeIsoCurrencyDefect" is empty.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'El valor configurado en el parámetro "CodigoIsoDivisaDefecto" se encuentra vacío';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'La valeur définie dans le paramètre "CodeDeviseIsoParDéfaut" est vide.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                                  'MSG_INFO_CONFIGURACION_DIVISA_VACIO',
                                              var$codvalor,
                                              var$usuario);
    
/*MSG_INFO_CONFIGURACION_DIVISA_INVALIDO -Al buscar el parámetro "CodigoIsoDivisaDefecto" es inválido */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'O valor definido no parâmetro "CodigoMoedaIsoPadrão" é inválido ou não existe.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'The value set in the "CodeIsoCurrencyDefect" parameter is invalid or does not exist.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'El valor configurado en el parámetro "CodigoIsoDivisaDefecto" es invalido o no existe';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'La valeur définie dans le paramètre "CodeDeviseIsoParDéfaut" n’est pas valide ou n’existe pas.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_PLANIFICACION',
                                              'MSG_INFO_CONFIGURACION_DIVISA_INVALIDO',
                                              var$codvalor,
                                              var$usuario);    

    /* lblTitulo */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ordens de Serviço';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Service Orders';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Órdenes de Servicio';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Ordres de service';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblTitulo',
                                               var$codvalor,
                                               var$usuario);
    /* lblContrato */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Contrato';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Contract';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Contrato';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Contrat';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblContrato',
                                               var$codvalor,
                                               var$usuario);
    /* lblOS */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O.S.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'S.O.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'O.S.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'O.S.';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblOS',
                                               var$codvalor,
                                               var$usuario);
    /* lblProducto */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Produtos';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Product';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Producto';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Produit';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblProducto',
                                               var$codvalor,
                                               var$usuario);
  /* lblInicio */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Começar';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Start';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Inicio';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Début';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblInicio',
                                               var$codvalor,
                                               var$usuario);
  /* lblFin */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Fim';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'End';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Fin';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Finir';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblFin',
                                               var$codvalor,
                                               var$usuario);
  /* lblEstado */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Doença';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Status';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Estado';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'État';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblEstado',
                                               var$codvalor,
                                               var$usuario);
   /* ddl_seleccione */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Selecionar';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Select';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Seleccione';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Sélectionner';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'ddl_seleccione',
                                               var$codvalor,
                                               var$usuario);
        /* lblCalculado */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Calculado';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Calculated';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Calculado';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Calculé';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblCalculado',
                                               var$codvalor,
                                               var$usuario);
        /* lblNoCalculado */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não calculado';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Not Calculated';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No Calculado';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Non Calculé';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblNoCalculado',
                                               var$codvalor,
                                               var$usuario);
  /* lblColumnaFecha */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Data';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Date';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Fecha';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Date';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaFecha',
                                               var$codvalor,
                                               var$usuario);
    /* lblFechaReferencia */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Referência de data';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Date Reference';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Fecha Referencia';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Date Référence';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblFechaReferencia',
                                               var$codvalor,
                                               var$usuario);
    /* lblFechaCalculo */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Cálculo de data';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Date Calculation';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Fecha Cálculo';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Calcul des dates';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblFechaCalculo',
                                               var$codvalor,
                                               var$usuario);
    /* lblSubtituloDetalles */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Detalhes';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Details';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Detalles';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Des détails';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblSubtituloDetalles',
                                               var$codvalor,
                                               var$usuario);
      /* lblPunto */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ponto';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Point';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Punto';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Point';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblPunto',
                                               var$codvalor,
                                               var$usuario);
      /* lblColumnaTipo */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Tipo';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Type';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Tipo';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Type';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaTipo',
                                               var$codvalor,
                                               var$usuario);
      /* lblColumnaCantidad */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Resultar';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Amount';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Cantidad';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Montant';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaCantidad',
                                               var$codvalor,
                                               var$usuario);
      /* lblColumnaDivisa */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Moeda';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Currency';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Divisa';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Monnaie';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaDivisa',
                                               var$codvalor,
                                               var$usuario);
        /* lblColumnaTipoMercancia */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Tipo de mercadoria';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Merchandise Type';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Tipo Mercancia';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Type de marchandise';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaTipoMercancia',
                                               var$codvalor,
                                               var$usuario);
      /* lblColumnaTotal */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Total';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Total';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Total';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Total';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColumnaTotal',
                                               var$codvalor,
                                               var$usuario);
   /* Debe ingresar un filtro - ORDENES_SERVICIO */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Você deve inserir pelo menos um filtro.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'You must enter at least one filter.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Debe ingresar al menos un filtro.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vous devez entrer au moins un filtre.'; 
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'msgDebeIngresarAlMenosUnFiltro',
                                               var$codvalor,
                                               var$usuario);
   /* lblOrdenServicio */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Orden de Serviço';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Service Order';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Orden de Servicio';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Ordre de service';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblOrdenServicio',
                                               var$codvalor,
                                               var$usuario);
   /* lblSubtituloNotificaciones */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Notificações';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Notifications';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Notificaciones';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Avis';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblSubtituloNotificaciones',
                                               var$codvalor,
                                               var$usuario);
   /* lblColIdentificador */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Identificador';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Identifier';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Identificador';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Identifiant';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColIdentificador',
                                               var$codvalor,
                                               var$usuario);
   /* lblColIntentos */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Tentativas';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Attempts';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Intentos';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Tentatives';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColIntentos',
                                               var$codvalor,
                                               var$usuario);
   /* lblColUltimoError */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ultimo erro';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Last Error';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Último Error';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Dernière Erreur';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColUltimoError',
                                               var$codvalor,
                                               var$usuario);
    /* lblColNroDeIntento */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Número da tentativa';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Attempt Number';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Número de Intento';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Numéro de tentative';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColNroDeIntento',
                                               var$codvalor,
                                               var$usuario);
    /* lblColObservaciones */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Observações';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Observations';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Observaciones';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Observations';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColObservaciones',
                                               var$codvalor,
                                               var$usuario);
   /* lblColError */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Erro';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Error';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Error';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Erreur';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblColError',
                                               var$codvalor,
                                               var$usuario);

   /* lblRecalcular */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Recalcular';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Recalculate';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Recalcular';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Recalculer';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblRecalcular',
                                               var$codvalor,
                                               var$usuario);

   /* lblNotificar */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Notificar';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Notify';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Notificar';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Notifier';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblNotificar',
                                               var$codvalor,
                                               var$usuario);
   /* lblVisualizar */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Visualizar';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Visualize';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Visualizar';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Visualiser';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblVisualizar',
                                               var$codvalor,
                                               var$usuario);

   /* lblNotificacion */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Notificação';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Notification';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Notificación';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Notification';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ORDENES_SERVICIO',
                                               'lblNotificacion',
                                               var$codvalor,
                                               var$usuario);

  /* lblDividirPorMovimiento - Dividir periodo por movimiento */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Dividir período por movimento';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Divide period by movement';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Dividir periodo por movimiento';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Diviser la période par mouvement';
   gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERICO',
                                               'lblDividirPorMovimiento',
                                               var$codvalor,
                                               var$usuario);


  /*MSG_INFO_FYH_INICIO_VIGENCIA - La fecha y hora de inicio informada no podrá ser menor que la fecha y hora actual */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'A data e hora de início informadas não podem ser inferiores à data e hora atuais';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'The reported start date and time cannot be less than the current date and time';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'La fecha y hora de inicio informada no podrá ser menor que la fecha y hora actual';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'La date et l´heure de début signalées ne peuvent pas être inférieures à la date et à l´heure actuelles';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_MAE',
                                              'MSG_INFO_FYH_INICIO_VIGENCIA',
                                              var$codvalor,
                                              var$usuario);

  /*MSG_INFO_FYH_INICIO_VIGENCIA_2 - La fecha y hora de inicio informada no puede ser modificada */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'A data e hora de início informadas não podem ser modificadas.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'The reported start date and time cannot be modified.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'La fecha y hora de inicio informada no puede ser modificada';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'La date et l´heure de début renseignées ne peuvent pas être modifiées.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_MAE',
                                              'MSG_INFO_FYH_INICIO_VIGENCIA_2',
                                              var$codvalor,
                                              var$usuario);

  /*MSG_DELEGACION_SIN_CODIGO_AJENO_MAE - Delegación sin código ajeno MAE */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Delegação sem código externo MAE.';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Delegation without external code MAE.';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Delegación sin código ajeno MAE.';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Délégation sans code externe MAE.';
  gepr_pdiccionario.sguardar_diccionario_arr ('IAC',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'ABM_MAE',
                                              'MSG_DELEGACION_SIN_CODIGO_AJENO_MAE',
                                              var$codvalor,
                                              var$usuario);
  
  /* MSG_SUBCANAL_OBRIGATORIO */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'É necessário informar pelo ao menos um sub-canal para cada canal.';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'It is necessary to report at least one sub-channel for each channel.';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Es necesario informar por lo menos un subcanal por cada canal.';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Il est nécessaire de signaler au moins un sous-canal pour chaque canal.';
  gepr_pdiccionario.sguardar_diccionario_arr('IAC',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'ABM_PLANIFICACION',
                                             'MSG_SUBCANAL_OBRIGATORIO',
                                             var$codvalor,
                                             var$usuario);

  /* titulo_tipplanificacion */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Tipo de planejamento';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Planning Type';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Tipo Planificación';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Type de Planification';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_tipplanificacion',
                                             var$codvalor,
                                             var$usuario);
											 
  /* titulo_planificacion */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Planejamento';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Planning';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Planificación';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Planification';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_planificacion',
                                             var$codvalor,
                                             var$usuario);
											 
  /* titulo_banco */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Banco';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Bank';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Banco';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Banque';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_banco',
                                             var$codvalor,
                                             var$usuario);
											 
  /* titulo_device */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'DeviceID';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'DeviceID';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'DeviceID';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'DeviceID';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_device',
                                             var$codvalor,
                                             var$usuario);
											 
  /* titulo_cliente */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Cliente';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Client';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Cliente';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Client';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_cliente',
                                             var$codvalor,
                                             var$usuario);		
											 
  /* titulo_fecmovimiento */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Movimento de data e hora';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Date and Time Movement';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Fecha y Hora Movimiento';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Mouvement date et de l''heure';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_fecmovimiento',
                                             var$codvalor,
                                             var$usuario);											 
  /* titulo_tipmovimiento */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Tipo Movimiento';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Movement Type';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Tipo Movimiento';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Type Mouvement';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_tipmovimiento',
                                             var$codvalor,
                                             var$usuario);	
  /* titulo_codmovimiento */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Código Movimiento';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Movement Code';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Código Movimiento';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Code Mouvement';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_codmovimiento',
                                             var$codvalor,
                                             var$usuario);	
  /* titulo_correo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'OS SEGUINTES MOVIMENTOS NÃO FORAM ACREDITADOS';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'THE FOLLOWING MOVEMENTS WERE NOT ACCREDITED';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'LOS SIGUIENTES MOVIMIENTOS NO FUERON ACREDITADOS';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'LES MOUVEMENTS SUIVANTS N''ONT PAS ÉTÉ ACCRÉDITÉS';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_correo',
                                             var$codvalor,
                                             var$usuario);	
											 
  /* titulo_fvo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Lista de Transações com erros ONLINE VALUE DATE';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'List of Transactions with errors ONLINE VALUE DATE';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Lista de Transacciones con errores FECHA VALOR ONLINE';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Liste des Transactions avec des erreurs DATE DE VALEUR EN LIGNE';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_fvo',
                                             var$codvalor,
                                             var$usuario);	

  /* titulo_fvc */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Lista de Transações com erros DATA VALOR LOTE e DATA VALOR COM CONFIRMAÇÃO';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'List of Transactions with errors DATE VALUE BATCH and DATE VALUE WITH CONFIRMATION';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Lista de Transacciones con errores FECHA VALOR BATCH y FECHA VALOR CON CONFIRMACIÓN';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Liste des Transactions avec erreurs DATE VALUE BATCH et DATE VALUE WITH CONFIRMATION';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_fvc',
                                             var$codvalor,
                                             var$usuario);	

  /* titulo_codptoservicio */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Código Pto. de Serviço';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Service Point Code';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Código Pto. Servicio';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Code Point de Service';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_codptoservicio',
                                             var$codvalor,
                                             var$usuario);
  /* titulo_desptoservicio */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Descrição do ponto de serviço';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Service Point Description';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Descripción Pto. Servicio';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Description du point de service';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_desptoservicio',
                                             var$codvalor,
                                             var$usuario);

  /* titulo_asunto */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'NOTIFICAÇÃO DE MOVIMENTOS NÃO ACREDITADOS';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'NOTIFICATION OF UNACCREDITED MOVEMENTS';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'NOTIFICACIÓN DE MOVIMIENTOS NO ACREDITADOS';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'NOTIFICATION DES MOUVEMENTS NON ACCRÉDITÉS';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_asunto',
                                             var$codvalor,
                                             var$usuario);
  
  /* titulo_leyendanota */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Avalie cada caso e, se necessário, gerencie o retrabalho manual. Considere a seguinte legenda.';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Evaluate each case and, if necessary, manage manual rework. Consider the following legend.';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Evaluar cada caso y de ser necesario, gestionar el reproceso manual. Considerar la siguiente leyenda.';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Évaluer chaque cas et, si nécessaire, gérer les reprises manuelles. Considérez la légende suivante.';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_leyendanota',
                                             var$codvalor,
                                             var$usuario);

  /* titulo_leyendafvo_alto */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'O limite de tentativas automáticas foi excedido ou foi rejeitado pelo Banco.';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'The limit of automatic retries has been exceeded or it has been rejected by the Bank.';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Se superó el limite de reintentos automaticos o ha sido rechazada por el Banco.';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'La limite de tentatives automatiques a été dépassée ou elle a été rejetée par la Banque.';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_leyendafvo_alto',
                                             var$codvalor,
                                             var$usuario);
   /* titulo_leyendafvc */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Não estava relacionado ao período.';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'It was not related to period.';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'No se relacionó a período. ';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Ce n''était pas lié à la période.';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_leyendafvc',
                                             var$codvalor,
                                             var$usuario);
  /* titulo_leyendafvo_bajo */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Em breve excederá o limite de tentativas automáticas ou possivelmente foi rejeitado pelo Banco.';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'It is soon to exceed the limit of automatic retries or it has possibly been rejected by the Bank.';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Está pronto a superar el limite de reintentos automaticos o posiblemente ha sido rechazada por el Banco.';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'Il va bientôt dépasser la limite des tentatives automatiques ou il a peut-être été rejeté par la Banque.';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_leyendafvo_bajo',
                                             var$codvalor,
                                             var$usuario);
  /* titulo_estado */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma(0) := 'pt';
  var$codvalor(0) := 'Estado';
  var$codidioma(1) := 'en';
  var$codvalor(1) := 'Status';
  var$codidioma(2) := 'es';
  var$codvalor(2) := 'Estado';
  var$codidioma(3) := 'fr';
  var$codvalor(3) := 'État';
  gepr_pdiccionario.sguardar_diccionario_arr('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                             'titulo_estado',
                                             var$codvalor,
                                             var$usuario);

  /*gvCantidad - Suma de Cantidad SAPR_TMOVIMIENTO_DETALLE.NEL_CANTIDAD */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'Quantidade';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Quantity';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'Cantidad';
  var$codidioma (3) := 'fr';
  var$codvalor (3) := 'Montant';
  gepr_pdiccionario.sguardar_diccionario_arr ('GenesisSaldos',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'SALDOS_CONSULTAR_TRANSACCIONES',
                                              'gvCantidad',
                                              var$codvalor,
                                              var$usuario);

  /*gvBaseDeviceId - DeviceID base de la MAE */
  var$codidioma := var$codidiomaVazio;
  var$codvalor := var$codvalorVazio;
  var$codidioma (0) := 'pt';
  var$codvalor (0) := 'DeviceID Base';
  var$codidioma (1) := 'en';
  var$codvalor (1) := 'Base DeviceID';
  var$codidioma (2) := 'es';
  var$codvalor (2) := 'DeviceID Base';
  gepr_pdiccionario.sguardar_diccionario_arr ('GenesisSaldos',
                                              var$codidioma,
                                              var$codCulturaDefecto,
                                              'SALDOS_CONSULTAR_TRANSACCIONES',
                                              'gvBaseDeviceId',
                                              var$codvalor,
                                              var$usuario);

  COMMIT;


EXCEPTION
  WHEN OTHERS THEN
    raise_application_error(-20001,
                            'Arquivo: Genesis.sql Script: X_DICCIONARIO' ||
                            sqlerrm);
    RAISE;
END;
/