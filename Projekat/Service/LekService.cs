// File:    LekService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class LekService

using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Exceptions
{
   public class LekService
   {

      public List<Model.Lek> PrikaziLekovePoTipu(List<Model.Lek> lekovi,TipKorisnika tip)
      {
            if (tip != TipKorisnika.administrator)
                for (int i = lekovi.Count - 1; i >= 0; i--)
                    if (lekovi[i].Obrisan)
                        lekovi.RemoveAt(i);
            return lekovi;
      }
      public List<Model.Lek> PrikaziSveLekove(int sortirajPo, TipKorisnika tip)
      {
            List<Model.Lek> lekovi = PrikaziLekovePoTipu(lekRepository.DobaviSve(), tip);
            if(lekovi.Count == 0)
            {
                throw new NoDrugsAvailableException();
            }
            if (sortirajPo == 0)
                lekovi = lekovi.OrderBy(o => o.Ime).ToList();
            else if (sortirajPo == 1)
                lekovi = lekovi.OrderBy(o => o.Proizvodjac).ToList();
            else
                lekovi = lekovi.OrderBy(o => o.Cena).ToList();
            return lekovi;
      }
      
      public Model.Lek DobaviLekPoSifri(String sifra, TipKorisnika tipKorisnika)
      {
            List<Model.Lek> lekovi = lekRepository.DobaviSve();
            lekovi = PrikaziLekovePoTipu(lekovi, tipKorisnika);
            Boolean postojiLek = false;
            Model.Lek lek = new Lek();
            if (lekovi.Count == 0)
                throw new NoDrugsAvailableException();
            for (int i = 0; i < lekovi.Count; i++)
                if (lekovi[i].Sifra.Equals(sifra))
                {
                    lek = lekovi[i];
                    postojiLek = true;
                    break;
                }
            if (!postojiLek)
                throw new NoDrugByPasswordException();
            return lek;
      }
      
      public Model.Lek DodajLek(Model.Lek lek)
      {
            List<Model.Lek> lekovi = lekRepository.DobaviSve();
            Boolean postojiLek = false;
            for(int i = 0; i < lekovi.Count; i++)
                if(lekovi[i].Sifra == lek.Sifra)
                    postojiLek = true;
            if (postojiLek)
                throw new InvalidDrugIdException();
            Console.WriteLine("\nLek je uspesno dodat.\n");
            return lekRepository.Kreiraj(lek);
      }
      
      public Model.Lek IzmeniLek(Model.Lek lek, TipKorisnika tipKorisnika)
      {
            Lek lekZaIzmenu = DobaviLekPoSifri(lek.Sifra, tipKorisnika);
            if (lek.Ime == "")
                lek.Ime = lekZaIzmenu.Ime;
            if (lek.Proizvodjac == "")
                lek.Proizvodjac = lekZaIzmenu.Proizvodjac;
            if (lek.Cena == 0)
                lek.Cena = lekZaIzmenu.Cena;
            Console.WriteLine("\nLek uspesno izmenjen.\n");
            lek = lekRepository.Izmeni(lek);
            return lek;
      }
      
      public void ObrisiLek(String sifraLeka, TipKorisnika tipKorisnika)
      {
            Model.Lek lekZaBrisanje = DobaviLekPoSifri(sifraLeka, tipKorisnika);
            lekZaBrisanje.Obrisan = true;
            Console.WriteLine("\nLek uspesno obrisan.\n");
            lekRepository.Izmeni(lekZaBrisanje);
      }

      public Boolean DodajLekUKorpu(Dictionary<String,int> korpa, int kolicina, Lek lek)
      {
            if (lek.Recept)
                throw new DrugForRecipeException();
            korpa[lek.Ime] = kolicina;
            Console.WriteLine("\nLek uspesno dodat u korpu.\n");
            return true;
      }
      
      public Boolean DodajLekoveSaRecepta(Dictionary<String,int> korpa, Recept recept)
      {
            foreach (String item in recept.Lekovi.Keys)
                korpa[item] = recept.Lekovi[item];
            return true;
      }
      
      public void PrikaziKorpu(Dictionary<String,int> korpa)
      {
            Console.WriteLine("Ime leka:\tKolicina:");
            foreach(String item in korpa.Keys)
                Console.WriteLine(item + "\t\t" + korpa[item].ToString());
            double ukupnaCena = 0;
            foreach(String item in korpa.Keys)
            {
                List<Lek> lekovi = lekRepository.DobaviLekovePoImenu(item);
                for(int i = 0; i < lekovi.Count; i++)
                    ukupnaCena += lekovi[i].Cena * korpa[item];
            }
            Console.WriteLine("\tUkupna cena svih lekova je:  " + ukupnaCena.ToString());
      }
      
      public void PotvrdiProdaju(Dictionary<String,int> korpa, String apotekar)
      {
            List<Racun> racuni = racunService.PrikaziSveRacune();
            Racun racun = new Racun(racuni.Count + 1, apotekar, DateTime.Now, korpa);
            foreach(String item in racun.Lekovi.Keys)
            {
                List<Lek> lekovi = lekRepository.DobaviLekovePoImenu(item);
                for(int i = 0; i < lekovi.Count; i++)
                    racun.UkupnaCena += lekovi[i].Cena * racun.Lekovi[item];
            }
            racunService.KreirajRacun(racun);
      }
      
      public List<Model.Lek> DobaviLekovePoImenu(String imeLeka, TipKorisnika tip)
      {
            List<Model.Lek> lekovi = lekRepository.DobaviSve();
            lekovi = PrikaziLekovePoTipu(lekovi, tip);
            Boolean postojiLek = false;
            List<Model.Lek> lekoviPoImenu = new List<Lek>();
            if (lekovi.Count == 0)
                throw new NoDrugsAvailableException();
            for (int i = 0; i < lekovi.Count; i++)
                if (lekovi[i].Ime.ToUpper().Contains(imeLeka.ToUpper()))
                {
                    lekoviPoImenu.Add(lekovi[i]);
                    postojiLek = true;
                }
            if (!postojiLek)
                throw new NoDrugsByNameException();
            return lekoviPoImenu;
        }
      
      public List<Model.Lek> DobaviLekovePoProizvodjacu(String proizvodjac, TipKorisnika tip)
      {
            List<Model.Lek> lekovi = lekRepository.DobaviSve();
            lekovi = PrikaziLekovePoTipu(lekovi, tip);
            Boolean postojiLek = false;
            List<Model.Lek> lekoviPoProizvodjacu = new List<Lek>();
            if (lekovi.Count == 0)
                throw new NoDrugsAvailableException();
            for (int i = 0; i < lekovi.Count; i++)
                if (lekovi[i].Proizvodjac.Equals(proizvodjac))
                {
                    lekoviPoProizvodjacu.Add(lekovi[i]);
                    postojiLek = true;
                }
            if (!postojiLek)
                throw new NoDrugsByNameException();
            return lekoviPoProizvodjacu;
        }
      
      public List<Model.Lek> DobaviLekovePoCeni(double minCena, double maksCena, TipKorisnika tip)
      {
            List<Model.Lek> lekovi = lekRepository.DobaviSve();
            lekovi = PrikaziLekovePoTipu(lekovi, tip);
            Boolean postojiLek = false;
            List<Model.Lek> lekoviPoCeni = new List<Lek>();
            if (lekovi.Count == 0)
                throw new NoDrugsAvailableException();
            for (int i = 0; i < lekovi.Count; i++)
                if (lekovi[i].Cena >= minCena && lekovi[i].Cena <= maksCena)
                {
                    lekoviPoCeni.Add(lekovi[i]);
                    postojiLek = true;
                }
            if (!postojiLek)
                throw new NoDrugsByPriceException();
            return lekoviPoCeni;
        }

      
      public Repository.LekRepository lekRepository = new Repository.LekRepository();
      public Exceptions.ReceptService receptService = new Exceptions.ReceptService();
        public Exceptions.RacunService racunService = new Exceptions.RacunService();
   
   }
}