// File:    RacunRepository.cs
// Created: Saturday, July 4, 2020 1:23:53 PM
// Purpose: Definition of Class RacunRepository

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class RacunRepository
   {

        private String putanja = @"..\..\Racun.txt";

        public List<Model.Racun> DobaviRacunePoApotekaru(String apotekar)
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            ucitaj.Close();
            List<Model.Racun> racuniPoApotekaru = JsonConvert.DeserializeObject<List<Model.Racun>>(json);
            for (int i = racuniPoApotekaru.Count-1; i >=0; i--)
                if (!racuniPoApotekaru[i].Apotekar.Equals(apotekar))
                    racuniPoApotekaru.RemoveAt(i);
            return racuniPoApotekaru;
        }
      
      public Model.Racun Kreiraj(Model.Racun racun)
      {
            if (File.Exists(putanja))
            {
                TextReader ucitaj = new StreamReader(putanja);
                String json = ucitaj.ReadToEnd();
                ucitaj.Close();
                List<Model.Racun> racuni = JsonConvert.DeserializeObject<List<Model.Racun>>(json);
                racuni.Add(racun);
                json = JsonConvert.SerializeObject(racuni);
                File.Create(putanja).Close();
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }
            else
            {
                List<Model.Racun> racuni = new List<Model.Racun>();
                racuni.Add(racun);
                File.Create(putanja).Close();
                String json = JsonConvert.SerializeObject(racuni);
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }

            return racun;
        }
      
      public List<Model.Racun> DobaviSve()
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            List<Model.Racun> racuni = JsonConvert.DeserializeObject<List<Model.Racun>>(json);
            ucitaj.Close();
            return racuni;
      }
   
   }
}