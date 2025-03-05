using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.GestionAlumnos<GestionCursos.Alumno>;

namespace GestionCursos
{
    internal static class Interfaz
    {
        private static string Version = "1.0";
        private static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS Y ALUMNOS {Version}\n\n\n" +
                "Bienvenido, por favor selecciona una opcion:\n\n" +
                "1. Administrar Alumnos\n" +
                "2. Administrar Cursos.\n" +
                "3. [Salir] (Cerrar programa)\n");
        }

        private static void InterfazAlumnos()
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

        private static void InterfazCrearAlummo(GestionAlumnos<Alumno> GestionadorDeAlumnos)
        {
            Console.Clear();
            Console.WriteLine("Por favor, ingresa el nombre del estudiante\n");
            string nombre = Validaciones.ValidarNombre(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Por favor, ingresa la edad del estudiante {nombre}");
            int edad = Validaciones.ValidarInt(Console.ReadLine(), 100);

            if (GestionadorDeAlumnos.alumnos.Exists(x => x.Nombre == nombre) && GestionadorDeAlumnos.alumnos.Exists(x => x.Edad == edad))
            {
                throw new InvalidOperationException("El alumno ingresado ya esta registrado");
            }

            Alumno NuevoAlumno = new Alumno(nombre, edad);

            GestionadorDeAlumnos.AgregarAlumno(NuevoAlumno);
            Console.Clear();
            Console.WriteLine("Alumno creado con exito\n");
            NuevoAlumno.MostrarInformacion();
        }

        private static void InterfazAlumnosBuscar()
        {
            Console.Clear();
            Console.WriteLine("GESTION DE ALUMNOS\n\n\n" +
                "Por favor, selecciona una opcion\n\n" +
                "1. Buscar Alumno por nombre\n" +
                "2. Buscar Alumno por matricula\n" +
                "3. [Salir] Volver al menu de alumnos\n");
        }

        private static void InterfazCursos()
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

        public static void InterfazCrearCurso(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos)
        {
            Console.Clear();

        }

        private static void InterfazCursosBuscar()
        {
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Por favor, selecciona una opcion:\n\n" +
                $"1. Cambiar profesor\n" +
                $"2. Ver alumnos del curso\n" +
                $"3. Inscribir alumno al curso\n" +
                $"4. Eliminar alumnos del curso\n" +
                $"5. Obtener promedio de alumno\n" +
                $"6. Asignar promedio a alumno\n" +
                $"7. Obtener promedio de todos los alumnos\n" +
                $"8. [Salir] menu de cursos");            

        }


        //DESDE AQUI INICIA LA IMPLEMENTACION DE LA INTERFAZ FUNCIONAL

        public static void Inicio()
        {

        }
    }
}
