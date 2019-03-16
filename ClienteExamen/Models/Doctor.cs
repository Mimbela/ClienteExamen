using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteExamen.Models
{
    public class Doctor
    {
        public int Doctor_Numero { get; set; }


        public string Apellido { get; set; }


        public string Especialidad { get; set; }


        public int Salario { get; set; }


        public int Hospital_cod { get; set; }
    }
}
