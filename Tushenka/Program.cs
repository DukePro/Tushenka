using static System.Console;

namespace Tushenka
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private const string ShowAllCommand = "1";
        private const string ShowExpiredCommand = "2";
        private const string Exit = "0";

        public void Run()
        {
            string userInput;
            bool isExit = false;

            Database database = new Database();
            database.CreateTushenkaCans();

            while (isExit == false)
            {
                WriteLine();
                WriteLine(ShowAllCommand + " - Показать всех");
                WriteLine(ShowExpiredCommand + " - Показать просрочку");
                WriteLine(Exit + " - Выход\n");

                userInput = ReadLine();

                switch (userInput)
                {
                    case ShowAllCommand:
                        database.ShowAll();
                        break;

                    case ShowExpiredCommand:
                        database.ShowExpiredCans();
                        break;

                    case Exit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Tushenka
    {
        public Tushenka(string name, int productionDate, int yearsToExpire)
        {
            Name = name;
            ProductionDate = productionDate;
            DateOfExpire = productionDate + yearsToExpire;
        }

        public string Name { get; private set; }
        public int ProductionDate { get; private set; }
        public int DateOfExpire { get; private set; }
    }

    class Database
    {
        private int _yearsToExpire = 5;
        private int _ammountOfRecords = 20;
        private List<Tushenka> _tushenkaCans = new List<Tushenka>();

        public void ShowAll(List<Tushenka>? tushenkaCans = null)
        {
            if (tushenkaCans == null)
            {
                tushenkaCans = _tushenkaCans;
            }

            foreach (var tushenka in tushenkaCans)
            {
                WriteLine($"{tushenka.Name}, Год изготовления: {tushenka.ProductionDate}, Годен до: {tushenka.DateOfExpire}");
            }
        }

        public void ShowExpiredCans()
        {
            WriteLine("Показать просрочку");

            var expiredTushenkaCans = _tushenkaCans.Where(tushenkaCan => tushenkaCan.DateOfExpire < DateTime.Now.Year).ToList();

            ShowAll(expiredTushenkaCans);
        }

        public void CreateTushenkaCans()
        {
            for (int i = 0; i < _ammountOfRecords; i++)
            {
                _tushenkaCans.Add(new Tushenka(GetName(), GetProductionDate(), _yearsToExpire));
            }
        }

        private string GetName()
        {
            string[] tushenkaNames =
            [
        "Божья коровка ",
        "Му-му",
        "Жадина-говядина",
        "Коровомешалка",
        "Марш на фарш",
        "Хрю-Хрю",
        "Душили-тушили",
        "Герасим",
        "Свинка Пеппа",
        "Весёлый Минотавр",
        "Гаврюша",
        "Здоровенная скотина",
        "Весёлый Пятачок",
        "Suzuki Hayabusa"
            ];

            string name = tushenkaNames[Utils.GetRandomNumber(tushenkaNames.Length - 1)];
            return name;
        }

        private int GetProductionDate()
        {
            int minDate = 2010;
            int maxDate = DateTime.Now.Year;

            return Utils.GetRandomNumber(minDate, maxDate);
        }
    }

    class Utils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static int GetRandomNumber(int maxValue)
        {
            return s_random.Next(maxValue);
        }
    }
}

//Есть набор тушенки. У тушенки есть название, год производства и срок годности.
//Написать запрос для получения всех просроченных банок тушенки.
//Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев. 
