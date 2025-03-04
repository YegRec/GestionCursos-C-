using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCursos
{
    internal static class Validaciones
    {
        public static string ValidarString(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }

        public static string ValidarString(string texto, int largo)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new ArgumentNullException("El texto ingresado es nulo o invalido");
            }

            return texto;
        }

        public static int ValidarInt(string numero)
        {
            if (string.IsNullOrEmpty(numero) || !int.TryParse(numero, out int num))
            {
                throw new ArgumentException("El numero ingresado es nulo o invalido");
            }

            return int.Parse(numero);
        }

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
