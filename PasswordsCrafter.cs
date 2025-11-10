namespace PasswordsCrafter2
{
    /* ----------------------------------------------------------------------------------------
     * ----------------------- Programa generador de Password ---------------------------------
     * ----------------------------------------------------------------------------------------
     * ------------------------------- PASSWORD RAIDER ----------------------------------------
     * ----------------------------------------------------------------------------------------
     * -------------------------------------- n0bdy -------------------------------------------
     * ---------- Inicio versión 0.0.1 - Delta ----- 2/8/24 -----------------------------------
     * ----------- Fin version 0.1 --- Debug -------        -----------------------------------
     * ----------- Fin version estable 1.0 ---------        ----------------------------------- 
     * ---------------------------------------------------------------------------------------- */

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main()
        {
            int length = 0;

            // Solicitar la longitud de las combinaciones y validar la entrada

            while (true)
            {
                Console.Write("Introduce la longitud máxima de los passwords a generar ......... : ");
                string input = Console.ReadLine();

                try
                {
                    length = int.Parse(input);
                    if (length <= 0)
                    {
                        Console.WriteLine("Por favor, introduce un número mayor que 0.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduce un número entero.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Número demasiado grande. Por favor, introduce un número más pequeño.");
                }
            }

            List<string> PasswordsCrafter = new List<string>();
            Stopwatch stopwatch = new();

            try
            {
                stopwatch.Start();
                GenerateCombinations(new char[length] , 0 , length , PasswordsCrafter);
                stopwatch.Stop();

                // Convertir la lista a array

                string[] combinationsArray = [.. PasswordsCrafter];

                // Mostrar la cantidad de combinaciones generadas y el tiempo transcurrido

                Console.WriteLine($"Cantidad de combinaciones generadas: {combinationsArray.Length}");
                Console.WriteLine($"Tiempo transcurrido: {stopwatch.Elapsed.TotalSeconds} segundos");

                // Preguntar si se desea guardar en un archivo

                Console.Write("¿Deseas guardar las combinaciones en un archivo de texto? (s/n): ");
                string saveToFile = Console.ReadLine().ToLower();

                if (saveToFile == "s" || saveToFile == "si")
                {
                    File.WriteAllLines("PasswordsCrafter.txt" , combinationsArray , Encoding.UTF8);
                    Console.WriteLine("Las combinaciones se han guardado en el archivo 'PasswordsCrafter.txt'.");
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Se ha producido un error de memoria debido a la gran cantidad de combinaciones posibles. Prueba con una longitud menor.");
            }
        }

        static void GenerateCombinations(char[] currentCombination , int position , int length , List<string> PasswordsCrafter)
        {
            int cursor_col;
            if (position == length)
            {

                PasswordsCrafter.Add(new string(currentCombination));
                File.WriteAllLines("PasswordsCrafter.txt" , currentCombination , Encoding.UTF8);
                return;
            }


            for (char c = (char) 32; c <= (char) 126; c++) // Rango ASCII imprimible (32-126)
            {
                currentCombination[position] = c;
                GenerateCombinations(currentCombination , position + 1 , length , PasswordsCrafter);

            }

            // Añadir la letra 'ñ' como un carácter extra

            currentCombination[position] = 'ñ';
            GenerateCombinations(currentCombination , position + 1 , length , PasswordsCrafter);
            Console.Write("Creando conbinación Nº " + position);
            cursor_col = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(0 , cursor_col);
        }
    }
}