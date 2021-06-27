using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logica;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CrearMovimiento_PostMovimiento_Valido()
        {
            Aplicacion ap = new Aplicacion();
            
            PostMovimiento servicio = new PostMovimiento();
            Respuesta exitosa = new Respuesta { Descripcion = "Exito", Movimiento = "1", Resultado = 201 };
            Respuesta response = new Respuesta();
            
            ap.Usuarios.Add(new Usuario {Documento="1",Saldo=200 });
            ap.Usuarios.Add(new Usuario { Documento = "4", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "3", Saldo = 200 });
            ap.Usuarios.Add(new Usuario {Documento="2" });

            servicio.DocEmisor = Convert.ToString(1);
            servicio.DocReceptor = Convert.ToString(2);
            servicio.Descripcion = "";
            servicio.Monto = 100;


            response = ap.CrearMovimiento(servicio);

            //PODRIA AGREGAR VALIDACION DE SALDOS, CONFIRMAR QUE LOS DOS USUARIOS QUEDAN CON EL SALDO CORRECTO
            Assert.AreEqual(response.Movimiento, exitosa.Movimiento );
            Assert.AreEqual(response.Resultado,exitosa.Resultado);

        }
        [TestMethod]
        public void CrearMovimiento_PostMovimiento_DniInvalido()
        {
            Aplicacion ap = new Aplicacion();

            PostMovimiento servicio = new PostMovimiento();
            Respuesta exitosa = new Respuesta { Descripcion = "Exito", Movimiento = "1", Resultado = 400 };
            Respuesta response = new Respuesta();

            ap.Usuarios.Add(new Usuario { Documento = "1", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "4", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "3", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "2" });

            servicio.DocEmisor = Convert.ToString(8);
            servicio.DocReceptor = Convert.ToString(2);
            servicio.Descripcion = "";
            servicio.Monto = 100;


            response = ap.CrearMovimiento(servicio);


            Assert.AreEqual(response.Descripcion, "DNI invalido");
            Assert.AreEqual(response.Resultado, exitosa.Resultado);

        }
        [TestMethod]
        public void CrearMovimiento_PostMovimiento_MontoInsuf()
        {
            Aplicacion ap = new Aplicacion();

            PostMovimiento servicio = new PostMovimiento();
            Respuesta exitosa = new Respuesta { Descripcion = "Exito", Movimiento = "1", Resultado = 400 };
            Respuesta response = new Respuesta();

            ap.Usuarios.Add(new Usuario { Documento = "1", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "4", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "3", Saldo = 200 });
            ap.Usuarios.Add(new Usuario { Documento = "2" });

            servicio.DocEmisor = Convert.ToString(4);
            servicio.DocReceptor = Convert.ToString(2);
            servicio.Descripcion = "";
            servicio.Monto = 300;


            response = ap.CrearMovimiento(servicio);


            Assert.AreEqual(response.Descripcion, "Monto insuficiente");
            Assert.AreEqual(response.Resultado, exitosa.Resultado);

        }
    }
}
