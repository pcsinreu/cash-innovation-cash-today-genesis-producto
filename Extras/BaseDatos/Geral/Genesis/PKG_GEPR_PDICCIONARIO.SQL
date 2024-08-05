create or replace package gepr_pdiccionario is
  /*Version: ###VERSION_COMP###*/

  TYPE tipo$codIdioma IS TABLE OF VARCHAR2(5) INDEX BY BINARY_INTEGER;
  TYPE tipo$valor IS TABLE OF VARCHAR2(4000) INDEX BY BINARY_INTEGER;

  procedure sguardar_diccionario_arr(par$codAplicacion           IN VARCHAR2,
                                par$codIdioma               IN tipo$codIdioma,
                                par$codCulturaDefecto       IN VARCHAR2,
                                par$codFuncionalidad        IN VARCHAR2,
                                par$codExpresion            IN VARCHAR2,
                                par$valor                IN tipo$valor,
                                par$usuario                 IN VARCHAR2);
                                
  procedure sguardar_diccionario(par$oidAplicacion           IN VARCHAR2,
                                par$codAplicacion           IN VARCHAR2,
                                par$codIdioma               IN VARCHAR2,
                                par$codCulturaDefecto       IN VARCHAR2,
                                par$codFuncionalidad        IN VARCHAR2,
                                par$codExpresion            IN VARCHAR2,
                                par$valor                IN VARCHAR2,
                                par$usuario                 IN VARCHAR2);

  procedure sins_tdiccionario(par$oidAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                IN VARCHAR2,
                                  par$defecto                 IN NUMBER,
                                  par$usuario                 IN VARCHAR2);

 procedure supd_tdiccionario(par$oidAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                IN VARCHAR2,
                                  par$usuario                 IN VARCHAR2);

  function fverifica_existe(par$oidAplicacion           IN VARCHAR2,
                            par$codIdioma               IN VARCHAR2,
                            par$codFuncionalidad        IN VARCHAR2,
                            par$codExpresion            IN VARCHAR2)
  RETURN BOOLEAN;
  
  function frecuperar_oidAplicacion(par$codAplicacion           IN VARCHAR2)
  RETURN VARCHAR2;

end gepr_pdiccionario;
/

