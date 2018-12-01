using System;

namespace CoreEscuela.Entidades
{
    public class Evaluación : ObjetoEscuelaBase
    {
        public float Nota { get; set; }

        public Alumno Alumno { get; set; }

        public Asignatura Asignatura { get; set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}