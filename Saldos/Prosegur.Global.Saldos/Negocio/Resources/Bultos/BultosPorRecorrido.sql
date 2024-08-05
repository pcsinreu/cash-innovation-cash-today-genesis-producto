select 
       PLA.Descripcion as PlantaDescripcion,
       to_number(CEV.Valor) as Recorrido,
       FRM.Descripcion as FormularioDescripcion,
       DOC.NumExterno as NumeroExterno,
       BUL.NumPrecinto as NumeroPrecinto,
       Trunc(DOC.Fecha) as Fecha,
       MON.Descripcion as MonedaDescripcion,
       case FRC.Condicion
         when 'E' then
          sum(DET.Importe) * -1
         else
          sum(DET.Importe)
       end as Importe
  from PD_DocumentoCabecera           DOC,
       PD_Formulario                  FRM,
       PD_DocumentoDetalle            DET,
       PD_Bulto                       BUL,
       PD_Moneda                      MON,
       PD_Especie                     ESP,
       PD_CampoExtraValor             CEV,
       PD_CentroProceso               CPR,
       PD_UsuarioCentroProceso        UCP,
       PD_Planta                      PLA,
       PD_FormularioReporteCondicion  FRC,
       PD_FormularioReporteCampoExtra FRE
 where MON.IdMoneda = ESP.IdMoneda
   and ESP.IdEspecie = DET.IdEspecie
   and DET.IdDocumento = NVL(DOC.IdDocDetalles, DOC.IdDocumento)
   and BUL.IdDocumento = NVL(DOC.IdDocDetalles, DOC.IdDocumento)
   and DOC.IdFormulario = FRM.IdFormulario
   and DOC.IdDocumento = CEV.IdDocumento
   and DOC.IdCentroProcesoOrigen = CPR.IdCentroProceso
   and PLA.IdPlanta = CPR.IdPlanta
   and CPR.IdCentroProceso = UCP.IdCentroProceso
   and CEV.IdCampoExtra = FRE.IdCampoExtra
   and FRE.IdFormulario = FRC.IdFormulario
   and FRE.Reporte = FRC.Reporte           
   and FRC.IdFormulario = DOC.IdFormulario
   and CEV.Valor Is Not Null
   and FRC.Reporte = 2
   and DOC.FECHA BETWEEN :DataIni AND :DataFim
   and UCP.IdUsuario = :IdUsr
 group by PLA.Descripcion,
          to_number(CEV.Valor),
          FRM.Descripcion,
          DOC.NumExterno,
          BUL.NumPrecinto,
          Trunc(DOC.Fecha),
          MON.Descripcion,
          FRC.Condicion
 order by PLA.Descripcion,
          to_number(CEV.Valor),
          FRM.Descripcion,
          Trunc(DOC.Fecha),
          DOC.NumExterno,
          BUL.NumPrecinto,
          MON.Descripcion,
          FRC.Condicion