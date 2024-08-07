using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
namespace Coursework
{
    internal class Program
    {
        static readonly string[] Names =
        {
                        "Liam", "Noah", "Oliver", "Elijah", "William",
    "James", "Benjamin", "Lucas", "Henry", "Alexander",
    "Mason", "Michael", "Ethan", "Daniel", "Jacob",
    "Logan", "Jackson", "Levi", "Sebastian", "Mateo",
    "Jack", "Owen", "Theodore", "Aiden", "Samuel",
    "Joseph", "John", "David", "Wyatt", "Matthew",
    "Luke", "Asher", "Carter", "Julian", "Grayson",
    "Leo", "Jayden", "Gabriel", "Isaac", "Lincoln",
    "Anthony", "Hudson", "Dylan", "Ezra", "Thomas",
    "Charles", "Christopher", "Jaxon", "Maverick", "Josiah",
    "Isaiah", "Andrew", "Elias", "Joshua", "Nathan",
    "Caleb", "Ryan", "Adrian", "Miles", "Eli",
    "Nolan", "Christian", "Aaron", "Cameron", "Ezekiel",
    "Colton", "Luca", "Landon", "Hunter", "Jonathan",
    "Santiago", "Axel", "Easton", "Cooper", "Jeremiah",
    "Angel", "Roman", "Connor", "Jameson", "Robert",
    "Greyson", "Jordan", "Ian", "Carson", "Jaxson",
    "Leonardo", "Nicholas", "Dominic", "Austin", "Everett",
    "Brooks", "Xavier", "Kai", "Jose", "Parker",
    "Adam", "Jace", "Wesley", "Kayden", "Silas",
    "Bennett", "Declan", "Waylon", "Weston", "Evan",
    "Emmett", "Micah", "Ryder", "Beau", "Damian",
    "Brayden", "Gael", "Rowan", "Harrison", "Bryson","Ava", "Sophia", "Isabella", "Mia", "Amelia",
    "Harper", "Evelyn", "Abigail", "Emily", "Elizabeth",
    "Sofia", "Avery", "Ella", "Scarlett", "Grace",
    "Chloe", "Victoria", "Riley", "Aria", "Lily",
    "Aubrey", "Zoey", "Penelope", "Lillian", "Addison",
    "Layla", "Natalie", "Camila", "Hannah", "Brooklyn",
    "Zoe", "Nora", "Leah", "Savannah", "Audrey",
    "Claire", "Eleanor", "Skylar", "Ellie", "Samantha",
    "Stella", "Paisley", "Violet", "Mila", "Allison",
    "Alexa", "Anna", "Hazel", "Aaliyah", "Ariana",
    "Lucy", "Caroline", "Sarah", "Genesis", "Kennedy",
    "Sadie", "Gabriella", "Madison", "Adeline", "Maya",
    "Autumn", "Aurora", "Piper", "Hailey", "Arianna",
    "Kaylee", "Ruby", "Serenity", "Eva", "Naomi",
    "Nevaeh", "Alice", "Luna", "Bella", "Quinn",
    "Lydia", "Peyton", "Melanie", "Kylie", "Aubree",
    "Mackenzie", "Kinsley", "Cora", "Julia", "Taylor",
    "Katherine", "Madeline", "Gianna", "Eliana", "Elena",
    "Vivian", "Willow", "Reagan", "Brianna", "Clara",
    "Faith", "Ashley", "Emilia", "Isabelle", "Annabelle",
    "Rylee", "Valentina", "Everly", "Hadley", "Sophie",
    "Alexandra", "Natalia", "Ivy", "Maria", "Josephine",
    "Delilah", "Bailey", "Jade", "Ximena", "Alexis",
    "Alyssa", "Brielle", "Jasmine", "Liliana", "Adalynn"
};
        static readonly string[] Surnames = { "Smith", "Johnson", "Williams", "Brown", "Jones",
                "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
                "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
                "Thomas", "Taylor", "Moore", "Jackson", "Martin",
                "Lee", "Perez", "Thompson", "White", "Harris",
                "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
                "Walker", "Young", "Allen", "King", "Wright",
                "Scott", "Torres", "Nguyen", "Hill", "Flores",
                "Green", "Adams", "Nelson", "Baker", "Hall",
                "Rivera", "Campbell", "Mitchell", "Carter", "Roberts",
                "Gomez", "Phillips", "Evans", "Turner", "Diaz",
                "Parker", "Cruz", "Edwards", "Collins", "Reyes",
                "Stewart", "Morris", "Morales", "Murphy", "Cook",
                "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper",
                "Peterson", "Bailey", "Reed", "Kelly", "Howard",
                "Ramos", "Kim", "Cox", "Ward", "Richardson",
                "Watson", "Brooks", "Chavez", "Wood", "James",
                "Bennett", "Gray", "Mendoza", "Ruiz", "Hughes",
                "Price", "Alvarez", "Castillo", "Sanders", "Patel",
                "Myers", "Long", "Ross", "Foster", "Jimenez",
                "Powell", "Jenkins", "Perry", "Russell", "Sullivan",
                "Bell", "Coleman", "Butler", "Henderson", "Barnes",
                "Gonzales", "Fisher", "Vasquez", "Simmons", "Romero",
                "Jordan", "Patterson", "Alexander", "Hamilton", "Graham",
                "Reynolds", "Griffin", "Wallace", "Moreno", "West" };
        static readonly string[] Specials = {
                        "Allergology", "Anesthesiology", "Cardiology", "Dermatology", "Emergency Medicine",
    "Endocrinology", "Gastroenterology", "Geriatrics", "Hematology", "Hepatology",
    "Infectious Disease", "Neonatology", "Nephrology", "Neurology", "Obstetrics",
    "Oncology", "Ophthalmology", "Orthopedics", "Otolaryngology", "Pathology",
    "Pediatrics", "Psychiatry", "Pulmonology", "Radiology", "Rheumatology",
    "Urology", "Surgery", "Plastic Surgery", "Transplant Surgery", "Traumatology",
    "Bariatric Surgery", "Cardiothoracic Surgery", "Vascular Surgery", "Proctology", "Podiatry",
    "Pediatric Surgery", "Osteopathy", "Oncologic Surgery", "Neurosurgery", "Maxillofacial Surgery",
    "Immunology", "Gynecology", "Genetics", "Forensic Medicine", "Family Medicine",
    "Epidemiology", "Endodontics", "Cytology", "Critical Care Medicine", "Cosmetic Dermatology"
};
        static void InsertPatient(int age, string name, string surname, SqlConnection connect)
        {
            // Оператор SQL 
            string sql = string.Format("Insert Into Patients" +
            "(Age, Name, Surname) Values(@Age, @Name, @Surname)");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                // Добавить параметры 
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.ExecuteNonQuery();
            }
        }
        static void InsertDoctor(int cabinet, string name, string surname, string special, SqlConnection connect)
        {
            // Оператор SQL 
            string sql = string.Format("Insert Into Doctors" +
            "(Cabinet, Name, Surname, Special) Values(@Cabinet, @Name, @Surname, @Special)");

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                // Добавить параметры 
                cmd.Parameters.AddWithValue("@Cabinet", cabinet);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@Special", special);
                cmd.ExecuteNonQuery();
            }
        }
        static async void ParallelGeneratePatientDataAndGetToDb()
        {
            // Параллельная обработка данных
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MyDB;Trusted_Connection=True;MultipleActiveResultSets=True; ";
            int Age;
            string Name, Surname;
            Random rand = new Random();
            var watch = Stopwatch.StartNew();
            SqlConnection connection = new SqlConnection(connectionString);
            Thread thread = Thread.CurrentThread;
            WriteLine($"\n - ParallelGeneratePatientData method thread ID : {thread.ManagedThreadId}");
            try
            {
                await connection.OpenAsync();
                Parallel.For(0, 1000, (i) =>
                {
                    Age = rand.Next(18, 51);
                    Name = Names[rand.Next(0, Names.Length)];
                    Surname = Surnames[rand.Next(0, Surnames.Length)];
                    InsertPatient(Age, Name, Surname, connection);
                });
            }
            finally
            {
                connection.Close();
            }
            watch.Stop();
            WriteLine($"\n - The execution time of ParallelGeneratePatientData method {watch.ElapsedMilliseconds} ms");
        }
        static async void ParallelGenarateDoctorDataAndGetToDb()
        {
            // Параллельная обработка данных
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MyDB;Trusted_Connection=True;MultipleActiveResultSets=True; ";
            int Cabinet;
            string Name, Surname, Special;
            Random rand = new Random();
            var watch = Stopwatch.StartNew();
            SqlConnection connection = new SqlConnection(connectionString);
            Thread thread = Thread.CurrentThread;
            WriteLine($"\n - ParallelGenerateDoctorData method thread ID : {thread.ManagedThreadId}");
            try
            {
                await connection.OpenAsync();
                Parallel.For(0, 1000, (i) =>
                {
                    Cabinet = rand.Next(18, 51);
                    Name = Names[rand.Next(0, Names.Length)];
                    Surname = Surnames[rand.Next(0, Surnames.Length)];
                    Special = Specials[rand.Next(0, Specials.Length)];
                    InsertDoctor(Cabinet, Name, Surname, Special, connection);
                });
            }
            finally
            {
                connection.Close();
            }
            watch.Stop();
            WriteLine($"\n - The execution time of ParallelGenerateDoctorData method {watch.ElapsedMilliseconds} ms");
        }
        static void NonParallelGeneratePatientDataAndGetToDb()
        {
            // Непараллельная обработка данных
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MyDB;Trusted_Connection=True;MultipleActiveResultSets=True; ";
            int Age;
            string Name, Surname;
            Random rand = new Random();
            var watch = Stopwatch.StartNew();
            SqlConnection connection = new SqlConnection(connectionString);
            Thread thread = Thread.CurrentThread;
            WriteLine($"\n - NonParallelGeneratePatientData method thread ID: {thread.ManagedThreadId}");
            try
            {
                connection.Open();
                for (int i = 0; i < 1000; i++)
                {
                    Age = rand.Next(18, 51);
                    Name = Names[rand.Next(0, Names.Length)];
                    Surname = Surnames[rand.Next(0, Surnames.Length)];
                    InsertPatient(Age, Name, Surname, connection);
                };
            }
            finally
            {
                connection.Close();
            }
            watch.Stop();
            WriteLine($"\n - The execution time of NonParallelGeneratePatientData method {watch.ElapsedMilliseconds} ms");
        }
        static void NonParallelGenarateDoctorDataAndGetToDb()
        {
            // Непараллельная обработка данных
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MyDB;Trusted_Connection=True;MultipleActiveResultSets=True; ";
            int Cabinet;
            string Name, Surname, Special;
            Random rand = new Random();
            var watch = Stopwatch.StartNew();
            SqlConnection connection = new SqlConnection(connectionString);
            Thread thread = Thread.CurrentThread;
            WriteLine($"\n - NonParallelGenerateDoctorData method thread ID: {thread.ManagedThreadId}");
            try
            {
                connection.Open();
                for (int i = 0; i<1000; i++)
                {
                    Cabinet = rand.Next(18, 51);
                    Name = Names[rand.Next(0, Names.Length)];
                    Surname = Surnames[rand.Next(0, Surnames.Length)];
                    Special = Specials[rand.Next(0, Specials.Length)];
                    InsertDoctor(Cabinet, Name, Surname, Special, connection);
                };
            }
            finally
            {
                connection.Close();
            }
            watch.Stop();
            WriteLine($"\n - The execution time of NonParallelGenerateDoctorData method {watch.ElapsedMilliseconds} ms");
        }
        public void ResetTables()
        {
            // Функция обнуления таблиц, для того чтобы не засорять БД
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MyDB;Trusted_Connection=True;MultipleActiveResultSets=True; ";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "TRUNCATE TABLE [dbo].[Doctors]";
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "TRUNCATE TABLE [dbo].[Patients]";
                command.ExecuteNonQuery();
            }
            WriteLine("\n - Таблицы обнулены");
            connection.Close();
        }
        static void Main()
        {
            int menu = 0;
            Program program = new Program();
            do
            {
                do
                {
                    WriteLine("\n 1 - Параллельная генерация данных и параллельное наполнение ими БД ");
                    WriteLine("\n 2 - Последовательная генерация данных и последовательное наполнение ими БД ");
                    WriteLine("\n 3 - Обнулить таблицы");
                    WriteLine("\n 4 - Выход ");
                    Write("\n Выбор : ");
                    if (int.TryParse(ReadLine(), out menu))

                        break;
                    else Clear();
                } while (true);
                switch (menu)
                {
                    case 1:
                        {
                            var watch = Stopwatch.StartNew();
                            try
                            {
                                Task task1 = Task.Run(() => ParallelGeneratePatientDataAndGetToDb());
                                Task task2 = Task.Run(() => ParallelGenarateDoctorDataAndGetToDb());
                                Task.WaitAll(task1, task2);
                            }
                            finally
                            {
                                watch.Stop();
                                WriteLine($"\n - The execution time of parallel methods {watch.ElapsedMilliseconds} ms");
                            }
                            ReadKey();
                            Clear();
                        }
                        break;
                    case 2:
                        {
                            var watch = Stopwatch.StartNew();
                            NonParallelGeneratePatientDataAndGetToDb();
                            NonParallelGenarateDoctorDataAndGetToDb();
                            watch.Stop();
                            WriteLine($"\n - The execution time of non parallel methods {watch.ElapsedMilliseconds} ms");
                            ReadKey();
                            Clear();
                        }
                        break;
                    case 3:
                        {
                            program.ResetTables();
                            ReadKey();
                            Clear();
                        }
                        break;
                }
            } while (menu!=4);
        }
    }
}

