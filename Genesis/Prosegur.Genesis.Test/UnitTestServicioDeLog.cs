using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Prosegur.Genesis.Test
{
    [TestClass]
    public class UnitTestServicioDeLog
    {
        /// <summary>
        /// Se encarga de validar de que devuelva un nuevo identificador.
        /// Si estamos probando DEV, deberemos colocar como código país "UY"
        /// </summary>
        [TestMethod]
        public void TestGenerarIdentificadorLlamada()
        {
            /* Do Something */
            string nuevoIdentificador = string.Empty;
            string codigoPais = "UY";
            Prosegur.Genesis.Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, string.Empty,
                                                                             ref nuevoIdentificador);

            Assert.IsTrue(!string.IsNullOrEmpty(nuevoIdentificador));
        }

        /// <summary>
        /// Se encarga de generar:
        ///   1) Generar un nuevo identificador
        ///   2) Generar una nueva llamada con el identificador
        ///   3) Agregar detalles a esa llamada
        ///   4) Finalizar dicha llamada.
        /// </summary>
        [TestMethod]
        public void TestRealizarInvocacion()
        {
            string nuevoIdentificador = string.Empty;
            string codigoPais = "UY";
            string origen = "Un origen";
            string version = "Una versión";
            string datosDeEntrada = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. Quis aute iure reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint obcaecat cupiditat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            string codigo_actual_id = "123";
            bool evaluar = false;
            bool estado_test = false;

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, origen,
                                                                             ref nuevoIdentificador);


            Logeo.Log.Movimiento.Logger.IniciaLlamada(nuevoIdentificador, origen, version, datosDeEntrada, codigoPais, codigo_actual_id);
            evaluar = !string.IsNullOrEmpty(nuevoIdentificador);

            if (evaluar)
            {
                TestAgregarDetalle(nuevoIdentificador, origen, version);
                TestFinalizarLlamada(nuevoIdentificador);


                List<LogeoEntidades.Log.Movimiento.Llamada> lista = Logeo.Log.Movimiento.Logger.RecuperarDatos(codigoPais, codigo_actual_id, nuevoIdentificador, null, null, origen, null, null, null, null, null, null);

                if (lista != null && lista.Count > 0)
                {
                    estado_test = lista.Any(x => x.Detalles.Count > 0);
                }
                


            }

            Assert.IsTrue(evaluar);
            Assert.IsTrue(estado_test);
        }

        private void TestAgregarDetalle(string identificador, string origen, string version)
        {
            string mensaje = string.Format("¡Acá hay un nuevo mensaje generado a las {0}!", System.DateTime.Now);
            string codigoDocumento = "codigo_documento";

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificador, origen, version, mensaje, codigoDocumento);
        }

        private void TestFinalizarLlamada(string identificador)
        {
            string nuevoIdentificador = identificador;
            string datosDeSalida = "Varios datos de salida!!!";
            string codigoResultado = "Codigo1";
            string descripcionResultado = "Resultado OK";

            Logeo.Log.Movimiento.Logger.FinalizaLlamada(nuevoIdentificador, datosDeSalida, codigoResultado, descripcionResultado, null);
        }
        
        [TestMethod]
        public void TestRecuperarDatosSinFechas()
        {
            string codigo_pais = "UY";
            string codigo_actual_id = "123";
            var lista = Logeo.Log.Movimiento.Logger.RecuperarDatos(codigo_pais, codigo_actual_id, "", null, null, null, null, null,null,null, null, null);
            bool estado_test = lista != null && lista.Count > 0;


            Assert.IsTrue(estado_test);
        }

        [TestMethod]
        public void TestRecuperarDatosConFechas()
        {
            string codigo_pais = "UY";
            string codigo_actual_id = "123";
            bool estado_test = false;

            var lista = Logeo.Log.Movimiento.Logger.RecuperarDatos(codigo_pais, codigo_actual_id, "", new System.DateTime(2021, 1, 4, 0,0,0), new System.DateTime(2021, 1, 6,23,59,59), null,null,null,null,null, null, null);

            estado_test = lista != null && lista.Count > 0;

            Assert.IsTrue(estado_test);
        }
    }
}
