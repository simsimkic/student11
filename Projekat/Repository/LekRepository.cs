// File:    LekRepository.cs
// Created: Saturday, July 4, 2020 1:23:53 PM
// Purpose: Definition of Class LekRepository

using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class LekRepository
   {

        private String putanja = @"..\..\Lek.txt";
      
      public List<Model.Lek> DobaviLekovePoImenu(String ime)
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            ucitaj.Close();
            List<Model.Lek> lekoviPoImenu = JsonConvert.DeserializeObject<List<Model.Lek>>(json);
            for (int i = lekoviPoImenu.Count-1; i >= 0; i--)
                if (!lekoviPoImenu[i].Ime.ToLower().Contains(ime.ToLower()))
                    lekoviPoImenu.RemoveAt(i);
            if(lekoviPoImenu.Count == 0)
                throw new AccessViolationException();
            else
                return lekoviPoImenu;
       }
      
      public Model.Lek Kreiraj(Model.Lek lek)
      {
            if (File.Exists(putanja))
            {
                TextReader ucitaj = new StreamReader(putanja);
                String json = ucitaj.ReadToEnd();
                ucitaj.Close();
                List<Model.Lek> lekovi = JsonConvert.DeserializeObject<List<Model.Lek>>(json);
                lekovi.Add(lek);
                json = JsonConvert.SerializeObject(lekovi);
                File.Create(putanja).Close();
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }
            else
            {
                List<Model.Lek> lekovi = new List<Model.Lek>();
                lekovi.Add(lek);
                Console.WriteLine("Uspesno dodat novi lek!");
                File.Create(putanja).Close();
                String json = JsonConvert.SerializeObject(lekovi);
                TextWriter upisi = new StreamWriter(putanja);
                upisi.Write(json);
                upisi.Close();
            }

            return lek;
        }
      
      public List<Model.Lek> DobaviSve()
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            List<Model.Lek> lekovi = JsonConvert.DeserializeObject<List<Model.Lek>>(json);
            ucitaj.Close();
            return lekovi;
      }
      
      public Model.Lek Izmeni(Model.Lek lek)
      {
            TextReader ucitaj = new StreamReader(putanja);
            String json = ucitaj.ReadToEnd();
            ucitaj.Close();
            List<Model.Lek> lekovi = JsonConvert.DeserializeObject<List<Model.Lek>>(json);
            for (int i = 0; i < lekovi.Count; i++)
                if (lekovi[i].Equals(lek))
                {
                    lekovi.RemoveAt(i);
                    lekovi.Insert(i, lek);
                }
            File.Create(putanja).Close();
            TextWriter upisi = new StreamWriter(putanja);
            json = JsonConvert.SerializeObject(lekovi);
            upisi.Write(json);
            upisi.Close();
            return lek;
      }
   
   }
}