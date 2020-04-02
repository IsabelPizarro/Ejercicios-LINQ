using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Utilities;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using ConsoleAppEjerciciosLinq.Models;

namespace ConsoleAppEjerciciosLinq
{
    public static class Extensions
    {
        public static string PrintElements<T>(this List<T> list)
        {
            string resultado = "";

            for (var i = 0; i < list.Count; i++)
            {
                resultado += list[i].ToString();
                if (i < list.Count - 1) resultado += ", ";
            }

            return resultado;
        }
    }

    public class Program
    {
        public static ModelNorthwind db;
        public static List<int> Numeros;
        public static List<decimal> Numeros2;
        public static List<string> Frutas;
        public static List<Persona> Gente;

        public static List<Employees> Empleados;
        public static List<Customers> Clientes;
        public static List<Orders> Pedidos;
        public static List<Products> Productos;

        public static List<Products_by_Category> ProductosCategoria;
        public static List<Invoices> Facturas;        
        public static List<Summary_of_Sales_by_Quarter> VentasTrimestrales;
        public static List<Summary_of_Sales_by_Year> VentasAnuales;


        static void Main(string[] args)
        {
            Init();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Escribe 0 (cero) para salir y C para borrar.");
            string valor = "-1";

            while (valor != "0")
            {
                Console.Write("Número de Ejercicio: ");
                valor = Console.ReadLine();

                if (valor != "0")
                {
                    if (valor.ToLower() == "c") Console.Clear();
                    else
                    {
                        var m = typeof(Program).GetMethods().Where(r => r.Name == "Exercise" + valor.PadLeft(2, '0')).FirstOrDefault();
                        if (m != null) _ = m.Invoke(null, null);
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No existe el ejercicio {0}.", valor.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
        }

        ///<summary>
        /// Colección: Numeros
        /// TODO (Dificultad: Baja)
        ///  - Obtener el valor más alto, el valor más bajo, la medía, la suma total de todos los número y el número de elementos.
        ///  - Extraer los números pares
        ///  - Extraer los números última cifra sea el número 7
        ///  - Extraer los números que contenga el número 7
        ///</summary>
        public static void Exercise01()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 01");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var numAlto = Numeros.Max();
            Console.WriteLine("Valor más alto {0}", numAlto);
            
            Console.WriteLine("Valor más bajo {0}", Numeros.Min());
            Console.WriteLine("Media: {0}", Numeros.Average().ToString("N2"));
            Console.WriteLine("Suma Total: {0}", Numeros.Sum());
            Console.WriteLine("Número de elementos: {0}", Numeros.Count());

            Console.WriteLine("Números Pares: {0}", Numeros.Where(r => r % 2 == 0).ToList().PrintElements());
            Console.WriteLine("Números finalizan 7: {0}", Numeros.Where(r => r.ToString()[r.ToString().Length - 1] == '7').ToList().PrintElements());
            Console.WriteLine("Números contienen 7: {0}", Numeros.Where(r => r.ToString().Contains("7")).ToList().PrintElements());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        ///<summary>
        /// Colección: Numeros2
        /// TODO (Dificultad: Media)
        ///  - Genera una nueva colección con el redondeo de los valores. Utiliza el método Decimal.round(x) donde x es la variable que contiene el valor.
        ///  - En una única sentencia rondea y extrae (selecciona) los impares. Utiliza dos métodos Select.
        ///</summary>
        public static void Exercise02()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 02");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Original: {0}", Numeros2.PrintElements());
            Console.WriteLine("Redondeo: {0}", Numeros2.Select(r => Decimal.Round(r)).ToList().PrintElements());
            Console.WriteLine("Redondeo (pares): {0}", Numeros2.Where(r => r % 2 == 0).Select(r => Decimal.Round(r)).ToList().PrintElements());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        ///<summary>
        /// Colección: Frutas
        /// TODO (Dificultad: Media)
        ///  - Extraer las frutas que comienza por M y termina por A.
        ///</summary>
        public static void Exercise03()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 03");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Frutas: {0}", Frutas.Where(r => r[0] == 'M' && r[r.Length - 1] == 'a').ToList().PrintElements());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        ///<summary>
        /// Colección: Frutas
        /// TODO (Dificultad: Media)
        ///  - Muestra el total de letras utilizadas para escribir el nombre de todas las frutas. Utiliza x.length y .Sum()
        ///</summary>
        public static void Exercise04()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 04");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Letras: {0}", Frutas.Sum(r => r.Length));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        ///<summary>
        /// Colección: Empleados
        /// TODO (Dificultad: Baja)
        ///  - Extraer el nombre, la dirección, la ciudad y la región de los empleados.
        ///</summary>
        public static void Exercise05()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 05");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var empleados = Empleados.Select(r => new { Name = (r.FirstName + " " + r.LastName), r.Address, r.City, r.Region }).ToList();

