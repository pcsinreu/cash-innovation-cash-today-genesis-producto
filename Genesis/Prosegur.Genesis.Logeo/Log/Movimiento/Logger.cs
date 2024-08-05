using System;
using System.Collections.Generic;

namespace Prosegur.Genesis.Logeo.Log.Movimiento
{
    public class Logger
    {
        /// <summary>
        /// Declaramos el constructor protegido por recomendación de SonarSource
        /// </summary>
        protected Logger()
        {
            //Solo por buenas practicas (ver https://rules.sonarsource.com/csharp/RSPEC-1118).
        }
        public static void GenerarIdentificador(string codigoPais, string recurso, ref string identificador)
        {
            try
            {
                AccesoDatos.GenesisSaldos.Logeo.Logeo.GeneraIdentificadorLlamada(ref codigoPais, ref recurso, ref identificador);
            }
            catch (Exception ex)
            {
                TratarErroBugsnag(ex);
            }
        }
        public static void IniciaLlamada(string identificador, string recurso, string version, object datosEntrada, string codigoPais, string codigoHashEntrada)
        {
            try
            {
                
                AccesoDatos.GenesisSaldos.Logeo.Logeo.IniciaLlamada(ref identificador,
                                                                ref recurso,
                                                                ref version,
                                                                ref datosEntrada,
                                                                ref codigoPais,
                                                                ref codigoHashEntrada);
            }
            catch (Exception ex)
            {
                TratarErroBugsnag(ex);
            }
        }
        public static void FinalizaLlamada(string identificador, object datosSalida, string codigoResultado, string descripcionResultado, string codigoHashSalida)
        {
            try
            {
                AccesoDatos.GenesisSaldos.Logeo.Logeo.FinalizaLlamada(ref identificador,
                                                                  ref datosSalida,
                                                                  ref codigoResultado,
                                                                  ref descripcionResultado,
                                                                  ref codigoHashSalida);
            }
            catch (Exception ex)
            {
                TratarErroBugsnag(ex);
            }

        }

        public static void AgregaDetalle(string identificador, string origen, string version, string mensaje, string codigoIdentificador)
        {
            try
            {
                AccesoDatos.GenesisSaldos.Logeo.Logeo.AgregaDetalle(ref identificador,
                                                               ref origen,
                                                               ref version,
                                                               ref mensaje,
                                                               ref codigoIdentificador);
            }
            catch (Exception ex)
            {
                TratarErroBugsnag(ex);
            }

        }

        public static List<LogeoEntidades.Log.Movimiento.Llamada> RecuperarDatos(string codigoPais, string codigoIdentificador, string identificadorLlamada, DateTime? fechaHoraInicio, DateTime? fechaHoraFin,
            string recurso, string hashEntrada, string hashSalida, string datosEntrada, string datosSalida, String sHostName, String ipE)
        {
            return AccesoDatos.GenesisSaldos.Logeo.Logeo.RecuperarDatos(codigoPais, codigoIdentificador, identificadorLlamada, recurso, datosEntrada, datosSalida, hashEntrada, hashSalida, fechaHoraInicio, fechaHoraFin, sHostName, ipE);
        }

        private static void TratarErroBugsnag(Exception ex)
        {
            try
            {
                Prosegur.BugsnagHelper.NotifyIfEnabled(ex);
            }
            catch (Exception)
            {
            }
        }
    }
}
