// File:    KorisnikRepository.cs
// Created: Saturday, July 4, 2020 1:23:53 PM
// Purpose: Definition of Class KorisnikRepository

using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class KorisnikRepository
   {

        private String putanja = @"..\..\Korisnik.txt";
      
      public Model.Korisnik Kreiraj(Model.Korisnik korisnik)
      {
            if (File.Exists(putanja))
            {
                TextReader ucitaj = new StreamReader(putanja);
                String json = ucitaj.ReadToEnd();
                ucitaj.Close();
                List<Model.Korisnik> korisnici = JsonConvert.DeserializeObject<List<Model.Korisnik>>(json);
                korisnici.Add(korisnik);
                json = JsonConvert.SerializeObject(korisnici);
                File.Create(putanja).Close();
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }
            else
            {
                List<Model.Korisnik> korisnici = new List<Model.Korisnik>();
                korisnici.Add(korisnik);
                Console.WriteLine("Uspesno kreiran novi korisnik!");
                File.Create(putanja).Close();
                String json = JsonConvert.SerializeObject(korisnici);
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }

            return korisnik;
        }
      
      public List<Model.Korisnik> DobaviSve()
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            List<Model.Korisnik> korisnici = JsonConvert.DeserializeObject<List<Model.Korisnik>>(json);
            ucitaj.Close();
            return korisnici;
        }
   
   }
}