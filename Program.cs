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
            //Interfaz 1 (ALUMNOS):
            // Agregar alumno.
            // Buscar Alumno.
            // Eliminar alumno.
            // Mostrar todos los alumnos.

            //Interfaz 2 (CURSOS)
            // Agregar Curso.
            // Buscar Curso.
            // Eliminar curso.
            // Mostrar todos los cursos.
            
            //Interfaz 2: (Dentro de un curso)
            // Ver alumnos del curso.
            // Inscribir alumno al curso.
            // Eliminar alumno del curso.
            // Obtener promedio de alumnos.
            // Asignar promedio a alumno.
            // Obtener promedio de todos los alumnos.


        }
    }
}
