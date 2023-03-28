
using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = @"staff.txt";
        int userChoice = 0;
        string[] personInfo = new string[7];

        while (userChoice != 3)
        {
            Console.WriteLine("Выберите действие\n\t1 - Вывести данные на экран\n\t2 - Заполнить данные и добавить новую запись\n\t3 - Завершить работу");

            userChoice = Int32.Parse(Console.ReadLine());

            if (userChoice == 1)
            {
                PrintInfo(path);
            }
            else if (userChoice == 2)
            {
                ushort lastId = GetLastId(path);
                WriteData(path, TextToWrite(lastId));
            }
            else if (userChoice == 3)
            {
                break;
            }
            else if (userChoice == 4)
            {
                Console.WriteLine("Last ID = " + GetLastId(path));
            }
            else
            {
                Console.WriteLine("Попробуйте еще раз");
                continue;
            }
        }

    }

    private static string TextToWrite(ushort lastId)
    {
        ushort id = ++lastId;
        int age = 0;
        DateTime dateBirth;
        string? placeBirth = "";
        string[] textWriteArr = new string[7];
        string textWrite = "";

        for (int num = 0; num < 7; num++)
        {
            switch (num)
            {
                case 0:
                    textWriteArr[0] = id.ToString();

                    break;

                case 1:
                    textWriteArr[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm").ToString();

                    break;

                case 2:
                    Console.WriteLine("Введите Фамилию Имя Отчество");

                    textWriteArr[2] = Console.ReadLine();

                    break;


                case 3:
                    Console.WriteLine("Введите рост");

                    textWriteArr[4] = Console.ReadLine();

                    break;

                case 4:
                    Console.WriteLine("Введите дату рождения");

                    while (!DateTime.TryParse(Console.ReadLine(), out dateBirth))
                    {
                        Console.WriteLine("Некорректно введенны данные, попробуйте еще раз");
                    }

                    age = DateTime.Now.Year - dateBirth.Year;
                    if (DateTime.Now.DayOfYear < dateBirth.DayOfYear)
                    {
                        age--;
                    }
                    textWriteArr[5] = dateBirth.ToString("d");
                    textWriteArr[3] = age.ToString();

                    break;

                case 5:
                    Console.WriteLine("Введите место рождения");

                    placeBirth = Console.ReadLine();
                    textWriteArr[6] = placeBirth;

                    break;
            }
        }
        foreach (var item in textWriteArr)
        {
            textWrite = textWrite + item + '#';
        }
        return textWrite;
    }

    private static void WriteData(string path, string textWrite)
    {
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine(textWrite);
        }
    }

    private static void PrintInfo(string path)
    {
        if (File.Exists(path))
        {
            PrintHead();
            PrintInfoFromFile(GetInfo(path));
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------\nКонец файла");
        }
        else
        {
            Console.WriteLine("Выводить нечего, сначала внесите данные");
        }
    }

    private static void PrintInfoFromFile(string[] personInfo)
    {
        for (int i = 0; i <= personInfo.Length - 1; i++)
        {
            string[] info = personInfo[i].Split('#');
            Console.WriteLine("|{0, -3}|{1,-30}|{2, -35}|{3,-7}|{4,5}|{5,-17}|{6,-35}|", info[0], info[1], info[2], info[3], info[4], info[5], info[6]);
        }
    }

    private static void PrintHead()
    {
        Console.WriteLine("|{0, -3}|{1,-30}|{2, -35}|{3,-7}|{4,5}|{5,-17}|{6,-35}|" +
            "\n--------------------------------------------------------------------------------------------------------------------------------------------"
            , "ID", "Дата и время добавления записи", "Ф. И. О.", "Возраст", "Рост", "Дату рождения", "Место рождения");
    }

    private static string[] GetInfo(string path)
    {
        string[] personInfo = new string[GetLastId(path)];
        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string? line;
                int i = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    personInfo[i] = line;
                    i++;
                }
            }
        }
        return personInfo;
    }
    private static ushort GetLastId(string path)
    {
        string[] personInfo = new string[7];
        if (File.Exists(path))
        {
            personInfo = File.ReadLines(path).Last().Split('#');
        }
        ushort id = Convert.ToUInt16(personInfo[0]);
        return id;
    }
}