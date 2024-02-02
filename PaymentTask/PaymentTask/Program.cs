using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAssistant
{
    internal class Program
    {
        private static char aileVeziyyeti;

        static void Main(string[] args)
        {
            Console.WriteLine("Əmək haqqını daxil edin:");
            double emekHaqqi = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Ailə vəziyyətini daxil edin (e/E - evli, s/S - subay):");
            char aileVeziyyeti = Convert.ToChar(Console.ReadLine());

            int ushaqSayi = 0;
            if (aileVeziyyeti != 's' && aileVeziyyeti != 'S')
            {
                Console.WriteLine("Uşaqların sayını daxil edin:");
                ushaqSayi = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Əlil olub olmamasını daxil edin (b/B - bəli, x/X - xeyr):");
            char elilStatusu = Convert.ToChar(Console.ReadLine());

            // Ailə müavinəti hesablama
            double aileMuvafiqeti = CalculateAileMuvafiqeti(aileVeziyyeti, ushaqSayi);

            // Uşaq pulu hesablama
            double usaqPulu = CalculateUsaqPulu(ushaqSayi);

            // Gəlir vergisi hesablama
            double gelirVergisiDerecesi = CalculateGelirVergisiDerecesi(emekHaqqi, elilStatusu);
            double gelirVergisiMebliyi = emekHaqqi * gelirVergisiDerecesi;

            // Əmək haqqının və xalis əmək haqqının hesablanması
            double emekHaqqiMiqdari = emekHaqqi + aileMuvafiqeti + usaqPulu;
            double xalisEmekHaqqi = emekHaqqiMiqdari - gelirVergisiMebliyi;

            // Əmək haqqının pul vahidlərinə çevrilməsi
            int[] pulVahidlari = { 200, 100, 50, 20, 10, 5, 1 };
            int[] pulSayi = CalculatePulSayi(xalisEmekHaqqi, pulVahidlari);

            // Nəticələrin ekrana çap edilməsi
            Console.WriteLine($"Ailə müavinəti: {aileMuvafiqeti} AZN");
            Console.WriteLine($"Uşaq pulu: {usaqPulu} AZN");
            Console.WriteLine($"Gəlir vergisi dərəcəsi: {gelirVergisiDerecesi * 100}%");
            Console.WriteLine($"Gəlir vergisinin məbləği: {gelirVergisiMebliyi} AZN");
            Console.WriteLine($"Ümumi əmək haqqı: {emekHaqqiMiqdari} AZN");
            Console.WriteLine("Xalis əmək haqqı vahidləri:");
            for (int i = 0; i < pulVahidlari.Length; i++)
            {
                Console.WriteLine($"{pulVahidlari[i]} AZN: {pulSayi[i]}");
            }
        }

        static double CalculateAileMuvafiqeti(char aileVeziyyeti, int ushaqSayi)
        {
            double aileMuvafiqeti = 0;

            if (aileVeziyyeti == 'e' || aileVeziyyeti == 'E')
            {
                aileMuvafiqeti += 50;
            }

            if (ushaqSayi > 0)
            {
                aileMuvafiqeti += 30 + (ushaqSayi - 1) * 5;
            }

            return aileMuvafiqeti;
        }

        static double CalculateUsaqPulu(int ushaqSayi)
        {
            return ushaqSayi * 15;
        }

        static double CalculateGelirVergisiDerecesi(double emekHaqqi, char elilStatusu)
        {
            double gelirVergisiDerecesi = 0;

            if (emekHaqqi <= 1000)
            {
                gelirVergisiDerecesi = 0.15;
            }
            else if (emekHaqqi <= 2000)
            {
                gelirVergisiDerecesi = 0.20;
            }
            else if (emekHaqqi <= 3000)
            {
                gelirVergisiDerecesi = 0.25;
            }
            else
            {
                gelirVergisiDerecesi = 0.30;
            }

            if (elilStatusu == 'b' || elilStatusu == 'B')
            {
                gelirVergisiDerecesi /= 2;
            }

            return gelirVergisiDerecesi;
        }

        static int[] CalculatePulSayi(double mebleg, int[] pulVahidlari)
        {
            int[] pulSayi = new int[pulVahidlari.Length];

            for (int i = 0; i < pulVahidlari.Length; i++)
            {
                pulSayi[i] = (int)(mebleg / pulVahidlari[i]);
                mebleg %= pulVahidlari[i];
            }

            return pulSayi;
        }
    }
}



