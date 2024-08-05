using System;
using System.Collections.Generic;

namespace Prosegur.Genesis.LogeoEntidades.Log.Movimiento
{
    public class Llamada
    {
        public string Identificador { get; set; }
        public string Recurso { get; set; }
        public string Version { get; set; }
        public string DatosEntrada { get; set; }
        public string DatosSalida { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string CodigoResultado { get; set; }
        public string DescripcionResultado { get; set; }
        public string CodigoPais { get; set; }
        public string HashEntrada { get; set; }
        public string HashSalida { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public List<DetalleLlamada> Detalles { get; set; }
        public Llamada()
        {
            Detalles = new List<DetalleLlamada>();
        }
    }
}