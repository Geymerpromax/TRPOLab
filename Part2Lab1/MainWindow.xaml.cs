using System;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Lab2Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string startData = "0 ";
        public bool isResult = false;
        public bool isPow = false;
        public bool isFactorial = false;
        public string parameters = "+-*/";

        public void FirstInput()
        {
            if (window.Content.ToString() == startData)
            {
                window.Content = string.Empty;
            }
        }

        private void ButtonSMClick(object sender, RoutedEventArgs e)
        {
            if (window.Content.ToString().Contains(','))
            {
                string newString = window.Content.ToString().Replace(",", ".");
                window.Content = newString;
            }

            Button btn = (Button)sender;

            //if (isResult || btn.Content.ToString() == "*" || btn.Content.ToString() == "/" || btn.Content.ToString() == "+" || btn.Content.ToString() == "-")
            //{
            //    miniWindow.Content += window.Content.ToString();
            //    window.Content = startData;
            //    isResult = false;
            //}

            FirstInput();

            string value = window.Content.ToString();

            if (btn.Content.ToString() == ".")
            {
                IsPointEnds(ref value);
                window.Content = value;
            }
            else if (btn.Content.ToString() == "!")
            {
                IsFactorialEnds(ref value);
                window.Content = value;
            }
            else
            {
                window.Content += btn.Content.ToString();
            }

            if (btn.Content.ToString() == "1")
            {
                isFactorial = true;
            }

            if (btn.Content.ToString() == "^")
            {
                isPow = true;
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            window.Content = startData;
            miniWindow.Content = "= ";
            isPow = false;
        }

        private void PrintResult(object sender, RoutedEventArgs e)
        {
            string originalString = window.Content.ToString();
            string modifyString = originalString;

            string startString = "";
            string endString = "";

            
            
            /////////////////////////// Замена факториала произведением            
            modifyString = ReFactorial(originalString, modifyString, startString, endString);
            originalString = modifyString;

            ///////////////////////// Замена степени произведением
            modifyString = ReStepen(originalString, modifyString, startString, endString);
            originalString = modifyString;

            /////////////////////////// Замена корня произведением            
            modifyString = ReCoren(originalString, modifyString, startString, endString);
            originalString = modifyString;

            ////////////////////////// Вывод
            if (modifyString.Contains(','))
            {
                string newString = modifyString.Replace(",", ".");
                modifyString = newString;
            }
            
            double result = Convert.ToDouble(new DataTable().Compute(modifyString, ""));

            miniWindow.Content = window.Content.ToString() + " = ";

            window.Content = result.ToString();

            isResult = true;
        }

        private string ReFactorial(string originalString, string modifyString, string startString, string endString)
        {
            int countOfFactorial = 0;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i].Equals('!'))
                {
                    countOfFactorial++;
                }
            }
            for (int i = 0; i < countOfFactorial; i++)
            {
                
                //// Определяем позицию знака факториала
                int positionFactorial = originalString.IndexOf('!');

                //// Определяем основание (строка)                               
                StringBuilder reversOsnova = new StringBuilder();
                int lenghtOsnovanie = 0;
                int firstSymbolsPostion = 0;

                for (int j = 1; j < originalString.Length; j++)
                {

                    if ((positionFactorial - j) < 0 || parameters.Contains(originalString[positionFactorial - j]))
                    {
                        break;
                    }
                    reversOsnova.Append(originalString[positionFactorial - j].ToString());
                    lenghtOsnovanie++;
                    firstSymbolsPostion = positionFactorial - j;
                }

                StringBuilder osnova = new StringBuilder();
                for (int k = reversOsnova.Length - 1; k >= 0; k--)
                {
                    osnova.Append(reversOsnova[k]);
                }

                int pogreshnostMassiva = 1;

                //// Сохраняем строку до основания степени    
                startString = originalString.Remove(positionFactorial - lenghtOsnovanie, originalString.Length - firstSymbolsPostion);

                //// Сохраняем строку после факториала степени
                endString = originalString.Remove(0, originalString.IndexOf('!') + pogreshnostMassiva);

                //// Получаем основание степени (int)             
                int osnovanie = Convert.ToInt32(osnova.ToString());


                //// Расчёт и формирование новой строки
                startString += Factorial(osnovanie) + endString;

                originalString = startString;

                modifyString = originalString;                
            }
            return modifyString;
        }
        private string ReStepen(string originalString, string modifyString, string startString, string endString)
        {
            int countOfStepen = 0;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i].Equals('^'))
                {
                    countOfStepen++;
                }
            }
            for (int i = 0; i < countOfStepen; i++)
            {
                //// Определяем позицию знака степени
                int positionStepeni = originalString.IndexOf('^');

                //// Определяем основание (строка)                               
                StringBuilder reversOsnova = new StringBuilder();
                int lenghtOsnovanie = 0;
                int firstSymbolsPostion = 0;

                for (int j = 1; j < originalString.Length; j++)
                {

                    if ((positionStepeni - j) < 0 || parameters.Contains(originalString[positionStepeni - j]))
                    {
                        break;
                    }
                    reversOsnova.Append(originalString[positionStepeni - j].ToString());
                    lenghtOsnovanie++;
                    firstSymbolsPostion = positionStepeni - j;
                }

                StringBuilder osnova = new StringBuilder();
                for (int k = reversOsnova.Length - 1; k >= 0; k--)
                {
                    osnova.Append(reversOsnova[k]);
                }

                //// Определяем показатель (строка)               
                StringBuilder pokaz = new StringBuilder();
                int lenghtPokazatel = 0;
                int secondSymbolsPostion = 0;


                for (int j = 1; j < originalString.Length - positionStepeni + j; j++)
                {
                    if ((positionStepeni + j) > originalString.Length - 1 || parameters.Contains(originalString[positionStepeni + j]))
                    {
                        break;
                    }
                    pokaz.Append(originalString[positionStepeni + j].ToString());
                    lenghtPokazatel++;
                    secondSymbolsPostion = positionStepeni + j;
                }

                int pogreshnostMassiva = 1;

                //// Сохраняем строку до основания степени    
                startString = originalString.Remove(positionStepeni - lenghtOsnovanie, originalString.Length - firstSymbolsPostion);

                //// Сохраняем строку после показателя степени
                endString = originalString.Remove(0, secondSymbolsPostion + pogreshnostMassiva);

                //// Получаем основание степени (int)             
                int osnovanie = Convert.ToInt32(osnova.ToString());

                //// Получаем показатель степени (int)
                int pokazatel = Convert.ToInt32(pokaz.ToString());

                //// Расчёт и формирование новой строки
                startString += Math.Pow(osnovanie, pokazatel) + endString;

                originalString = startString;

                modifyString = originalString;
            }
            return modifyString;
        }
        private string ReCoren(string originalString, string modifyString, string startString, string endString)
        {
            int countOfCoren = 0;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i].Equals('√'))
                {
                    countOfCoren++;
                }
            }
            for (int i = 0; i < countOfCoren; i++)
            {
                //// Определяем позицию знака корня
                int positionStepeni = originalString.IndexOf('√');

                //// Определяем основание (строка)    

                StringBuilder osnova = new StringBuilder();
                int lenghtOsnovanie = 0;
                int secondSymbolsPostion = 0;


                for (int j = 1; j < originalString.Length - positionStepeni + j; j++)
                {
                    if ((positionStepeni + j) > originalString.Length - 1 || parameters.Contains(originalString[positionStepeni + j]))
                    {
                        break;
                    }
                    osnova.Append(originalString[positionStepeni + j].ToString());
                    lenghtOsnovanie++;
                    secondSymbolsPostion = positionStepeni + j;
                }

                int pogreshnostMassiva = 1;


                //// Сохраняем строку после основания корня
                endString = originalString.Remove(0, secondSymbolsPostion + pogreshnostMassiva);

                //// Получаем основание корня (int)
                int osnovanie = Convert.ToInt32(osnova.ToString());

                //// Расчёт и формирование новой строки
                startString += Math.Pow(osnovanie, 0.5).ToString() + endString;

                originalString = startString;

                modifyString = originalString;
            }
            return modifyString;
        }

        private void IsPointEnds(ref string str)
        {
            if (str.EndsWith('.') == false)
            {
                if (str.EndsWith(' ') || str == string.Empty || str.EndsWith('+') || str.EndsWith('-') || str.EndsWith('/') || str.EndsWith('*'))
                {
                    str += "0.";
                }
                else
                {
                    str += ".";
                }
            }
        }

        private void IsFactorialEnds(ref string str)
        {
            if (str.EndsWith('!') == false)
            {
                if (str.EndsWith(' ') || str == string.Empty || str.EndsWith('+') || str.EndsWith('-') || str.EndsWith('/') || str.EndsWith('*'))
                {
                    str += "0!";
                }
                else
                {
                    str += "!";
                }
            }
        }

        public static long Factorial(long n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }

    }
}