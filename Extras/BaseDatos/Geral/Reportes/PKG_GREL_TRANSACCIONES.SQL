create or replace package sp_grel_transacciones_48 as

  procedure get_saldo_inicial
  (
    p_com_cliente               in varchar2
   ,p_com_sub_cliente           in varchar2
   ,p_com_punto_servicio        in varchar2
   ,p_com_sub_canales           in varchar2
   ,p_com_sector                in varchar2
   ,p_com_planta                in varchar2
   ,p_com_delegacion            in varchar2
   ,p_com_divisas_medios        in varchar2
   ,p_com_disponible            in number default 0
   ,p_com_no_disponible         in number default 0
   ,p_com_fecha_desde           in timestamp
   ,p_com_fecha_hasta           in timestamp
   ,p_filtro_sub_cliente        in number default 0
   ,p_filtro_pto_servicio       in number default 0
   ,p_filtro_sub_canal          in number default 0
   ,p_filtro_sector             in number default 0
   ,p_filtro_planta             in number default 0
   ,p_filtro_forms              in number default 0
   ,p_com_formulario            in varchar2
   ,p_filtro_divisas            in number default 0
   ,p_com_canales               in varchar2
   ,p_com_tipo_valor            in varchar2
   ,p_considerar_sectores_hijos in number default 1
   ,p_filtro_fecha              in varchar2 default 'CREACION'
   ,p_considerar_notificados    in number default 2
   ,p_considerar_acreditados    in number default 2
   ,p_retorno_transacciones     out sys_refcursor
  );

  procedure get_cuentas
  (
    p_com_cliente        in varchar2
   ,p_com_sub_cliente    in varchar2
   ,p_com_punto_servicio in varchar2
   ,p_com_sub_canales    in varchar2
   ,p_com_sector         in varchar2
   ,p_com_planta         in varchar2
   ,p_com_delegacion     in varchar2
   ,p_com_canales        in varchar2
   ,p_todos_niveis       in number
  );

  procedure get_transacciones
  (
    p_com_cliente         in varchar2
   ,p_com_sub_cliente     in varchar2
   ,p_com_punto_servicio  in varchar2
   ,p_com_sub_canales     in varchar2
   ,p_com_sector          in varchar2
   ,p_com_planta          in varchar2
   ,p_com_delegacion      in varchar2
   ,p_com_divisas_medios  in varchar2
   ,p_com_disponible      in number default 0
   ,p_com_no_disponible   in number default 0
   ,p_com_fecha_desde     in timestamp
   ,p_com_fecha_hasta     in timestamp
   ,p_saldo_inicial       in number default 0
   ,p_filtro_forms        in number default 0
   ,p_com_formulario      in varchar2
   ,p_filtro_divisas      in number default 0
   ,p_com_canales         in varchar2
   ,p_clausula_divisa     in varchar2
   ,p_tiene_saldo_inicial in number
   ,p_com_tipo_valor      in varchar2
   ,p_filtro_fecha        in varchar2
   ,p_considerar_notificados    in number default 2
   ,p_considerar_acreditados    in number default 2
  );

