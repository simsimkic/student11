// File:    Korisnik.cs
// Created: Saturday, July 4, 2020 1:26:37 PM
// Purpose: Definition of Class Korisnik

using System;

namespace Model
{
   public class Korisnik
   {

        public Korisnik() 
        {
        }

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, TipKorisnika tipKorisnika)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            TipKorisnika = tipKorisnika;
        }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public TipKorisnika TipKorisnika { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Korisnik korisnik &&
                   KorisnickoIme == korisnik.KorisnickoIme;
        }

        public override string ToString()
        {
            String tip = "Administrator";
            if(TipKorisnika == TipKorisnika.apotekar)
                tip = "Apotekar";
            else if(TipKorisnika == TipKorisnika.lekar)
                tip = "Lekar";
            return KorisnickoIme + "\t\t" + Ime + "\t\t" + Prezime + "\t\t\t" + tip;
        }
    }
}