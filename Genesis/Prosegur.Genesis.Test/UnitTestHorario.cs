using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prosegur.Genesis.Test
{
    [TestClass]
    public class UnitTestHorario
    {
        [TestMethod]
        public void TestFormatoHorario24HH()
        {
            DateTime horaActual1 = new DateTime(2020, 11, 30, 16, 35, 0);
            int horarioActual1 = Int16.Parse(horaActual1.ToString("HHmm"));

            DateTime horaActual2 = new DateTime(2020, 11, 30, 12, 00, 0);
            int horarioActual2 = Int16.Parse(horaActual2.ToString("HHmm"));

            DateTime horaActual3 = new DateTime(2020, 11, 30, 11, 59, 16);
            int horarioActual3 = Int16.Parse(horaActual3.ToString("HHmm"));

            DateTime horaActual4 = new DateTime(2020, 11, 30, 00, 00, 00);
            int horarioActual4 = Int16.Parse(horaActual4.ToString("HHmm"));

            Assert.IsTrue(horarioActual1.Equals(1635));
            Assert.IsTrue(horarioActual2.Equals(1200));
            Assert.IsTrue(horarioActual3.Equals(1159));
            Assert.IsTrue(horarioActual4.Equals(0));
        }
    }
}

