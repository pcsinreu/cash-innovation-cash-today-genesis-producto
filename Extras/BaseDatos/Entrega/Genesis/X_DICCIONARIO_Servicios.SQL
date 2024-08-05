/* Formatted on 5/5/2020 16:13:53 (QP5 v5.252.13127.32867) */
DECLARE
   var$codidioma           gepr_pdiccionario.tipo$codIdioma;
   var$codidiomaVazio      gepr_pdiccionario.tipo$codIdioma;
   var$codvalor            gepr_pdiccionario.tipo$valor;
   var$codvalorVazio       gepr_pdiccionario.tipo$valor;
   var$codCulturaDefecto   VARCHAR2 (5);
   var$usuario             VARCHAR2 (50);
BEGIN
  /* 0040320000 - Exito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '0040320000',
                                               var$codvalor,
                                               var$usuario);

  


   /* 2040320000 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320000',
                                               var$codvalor,
                                               var$usuario);
    
   /* 2040320001 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320001',
                                               var$codvalor,
                                               var$usuario);

   /* 2040320002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320002',
                                               var$codvalor,
                                               var$usuario);

  /* 3040320000 -Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
  var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) :=
      'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) :=
      'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) :=
      'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '3040320000',
                                               var$codvalor,
                                               var$usuario);

  /* 3040320001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '3040320001',
                                               var$codvalor,
                                               var$usuario);

   /*2040320003 - El código de país {0} ingresado no existe */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do país {0} inserido não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The country code {0} entered does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de país {0} ingresado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Le code de pays {0} entré n’existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320003',
                                               var$codvalor,
                                               var$usuario);

  /*2040320004 - El código de acción [{0}] ingresado no es válido */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código de ação {0} inserido não é válido.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The action code {0} entered is not valid.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de acción {0} ingresado no es válido.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Le code d’action {0} saisi n’est pas valide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320004',
                                               var$codvalor,
                                               var$usuario);

   /*2040320005 - Más de un punto de servicio encontrado */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Mais de um ponto de serviço encontrado.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'More than one service point found.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Más de un punto de servicio encontrado.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Plus d’un point de service trouvé.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320005',
                                               var$codvalor,
                                               var$usuario);
   
    /*2040320006 - No se encontró el cliente/subcliente/punto */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não foi encontrado o cliente/subcient/punto.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The client/subclient/point was not found.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No se encontró el cliente/subcliente/punto.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Client/sous-client/point introuvable.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320006',
                                               var$codvalor,
                                               var$usuario);
   
   /*2040320007 - No se encontraron registros con los parámetros ingresados */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não foram encontrados registros com os parâmetros inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'No records were found with the parameters entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No se encontraron registros con los parámetros ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Aucun enregistrement n’a été trouvé avec les paramètres saisis.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARACUERDOSSERVICIO',
                                               '2040320007',
                                               var$codvalor,
                                               var$usuario);
   /* 0040330000 - Exito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '0040330000',
                                               var$codvalor,
                                               var$usuario);

  


   /* 2040330000 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330000',
                                               var$codvalor,
                                               var$usuario);
    
   /* 2040330001 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330001',
                                               var$codvalor,
                                               var$usuario);

   /* 2040330002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330002',
                                               var$codvalor,
                                               var$usuario);

      /* 2040330003 - No informó el código de país. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não informou o código do país.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'It did not report the country code.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No informó el código de país.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Il n’a pas signalé le code de pays.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330003',
                                               var$codvalor,
                                               var$usuario);


   /* 2040330004 - El código de país es inexistente. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do país é inexistente.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The country code is non-existent.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de país es inexistente.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code de pays est inexistant.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330004',
                                               var$codvalor,
                                               var$usuario);


    /* 2040330005 - El source reference id informado no existe. */
     var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O id de referência da fonte relatada não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The reported source reference id does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El source reference id informado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le source reference id informé n’existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330005',
                                               var$codvalor,
                                               var$usuario);

    /* 2040330006 - Debe informar o el campo fecha o el campo source reference id. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Você deve informar o campo de data ou o campo de referência de referência da fonte.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'You must report either the date field or the source reference id field.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Debe informar o el campo fecha o el campo source reference id.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vous devez indiquer soit le champ date, soit le champ source reference id.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330006',
                                               var$codvalor,
                                               var$usuario);

   /* 2040330007 - Con los filtros informados no se encontraron registros de saldos acuerdo. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Com os filtros relatados, não foram encontrados registros de saldos de concordância.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'With the reported filters, no records of agreement balances were found.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Con los filtros informados no se encontraron registros de saldos acuerdo.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Avec les filtres signalés, aucun enregistrement de soldes d'|| chr(39) || ' accords n' || chr(39) || 'a été trouvé.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '2040330007',
                                               var$codvalor,
                                               var$usuario);

  /* 3040330000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) :=
      'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) :=
      'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) :=
      'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '3040330000',
                                               var$codvalor,
                                               var$usuario);

  /* 3040330001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARSALDOSACUERDO',
                                               '3040330001',
                                               var$codvalor,
                                               var$usuario);

   /* 2000110010 - Debe informar al menos un importe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Você deve informar pelo menos um valor.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'You must report at least one amount.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Debe informar al menos un importe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vous devez déclarer au moins un montant.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis', var$codidioma, var$codCulturaDefecto, 'ALTAMOVIMIENTOSPROVISIONEFECTIVO', '2000110010', var$codvalor, var$usuario);


   /* 0040300000 - Éxito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Exito.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '0040300000',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300000 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) :=
      'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données saisies.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300000',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300001 - Petición vacía o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Pedido vazio ou inválido.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid request.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacía o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Demande vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300001',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300002 - El valor del Token de seguridad es invalido: "[{0}]". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do token de segurança é inválido: "[{0}]".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The value of the Security Token is invalid: "[{0}]".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "[{0}]".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'La valeur du jeton de sécurité nest pas valide : "[{0}]".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300002',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300003 - El código de planificación [{0}] ingresado no existe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código de planejamento [{0}] inserido não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The planning code [{0}] entered does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de planificación [{0}] ingresado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code de planification [{0}] saisi n’existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300003',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300004 - El código de máquina [{0}] ingresado no existe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código da máquina [{0}] inserido não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The machine code [{0}] entered does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de máquina [{0}] ingresado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code machine [{0}] saisi n’existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300004',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300005 - El código de país [{0}] ingresado no existe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código de país [{0}] inserido não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The country code [{0}] entered does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de país [{0}] ingresado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code de pays [{0}] saisi n’existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300005',
                                               var$codvalor,
                                               var$usuario);
   
   /* 2040300006 - Es necesario informar una fecha. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Uma data precisa ser informada.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'A date needs to be reported.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Es necesario informar una fecha.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une date doit être rapportée.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300006',
                                               var$codvalor,
                                               var$usuario);

   /* 3040300000 - contecio un error en la aplicacion. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu um erro no aplicativo. Verifique com o administrador do sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'An error occurred in the application. Check with your system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une erreur sest produite dans lapplication. Vérifiez auprès de votre administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '3040300000',
                                               var$codvalor,
                                               var$usuario);

   /* 2040300007 - Es obligatorio informar al menos una MAE. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'É obrigatório relatar pelo menos um MAE.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'It is mandatory to report at least one MAE.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Es obligatorio informar al menos una MAE.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Il est obligatoire de déclarer au moins un MAE.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               '2040300007',
                                               var$codvalor,
                                               var$usuario);
   /* MSG_RES_0 - No planificada. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não planejado';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Not planned';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No planificada';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pas prévu';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               'MSG_RES_0',
                                               var$codvalor,
                                               var$usuario);
   /* MSG_RES_1 - Planificada. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Planejado';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Planned';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Planificada';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Prévu';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               'MSG_RES_1',
                                               var$codvalor,
                                               var$usuario);
   /* MSG_RES_2 - Inexistente. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Inexistente';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Non-existent';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Inexistente';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Inexistant';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECUPERARMAESPLANIFICADAS',
                                               'MSG_RES_2',
                                               var$codvalor,
                                               var$usuario);

   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O identificador {0} e o código externo já {1} estão configurados para a {2} de código {3} e {4} de descrição.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The {0} identifier and external code is {1} already configured for the {3} code {2} and description {4}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El identificador {0} y código externo {1} ya está configurado para el {2} de código {3} y descripción {4}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'L’identificateur {0} et de code externe {1} est déjà configuré pour le {2} de code {3} et de description {4}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIGURARCLIENTES',
                                               '2040080013',
                                               var$codvalor,
                                               var$usuario);
   /* 0040340000 - Éxito. */
