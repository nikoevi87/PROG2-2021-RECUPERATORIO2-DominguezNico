using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica;

namespace RecuperatorioNicoD.Controllers
{
    public class MovimientoController : ApiController
    {
        Aplicacion Ap = new Aplicacion();
        // INNECESARIO
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movimiento/5
        public IHttpActionResult Get(string documento)
        {

            GetLista respuesta = Ap.ListadoMovimientos(documento);
            //PROBLEMA DE DISEÑO. LA LOGICA DE NEGOCIO NO DEBE CONOCER STATUS CODES. ESO ES PARTE DE LA CAPA DE SERVICIOS
            if (respuesta.Resultado == 200)
            {
                return Ok(respuesta.Listado);
            }

            return Content(HttpStatusCode.BadRequest, respuesta);
        }

        // POST: api/Movimiento
        public IHttpActionResult Post([FromBody]PostMovimiento value)
        {
            Respuesta respuesta = Ap.CrearMovimiento(value);
            //PROBLEMA DE DISEÑO. LA LOGICA DE NEGOCIO NO DEBE CONOCER STATUS CODES. ESO ES PARTE DE LA CAPA DE SERVICIOS

            if (respuesta.Resultado == 201)
            {
                return Created("", Ap.ObtenerMovimientoPorId(respuesta.Movimiento));
            }
            return Content(HttpStatusCode.BadRequest,respuesta);



        }

        // INNECESARIO
        public void Put(int id, [FromBody]string value)
        {
        }

        
        public IHttpActionResult Delete(string id)
        {
            Respuesta respuesta = Ap.CancelarMovimiento(id);
            //PROBLEMA DE DISEÑO. LA LOGICA DE NEGOCIO NO DEBE CONOCER STATUS CODES. ESO ES PARTE DE LA CAPA DE SERVICIOS
            if (respuesta.Resultado == 200)
            {
                return Ok(respuesta);
            }
            return Content(HttpStatusCode.BadRequest,respuesta);
        }
    }
}
