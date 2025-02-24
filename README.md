Proyecto: Sistema de Gestión de Cursos Académicos
Descripción:
Vas a desarrollar un sistema para gestionar cursos y alumnos. Cada curso puede tener múltiples alumnos inscritos. El sistema permitirá:

Agregar y eliminar cursos.
Inscribir y eliminar alumnos de los cursos.
Calcular el promedio de notas de un curso.
Filtrar alumnos por nombre, edad o promedio.
Ordenar alumnos por diferentes criterios.
Guardar y cargar los datos de cursos y alumnos desde un archivo JSON.
Usar Git para gestionar el control de versiones.
📦 Requisitos del Proyecto:
Clases Principales:

Alumno: Propiedades Nombre, Edad, Promedio.
Curso: Propiedades Nombre, Profesor y una lista genérica de alumnos.
GestorCursos<T> (Genérico): Maneja las operaciones relacionadas con los cursos.
Interfaces:

IAlumno: Métodos MostrarInformacion() y ObtenerPromedio().
ICurso: Métodos AgregarAlumno(), EliminarAlumno() y CalcularPromedioCurso().
Eventos y Delegados:

Evento al agregar un nuevo alumno al curso.
Evento al eliminar un alumno.
Métodos CRUD:

AgregarCurso(), EliminarCurso(), InscribirAlumno(), EliminarAlumno().
Funcionalidades Adicionales:

Filtrar alumnos usando Predicate<T> y mostrar usando Action<T>.
Ordenar alumnos usando Func<T, object> y LINQ.
Guardar y cargar los cursos y alumnos usando JSON.
Uso de Git:

Crea un repositorio en GitHub llamado SistemaGestionCursos.
Realiza commits periódicos con mensajes descriptivos.
Crea un archivo .gitignore para evitar archivos innecesarios.
Agrega un README.md explicando el propósito y funcionamiento del sistema.