var$codidioma := var$codidiomaVazio;
var$codvalor := var$codvalorVazio;
var$codidioma (0) := 'pt';
var$codvalor (0) := 'Sucesso.';
var$codidioma (1) := 'en';
var$codvalor (1) := 'Success.';
var$codidioma (2) := 'es';
var$codvalor (2) := 'Éxito.';
var$codidioma (3) := 'fr';
var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIRMARPERIODOS',
                                               '0040340000',
                                               var$codvalor,
                                               var$usuario);

/* 2040340000 - Verifique los datos ingresados. */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'Verifique os dados inseridos.';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'Check the data entered.';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'Verifique los datos ingresados.';
 var$codidioma (3) := 'fr';
 var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIRMARPERIODOS',
                                               '2040340000',
                                               var$codvalor,
                                               var$usuario);

  /* 2040340001 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIRMARPERIODOS',
                                               '2040340001',
                                               var$codvalor,
                                               var$usuario);

/* 2040340002 - El valor del Token de seguridad es invalido: "{0}". */
var$codidioma := var$codidiomaVazio;
var$codvalor := var$codvalorVazio;
var$codidioma (0) := 'pt';
var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
var$codidioma (1) := 'en';
var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
var$codidioma (2) := 'es';
var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
var$codidioma (3) := 'fr';
var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                            var$codidioma,
                                            var$codCulturaDefecto,
                                            'CONFIRMARPERIODOS',
                                            '2040340002',
                                            var$codvalor,
                                            var$usuario);

 /*2040340003 - No se informó el código del país. */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'O código do país não foi informado.';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'The country code was not reported.';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'No se informó el código del país.';
 var$codidioma (3) := 'fr';
 var$codvalor (3) :=
    'Le code du pays n’a pas été déclaré.';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340003',
                                             var$codvalor,
                                             var$usuario);

 /*2040340004 - El código de país {0} ingresado no existe */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'O código do país {0} inserido não existe.';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'The country code {0} entered does not exist.';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'El código de país {0} ingresado no existe.';
 var$codidioma (3) := 'fr';
 var$codvalor (3) :=
    'Le code de pays {0} entré n’existe pas.';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340004',
                                             var$codvalor,
                                             var$usuario);

