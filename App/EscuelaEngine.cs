using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }
        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria, ciudad: "Bogotá", pais: "Colombia", direccion: "Cr 9 calle 72");
            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones(5);
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprEval = false)
        {
            foreach (var obj in dic)
            {
                Printer.WriteTitle(obj.Key.ToString());

                foreach (var val in obj.Value)
                {
                    switch (obj.Key)
                    {
                        case LlaveDiccionario.Evaluación:
                            if (imprEval)
                                Console.WriteLine(val);
                            break;
                        case LlaveDiccionario.Escuela:
                            Console.WriteLine($"Escuela: {val}");
                            break;
                        case LlaveDiccionario.Alumno:
                            Console.WriteLine($"Alumno: {val.Nombre}");
                            break;
                        case LlaveDiccionario.Curso:
                            var curtemp = val as Curso;
                            if (curtemp != null)
                                Console.WriteLine($"Curso: {curtemp.Nombre} Cantidad Alumnos: {curtemp.Alumnos.Count}");
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            List<Evaluación> lstEvaluación = new List<Evaluación>();
            List<Alumno> listaAlumnos = new List<Alumno>();
            List<Asignatura> listaAsignaturas = new List<Asignatura>();

            foreach (var curso in Escuela.Cursos)
            {
                listaAlumnos.AddRange(curso.Alumnos);
                listaAsignaturas.AddRange(curso.Asignaturas);

                foreach (var alumno in curso.Alumnos)
                {
                    lstEvaluación.AddRange(alumno.Evaluaciones);
                }
            }
            diccionario.Add(LlaveDiccionario.Alumno, listaAlumnos);
            diccionario.Add(LlaveDiccionario.Asignatura, listaAsignaturas);
            diccionario[LlaveDiccionario.Evaluación] = lstEvaluación;
            // diccionario.Add(LlaveDiccionario.Evaluación, lstEvaluación.Cast<ObjetoEscuelaBase>());

            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
           bool traeEvaluaciones = true,
           bool tareAlumnos = true,
           bool traeAsignaturas = true,
           bool traeCursos = true)
        {
            int dummy = 0;
            return GetObjetosEscuela(out dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
           out int conteoEvaluaciones,
           bool traeEvaluaciones = true,
           bool tareAlumnos = true,
           bool traeAsignaturas = true,
           bool traeCursos = true)
        {
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
          out int conteoEvaluaciones,
          out int conteoCursos,
          bool traeEvaluaciones = true,
          bool tareAlumnos = true,
          bool traeAsignaturas = true,
          bool traeCursos = true)
        {
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
          out int conteoEvaluaciones,
          out int conteoCursos,
          out int conteoAsignaturas,
          bool traeEvaluaciones = true,
          bool tareAlumnos = true,
          bool traeAsignaturas = true,
          bool traeCursos = true)
        {
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool tareAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true)
        {
            conteoEvaluaciones = conteoAsignaturas = conteoAlumnos = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            if (traeCursos) listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traeAsignaturas) listaObj.AddRange(curso.Asignaturas);

                if (tareAlumnos) listaObj.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();
        }

        public List<ObjetoEscuelaBase> GetObjetosEscuela()
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            listaObj.AddRange(Escuela.Cursos);

            foreach (var curso in Escuela.Cursos)
            {
                listaObj.AddRange(curso.Asignaturas);
                listaObj.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listaObj.AddRange(alumno.Evaluaciones);
                }
            }

            return listaObj;
        }

        #region Metodos de Carga

        private void CargarEvaluaciones(int cantEvaluaciones)
        {
            //Cuantos milisegundos a pasado desde que se encendió la maquina
            //System.Environment.TickCount
            
            Random rnd = new Random(System.Environment.TickCount);
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        Evaluación[] evaluacións = new Evaluación[cantEvaluaciones];
                      
                        for (int i = 0; i < cantEvaluaciones; i++)
                        {
                            evaluacións[i] = new Evaluación
                            {
                                Alumno = alumno,
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre}-Eval#{i + 1}",
                                Nota = MathF.Round((5 * (float)rnd.NextDouble()),2)
                            };
                        }
                        alumno.Evaluaciones.AddRange(evaluacións);
                        asignatura.Evaluaciones.AddRange(evaluacións);
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>{
                    new Asignatura{Nombre = "Matemáticas"},
                    new Asignatura{Nombre = "Educación Física"},
                    new Asignatura{Nombre = "Castellano"},
                    new Asignatura{Nombre = "Ciencias Naturales"},
                    new Asignatura{Nombre = "Español"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre = { "Alba", "Felipe", "Eusebio", "Earld", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruíz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "" };

            var listaAlumnos = from n1 in nombre
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy(al => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>{
                new Curso {Nombre = "101", Jornada = TiposJornada.Mañana},
                new Curso {Nombre = "201", Jornada = TiposJornada.Mañana},
                new Curso {Nombre = "301", Jornada = TiposJornada.Mañana},
                new Curso {Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso {Nombre = "501", Jornada = TiposJornada.Tarde}
            };

            foreach (var curso in Escuela.Cursos)
            {
                curso.Alumnos = GenerarAlumnosAlAzar(new Random().Next(5, 20));
            }
        }

        #endregion

    }
}