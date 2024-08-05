using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Prosegur.Genesis.Utilidad;
using Prosegur.Genesis.Comunicacion.ProxyWS.WebApi;

using Newtonsoft.Json;

namespace Prosegur.Genesis.Mail
{
    public static class MailUtil
    {
        #region Properties

        //ClientCredentialParameters
        private static string _MailSendgridTempladeId;

        private static string _MailSendgridAPIKey;

        private static string _MailAutenticacionClientId;

        private static string _MailAutenticacionURL;

        private static string _MailAutenticacionScope;

        private static string _MailURLEnvio;

        private static string _MailAutenticacionClientSecret;
        #endregion

        #region Procedures

        public static void SendMail(string _asunto, string _cuerpo, string _destinatarios, string codigoPais)
        {
            string[] _listaDestinatarios =new string[] { };
            if (_destinatarios.Contains(",")) _listaDestinatarios = _destinatarios.Split(',');
            if (_destinatarios.Contains(";")) _listaDestinatarios = _destinatarios.Split(';');
            string identificadorLlamada = String.Empty;

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "EnviarCorreos", ref identificadorLlamada);

                try
                {
                    HttpClient clientTest = new HttpClient();
                    HttpUtil http = new HttpUtil();

                    string[] strCategories = new string[1];
                    strCategories[0] = "GENESIS-PRODUCTO";
                    object[] _to = new object[_listaDestinatarios.Length];
                    var numDesti = 0;

                    foreach(var des in _listaDestinatarios)
                    {
                        _to[numDesti] = new { email = des };
                        numDesti += 1;
                    };

                    GetparametrosEnvioCorreo(identificadorLlamada);
                    object _personalizations = new { to=_to, dynamic_template_data = $"{{\"body\": \"{_cuerpo}\",\"subject\": \"{_asunto}\"}}" };
                    object[] _arrayPer = new object[] { _personalizations };
                    jsonBody _body = new jsonBody()
                    {
                        sendgrid_api_key = _MailSendgridAPIKey,
                        personalizations = _arrayPer,
                        from = new
                        {
                            email = "no-reply-genesis-producto@prosegurcash.com",
                            name = "Génesis Producto"
                        },                       
                        template_id = _MailSendgridTempladeId,
                        categories = strCategories
                    };

                    if (!string.IsNullOrWhiteSpace(identificadorLlamada))
                    {
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "EnviarCorreos", Comon.Util.VersionCompleta.ToString(), JsonConvert.SerializeObject(_body), codigoPais, JsonConvert.SerializeObject(_body).GetHashCode().ToString());
                    }

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                  "Prosegur.Genesis.Mail.MailUtil.SendMail",
                                                  Comon.Util.VersionCompleta.ToString(),
                                                  $"llamada GetToken", "");

                    var _token = GetToken(identificadorLlamada);

                    if (_MailURLEnvio != null && !string.IsNullOrWhiteSpace(_token))
                    {
                        var headers = new Dictionary<string, string>();
                        headers.Add("AUTHORIZATION", String.Format("Bearer {0}", _token));

                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.Mail.MailUtil.SendMail",
                                                        Comon.Util.VersionCompleta.ToString(),
                                                        $"Inicia llamada a la API de sengrid", "");

