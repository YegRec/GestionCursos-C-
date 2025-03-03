using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GestionCursos.Program;

namespace GestionCursos
{
    internal class Curso<T> : ICurso<T> where T : Alumno
    {
        [JsonInclude]
        public string Profesor { get; private set; }
        [JsonInclude]
        public string Nombre { get; private set; }
        [JsonInclude]
        public List<T> ListaAlumnos { get; private set; } = new List<T>();
        [JsonInclude]
        //Usaremos este parametro para indicar un limite de alumnos que pueden
        //inscribirse en el curso.
        public int LimiteEstudiantes { get; private set; }


        public Curso(string nombre, string profesor, int limite)
        {
            Nombre = nombre;
            Profesor = profesor;
            LimiteEstudiantes = limite;
        }

        public void AgregarAlumno(T alumno)
        {
            if (string.IsNullOrEmpty(alumno.CursoAsignado))
            {
                ListaAlumnos.Add(alumno);
                alumno.AsignarCurso(Nombre, ListaAlumnos.Count);
                return;
            }

            throw new InvalidOperationException($"El alumno {alumno.Matricula} ya se encuentra en el curso: {alumno.CursoAsignado}");
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

        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre}\n" +
                $"Profesor: {Profesor}\n" +
                $"Estudiantes: {ListaAlumnos.Count}/{LimiteEstudiantes}\n");
        }

        public void CambiarProfesor(string profesor)
        {
            if (profesor == Profesor)
            {
                Console.WriteLine("Los nombres ingresados son identicos");
                return;
            }

            Profesor = profesor;
            Console.WriteLine("Nombre cambiado con exito.");
        }

        public void VerAlumnosDelCurso()
        {
            if (!ListaAlumnos.Any())
            {
                throw new InvalidOperationException("No existen alumnos en este curso");
            }

            ListaAlumnos.ForEach(x => x.MostrarInformacion());
        }

        public void ObtenerPromedioDelCurso()
        {
            if (ListaAlumnos.Any())
            {
                Console.WriteLine($"El promedio de los alumnos es: {(ListaAlumnos.Sum(x => x.Promedio) / ListaAlumnos.Count)}");
                return;
            }

            throw new InvalidOperationException("No existen alumnos para calcular el promedio");
            
        }





    }
}
