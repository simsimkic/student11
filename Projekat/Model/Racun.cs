// File:    Racun.cs
// Created: Saturday, July 4, 2020 1:26:37 PM
// Purpose: Definition of Class Racun

using System;
using System.Collections.Generic;

namespace Model
{
   public class Racun
   {
        public Racun()
        {

        }
        public Racun(int sifra, string apotekar, DateTime datum, Dictionary<string, int> lekovi)
        {
            Sifra = sifra;
            Apotekar = apotekar;
            Datum = datum;
            Lekovi = lekovi;
        }

        public int Sifra { get; set; }
        public string Apotekar { get; set; }
        public DateTime Datum { get; set; }
        public Dictionary<string, int> Lekovi { get; set; }
        public double UkupnaCena { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Racun racun &&
                   Sifra == racun.Sifra;
        }

        public override string ToString()
        {
            Console.WriteLine("Sifra racuna:\tIme apotekara:\tDatum izdavanja:\tLekovi:\t\t\tUkupna cena:");
            String recnikLekova = "";
            foreach(String item in Lekovi.Keys)
            {
                recnikLekova += item + ":" + Lekovi[item].ToString() + " ";
            }
            return Sifra.ToString() + "\t\t" + Apotekar + "\t\t" + Datum.ToString() + "\t" + recnikLekova + "\t" + UkupnaCena.ToString();
        }
    }
}