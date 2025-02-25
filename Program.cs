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
            double ObtenerPromedio();
        }

        public interface ICurso<T>
        {
            void AgregarAlumno(T alumno);
            void EliminarAlumno(T alumno);
            double CalcularPromedioDelCurso();
        }
        static void Main(string[] args)
        {




        }
    }
}
