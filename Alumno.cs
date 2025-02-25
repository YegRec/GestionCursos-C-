using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.Program;


namespace GestionCursos
{
    internal class Alumno : IAlumno
    {
        public string Nombre { get; private set; }
        public int Edad {  get; private set; }
        public double Promedio { get; private set; }

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

    }
}
