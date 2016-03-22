﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gabriel.Cat.Xarxa
{
    /// <summary>
    /// Servicio free de www.getipintel.net
    /// </summary>
    public class GetIpIntelServicioValidacionIP : ServicioValidacionIP
    {

        public override bool ValidaIp(string ipAComprobar)
        {
            //esta web mira proxy,vpn,red TOR y bad ip detection
            const char NOUSAPROXYETC = '1';
            string pathWebConIp = "http://check.getipintel.net/check.php?ip=" + ipAComprobar;
            LanzarMensaje("Se usara la web '{0}' para validar la ip", pathWebConIp);
            System.Net.Http.HttpClient cliente = new System.Net.Http.HttpClient();
            Task<System.Net.Http.HttpResponseMessage> respuesta = cliente.GetAsync(pathWebConIp);
            Task<string> datosRespuesta;
            string respuestaString;
            respuesta.Wait();
            datosRespuesta = respuesta.Result.Content.ReadAsStringAsync();//metodo para mirarlo online :)
            datosRespuesta.Wait();
            respuestaString = datosRespuesta.Result;
            LanzarMensaje("La respuesta de la web '{0}' para la ip {1}", respuestaString, ipAComprobar);
            return respuestaString.Contains(NOUSAPROXYETC);
        }
    }
}
