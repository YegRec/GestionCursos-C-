using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GestionCursos.Program;


namespace GestionCursos
{
    internal class Alumno : IAlumno
    {
        [JsonInclude]
        public string Nombre { get; private set; }
        [JsonInclude]
        public int Edad {  get; private set; }
        [JsonInclude]
        public double Promedio { get; private set; }
        [JsonInclude]
        //Usaremos esta variable CursoSeleccion para guardar el curso en el que el
        //alumno se encuentra estudiando. Ya que cada curso tiene un nombre, al momento de asignar
        //el alumno al curso asignaremos el nombre del curso a esta variable.
        public string CursoAsignado { get; private set; }

        public Alumno(string nombre , int edad, double promedio)
        {
            Nombre = nombre;
            Edad = edad;
            Promedio = promedio;
        } 

        public void MostrarInformacion()
        {
            Console.WriteLine($"\nNombre: {Nombre}\n" +
                $"Edad: {Edad}\n" +
                $"Promedio: {Promedio}");
        }

        public double ObtenerPromedio()
        {
            return Promedio;
        }


        //Usaremos este metodo para asignar un valor a promedio ya que no
        //se puede modificar el valor de Promedio directamente ya que es privado.
        public void AsignarPromedio(double promedio)
        {
            Promedio = promedio;
        }

        //Metodo que usaremos para asignar el nombre del curso al alumno.
        public void AsignarCurso(string curso)
        {
            CursoAsignado = curso;
        }

    }
}
