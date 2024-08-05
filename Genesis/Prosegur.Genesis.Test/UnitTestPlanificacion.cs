using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prosegur.Genesis.Test
{
    [TestClass]
    public class AccesoADatosPlanificacionUnitTest
    {

        /// <summary>
        /// Test: un plan con programacion domingos y jueves. Parados un miercoles
        /// Assert: Tiene que devolver la fecha fin de la programación del jueves
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaA()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 7; //Domingo es el número 7
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 23, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 7
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 16, 31, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);

            int horarioActual = 1100;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 3;
            int numeroDeDia = proximoDiaConPlanificacion;



            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BusquedaHorarioFinPeriodoActualIntensiva(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio,ref  horarioFinal, numeroDeDia, ref proximoDiaConPlanificacion);

            Assert.IsTrue(horaFin.Value.Hour.Equals(16), "Se esperaba el horario 16 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(31), "Se esperaba el minuto 31 y el valor es: " + horaFin.Value.Minute.ToString());
        }


        /// <summary>
        /// Test: un plan con programacion domingos y jueves. Parados un domingo.
        /// Assert: Tiene que devolver la fecha fin de la programación del jueves
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaB()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 7; //Domingo es el número 7
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 23, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 4
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 16, 31, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);

            int horarioActual = 1100;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 7;
            int numeroDeDia = proximoDiaConPlanificacion;


            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BusquedaHorarioFinPeriodoActualIntensiva(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio, ref horarioFinal, numeroDeDia, ref proximoDiaConPlanificacion);


            Assert.IsTrue(horaFin.Value.Hour.Equals(16), "Se esperaba el horario 16 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(31), "Se esperaba el minuto 31 y el valor es: " + horaFin.Value.Minute.ToString());
        }


        /// <summary>
        /// Test: un plan con programacion domingos y jueves. Parados un sábado.
        /// Assert: Tiene que devolver la fecha fin de la programación del domingo
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaC()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 7; //Domingo es el número 7
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 23, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 4
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 16, 31, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);

            int horarioActual = 1100;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 6; //Sábado es el número 6
            int numeroDeDia = proximoDiaConPlanificacion;


            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BusquedaHorarioFinPeriodoActualIntensiva(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio, ref horarioFinal, numeroDeDia, ref proximoDiaConPlanificacion);


            Assert.IsTrue(horaFin.Value.Hour.Equals(23), "Se esperaba el horario 23 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(0), "Se esperaba el minuto 0 y el valor es: " + horaFin.Value.Minute.ToString());
        }


        /// <summary>
        /// Test: un plan con programacion lunes y jueves. Parados un viernes
        /// Assert: Tiene que devolver la fecha fin de la programación del lunes
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaD()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 1; //Lunes = 1
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 10, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 4
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 11, 00, 0);

            Comon.Clases.PlanXProgramacion plan3 = new Comon.Clases.PlanXProgramacion();
            plan3.NecDiaFin = 4; //Jueves es el número 4
            plan3.FechaHoraFin = new DateTime(1, 1, 1, 17, 00, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);
            objPlanificacion.Programacion.Add(plan3);

            int horarioActual = 1630;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 5; //Viernes = 5
            int numeroDeDia = proximoDiaConPlanificacion;


            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BusquedaHorarioFinPeriodoActualIntensiva(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio, ref horarioFinal, numeroDeDia, ref proximoDiaConPlanificacion);


            Assert.IsTrue(horaFin.Value.Hour.Equals(10), "Se esperaba el horario 10 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(0), "Se esperaba el minuto 0 y el valor es: " + horaFin.Value.Minute.ToString());
        }

        /// <summary>
        /// Test: un plan con programacion lunes y jueves. Parados un miércoles
        /// Assert: Tiene que devolver la fecha fin de de la primera del jueves (11:00)
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaE()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 1; //Lunes = 1
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 10, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 4
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 11, 00, 0);

            Comon.Clases.PlanXProgramacion plan3 = new Comon.Clases.PlanXProgramacion();
            plan3.NecDiaFin = 4; //Jueves es el número 4
            plan3.FechaHoraFin = new DateTime(1, 1, 1, 17, 00, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);
            objPlanificacion.Programacion.Add(plan3);

            int horarioActual = 1630;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 3; //Viernes = 5
            int numeroDeDia = proximoDiaConPlanificacion;


            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BusquedaHorarioFinPeriodoActualIntensiva(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio, ref horarioFinal, numeroDeDia, ref proximoDiaConPlanificacion);


            Assert.IsTrue(horaFin.Value.Hour.Equals(11), "Se esperaba el horario 11 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(0), "Se esperaba el minuto 0 y el valor es: " + horaFin.Value.Minute.ToString());
        }


        /// <summary>
        /// Test: un plan con programacion lunes y jueves. Parados un jueves a las 12:00
        /// Assert: Tiene que devolver la fecha fin de de la segunda del jueves (17:00)
        /// Comentario: Se considera que no encontró programación que se ajuste al mismo día.
        /// </summary>
        [TestMethod]
        public void TestBusquedaHorarioFinPeriodoActualIntensivaF()
        {
            var objPlanificacion = new Comon.Clases.Planificacion();
            objPlanificacion.Programacion = new System.Collections.Generic.List<Comon.Clases.PlanXProgramacion>();

            Comon.Clases.PlanXProgramacion plan1 = new Comon.Clases.PlanXProgramacion();
            plan1.NecDiaFin = 1; //Lunes = 1
            plan1.FechaHoraFin = new DateTime(1, 1, 1, 10, 0, 0);


            Comon.Clases.PlanXProgramacion plan2 = new Comon.Clases.PlanXProgramacion();
            plan2.NecDiaFin = 4; //Jueves es el número 4
            plan2.FechaHoraFin = new DateTime(1, 1, 1, 11, 00, 0);

            Comon.Clases.PlanXProgramacion plan3 = new Comon.Clases.PlanXProgramacion();
            plan3.NecDiaFin = 4; //Jueves es el número 4
            plan3.FechaHoraFin = new DateTime(1, 1, 1, 17, 00, 0);

            objPlanificacion.Programacion.Add(plan1);
            objPlanificacion.Programacion.Add(plan2);
            objPlanificacion.Programacion.Add(plan3);

            int horarioActual = 1200;
            DateTime? horaFin = null;
            int horarioInicio = 0, horarioFinal = 0;
            int proximoDiaConPlanificacion = 4; //Jueves = 4
            int numeroDeDia = proximoDiaConPlanificacion;


            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.BuscarHorarioFinPeriodoActual(objPlanificacion, horarioActual, ref horaFin, ref horarioInicio, ref horarioFinal, numeroDeDia, true);


            Assert.IsTrue(horaFin.Value.Hour.Equals(17), "Se esperaba el horario 17 y el valor es: " + horaFin.Value.Hour.ToString());
            Assert.IsTrue(horaFin.Value.Minute.Equals(0), "Se esperaba el horario 0 y el valor es: " + horaFin.Value.Minute.ToString());
        }
    }
}
