using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCursos
{
    internal static class Interfaz
    {
        private static string Version = "1.0";
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS Y ALUMNOS {Version}\n\n\n" +
                "Bienvenido, por favor selecciona una opcion:\n\n" +
                "1. Administrar Alumnos\n" +
                "2. Administrar Cursos.\n" +
                "3. [Salir] (Cerrar programa)\n");
        }

        public static void InterfazAlumnos()
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE ALUMNOS\n\n\n" +
                $"Por favor, selecciona una opcion:\n\n" +
                $"1. Agregar Alumno\n" +
                $"2. Buscar Alumno\n" +
                $"3. Eliminar Alumno\n" +
                $"4. Mostrar todos los Alumnos\n" +
                $"5. Ordenar Alumnos\n" +
                $"6. Filtrar Alumnos\n" +
                $"7. Cargar Alumnos por JSON\n" +
                $"8. Guardar Alumnos por JSON\n" +
                $"9. [Salir] Volver al menu principal\n");
        }

        public static void InterfazAlumnosBuscar()
        {
            Console.Clear();
            Console.WriteLine("GESTION DE ALUMNOS\n\n\n" +
                "Por favor, selecciona una opcion\n\n" +
                "1. Buscar Alumno por nombre\n" +
                "2. Buscar Alumno por matricula\n" +
                "3. [Salir] Volver al menu de alumnos\n");
        }

        public static void InterfazCursos()
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Por favor, selecciona una opcion\n\n" +
                $"1. Agregar curso\n" +
                $"2. Buscar curso\n" +
                $"3. Eliminar curso\n" +
                $"4. Mostrar todos los cursos\n" +
                $"5. Cargar cursos por JSON\n" +
                $"6. Guardar cursos por JSON\n" +
                $"7. [Salir] Volver al menu principal\n");
        }

        public static void InterfazCursosBuscar()
        {
            

            //Interfaz 2: (Dentro de un curso)
            // Cambiar profesor.
            // Ver alumnos del curso.
            // Inscribir alumno al curso.
            // Eliminar alumno del curso.
            // Obtener promedio de alumno.
            // Asignar promedio a alumno.
            // Obtener promedio de todos los alumnos.
        }
    }
}
