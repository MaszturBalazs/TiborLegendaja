using System.Threading.Channels;

namespace JatekFeladat
{

    internal class Program
    {
        public static void Harcolas(Varazslo varazslo, Karakter ellenfeled, int ellenfel, ref bool jatekvege, ref bool nyertel)
        {
            while (true)
            {
                varazslo.ToString();
                Console.WriteLine(ellenfeled.ToString());
                Console.WriteLine("\n----------------------------------------");
                ellenfeled.HP -= varazslo.Tamadas();
                //Ha nyertél
                if (ellenfeled.HP <= 0 && ellenfel != 4)
                {
                    varazslo.Nyeres(ellenfel);
                    break;
                }
                //Ha a tipusa sárkány és nyertél
                if(ellenfeled.HP <= 0)
                {
                    varazslo.Nyeres(ellenfel);
                    nyertel = true;
                    jatekvege = true;
                    break;
                }
                Console.WriteLine(ellenfeled.ToString());
                varazslo.HP -= ellenfeled.Tamadas();
                if (varazslo.HP <= 0)
                {
                    jatekvege = true;
                    break;
                }
                Console.WriteLine("\n------------Következő menet!------------");
            }
        }

        static void Main(string[] args)
        {
            //Random szám az ellenfél választáshoz
            Random rand = new Random();

            //Üdvözöljük a játékost
            Console.WriteLine("Üdvözöllek a játékban!");
            Console.WriteLine("A neved Tibor és egy varázsló vagy!");
            Console.WriteLine("Képzeld el magad egy szép erdőben.");
            Console.WriteLine("Válaszd ki a nehézségi szintedet:\n1. Könnyű\n2. Nehéz");
            int valasz = int.TryParse(Console.ReadLine(), out valasz) ? valasz : 3;
            Varazslo Tibor = new Varazslo(valasz);

            bool jatekvege = false;
            bool nyertel = false;
            int ellenfel = 0;

            Tibor.ToString();

            //Játékmenet

            while (!jatekvege)
            {
                //Választás menüje
                Tibor.Valasztas(out valasz);
                //Ekkor nézzük meg, hogy milyen statisztikái vannak a karakternek
                if (valasz == 1)
                {
                    Tibor.ToString();
                }
                //Harcolás
                else if (valasz == 2)
                {
                    ellenfel = rand.Next(1, 3);
                    if (ellenfel == 1 || ellenfel == 2)
                    {
                        Ellenfel ellenfeled = new Ellenfel(ellenfel,Tibor.Szint);
                        Harcolas(Tibor, ellenfeled, ellenfel, ref jatekvege, ref nyertel);
                    }
                    else if (ellenfel == 3)
                    {
                        Tolvaj tolvaj = new Tolvaj(Tibor.Szint);
                        Console.WriteLine(tolvaj.ToString());
                        tolvaj.HP -= Tibor.Tamadas();
                        if (tolvaj.HP <= 0)
                        {
                            Tibor.Nyeres(ellenfel);
                        }
                        else
                        {
                            tolvaj.Lopas(ref Tibor.XP);
                        }
                    }
                }
                //Gyógyulás
                else if (valasz == 3)
                {
                    
                    Tibor.Gyogyulas();
                }
                //Sárkánnyal Harcolás
                else if (valasz == 4)
                {
                    //3-as az "Ellenfél" tipusa, ami azt jelenti, hogy egy sárkánnyal harcolsz!
                    Ellenfel ellenfeled = new Ellenfel(3, Tibor.Szint);
                    //Az ellenfél "4", ami azt jelenti, hogy egy sárkánnyal harcolsz!
                    Harcolas(Tibor, ellenfeled, 4, ref jatekvege, ref nyertel);
                }
                else {
                    Console.WriteLine("Melléfogtál!");
                }

            }


            if (nyertel) {
                Console.WriteLine("\nSikerrel jártál! Megnyerted a nagy kincsládát!");
            }
            else
            {
                Console.WriteLine("\nPróbáld újra!");
            }
            
        }
    }
}