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
        // GET: api/Movimiento
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movimiento/5
        public IHttpActionResult Get(string documento)
        {

            GetLista respuesta = Ap.ListadoMovimientos(documento);
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
            if (respuesta.Resultado == 201)
            {
                return Created("", Ap.ObtenerMovimientoPorId(respuesta.Movimiento));
            }
            return Content(HttpStatusCode.BadRequest,respuesta);



        }

        // PUT: api/Movimiento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movimiento/5
        public IHttpActionResult Delete(string id)
        {
            Respuesta respuesta = Ap.CancelarMovimiento(id);
            if (respuesta.Resultado == 200)
            {
                return Ok(respuesta);
            }
            return Content(HttpStatusCode.BadRequest,respuesta);
        }
    }
}
