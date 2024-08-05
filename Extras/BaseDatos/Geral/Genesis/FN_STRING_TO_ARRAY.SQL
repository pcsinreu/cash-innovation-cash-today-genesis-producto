CREATE OR REPLACE TYPE "VARCHAR2_ARRAY" AS TABLE OF VARCHAR2(32767)
/
CREATE OR REPLACE FUNCTION STRING_TO_ARRAY_###VERSION### 
( p_string        IN VARCHAR2,
  p_seperator     IN VARCHAR2 DEFAULT ',' ) RETURN VARCHAR2_ARRAY DETERMINISTIC IS
 
  l_string        VARCHAR2(4000) := p_seperator||p_string||p_seperator;
  l_array         VARCHAR2_ARRAY := VARCHAR2_ARRAY();

BEGIN
  /*Version: ###VERSION_COMP###*/

  IF LENGTH(p_seperator) != 1 THEN
    RAISE_APPLICATION_ERROR(-20101,'Seperator should only be one character');
  END IF;

  SELECT SUBSTR(l_string, INSTR(l_string,p_seperator,1,LEVEL)+1,
                          INSTR(l_string,p_seperator,1,LEVEL+1) - INSTR(l_string,p_seperator,1,LEVEL)-1 ) token
    BULK COLLECT INTO l_array
    FROM DUAL
 CONNECT BY LEVEL < LENGTH(l_string)-LENGTH(REPLACE(l_string,p_seperator,''));

  RETURN(l_array);

END; 
 
/
