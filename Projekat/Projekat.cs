using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Controller;
using Model;
using Repository;
using Exceptions;

namespace Projekat
{
    class Projekat
    {
        public Korisnik ulogovaniKorisnik;
        Controller.KorisnikController korisnikController = new Controller.KorisnikController();
        Controller.LekController lekController = new Controller.LekController();
        Controller.RacunController racunController = new Controller.RacunController();
        Controller.ReceptController receptController = new Controller.ReceptController();
        static public void Main(String[] args)
        {

            Projekat projekat = new Projekat();

            Boolean radi = true;
            do
            {
                Console.WriteLine("\n\n\t\tGlavni Meni");
                Console.WriteLine("1. Uloguj se.");
                Console.WriteLine("2. Izadji iz aplikacije.");
                int opcija = Convert.ToInt32(Console.ReadLine());
                switch (opcija)
                {
                    case 1:
                        Console.WriteLine("Unesi korisnicko ime: ");
                        String korisnickoIme = Console.ReadLine();
                        Console.WriteLine("Unesi lozinku: ");
                        String lozinka = Console.ReadLine();
                        projekat.ulogovaniKorisnik = projekat.korisnikController.Login(korisnickoIme, lozinka);
                        if(projekat.ulogovaniKorisnik != null)
                        {
                            projekat.MeniZaUlogovanogKorisnika(projekat);
                        }
                        break;
                    case 2:
                        radi = false;
                        break;
                }
            } while (radi);
        }

        public void MeniZaUlogovanogKorisnika(Projekat projekat)
        {
            if (projekat.ulogovaniKorisnik.TipKorisnika == TipKorisnika.administrator)
                PocetnaStranaAdministratora(projekat);
            else if (projekat.ulogovaniKorisnik.TipKorisnika == TipKorisnika.apotekar)
                PocetnaStranaApotekara(projekat);
            else
                PocetnaStranaLekara(projekat);
        }

