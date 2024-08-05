DECLARE
  v_col_exists NUMBER ;
BEGIN
  SELECT count(*) INTO v_col_exists
    FROM user_tab_cols
    WHERE column_name = 'OID_TIPO_PERIODO'
      AND table_name = 'SAPR_TPLANXMENSAJE';

   IF (v_col_exists = 0) THEN
 
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXMENSAJE ADD OID_TIPO_PERIODO VARCHAR2(50) NULL';
      COMMIT;
   ELSE
    DBMS_OUTPUT.PUT_LINE('The column OID_TIPO_PERIODO already exists');
  END IF;
END;
/
declare
  l_nullable user_tab_columns.nullable%type;
  v_oid_tipo_periodo VARCHAR2(200) ;
begin
  select nullable into l_nullable
  from user_tab_columns
  where table_name = 'SAPR_TPLANXMENSAJE'
  and   column_name = 'OID_TIPO_PERIODO';
DBMS_OUTPUT.PUT_LINE(l_nullable);
  if l_nullable <> 'N' then
   SELECT oid_tipo_periodo INTO v_oid_tipo_periodo FROM sapr_ttipo_periodo WHERE cod_tipo_periodo = 'AC';
      UPDATE SAPR_TPLANXMENSAJE SET OID_TIPO_PERIODO = v_oid_tipo_periodo;
      EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TPLANXMENSAJE MODIFY OID_TIPO_PERIODO VARCHAR2(50) NOT NULL';
      COMMIT;
  end if;
end;
/