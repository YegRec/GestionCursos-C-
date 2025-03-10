using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GestionCursos.GestionAlumnos<GestionCursos.Alumno>;

namespace GestionCursos
{
    internal static class Interfaz
    {
        //Este bool se usa para controlar el guardado/cargado de archivos. Dado que no se decea poder cargar cursos
        //Sin antes cargar los alumnos.
        public static bool Cargados = false;
        public static bool Guardados = false;

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
                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch (seleccion)
                {
                    case "1":
                        Interfaz.InterfazCrearAlummo(GestionadorAlumnos);
                        break;
                    case "2":
                        Interfaz.InterfazAlumnosBuscar(GestionadorAlumnos);
                        break;
                    case "3":
                        Interfaz.InterfazAlumnoEliminar(GestionadorAlumnos);
                        break;
                    case "4":
                        Console.Clear();
                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para mostrar");
                        }

                        GestionadorAlumnos.alumnos.ForEach(x => x.MostrarInformacion());
                        Interfaz.Esperar();
                        break;
                    case "5":
                        Console.Clear();
                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para ordenar");
                        }

                        Interfaz.InterfazOrdenarAlumnos(GestionadorAlumnos);
                        break;
                    case "6":
                        Console.Clear();

                        if (!GestionadorAlumnos.alumnos.Any())
                        {
                            throw new InvalidOperationException("No existen alumnos para filtrar");
                        }

                        Interfaz.InterfazFiltrarAlumnos(GestionadorAlumnos);
                        break;
                    case "7":
                        if (!Cargados)
                        {
                            GestionadorAlumnos.CargarDatosJSON();
                            Cargados = true;
                        }
                        else
                        {
                            Console.WriteLine("Los alumnos ya han sido cargados...");
                        }
                        Interfaz.Esperar();
                        break;
                    case "8":
                        if (!Guardados)
                        {
                            GestionadorAlumnos.GuardarDatosJSON();
                            Guardados = true;
                        }
                        else
                        {
                            Console.WriteLine("Los alumnos ya han sido Guardados...");
                        }
                        Interfaz.Esperar();
                        break;