/*2040340005 - No fue posible encontrar un período con el DeviceID "{0}" y el Identificador "{1}" que esté pendiente de confirmación ("CO"). */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'Não foi possível encontrar um período com o DeviceID "{0}" e o identificador "{1}" que está pendente de confirmação ("CO").';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'It was not possible to find a period with the DeviceID "{0}" and the Identifier "{1}" that is pending confirmation ("CO").';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'No fue posible encontrar un período con el DeviceID "{0}" y el Identificador "{1}" que esté pendiente de confirmación ("CO").';
 var$codidioma (3) := 'fr';
 var$codvalor (3) :=
    'Il n’a pas été possible de trouver une période avec l’ID de périphérique «{0}» et l’identifiant «{1}» en attente de confirmation («CO»).';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340005',
                                             var$codvalor,
                                             var$usuario);

/*2040340006 - El período del DeviceID "{0}" e Identificador "{1}" ya fue confirmado en "{2}" (código confirmación "{3}"). */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'O período com o DeviceID "{0}" e o identificador "{1}" já foi confirmado em "{2}" (código de confirmação "{3}").';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'The period with the DeviceID "{0}" and the Identifier "{1}" was already confirmed in "{2}" (confirmation code "{3}").';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'El período del DeviceID "{0}" e Identificador "{1}" ya fue confirmado en "{2}" (código confirmación "{3}").';
 var$codidioma (3) := 'fr';
 var$codvalor (3) := 'La période avec le DeviceID «{0}» et l´identifiant «{1}» a déjà été confirmée dans «{2}» (code de confirmation «{3}»).';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340006',
                                             var$codvalor,
                                             var$usuario);

