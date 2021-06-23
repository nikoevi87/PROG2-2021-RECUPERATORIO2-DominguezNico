﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Usuario
    {
        public string NombreApellido { get; set; }
        public string Documento { get; set; }
        public double Saldo { get; set; }
        public List<Movimiento> HistoricoMov { get; set; }


        public Usuario()
        {
            HistoricoMov = new List<Movimiento>();
        }






    }
}
