using static System.Console;

namespace StewedMeatCans
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
        public void Run()
        {
            const string ShowAllCommand = "1";
            const string ShowExpiredCommand = "2";
            const string Exit = "0";

            string userInput;
            bool isExit = false;

            Database database = new Database();
            database.CreateStewedMeatCans();

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

    class StewedMeat
    {
        public StewedMeat(string name, int productionDate, int yearsToExpire)
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
        private List<StewedMeat> _stewedMeatCans = new List<StewedMeat>();

        public void ShowAll(List<StewedMeat>? stewedMeatCans = null)
        {
            if (stewedMeatCans == null)
            {
                stewedMeatCans = _stewedMeatCans;
            }

            foreach (var stewedMeat in stewedMeatCans)
            {
                WriteLine($"{stewedMeat.Name}, Год изготовления: {stewedMeat.ProductionDate}, Годен до: {stewedMeat.DateOfExpire}");
            }
        }

        public void ShowExpiredCans()
        {
            WriteLine("Показать просрочку");

            var expiredStewedMeatCans = _stewedMeatCans.Where(stewedMeatCan => stewedMeatCan.DateOfExpire < DateTime.Now.Year).ToList();

            ShowAll(expiredStewedMeatCans);
        }

        public void CreateStewedMeatCans()
        {
            for (int i = 0; i < _ammountOfRecords; i++)
            {
                _stewedMeatCans.Add(new StewedMeat(GetName(), GetProductionDate(), _yearsToExpire));
            }
        }

        private string GetName()
        {
            string[] stewedMeatNames =
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

            string name = stewedMeatNames[Utils.GetRandomNumber(stewedMeatNames.Length - 1)];
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
