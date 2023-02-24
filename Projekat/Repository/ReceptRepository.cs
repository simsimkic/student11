// File:    ReceptRepository.cs
// Created: Saturday, July 4, 2020 1:23:53 PM
// Purpose: Definition of Class ReceptRepository

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Repository
{
   public class ReceptRepository
   {
      
      private String putanja = @"..\..\Recept.txt";

        public Model.Recept Kreiraj(Model.Recept recept)
      {
            if (File.Exists(putanja))
            {
                TextReader ucitaj = new StreamReader(putanja);
                String json = ucitaj.ReadToEnd();
                ucitaj.Close();
                List<Model.Recept> recepti = JsonConvert.DeserializeObject<List<Model.Recept>>(json);
                recepti.Add(recept);
                json = JsonConvert.SerializeObject(recepti);
                File.Create(putanja).Close();
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }
            else
            {
                List<Model.Recept> recepti = new List<Model.Recept>();
                recepti.Add(recept);
                File.Create(putanja).Close();
                String json = JsonConvert.SerializeObject(recepti);
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }

         return recept;
      }
      
      public List<Model.Recept> DobaviSve()
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            List<Model.Recept> recepti = JsonConvert.DeserializeObject<List<Model.Recept>>(json);
            ucitaj.Close();
            return recepti;
      }
   
   }
}