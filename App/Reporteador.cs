using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;

        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjetoEscuela)
        {
            if (dicObjetoEscuela == null)
                throw new ArgumentNullException(nameof(dicObjetoEscuela));
            _diccionario = dicObjetoEscuela;
        }

        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluación, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluación>();
            }
            return new List<Evaluación>();
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from eva in listaEvaluaciones
                    select eva.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluaXAsig()
        {
            Dictionary<string, IEnumerable<Evaluación>> dicRta = new Dictionary<string, IEnumerable<Evaluación>>();

            var listaAsignaturas = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsignaturas)
            {
                var evalAsig = (from eval in listaEval
                                where eval.Asignatura.Nombre.Equals(asig)
                                select eval);

                dicRta.Add(asig, evalAsig);
            }

            return dicRta;
        }

        public Dictionary<string, IEnumerable<object>> GetPromeAlumnnPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicEvalXAsig = GetDicEvaluaXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promAlumn = (from eval in asigConEval.Value
                                 group eval by new
                                 {
                                     eval.Alumno.UniqueId,
                                     eval.Alumno.Nombre
                                 }
                                 into grupoEvalAlumno
                                 select new AlumnoPromedio
                                 {
                                     AlumnoId = grupoEvalAlumno.Key.UniqueId,
                                     AlumnoNombre = grupoEvalAlumno.Key.Nombre,
                                     Promedio = grupoEvalAlumno.Average(evaluacion => evaluacion.Nota)
                                 });

                rta.Add(asigConEval.Key, promAlumn);
            }

            return rta;
        }

        public List<object> GetMejoresPromedioXAsignatura(string asignatura, int top = 10)
        {
            var rta = GetPromeAlumnnPorAsignatura();
            var asig = rta.GetValueOrDefault(asignatura).OrderByDescending(prom => ((AlumnoPromedio)prom).Promedio).Take(top);

            return asig.ToList();
        }
    }
}