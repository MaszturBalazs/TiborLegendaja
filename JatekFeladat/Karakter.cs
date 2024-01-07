using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JatekFeladat
{
    internal class Karakter
    {
        //Random szám
        public Random rand = new Random();

        //AlapVáltozók
        public string Nev { get; set; }
        public int HP { get; set; }
        public int SebzesMin { get; set; }
        public int SebzesMax { get; set; }

        //Támadás egy random számot ír ki;
        public int Tamadas()
        {
            int tamadas = rand.Next(SebzesMin, SebzesMax);
            Console.WriteLine($"Megtámadtak!\t(-{tamadas}HP)");
            return tamadas;
        }

        //Kiiratás
        public override string ToString()
        {
            return $"\n======{Nev}======\nÉlet:\t{HP}\nSebzés:\t{SebzesMin}-{SebzesMax}\n============\n";
        }
    }

    class Varazslo : Karakter
    {
        //Varázslónál több változó is van.
        public int MP;
        public int XP;
        int XP_Limit;
        public int Szint;
        public Varazslo(int Nehezseg)
        {
            Nev = "Tibor";
            XP = 0;
            Szint = 1;
            if (Nehezseg == 1)
            {
                Console.WriteLine("Könnyű nehézséget választottad!");
                HP = 100;
                MP = 100;
                XP_Limit = 100;
                SebzesMin = 10;
                SebzesMax = 20;
            }
            else if (Nehezseg == 2)
            {
                Console.WriteLine("Nehéz nehézséget választottad!");
                HP = 60;
                MP = 60;
                XP_Limit = 80;
                SebzesMin = 15;
                SebzesMax = 25;
            }
            else
            {
                Console.WriteLine("Hibás nehézség volt megadva, alapértelmezetten a könnyű nehézségre hagyatkozunk.");
                HP = 100;
                MP = 100;
                XP_Limit = 100;
                SebzesMin = 10;
                SebzesMax = 20;
            }
        }

        public void Nyeres(int tipus)
        {
            Console.WriteLine("\nNyertél!\n");
            //Koboldot győztél le
            if(tipus == 1)
            {
                XP += 40;
                MP += 50;
            }
            //Élőhalott katonát győztél le
            else if (tipus == 2)
            {
                XP += 80;
                MP += 80;
            }
            //Tolvajt győztél le
            else if (tipus == 3)
            {
                XP += 60;
            }
            //Sárkányt győzted le
            else if (tipus == 4)
            {
                Console.WriteLine("Legyőzted a sárkányt!");
            }



        }

        //Támadás egy random számot ír ki
        public int Tamadas()
        {
            Console.WriteLine("\nMit szeretnél csinálni?\n1. Bottal ütés\n2. Varázslás\n3. Gyógyítás\n");
            int tipus = int.TryParse(Console.ReadLine(), out tipus) ? tipus : 8;
            
            int tamadas;
            if (tipus == 1)
            {
                tamadas = rand.Next(SebzesMin, SebzesMax);
                Console.WriteLine($"\nTámadtál! (-{tamadas}HP)\n");
                return tamadas;
            }
            else if (tipus == 2)
            {
                if(MP >= 10)
                {
                    tamadas = rand.Next(SebzesMin + 5, SebzesMax + 5);
                    Console.WriteLine($"\nTámadtál varázslattal! (-10MP|-{tamadas}HP)\n");
                    MP -= 10;
                    return tamadas;
                }
                else
                {
                    Console.WriteLine("\nNincs elég MP-d, ezért bottal ütsz!\n");
                    tamadas = rand.Next(SebzesMin, SebzesMax);
                    return tamadas;
                }
            }
            else if(tipus == 3)
            {
                Gyogyulas();
            }
            else
            {
                Console.WriteLine("\nMelléfogtál!\n");
                return 0;
            }
            return 0;
        }

        public void Gyogyulas()
        {
            if (MP >= 10)
            {
                Console.WriteLine($"\nGyógyultál! (+{20*Szint}HP)\n");
                MP -= 10;
                HP += 20 * Szint;
            }
            else
            {
                Console.WriteLine("\nNincs elég MP-d, melléfogtál!\n");
            }
        }

        public void Valasztas(out int valasz)
        {
            if (XP >= XP_Limit)
            {
                Szint++;
                Console.WriteLine("\nSzintet léptél! (+1 Szint)\n");
                HP += 20;
                MP += 20;
                SebzesMax += 5;
                SebzesMin += 5;
                XP_Limit += 20 * Szint;
                XP = 0;
            }
            Console.WriteLine("Mit akarsz csinálni?\n1.Csekkolás\n2.Harcolni!\n3.Gyógyítás\n4.Sárkánnyal harcolni!\nVálaszod: ");
            valasz = int.TryParse(Console.ReadLine(), out valasz) ? valasz : 8;
        }

        //Kiiratás
        public void ToString()
        {
            if (HP <= 10)
            {
                Console.WriteLine("Kevés életed maradt!");
            }
            Console.WriteLine($"\n======{Nev}======\nÉlet:\t{HP}\nMP:\t{MP}\nSebzés:\t{SebzesMin}-{SebzesMax}\nXP:\t[{XP}-{XP_Limit}]\nSzint:\t{Szint} (kell még {XP_Limit-XP}XP)\n============\n");
        }
    }

    class Ellenfel : Karakter
    {
        public Ellenfel(int tipus, int szint)
        {
            //Kobold ellenfél
            if(tipus == 1)
            {
                Nev = "Kobold";
                if (szint != 1)
                {
                    HP = 20 + (szint * 2);
                    SebzesMin = 2 + (szint * 2);
                    SebzesMax = 5 + (szint * 2);
                }
                else
                {
                    HP = 20;
                    SebzesMin = 2;
                    SebzesMax = 5;
                }
            }
            //Élőhalott katona ellenfél
            else if (tipus == 2)
            {
                Nev = "Élőhalott katona";
                if (szint != 1)
                {
                    HP = 40 + (szint * 2);
                    SebzesMin = 5 + (szint * 2);
                    SebzesMax = 10 + (szint * 2);
                }
                else
                {
                    HP = 40;
                    SebzesMin = 5;
                    SebzesMax = 10;
                }
            }
            //Sárkány ellenfél
            else if (tipus == 3)
            {
                Nev = "Sárkány";
                HP = 1000;
                SebzesMin = 30;
                SebzesMax = 70;
            }
            else
            {
                Console.WriteLine("Nem létező karakter!");
            }
        }
    }

    class Tolvaj : Karakter
    {
        public Tolvaj(int szint)
        {
            Nev = "Tolvaj";
            if (szint != 1)
            {
                HP = 15 + (szint * 2);
            }
            else
            {
                HP = 15;
            }
        }
        public void Lopas(ref int xp)
        {
            Console.WriteLine("Loptak tőled XP-t!");
            xp = (int)Math.Floor(xp * 0.8);
        }
    }

}