end sp_grel_transacciones_48;
/
create or replace package body sp_grel_transacciones_48 as

  /*================================================================================================
  | Revisando método original da package sp_grel_transacciones_48
  ================================================================================================*/
  procedure get_saldo_inicial
  (
    p_com_cliente               in varchar2
   ,p_com_sub_cliente           in varchar2
   ,p_com_punto_servicio        in varchar2
   ,p_com_sub_canales           in varchar2
   ,p_com_sector                in varchar2
   ,p_com_planta                in varchar2
   ,p_com_delegacion            in varchar2
   ,p_com_divisas_medios        in varchar2
   ,p_com_disponible            in number default 0
   ,p_com_no_disponible         in number default 0
   ,p_com_fecha_desde           in timestamp
   ,p_com_fecha_hasta           in timestamp
   ,p_filtro_sub_cliente        in number default 0
   ,p_filtro_pto_servicio       in number default 0
   ,p_filtro_sub_canal          in number default 0
   ,p_filtro_sector             in number default 0
   ,p_filtro_planta             in number default 0
   ,p_filtro_forms              in number default 0
   ,p_com_formulario            in varchar2
   ,p_filtro_divisas            in number default 0
   ,p_com_canales               in varchar2
   ,p_com_tipo_valor            in varchar2
   ,p_considerar_sectores_hijos in number
   ,p_filtro_fecha              in varchar2 default 'CREACION'
   ,p_considerar_notificados    in number default 2
   ,p_considerar_acreditados    in number default 2
   ,p_retorno_transacciones     out sys_refcursor
  ) is

    v_sql_where_bus_saldo          varchar2(32767);
    v_des_control_error            varchar2(100);
    v_sql                          clob;
    v_cantidad_cuentas             number;
    v_gerar_saldo_inicial          number;
    v_gerou_saldo_inicial          number;
    v_recuperar_fecha              number;
    v_totalizador_saldo            number;
    v_es_tesoro                    number;
    v_oid_formulario_cierre_tesoro varchar(36);
    v_existe                       number;
    v_fecha_saldo_inicial          timestamp;
    v_fecha_desde_original         timestamp := FN_GMT_DELEGACION(substr(p_com_delegacion, 0, 36),
                                                                  p_com_fecha_desde);
    v_fecha_desde                  timestamp := FN_GMT_DELEGACION(substr(p_com_delegacion, 0, 36),
                                                                  p_com_fecha_desde);
    v_fecha_hasta                  timestamp := FN_GMT_DELEGACION(substr(p_com_delegacion, 0, 36),
                                                                  p_com_fecha_hasta);
    v_divisas_saldo_inicial        varchar2(5000);
    v_divisas_sem_saldo            varchar2(5000) := NVL(p_com_divisas_medios,' ');
    v_cod_tipo_medio_pago          varchar2(15) := '';
    v_oid_sectores                 varchar2(32767) := '';
    v_sql_sectores_hijos           varchar2(32767) := '';
    v_oid_sector                   varchar2(36);
    v_tabela_historico             varchar2(50);
    v_where_medio_pago             varchar2(100);

    type sector_cur_type is ref cursor;
    v_sector_cur sector_cur_type;

  begin
    /* Tipo Valor */
    if p_com_tipo_valor = 'EFECTIVO'
    then
      v_cod_tipo_medio_pago := '-';
    elsif p_com_tipo_valor = 'CHEQUE'
    then
      v_cod_tipo_medio_pago := 'codtipob';
    elsif p_com_tipo_valor = 'TICKET'
    then
      v_cod_tipo_medio_pago := 'codtipoa';
    elsif p_com_tipo_valor = 'OTROS_VALORES'
    then
      v_cod_tipo_medio_pago := 'codtipo';
    end if;

    /* Limpar tabelas temporárias */
    v_des_control_error := 'LIMPANDO TABELA TEMPORARIA';
    execute immediate 'truncate table rpge_gtt_tdatos_cuenta';
    execute immediate 'truncate table rpge_gtt_tsaldo_inicial_trans';
    execute immediate 'truncate table rpge_gtt_tdatos_trasacciones';
    execute immediate 'truncate table rpge_gtt_tdatos_documento';

    v_des_control_error := 'MONTANDO CLAUSULA WHERE RECUPERAR CUENTA';
      v_oid_sectores := p_com_sector;

    /* Setor? */
    if (p_com_sector <> ' ')
    then

      /* verifica se deve recuperar os sectores filhos dos sectores informados */
      if p_considerar_sectores_hijos = 1
      then
        v_oid_sectores := '';
        v_sql_sectores_hijos :=
          q'[select oid_sector from rpge_ysector start with oid_sector in (']' ||p_com_sector ||''')'
          ||'connect by nocycle prior oid_sector = oid_sector_padre';

        open v_sector_cur for v_sql_sectores_hijos;
        loop
          fetch v_sector_cur into v_oid_sector;
          exit when v_sector_cur%notfound;
          v_oid_sectores := v_oid_sectores || q'[,']' ||v_oid_sector ||'''';
        end loop;
        close v_sector_cur;
      end if;
    else
      /* verifica se deve recuperar os sectores filhos dos sectores informados */
      if p_considerar_sectores_hijos = 0
      then
        v_oid_sectores := '';
        v_sql_sectores_hijos :=
            ' select s.oid_sector from rpge_ysector s '
          ||' inner join rpge_yplanta p on p.oid_planta = s.oid_planta '
          ||' where s.oid_sector_padre is null ';

        /* Planta? */
        if (p_com_planta <> ' ')
        then
          v_sql_sectores_hijos :=
            v_sql_sectores_hijos ||q'[ and p.oid_planta in (']' ||p_com_planta ||''')';
        end if;

        /* Delegação? */
        if (p_com_delegacion <> ' ')
        then
          v_sql_sectores_hijos :=
            v_sql_sectores_hijos || q'[ and p.oid_delegacion in (']' ||p_com_delegacion ||''')';
        end if;

        open v_sector_cur for v_sql_sectores_hijos;
        loop
          fetch v_sector_cur into v_oid_sector;
          exit when v_sector_cur%notfound;
          v_oid_sectores := v_oid_sectores || q'[',']' ||v_oid_sector ||'''';
        end loop;
        close v_sector_cur;
      end if;
    end if;

    /* retirar a primeira virgula a primeira aspas e última aspas */
    v_oid_sectores := substr(v_oid_sectores, 3, length(v_oid_sectores) - 3);

    /* Carregar as contas */
    get_cuentas(p_com_cliente, p_com_sub_cliente, p_com_punto_servicio, p_com_sub_canales,
                v_oid_sectores, p_com_planta, p_com_delegacion, p_com_canales, 0);

    v_des_control_error := 'EXECUTANDO VALIDACOES PARA GERAR O SALDO INICIAL';


    if (p_com_tipo_valor = 'EFECTIVO') THEN
       v_tabela_historico :=' RPGE_YSALDO_EFECTIVO_HISTORICO ';
       v_where_medio_pago :='';
    else
       v_tabela_historico :=' RPGE_YSALDO_MEDIO_PAGO_HIST ';
       v_where_medio_pago :=' AND H.cod_tipo_medio_pago =''' || v_cod_tipo_medio_pago ||'''';
    end if;

    v_sql :='insert into rpge_gtt_tsaldo_inicial_trans(num_valor_saldo, fecha_documento, oid_divisa, cod_iso_divisa)'
    ||' WITH DATA_SALDO AS  '
    ||'    (SELECT NULL AS NAO_APAGAR, H.OID_DIVISA, MAX(H.FEC_SALDO) AS FEC_SALDO  '
    ||'    FROM ' || v_tabela_historico || ' H'
    ||'     INNER JOIN rpge_gtt_tdatos_cuenta cue  '
    ||'         on cue.oid_cuenta = H.oid_cuenta_saldo  '
    ||'    WHERE H.BOL_DISPONIBLE = 1 '
    ||     v_where_medio_pago
    || q'[ AND H.FEC_SALDO < to_date(']' || to_char(p_com_fecha_desde, 'dd/mm/yyyy hh24:mi:ss') || q'[','dd/mm/yyyy hh24:mi:ss')]';

    if (p_com_divisas_medios is not null)
    then
      v_sql := v_sql || q'[ and H.oid_divisa in (']' || p_com_divisas_medios || ''')';
    end if;

    v_sql := v_sql ||' GROUP BY H.OID_DIVISA), SALDO_HISTORICO AS('
    ||'   SELECT SUM(H.NUM_IMPORTE) AS NUM_IMPORTE  '
    ||'          ,MIN(H.GMT_CREACION) GMT_CREACION '
    ||'          ,H.OID_DIVISA '
    ||'          ,DIV.COD_ISO_DIVISA '
    ||'    FROM ' || v_tabela_historico || ' H'
    ||'    INNER JOIN rpge_gtt_tdatos_cuenta cue '
    ||'       on cue.oid_cuenta = H.oid_cuenta_saldo '
    ||'    INNER JOIN DATA_SALDO DS '
    ||'       ON DS.FEC_SALDO = H.FEC_SALDO '
    ||'      AND DS.OID_DIVISA = H.OID_DIVISA '
    ||'    INNER JOIN RPGE_YDIVISA DIV ON DIV.OID_DIVISA = H.OID_DIVISA '
    ||'    WHERE H.BOL_DISPONIBLE = 1 '
    ||     v_where_medio_pago
    ||'    GROUP BY '
    ||'          H.OID_DIVISA '
    ||'          ,DIV.COD_ISO_DIVISA) SELECT * FROM SALDO_HISTORICO';

    execute immediate v_sql;
    commit;
	   
    begin
      select nvl(min(fecha_documento),v_fecha_desde_original)
             into v_fecha_desde
             from rpge_gtt_tsaldo_inicial_trans;
    exception
      when no_data_found then
          v_fecha_desde:= v_fecha_desde_original;
    end;

    get_transacciones(p_com_cliente, p_com_sub_cliente, p_com_punto_servicio,
                        p_com_sub_canales, v_oid_sectores, p_com_planta, p_com_delegacion,
                        p_com_divisas_medios, p_com_disponible, p_com_no_disponible, v_fecha_desde,
                        v_fecha_hasta, 0, p_filtro_forms, p_com_formulario, 1,
                        p_com_canales, ' IN ', 1, p_com_tipo_valor, p_filtro_fecha, p_considerar_notificados, p_considerar_acreditados);

    /* Retorno */
    open p_retorno_transacciones for
      select t.fyh_gestion
            ,t.fecha_resolusion
            ,t.cliente_origen
            ,t.subcliente_origen
            ,t.punto_servicio_origen
            ,t.canal_origen
            ,t.des_sector_ori
            ,t.cliente_destino
            ,t.subcliente_destino
            ,t.punto_servicio_destino
            ,t.canal_destino
            ,t.des_sector_des
            ,t.des_divisa
            ,t.bol_disponible
            ,t.cod_tipo_movimiento
            ,t.bol_certificado
            ,t.des_formulario
            ,t.cod_externo
            ,t.cod_comprobante
            ,t.des_usuario_creacion
            ,t.bol_contra_movimiento
            ,sum(t.num_importe) num_importe
            ,max(t.num_saldo_inicial) num_saldo_inicial
            ,t.fyh_cierre_tesoro
            ,t.bol_tiene_saldo_inicial
            ,max(t.fecha_trans) as fecha_trans
            ,t.des_sector_aplicado
            ,t.obs_documento
        from rpge_gtt_tdatos_trasacciones t
       group by t.fyh_gestion
               ,t.fecha_resolusion
               ,t.cliente_origen
               ,t.subcliente_origen
               ,t.punto_servicio_origen
               ,t.canal_origen
               ,t.des_sector_ori
               ,t.cliente_destino
               ,t.subcliente_destino
               ,t.punto_servicio_destino
               ,t.canal_destino
               ,t.des_sector_des
               ,t.des_divisa
               ,t.bol_disponible
               ,t.cod_tipo_movimiento
               ,t.bol_certificado
               ,t.des_formulario
               ,t.cod_externo
               ,t.cod_comprobante
               ,t.des_usuario_creacion
               ,t.bol_contra_movimiento
               ,t.fyh_cierre_tesoro
               ,t.bol_tiene_saldo_inicial
               ,t.des_sector_aplicado
               ,t.obs_documento;

  exception
    when others then
      begin
        execute immediate 'truncate table rpge_gtt_tdatos_cuenta';
        execute immediate 'truncate table rpge_gtt_tsaldo_inicial_trans';
        execute immediate 'truncate table rpge_gtt_tdatos_trasacciones';
        execute immediate 'truncate table rpge_gtt_tdatos_documento';
        raise_application_error(-20002, substr(sqlcode || '   ' || sqlerrm || ' ' || v_des_control_error,1,4000));
      end;

  end get_saldo_inicial;


  /*================================================================================================
  | Revisando método original da package sp_grel_transacciones_48
  ================================================================================================*/
  procedure get_cuentas
  (
    p_com_cliente        in varchar2
   ,p_com_sub_cliente    in varchar2
   ,p_com_punto_servicio in varchar2
   ,p_com_sub_canales    in varchar2
   ,p_com_sector         in varchar2
   ,p_com_planta         in varchar2
   ,p_com_delegacion     in varchar2
   ,p_com_canales        in varchar2
   ,p_todos_niveis       in number
  ) is

    /* variáveis */
    o$cmd_sql   clob;
    o$where_sql varchar2(32767):=' ';

  begin

    /* Limpar tabela temporária */
    execute immediate 'truncate table rpge_gtt_tdatos_cuenta';

    /* Comando SQL */
    o$cmd_sql :=
        ' select distinct '
      ||'        cu.oid_cuenta '
      ||'   from rpge_ycuenta cu '
      ||'  inner join rpge_ycliente cl '
      ||'     on cl.oid_cliente = cu.oid_cliente '
      ||'  inner join rpge_ysector se '
      ||'     on se.oid_sector = cu.oid_sector '
      ||'  inner join rpge_yplanta pl '
      ||'     on pl.oid_planta = se.oid_planta '
      ||'  inner join rpge_ydelegacion del '
      ||'     on del.oid_delegacion = pl.oid_delegacion '
      ||'  inner join rpge_ysubcanal sc '
      ||'     on sc.oid_subcanal = cu.oid_subcanal '
      ||'  inner join rpge_ycanal ca '
      ||'     on ca.oid_canal = sc.oid_canal '
      ||'   left join rpge_ysubcliente scl '
      ||'     on scl.oid_subcliente = cu.oid_subcliente '
      ||'   left join rpge_ypunto_servicio pto '
      ||'     on pto.oid_pto_servicio = cu.oid_pto_servicio '
      ||'  where 0=0 '
    ;

    /*==================================================
    | Montar query de CUENTA c/ os parâmetros informados
    ==================================================*/

    /* Cliente */
    o$cmd_sql := o$cmd_sql || q'[ and cl.oid_cliente in (']' ||p_com_cliente ||''')';

    /* Subcliente */
    if (p_com_sub_cliente <> ' ')
    then
      o$where_sql := o$where_sql || q'[ and cu.oid_subcliente in (']' ||p_com_sub_cliente ||''')';

      /* Ponto de Serviço? */
      if (p_com_punto_servicio <> ' ')
      then
        o$where_sql := o$where_sql || q'[ and cu.oid_pto_servicio in (']' ||p_com_punto_servicio ||''')';
      elsif (p_todos_niveis = 0)
      then
        o$where_sql := o$where_sql || ' and cu.oid_pto_servicio is null';
      end if;
    elsif (p_todos_niveis = 0)
    then
      o$where_sql := o$where_sql || ' and cu.oid_subcliente is null and cu.oid_pto_servicio is null ';
    end if;

    /* Canal? */
    if (p_com_canales <> ' ')
    then
      dbms_output.put_line('p_com_canales');
      o$where_sql := o$where_sql || q'[ and ca.oid_canal in (']' ||p_com_canales ||''')';

      /* Subcanal? */
      if (p_com_sub_canales <> ' ')
      then
        o$where_sql := o$where_sql || q'[ and cu.oid_subcanal in (']' ||p_com_sub_canales ||''')';
      end if;
    end if;

    /* Setor? */
    if (p_com_sector <> ' ')
    then
      o$where_sql := o$where_sql || q'[ and cu.oid_sector in (']' ||p_com_sector ||''')';
    end if;

    /* Planta? */
    if (p_com_planta <> ' ')
    then
      o$where_sql :=
        o$where_sql || q'[ and pl.oid_planta in (']' ||p_com_planta ||''')';
    end if;

    /* Delegação? */
    if (p_com_delegacion <> ' ')
    then
      o$where_sql :=
        o$where_sql || q'[ and del.oid_delegacion in (']' || p_com_delegacion ||''')';
    end if;

    o$cmd_sql := o$cmd_sql || o$where_sql;
    o$cmd_sql := 'insert into rpge_gtt_tdatos_cuenta(oid_cuenta) ' ||o$cmd_sql;

    execute immediate o$cmd_sql;
    commit;

  end get_cuentas;


  /*================================================================================================
  | Revisando método original da package sp_grel_transacciones_48
  ================================================================================================*/
  procedure get_transacciones
  (
    p_com_cliente         in varchar2
   ,p_com_sub_cliente     in varchar2
   ,p_com_punto_servicio  in varchar2
   ,p_com_sub_canales     in varchar2
   ,p_com_sector          in varchar2
   ,p_com_planta          in varchar2
   ,p_com_delegacion      in varchar2
   ,p_com_divisas_medios  in varchar2
   ,p_com_disponible      in number default 0
   ,p_com_no_disponible   in number default 0
   ,p_com_fecha_desde     in timestamp
   ,p_com_fecha_hasta     in timestamp
   ,p_saldo_inicial       in number default 0
   ,p_filtro_forms        in number default 0
   ,p_com_formulario      in varchar2
   ,p_filtro_divisas      in number default 0
   ,p_com_canales         in varchar2
   ,p_clausula_divisa     in varchar2
   ,p_tiene_saldo_inicial in number
   ,p_com_tipo_valor      in varchar2
   ,p_filtro_fecha        in varchar2
   ,p_considerar_notificados    in number default 2
   ,p_considerar_acreditados    in number default 2
  ) is

    /* variáveis */
    v_cod_tipo_medio_pago varchar2(100) := '';
    v_sql                 varchar2(30000);

  begin
