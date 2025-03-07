using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
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

        public void EliminarAlumnoPorNombre(string nombre)
        {
            Alumno resultado = alumnos.FirstOrDefault(x => x.Nombre.ToUpper() == nombre.ToUpper());

            if (resultado == null)
            {
                throw new NullReferenceException($"El alumno {nombre.ToUpper()} no existe");
            }
            else
            {
                alumnos.Remove(resultado);
                Console.WriteLine($"El alumno ha sido eliminado");
            }
        }

        public void EliminarAlumnoPorMatricula(string matricula)
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
            if (alumnos.Any())
            {
                alumnos.ForEach(x => x.MostrarInformacion());
                return;
            }

            throw new InvalidOperationException("No existen alumnos param mostrar");
        }


        //Usaremos este metodo para mostrar ordenadamente los alumnos segun el criterio
        //que deceemos. Usaremos Func<> ya que el ejercicio pide el uso de este objeto.
        //
        //El metodo verifica primero que hayan objetos en la lista principal y luego
        //las ordena segun el criterio y muestra el resultado inmediatamente.
        //Si no hay objetos en la lista lanza una excepcion.
        public void OrdenarAlumnos(Func<Alumno, object> criterio)
        {
            if (alumnos.Any())
            {
                var AlumnosOrdenados = alumnos.OrderBy(criterio).ToList();

                AlumnosOrdenados.ForEach(x => x.MostrarInformacion());
                return;
            }

            throw new InvalidOperationException("No existe ningun alumno para ordenar.");
        }

        //Usaremos este metodo para filtrar y buscar entre la lista de alumnos
        //con criterios o filtros especificos.
        public void FiltrarAlumnos(Predicate<Alumno> criterio)
        {
            if (!alumnos.Any())
            {
                throw new InvalidOperationException("No existe ningun alumno para filtrar");
            }

            alumnos.ForEach(x => { if (criterio(x)) x.MostrarInformacion(); });
        }

        //Metodos GuardarDatosJSON y CargarDatosJSON se ocuparan de guardar/cargar los alumnos desde archivos Json.
        //Los archivos se encontraran en la carpetam temp de buscando %appdata% en el buscador de windows.
        public void GuardarDatosJSON()
        {
            string rutaArchivo = Path.Combine(Path.GetTempPath(), "ListaAlumnos.json");

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };
            string json = JsonSerializer.Serialize(alumnos, opciones);
            File.WriteAllText(rutaArchivo, json);
            Console.WriteLine("Archivo guardado con exito");

        }

        public void CargarDatosJSON()
        {
            string rutaArchivo = Path.Combine(Path.GetTempPath(), "ListaAlumnos.json");

            if (File.Exists(rutaArchivo))
            {
                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                };

                string json = File.ReadAllText(rutaArchivo);
                alumnos = JsonSerializer.Deserialize<List<Alumno>>(json, opciones);
                Console.WriteLine("El archivo fue cargado con exito.");
            }
            else
            {
                Console.WriteLine("Error: No se encontro ningun archivo para cargar");
            }

        }
        




    }
}
