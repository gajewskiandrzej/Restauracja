using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restauracja
{
    public enum StanStolika {wolny, zajęty, zarezerwowany, wycofany }

    public abstract class ObslugaGosci
    {
        public void WyszukajWolnyStolik()
        {
            foreach (Stolik wolnyStolik in Restauracja.stoliki)
            {
                if (wolnyStolik.StanStolika == StanStolika.wolny)
                {
                    Console.WriteLine("Wolny stolik: " + wolnyStolik);
                }
            }
        }

        public void ZarezerwujStolik(int nrStolika, string nazwiskoGoscia)
        {
            foreach (Stolik mojStolik in Restauracja.stoliki)
            {
                if (mojStolik.NrStolika == nrStolika)
                {
                    mojStolik.StanStolika = StanStolika.zarezerwowany;
                    mojStolik.NazwiskoGoscia = nazwiskoGoscia;
                }
            }
        }
    }

    public class Restauracja
    {
        public static Queue<Stolik> stoliki = new Queue<Stolik>();

        public void PokazStoliki()
        {
            foreach (Stolik stolik in stoliki)
            {
                Console.WriteLine("Stolik: " + stolik);
            }
        }
    }

    public class Stolik
    {
        public int NrStolika { get; set; }
        public StanStolika StanStolika { get; set;}
        public string NazwiskoGoscia { get; set; }

        public Stolik(int nrStolika, StanStolika stanStolika, string nazwiskoGoscia)
        {
            NrStolika = nrStolika;
            stanStolika = StanStolika;
            nazwiskoGoscia = NazwiskoGoscia;
        }

        public Stolik()
        {
            NrStolika = 0;
            StanStolika = StanStolika.wolny;
            NazwiskoGoscia = string.Empty;
        }

        public void Zarezerwuj(int nrStolika)
        {
            foreach (Stolik stolik in Restauracja.stoliki)
            {
                if (stolik.NrStolika == nrStolika)
                {
                    stolik.StanStolika = StanStolika.zarezerwowany;
                }
            }
        }

        public void Wydaj(int nrStolika, string nazwiskoGoscia)
        {
            foreach (Stolik stolik in Restauracja.stoliki)
            {
                if (stolik.NrStolika == nrStolika)
                {
                    stolik.StanStolika = StanStolika.zajęty;
                    stolik.NazwiskoGoscia = nazwiskoGoscia;
                }
            }
        }

        public void Wycofaj(int nrStolika)
        {
            foreach (Stolik stolik in Restauracja.stoliki)
            {
                if (stolik.NrStolika == nrStolika)
                {
                    stolik.StanStolika = StanStolika.wycofany;
                }
            }
        }

        public void Zwolnij(int nrStolika)
        {
            foreach (Stolik stolik in Restauracja.stoliki)
            {
                if (stolik.NrStolika == nrStolika)
                {
                    stolik.StanStolika = StanStolika.wolny;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Stolik nr: {0} Jest: {1} Nazwisko gościa {2}", NrStolika, StanStolika, NazwiskoGoscia);
        }

    }

    public class Gosc : ObslugaGosci
    {
        string Nazwisko { get; set; }
        public Gosc(string nazwisko)
        {
            Nazwisko = nazwisko;
        }
    }

    public class Kelner : ObslugaGosci
    {
        string NazwiskoKelnera { get; set; }
        public Kelner(string nazwisko)
        {
            NazwiskoKelnera = nazwisko;
        }
    }

    public class Kierownik
    {
        string NazwiskoKierownika { get; set; }
        public Kierownik(string nazwisko)
        {
            NazwiskoKierownika = nazwisko;
        }

        public void DodajStolik(Stolik stolik)
        {
            Restauracja.stoliki.Enqueue(stolik);
        }

        public void UsunStolik(int nrStolika)
        {
            foreach (Stolik stolik in Restauracja.stoliki)
            {
                if (stolik.NrStolika == nrStolika)
                {
                    Restauracja.stoliki.Dequeue();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RESTAURACJA KUBUŚ WITA");
            Console.WriteLine("=================================================");

            Restauracja restauracjaKubus = new Restauracja();
            Kierownik panKierownik = new Kierownik("Mieczysław");
            Kelner kelner = new Kelner("Pan Stasiu");

            Stolik stolikNr1 = new Stolik(1,StanStolika.wolny,"");
            Stolik stolikNr2 = new Stolik(2, StanStolika.wolny, "");
            Stolik stolikNr3 = new Stolik(3, StanStolika.wolny, "");
            Stolik stolikNr4 = new Stolik(4, StanStolika.wolny, "");
            Stolik stolikNr5 = new Stolik(5, StanStolika.wolny, "");

            panKierownik.DodajStolik(stolikNr1);
            panKierownik.DodajStolik(stolikNr2);
            panKierownik.DodajStolik(stolikNr3);
            panKierownik.DodajStolik(stolikNr4);
            panKierownik.DodajStolik(stolikNr5);

            restauracjaKubus.PokazStoliki();

            Gosc gosc1 = new Gosc("Gajewski");
            Gosc gosc2 = new Gosc("Drozd");
            Gosc gosc3 = new Gosc("Duda");
            Gosc gosc4 = new Gosc("Czekała");

            Console.WriteLine("Wyszukiwanie wolnych stolików przez gościa Gajewski");
            Console.WriteLine("===================================================");
            gosc1.WyszukajWolnyStolik();

            gosc1.ZarezerwujStolik(1,"Gajewski");
            kelner.ZarezerwujStolik(2, "Drozd");
            stolikNr1.Wydaj(1, "Gajewski");

            Console.WriteLine("Stan stolików po rezerwacji");
            Console.WriteLine("===================================================");

            restauracjaKubus.PokazStoliki();

            Console.ReadKey();

        }
    }
}
