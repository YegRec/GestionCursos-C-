using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GestionCursos.Program;
using static GestionCursos.GestionAlumnos<GestionCursos.Alumno>;


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
        [JsonInclude]

        //Asignaremos una matricula unica a cada estudiante, tomando en cuenta, nombres, edad y 
        //El curso que estaria cursando el alumno.
        public string Matricula { get; private set; }


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

        //Metodo que usaremos para asignar el nombre del curso y matricula al alumno.
        public void AsignarCurso(string curso, int num)
        {
            CursoAsignado = curso;
            AsignarMatricula(num);
        }


        //La funcion de este metodo es reiniciar la matricula del alumno. Ya que puede darse el caso de que el alumno
        //Decee seleccionar otro curso, en ese caso se deberia asignar una nueva matricula la cual se modificara
        //Dependiendo del nuevo curso que seleccione el alumno.
        public void RemoverCurso(string curso)
        {
            CursoAsignado = string.Empty;
            Matricula = string.Empty;
        }

        //Usaremos este metodo para controlar la asignacion de una matricula a un alumno.
        //El metodo recibe un numero que es el numero del alumno dentro del curso que selecciono.

        //El parametro Inombres toma el nombre del usuario y lo convierte en un array de strings
        //para asi manejar casos en los que el usuario tenga mas de un nombre o el nombre y apellidos.

        //El parametro InicialesNombre verifica si Nombre tiene mas de 2 palabras, si es asi toma las 2 primeras
        //y selecciona la primera letra de cada palabra. EJ: Nombre = Jose Alfredo. InicialesNombre = JA.
        //En caso de tener un solo nombre se seleccionan 3 letras del nombre. EJ: Nombre = Alfredo. InicialesNombre = ALF.

        //El parametro Icurso funciona exactamente igual que el Inombres solo que en vez del nombre
        //Utiliza el Nombre del curso asignado al alumno para hacer la seleccion de letras.

        //Finalmente la matricula se juntan todas las iniciales separadas por guiones y la edad del alumno.
        private void AsignarMatricula(int num)
        {
            string[] Inombres = Nombre.Split(' ');
            string InicialesNombre = (Inombres.Length > 1) ? string.Concat(Inombres.Take(2).Select(x => x.Substring(0,1).ToUpper())) : Nombre.Substring(0, Math.Min(3, Nombre.Length)).ToUpper();

            string[] Icursos = CursoAsignado.Split(' ');
            string Iniciales = (Icursos.Length > 1) ? string.Concat(Icursos.Take(2).Select(x => x.Substring(0, 1).ToUpper())) : CursoAsignado.Substring(0, Math.Min(3, CursoAsignado.Length)).ToUpper();

            Matricula = "M-" + InicialesNombre + Edad + "-" + Iniciales + "-" + num.ToString();
        }


    }
}
