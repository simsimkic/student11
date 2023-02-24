// File:    Lek.cs
// Created: Saturday, July 4, 2020 1:26:37 PM
// Purpose: Definition of Class Lek

using System;

namespace Model
{
   public class Lek
   {
        public Lek()
        {
        }

        public Lek(string sifra, string ime, string proizvodjac, bool recept, double cena, bool obrisan = false)
        {
            Sifra = sifra;
            Ime = ime;
            Proizvodjac = proizvodjac;
            Recept = recept;
            Cena = cena;
            Obrisan = obrisan;
        }

        public string Sifra { get; set; }
        public string Ime { get; set; }
        public string Proizvodjac { get; set; }
        public bool Recept { get; set; }
        public double Cena { get; set; }
        public bool Obrisan { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Lek lek &&
                   Sifra == lek.Sifra;
        }

        public override string ToString()
        {
            String prekoRecepta = "Ne";
            if (Recept)
            {
                prekoRecepta = "Da";
            }
            return Sifra + "\t\t" + Ime + "\t\t" + Proizvodjac + "\t\t\t" + prekoRecepta + "\t\t\t\t" + Cena.ToString();
        }
    }
}