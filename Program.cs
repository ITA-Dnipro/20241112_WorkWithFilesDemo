using System.Runtime.CompilerServices;

namespace _20241112_WorkWithFilesDemo
{
    internal class Program
    {
        const string DEFAULT_FILE_NAME = "my_data.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            FileInfo myFile = new FileInfo(DEFAULT_FILE_NAME);

            if (myFile.Exists) 
            {
                myFile.Delete();
                Console.WriteLine(":)");
            }
            else 
            {
                Console.WriteLine(":(");
            }

            Console.WriteLine("Current folder: {0}", Directory.GetCurrentDirectory());

            string[] results = Directory.GetFiles("C:\\Users\\opiur\\source\\repos", "*.cs", SearchOption.AllDirectories);

            int i = 0;
            foreach (string result in results) 
            {
                Console.WriteLine(result);

                if (++i == 10)
                {
                    break;
                }
            }

            //File.Copy

            #region Read file

            string fileName = "..\\..\\..\\Program.cs";
            
            string program = File.ReadAllText(fileName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(program);

            //Console.WriteLine("Press any key...");
            //Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Magenta;

            //FileStream fs = null;  // = new FileStream(fileName, FileMode.Open);

            //try
            //{
            //    fs = new FileStream(fileName, FileMode.Open);
            //}
            //finally
            //{
            //    //fs.Dispose();
            //    fs.Close();
            //}

            //using (FileStream fs = new FileStream(fileName, FileMode.Open))
            //{
            //    int data;
            //    do
            //    {
            //        data = fs.ReadByte();

            //        if (data != -1)
            //        {
            //            char ch = (char)fs.ReadByte();
            //            Console.Write(ch);
            //        }
            //    } while (data != -1);
            //}    // fs.Dispose();

            //using (Class1 c = new Class1())
            //{

            //}

            FileInfo newFile = new FileInfo(fileName);
            string newFileName = newFile.Name + "Copy";

            FileInfo newFileCopy = new FileInfo(newFile.Directory + "\\" + newFileName);

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                StreamReader rdr = new StreamReader(fs);

                using (FileStream fsOut = new FileStream(newFileCopy.FullName, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter wrtr = new StreamWriter(fsOut);

                    string line = string.Empty;
                    do
                    {
                        line = rdr.ReadLine();
                        Console.WriteLine(line);

                        if (line != null)
                        {
                            string newLine = line.Replace(' ', '\t');

                            wrtr.WriteLine(newLine);
                        }                        
                    } while (line != null);

                    fsOut.Flush();
                }     // fsOut.Dispose();          
            }    // fs.Dispose();

            #endregion

            string filePerson = "person.txt";

            Person p = new Person() { Id = 2, Name = "Dmytro", Age = 18.5 };

            using (FileStream fsOut = new FileStream(filePerson, FileMode.Create, FileAccess.Write))
            {
                StreamWriter wrtr = new StreamWriter(fsOut);

                //wrtr.Write(p.Id);
                //wrtr.Write(p.Name);
                //wrtr.Write(p.Age);
                //wrtr.WriteLine();

                wrtr.WriteLine("{0} {1} {2}", p.Id, p.Name, p.Age);

                wrtr.Flush();
                //fsOut.Flush();
            }

            Person pCopy = null;

            using (FileStream fs = new FileStream(filePerson, FileMode.Open, FileAccess.Read))
            {
                StreamReader rdr = new StreamReader(fs);

                string line = rdr.ReadLine();

                string[] fields = line.Split();

                pCopy = new Person()
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1],
                    Age = double.Parse(fields[2])
                };
            }

            Console.WriteLine("pCopy: {0} - {1} ({2})", pCopy.Id, pCopy.Name, pCopy.Age);

            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }
    }
}