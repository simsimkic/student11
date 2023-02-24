// File:    RacunService.cs
// Created: Saturday, July 4, 2020 1:25:04 PM
// Purpose: Definition of Class RacunService

using System;
using System.Collections.Generic;

namespace Exceptions
{
   public class RacunService
   {
      public Model.Racun KreirajRacun(Model.Racun racun)
      {
            return racunRepository.Kreiraj(racun);
      }
      
      public List<Model.Racun> PrikaziSveRacune()
      {
            return racunRepository.DobaviSve();
      }
      
      public List<Model.Racun> PrikaziSveRacunePoApotekaru(String imeApotekara)
      {
            korisnikService.DobaviKorisnikaPoImenu(imeApotekara);
            return racunRepository.DobaviRacunePoApotekaru(imeApotekara);
      }
      
      public Repository.RacunRepository racunRepository = new Repository.RacunRepository();
      public Exceptions.KorisnikService korisnikService = new Exceptions.KorisnikService();
   
   }
}