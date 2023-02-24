// File:    Recept.cs
// Created: Saturday, July 4, 2020 1:26:37 PM
// Purpose: Definition of Class Recept

using System;
using System.Collections.Generic;

namespace Model
{
   public class Recept
   {
        public Recept()
        {
            Lekovi = new Dictionary<String, int>();
        }
        public Recept(int sifra, string lekar, string jmbgPacijenta, DateTime datum, Dictionary<String, int> lekovi)
        {
            Sifra = sifra;
            Lekar = lekar;
            JmbgPacijenta = jmbgPacijenta;
            Datum = datum;
            Lekovi = lekovi;
        }

        public int Sifra { get; set; }
        public string Lekar { get; set; }
        public string JmbgPacijenta { get; set; }
        public DateTime Datum { get; set; }
        public Dictionary<string, int> Lekovi { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Recept recept &&
                   Sifra == recept.Sifra;
        }

        public override string ToString()
        {
            String recnikLekova = "";
            foreach (String item in Lekovi.Keys)
            {
                recnikLekova += item + ":" + Lekovi[item].ToString() + " ";
            }
            return Sifra.ToString() + "\t\t" + Lekar + "\t\t" + JmbgPacijenta + "\t\t" + Datum.ToString() + "\t" + recnikLekova;
        }
    }
}