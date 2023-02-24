// File:    ReceptService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class ReceptService

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace Exceptions
{
   public class ReceptService
   {    

      public Model.Recept KreirajRecept(Model.Recept recept)
      {
            if (!DaLiJeJmbgValidan(recept.JmbgPacijenta))
                throw new InvalidCredentialException();
            LekService lekService = new LekService();
            foreach(var lek in recept.Lekovi.Keys)
            {
                List<Model.Lek> lekoviSaRecepta = lekService.DobaviLekovePoImenu(lek, TipKorisnika.lekar);
                for(int i = 0; i < lekoviSaRecepta.Count; i++)
                    if (!lekoviSaRecepta[i].Recept)
                        throw new InvalidRecipeDrugException();
            }
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            recept.Sifra = recepti.Count + 1;
            Console.WriteLine("\nRecept uspesno kreiran.\n");
            return receptRepository.Kreiraj(recept);

        }
      
      public List<Model.Recept> PrikaziSveRecepte(int sortirajPo)
      {
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            if(recepti.Count == 0)
                throw new NoRecipesAvailableException();
            if (sortirajPo == 0)
                recepti = recepti.OrderBy(o => o.Sifra).ToList();
            else if (sortirajPo == 1)
                recepti = recepti.OrderBy(o => o.Lekar).ToList();
            else
                recepti = recepti.OrderBy(o => o.Datum).ToList();
            return recepti;
      }
      
      public Model.Recept DobaviReceptPoSifri(int sifra)
      {
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            Boolean postojiRecept = false;
            Model.Recept receptPoSifri = new Recept();
            if (recepti.Count == 0)
                throw new NoRecipesAvailableException();
            for(int i = 0; i < recepti.Count; i++)
                if(recepti[i].Sifra == sifra)
                {
                    postojiRecept = true;
                    receptPoSifri = recepti[i];
                    break;
                }
            if (!postojiRecept)
                throw new NoRecipeByPasswordException();
            return receptPoSifri;
      }
      
      public List<Model.Recept> DobaviReceptePoLekaru(String lekar)
      {
            korisnikService.DobaviKorisnikaPoImenu(lekar);
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            Boolean postojiRecept = false;
            List<Model.Recept> receptiPoLekaru = new List<Recept>();
            if (recepti.Count == 0)
                throw new NoRecipesAvailableException();
            for(int i = 0; i < recepti.Count; i++)
                if (recepti[i].Lekar.Equals(lekar))
                {
                    receptiPoLekaru.Add(recepti[i]);
                    postojiRecept = true;
                }
            if (!postojiRecept)
                throw new NoRecipeByDoctorNameException();
            return receptiPoLekaru;
      }
      
      public List<Model.Recept> DobaviReceptePoJmbgPacijenta(String jmbg)
      {
            if (!DaLiJeJmbgValidan(jmbg))
                throw new InvalidCredentialException();
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            Boolean postojiRecept = false;
            List<Model.Recept> receptiPoJmbgu = new List<Recept>();
            if (recepti.Count == 0)
                throw new NoRecipesAvailableException();
            for (int i = 0; i < recepti.Count; i++)
                if (recepti[i].JmbgPacijenta.Equals(jmbg))
                {
                    receptiPoJmbgu.Add(recepti[i]);
                    postojiRecept = true;
                }
            if (!postojiRecept)
                throw new NoRecipeByPersonalNumberException();
            return receptiPoJmbgu;
        }
      
      public List<Model.Recept> DobaviReceptePoLeku(String lek)
      {
            List<Model.Recept> recepti = receptRepository.DobaviSve();
            Boolean postojiRecept = false;
            List<Model.Recept> receptiPoLeku = new List<Recept>();
            if (recepti.Count == 0)
                throw new NoRecipesAvailableException();
            for(int i = 0; i < recepti.Count; i++)
                foreach(var lekUReceptu in recepti[i].Lekovi.Keys)
                    if (lekUReceptu.Equals(lek))
                    {
                        receptiPoLeku.Add(recepti[i]);
                        postojiRecept = true;
                        break;
                    }
            if (!postojiRecept)
                throw new NoRecipeByDrugNameException();
            return receptiPoLeku;

      }

      public Boolean DaLiJeJmbgValidan(String jmbg)
      {
            Boolean validan = true;
            if(jmbg.Length != 13)
                validan = false;
            for(int i=0; i < jmbg.Length; i++)
                if(jmbg[i] < 48 || jmbg[i] > 57)
                    validan = false;
            return validan;
      }

        public Repository.ReceptRepository receptRepository = new Repository.ReceptRepository();
        public Exceptions.KorisnikService korisnikService = new Exceptions.KorisnikService();
   
   }
}