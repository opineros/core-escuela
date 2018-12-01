using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Asignatura
    {
        public string UniqueId { get; set; }
        public string Nombre { get; set; }

        private List<Evaluación> _evaluaciones;
        public List<Evaluación> Evaluaciones
        {
            get { return _evaluaciones ?? new List<Evaluación>(); }
            set { _evaluaciones = value; }
        }

        public Asignatura() => UniqueId = Guid.NewGuid().ToString();

    }
}