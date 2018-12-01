using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Alumno : ObjetoEscuelaBase
    {
        public Alumno()
        {
            Evaluaciones = new List<Evaluación>();
        }

        public List<Evaluación> Evaluaciones { get; set; }

    }
}