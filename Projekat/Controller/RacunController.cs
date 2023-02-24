// File:    RacunController.cs
// Created: Saturday, July 4, 2020 1:26:07 PM
// Purpose: Definition of Class RacunController

using Exceptions;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class RacunController
   {     
      public List<Model.Racun> PrikaziSveRacune()
      {
            return racunService.PrikaziSveRacune();
      }
      
      public List<Model.Racun> PrikaziSveRacunePoApotekaru(String imeApotekara)
      {
            try
            {
                return racunService.PrikaziSveRacunePoApotekaru(imeApotekara);
            }
            catch(NotRegisteredUserException ex)
            {
                Console.WriteLine("Ne postoji nijedan registrovan apotekar u sistemu.");
            }
            catch(NoUserByNameException ex)
            {
                Console.WriteLine("Ne postoji apotekar sa unesenim imenom.");
            }
            return null;
      }
      
      public Exceptions.RacunService racunService = new Exceptions.RacunService();
   
   }
}