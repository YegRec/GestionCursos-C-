using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.Alumno;

namespace GestionCursos
{
    internal class GestionAlumnos<T> where T : Alumno
    {
        
        public List<T> alumnos {  get; private set; } = new List<T>();

        public void AgregarAlumno(T alumno)
        {
            alumnos.Add(alumno);
        }
        




    }
}
