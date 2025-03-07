using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.GestionAlumnos<GestionCursos.Alumno>;

namespace GestionCursos
{
    internal static class Interfaz
    {
        private static string Version = "1.0";

        //Interfaz menu principal.
        private static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS Y ALUMNOS {Version}\n\n\n" +
                "Bienvenido, por favor selecciona una opcion:\n\n" +
                "1. Administrar Alumnos\n" +
                "2. Administrar Cursos.\n" +
                "3. [Salir] (Cerrar programa)\n");
        }


        //Esta interfaz maneja todas las interacciones relacionadas con los alumnos.
        //Agregar, eliminar, etc. Recibe como parametro un Gestionador de Alumnos
        //El cual se usara como base para trabajar sobre el.
        private static void InterfazAlumnos(GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
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
                int seleccion = Validaciones.ValidarInt(Console.ReadLine());

                switch (seleccion)
                {
                    case 1:
                        Interfaz.InterfazCrearAlummo(GestionadorAlumnos);
                        break;
                    case 2:
                        Interfaz.InterfazAlumnosBuscar(GestionadorAlumnos);
                        break;
                    case 3:
                        Interfaz.InterfazAlumnoEliminar(GestionadorAlumnos);
                        break;
                    case 4:
                        Console.Clear();
                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para mostrar");
                        }

                        GestionadorAlumnos.alumnos.ForEach(x => x.MostrarInformacion());
                        Interfaz.Esperar();
                        break;
                    case 5:
                        Console.Clear();
                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para ordenar");
                        }

                        Interfaz.InterfazOrdenarAlumnos(GestionadorAlumnos);
                        break;
                    case 6:
                        Console.Clear();

                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para filtrar");
                        }

                        Interfaz.InterfazFiltrarAlumnos(GestionadorAlumnos);
                        break;
                    case 7:
                        GestionadorAlumnos.CargarDatosJSON();
                        Interfaz.Esperar();
                        break;
                    case 8:
                        GestionadorAlumnos.GuardarDatosJSON();
                        Interfaz.Esperar();
                        break;

                    default:
                        break;
                }

                if (seleccion == 9)
                {
                    break;
                }
            }

        }

        //Metodo de interfaz encargado de crear un alumno.
        //Pide al usuario los parametros necesarios para la creacion del alumno: edad y nombre con apellido.
        //usando la clase Validaciones y sus metodos procesamos la informacion solicitada al usuario
        //y confirmamos que todo este correcto antes de proceder con la creacion del alumno
        //Tambien se hace una ultima verificacion y es comprobar si el alumnos que se esta creando
        //no este previamente agregado en la lista de alumnos.
        //
        //El metodo recibe un GestionadorDeAlumnos como base y como lista de alumnos.
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
            Interfaz.Esperar();
        }

        //Metodo de interfaz para buscar alumnos.
        //
        //Da la opcion al usuario de elegir si decea buscar un alumno
        //por nombre o matricula
        //OJO: Un alumno no adquiere una matricula hasta ser asignado a un curso.
        private static void InterfazAlumnosBuscar(GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("GESTION DE ALUMNOS\n\n\n" +
                    "Por favor, selecciona una opcion\n\n" +
                    "1. Buscar Alumno por nombre\n" +
                    "2. Buscar Alumno por matricula\n" +
                    "3. [Salir] Volver al menu de alumnos\n");
                int seleccion = Validaciones.ValidarInt(Console.ReadLine());

                switch (seleccion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Por favor ingresa el nombre del estudiante");
                        string nombre = Validaciones.ValidarNombre(Console.ReadLine());
                        GestionadorAlumnos.BuscarAlumnoPorNombre(nombre);
                        Interfaz.Esperar();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Por favor ingresa la matricula del estudiante");
                        string matricula = Validaciones.ValidarString(Console.ReadLine(), 12);
                        GestionadorAlumnos.BuscarAlumnoPorMatricula(matricula);
                        Interfaz.Esperar();
                        break;
                    default:
                        break;
                }

                if (seleccion == 3)
                {
                    break;
                }

            }

        }
        //Metodo para eliminar un alumnos de la lista.
        //Da la posibilidad al usuario de eliminar un alumno de la lista
        //principal de alumnos.
        private static void InterfazAlumnoEliminar(GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("GESTION DE ALUMNOS\n\n\n" +
                    "Por favor, selecciona una opcion\n\n" +
                    "1. Eliminar Alumno por nombre\n" +
                    "2. Eliminar Alumno por matricula\n" +
                    "3. [Salir] Volver al menu de alumnos\n");

                int seleccion = Validaciones.ValidarInt(Console.ReadLine()); // Valor usado para la seleccion

                switch(seleccion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Por favor, ingresa el nombre del alumno a eliminar");
                        string nombre = Validaciones.ValidarNombre(Console.ReadLine()); //Solicita y verifica el input del usuario.

                        if (!GestionadorAlumnos.alumnos.Exists(x => x.Nombre.ToLower() == nombre.ToLower()))//Verifica que el alumno existe.
                        {
                            throw new ArgumentNullException($"El estudiante {nombre} no existe");//Lanza si el alumno no existe
                        }

                        Console.WriteLine("Alumno encontrado\n");
                        GestionadorAlumnos.alumnos.Find(x => x.Nombre.ToLower() == nombre.ToLower()).MostrarInformacion();//Se muestra el alumno


                        //Se procede a confirmar al usuario si decea eliminar el alumno.
                        //Si el usuario ingresa un valor incorrecto o invalido se cancela la opracion completamente.
                        Console.WriteLine("Seguro deceas eliminar este alumno? (Y/N)");
                        string opcion = Validaciones.ValidarString(Console.ReadLine(), 1);
                        switch (opcion.ToUpper())
                        {
                            case "Y":
                                GestionadorAlumnos.EliminarAlumnoPorNombre(nombre);
                                break;
                            case "N":
                                Console.WriteLine("Cancelando operacion...");
                                break;
                            default:
                                Console.WriteLine("Seleccion incorrecta. Cancelando operacion.");
                                break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Por favor, ingresa la matricula del alumno a eliminar");
                        string matricula = Validaciones.ValidarString(Console.ReadLine());

                        if (!GestionadorAlumnos.alumnos.Exists(x => x.Matricula.ToUpper() == matricula.ToUpper()))
                        {
                            throw new ArgumentNullException($"El estudiante con la matricula {matricula.ToUpper()} no existe");
                        }

                        Console.WriteLine("Alumno encontrado:\n");
                        GestionadorAlumnos.alumnos.Find(x => x.Matricula.ToUpper() == matricula.ToUpper()).MostrarInformacion();

                        Console.WriteLine("\nSeguro deceas eliminar este alumno? (Y/N)");
                        string opcion2 = Validaciones.ValidarString(Console.ReadLine(), 1);
                        switch (opcion2.ToUpper())
                        {
                            case "Y":
                                GestionadorAlumnos.EliminarAlumnoPorMatricula(matricula);
                                break;
                            case "N":
                                Console.WriteLine("Cancelando operacion...");
                                break;
                            default:
                                Console.WriteLine("Seleccion incorrecta. Cancelando operacion.");
                                break;
                        }
                        break;
                        case 3:
                        break;
                }
                if (seleccion == 3)
                {
                    break;
                }

                Interfaz.Esperar();
            }
        }


        //metodo usado para conectar la interfaz con los metodos de ordenamiento del GestionadorDeAlumnos
        //Ordena los alumnos segun el criterio o seleccion hecha.

        //Luego de la seleccion muestra la lista completa de alumnos ordenada segun el criterio seleccionado.
        private static void InterfazOrdenarAlumnos(GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"GESTION ALUMNOS\n\n\n" +
                    $"Por favor selecciona una opcion:\n\n" +
                    $"1. Ordenar alumnos por nombre\n" +
                    $"2. Ordenar alumnos por edad\n" +
                    $"3. Ordenar alumnos por promedio\n" +
                    $"4. [Salir] Volver al menu de alumnos");

                int seleccion = Validaciones.ValidarInt(Console.ReadLine()); //valida la seleccion
                Console.Clear();

                switch(seleccion)
                {
                    case 1:
                        Console.WriteLine("Ordenando alumnos por nombre:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Nombre);
                        Interfaz.Esperar();
                        break;
                    case 2:
                        Console.WriteLine("Ordenando alumnos por edad:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Edad);
                        Interfaz.Esperar();
                        break;
                    case 3:
                        Console.WriteLine("Ordenando alumnos por promedio:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Promedio);
                        Interfaz.Esperar();
                        break;
                }

                if (seleccion == 4)
                {
                    break;
                }

            }
        }

        //Filtra los alumnos segun el criterio seleccionado
        //Luego muestra solo los alumnos que cumplen con dicho criterio.
        private static void InterfazFiltrarAlumnos(GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"GESTION ALUMNOS\n\n\n" +
                    $"Por favor, selecciona una opcion:\n\n" +
                    $"1. Filtrar alumnos por edad\n" +
                    $"2. Filtrar alumnos por promedio\n" +
                    $"3. [Salir] Volver al menu de alumnos\n");

                int seleccion = Validaciones.ValidarInt(Console.ReadLine());

                switch(seleccion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ingresa la edad a filtrar (mayor que)");
                        int readResult = Validaciones.ValidarInt(Console.ReadLine());
                        GestionadorAlumnos.FiltrarAlumnos(x => x.Edad > readResult);
                        Interfaz.Esperar();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ingresa el promedio a filtrar (menor que) (valor maximo: 10)");
                        int readResult2 = Validaciones.ValidarInt(Console.ReadLine(), 10);
                        GestionadorAlumnos.FiltrarAlumnos(x => x.Promedio < readResult2);
                        Interfaz.Esperar();
                        break;                        
                }

                if (seleccion == 3)
                {
                    break;
                }
            }
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

        private static void InterfazCrearCurso(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos)
        {
            Console.Clear();
            Console.WriteLine("Por favor ingresa el nombre del curso\n");
            string nombreCurso = Validaciones.ValidarString(Console.ReadLine(), 3);

            if (GestionadorDeCursos.cursos.Exists(x => x.Nombre == nombreCurso))
            {
                throw new InvalidOperationException("El curso ingresado ya existe");
            }

            Console.Clear();
            Console.WriteLine("Por favor ingresa el nombre del profesor");
            string nombreProfesor = Validaciones.ValidarNombre(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Ingresa el numero maximo de estudiantes del curso");
            int limite = Validaciones.ValidarInt(Console.ReadLine());

            Curso<Alumno> NuevoCurso = new Curso<Alumno>(nombreCurso, nombreProfesor, limite);
            GestionadorDeCursos.AgregarCurso(NuevoCurso);
            Console.WriteLine($"EL curso {nombreCurso} fue creado con exito");
            NuevoCurso.MostrarInformacion();

        }

        private static void InterfazCursosBuscar()
        {
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Por favor, selecciona una opcion:\n\n" +
                $"1. Cambiar profesor\n" +
                $"2. Ver alumnos del curso\n" +
                $"3. Inscribir alumno al curso\n" +
                $"4. Eliminar alumno del curso\n" +
                $"5. Obtener promedio de alumno\n" +
                $"6. Asignar promedio a alumno\n" +
                $"7. Obtener promedio de todos los alumnos\n" +
                $"8. [Salir] menu de cursos");            

        }

        public static void Esperar()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar");
            Console.ReadKey();
        }

        //DESDE AQUI INICIA LA IMPLEMENTACION DE LA INTERFAZ FUNCIONAL

        public static void Inicio(GestionAlumnos<Alumno> GestionadorAlumnos, GestionCursos<Curso<Alumno>, Alumno> GestionadorCursos)
        {
            while (true)
            {
                Console.Clear();
                Interfaz.MainMenu();

                try
                {
                    int seleccion = Validaciones.ValidarInt(Console.ReadLine());

                    switch(seleccion)
                    {
                        case 1:
                            Interfaz.InterfazAlumnos(GestionadorAlumnos);
                            break;






                    }

                    if (seleccion == 3)
                    {
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    }

                    //fin switch











                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Interfaz.Esperar();
                }
                catch (NullReferenceException ez)
                {
                    Console.WriteLine($"{ez.Message}");
                    Interfaz.Esperar();
                }
                catch (ArgumentNullException ec)
                {
                    Console.WriteLine($"{ec.Message}");
                    Interfaz.Esperar();
                }
                catch (Exception op)
                {
                    Console.WriteLine($"Algo salio mal: {op.Message}");
                    Interfaz.Esperar();
                }

            }
            //fin
        }
    }
}
