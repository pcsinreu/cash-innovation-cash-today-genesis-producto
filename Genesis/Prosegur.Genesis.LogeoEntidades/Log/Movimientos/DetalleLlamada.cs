using System;

namespace Prosegur.Genesis.LogeoEntidades.Log.Movimiento
{
    public class DetalleLlamada
    {
        public string Origen { get; set; }
        public string Version { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaHora { get; set; }
        public string CodigoIdentificador { get; set; }
    }
}