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
            // Ordenar Alumnos
            // Filtrar Alumnos
            // Cargar estudiantes por JSON
            // Guardar estudiantes por JSON

            //Interfaz 2 (CURSOS)
            // Agregar Curso.
            // Buscar Curso.
            // Eliminar curso.
            // Mostrar todos los cursos.
            // Cargar Cursos por JSON
            // Guardar Cursos por JSON

            //Interfaz 2: (Dentro de un curso)
            // Cambiar profesor.
            // Ver alumnos del curso.
            // Inscribir alumno al curso.
            // Eliminar alumno del curso.
            // Obtener promedio de alumno.
            // Asignar promedio a alumno.
            // Obtener promedio de todos los alumnos.

            GestionAlumnos<Alumno> Alumnos = new GestionAlumnos<Alumno>();
            GestionCursos<Curso<Alumno>, Alumno> Cursos = new GestionCursos<Curso<Alumno>, Alumno>();

            Interfaz.Inicio(Alumnos, Cursos);

        }
    }
}
