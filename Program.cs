using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCursos
{
    internal class Program
    {
        public interface IAlumno
        {
            void MostrarInformacion();
            void ObtenerPromedio();
        }

        public interface ICurso
        {
            void AgregarAlumno();
            void EliminarAlumno();
            void CalcularPromedioDelCurso();
        }
        static void Main(string[] args)
        {




        }
    }
}
