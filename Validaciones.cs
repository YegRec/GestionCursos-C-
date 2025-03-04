using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (string.IsNullOrEmpty(texto))
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }


        //El mismo metodo anterior, solo que en este caso se ingresa un numero entero
        //que controla y verifica que el string "texto" no se mas largo al numero ingresado
        //EJ: ("Zapato", 8) = true. Significa que la palabra "Zapato".lenght no es mayor a 8
        //por ende el input es valido.
        public static string ValidarString(string texto, int largo)
        {
            if (string.IsNullOrEmpty(texto) || texto.Length > largo)
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }

        //Este metodo recibe un string numero y verifica que el string recibido sea un numero entero valido
        //en caso contrario lanzara una excepcion.
        public static int ValidarInt(string numero)
        {
            if (string.IsNullOrEmpty(numero) || !int.TryParse(numero, out int num))
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return int.Parse(numero);
        }

        //Este metodo recibe un string numero y un int para validar que el numero
        //recibido no sea mayor al segundo valor "largo"
        public static int ValidarInt(string numero, int largo)
        {
            if (string.IsNullOrEmpty(numero) || !int.TryParse(numero, out int num) || int.Parse(numero) > largo)
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return int.Parse(numero);
        }

        public static double ValidarDouble(string numero)
        {
            if (string.IsNullOrEmpty(numero) || !double.TryParse(numero, out double num))
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return double.Parse(numero);
        }


        public static double ValidarDouble(string numero, double largo)
        {
            if (string.IsNullOrEmpty(numero) || !double.TryParse(numero, out double num) || double.Parse(numero) > largo)
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return double.Parse(numero);
        }


    }
}
