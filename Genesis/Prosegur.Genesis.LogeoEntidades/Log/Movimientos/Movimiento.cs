using System;
using System.Collections.Generic;

namespace Prosegur.Genesis.LogeoEntidades.Log.Movimiento
{
    public class Movimiento
    {
        public string Identificador { get; set; }
        public string CodigoPais { get; set; }
        public string ActualID { get; set; }
        public DateTime FechaHoraPrimeraLlamada { get; set; }
        public DateTime FechaHoraUltimaLlamada { get; set; }
        public List<Llamada> Llamadas { get; set; }
        public Movimiento() 
        {
            Llamadas = new List<Llamada>();
        }
    }
}
