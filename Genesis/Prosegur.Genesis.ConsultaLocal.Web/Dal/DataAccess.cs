//using Prosegur.Genesis.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Concepto.Models;
using Prosegur.Genesis.ConsultaLocal.AccesoDatos;

namespace Concepto.Dal
{
    public static class DataAccess
    {




        public static List<AccessLog> ObtenerLog()
        {
            List<AccessLog> logs = new List<AccessLog>();


            for (int i = 0; i <= 20; i++)
            {
                AccessLog accessLog = new AccessLog { accesoOtorgado = true, login = "GSOSA" + i, modulo = "ATM" + i };

                logs.Add(accessLog);
            }


            return logs;
        }


        public static DataSet GetData(string action)
        {
            DataSet result = ModeloDD.ProcesarAccion(action);

            return result;
        }

        public static DataSet GetData(Dictionary<string, string> acciones)
        {
            DataSet result = ModeloDD.ProcesarAccion(acciones);

            return result;
        }
    }
}