using System;
using MassaKDriver100;
using System.Net.Sockets;
using System.Text;

namespace ScalesMassaK_tst
{
    class Program
    {
        //private const int port = 5001;
        //private const string server = "10.10.11.130:5001";

        static void Main(string[] args)
        {
            

            Scales Scales = new Scales();
            int res;

            try
            {                
                //string server;
                //Console.Write("Введите Адрес подключения: ");
                //Scales.Connection = Console.ReadLine();
                Scales.Connection = ("10.10.11.130:5001");
                
                res = Scales.OpenConnection();
                                
                if (res == 0)
                {
                    Console.WriteLine(
                    "Тест подключения весов к порту: "
                    //+ server
                    + " успешно пройден! "
                    + " параметр = "
                    + res.ToString());                    
                }
                else 
                {
                    Console.WriteLine(
                    "Ошибка подключения к весам "
                    //+ server
                    + " параметр = "
                    + res.ToString());
                }                

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            try
            {
                //string Result = "0.000";
                res = Scales.ReadWeight();
                if (res == 0)
                {
                    res = Scales.Division;
                    float KoefDiv = 1.0F;

                    if (res == 0) //100 мг
                    { KoefDiv = 0.000001F; }
                    else if (res == 1) //1 г
                    { KoefDiv = 0.001F; }
                    else if (res == 2) //10 г
                    { KoefDiv = 0.01F; }
                    else if (res == 3) //100 г
                    { KoefDiv = 0.1F; }
                    else if (res == 4) //1 кг    
                    { KoefDiv = 1F; } 

                    Console.WriteLine(res.ToString() + " / " + KoefDiv.ToString());
                    if (Scales.Stable == 1)
                    {
                        float sW = Scales.Weight;
                        float Weight = KoefDiv * sW;

                        Scales.CloseConnection();
                        Console.WriteLine(
                              "Значение индикатора: " + sW
                            + System.Environment.NewLine
                            + " Полученный вес " + Weight
                            + System.Environment.NewLine
                            + " цена деления " + Convert.ToString(KoefDiv));
                    }
                    
                }
                else
                {
                    Scales.CloseConnection();

                    Console.WriteLine(
                    "Ошибка полученния веса "
                    + res.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            Console.ReadKey();
        }
    }
}
