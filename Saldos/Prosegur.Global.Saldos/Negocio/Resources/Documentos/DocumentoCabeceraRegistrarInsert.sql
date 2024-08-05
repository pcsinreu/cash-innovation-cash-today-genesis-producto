﻿INSERT INTO PD_DocumentoCabecera
  (IdDocumento,
   Fecha,
   IdCentroProcesoOrigen,
   IdCentroProcesoDestino,
   IdClienteOrigen,
   IdClienteDestino,
   IdBanco,
   IdUsuario,
   IdFormulario,
   IdBancoDeposito,
   FechaGestion,
   NumExterno,
   IdGrupo,
   Agrupado,
   EsGrupo,
   IdOrigen,
   Reenviado,
   Disponible,
   Sustituido,
   EsSustituto,
   IdDocDetalles,
   IdDocBultos,
   IdDocCamposExtra,
   IdPrimordial,
   Legado,
   IdMovimentacionFondo)
VALUES
  (:IdDocumento,
   :Fecha,
   :CentroProcesoOrigen,
   :CentroProcesoDestino,
   :ClienteOrigen,
   :ClienteDestino,
   :Banco,
   :IdUsuario,
   :IdFormulario,
   :BancoDeposito,
   :FechaGestion,
   :NumExterno,
   :IdGrupo,
   :Agrupado,
   :EsGrupo,
   :IdOrigen,
   :Reenviado,
   :Disponible,
   :Sustituido,
   :EsSustituto,
   :IdDocDetalles,
   :IdDocBultos,
   :IdDocCamposExtra,
   :IdPrimordial,
   :Legado,
   :IdMovimentacionFondo)