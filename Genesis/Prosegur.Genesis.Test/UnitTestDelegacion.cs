using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prosegur.Genesis.ContractoServicio;
using Prosegur.Genesis.LogicaNegocio;
using System.Collections.Generic;
//using Prosegur.Genesis.LogicaNegocio.AccionGenesisLogin;
//using Prosegur.Genesis.LogicaNegocio;
//using Prosegur.Genesis.ContractoServicio.GenesisLogin;
//using Prosegur.Genesis.LogicaNegocio.AccionGenesisLogin;
//using Prosegur.Genesis.LogicaNegocio;

namespace Prosegur.Genesis.Test
{
    [TestClass]
    public class UnitTestDelegacion
    {
        /// <summary>
        ///Escenario 00: Escenario que siempre da OK.
        /// </summary>s
        [TestMethod]
        public void TestFakeSiempreEsVerdadero()
        {
            //Arrage
            bool resultadoEsperado;
            resultadoEsperado = true;

            //ACT
            bool respuesta = true;
            respuesta = (1 > 0); //Siempre va a ser verdadero

            //Assert
            Assert.AreEqual(resultadoEsperado, respuesta);
        }


        /// <summary>
        ///Escenario 01: TestEncontrarDelegaciones
        /// </summary>s
        [TestMethod]
        public void TestObtenerDelegacionesDESA()
        {

            //Arrage
            bool resultadoEsperado;
            resultadoEsperado = true;
            bool resultadoActual;


            //ACT
            var peticion = new ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion();
            peticion.CodigosDelegaciones = new List<string>();
            peticion.CodigosDelegaciones.Add("UY001");
            peticion.CodigosDelegaciones.Add("UY002");
            peticion.CodigosDelegaciones.Add("UY003");
            peticion.CodigosDelegaciones.Add("1");
            peticion.CodigosDelegaciones.Add("2");
            peticion.CodigosDelegaciones.Add("3");
            var objRespuesta = new ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta();


            objRespuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Delegacion.ObtenerDelegaciones(peticion);

            resultadoActual = (objRespuesta.Delegaciones.Count > 0);

            //ASSERT

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }
    }
}
