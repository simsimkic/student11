// File:    KorisnikService.cs
// Created: Saturday, July 4, 2020 12:27:59 PM
// Purpose: Definition of Class KorisnikService

using Model;
using Projekat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Exceptions
{
   public class KorisnikService
   {
      public Model.Korisnik Login(String korisnickoIme, String lozinka)
      {
            Boolean postojiKorisnik = false;
            Model.Korisnik ulogovaniKorisnik = new Korisnik();
            List<Model.Korisnik> korisnici = korisnikRepository.DobaviSve();
            if (korisnici.Count == 0)
                throw new NotRegisteredUserException();
            for(int i=0; i < korisnici.Count; i++)
                if (korisnici[i].KorisnickoIme.Equals(korisnickoIme))
                {
                    postojiKorisnik = true;
                    ulogovaniKorisnik = korisnici[i];
                }
            if (!postojiKorisnik)
                throw new NotRegisteredUserException();
            else
            {
                if (ulogovaniKorisnik.Lozinka.Equals(lozinka))
                    return ulogovaniKorisnik;
                else
                    throw new InvalidPasswordException();
            }
      }
      
      public Model.Korisnik RegistracijaKorisnika(Model.Korisnik korisnik)
      {
            List<Model.Korisnik> korisnici = korisnikRepository.DobaviSve();
            for (int i = 0; i < korisnici.Count; i++)
                if (korisnici[i].KorisnickoIme.Equals(korisnik.KorisnickoIme))
                    throw new InvalidUsernameException();
            Console.WriteLine("Uspesno kreiran novi korisnik.");
            return korisnikRepository.Kreiraj(korisnik);
      }
      
      public List<Model.Korisnik> PrikaziSveKorisnike(int sortirajPo)
      {
            List<Model.Korisnik> korisnici =  korisnikRepository.DobaviSve();
            if (sortirajPo == 0)
                korisnici = korisnici.OrderBy(o => o.Ime).ToList();
            else if(sortirajPo == 1)
                korisnici = korisnici.OrderBy(o => o.Prezime).ToList();
            else
                korisnici = korisnici.OrderBy(o => o.TipKorisnika).ToList();
            return korisnici;
      }

      public Korisnik DobaviKorisnikaPoImenu(String imeKorisnika)
      {
            List<Model.Korisnik> korisnici = korisnikRepository.DobaviSve();
            Boolean postojiKorisnik = false;
            Model.Korisnik korisnikPoImenu = new Korisnik();
            if (korisnici.Count == 0)
                throw new NotRegisteredUserException();
            for(int i = 0; i < korisnici.Count; i++)
                if (korisnici[i].Ime.Equals(imeKorisnika))
                {
                    postojiKorisnik = true;
                    korisnikPoImenu = korisnici[i];
                    break;
                }
            if (!postojiKorisnik)
                throw new NoUserByNameException();
            return korisnikPoImenu;
      }

      public Repository.KorisnikRepository korisnikRepository = new Repository.KorisnikRepository();
   
   }
}