using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Álgebra;

namespace Gauss_Vs_Cramer
{

    class SistemaEcuacionesLineales<K>
    {
        private Matriz<K> A;
        private Vector<K> B;

        public SistemaEcuacionesLineales(Matriz<K> A, Vector<K> B)
        {
            
        }

        //Sintaxis: eq1, eq2, ..., eqn
        //eq1: 2*x-5*adfhjklas+4*8+7=0
        /*
         * Pasos a seguir: 
         *  -quitar espacios.
         *  -encontrar variables: todo conjunto de letras entre dos signos matemáticos
         *  -hacer operaciones numéricas correspondientes
         *  -hallar las matrices asociadas
         */
        public SistemaEcuacionesLineales(string texto)
        {

        }

        public Vector<K> Cramer()
        {
            Vector<K> soluciones = new Vector<K>(A.n);
            K detA = A.Det();
            for (int i = 1; i <= soluciones.Dim; i++)
                soluciones[i] = (dynamic)A.CamFil(i, B).Det() / (dynamic)detA;
            return soluciones;
        }

        public Vector<K> Gauss()
        {

        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Matemáticas.Cifras(234546465).ToStr());
            Console.ReadLine();

        }
    }
}
