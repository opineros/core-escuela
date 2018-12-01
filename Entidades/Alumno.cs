using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Alumno
    {
        public string UniqueId { get; private set; }

        public string Nombre { get; set; }

        private List<Evaluación> _evaluaciones;
        public List<Evaluación> Evaluaciones
        {
            get { return _evaluaciones?? new List<Evaluación>();}
            set { _evaluaciones = value; }
        }

        public Alumno() => UniqueId = Guid.NewGuid().ToString();
    }
}