using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCursos
{
    internal class GestionCursos<TCurso, TAlumno>
        where TCurso : Curso<Alumno>
        where TAlumno : Alumno
    {
        public List<TCurso> cursos { get; private set; } = new List<TCurso>();

        public void AgregarCurso(TCurso curso)
        {
            cursos.Add(curso);
        }

        public void EliminarCurso(TCurso curso)
        {
            cursos.Remove(curso);
        }

        public void InscribirAlumno(TCurso curso, TAlumno alumno)
        {
            curso.AgregarAlumno(alumno);
        }

        public void RemoverAlumno(TCurso curso, TAlumno alumno)
        {
            curso.EliminarAlumno(alumno);
        }


        //Metodo para filtrar los alumnos segun el criterio deceado.
        //El metodo recibe el curso y filtra los alumnos de ese curso segun el criterio
        //del predicate recibido.
        //
        //Se puede hacer de forma mas sencilla:
        //
        //public List<Alumno> FiltrarAlumnos(TCurso curso, Predicate<Alumno> criterio)
        //{
        //      return curso.ListaAlumnos.FindAll(criterio).ToList();
        //}
        //
        //Por cuestiones de que el ejercicio solicita crear un Acction<> en este caso
        //Tendre que usar el metodo de esta forma para cumplir con el mandato del ejercicio:
        //
        public void FiltrarAlumnos(TCurso curso, Predicate<Alumno> criterio)
        {
            Action<Alumno> Accion = x => x.MostrarInformacion();

            List<Alumno> lista = curso.ListaAlumnos.FindAll(criterio).ToList();

            lista.ForEach(x => Accion(x));
        }

        //Metodo para ordenar alumnos en un curso. Se utiliza el metodo "ActualizarAlumnos"
        //dado que la lista no es accesible fuera de la clase principal.
        //El metodo recibe el curso y la funccion requerida. Luego invoca el metodo
        //dentro del curso y pasa como parametro la lista ordenada con el parametro
        //recibido.
        public void OrdenarAlumnos(TCurso curso, Func<Alumno, object> funcion)
        {
            curso.ActualizarLista(curso.ListaAlumnos.OrderBy(x => funcion(x)).ToList());
        }


    }
}
