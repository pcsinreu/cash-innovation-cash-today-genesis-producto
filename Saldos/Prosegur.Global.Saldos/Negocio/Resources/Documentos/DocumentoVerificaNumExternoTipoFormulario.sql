select *
from 
  (
      SELECT P.IDFORMULARIO
      FROM PD_DOCUMENTOCABECERA P
      WHERE P.NUMEXTERNO = :numExterno
			   and P.IDDOCUMENTO <> :IdDocumento
			   and ((P.IDDOCUMENTO <> :IdOrigen and P.REENVIADO = 0) or :IdOrigen is null)
			   and P.SUSTITUIDO <> 1
			   and P.REENVIADO <> 1
      ORDER BY P.FECHA DESC
  )
WHERE rownum = 1
