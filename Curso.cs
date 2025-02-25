using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.Program;

namespace GestionCursos
{
    internal class Curso<T> : ICurso<T> where T : Alumno
    {
        public string Profesor { get; private set; }
        public string Nombre { get; private set; }
        public List<T> ListaAlumnos { get; private set; } = new List<T>();

        public Curso(string nombre, string profesor)
        {
            Nombre = nombre;
            Profesor = profesor;
        }

        public void AgregarAlumno(T alumno)
        {
            ListaAlumnos.Add(alumno);
        }

        public void EliminarAlumno(T alumno)
        {
            ListaAlumnos.Remove(alumno);
        }

        public double CalcularPromedioDelCurso()
        {
            return ListaAlumnos.Sum(x => x.ObtenerPromedio());
        }




    }
}