                    default:
                        break;
                }

                if (seleccion == "9")
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
                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch (seleccion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Por favor ingresa el nombre del estudiante");
                        string nombre = Validaciones.ValidarNombre(Console.ReadLine());
                        GestionadorAlumnos.BuscarAlumnoPorNombre(nombre);
                        Interfaz.Esperar();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Por favor ingresa la matricula del estudiante");
                        string matricula = Validaciones.ValidarString(Console.ReadLine(), 12);
                        GestionadorAlumnos.BuscarAlumnoPorMatricula(matricula);
                        Interfaz.Esperar();
                        break;
                    default:
                        break;
                }

                if (seleccion == "3")
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

                string seleccion = Validaciones.ValidarString(Console.ReadLine()); // Valor usado para la seleccion

                switch(seleccion)
                {
                    case "1":
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
                    case "2":
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
                        case "3":
                        break;
                }
                if (seleccion == "3")
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

                string seleccion = Validaciones.ValidarString(Console.ReadLine()); //valida la seleccion
                Console.Clear();

                switch(seleccion)
                {
                    case "1":
                        Console.WriteLine("Ordenando alumnos por nombre:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Nombre);
                        Interfaz.Esperar();
                        break;
                    case "2":
                        Console.WriteLine("Ordenando alumnos por edad:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Edad);
                        Interfaz.Esperar();
                        break;
                    case "3":
                        Console.WriteLine("Ordenando alumnos por promedio:\n\n");
                        GestionadorAlumnos.OrdenarAlumnos(x => x.Promedio);
                        Interfaz.Esperar();
                        break;
                }

                if (seleccion == "4")
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

                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Ingresa la edad a filtrar (mayor que)");
                        int readResult = Validaciones.ValidarInt(Console.ReadLine());
                        GestionadorAlumnos.FiltrarAlumnos(x => x.Edad > readResult);
                        Interfaz.Esperar();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Ingresa el promedio a filtrar (menor que) (valor maximo: 10)");
                        int readResult2 = Validaciones.ValidarInt(Console.ReadLine(), 10);
                        GestionadorAlumnos.FiltrarAlumnos(x => x.Promedio < readResult2);
                        Interfaz.Esperar();
                        break;                        
                }

                if (seleccion == "3")
                {
                    break;
                }
            }
        }

        //Interfaz de usuario encargada de manejar los cursos
        private static void InterfazCursos(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos, GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
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

                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion)
                {
                    case "1":
                        Console.Clear();
                        Interfaz.InterfazCrearCurso(GestionadorDeCursos);
                        Interfaz.Esperar();
                        break;
                    case "2":
                        Interfaz.BuscarCurso(GestionadorDeCursos, GestionadorAlumnos);
                        break;
                    case "3":
                        Interfaz.InterfazEliminarCurso(GestionadorDeCursos);
                        break;
                    case "4":
                        if (!GestionadorDeCursos.cursos.Any())
                        {
                            Console.Clear();
                            Console.WriteLine("GESTION DE CURSOS\n\n\n" +
                                "No existe ningun curso para mostrar.");
                        }
                        else
                        {
                            Console.Clear();
                            GestionadorDeCursos.cursos.ForEach(x => x.MostrarInformacion());

                        }
                        Interfaz.Esperar();
                        break;
                    case "5":
                        if (Cargados == true)
                        {
                            GestionadorDeCursos.CargarCursosJSON();
                        }
                        else
                        {
                            Console.WriteLine("Es necesario cargar los alumnos antes de cargar los cursos.");
                        }
                        Interfaz.Esperar();
                        break;
                    case "6":
                        if (Guardados == true)
                        {
                            GestionadorDeCursos.GuardarCursosJSON();
                        }
                        else
                        {
                            Console.WriteLine("Es necesario guardar los alumnos antes de guardar los cursos");
                        }
                        Interfaz.Esperar();
                        break;
                }

                if (seleccion == "7")
                {
                    break;
                }
                //fin switch
            }
        }

        //Metodo de interfaz para eliminar curso. Utiliza un gestionador de cursos y itera entre la lista de cursos
        //para buscar el curso que el usuario haya escrito y confirmar para eliminarlo.
        private static void InterfazEliminarCurso(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos)
        {
            Console.Clear();
            Console.WriteLine("GESTION DE CURSOS\n\n\n" +
                "Por favor, ingresa el nombre del curso a eliminar");

            string nombre = Validaciones.ValidarString(Console.ReadLine());

            var Curso = GestionadorDeCursos.cursos.Find(x => x.Nombre.ToLower() == nombre.ToLower());

            if (Curso == null)
            {
                throw new InvalidOperationException("\nEL curso no existe");
            }

            Console.Clear();
            Console.WriteLine($"Curso encontrado:\n");
            Curso.MostrarInformacion();

            Console.WriteLine("Seguro deceas eliminar este curso? (Y/N)");
            string seleccion = Validaciones.ValidarString(Console.ReadLine());

            switch(seleccion.ToLower())
            {
                case "y":
                    GestionadorDeCursos.EliminarCurso(Curso);
                    Console.WriteLine("El curso ha sido eliminado con exito.");
                    break;
                case "n":
                    Console.WriteLine("Cancelando operacion...");
                    break;
                default:
                    Console.WriteLine("Seleccion incorrecta. Cancelando operacion...");
                    break;
            }

            Interfaz.Esperar();
        }


        //Metodo de interfaz para crear un curso.
        //Solicita al usuario nombre, profesor y cantidad de alumnos maximo del curso
        //Usando la clase Validaciones para validar que los datos ingresados sean correctos
        private static void InterfazCrearCurso(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos)
        {
            Console.Clear();
            Console.WriteLine("Por favor ingresa el nombre del curso\n");
            string nombreCurso = Validaciones.ValidarString(Console.ReadLine(), 20);

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
            Console.Clear();
            Console.WriteLine($"EL curso {nombreCurso} fue creado con exito\n");
            NuevoCurso.MostrarInformacion();

        }

        //Metodo de interfaz encargado de buscar un curso y administrarlo en caso de encontrarlo.
        private static void BuscarCurso(GestionCursos<Curso<Alumno>, Alumno> GestionadorDeCursos, GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                    $"Por favor, ingresa el nombre del curso\n");

                string NombreCurso = Validaciones.ValidarString(Console.ReadLine());

                if (!GestionadorDeCursos.cursos.Exists(x => x.Nombre.ToLower() == NombreCurso.ToLower()))
                {
                    throw new ArgumentNullException($"No existe un curso llamado {NombreCurso}");
                }

                Console.Clear();
                Console.WriteLine($"Curso encontrado:\n");

                var Curso = GestionadorDeCursos.cursos.Find(x => x.Nombre.ToLower() == NombreCurso.ToLower());
                Curso.MostrarInformacion();

                Console.WriteLine($"Deceas administrar el curso? (Y/N)\n");

                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion.ToLower())
                {
                    case "y":
                        Interfaz.InterfazAdministrarCurso(Curso, GestionadorAlumnos);
                        seleccion = "salir.while";
                        break;
                    case "n":
                        Console.WriteLine("Cancelando operacion...");
                        seleccion = "salir.while";
                        Interfaz.Esperar();
                        break;
                }

                if (seleccion == "salir.while")
                {
                    break;
                }
            }
        }


        //Metodo de interfaz encargado de administrar un curso. Agregar, eliminar alumnos, asignar promedios etc...
        private static void InterfazAdministrarCurso(Curso<Alumno> Curso, GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                    $"Por favor, selecciona una opcion:\n\n" +
                    $"1. Cambiar profesor\n" +
                    $"2. Ver alumnos del curso\n" +
                    $"3. Inscribir alumno al curso\n" +
                    $"4. Eliminar alumno del curso\n" +
                    $"5. Asignar promedio a alumno\n" +
                    $"6. Obtener promedio de todos los alumnos\n" +
                    $"7. [Salir] menu de cursos");

                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion)
                {
                    case "1":
                        Interfaz.InterfazCambioProfesor(Curso);
                        break;
                    case "2":
                        Curso.VerAlumnosDelCurso();
                        Interfaz.Esperar();
                        break;
                    case "3":
                        Interfaz.InterfazInscribirAlumno(Curso, GestionadorAlumnos);
                        break;
                    case "4":
                        Interfaz.InterfazEliminarAlumnoDeCurso(Curso);
                        break;
                    case "5":
                        Interfaz.InterfazAsignarPromedio(Curso);
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("GESTION DE CURSOS\n\n\n");
                        if (Curso.ListaAlumnos.Any())
                        {
                            Console.WriteLine($"El promedio de los alumnos es: {(Curso.ListaAlumnos.Sum(x => x.Promedio) / Curso.ListaAlumnos.Count)}");
                        }
                        else
                        {
                            Console.WriteLine("No hay alumnos inscritos en el curso...");
                        }
                        Interfaz.Esperar();
                        break;
                }

                if (seleccion == "7")
                {
                    break;
                }
            }

        }

        //Metodo para cambiar profesor de un curso.
        private static void InterfazCambioProfesor(Curso<Alumno> Curso)
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Por favor, ingresa el nombre del nuevo profesor");

            string nuevoNombre = Validaciones.ValidarNombre(Console.ReadLine());


            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Esta seguro que deceas reemplazar a {Curso.Profesor} por {nuevoNombre}? (Y/N)\n");

            string seleccion = Validaciones.ValidarString(Console.ReadLine());

            switch(seleccion.ToLower())
            {
                case "y":
                    Curso.CambiarProfesor(nuevoNombre);
                    Interfaz.Esperar();
                    break;
                case "n":
                    Console.WriteLine("Operacion cancelada...");
                    Interfaz.Esperar();
                    break;
            }

        }


        //Metodo para inscribir un alumno al curso.
        private static void InterfazInscribirAlumno(Curso<Alumno> Curso, GestionAlumnos<Alumno> GestionadorAlumnos)
        {
            Console.Clear();
            Console.WriteLine($"GESTION DE CURSOS\n\n\n" +
                $"Por favor, ingresa el nombre del alumno\n");

            string nombreAlumno = Validaciones.ValidarNombre(Console.ReadLine());

            if (!GestionadorAlumnos.alumnos.Exists(x => x.Nombre.ToLower() == nombreAlumno.ToLower()))
            {
                throw new InvalidOperationException("El alumno ingresado no existe");
            }

            var Alumno = GestionadorAlumnos.alumnos.Find(x => x.Nombre.ToLower() == nombreAlumno.ToLower());

            Console.Clear();
            Console.WriteLine("Alumno encontrado:\n");
            Alumno.MostrarInformacion();

            if (!string.IsNullOrEmpty(Alumno.CursoAsignado))
            {
                Console.WriteLine($"\nEl alumno ya se encuentra en el curso: {Alumno.CursoAsignado}.\n" +
                    $"por lo que no puede ser Incrito a {Curso.Nombre}");
            }
            else
            {
                Console.WriteLine("\nInscribir el alumno al curso? (Y/N)\n");
                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion.ToLower())
                {
                    case "y":
                        Console.Clear();
                        Curso.AgregarAlumno(Alumno);
                        Console.WriteLine("Alumno agregado con exito\n");
                        Alumno.MostrarInformacion();
                        break;
                    case "n":
                        Console.WriteLine("Cancelando operacion...");
                        break;
                }                
            }

            Interfaz.Esperar();
        }

        //Metodo para eliminar un alumno del curso
        private static void InterfazEliminarAlumnoDeCurso(Curso<Alumno> Curso)
        {
            Console.Clear();
            Console.WriteLine("GESTIONAR CURSO\n\n\n" +
                "Por favor, ingresa el nombre del alumno\n");

            string nombreAlumno = Validaciones.ValidarNombre(Console.ReadLine());

            if (Curso.ListaAlumnos.Exists(x => x.Nombre.ToLower() == nombreAlumno.ToLower()))
            {
                Console.Clear();
                Console.WriteLine("Alumno encontrado:\n");
                var nuevoalumno = Curso.ListaAlumnos.Find(x => x.Nombre.ToLower() == nombreAlumno.ToLower());
                nuevoalumno.MostrarInformacion();

                Console.WriteLine("\nSeguro deceas eliminar este alumno del curso? (Y/N)\n");

                string seleccion = Validaciones.ValidarString(Console.ReadLine());

                switch(seleccion.ToLower())
                {
                    case "y":
                        Curso.EliminarAlumno(nuevoalumno);
                        nuevoalumno.RemoverCurso();
                        Console.WriteLine("Alumno eliminado con exito");
                        break;
                    case "n":
                        Console.WriteLine("Cancelando operacion...");
                        break;
                    default:
                        Console.WriteLine("Seleccion incorrecta, cancelando operacion...");
                        break;                         
                }
            }
            else
            {
                Console.WriteLine($"El alumno {nombreAlumno} no se encuentra en este curso o no existe");
            }
            Interfaz.Esperar();
        }

        //Metodo para asignar promedio a un alumno
        private static void InterfazAsignarPromedio(Curso<Alumno> Curso)
        {
            Console.Clear();
            Console.WriteLine("GESTION DE CURSOS\n\n\n" +
                "Por favor, ingresa la matricula del alumno");

            string matricula = Validaciones.ValidarString(Console.ReadLine());

            var Alumno = Curso.ListaAlumnos.Find(x => x.Matricula.ToUpper() == matricula.ToUpper());

            if (Alumno == null)
            {
                throw new InvalidOperationException($"El alumno con la matricula {matricula} no existe");
            }

            Console.Clear();
            Console.WriteLine("Alumno encontrado:\n");
            Alumno.MostrarInformacion();

            Console.WriteLine("Ingresa el promedo del alumno\n");
            double promedio = Validaciones.ValidarDouble(Console.ReadLine());

            Alumno.AsignarPromedio(promedio);
            Console.Clear();
            Console.WriteLine($"Se actualizo el promedio de {Alumno.Matricula.ToUpper()} con exito\n");

            Alumno.MostrarInformacion();
            Interfaz.Esperar();

        }


        //Metodo de espera, usado cuando se termina de ejecutar algun metodo.
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
                        case 2:
                            Interfaz.InterfazCursos(GestionadorCursos, GestionadorAlumnos);
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
