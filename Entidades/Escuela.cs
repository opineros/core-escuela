
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Escuela
    {
        private string nombre;
        public string Nombre
        {
            get { return "Copia:" + nombre; }
            set { nombre = value.ToUpper(); }
        }

        public int AñoFundación { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public string Direccion;

        public string Ceo;

        public TiposEscuela TipoEscuela { get; set; }

        public List<Curso> Cursos { get; set; }

        public Escuela(string nombre, int año) => (Nombre, AñoFundación) = (nombre, año); //Igualación por tuplas - característica de los programas funcionales

        public Escuela(string nombre, int año, TiposEscuela tipo, string pais = "", string ciudad = "", string direccion = "", string ceo = "") =>
            //Asignación de tuplas
            (Nombre, AñoFundación, Pais, Ciudad, Ceo) = (nombre, año, pais, ciudad, ceo);

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} {System.Environment.NewLine}Pais: {Pais}, Ciudad: {Ciudad}";
        }
    }
}