// File:    LekController.cs
// Created: Saturday, July 4, 2020 1:26:07 PM
// Purpose: Definition of Class LekController

using Model;
using Repository;
using Exceptions;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class LekController
   {
      public List<Model.Lek> PrikaziSveLekove(int sortirajPo, TipKorisnika tip)
      {
            List<Model.Lek> lekovi = new List<Model.Lek>();
            try
            {
                lekovi = lekService.PrikaziSveLekove(sortirajPo, tip);
                return lekovi;
            }
            catch(NoDrugsAvailableException ex)
            {
                Console.WriteLine("Ne postoje lekovi za zeljeni tip korisnika.");
            }
            return null;
      }
      
      public Model.Lek DobaviLekPoSifri(String sifra, TipKorisnika tipKorisnika)
      {
            try
            {
                return lekService.DobaviLekPoSifri(sifra, tipKorisnika);
            }
            catch(NoDrugsAvailableException ex)
            {
                Console.WriteLine("Nema dostupnih lekova u magacinu.");
            }
            catch(NoDrugByPasswordException ex)
            {
                Console.WriteLine("Ne postoji lek sa unesenom sifrom.");
            }
            return null;
            
      }
      
      public Model.Lek DodajLek(Model.Lek lek)
      {
            try
            {
                return lekService.DodajLek(lek);
            }
            catch(InvalidDrugIdException ex)
            {
                Console.WriteLine("Vec postoji lek sa istom sifrom leka.");
            }

            return null;
        }
      
      public Model.Lek IzmeniLek(Model.Lek lek, TipKorisnika tipKorisnika)
      {
                return lekService.IzmeniLek(lek, tipKorisnika);
      }
      
      public void ObrisiLek(String sifraLeka, TipKorisnika tipKorisnika)
      {
            try
            {
                lekService.ObrisiLek(sifraLeka, tipKorisnika);
            }
            catch(NoDrugsAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan lek u sistemu.");
            }
            catch(NoDrugByPasswordException ex)
            {
                Console.WriteLine("Ne postoji lek sa unesenom sifrom.");
            }
      }
      
      public Boolean DodajLekUKorpu(Dictionary<String, int> korpa, int kolicina, Lek lek)
      {
            try
            {
                return lekService.DodajLekUKorpu(korpa, kolicina, lek);
            }
            catch(DrugForRecipeException ex)
            {
                Console.WriteLine("Lek se izdaje na recept.");
            }
            return false;
      }
      
      public Boolean DodajLekoveSaRecepta(Dictionary<String,int> korpa, Recept recept)
      {
            try
            {
                return lekService.DodajLekoveSaRecepta(korpa, recept);
            }
            catch(AccessViolationException ex)
            {
                Console.WriteLine("Ne postoji recept sa unesenom sifrom.");
                return false;
            }
      }
      
      public void PrikaziKorpu(Dictionary<String,int> korpa)
      {
            lekService.PrikaziKorpu(korpa);
      }
      
      public void PotvrdiProdaju(Dictionary<String,int> korpa, String apotekar)
      {
            if(korpa.Count != 0)
                lekService.PotvrdiProdaju(korpa, apotekar);
            else
                Console.WriteLine("Ne moze se prodati prazna korpa.");
      }
      
      public List<Model.Lek> DobaviLekovePoImenu(String imeLeka, TipKorisnika tip)
      {
            try
            {
                return lekService.DobaviLekovePoImenu(imeLeka, tip);
            }
            catch(NoDrugsAvailableException ex)
            {
                Console.WriteLine("Nema dostupnih lekova u magacinu.");
            }
            catch(NoDrugsByNameException ex)
            {
                Console.WriteLine("Ne postoji nijedan lek sa unesenim imenom.");
            }
            return null;
      }
      
      public List<Model.Lek> DobaviLekovePoProizvodjacu(String proizvodjac, TipKorisnika tip)
      {
            try
            {
                return lekService.DobaviLekovePoProizvodjacu(proizvodjac, tip);
            }
            catch (NoDrugsAvailableException ex)
            {
                Console.WriteLine("Nema dostupnih lekova u magacinu.");
            }
            catch (NoDrugsByNameException ex)
            {
                Console.WriteLine("Ne postoji nijedan lek sa unesenim imenom proizvodjaca.");
            }
            return null;
        }
      
      public List<Model.Lek> DobaviLekovePoCeni(double minCena, double maksCena, TipKorisnika tip)
      {
            try
            {
                return lekService.DobaviLekovePoCeni(minCena, maksCena, tip);
            }
            catch (NoDrugsAvailableException ex)
            {
                Console.WriteLine("Nema dostupnih lekova u magacinu.");
            }
            catch (NoDrugsByPriceException ex)
            {
                Console.WriteLine("Ne postoji nijedan lek sa unesenim opsegom cena.");
            }
            return null;
        }

        public Exceptions.LekService lekService = new Exceptions.LekService();
   
   }
}