/*2040340007 - Ocurrió un error al confirmar el período con el DeviceID "{0}" y el Identificador "{1}": {2} */
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'Ocorreu um erro ao confirmar o período com DeviceID "{0}" e Identificador "{1}": {2}';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'An error occurred while confirming the period with DeviceID "{0}" and Identifier "{1}": {2}';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'Ocurrió un error al confirmar el período con el DeviceID "{0}" y el Identificador "{1}": {2}';
 var$codidioma (3) := 'fr';
 var$codvalor (3) := 'Une erreur s´est produite lors de la confirmation de la période avec DeviceID «{0}» et l´identifiant «{1}» : {2}';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340007',
                                             var$codvalor,
                                             var$usuario);

 /*2040340008 - El período de  Bóveda de DeviceID "{0}" e Identificador "{1}"  posee períodos de recojo relacionados que no se encuentran confirmados.*/
 var$codidioma := var$codidiomaVazio;
 var$codvalor := var$codvalorVazio;
 var$codidioma (0) := 'pt';
 var$codvalor (0) := 'O período do Vault de DeviceID "{0}" e Identifier "{1}" tem períodos de coleta relacionados que não foram confirmados.';
 var$codidioma (1) := 'en';
 var$codvalor (1) := 'The Vault period of DeviceID "{0}" and Identifier "{1}" has related pick up periods that are not confirmed.';
 var$codidioma (2) := 'es';
 var$codvalor (2) := 'El período de Bóveda de DeviceID "{0}" e Identificador "{1}"  posee períodos de recojo relacionados que no se encuentran confirmados.';
 var$codidioma (3) := 'fr';
 var$codvalor (3) := 'La période Vault de DeviceID "{0}" et d´identifiant "{1}" a des périodes de collecte associées qui ne sont pas confirmées.';
 gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                             var$codidioma,
                                             var$codCulturaDefecto,
                                             'CONFIRMARPERIODOS',
                                             '2040340008',
                                             var$codvalor,
                                             var$usuario);

/* 3040340000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) :=
      'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) :=
      'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) :=
      'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIRMARPERIODOS',
                                               '3040340000',
                                               var$codvalor,
                                               var$usuario);

/* 3040340001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'CONFIRMARPERIODOS',
                                               '3040340001',
                                               var$codvalor,
                                               var$usuario);

/* 2040100012 - No fue posible relacionar el movimiento a un per�odo, la planificaci�n no tiene un per�odo abierto. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Tipo de período incorreto.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Incorrect period type.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Tipo de período incorrecto.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Type de période incorrect.';
   gepr_pdiccionario.sguardar_diccionario_arr (
      'Genesis',
      var$codidioma,
      var$codCulturaDefecto,
      'RELACIONARMOVIMIENTOSPERIODOS',
      '2040100012',
      var$codvalor,
      var$usuario);

    /* 0060050000 - Éxito. */
var$codidioma := var$codidiomaVazio;
var$codvalor := var$codvalorVazio;
var$codidioma (0) := 'pt';
var$codvalor (0) := 'Sucesso.';
var$codidioma (1) := 'en';
var$codvalor (1) := 'Success.';
var$codidioma (2) := 'es';
var$codvalor (2) := 'Éxito.';
var$codidioma (3) := 'fr';
var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ACTUALIZARPERIODOS',
                                               '0060050000',
                                               var$codvalor,
                                               var$usuario);
 /* 2060050000 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ACTUALIZARPERIODOS',
                                               '2060050000',
                                               var$codvalor,
                                               var$usuario);

 /* 2060050001 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ACTUALIZARPERIODOS',
                                               '2060050001',
                                               var$codvalor,
                                               var$usuario);

 /* 2060050002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ACTUALIZARPERIODOS',
                                               '2060050002',
                                               var$codvalor,
                                               var$usuario);

 /* 3060050000 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'ACTUALIZARPERIODOS',
                                               '3060050000',
                                               var$codvalor,
                                               var$usuario);

   /* 0040350000 - Éxito. */