            Console.WriteLine("EMPLEADOS");
            foreach (var item in empleados)
            {
                Console.WriteLine("{0} - {1} {2} ({3})", item.Name, item.Address.Replace(Environment.NewLine, ""), item.Region, item.City);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }


        /// Colección: Empleados
        /// TODO (Dificultad: Baja)
        ///  - Extraer el nombre, la dirección, la ciudad y la región de los empleados.
        ///</summary>
        public static void Exercise06()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 06");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var empleados = Empleados.Select(r => new { Name = (r.FirstName + " " + r.LastName), r.Address, r.City, r.Region }).ToList();

            Console.WriteLine("EMPLEADOS");
            foreach (var item in empleados)
            {
                Console.WriteLine("{0} - {1} {2} ({3})", item.Name, item.Address.Replace(Environment.NewLine, ""), item.Region, item.City);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;


        }


        /// Colección: Empleados
        /// TODO (Dificultad: Baja)
        ///  - Extraer el nombre, la dirección, la ciudad y la región de los empleados que viven en USA.
        ///</summary>
        public static void Exercise07()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 07");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var empleados = Empleados
                .Where(r => r.Country == "USA")
                .Select(r => new { Name = (r.FirstName + " " + r.LastName), r.Address, r.City, r.Region })
                .ToList();

            Console.WriteLine("EMPLEADOS DE USA");
            foreach (var item in empleados)
            {
                Console.WriteLine("{0} - {1} {2} ({3})", item.Name, item.Address.Replace(Environment.NewLine, ""), item.Region, item.City);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// Colección: Empleados
        /// TODO (Dificultad: Media)
        ///  - Extraer el nombre, la dirección, la ciudad y la región de los empleados mayores de 50 años. La fecha actual se obtiene Datetime.Now.
        ///</summary>
        public static void Exercise08()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 08");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var empleados = Empleados
                .Where(r => r.BirthDate <= DateTime.Now.AddDays(-50) && r.BirthDate != null)
                .Select(r => new { Name = (r.FirstName + " " + r.LastName), r.Address, r.City, r.Region, r.BirthDate })
                .ToList();

            Console.WriteLine("EMPLEADOS DE MAYORES DE 50 AÑOS");
            foreach (var item in empleados)
            {
                Console.WriteLine("{0} - {1} {2} ({3}) - {4}", item.Name, item.Address.Replace(Environment.NewLine, ""), item.Region, item.City, item.BirthDate.GetValueOrDefault().ToShortDateString());
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }


        /// Colección: VentasAnuales
        /// TODO (Dificultad: Baja)
        ///  - Total de ventas del año 1996.
        ///</summary>
        public static void Exercise09()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 09");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Ventas 1996: {0}", VentasAnuales
                .Where(r => r.ShippedDate.GetValueOrDefault().Year == 1996)
                .Sum(r => r.Subtotal));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }


        /// Colección: Varias
        /// TODO (Explicación)
        ///  - Reflexiona el ejemplo con el método INTERSECT que nos propociona la interseción (coincidencias) entre dos colecciones.
        ///  - Reflexiona su utiliza en el segundo ejemplo.
        ///</summary>
        public static void Exercise10()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 10");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            //EJEMPLO
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string[] saludos1 = new string[] { "hola", "ey", "buenos días", "buenas noches", "buen finde", "adios" };
            string[] saludos2 = new string[] { "como estas", "que tal", "hola", "buenos días", "ciao" };

            List<string> intersect = saludos1
                .Intersect(saludos2)
                .ToList();

            foreach (string value in intersect)
            {
                Console.WriteLine(value);
            }


