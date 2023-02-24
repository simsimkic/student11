// File:    KorisnikController.cs
// Created: Saturday, July 4, 2020 1:26:07 PM
// Purpose: Definition of Class KorisnikController

using Exceptions;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class KorisnikController
   {
      
      public Model.Korisnik Login(String korisnickoIme, String lozinka)
      {
            Model.Korisnik ulogovaniKorisnik = new Model.Korisnik();
            try
            {
                ulogovaniKorisnik = korisnikService.Login(korisnickoIme, lozinka);
                return ulogovaniKorisnik;
            }
            catch(NotRegisteredUserException ex)
            {
                Console.WriteLine("Ne postoji korisnik sa unesenim korisnickim imenom.");
            }
            catch(InvalidPasswordException ex)
            {
                Console.WriteLine("Unesena je pogresna lozinka.");
            }
            return null;
      }
      
      public Model.Korisnik RegistracijaKorisnika(Model.Korisnik korisnik)
      {
            try
            {
                return korisnikService.RegistracijaKorisnika(korisnik);
            }
            catch(InvalidUsernameException ex)
            {
                Console.WriteLine("Postoji korisnik sa unesenim korisnickim imenom.");
            }
            return null;
      }
      
      public List<Model.Korisnik> PrikaziSveKorisnike(int sortirajPo)
      {
            List<Model.Korisnik> korisnici = korisnikService.PrikaziSveKorisnike(sortirajPo);
            if(korisnici.Count == 0)
            {
                Console.WriteLine("Nema korisnika u sistemu.");
                return null;
            }
            return korisnici;
      }
      
      public Exceptions.KorisnikService korisnikService = new Exceptions.KorisnikService();
   
   }
}