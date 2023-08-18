using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Xml.Schema;

namespace FinalVaerksted
{
    internal class Program
    {
        static List<Bil> bilListe = new List<Bil>();
        static List<Bil> fejlBilListe = new List<Bil>();

        static void Main(string[] args)
        {
            
            FejlBil();
            while (true) { Menu(); }

        }

        static void Menu()
        {
           // Console.WriteLine(bilListe);
            Console.Clear();
            VisBiler();
            Console.WriteLine("\nVelkommen til Værkstedet!");
            Console.WriteLine("\n--Dette er hovedmenuen --\n");
            Console.WriteLine("Tryk 1. for at Registrere ny kunde");
            Console.WriteLine("Tryk 2. for at vise kunde-kontaktinfo");
            Console.WriteLine("Tryk 3. for at afslutte programmet (fyraften, tak for i dag)");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    OpretKunde();
                    Console.WriteLine("\nTryk på vilkårlig tast for at gå videre");
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    FindKunde();
                    Console.ReadKey();
                    break;

                case ConsoleKey.D3:
                    Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("Ugyldig tast indtastet");
                    break;
            }


        }

        static Person OpretPerson()
        {
            Console.WriteLine("\n-- Opret kunde menu --");

            Person person = new Person();
            Console.Write("Fornavn: ");
            person.Fornavn = Console.ReadLine();
            Console.Write("Efternavn: ");
            person.Efternavn = Console.ReadLine();
            Console.Write("Telefonnummer: ");
            person.TelefonNr = Console.ReadLine();
            return person;
        }

        static Bil OpretBil()
        {

            Console.WriteLine("\n-- Opret bil menu --");

            Bil bil = new Bil();
            Console.Write("Nummerplade: ");
            bil.Nummerplade = Console.ReadLine();
            Console.Write("Mærke: ");
            bil.Mærke = Console.ReadLine();
            Console.Write("Model: ");
            bil.Model = Console.ReadLine();
            Console.Write("Motorstørrelse: ");
            bil.Motorstørrelse = Console.ReadLine();
            Console.Write("Registreringsdato: ");
            bil.Registreringsdato = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Årgang: ");
            bil.Årgang = Convert.ToInt32(Console.ReadLine());
            Console.Write("Sidst synet dato: ");
            bil.SidsteSyn = Convert.ToDateTime(Console.ReadLine());

            return bil;
                       
        }

        private static void OpretKunde()
        {
            Person ejer = OpretPerson();
            Bil bil = OpretBil();
            bil.Ejer = ejer;
            
            bilListe.Add(bil);

            string str = ManglerSyn(bil) ? "\nBilen skal synes" : "\nBilen skal IKKE synes";
            Console.WriteLine(str);

            string str2 = ErBilUlovlig(bil);
            if (str2 != null) Console.WriteLine("Bilen har følgende fabriksfejl: " + str2);

           
            
        }


        static bool ManglerSyn(Bil bil)
        {
            if (bil.Registreringsdato.AddYears(5) >= DateTime.Now) return false;

            if (bil.SidsteSyn.AddYears(2) >= DateTime.Now) return false;
            return true;
        }

        static void VisKunde(Person ejer)
        {
            Console.WriteLine($"Ejer af bilen: {ejer.Fornavn} {ejer.Efternavn} \nTelefonnummer: {ejer.TelefonNr}");
        }




        private static void FindKunde()
        {
            Console.Clear();
            Console.WriteLine("Indtast nummerplade for at finde kundens oplysninger:");
            string input = Console.ReadLine();

            foreach (var bil in bilListe) 
            {
                VisKunde(bil.Ejer);
            }

         

        }

        
        static void FejlBil()
        {
            Bil bil1 = new Bil() { Mærke = "Alfa Romeo", Model = "Giulia", Årgang = 2019, Registreringsdato = new DateTime(2021, 8, 1), Fabriksfejl = "Udstødning" };
            Bil bil2 = new Bil() { Mærke = "Fiat", Model = "Punto", Årgang = 2010, Registreringsdato = new DateTime(2018, 1, 1), Fabriksfejl = "Styretøjet" };
            fejlBilListe.Add(bil1);
            fejlBilListe.Add(bil2);
             
        }

        static String ErBilUlovlig(Bil bil)
        {
            foreach (var fejlBil in fejlBilListe)
            {
                if (bil.Mærke == fejlBil.Mærke &&
                    bil.Model == fejlBil.Model &&
                    bil.Årgang <= fejlBil.Årgang) return fejlBil.Fabriksfejl;
            }
            return null;
        }

        static void VisBiler()
        {
            if (bilListe.Count > 0)
            {
                foreach(Bil bil in bilListe)
                {
                    VisBil(bil);
                    Console.WriteLine();
                }
            }
        }

        static void VisBil(Bil bil)
        {
            Console.WriteLine($"\nBil: {bil.Mærke} {bil.Model} \tNummerplade: {bil.Nummerplade}");
            Console.WriteLine($"Reg.Dato: {bil.Registreringsdato.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)} \tSidst synet: {bil.SidsteSyn.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)}");
        
    }





    }
}