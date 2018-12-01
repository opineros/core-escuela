using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000,1000,3);
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad:10);

            //WriteLine("Curso.Hash" + engine.Escuela.GetHashCode()); //Se pide el hash que identifica el objeto
            //Predicate<Curso> miAlgoritmo = Predicate; //Encapsulacion de algoritmos => delegado
            //engine.Escuela.Cursos.RemoveAll(miAlgoritmo);

            // engine.Escuela.Cursos.RemoveAll(delegate (Curso cur)
            // {
            //     return cur.Nombre.Equals("301");
            // });

            //Expreciones lambda => tambien son delegados
            //engine.Escuela.Cursos.RemoveAll(curso => curso.Nombre.Equals("501") && curso.Jornada == TiposJornada.Mañana);

            //ImpimirCursosEscuela(engine.Escuela);

            //int dommy = 0;
            // var listaObjetos = engine.GetObjetosEscuela(
            //     out int conteoEvaluaciones,
            //     out dommy,
            //     out dommy,
            //     out dommy
            //    );

            // var listaILugar = from obj in listaObjetos
            //                   where obj is Alumno
            //                   select (Alumno)obj;

            //engine.Escuela.LimpiarLugar();

            //var dictemp = engine.GetDiccionarioObjetos();
            //engine.ImprimirDiccionario(dictemp, true);

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evaList = reporteador.GetListaEvaluaciones();
            var listaAsg = reporteador.GetListaEvaluaciones();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnnPorAsignatura();
            var listTopPromedioAsignatura = reporteador.GetMejoresPromedioXAsignatura(LlavesAsignatura.Español, 20);

            Printer.WriteTitle("Captura de una Evaluación por Consola");
            var newEval = new Evaluación();
            string nombre, notaString;

            WriteLine("Ingrese el nombre de la evaluación");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacio");
                WriteLine("Saliendo del programa");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluación a sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PresioneENTER();
            notaString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacio");
                WriteLine("Saliendo del programa");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notaString);
                    if (newEval.Nota < 0 || newEval.Nota > 5)
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    WriteLine("La nota de la evaluación a sido ingresado correctamente");
                }
                catch (ArgumentOutOfRangeException arg)
                {
                    Printer.WriteTitle(arg.Message);
                    WriteLine("Saliendo del programa");
                }
                catch
                {
                    Printer.WriteTitle("El valor de la nota no es un número valido");
                    WriteLine("Saliendo del programa");
                }
                finally
                {
                    Printer.WriteTitle("FINALLY");
                    Printer.Beep(2500, 500, 3);
                }
            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            //Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIÓ");
        }

        private static bool Predicate(Curso cursobj)
        {
            return cursobj.Nombre.Equals("301");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
