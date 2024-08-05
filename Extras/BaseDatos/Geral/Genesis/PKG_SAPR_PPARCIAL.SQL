CREATE OR REPLACE PACKAGE SAPR_PPARCIAL_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  /* sins_tparcial */
  PROCEDURE sins_tparcial(par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_externo       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_estado        IN gepr_pcomon_###VERSION###.tipo$est_,
                          par$oid_iac           IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tparcial */
  PROCEDURE sdel_tparcial(par$oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$deletes     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sgrabar_parcial */
  PROCEDURE sgrabar_parcial(par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_externo       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado        IN gepr_pcomon_###VERSION###.tipo$est_,
                            par$oid_iac           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tipo_formato  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_parciales */
  PROCEDURE scargar_tt_oids_parciales(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fregistro_existe */
  FUNCTION fregistro_existe(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* fvalidar_parcial */
  FUNCTION fvalidar_parcial(par$cod_estado  IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  FUNCTION frecupera_lista_valor(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo    IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

END SAPR_PPARCIAL_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PPARCIAL_###VERSION### AS

  /* sins_tparcial */
  PROCEDURE sins_tparcial(par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_externo       IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_estado        IN gepr_pcomon_###VERSION###.tipo$est_,
                          par$oid_iac           IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                          par$oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    INSERT INTO SAPR_TPARCIAL
      (OID_PARCIAL,
       OID_EXTERNO,
       OID_BULTO,
       COD_PRECINTO,
       COD_ESTADO,
       OID_IAC,
       COD_USUARIO_RESPONSABLE,
       COD_PUESTO_RESPONSABLE,
       NEC_SECUENCIA,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       OID_PARCIAL_PADRE,
       OID_PARCIAL_SUSTITUTA,
       DES_USUARIO_MODIFICACION)
    VALUES
      (par$oid_parcial,
       par$oid_externo,
       par$oid_bulto,
       par$cod_precinto,
       par$cod_estado,
       par$oid_iac,
       par$usuario_resp,
       par$puesto_resp,
       par$nec_secuencia,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$oid_parcial_padre,
       par$oid_parcial_sub,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tparcial;

  /* sdel_tparcial */
  PROCEDURE sdel_tparcial(par$oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$deletes     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    IF par$oid_parcial IS NOT NULL THEN
      DELETE SAPR_TPARCIAL WHERE OID_PARCIAL = par$oid_parcial;
      par$deletes := sql%rowcount;
    ELSIF par$oid_bulto IS NOT NULL THEN
      DELETE SAPR_TPARCIAL WHERE OID_BULTO = par$oid_bulto;
      par$deletes := sql%rowcount;
    ELSE
      DELETE SAPR_TPARCIAL
       WHERE OID_BULTO IN (SELECT OID_BULTO
                             FROM SAPR_TBULTO
                            WHERE OID_REMESA = par$oid_remesa);
      par$deletes := sql%rowcount;
    END IF;
  
  END sdel_tparcial;

  /* sgrabar_parcial */
  PROCEDURE sgrabar_parcial(par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_externo       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado        IN gepr_pcomon_###VERSION###.tipo$est_,
                            par$oid_iac           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tipo_formato  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$oid_parcial gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
    par$inserts := 0;
  
    IF fvalidar_parcial(par$cod_estado, par$usuario, par$cod_cultura) THEN
    
      var$oid_parcial := par$oid_parcial;
      IF par$oid_parcial IS NULL THEN
        SELECT SYS_GUID() INTO var$oid_parcial FROM DUAL;
      END IF;
    
      -- Insere Parcial
      sins_tparcial(var$oid_parcial,
                    par$oid_externo,
                    par$oid_bulto,
                    par$cod_precinto,
                    par$cod_estado,
                    par$oid_iac,
                    par$usuario_resp,
                    par$puesto_resp,
                    par$nec_secuencia,
                    par$oid_parcial_padre,
                    par$oid_parcial_sub,
                    par$usuario,
                    par$inserts);
      par$inserts := par$inserts + sql%rowcount;
    
      /* Insere o formato da parcial */
      IF par$oid_tipo_formato IS NOT NULL THEN
        sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                          par$oid_remesa      => par$oid_remesa,
                                                          par$oid_bulto       => par$oid_bulto,
                                                          par$oid_parcial     => par$oid_parcial,
                                                          par$oid_lista_valor => par$oid_tipo_formato,
                                                          par$cod_lista_valor => null,
                                                          par$cod_lista_tipo  => null,
                                                          par$cod_usuario     => par$usuario,
                                                          par$cod_cultura     => par$cod_cultura,
                                                          par$inserts         => par$inserts);
        par$inserts := par$inserts + sql%rowcount;
      END IF;
    
    END IF;
  END sgrabar_parcial;

  /* scargar_tt_oids_parciales */
  PROCEDURE scargar_tt_oids_parciales(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_BULTO
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'BULTO')
      SELECT DISTINCT P.OID_PARCIAL, 'PARCIAL'
        FROM SAPR_TPARCIAL P
       INNER JOIN q
          ON q.OID_BULTO = P.OID_BULTO
       WHERE P.OID_PARCIAL IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_parciales;

  /* fregistro_existe */
  FUNCTION fregistro_existe(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
    var$cant INT;
  BEGIN
    SELECT COUNT(1)
      INTO var$cant
      FROM sapr_tparcial
     WHERE oid_parcial = par$oid_parcial;
    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
  END fregistro_existe;

  /* fvalidar_parcial */
  FUNCTION fvalidar_parcial(par$cod_estado IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$usuario    IN gepr_pcomon_###VERSION###.tipo$usr_
                            
                            /* cultura/idioma */,
                            par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
  BEGIN
  
    IF par$cod_estado IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Estado',
                                                                  0));
      RETURN False;
    END IF;
  
    IF par$usuario IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Usuario',
                                                                  0));
      RETURN False;
    END IF;
  
    RETURN True;
  END fvalidar_parcial;

  FUNCTION frecupera_lista_valor(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo    IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$retorno gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    BEGIN
    
      SELECT LV.OID_LISTA_VALOR
        INTO var$retorno
        FROM SAPR_TLISTA_VALORXELEMENTO VE
       INNER JOIN GEPR_TLISTA_VALOR LV
          ON VE.OID_LISTA_VALOR = LV.OID_LISTA_VALOR
       INNER JOIN GEPR_TLISTA_TIPO LT
          ON LT.OID_LISTA_TIPO = VE.OID_LISTA_TIPO
       WHERE LT.COD_TIPO = par$cod_tipo
         AND VE.OID_PARCIAL = par$oid_parcial;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$retorno := NULL;
    END;
  
    return var$retorno;
  end frecupera_lista_valor;

END SAPR_PPARCIAL_###VERSION###;
/
