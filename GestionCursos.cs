using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace GestionCursos
{
    internal class GestionCursos<TCurso, TAlumno>
        where TCurso : Curso<Alumno>
        where TAlumno : Alumno
    {
        [JsonInclude]
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


        public TCurso BuscarCurso(string nombre)
        {
            TCurso Resultado = cursos.Find(x => x.Nombre == nombre);

            return Resultado;
        }

        public void MostrarTodosLosCursos()
        {
            if (cursos.Any())
            {
                cursos.ForEach(x => x.MostrarInformacion());
                return;
            }

            throw new InvalidProgramException("No hay cursos para mostrar");

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

        public void GuardarCursosJSON()
        {
            string rutaArchivo = Path.Combine(Path.GetTempPath(), "Cursos.json");

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };

            string json = JsonSerializer.Serialize(cursos, opciones);
            File.WriteAllText(rutaArchivo, json);

            Console.WriteLine("Archivo guardado con exito.");
        }

        public void CargarCursosJSON()
        {
            string rutaArchivo = Path.Combine(Path.GetTempPath(), "Cursos.json");

            if (File.Exists(rutaArchivo))
            {
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                };
                string json = File.ReadAllText(rutaArchivo);
                cursos = JsonSerializer.Deserialize<List<TCurso>>(json, opciones);
                Console.WriteLine("Cursos cargados con exito.");
            }
            else
            {
                Console.WriteLine("El arciho de carga no existe.");
            }

        }


    }
}
