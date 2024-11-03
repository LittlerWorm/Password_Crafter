/* ----------------------------------------------------------------------------------------
   * ----------------------- Programa generador de listas  ---------------------------------
   * ----------------------------------------------------------------------------------------
   * ------------------------------- PASSWORDS CRAFTER ----------------------------------------
   * ----------------------------------------------------------------------------------------
   * -------------------------------------- n0bdy -------------------------------------------
   * ---------- Inicio versión 0.0.1 - Delta ----- 2/8/24 -----------------------------------
   * ----------- Fin version 0.1 --- Debug -------        -----------------------------------
   * ----------- Fin version estable 1.0 ---------        ----------------------------------- 
   * ---------------------------------------------------------------------------------------- */



using System.Diagnostics;
using System.IO;
using System.Text;
//using System.Timers;
//using System.ComponentModel.Design;
//using System.ComponentModel.DataAnnotations;

class Passwords_Crafter
{

    private static void Main()
    {
        // Declaracion de variables 
        bool error = false;
        int passLength;
        string inputText, pathToSave, nameFileList = "Password_Crafter.txt";

        // -------------------  Solicita el nombre para el archivo de la lista ------------------------------------

        // €€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€  ARREGLAR CONTROL DE FORMATO, €€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€

        while (true)
        {
            try
            {
                Console.WriteLine("***********************************************************");
                Console.WriteLine("******            FORMATO PARA EL NOMBRE             ******");
                Console.WriteLine("***********************************************************");
                Console.WriteLine("\nIntroduce letras mayusculas o minusculas, y/o numeros, no admite puntos, comas, o caracteres especiales," +
                    "\n no poner el formato de salidad del archivo, simpre sera .txt por defecto\n");
                Console.Write("Indica el nombre del fichero  para la lista...:");
                nameFileList = Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("\nEntrada no válida. Por favor, introduce un nombre válido.");
                error = true;
            }
            if (error)

                continue;
            else
                break;
        }

        // Solicitar la longitud de las combinaciones y validar la entrada

        while (true)
        {
            try
            {
                Console.Write("Introduce la longitud máxima de los passwords a generar ......... : ");
                inputText = Console.ReadLine();
                passLength = int.Parse(inputText);

                if (passLength <= 0)
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
            catch (ArgumentNullException)
            {
                Console.WriteLine("Introduzca un número entero.");
            }
        }

        // ---------------- Genera el archivo donde guardar la lista

        //  pathToSave = $"C:\\Users\\n0bdy\\Documents\\{nameFileList}.txt"; // ######################### ARREGLAR LA GESTION DE DIRECTORIO ##########################

        pathToSave = $"D:\\PCD\\{nameFileList}.txt"; // ######################### ARREGLAR LA GESTION DE DIRECTORIO ##########################

        pathToSave = GetUniqueFileName(pathToSave);
        Console.WriteLine($"La ubicacion donde se guardará lista es {pathToSave}");

        if (!File.Exists(pathToSave)) // ########################## CONTROLAR SI EXISTE EL FICHERO , PARA AGREGAR NUMERO AL FINAL DEL NOMBRE ############################
        {
            using StreamWriter generateFileList = File.CreateText(pathToSave);
            try
            {
                GenerateCombinations(passLength, generateFileList);
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Sin permiso de escrutora en el path usado, o espacion insuficiente en disco.");
            }
        }
        // ------------ Compruba si el nombre del archivo existe y agrega numero para no sobre escribir

        static string GetUniqueFileName(string path)
        {
            if (!File.Exists(path))
            {
                return path;  // Si el archivo no existe, devolver el mismo nombre.
            }

            string directory = Path.GetDirectoryName(path) ?? "";  // Obtener el directorio del archivo.
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);  // Nombre del archivo sin extensión.
            string extension = Path.GetExtension(path);  // Obtener la extensión del archivo.

            int counter = 1;  // Inicializar contador.
            string newPath;

            // Generar un nuevo nombre incrementando un contador hasta encontrar un nombre único.
            do
            {
                newPath = Path.Combine(directory, $"{fileNameWithoutExtension}({counter}){extension}");
                counter++;
            } while (File.Exists(newPath));

            return newPath;  // Devolver el nuevo nombre único del archivo.
        }



        static void GenerateCombinations(int length, StreamWriter generateFileList)
        {
            Stack<(char[] combination, int position)> stack = new Stack<(char[], int)>();
            char[] initialCombination = new char[length];
            stack.Push((initialCombination, 0));

            while (stack.Count > 0)
            {
                var (currentCombination, position) = stack.Pop();

                // Caso base: cuando se alcanza la longitud deseada.
                if (position == length)
                {
                    generateFileList.WriteLine(new string(currentCombination));
                    continue;
                }

                // Generar combinaciones para caracteres imprimibles (32-126).
                for (char c = (char)32; c <= (char)126; c++)
                {
                    char[] newCombination = (char[])currentCombination.Clone();
                    newCombination[position] = c;
                    stack.Push((newCombination, position + 1));
                }

                // Añadir la letra 'ñ' y 'Ñ' como un carácter extra.

                char[] newCombinationWith_ñ = (char[])currentCombination.Clone();
                newCombinationWith_ñ[position] = (char)209;
                stack.Push((newCombinationWith_ñ, position + 1));

                char[] newCombinationWith_Ñ = (char[])currentCombination.Clone();
                newCombinationWith_Ñ[position] = (char)211;
                stack.Push((newCombinationWith_Ñ, position + 1));
            }
        }
    }
}