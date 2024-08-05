using Concepto.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Concepto.Models;
using System.Data;
using Newtonsoft.Json;
using Prosegur.Genesis.ConsultaLocal.Web;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace Concepto.Api
{
    public class LogApiController : System.Web.Http.ApiController
    {

        // GET: api/LogApi
        [System.Web.Http.HttpGet]
        public IEnumerable<AccessLog> ObtenerLogs()
        {
            var logs = DataAccess.ObtenerLog();

            return logs;
        }

        // GET: api/EjecutarAccion
        [System.Web.Http.HttpGet]
        public String EjecutarAccion(string action)
        {
            var result = DataAccess.GetData(action);

            var result2 = JsonConvert.SerializeObject(result);

            return result2;
        }

        [System.Web.Http.HttpPost]
        public Task<string> EjecutarAccion(Dictionary<string, string> actions)
        {
            string result2;
            try
            {
                var result = DataAccess.GetData(actions);
                result2 = JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                string a = e.Message;
                return Task.FromResult(a);
            }



            return Task.FromResult(result2);

           
          //var result = DataAccess.GetData(actions);
          // var result2 = JsonConvert.SerializeObject(result);        
       

           // return result2;
        }


        [HttpGet]
        public string GetuserControlHtml()
        {
            return RenderUserControl("namespace", "controlName");
        }

        private string RenderUserControl(string assembly,
             string controlName)
        {
            FormLessPage pageHolder =
                      new FormLessPage() { AppRelativeTemplateSourceDirectory = HttpRuntime.AppDomainAppVirtualPath }; //allow for "~/" paths to resolve

            dynamic control = null;

            //assembly = "Com.YourNameSpace.UI"; //example
            //controlName = "YourCustomControl"
            string fullyQaulifiedAssemblyPath = string.Format("{0}.{1},{0}", assembly, controlName);

            Type type = Type.GetType(fullyQaulifiedAssemblyPath);
            if (type != null)
            {
                control = pageHolder.LoadControl(type, null);
                control.Bla1 = "test"; //bypass compile time checks on property setters if needed
                control.Blas2 = true;

            }

            pageHolder.Controls.Add(control);
            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, output, false);
            return output.ToString();
        }

    }
}