        public void PocetnaStranaAdministratora(Projekat projekat)
        {
            
            Boolean radi = true;

            do
            {
                Console.WriteLine("\n\n\t\tPocetna Administrator");
                Console.WriteLine("1. Prikazi sve lekove.");
                Console.WriteLine("2. Pretrazi lekove.");
                Console.WriteLine("3. Prikazi sve recepte.");
                Console.WriteLine("4. Pretrazi recepte.");
                Console.WriteLine("5. Registruj korisnika.");
                Console.WriteLine("6. Prikazi sve korisnike.");
                Console.WriteLine("7. Kreiraj izvestaj.");
                Console.WriteLine("8. Dodaj lek.");
                Console.WriteLine("9. Izmeni lek.");
                Console.WriteLine("10. Obrisi lek.");
                Console.WriteLine("11. Izloguj se.");
                int opcija = Convert.ToInt32(Console.ReadLine());
                switch (opcija)
                {
                    case 1:
                        Console.WriteLine("Sortiraj lekove po: 1. Ime, 2. Proizvodjac ili 3. Cena");
                        int sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Lek> lekovi = projekat.lekController.PrikaziSveLekove(sortirajPo-1, projekat.ulogovaniKorisnik.TipKorisnika);
                        if (lekovi != null)
                        {
                            Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                            for (int i = 0; i < lekovi.Count; i++)
                                Console.WriteLine(lekovi[i]);
                        }
                        break;
                    case 2:
                        Model.Lek lekZaPretragu = new Lek();
                        Console.WriteLine("Pretrazi lekove po: 1. Sifra, 2. Ime, 3. Proizvodjac ili 4. Opseg cene");
                        int pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru leka: ");
                            String sifraLeka = Console.ReadLine();
                            lekZaPretragu = lekController.DobaviLekPoSifri(sifraLeka, TipKorisnika.administrator);
                            if (lekZaPretragu != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                Console.WriteLine(lekZaPretragu);
                            }
                        }
                        else
                        {
                            List<Lek> pretrazeniLekovi = new List<Lek>();
                            if(pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime leka: ");
                                String imeLeka = Console.ReadLine();
                                pretrazeniLekovi = lekController.DobaviLekovePoImenu(imeLeka,projekat.ulogovaniKorisnik.TipKorisnika);
                            }else if(pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi ime proizvodjaca: ");
                                String imeLeka = Console.ReadLine();
                                pretrazeniLekovi = lekController.DobaviLekovePoProizvodjacu(imeLeka, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            else
                            {
                                Console.WriteLine("Unesi minimalnu cenu: ");
                                double minCena = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Unesi maksimalnu cenu: ");
                                double maksCena = Convert.ToDouble(Console.ReadLine());
                                pretrazeniLekovi = lekController.DobaviLekovePoCeni(minCena, maksCena, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            if (pretrazeniLekovi != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                for (int i = 0; i < pretrazeniLekovi.Count; i++)
                                    Console.WriteLine(pretrazeniLekovi[i]);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Sortiraj recepte po: 1. Sifra, 2. Lekar ili 3. Datum");
                        sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Recept> recepti = projekat.receptController.PrikaziSveRecepte(sortirajPo - 1);
                        if(recepti != null)
                        {
                            Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                            for (int i = 0; i < recepti.Count; i++)
                                Console.WriteLine(recepti[i]);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Pretrazi recepte po: 1. Sifra, 2. Lekar, 3. Jmbg pacijenta ili 4. Jedan lek");
                        pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru recepta: ");
                            int sifraRecepta = Convert.ToInt32(Console.ReadLine());
                            Recept receptPoSifri = receptController.DobaviReceptPoSifri(sifraRecepta);
                            if(receptPoSifri != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                Console.WriteLine(receptController.DobaviReceptPoSifri(sifraRecepta));
                            }
                        }
                        else
                        {
                            List<Recept> pretrazeniRecepti = new List<Recept>();
                            if (pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime lekara: ");
                                String imeRecepta = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLekaru(imeRecepta);
                            }
                            else if (pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi jmbg pacijenta: ");
                                String jmbg = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoJmbgPacijenta(jmbg);
                            }
                            else
                            {
                                Console.WriteLine("Unesi ime jednog leka: ");
                                String imeRecepta = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLeku(imeRecepta);
                            }
                            if(pretrazeniRecepti != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                for (int i = 0; i < pretrazeniRecepti.Count; i++)
                                    Console.WriteLine(pretrazeniRecepti[i]);
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine("Unesi ime korisnika: ");
                        String ime = Console.ReadLine();
                        Console.WriteLine("Unesi prezime korisnika:");
                        String prezime = Console.ReadLine();
                        Console.WriteLine("Unesi korisnicko ime:");
                        String korisnickoIme = Console.ReadLine();
                        Console.WriteLine("Unesi lozinku korisnika: ");
                        String lozinka = Console.ReadLine();
                        Console.WriteLine("Tip korisnika: 1. Lekar ili 2. Apotekar");
                        String tip = Console.ReadLine();
                        if (tip == "1")
                            korisnikController.RegistracijaKorisnika(new Korisnik(korisnickoIme, lozinka, ime, prezime, TipKorisnika.lekar));
                        else
                            korisnikController.RegistracijaKorisnika(new Korisnik(korisnickoIme, lozinka, ime, prezime, TipKorisnika.apotekar));
                        break;
                    case 6:
                        Console.WriteLine("Sortiraj korisnike po: 1. Ime, 2. Prezime ili 3. Tip korisnika");
                        sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Korisnik> korisnici = projekat.korisnikController.PrikaziSveKorisnike(sortirajPo - 1);
                        if(korisnici != null)
                        {
                            Console.WriteLine("Korisnicko ime:\tIme korisnika:\tPrezime korisnika:\tTip korisnika:");
                            for (int i = 0; i < korisnici.Count; i++)
                                Console.WriteLine(korisnici[i]);
                        }
                        break;
                    case 7:
                        Console.WriteLine("Odaberi tip izvestaja: 1. Svi lekovi, 2. Lekovi po proizvodjacu ili 3. Lekovi po apotekaru.");
                        int tipIzvestaja = Convert.ToInt32(Console.ReadLine());
                        List<Racun> racuni = new List<Racun>();
                        Dictionary<String, int> kolicina = new Dictionary<String, int>();  // Ime leka i prodata kolicina
                        Dictionary<String, double> ukupnaCena = new Dictionary<String, double>();  // Ime leka i ukupna cena svih prodatih
                        if(tipIzvestaja == 1)
                        {
                            racuni = racunController.PrikaziSveRacune();
                            for(int i = 0; i < racuni.Count; i++)
                                foreach(String item in racuni[i].Lekovi.Keys)
                                {
                                    lekovi = lekController.DobaviLekovePoImenu(item, TipKorisnika.administrator);
                                    for (int j = 0; j < lekovi.Count; j++)
                                        if (kolicina.ContainsKey(lekovi[j].Ime))
                                        {
                                            kolicina[lekovi[j].Ime] += racuni[i].Lekovi[item];
                                            ukupnaCena[lekovi[j].Ime] += lekovi[j].Cena * racuni[i].Lekovi[item];
                                        }
                                        else
                                        {
                                            kolicina[lekovi[j].Ime] = racuni[i].Lekovi[item];
                                            ukupnaCena[lekovi[j].Ime] = lekovi[j].Cena * racuni[i].Lekovi[item];
                                        }
                                }
                        }else if(tipIzvestaja == 2)
                        {
                            Console.WriteLine("Unesi ime proizvodjaca: ");
                            String imeProizvodjaca = Console.ReadLine();
                            racuni = racunController.PrikaziSveRacune();
                            for (int i = 0; i < racuni.Count; i++)
                                foreach (String item in racuni[i].Lekovi.Keys)
                                {
                                    lekovi = lekController.DobaviLekovePoImenu(item, TipKorisnika.administrator);
                                    for (int j = 0; j < lekovi.Count; j++)
                                        if (lekovi[j].Proizvodjac == imeProizvodjaca)
                                            if (kolicina.ContainsKey(lekovi[j].Ime))
                                            {
                                                kolicina[lekovi[j].Ime] += racuni[i].Lekovi[item];
                                                ukupnaCena[lekovi[j].Ime] += lekovi[j].Cena * racuni[i].Lekovi[item];
                                            }
                                            else
                                            {
                                                kolicina[lekovi[j].Ime] = racuni[i].Lekovi[item];
                                                ukupnaCena[lekovi[j].Ime] = lekovi[j].Cena * racuni[i].Lekovi[item];
                                            }
                                }
                        }
                        else
                        {
                            Console.WriteLine("Unesi ime apotekara: ");
                            String imeApotekara = Console.ReadLine();
                            racuni = racunController.PrikaziSveRacunePoApotekaru(imeApotekara);
                            if(racuni != null)
                            {
                                for (int i = 0; i < racuni.Count; i++)
                                    foreach (String item in racuni[i].Lekovi.Keys)
                                    {
                                        lekovi = lekController.DobaviLekovePoImenu(item, TipKorisnika.administrator);
                                        for (int j = 0; j < lekovi.Count; j++)
                                            if (kolicina.ContainsKey(lekovi[j].Ime))
                                            {
                                                kolicina[lekovi[j].Ime] += racuni[i].Lekovi[item];
                                                ukupnaCena[lekovi[j].Ime] += lekovi[j].Cena * racuni[i].Lekovi[item];
                                            }
                                            else
                                            {
                                                kolicina[lekovi[j].Ime] = racuni[i].Lekovi[item];
                                                ukupnaCena[lekovi[j].Ime] = lekovi[j].Cena * racuni[i].Lekovi[item];
                                            }
                                    }
                            }
                        }
                        if (kolicina.Count != 0)
                            Console.WriteLine("Ime leka:\tProdata kolicina:\tUkupna cena:");
                        else if(racuni != null)
                            Console.WriteLine("Ne postoje prodati lekovi za zeljeni unos.");
                        foreach (String item in kolicina.Keys)
                            Console.WriteLine(item + "\t\t" + kolicina[item].ToString() + "\t\t\t" + ukupnaCena[item].ToString());
                        break;
                    case 8:
                        Lek lek = new Lek();
                        Console.WriteLine("Unesi ime leka: ");
                        lek.Ime = Console.ReadLine();
                        Console.WriteLine("Unesi sifru leka: ");
                        lek.Sifra = Console.ReadLine();
                        Console.WriteLine("Unesi cenu leka: ");
                        lek.Cena = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Unesi ime proizvodjaca leka: ");
                        lek.Proizvodjac = Console.ReadLine();
                        Console.WriteLine("Da li se lek izdaje na recept (D/N)?");
                        String recept = Console.ReadLine();
                        if(recept == "D")
                            lek.Recept = true;
                        else
                            lek.Recept = false;
                        lekController.DodajLek(lek);
                        break;
                    case 9:
                        lek = new Lek();
                        Console.WriteLine("Unesi sifru leka koji zelis da izmenis: ");
                        lek.Sifra = Console.ReadLine();
                        lek = lekController.DobaviLekPoSifri(lek.Sifra, TipKorisnika.administrator);
                        if (lek != null)
                        {
                            Console.WriteLine("Izmeni ime leka (" + lek.Ime + "): ");
                            lek.Ime = Console.ReadLine();
                            Console.WriteLine("Izmeni cenu leka (" + lek.Cena.ToString() + "): ");
                            String cena = Console.ReadLine();
                            if (cena == "")
                                lek.Cena = 0;
                            else
                                lek.Cena = Convert.ToDouble(cena);
                            Console.WriteLine("Izmeni ime proizvodjaca leka (" + lek.Proizvodjac + "): ");
                            lek.Proizvodjac = Console.ReadLine();
                            lekController.IzmeniLek(lek, TipKorisnika.administrator);
                        }
                        break;
                    case 10:
                        lek = new Lek();
                        Console.WriteLine("Unesi sifru leka koji zelis da obrises: ");
                        lek.Sifra = Console.ReadLine();
                        lekController.ObrisiLek(lek.Sifra, TipKorisnika.administrator);
                        break;
                    case 11:
                        projekat.ulogovaniKorisnik = null;
                        radi = false;
                        break;
                }
            } while (radi);
        }

        public void PocetnaStranaApotekara(Projekat projekat)
        {
           
            Boolean radi = true;


            do
            {
                Console.WriteLine("\n\n\t\tPocetna Apotekar");
                Console.WriteLine("1. Prikazi sve lekove.");
                Console.WriteLine("2. Pretrazi lekove.");
                Console.WriteLine("3. Prikazi sve recepte.");
                Console.WriteLine("4. Pretrazi recepte.");
                Console.WriteLine("5. Prodaja lekova.");
                Console.WriteLine("6. Dodaj lek.");
                Console.WriteLine("7. Izmeni lek.");
                Console.WriteLine("8. Obrisi lek.");
                Console.WriteLine("9. Izloguj se.");
                int opcija = Convert.ToInt32(Console.ReadLine());
                switch (opcija)
                {
                    case 1:
                        Console.WriteLine("Sortiraj lekove po: 1. Ime, 2. Proizvodjac ili 3. Cena");
                        int sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Lek> lekovi = projekat.lekController.PrikaziSveLekove(sortirajPo - 1, projekat.ulogovaniKorisnik.TipKorisnika);
                        if (lekovi != null)
                        {
                            Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                            for (int i = 0; i < lekovi.Count; i++)
                                Console.WriteLine(lekovi[i]);
                        }
                        break;
                    case 2:
                        Model.Lek lekZaPretragu = new Lek();
                        Console.WriteLine("Pretrazi lekove po: 1. Sifra, 2. Ime, 3. Proizvodjac ili 4. Opseg cene");
                        int pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru leka: ");
                            String sifraLeka = Console.ReadLine();
                            lekZaPretragu = lekController.DobaviLekPoSifri(sifraLeka, TipKorisnika.apotekar);
                            if (lekZaPretragu != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                Console.WriteLine(lekZaPretragu);
                            }
                        }
                        else
                        {
                            List<Lek> pretrazeniLekovi = new List<Lek>();
                            if (pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime leka: ");
                                String ime = Console.ReadLine();
                                try
                                {
                                    pretrazeniLekovi = lekController.DobaviLekovePoImenu(ime, projekat.ulogovaniKorisnik.TipKorisnika);
                                }
                                catch(AccessViolationException ex)
                                {
                                    Console.WriteLine("Ne postoji lek sa unesenim imenom.");
                                }

                            }
                            else if (pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi ime proizvodjaca: ");
                                String ime = Console.ReadLine();
                                pretrazeniLekovi = lekController.DobaviLekovePoProizvodjacu(ime, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            else
                            {
                                Console.WriteLine("Unesi minimalnu cenu: ");
                                double minCena = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Unesi maksimalnu cenu: ");
                                double maksCena = Convert.ToDouble(Console.ReadLine());
                                pretrazeniLekovi = lekController.DobaviLekovePoCeni(minCena, maksCena, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            if (pretrazeniLekovi != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                for (int i = 0; i < pretrazeniLekovi.Count; i++)
                                    Console.WriteLine(pretrazeniLekovi[i]);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Sortiraj recepte po: 1. Sifra, 2. Lekar ili 3. Datum");
                        sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Recept> recepti = projekat.receptController.PrikaziSveRecepte(sortirajPo - 1);
                        if (recepti != null)
                        {
                            Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                            for (int i = 0; i < recepti.Count; i++)
                                Console.WriteLine(recepti[i]);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Pretrazi recepte po: 1. Sifra, 2. Lekar, 3. Jmbg pacijenta ili 4. Jedan lek");
                        pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru recepta: ");
                            int sifraRecepta = Convert.ToInt32(Console.ReadLine());
                            Recept receptPoSifri = receptController.DobaviReceptPoSifri(sifraRecepta);
                            if (receptPoSifri != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                Console.WriteLine(receptController.DobaviReceptPoSifri(sifraRecepta));
                            }
                        }
                        else
                        {
                            List<Recept> pretrazeniRecepti = new List<Recept>();
                            if (pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime lekara: ");
                                String ime = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLekaru(ime);
                            }
                            else if (pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi jmbg pacijenta: ");
                                String jmbg = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoJmbgPacijenta(jmbg);
                            }
                            else
                            {
                                Console.WriteLine("Unesi ime jednog leka: ");
                                String ime = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLeku(ime);
                            }
                            if (pretrazeniRecepti != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                for (int i = 0; i < pretrazeniRecepti.Count; i++)
                                    Console.WriteLine(pretrazeniRecepti[i]);
                            }
                        }
                        break;
                    case 5:
                        Dictionary<String, int> korpa = new Dictionary<String, int>();
                        int krajProdaje = 0;
                        do
                        {
                            Console.WriteLine("\n\n\t\tProdaja lekova.");
                            Console.WriteLine("1. Dodaj lek unosom sifre.");
                            Console.WriteLine("2. Dodaj lekove sa recepta.");
                            Console.WriteLine("3. Pregledaj korpu.");
                            Console.WriteLine("4. Potvrdi kupovinu.");
                            Console.WriteLine("5. Odustani.");

                            opcija = Convert.ToInt32(Console.ReadLine());

                            switch (opcija)
                            {
                                case 1:
                                    do
                                    {
                                        Lek lekZaProdaju = new Lek();
                                        Console.WriteLine("Unesi sifru leka: ");
                                        lekZaProdaju.Sifra = Console.ReadLine();
                                        Console.WriteLine("Unesi kolicinu leka: ");
                                        int kolicina = Convert.ToInt32(Console.ReadLine());
                                        lekZaProdaju = lekController.DobaviLekPoSifri(lekZaProdaju.Sifra, TipKorisnika.apotekar);
                                        if(lekZaProdaju != null)
                                            lekController.DodajLekUKorpu(korpa, kolicina, lekZaProdaju);
                                        Console.WriteLine("Unosi jos lekova po sifri (D/N)?");
                                    } while (Console.ReadLine() == "D");
                                    break;
                                case 2:
                                    Recept receptZaKorpu = new Recept();
                                    Console.WriteLine("Unesi sifru recepta: ");
                                    receptZaKorpu.Sifra = Convert.ToInt32(Console.ReadLine());
                                    receptZaKorpu = receptController.DobaviReceptPoSifri(receptZaKorpu.Sifra);
                                    if(receptZaKorpu != null)
                                        lekController.DodajLekoveSaRecepta(korpa, receptZaKorpu);
                                    break;
                                case 3:
                                    lekController.PrikaziKorpu(korpa);
                                    break;
                                case 4:
                                    lekController.PotvrdiProdaju(korpa, ulogovaniKorisnik.Ime);
                                    krajProdaje = 1;
                                    break;
                                case 5:
                                    krajProdaje = 1;
                                    break;
                            }
                        } while (krajProdaje == 0);
                        break;
                    case 6:
                        Lek lek = new Lek();
                        Console.WriteLine("Unesi ime leka: ");
                        lek.Ime = Console.ReadLine();
                        Console.WriteLine("Unesi sifru leka: ");
                        lek.Sifra = Console.ReadLine();
                        Console.WriteLine("Unesi cenu leka: ");
                        lek.Cena = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Unesi ime proizvodjaca leka: ");
                        lek.Proizvodjac = Console.ReadLine();
                        Console.WriteLine("Da li se lek izdaje na recept (D/N)?");
                        String recept = Console.ReadLine();
                        if (recept == "D")
                            lek.Recept = true;
                        else
                            lek.Recept = false;
                        lekController.DodajLek(lek);
                        break;
                    case 7:
                        lek = new Lek();
                        Console.WriteLine("Unesi sifru leka koji zelis da izmenis: ");
                        lek.Sifra = Console.ReadLine();
                        lek = lekController.DobaviLekPoSifri(lek.Sifra, TipKorisnika.apotekar);
                        if(lek != null)
                        {
                            Console.WriteLine("Izmeni ime leka (" + lek.Ime + "): ");
                            lek.Ime = Console.ReadLine();
                            Console.WriteLine("Izmeni cenu leka (" + lek.Cena.ToString() + "): ");
                            String cena = Console.ReadLine();
                            if (cena == "")
                                lek.Cena = 0;
                            else
                                lek.Cena = Convert.ToDouble(cena);
                            Console.WriteLine("Izmeni ime proizvodjaca leka (" + lek.Proizvodjac + "): ");
                            lek.Proizvodjac = Console.ReadLine();
                            lekController.IzmeniLek(lek, TipKorisnika.apotekar);
                        }
                        break;
                    case 8:
                        lek = new Lek();
                        Console.WriteLine("Unesi sifru leka koji zelis da obrises: ");
                        lek.Sifra = Console.ReadLine();
                        lekController.ObrisiLek(lek.Sifra, TipKorisnika.apotekar);
                        break;
                    case 9:
                        projekat.ulogovaniKorisnik = null;
                        radi = false;
                        break;
                }
            } while (radi);
        }

        public void PocetnaStranaLekara(Projekat projekat)
        {
            
            Boolean radi = true;


            do
            {
                Console.WriteLine("\n\n\t\tPocetna Lekar");
                Console.WriteLine("1. Prikazi sve lekove.");
                Console.WriteLine("2. Pretrazi lekove.");
                Console.WriteLine("3. Prikazi sve recepte.");
                Console.WriteLine("4. Pretrazi recepte.");
                Console.WriteLine("5. Kreiraj recept.");
                Console.WriteLine("6. Izloguj se.");
                int opcija = Convert.ToInt32(Console.ReadLine());
                switch (opcija)
                {
                    case 1:
                        Console.WriteLine("Sortiraj lekove po: 1. Ime, 2. Proizvodjac ili 3. Cena");
                        int sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Lek> lekovi = projekat.lekController.PrikaziSveLekove(sortirajPo - 1, projekat.ulogovaniKorisnik.TipKorisnika);
                        if (lekovi != null)
                        {
                            Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                            for (int i = 0; i < lekovi.Count; i++)
                                Console.WriteLine(lekovi[i]);
                        }
                        break;
                    case 2:
                        Model.Lek lekZaPretragu = new Lek();
                        Console.WriteLine("Pretrazi lekove po: 1. Sifra, 2. Ime, 3. Proizvodjac ili 4. Opseg cene");
                        int pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru leka: ");
                            String sifraLeka = Console.ReadLine();
                            lekZaPretragu = lekController.DobaviLekPoSifri(sifraLeka, TipKorisnika.lekar);
                            if (lekZaPretragu != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                Console.WriteLine(lekZaPretragu);
                            }
                        }
                        else
                        {
                            List<Lek> pretrazeniLekovi = new List<Lek>();
                            if (pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime leka: ");
                                String ime = Console.ReadLine();
                                pretrazeniLekovi = lekController.DobaviLekovePoImenu(ime, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            else if (pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi ime proizvodjaca: ");
                                String ime = Console.ReadLine();
                                pretrazeniLekovi = lekController.DobaviLekovePoProizvodjacu(ime, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            else
                            {
                                Console.WriteLine("Unesi minimalnu cenu: ");
                                double minCena = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Unesi maksimalnu cenu: ");
                                double maksCena = Convert.ToDouble(Console.ReadLine());
                                pretrazeniLekovi = lekController.DobaviLekovePoCeni(minCena, maksCena, projekat.ulogovaniKorisnik.TipKorisnika);
                            }
                            if(pretrazeniLekovi != null)
                            {
                                Console.WriteLine("Sifra leka:\tIme leka:\tProizvodjac leka:\tIzdaje se preko recepta:\tCena:");
                                for (int i = 0; i < pretrazeniLekovi.Count; i++)
                                    Console.WriteLine(pretrazeniLekovi[i]);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Sortiraj recepte po: 1. Sifra, 2. Lekar ili 3. Datum");
                        sortirajPo = Convert.ToInt32(Console.ReadLine());
                        List<Recept> recepti = projekat.receptController.PrikaziSveRecepte(sortirajPo - 1);
                        if (recepti != null)
                        {
                            Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                            for (int i = 0; i < recepti.Count; i++)
                                Console.WriteLine(recepti[i]);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Pretrazi recepte po: 1. Sifra, 2. Lekar, 3. Jmbg pacijenta ili 4. Jedan lek");
                        pretraziPo = Convert.ToInt32(Console.ReadLine());
                        if (pretraziPo == 1)
                        {
                            Console.WriteLine("Unesi sifru recepta: ");
                            int sifraRecepta = Convert.ToInt32(Console.ReadLine());
                            Recept receptPoSifri = receptController.DobaviReceptPoSifri(sifraRecepta);
                            if (receptPoSifri != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                Console.WriteLine(receptController.DobaviReceptPoSifri(sifraRecepta));
                            }
                        }
                        else
                        {
                            List<Recept> pretrazeniRecepti = new List<Recept>();
                            if (pretraziPo == 2)
                            {
                                Console.WriteLine("Unesi ime lekara: ");
                                String ime = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLekaru(ime);
                            }
                            else if (pretraziPo == 3)
                            {
                                Console.WriteLine("Unesi jmbg pacijenta: ");
                                String jmbg = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoJmbgPacijenta(jmbg);
                            }
                            else
                            {
                                Console.WriteLine("Unesi ime jednog leka: ");
                                String ime = Console.ReadLine();
                                pretrazeniRecepti = receptController.DobaviReceptePoLeku(ime);
                            }
                            if (pretrazeniRecepti != null)
                            {
                                Console.WriteLine("Sifra recepta:\tIme lekara:\tJmbg pacijenta:\t\tDatum izdavanja:\tLekovi:");
                                for (int i = 0; i < pretrazeniRecepti.Count; i++)
                                    Console.WriteLine(pretrazeniRecepti[i]);
                            }
                        }
                        break;
                    case 5:
                        Recept recept = new Recept();
                        Console.WriteLine("Unesi jmbg pacijenta: ");
                        recept.JmbgPacijenta = Console.ReadLine();
                        do
                        {
                            Console.WriteLine("Unesi ime leka: ");
                            String imeLeka = Console.ReadLine();
                            recept.Lekovi[imeLeka] = 0;
                            Console.WriteLine("Unesi kolicinu leka: ");
                            recept.Lekovi[imeLeka] += Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Da li zelite da dodate jos lekova (D/N)?");
                        } while (Console.ReadLine() == "D");
                        recept.Datum = DateTime.Now;
                        recept.Lekar = ulogovaniKorisnik.Ime;
                        receptController.KreirajRecept(recept);
                        break;
                    case 6:
                        projekat.ulogovaniKorisnik = null;
                        radi = false;
                        break;
                }
            } while (radi);
        }

    }
}
