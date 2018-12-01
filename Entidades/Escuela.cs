
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    [DebuggerDisplay("AñoFundación = {AñoFundación}, Pais = {Pais}, Ciudad = {Ciudad}, Direccion = {Direccion}, Ceo = {Ceo}")]
    public class Escuela : ObjetoEscuelaBase, ILugar
    {
        public int AñoFundación { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public string Direccion;

        public string Ceo;

        public TiposEscuela TipoEscuela { get; set; }

        public List<Curso> Cursos { get; set; }
        public string Dirección { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Escuela(string nombre, int año) => (Nombre, AñoFundación) = (nombre, año); //Igualación por tuplas - característica de los programas funcionales

        public Escuela(string nombre, int año, TiposEscuela tipo, string pais = "", string ciudad = "", string direccion = "", string ceo = "") =>
            //Asignación de tuplas
            (Nombre, AñoFundación, Pais, Ciudad, Ceo, Direccion) = (nombre, año, pais, ciudad, ceo, direccion);

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} {System.Environment.NewLine}Pais: {Pais}, Ciudad: {Ciudad}";
        }

        public void LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limpiando Escuela...");
            
            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }

            Console.WriteLine($"Escuela {Nombre} Limpia...");
            //Printer.Beep(1000, cantidad:3);
        }
    }
}