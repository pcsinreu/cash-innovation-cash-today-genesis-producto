UPDATE PD_CampoExtra
   SET Nombre           = :Nombre,
       IdTipoCampoExtra = :IdTipoCampoExtra,
       SeValida         = :SeValida
 WHERE IdCampoExtra = :IdCampoExtra