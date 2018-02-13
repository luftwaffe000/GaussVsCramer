using System;

namespace Álgebra
{
    class Quebrado
    {
        int n, d;

        public Quebrado(int n, int d)
        {
            this.n = n;
            this.d = d;
            Simplificar(this);
        }

        public Quebrado(string texto)
        {
            string[] cadena = texto.Split('/');
            n = Convert.ToInt32(cadena[0]);
            d = Convert.ToInt32(cadena[1]);
        }

        public static Quebrado Simplificar(Quebrado q)
        {
            if (q.d < 0)
            {
                q.n = -q.n;
                q.d = -q.d;
            }
            int mcd = Matemáticas.Mcd(Math.Abs(q.n), q.d);
            return new Quebrado(q.n / mcd, q.d / mcd);
        }

        public static Quebrado operator +(Quebrado a, Quebrado b)
        {
            return Simplificar(new Quebrado(a.n * b.d + b.n * a.d, a.d * b.d));
        }

        public static Quebrado operator -(Quebrado q)
        {
            return Simplificar(new Quebrado(-q.n, q.d));
        }

        public static Quebrado operator -(Quebrado a, Quebrado b)
        {
            return a + (-b);
        }

        public static Quebrado operator *(Quebrado a, Quebrado b)
        {
            return Simplificar(new Quebrado(a.n * b.n, a.d * b.d));
        }

        public static Quebrado operator !(Quebrado q) //Lo usamos para invertir.
        {
            if (q.n < 0)
            {
                q.n = -q.n;
                q.d = -q.d;
            }
            return new Quebrado(q.d, q.n);
        }

        public static Quebrado operator /(Quebrado a, Quebrado b)
        {
            return a * (!b);
        }

        public static implicit operator double(Quebrado q)
        {
            return q.n / q.d;
        }

        public static implicit operator float(Quebrado q)
        {
            return q.n / q.d;
        }

        public static implicit operator Quebrado(int n)
        {
            return new Quebrado(n, 1);
        }

        /*
        public static implicit operator Quebrado(double x)
        {

        }

        public static implicit operator Quebrado(float x)
        {

        }
        */

        public override string ToString()
        {
            if (d == 1)
                return n.ToString();
            else
                return n.ToString() + "/" + d.ToString();
        }

    }

    class Vector<K>
    {
        private K[] vector;

        public Vector(K[] array) { vector = array; }
        public Vector(int dim) { vector = new K[dim]; }

        public int Dim { get { return vector.Length; } }

        public K this [int i]
        {
            get
            {
                return vector[i - 1];
            }
            set
            {
                vector[i - 1] = value;
            }
        }

        public Vector<K> CamElto(int i, K valor)
        {
            Vector<K> temp = new Vector<K>(Dim);
            temp[i] = valor;
            return temp;
        }

        public static implicit operator Vector<K>(K[] array) { return new Vector<K>(array); }

        public static Vector<K> operator +(Vector<K> v, Vector<K> w)
        {
            Vector<K> suma = new Vector<K>(v.Dim);
            for (int i = 1; i <= v.Dim; i++)
                suma[i] = (dynamic)v[i] + (dynamic)w[i];
            return suma;
        }

        public static Vector<K> operator -(Vector<K> v)
        {
            return (dynamic)(-1) * v;
        }

        public static Vector<K> operator -(Vector<K> v, Vector<K> w)
        {
            return v + (-w);
        }

        public static Vector<K> operator *(K t, Vector<K> v)
        {
            Vector<K> temp = new Vector<K>(v.Dim);
            for (int i = 1; i <= v.Dim; i++)
                temp[i] = (dynamic)t * (dynamic)temp[i];
            return temp;
        }

        public static K operator *(Vector<K> v, Vector<K> w)
        {
            K núm = (dynamic)0;
            for (int i = 1; i <= v.Dim; i++)
                núm += (dynamic)v[i] * (dynamic)v[i];
            return núm;
        }

        public override string ToString()
        {
            string cadena = "(";
            for (int i = 1; i < Dim; i++)
                cadena += this[i].ToString() + ", ";
            cadena += this[Dim] + ")";
            return cadena;
        }

    }

    class Matriz<K> //Matriz mxn sobre K
    {

        private K[,] matriz;

        public int m { get { return matriz.GetLength(0); } }

        public int n { get { return matriz.GetLength(1); } }

        public Matriz(Vector<K>[] filas)
        {
            matriz = new K[filas.Length, filas[0].Dim];
            Filas = filas;
        }

        public Matriz(int m, int n)
        {
            matriz = new K[m, n];
        }

        public K this [int i, int j]
        {
            get
            {
                return matriz[i - 1, j - 1];
            }
            set
            {
                matriz[i - 1, j - 1] = value;
            }
        }

        public Vector<K> ObtFil(int i)
        {
            Vector<K> fila = new Vector<K>(n);
            for (int j = 1; j <= n; j++)
                fila[j] = this[i, j];
            return fila;
        }

        public Vector<K> ObtCol(int i)
        {
            Vector<K> col = new Vector<K>(m);
            for (int j = 1; j <= m; j++)
                col[j] = this[j, i];
            return col;
        }