create or replace package body gepr_pdiccionario is

  procedure sguardar_diccionario_arr(par$codAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN tipo$codIdioma,
                                  par$codCulturaDefecto       IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                   IN tipo$valor,
                                  par$usuario                 IN VARCHAR2) is
    var$oidAplicacion VARCHAR2(36);
  begin
    
      -- Busca OID da aplicação pelo código
    var$oidAplicacion := frecuperar_oidAplicacion(par$codAplicacion);
    
    FOR idx IN par$codIdioma.first .. par$codIdioma.last
    LOOP
      sguardar_diccionario(var$oidAplicacion,
                           NULL,
                           par$codIdioma(idx),
                           par$codCulturaDefecto,
                           par$codFuncionalidad,
                           par$codExpresion,
                           par$valor(idx),
                           par$usuario);
    END LOOP;
    
  end sguardar_diccionario_arr;
  
  procedure sguardar_diccionario(par$oidAplicacion           IN VARCHAR2,
                                  par$codAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN VARCHAR2,
                                  par$codCulturaDefecto       IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                   IN VARCHAR2,
                                  par$usuario                 IN VARCHAR2) is
    var$oidAplicacion VARCHAR2(36);
    var$codCultura tipo$codIdioma;
    var$defecto NUMBER;
  begin

    IF par$oidAplicacion IS NULL THEN
      -- Busca OID da aplicação pelo código
      var$oidAplicacion := frecuperar_oidAplicacion(par$codAplicacion);
    ELSE
      var$oidAplicacion := par$oidAplicacion;
    END IF;  

    --Carrega lista de culturas que serão adicionadas
    CASE WHEN par$codIdioma = 'pt-BR' THEN

      var$codCultura(1) := 'pt-BR';
      
    WHEN par$codIdioma = 'pt' THEN

      var$codCultura(0) := 'pt';
      var$codCultura(1) := 'pt-BR';
      var$codCultura(2) := 'pt-PT';

    WHEN par$codIdioma = 'en' THEN

      var$codCultura(0) := 'en';
      var$codCultura(1) := 'en-CN';
      var$codCultura(2) := 'en-IN';
      var$codCultura(3) := 'en-US';
      var$codCultura(4) := 'en-ZA';

    WHEN par$codIdioma = 'es' THEN

      var$codCultura(0) := 'es';
      var$codCultura(1) := 'es-AR';
      var$codCultura(2) := 'es-ES';
      var$codCultura(3) := 'es-PE';

    WHEN par$codIdioma = 'fr' THEN

      var$codCultura(0) := 'fr';
      var$codCultura(0) := 'fr-FR';

    WHEN par$codIdioma = 'zh' THEN

      var$codCultura(0) := 'zh';
      var$codCultura(0) := 'zh-HK';
    ELSE
      raise_application_error(-20002,'Idioma incexistente', true);
    END CASE;

    FOR idx IN var$codCultura.first .. var$codCultura.last
    LOOP

      -- Verifica de a chave já existe para aquela aplicação e funcionalidade
      IF fverifica_existe(var$oidAplicacion,
                          var$codCultura(idx),
                          par$codFuncionalidad,
                          par$codExpresion) THEN
        -- Atualiza valor
        supd_tdiccionario(var$oidAplicacion,
                          var$codCultura(idx),
                          par$codFuncionalidad,
                          par$codExpresion,
                          par$valor,
                          par$usuario);

     ELSE
       
        var$defecto := 0;
        IF par$codCulturaDefecto IS NOT NULL THEN
           --Define como padrão cultura recebida
           IF par$codCulturaDefecto = var$codCultura(idx) THEN
             var$defecto := 1;
           END IF;
        ELSE
          --Define o primeiro de cada idioma como o padrão
          IF idx = 0 THEN
            var$defecto := 1;
          END IF;
        END IF;
        
        -- Insere chave dicionário
        sins_tdiccionario(var$oidAplicacion,
                          var$codCultura(idx),
                          par$codFuncionalidad,
                          par$codExpresion,
                          par$valor,
                          var$defecto,
                          par$usuario);
     END IF;

    END LOOP;

  end sguardar_diccionario;

  procedure sins_tdiccionario(par$oidAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                   IN VARCHAR2,
                                  par$defecto                 IN NUMBER,
                                  par$usuario                 IN VARCHAR2) is
  begin

    INSERT INTO GEPR_TDICCIONARIO
                                 (OID_DICCIONARIO,
                                 OID_APLICACION,
                                 COD_IDIOMA,
                                 COD_FUNCIONALIDAD,
                                 COD_EXPRESION,
                                 VALOR,
                                 BOL_DEFECTO,
                                 DES_USUARIO_CREACION,
                                 GMT_CREACION,
                                 DES_USUARIO_ACTUALIZACION,
                                 GMT_ACTUALIZACION)
                           VALUES
                                (SYS_GUID(),
                                par$oidAplicacion,
                                par$codIdioma,
                                par$codFuncionalidad,
                                par$codExpresion,
                                par$valor,
                                par$defecto,
                                par$usuario,
                                SYSDATE,
                                par$usuario,
                                SYSDATE);
  end sins_tdiccionario;

  FUNCTION fverifica_existe(par$oidAplicacion           IN VARCHAR2,
                            par$codIdioma               IN VARCHAR2,
                            par$codFuncionalidad        IN VARCHAR2,
                            par$codExpresion            IN VARCHAR2)
      RETURN BOOLEAN
  IS
       var$cant INT;
  BEGIN
      SELECT COUNT(*) INTO var$cant
      FROM GEPR_TDICCIONARIO D
      WHERE D.OID_APLICACION = par$oidAplicacion AND
            D.COD_IDIOMA = par$codIdioma AND
            D.COD_FUNCIONALIDAD = par$codFuncionalidad AND
            D.COD_EXPRESION = par$codExpresion;

      RETURN NVL(var$cant, 0) > 0;
  END fverifica_existe;

  procedure supd_tdiccionario(par$oidAplicacion           IN VARCHAR2,
                                  par$codIdioma               IN VARCHAR2,
                                  par$codFuncionalidad        IN VARCHAR2,
                                  par$codExpresion            IN VARCHAR2,
                                  par$valor                   IN VARCHAR2,
                                  par$usuario                 IN VARCHAR2) is
  begin

    UPDATE GEPR_TDICCIONARIO SET VALOR = par$valor, DES_USUARIO_ACTUALIZACION = par$usuario, GMT_ACTUALIZACION = SYSDATE
    WHERE OID_APLICACION = par$oidAplicacion
          AND COD_FUNCIONALIDAD = par$codFuncionalidad
          AND COD_EXPRESION = par$codExpresion
          AND COD_IDIOMA = par$codIdioma;

  end supd_tdiccionario;
  
  FUNCTION frecuperar_oidAplicacion(par$codAplicacion           IN VARCHAR2)
      RETURN VARCHAR2
  IS
       var$oidAplicacion VARCHAR2(36);
  BEGIN
      BEGIN
      -- Busca OID da aplicação pelo código
      SELECT OID_APLICACION
      INTO var$oidAplicacion
      FROM GEPR_TAPLICACION AP
      WHERE AP.COD_APLICACION = par$codAplicacion;
    EXCEPTION
       WHEN NO_DATA_FOUND THEN raise_application_error(-20001,'Não foi possível recuperar o ID da aplicação', true);
    END;
    
    RETURN var$oidAplicacion;
  END frecuperar_oidAplicacion;

end gepr_pdiccionario;
/