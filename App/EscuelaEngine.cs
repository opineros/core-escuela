using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public class EscuelaEngine
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

        private void CargarEvaluaciones(int cantEvaluaciones)
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        Evaluación[] evaluacións = new Evaluación[cantEvaluaciones];
                        Random rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < cantEvaluaciones; i++)
                        {
                            Evaluación evaluacion = new Evaluación
                            {
                                Alumno = alumno,
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre}-Eval#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble())
                            };
                            evaluacións[i] = evaluacion;
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
    }
}