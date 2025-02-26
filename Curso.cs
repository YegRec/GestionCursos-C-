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

        //Usaremos este metodo para controlar cuando se requiera modificar la lista
        //de alumnos, dado que la lista principal es privada no es accesible fuera de esta clase
        //con este metodo podremos modificar la lista asignando otra lista.
        //Este metodo sera utilizado para ordenar la lista de alumnos, recibiendo como argumento
        //la lista ordenada y asignandola como principal.
        public void ActualizarLista(List<T> NuevaLista)
        {
            ListaAlumnos = NuevaLista.ToList();
        }




    }
}
