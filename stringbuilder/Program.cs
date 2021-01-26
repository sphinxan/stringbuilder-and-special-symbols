using System;
using System.Text;

namespace stringbuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringBuilder - это класс, представляющий собой изменяемую строку
            var builder = new StringBuilder();
            
            builder.Append("Some "); //добавляет к строке подстроку
            builder.Append("string ");
            builder.Append("#15");
            builder.Remove(0, 5); //удаляет опред колво символов начиная с опред индекса
            builder.Insert(0, "test "); //вставляет подстроку начиная с опред индекса
            builder.Replace(",", "."); //заменяет все вхождения опред символа или подстроки на другой
            //Console.WriteLine(builder.ToString());
            //Также можно манипулировать отдельными символами
            builder[0] = 'T';
            //превратить в строку
            var str = builder.ToString();
            Console.WriteLine(str);

            //конкатенация большого количества строк "в лоб" потребует очень много памяти в куче, и будет работать медленно
            var str1 = "";
            for (int i = 0; i < 50000; i++)
            {
                str1 += i.ToString() + ", ";
            }
            //Конкатенация со StringBuilder работает в сотни раз быстрее
            var builder1 = new StringBuilder();
            for (int i = 0; i < 50000; i++)
            {
                builder1.Append(i);
                builder1.Append(", ");
            }

            /*var watch = new Stopwatch();
            watch.Start();
            /действия/
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);*/
        }

        //pop и push — это операции работы со стеком. push дописывает указанную строку в конец текста, а pop удаляет из конца указанное количество символов
        private static string ApplyCommands(string[] commands) //(push Привет! Это снова я! Пока!  pop 5  push Как твои успехи? Плохо?  push qwertyuiop  push 1234567890  pop 26)
        {
            var builder = new StringBuilder();
            foreach (var line in commands)
            {
                string[] words = line.Split(' ');
                if (words[0] == "push")
                    builder.Append(line.Substring(5));
                else builder.Remove(builder.Length - int.Parse(words[1]), int.Parse(words[1]));
            }
            return builder.ToString();
            /*или: foreach (var line in commands)
            {
                if (line.StartsWith("push"))
                    builder.Append(line.Substring(5));
                else
                    builder.Length -= int.Parse(line.Split(' ')[1]);
            }*/
        }


        //специальные символы
        static void Main1()
        {
            Console.WriteLine("First line\nSecond line"); //Символ перевода строки   выведет на новой строке вторую часть
            Console.WriteLine("10%\r20%\r30%"); //Символ возврата каретки   вывод:30%
            Console.WriteLine("10\t100\n10000\t1"); //Символ табуляции - плохой способ делать таблички
            Console.WriteLine("This is \" quotes"); //Вывод кавычки   вывод: This is " quotes
            //Console.WriteLine("C:\Users\admin"); // ошибка компиляции, Так нельзя, компилятор пытается воспринять \U как спецсимвол
            Console.WriteLine("C:\\Users\\admin"); //бэкслеш надо экранировать
            Console.WriteLine(@"C:\Users\admin"); //Или использовать особую строку, в которой спецсимволы не допускаются, с помощью @
            //Единственный символ, который нужно экранировать внутри особой строки - кавычки. 
            Console.WriteLine(@"This is "" quotes"); //Они экранируются удвоением   вывод: This is " quotes
        }


        //Форматированный вывод
        public static void Main2()
        {
            var a = 13;
            var b = 14.3456789;
            Console.WriteLine(a + " " + b); //Всегда можно писать так  выведет: 13 14,3456789
            //Но для больших документов это не удобно. Кроме того, не получится настроить, например, количество цифр после запятой, и можно так
            Console.WriteLine("{0} {1}", a, b); // 13 14,3456789

            //Для того, чтобы отформатировать строку без вывода, используйте string.Format
            var formattedString = string.Format("{0} {1}", a, b);

            //Форматированный вывод позволяет настроить точность округления
            Console.WriteLine("{0:0.000} {1:0.0000}", 1.23456, 1.23456); // 1,235 1,2346

            //Вывод завершающих нулей
            Console.WriteLine("{0:0.000} {1:0.###}", 1.2, 1.2); // 1,200 1,2

            //Добивание нулями слева
            Console.WriteLine("{0:D4}", 42); //0042

            //Разбиение на колонки и выравнивание по правому
            Console.WriteLine("{0,10}|\n{1,10}|", 12345, 123);
            //		12345|
            //		  123|

            //или левому краю
            Console.WriteLine("{0,-10}|\n{1,-10}|", 12345, 123);
            // 12345	|
            // 123		|

            //А также комбинации выравнивания и округления
            Console.WriteLine("{0,10:0.00}|\n{1,10:0.000}|", 1.45, 21.345);
            //		1,45|
            //	  21,345|

            //Форматирование времени и даты
            Console.WriteLine("{0:hh:mm:ss}", DateTime.Now); // 06:01:54

            // MM и mm — это Месяцы и минуты. Различаются только регистром.
            Console.WriteLine("{0:yy-MM-dd}", DateTime.Now); // 17-07-19

            // Можно менять количество букв и порядок:
            Console.WriteLine("{0:d-MM-yyyy}", DateTime.Now); // 1-12-2014

            //Фигурные скобки НЕ ЯВЛЯЮТСЯ спецсимволами шарпа:
            Console.WriteLine("{}"); //Это будет работать   вывод {}

            //Но они являются спецсимволами метода string.Format, и их нельзя использовать просто так,
            //если вызывается этот метод
            //Console.WriteLine("{0}{}", a); //Это скомпилируется, но выбросит исключение
            //Надо их экранировать удвоением
            Console.WriteLine("{0}{{}}", a); // 13{}
        }
    }
}
