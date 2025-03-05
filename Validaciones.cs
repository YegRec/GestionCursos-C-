using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionCursos
{
    //Esta clase su proposito principal es para validar los inputs del usuario
    //validar que los usuarios siempre ingresen los valores indicados o esperados
    //de lo contrario se lanzara un error.
    internal static class Validaciones
    {

        //Este metodo se encargara de validar que el usuario ingrese un string adecuado
        //Verificara que el string sea ingresado correctamente, no sea nulo ni vacio
        //de lo contrario lanzara una excepcion.
        //
        public static string ValidarString(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }

        //USaremos este metodo para validar la entrada de nombres por parte del usuario
        //el metodo recibe un texto que sera el input del usuario y luego valida si no esta vacio o nullo
        //luego verifica usando un patron y un regex que el texto ingresado corresponga a un nombre y un apellido
        //de al menos 4 caracteres o mas cada uno y que no se permitan caracteres especiales como comillas, comas
        //o guiones.
        public static string ValidarNombre(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new ArgumentNullException("El nombre ingresado es nulo o invalido");
            }

            string patron = @"^([A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-'][A-Za-zÁÉÍÓÚáéíóúÑñ]+)?)(\s[A-Za-zÁÉÍÓÚáéíóúÑñ]+(?:[-'][A-Za-zÁÉÍÓÚáéíóúÑñ]+)?)+$";

            if (!Regex.IsMatch(texto.Trim(), patron))
            {
                throw new ArgumentNullException("El texto ingresado debe contener un nombre y un apellido");
            }

            return texto.Trim();
        }


        //El mismo metodo anterior, solo que en este caso se ingresa un numero entero
        //que controla y verifica que el string "texto" no se mas largo al numero ingresado
        //EJ: ("Zapato", 8) = true. Significa que la palabra "Zapato".lenght no es mayor a 8
        //por ende el input es valido.
        public static string ValidarString(string texto, int largo)
        {
            if (string.IsNullOrWhiteSpace(texto) || texto.Length > largo)
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }

        //Este metodo recibe un string numero y verifica que el string recibido sea un numero entero valido
        //en caso contrario lanzara una excepcion.
        public static int ValidarInt(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || !int.TryParse(numero, out int num))
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return int.Parse(numero);
        }

        //Este metodo recibe un string numero y un int para validar que el numero
        //recibido no sea mayor al segundo valor "largo"
        public static int ValidarInt(string numero, int largo)
        {
            if (string.IsNullOrWhiteSpace(numero) || !int.TryParse(numero, out int num) || int.Parse(numero) > largo)
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return int.Parse(numero);
        }

        public static double ValidarDouble(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || !double.TryParse(numero, out double num))
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return double.Parse(numero);
        }


        public static double ValidarDouble(string numero, double largo)
        {
            if (string.IsNullOrWhiteSpace(numero) || !double.TryParse(numero, out double num) || double.Parse(numero) > largo)
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return double.Parse(numero);
        }


    }
}