var$codidioma := var$codidiomaVazio;
var$codvalor := var$codvalorVazio;
var$codidioma (0) := 'pt';
var$codvalor (0) := 'Sucesso.';
var$codidioma (1) := 'en';
var$codvalor (1) := 'Success.';
var$codidioma (2) := 'es';
var$codvalor (2) := 'Éxito.';
var$codidioma (3) := 'fr';
var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '0040350000',
                                               var$codvalor,
                                               var$usuario);
 /* 2040350001 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350001',
                                               var$codvalor,
                                               var$usuario);

 /* 2040350000 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350000',
                                               var$codvalor,
                                               var$usuario);

 /* 2040350002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350002',
                                               var$codvalor,
                                               var$usuario);
--

 /* 2040350003 - No se informó el código del país. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do país não foi informado.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The country code was not reported.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No se informó el código del país.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code du pays n’a pas été déclaré.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350003',
                                               var$codvalor,
                                               var$usuario);

  /* 2040350004 - El código del país es inexistente. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do país é inexistente.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The country code is non-existent.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código del país es inexistente.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code pays est inexistant.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350004',
                                               var$codvalor,
                                               var$usuario);
     /* 2040350005 - No fue posible encontrar un período con el DeviceID "{0}", el Identificador "{1}" que esté pendiente de reconfirmación "{(No Acreditado "NA"), (No Confirmado "NC")}" y tenga el tipo de periodo "{Acreditación, Shipout, Ingreso a Bóveda}" */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Não foi possível encontrar um período com o DeviceID "{0}", o identificador "{1}" que está pendente de reconfirmação "{(Não creditado "NA"), (Não Confirmado "NC")}" e tem o tipo de período "{Credenciamento, Shipout, Vault Entry}""{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'It was not possible to find a period with the DeviceID "{0}", the Identifier "{1}" that is pending reconfirmation "{(Not Credited "NA"), (Not Confirmed "NC")}" and has the type of period "{Accreditation, Shipout, Vault Entry}""{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'No fue posible encontrar un período con el DeviceID "{0}", el Identificador "{1}" que esté pendiente de reconfirmación "{(No Acreditado "NA"), (No Confirmado "NC")}" y tenga el tipo de periodo "{Acreditación, Shipout, Ingreso a Bóveda}""{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Il n’a pas été possible de trouver une période avec le DeviceID « {0}», l’identifiant « {1}» qui est en attente de reconfirmation « {(Non crédité « NA»), (Non confirmé « NC»)}» et a le type de période « {Accréditation, Shipout, Vault Entry}""{0}».';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350005',
                                               var$codvalor,
                                               var$usuario);
     /* 2040350006 - El período del DeviceID "{0}" y Identificador "{1}" con el tipo de periodo "{Acreditación, Shipout, Ingreso a Bóveda}" ya fue acreditado "{2}" (Fecha que fue informada y que se grabó en el dicho período)
(código confirmación "{3}"). */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O período do DeviceID "{0}" e do identificador "{1}" com o tipo de período "{2}" já foi creditado como "{3}" (código de confirmação "{4}").';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The period of the DeviceID "{0}" and Identifier "{1}" with the type of period "{2}" was already credited "{3}" (confirmation code "{4}").';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El período del DeviceID "{0}" y Identificador "{1}" con el tipo de periodo "{2}" ya fue acreditado "{3}" (código confirmación "{4}").';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'La période de l’ID de périphérique « {0}» et de l’identifiant « {1}» avec le type de période « {2}» était déjà créditée « {3}» (code de confirmation « {4} »).';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '2040350006',
                                               var$codvalor,
                                               var$usuario);
     /* 3040350000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu um erro na aplicação. Verifique com o administrador do sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'An error occurred in the application. Check with your system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une erreur s’est produite dans l’application. Vérifiez auprès de votre administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '3040350000',
                                               var$codvalor,
                                               var$usuario);
    

 /* 3040350001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) :=
      'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'RECONFIRMARPERIODOS',
                                               '3040350001',
                                               var$codvalor,
                                               var$usuario);


 
   /* 0060070000 - Exito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '0060070000',
                                               var$codvalor,
                                               var$usuario);

   /* 2060070000 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '2060070000',
                                               var$codvalor,
                                               var$usuario);

   /* 2060070001 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '2060070001',
                                               var$codvalor,
                                               var$usuario);
    


   /* 2060070002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '2060070002',
                                               var$codvalor,
                                               var$usuario);

   /* 3060070000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '3060070000',
                                               var$codvalor,
                                               var$usuario);

   /* 3060070001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'DEPURACION',
                                               '3060070001',
                                               var$codvalor,
                                               var$usuario);
                                               
   /* 2060000001 - Debe informar el código de país. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Você deve informar o código do país.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'You must inform the country code.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Debe informar el código de país.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vous devez renseigner le code du pays.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERICO',
                                               '2060000001',
                                               var$codvalor,
                                               var$usuario);

   /* 2060000002 - El código de país informado no existe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do país relatado não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The reported country code does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de país informado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code de pays indiqué n' || CHR(39) || 'existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERICO',
                                               '2060000002',
                                               var$codvalor,
                                               var$usuario);
   /* 0060070000 - Exito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '0060080000',
                                               var$codvalor,
                                               var$usuario);

   /* 2060070000 - Petición vacia o inválida. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '2060080000',
                                               var$codvalor,
                                               var$usuario);

   /* 2060070001 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '2060080001',
                                               var$codvalor,
                                               var$usuario);
    


   /* 2060070002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '2060080002',
                                               var$codvalor,
                                               var$usuario);

   /* 3060070000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '3060080000',
                                               var$codvalor,
                                               var$usuario);

   /* 3060070001 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'NOTIFICARMOVIMIENTOSNOACREDITADOS',
                                               '3060080001',
                                               var$codvalor,
                                               var$usuario);
   

   /* 0060060000 - Exito. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Sucesso.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Success.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Éxito.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le succès.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '0060060000',
                                               var$codvalor,
                                               var$usuario);
   /* 2060060000 - Petición vacía o inválida.. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Petição vazia ou inválida.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Empty or invalid petition.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Petición vacia o inválida.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Pétition vide ou invalide.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '2060060000',
                                               var$codvalor,
                                               var$usuario);

   /* 2060060001 - Verifique los datos ingresados. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Verifique os dados inseridos.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'Check the data entered.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Verifique los datos ingresados.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Vérifiez les données entrées.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '2060060001',
                                               var$codvalor,
                                               var$usuario);

   /* 2060060002 - El valor del Token de seguridad es invalido: "{0}". */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O valor do Token de segurança esta inválido: "{0}".';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El valor del Token de seguridad es invalido: "{0}".';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'El valor del Token de seguridad es invalido: "{0}".';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '2060060002',
                                               var$codvalor,
                                               var$usuario);


   /* 3060060000 - Aconteció un error en la aplicación. Consulte con el administrador del sistema. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Acontecio un error en la aplicacion. Consulte con el administrador del sistema.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'An error occurred in the application. Check with the system administrator.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Aconteció un error en la aplicación. Consulte con el administrador del sistema.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Une erreur s´est produite dans l´application. Vérifiez auprès de l´administrateur système.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '3060060000',
                                               var$codvalor,
                                               var$usuario);


   /* 3060060000 - Se produjo el siguiente error en la aplicación: {0} */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'Ocorreu o seguinte erro na aplicação: {0}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The following error occurred in the application: {0}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'Se produjo el siguiente error en la aplicación: {0}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'L´erreur d´application suivante s´est produite: {0}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '3060060001',
                                               var$codvalor,
                                               var$usuario);


   /* 2060060003 - El código de MAE informado no existe. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código do MAE relatado não existe.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The reported MAE code does not exist.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código de MAE informado no existe.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code de MAE indiqué n' || CHR(39) || 'existe pas.';
   gepr_pdiccionario.sguardar_diccionario_arr ('Genesis',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERARPERIODOS',
                                               '2060060003',
                                               var$codvalor,
                                               var$usuario);

   /* 2040010028 - El código externo {0} ya está configurado para el {1} de código {2} y descripción {3}. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O código externo {0} já está configurado para o {1} do código {2} e descrição {3}.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The external code {0} is already configured for the {1} of code {2} and description {3}.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El código externo {0} ya está configurado para el {1} de código {2} y descripción {3}.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le code externe {0} est déjà configuré pour le {1} du code {2} et la description {3}.';
   gepr_pdiccionario.sguardar_diccionario_arr ('GenesisSaldos',
                                               var$codidioma,
                                               var$codCulturaDefecto,
                                               'GENERICO',
                                               '2040010028',
                                               var$codvalor,
                                               var$usuario);

   /* 2040100017 - El movimiento informado es de un punto de servicio que no está configurado a la máquina y por esto no podrá se relacionar a uno período de acreditación. */
   var$codidioma := var$codidiomaVazio;
   var$codvalor := var$codvalorVazio;
   var$codidioma (0) := 'pt';
   var$codvalor (0) := 'O movimento relatado é de um ponto de serviço que não está configurado para a máquina e, portanto, não pode ser relacionado a um período de crédito.';
   var$codidioma (1) := 'en';
   var$codvalor (1) := 'The reported movement is from a service point that is not configured to the machine and therefore cannot be related to a crediting period.';
   var$codidioma (2) := 'es';
   var$codvalor (2) := 'El movimiento informado es de un punto de servicio que no está configurado a la máquina y por esto no podrá se relacionar a uno período de acreditación.';
   var$codidioma (3) := 'fr';
   var$codvalor (3) := 'Le mouvement signalé provient d' || CHR(39) || 'un point de service qui n' || CHR(39) || 'est pas configuré pour la machine et ne peut donc pas être lié à une période de crédit.';
   gepr_pdiccionario.sguardar_diccionario_arr (
                                                'Genesis',
                                                var$codidioma,
                                                var$codCulturaDefecto,
                                                'RELACIONARMOVIMIENTOSPERIODOS',
                                                '2040100017',
                                                var$codvalor,
                                                var$usuario);

   COMMIT;

EXCEPTION
   WHEN OTHERS
   THEN
      raise_application_error (
         -20001,
         'Arquivo: Genesis.sql Script: X_DICCIONARIO_Servicios' || SQLERRM);
      RAISE;
END;
/