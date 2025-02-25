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


    }
}
