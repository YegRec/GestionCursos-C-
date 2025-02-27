using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using static GestionCursos.Alumno;

namespace GestionCursos
{
    internal class GestionAlumnos<T> where T : Alumno
    {
        [JsonInclude]
        public List<Alumno> alumnos {  get; private set; } = new List<Alumno>();

        public void AgregarAlumno(T alumno)
        {
            alumnos.Add(alumno);
        }

        public void BuscarAlumnoPorNombre(string nombre)
        {
            Alumno resultado = alumnos.FirstOrDefault(x => x.Nombre.ToLower() == nombre.ToLower());

            if (resultado == null)
            {
                throw new NullReferenceException($"El alumno: {nombre}, no existe");
            }
            else
            {
                Console.WriteLine("Alumno encontrado: ");
                resultado.MostrarInformacion();
            }
        }

        public void BuscarAlumnoPorMatricula(string matricula)
        {
            Alumno resultado = alumnos.FirstOrDefault(x => x.Matricula == matricula.ToUpper());

            if (resultado == null)
            {
                throw new NullReferenceException($"El alumno con la matricula: {matricula.ToUpper()}, no existe");
            }
            else
            {
                Console.WriteLine($"Alumno {matricula.ToUpper()} encontrado: ");
                resultado.MostrarInformacion();
            }
        }

        public void EliminarAlumno(string matricula)
        {
            Alumno resultado = alumnos.FirstOrDefault(x => x.Matricula.ToUpper() == matricula.ToUpper());

            if (resultado == null)
            {
                throw new NullReferenceException($"El alumno {matricula.ToUpper()} no existe");
            }
            else
            {
                alumnos.Remove(resultado);
                Console.WriteLine($"El alumno ha sido eliminado");
            }
        }

        public void MostrarTodosAlumnos()
        {
            alumnos.ForEach(x => x.MostrarInformacion());
        }
        




    }
}
