using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
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

            ImpimirCursosEscuela(engine.Escuela);
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