        public Matriz<K> CamFil(int i, Vector<K> valor)
        {
            Matriz<K> temp = new Matriz<K>(m, n);
            temp = this;
            for (int j = 1; j <= n; j++)
                temp[i, j] = valor[j];
            return temp;
        }

        public Matriz<K> CamCol(int i, Vector<K> valor)
        {
            Matriz<K> temp = new Matriz<K>(m, n);
            temp = this;
            for (int j = 1; j <= m; j++)
                temp[j, i] = valor[j];
            return temp;
        }

        public Vector<K>[] Filas
        {
            get
            {
                Vector<K>[] fil = new Vector<K>[m];
                for (int i = 1; i <= m; i++)
                    fil[i - 1] = ObtFil(i);
                return fil;
            }
            set
            {
                for (int i = 1; i <= m; i++)
                    CamFil(i, value[i - 1]);
            }
        }
        public Vector<K>[] Columnas
        {
            get
            {
                Vector<K>[] col = new Vector<K>[n];
                for (int i = 1; i <= n; i++)
                    col[i - 1] = ObtCol(i);
                return col;
            }
            set
            {
                for (int i = 1; i <= n; i++)
                    CamCol(i, value[i - 1]);
            }
        }

        public Matriz<K> Trasponer()
        {
            return new Matriz<K>(Columnas);
        }

        public static Matriz<K> operator +(Matriz<K> A, Matriz<K> B)
        {
            Vector<K>[] filas = new Vector<K>[A.m];
            for (int i = 1; i <= A.m; i++)
                filas[i] = A.ObtFil(i) + B.ObtFil(i);
            return new Matriz<K>(filas);
        }

        public static Matriz<K> operator -(Matriz<K> A)
        {
            Vector<K>[] filas = new Vector<K>[A.m];
            for (int i = 1; i <= A.m; i++)
                filas[i] = -A.ObtFil(i);
            return new Matriz<K>(filas);
        }

        public static Matriz<K> operator -(Matriz<K> A, Matriz<K> B)
        {
            return A + (-B);
        }

        public static Matriz<K> operator *(K t, Matriz<K> A)
        {
            Vector<K>[] filas = new Vector<K>[A.m];
            for (int i = 1; i <= A.m; i++)
                filas[i] = t * A.ObtFil(i);
            return new Matriz<K>(filas);
        }


        public static Matriz<K> operator *(Matriz<K> A, Matriz<K> B)
        {
            Matriz<K> producto = new Matriz<K>(A.m, B.n);
            for (int i = 1; i <= A.m; i++)
                for (int j = 1; j <= B.n; j++)
                    producto[i, j] = A.ObtFil(i) * B.ObtCol(j);
            return producto;
        }

        public K Det()
        {
            if (m == 1)
                return this[1, 1];
            else
            {
                K det = (dynamic)0;
                for (int k = 1; k <= m; k++)
                    det += (dynamic)this[k, 1] * (dynamic)Adjunto(k, 1).Det();
                return det;
            }
        }

        public Matriz<K> Adjunto(int i, int j)
        {
            //Quitamos la fila i
            Matriz<K> M = new Matriz<K>(m - 1, n);
            for (int k = 1, l = 1; k <= M.m; k++, l++)
            {
                if (l == i) l++;
                M.CamFil(k, ObtFil(l));
            }
            //Quitamos la columna j
            Matriz<K> P = new Matriz<K>(m - 1, n - 1);
            for (int k = 1, l = 1; k <= P.n; k++, l++)
            {
                if (l == j) l++;
                M.CamCol(k, ObtCol(l));
            }
            return (dynamic)Math.Pow(-1, i + j) * P;
        }

        public static implicit operator Matriz<K>(K[][] array)
        {
            Vector<K>[] filas = new Vector<K>[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
                filas[i] = array[i];
            return new Matriz<K>(filas);
        }

        public static implicit operator Matriz<K>(K[,] array)
        {
            K[][] filas = new K[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                K[] fil = new K[array.GetLength(1)];
                for (int j = 0; j < array.GetLength(1); j++)
                    fil[j] = array[i, j];
                filas[i] = fil;
            }
            return filas;
        }
    }

    public static class Matemáticas
    {
        public static int Mcd(int n, int m)
        {
            int r = n % m;
            while (r != 0)
            {
                n = m;
                m = r;
                r = n % m;
            }
            return m;
        }

        public static int[] Cifras(int x)
        {
            int nCifras = 0;
            for (nCifras = 0; x / (Math.Pow(10, nCifras)) >= 1; nCifras++) ;
            int[] cifras = new int[nCifras];
            for (int i = nCifras - 1; i >= 0; i--)
            {
                cifras[i] = x % 10;
                x /= 10;
            }
            return cifras;
        }

    }

    static class Extensiones
    {
        public static string ToStr(this int[] x)
        {
            string cadena = "";
            for (int i = 0; i < x.Length - 1; i++)
                cadena += x[i].ToString() + "\t";
            cadena += x[x.Length - 1];
            return cadena;
        }

    }
}