                        var respuesta = http.PostWithHeaders<ContractoServicio.Contractos.Notification.DeliveredMessages.Response>(identificadorLlamada, _MailURLEnvio, _body, headers);

                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.Mail.MailUtil.SendMail",
                                                        Comon.Util.VersionCompleta.ToString(),
                                                        $"Finaliza llamada a la API de sengrid", "");

                            Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, JsonConvert.SerializeObject(respuesta), respuesta.StatusCode, respuesta.ReasonPhrase, JsonConvert.SerializeObject(respuesta).GetHashCode().ToString());

                    }
                }
                catch (Exception ex)
                {
                    Logeo.Log.Movimiento.Logger.AgregaDetalle("",
                                                  "Prosegur.Genesis.Mail.MailUtil.SendMail",
                                                  Comon.Util.VersionCompleta.ToString(),
                                                  $"Excepcion: {ex.Message} InnerException: {ex.InnerException}", "");
                }
        }
        // configurar url en parámetros

        public class jsonBody
        {
            public string sendgrid_api_key { get; set; }
            public object[] personalizations { get; set; }
            public object from { get; set; }
            public string template_id { get; set; }
            public string[] categories { get; set; }
        }

        private static string GetToken(string identificadorLlamada)
        {
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                          "Prosegur.Genesis.Mail.MailUtil.GetToken",
                                          Comon.Util.VersionCompleta.ToString(),
                                          $"Comienza obtención de parametros autenticacion token", "");

            var parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridTempladeId");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailSendgridTempladeId = null;
                }
            }
            else
            {
                _MailSendgridTempladeId = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridAPIKey");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailSendgridAPIKey = null;
                }
            }
            else
            {
                _MailSendgridAPIKey = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailAutenticacionClientId");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailAutenticacionClientId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailAutenticacionClientId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailAutenticacionClientId = null;
                }
            }
            else
            {
                _MailAutenticacionClientId = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailAutenticacionURL");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailAutenticacionURL = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailAutenticacionURL = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailAutenticacionURL = null;
                }
            }
            else
            {
                _MailAutenticacionURL = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailAutenticacionScope");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailAutenticacionScope = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailAutenticacionScope = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailAutenticacionScope = null;
                }
            }
            else
            {
                _MailAutenticacionScope = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridAPIKey");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailSendgridAPIKey = null;
                }
            }
            else
            {
                _MailSendgridAPIKey = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailURLEnvio");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailURLEnvio = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailURLEnvio = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailURLEnvio = null;
                }
            }
            else
            {
                _MailURLEnvio = null;
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailAutenticacionClientSecret");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailAutenticacionClientSecret = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailAutenticacionClientSecret = parametroAux.ElementAt(0).Valores.ElementAt(0);
                    else
                        _MailAutenticacionClientSecret = null;
                }
            }
            else
            {
                _MailAutenticacionClientSecret = null;
            }

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                          "Prosegur.Genesis.Mail.MailUtil.GetToken",
                                          Comon.Util.VersionCompleta.ToString(),
                                          $"Finaliza obtención de parametros autenticacion token", "");

            var parametros = new Dictionary<string, string>  {
                { Comon.Constantes.CONST_CLIENT_ID, _MailAutenticacionClientId},
                { Comon.Constantes.CONST_CLIENT_SECRET, _MailAutenticacionClientSecret},
                { Comon.Constantes.CONST_SCOPE, _MailAutenticacionScope},
                { Comon.Constantes.CONST_GRANT_TYPE, Comon.Constantes.CONST_CLIENT_CREDENTIALS}
            };

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                        "Prosegur.Genesis.Mail.MailUtil.GetToken",
                                        Comon.Util.VersionCompleta.ToString(),
                                        $"Se obtiene parámetros de autenticación: URLAutenticacion: {_MailAutenticacionURL}, parametros: {String.Join(Environment.NewLine, parametros)} ", "");

            //Validar parámetros para obtener token
            if (_MailAutenticacionClientId != null && _MailAutenticacionClientSecret != null && _MailAutenticacionScope != null && _MailAutenticacionURL !=null)
            {
                return TokensModule.BuscarTokenBearerConClientCredencial("Mail", _MailAutenticacionURL, parametros, identificadorLlamada, "Prosegur.Genesis.Mail.GetToken");
            }
            else
            {
                return "";
            }
        }

        private static void GetparametrosEnvioCorreo(string identificadorLlamada)
        {
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                          "Prosegur.Genesis.Mail.MailUtil.GetparametrosEnvioCorreo",
                                          Comon.Util.VersionCompleta.ToString(),
                                          $"Comienza obtención de parametros envio correo", "");

            var parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridTempladeId");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0);
                }
            }

            parametroAux = AccesoDatos.Genesis.Parametros.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS,  "MailSendgridAPIKey");
            if (parametroAux != null && parametroAux.Count > 0)
            {
                if (!parametroAux.ElementAt(0).MultiValue && parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                    _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                else
                {
                    if (parametroAux.ElementAt(0).Valores.ElementAt(0) != null)
                        _MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0);
                }
            }


            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                          "Prosegur.Genesis.Mail.MailUtil.GetparametrosEnvioCorreo",
                                          Comon.Util.VersionCompleta.ToString(),
                                          $"Finaliza obtención de parametros envio correo", "");
        }
        #endregion
    }
}
