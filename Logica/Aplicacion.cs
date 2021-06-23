using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Aplicacion
    {
        public List<Movimiento> Movimientos { get; set; }
        public List<Usuario> Usuarios { get; set; }




        public Aplicacion()
        {
            Movimientos = new List<Movimiento>();
            Usuarios = new List<Usuario>();

        }

        public Respuesta CrearMovimiento(PostMovimiento value)
        {
            Respuesta respuesta = new Respuesta();
            Usuario receptor = Usuarios.Find(x=>x.Documento==value.DocReceptor);
            Usuario emisor = Usuarios.Find(x => x.Documento == value.DocEmisor);
            if (receptor != null && emisor!= null)
            {
                if (emisor.Saldo>=value.Monto)
                {
                    Movimiento nuevoMovimiento = new Movimiento()
                    {
                        Emisor = emisor,
                        Receptor = receptor,
                        Numero = Convert.ToString(Movimientos.Count + 1),
                        Descripcion = value.Descripcion,
                        Dia = DateTime.Today,
                        Monto = value.Monto
                    };
                    emisor.Saldo -= value.Monto;
                    receptor.Saldo += value.Monto;
                    respuesta.Resultado = 201;
                    emisor.HistoricoMov.Add(nuevoMovimiento);
                    receptor.HistoricoMov.Add(nuevoMovimiento);
                    Movimientos.Add(nuevoMovimiento);
                    respuesta.Movimiento = nuevoMovimiento.Numero;
                    return respuesta;

                }
                respuesta.Resultado = 400;
                respuesta.Descripcion = "Monto insuficiente";
                return respuesta;
                
            }
            respuesta.Resultado = 400;
            respuesta.Descripcion = "DNI invalido";
            return respuesta;

        }

        public GetLista ListadoMovimientos(string documento)
        {
            GetLista resp = new GetLista();
            Usuario persona = Usuarios.Find(x=>x.Documento==documento);
            if (persona != null)
            {
                List<Movimiento> ListadoUsuario = new List<Movimiento>();
                foreach (var item in Movimientos)
                {
                    if (item.Emisor == persona || item.Receptor == persona)
                    {
                        ListadoUsuario.Add(item);
                    }
                }
                ListadoUsuario.OrderByDescending(x=>x.Dia);
                resp.Listado = ListadoUsuario;
                resp.Resultado = 200;
                resp.Descripcion = "Listado del usuario DNI: " + documento;
                

            }
            resp.Descripcion = "El usuario no existe";
            resp.Resultado = 404;
            return resp;

            
        }

        public Respuesta CancelarMovimiento(string id)
        {
            Respuesta resp = new Respuesta();
            Movimiento movEncontrado = Movimientos.Find(x=>x.Numero==id);
            if (movEncontrado!= null)
            {
                Movimiento nuevoMovimiento = new Movimiento();
                movEncontrado.Emisor.Saldo += movEncontrado.Monto;
                movEncontrado.Receptor.Saldo -= movEncontrado.Monto;
                nuevoMovimiento.Descripcion = "Cancelacion del movimiento : " + id;
                nuevoMovimiento.Dia = DateTime.Today;
                nuevoMovimiento.Monto = movEncontrado.Monto;
                nuevoMovimiento.Numero = Convert.ToString(Movimientos.Count + 1);
                Movimientos.Add(nuevoMovimiento);
                movEncontrado.Emisor.HistoricoMov.Add(nuevoMovimiento);
                movEncontrado.Receptor.HistoricoMov.Add(nuevoMovimiento);
                resp.Movimiento = nuevoMovimiento.Numero;
                resp.Resultado = 200;
                resp.Descripcion = nuevoMovimiento.Descripcion;
                return resp;
                

            }
            resp.Resultado = 400;
            resp.Descripcion = "No se ha encontrado el movimiento especificado";
            return resp;

        }

        public object ObtenerMovimientoPorId(string movimiento)
        {
            return Movimientos.Find(x=>x.Numero==movimiento);
        }
    }
}