            //TODO Queremos saber que clientes tiene en comun Andrew y Laura
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //1. Seleccionamos los clientes del empleado 2 (Andrew Fuller) utilizando la tabla de pedidos, son 59. 
            var idClientesAndrew = Pedidos
                .Where(r => r.EmployeeID == 2)
                .Select(r => r.CustomerID)
                .Distinct()
                .ToList();

            //2. Seleccionamos los clientes del empleado 8 (Laura Callahan) utilizando la tabla de pedidos, son 56.
            var idClientesLaura = Pedidos
                .Where(r => r.EmployeeID == 8)
                .Select(r => r.CustomerID)
                .Distinct()
                .ToList();

            //3. Utilizamos Intersect para conocer las coincidencias, son 38.
            var idClientesComunes = idClientesAndrew.Intersect(idClientesLaura).ToList();

            //4. Creamos una lista con los clientes comunes. La condición es que CustomerID este incluido en la lista de IDs comunes.
            var clientesComunes = Clientes
                .Where(r => idClientesComunes.Contains(r.CustomerID))
                .ToList();

            Console.WriteLine("Clientes comúnes entre Andrew y Laura");
            foreach (var c in clientesComunes)
            {
                Console.WriteLine("{0}# {1} - {2}", c.CustomerID, c.CompanyName, c.Country);
            }
        }


        /// Colección: ProductosCategoria
        /// TODO (Dificultad: Baja)
        ///  - Extraer listado de Condimentos, CategoryName debe ser igual a Condiments
        ///  - Ordenarlos por la cantidad de Stock
        ///  - Coger los 3 productos de categoría Condimentos con menor stock, utiliza o finaliza con .Take(3).ToList()
        ///</summary>
        public static void Exercise11()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 11");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var condimentos = ProductosCategoria
                .Where(r => r.CategoryName == "Condiments")
                .OrderBy(r => r.UnitsInStock)
                .Take(3)
                .ToList();

            foreach (var item in condimentos)
            {
                Console.WriteLine("{0} - Stock {1}", item.ProductName, item.UnitsInStock);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }


        /// Colección: ProductosCategoria
        /// TODO (Dificultad: Baja)
        ///  - Extraer nombre del producto que comienza por la palabra Louisiana y cada unidad contiene 32 bottles.
        ///    Utiliza las propiedades ProductName y QuantityPerUnit
        ///</summary>
        public static void Exercise12()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 12");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var productos = ProductosCategoria
                .Where(r => r.ProductName.StartsWith("Louisiana") && r.QuantityPerUnit.Contains("32"))
                .ToList();

            foreach (var item in productos)
            {
                Console.WriteLine("{0} - Cantidad (unidad): {1}", item.ProductName, item.QuantityPerUnit);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }


        /// Colección: Pedidos (solo para carga local)
        /// TODO (Dificultad: Alta)
        ///  - Extraer el identificador del pedido y total del mismo (este dato es calculado, no existe en la base de datos)
        ///    Calculamos el total sumando la multiplicación de la cantidad y el precio de la propiedad Order_Details 
        ///</summary>
        public static void Exercise13()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 13");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var pedidos = Pedidos
                .Select(r => new { r.OrderID, Total = r.Order_Details.Sum(s => s.Quantity * s.UnitPrice) })
                .ToList();

            foreach (var item in pedidos)
            {
                Console.WriteLine("#{0} -> {1} Euros", item.OrderID, item.Total.ToString("N2"));
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// Colección: Empleados
        /// TODO (Dificultad: Media)
        ///  - Cual es el mayor de los empleados que reporta al empleado 5 (Steven Buchanan).
        ///  - Filtra, usuarios que reporan a Steven utilizando la propiedad ReportTo
        ///  - Ordena, por fecha de naciemiento
        ///  - Selecciona el primero con .FirstOrDefault() o .First() o .Take(1)
        ///</summary>
        public static void Exercise14()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 14");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var empleado = Empleados
                .Where(r => r.ReportsTo == 5)
                .OrderBy(r => r.BirthDate)
                .FirstOrDefault();

            Console.WriteLine("{0} {1} {2}", empleado.FirstName, empleado.LastName, empleado.BirthDate.GetValueOrDefault().ToShortDateString());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// Colección: Productos
        /// TODO (Dificultad: Media)
        ///  - De los diez productos con más stock extraer los cinco de mayor precio.
        ///</summary>
        public static void Exercise15()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ejercicio 15");
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            var productos = Productos
                .OrderByDescending(r => r.UnitsInStock)
                .Take(10)
                .OrderByDescending(r => r.UnitPrice)
                .Take(5)
                .ToList();

            foreach (var item in productos)
            {
                Console.WriteLine("{0} - Stock: {1} - Precio: {2}", item.ProductName, item.UnitsInStock, item.UnitPrice);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// Inicializa las colecciones utilizadas en el Ejercicio
        /// </summary>
        static void Init() 
        {
            db = new ModelNorthwind();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            Numeros = new List<int>() { 12, 125, 52, 171, 128, 51, 12, 515, 77, 211, 997, 62, 1216, 11, 921 };

            Numeros2 = new List<decimal>() { 2.4M, 1.25M, 1.52M, 1371.8551M, 28, 551, 1.2M, 515.16M, 97.345M, 2.11M, 97, 6.51252M, 12.16M, 1.1M, 9.321M };
            
            Frutas = new List<string>() { "Manzana", "Banana", "Fresa", "Mango", "Naraja", "Pera", "Limón", "Maracuya", "Pomelo", "Sandía", "Platano", "Arandanos", "Uva" };

            Gente = new List<Persona>() {
                new Persona("Bill", "Smith", 17),
                new Persona("Sarah", "Jones", 22),
                new Persona("Stacy","Baker", 21),
                new Persona("Vivianne","Dexter", 69 ),
                new Persona("Bob","Smith", 16 ),
                new Persona("Brett","Baker", 51 ),
                new Persona("Mark","Parker", 19),
                new Persona("Alice","Thompson", 18),
                new Persona("Evelyn","Thompson", 58 ),
                new Persona("Mort","Martin", 15),
                new Persona("Eugene","deLauter", 84 ),
                new Persona("Gail","Dawson", 19 ),
            };

            /*
             * Selecciona la carga de datos local u online. Comenta o descomenta el método deseado.
             */

            //Realizamos la carga de datos de la base de datos local
            LoadDataLocalDB();

            //Realizamos la carga de datos del APIRest online
            //LoadDataAPIRest();
        }


        /// <summary>
        /// Carga datos de la base datos local
        /// </summary>
        static void LoadDataLocalDB() 
        {
            Empleados = db.Employees.ToList();
            Clientes = db.Customers.ToList();
            Pedidos = db.Orders.Include(r => r.Order_Details).ToList();
            Productos = db.Products.ToList();
            ProductosCategoria = db.Products_by_Category.ToList();
            Facturas = db.Invoices.ToList();
            VentasTrimestrales = db.Summary_of_Sales_by_Quarter.ToList();
            VentasAnuales = db.Summary_of_Sales_by_Year.ToList();
        }


        /// <summary>
        /// Carga datos del APIRest
        /// </summary>
        static void LoadDataAPIRest()
        {
            HttpResponseMessage r;
            var http = new HttpClient();

            r = http.GetAsync("http://northwind.demos.network/api/employees?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) Empleados = r.Content.ReadAsAsync<List<Employees>>().Result;

            r = http.GetAsync("http://northwind.demos.network/api/customers?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) Clientes = r.Content.ReadAsAsync<List<Customers>>().Result;

            r = http.GetAsync("http://northwind.demos.network/api/orders?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) Pedidos = r.Content.ReadAsAsync<List<Orders>>().Result;

            r = http.GetAsync("http://northwind.demos.network/api/products?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) Productos = r.Content.ReadAsAsync<List<Products>>().Result;

            r = http.GetAsync("http://northwind.demos.network/api/products_by_category?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) ProductosCategoria = r.Content.ReadAsAsync<List<Products_by_Category>>().Result;

            r = http.GetAsync("http://northwind.demos.network/api/invoices?type=json").Result;
            if (r.StatusCode == System.Net.HttpStatusCode.OK) Facturas = r.Content.ReadAsAsync<List<Invoices>>().Result;

            //No disponible para cargar Online
            VentasTrimestrales = new List<Summary_of_Sales_by_Quarter>();
            
            //No disponible para cargar Online
            VentasAnuales = new List<Summary_of_Sales_by_Year>();
        }
    }

    public class Persona
    {
        public Persona(string nombre, string apellidos, int edad)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
        }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellidos} {Edad.ToString("N0")}";
        }
    }
}
