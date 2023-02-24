// File:    ReceptController.cs
// Created: Saturday, July 4, 2020 1:26:07 PM
// Purpose: Definition of Class ReceptController

using Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Controller
{
   public class ReceptController
   {
      public Model.Recept KreirajRecept(Model.Recept recept)
      {
            try
            {
                return receptService.KreirajRecept(recept);
            }
            catch(InvalidCredentialException ex)
            {
                Console.WriteLine("Unesite validan JMBG pacijenta.");
            }
            catch(NoDrugsAvailableException ex)
            {
                Console.WriteLine("Nema nijedan lek u sistemu.");
            }
            catch(NoDrugsByNameException ex)
            {
                Console.WriteLine("Ne postoji lek sa unesenim imenom.");
            }
            catch(InvalidRecipeDrugException ex)
            {
                Console.WriteLine("Uneseni lek se ne izdaje na recept.");
            }
            return null;
      }
      
      public List<Model.Recept> PrikaziSveRecepte(int sortirajPo)
      {
            try
            {
                return receptService.PrikaziSveRecepte(sortirajPo);
            }
            catch(NoRecipesAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept u bazi.");
            }
            return null;
      }
      
      public Model.Recept DobaviReceptPoSifri(int sifra)
      {
            try
            {
                return receptService.DobaviReceptPoSifri(sifra);
            }
            catch(NoRecipesAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept u bazi.");
            }
            catch(NoRecipeByPasswordException ex)
            {
                Console.WriteLine("Ne postoji recept sa unesenom sifrom.");
            }
            return null;
      }
      
      public List<Model.Recept> DobaviReceptePoLekaru(String lekar)
      {
            try
            {
                return receptService.DobaviReceptePoLekaru(lekar);
            }
            catch(NotRegisteredUserException ex)
            {
                Console.WriteLine("Ne postoji nijedan lekar u sistemu.");
            }
            catch(NoUserByNameException ex)
            {
                Console.WriteLine("Nema lekara sa unesenim imenom.");
            }
            catch(NoRecipesAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept u bazi.");
            }
            catch(NoRecipeByDoctorNameException ex)
            {
                Console.WriteLine("Ne postoji nijedan izdati recept od strane trazenog lekara.");
            }
            return null;
      }
      
      public List<Model.Recept> DobaviReceptePoJmbgPacijenta(String jmbg)
      {
            try
            {
                return receptService.DobaviReceptePoJmbgPacijenta(jmbg);
            }
            catch(InvalidCredentialException ex)
            {
                Console.WriteLine("Uneseni JMBG je nevalidnog formata.");
            }
            catch(NoRecipesAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept u bazi.");
            }
            catch(NoRecipeByPersonalNumberException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept izdat pacijentu sa unesenim JMBG-om.");
            }
            return null;
      }
      
      public List<Model.Recept> DobaviReceptePoLeku(String lek)
      {
            try
            {
                return receptService.DobaviReceptePoLeku(lek);
            }
            catch(NoRecipesAvailableException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept u bazi.");
            }
            catch(NoRecipeByDrugNameException ex)
            {
                Console.WriteLine("Ne postoji nijedan recept koji sadrzi uneseni lek.");
            }
            return null;
      }
      
      public Exceptions.ReceptService receptService = new Exceptions.ReceptService();
   
   }
}