/* Preencher tabela temporária com as CUENTAS vinculadas */
    get_cuentas(p_com_cliente, p_com_sub_cliente, p_com_punto_servicio, p_com_sub_canales,
                p_com_sector, p_com_planta, p_com_delegacion, p_com_canales, 1);

    /* Verificar tipo Valor */
    if p_com_tipo_valor = 'CHEQUE'
    then
      v_cod_tipo_medio_pago := 'codtipob';
    elsif p_com_tipo_valor = 'TICKET'
    then
      v_cod_tipo_medio_pago := 'codtipoa';
    elsif p_com_tipo_valor = 'OTROS_VALORES'
    then
      v_cod_tipo_medio_pago := 'codtipo';
    end if;


    /* Buscar as transações existentes */
    v_sql :=
        ' insert into rpge_gtt_tdatos_trasacciones '
      ||'   (fyh_gestion, fecha_resolusion, cliente_origen, subcliente_origen, punto_servicio_origen, '
      ||'    canal_origen, des_sector_ori, cliente_destino, subcliente_destino, punto_servicio_destino, '
      ||'    canal_destino, des_sector_des, des_divisa, bol_disponible, cod_tipo_movimiento, bol_certificado, '
      ||'    des_formulario, cod_externo, cod_comprobante, des_usuario_creacion, bol_contra_movimiento, '
      ||'    num_importe, num_saldo_inicial, fyh_cierre_tesoro, bol_tiene_saldo_inicial, fecha_trans, '
      ||'    des_sector_aplicado, obs_documento) '
      ||' with q as ( '
      ||'   select docu.cod_comprobante '
      ||'         ,docu.oid_documento identificador '
      ||'         ,docu.cod_externo cod_externo '
      ||'         ,docu.fyh_gestion '
      ||'         ,docu.gmt_creacion fecha_resolusion '
      ||'         ,trans.oid_divisa '
      ||'         ,trans.bol_disponible '
      ||'         ,trans.num_importe '
      ||'         ,trans.cod_tipo_movimiento '
      ||'         ,docu.bol_certificado '
      ||'         ,form.des_formulario '
      ||'         ,docu.des_usuario_creacion '
      ||'         ,trans.gmt_creacion fecha_trans '
      ||'         ,trans.bol_contra_movimiento '
      ||'         ,docu.oid_cuenta_origen '
      ||'         ,docu.oid_cuenta_destino '
      ||'         ,trans.oid_cuenta_saldo '
      ||'         ,docu.obs_documento '
      ||'    from rpge_ydocumento docu ';

    if (p_com_tipo_valor = 'EFECTIVO')
    then
      v_sql := v_sql
        || ' inner join rpge_ytransaccion_efectivo trans on docu.oid_documento = trans.oid_documento ';
    else
      v_sql := v_sql
        || ' inner join rpge_ytransaccion_medio_pago trans on docu.oid_documento = trans.oid_documento ';
    end if;

    v_sql := v_sql
      || ' inner join rpge_yformulario form on docu.oid_formulario = form.oid_formulario '
      || ' inner join rpge_gtt_tdatos_cuenta gttc on gttc.oid_cuenta = trans.oid_cuenta_saldo ';

    if (p_considerar_acreditados <> 2)
    then
      v_sql := v_sql
        || ' left join rpge_yperiodoxdocumento pd on pd.oid_documento = docu.oid_documento '
        || ' left join rpge_yperiodo p on p.oid_periodo = pd.oid_periodo '
        || ' left join rpge_yestado_periodo estp on estp.oid_estado_periodo = p.oid_estado_periodo ';
    end if;
    
    v_sql := v_sql
      || q'[ where docu.cod_estado = 'AC' ]';

    v_sql := v_sql
      || ' and (trans.bol_disponible = ' || p_com_disponible ||' or trans.bol_disponible =' || p_com_no_disponible || ')';

    if p_filtro_fecha = 'CREACION'
    then
      v_sql := v_sql
        || q'[ and sys_extract_utc(trans.gmt_creacion) >= to_timestamp(']'
        || to_char(p_com_fecha_desde, 'dd/mm/yyyy hh24:mi:ss')
        || q'[','dd/mm/yyyy hh24:mi:ss TZH:TZM')]';

     v_sql := v_sql
        || q'[ and sys_extract_utc(trans.gmt_creacion) <= to_timestamp(']'
        || to_char(p_com_fecha_hasta, 'dd/mm/yyyy hh24:mi:ss')
        || q'[','dd/mm/yyyy hh24:mi:ss')]';
    else
      v_sql := v_sql
        || q'[ and docu.fyh_gestion >= to_date(']'
        || to_char(p_com_fecha_desde, 'dd/mm/yyyy hh24:mi:ss')
        || q'[','dd/mm/yyyy hh24:mi:ss')]';

      v_sql := v_sql
        || q'[ and docu.fyh_gestion <= to_date(']'
        || to_char(p_com_fecha_hasta, 'dd/mm/yyyy hh24:mi:ss')
        || q'[','dd/mm/yyyy hh24:mi:ss')]';
    end if;

    if (p_com_formulario is not null)
    then
      v_sql := v_sql || q'[ and docu.oid_formulario in (']' || p_com_formulario || ''')';
    end if;
    
    if (p_considerar_notificados <> 2)
    then
      v_sql := v_sql || ' and docu.bol_notificado = ' || p_considerar_notificados || ' ';
    end if;
    
    if (p_considerar_acreditados <> 2)
    then
      if (p_considerar_acreditados = 1)
      then
         v_sql := v_sql || q'[ and estp.cod_estado_periodo in ('AC', 'IG') ]';
      else
         v_sql := v_sql || q'[ and estp.cod_estado_periodo not in ('AC', 'IG') ]';
      end if;
    end if;

    if (p_com_divisas_medios is not null)
    then
      v_sql := v_sql || q'[ and trans.oid_divisa in (']' || p_com_divisas_medios || ''')';
    end if;

    if (p_com_tipo_valor <> 'EFECTIVO')
    then
      v_sql := v_sql || ' and trans.cod_tipo_medio_pago =''' || v_cod_tipo_medio_pago ||'''';
    end if;

    /* Retorno */
    v_sql := v_sql || ' ) ';
    v_sql := v_sql
      ||' select fn_gmt_delegacion(vco.oid_delegacion, fyh_gestion) fyh_gestion '
      ||'       ,fn_gmt_delegacion(vco.oid_delegacion, fecha_resolusion) fecha_resolusion '
      ||'       ,vco.des_cliente as cliente_origen '
      ||'       ,vco.des_subcliente as subcliente_origen '
      ||'       ,vco.des_pto_servicio as punto_servicio_origen '
      ||'       ,vco.des_canal as canal_origen '
      ||'       ,vco.des_sector as des_sector_ori '
      ||'       ,vcd.des_cliente as cliente_destino '
      ||'       ,vcd.des_subcliente as subcliente_destino '
      ||'       ,vcd.des_pto_servicio as punto_servicio_destino '
      ||'       ,vcd.des_canal as canal_destino '
      ||'       ,vcd.des_sector as des_sector_destino '
      ||'       ,div.des_divisa '
      ||'       ,q.bol_disponible '
      ||'       ,q.cod_tipo_movimiento '
      ||'       ,q.bol_certificado '
      ||'       ,q.des_formulario '
      ||'       ,case when pc.cod_precinto is null then q.cod_externo else pc.cod_precinto end as cod_precinto '
      ||'       ,q.cod_comprobante '
      ||'       ,q.des_usuario_creacion '
      ||'       ,q.bol_contra_movimiento '
      ||'       ,q.num_importe '

      /* Saldo inicial */
      ||',nvl(sit.num_valor_saldo,0) as num_saldo_inicial '

      /* Data Cierre */
      ||',fn_gmt_delegacion(vco.oid_delegacion, '
      ||q'[to_date(']' || to_char(p_com_fecha_desde, 'dd/mm/yyyy hh24:mi:ss')|| q'[','dd/mm/yyyy hh24:mi:ss') ]'
      ||') fyh_cierre_tesoro '

      ||'       ,case when sit.num_valor_saldo is not null then 1 else 0 end bol_tiene_saldo_inicial '
      ||'       ,fn_gmt_delegacion(vco.oid_delegacion, q.fecha_trans) fecha_trans '
      ||'       ,vca.des_sector as des_sector_aplicado '
      ||'       ,obs_documento '
      ||'   from q '
      ||'  inner join rpge_yvcuenta vco '
      ||'     on vco.oid_cuenta = q.oid_cuenta_origen '
      ||'  inner join rpge_yvcuenta vcd '
      ||'     on vcd.oid_cuenta = q.oid_cuenta_destino '
      ||'  inner join rpge_yvcuenta vca '
      ||'     on vca.oid_cuenta = q.oid_cuenta_saldo '
      ||'  inner join rpge_ydivisa div '
      ||'     on div.oid_divisa = q.oid_divisa '
      ||'  left join rpge_gtt_tsaldo_inicial_trans sit on sit.oid_divisa = div.oid_divisa '
	  ||'  left join rpge_ycontenedor con on con.oid_documento = q.identificador '
	  ||'  left join rpge_yprecintoxcontenedor pc on pc.oid_contenedor = con.oid_contenedor and pc.bol_precinto_automatico = 1 ';

    execute immediate v_sql;
    commit;

  end get_transacciones;

end sp_grel_transacciones_48;
/
