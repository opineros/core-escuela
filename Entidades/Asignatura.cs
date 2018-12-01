using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Asignatura : ObjetoEscuelaBase
    {
        public Asignatura()
        {
            Evaluaciones = new List<Evaluación>();
        }

        public List<Evaluación> Evaluaciones { get; set; }

    }
}