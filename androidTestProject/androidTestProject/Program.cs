using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace androidTestProject
{
    class Program
    {
        const string GRADLEPATH = @"app\build.gradle";
        const string MANIFESTPATH = @"app\src\main\AndroidManifest.xml";
        const string LAYOUTPATH = @"app\src\main\res\layout\";
        static string JAVAPATH;
        static string packageName;
        static string modulePackageName;
        static string moduleName;
        
        static void Main(string[] args)
        {
            greeting();
            getPackageName();
            
            purge();

            populate();

            write("Sync gradle files and run a clean build to complete Setup");
            write("Press any key to exit");
            Console.ReadKey();
        }

        //deletes all layout/class files
        public static void purge()
        {
            string[] filePaths = Directory.GetFiles(LAYOUTPATH);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            string[] filePaths2 = Directory.GetFiles(JAVAPATH);
            foreach (string filePath in filePaths2)
                File.Delete(filePath);
        }
        
        public static void populate()
        {

            //replaces gradle/maifest file with standard AndroidAnnotations + CouchBase stuff
            File.WriteAllText(GRADLEPATH, replace(Files.GRADLE));
            File.WriteAllText(MANIFESTPATH, replace(Files.ANDROIDMANIFEST));

            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Activity.java"), replace(Files.MAINACTIVITY));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Fragment.java"), replace(Files.MAINFRAGMENT));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Contract.java"), replace(Files.MAINCONTRACT));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Presenter.java"), replace(Files.MAINPRESENTER));
            File.WriteAllText(Path.Combine(JAVAPATH, "ActivityUtils.java"), replace(Files.ACTIVITYUTILS));

            File.WriteAllText(Path.Combine(LAYOUTPATH, "activity_" + moduleName.ToLower() + ".xml"), replace(Files.ACTIVITYLAYOUT));
            File.WriteAllText(Path.Combine(LAYOUTPATH, "fragment_" + moduleName.ToLower() + ".xml"), replace(Files.FRAGMENTLAYOUT));
        }
        

        public static void greeting()
        {
            write("Assumptions:");
            write("this exe is located inside the android project folder");
            write("this project's android manifest and build.gradle have not been modified, all changes will be overridden");
            write("Please enter you module name. The first letter should be capital");
            write("for example, entering 'Test' will yield 'TestActivity, TestFragment, activity_test, etc");
            write("");
            moduleName = Console.ReadLine();
        }

        public static void write(string text)
        {
            Console.WriteLine(text);
        }

        public static void getPackageName()
        {
            StreamReader file = new System.IO.StreamReader(GRADLEPATH);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("applicationId"))
                {
                    foreach (Match match in Regex.Matches(line, "\".*?\""))
                    {
                        string intermediate = match.ToString();
                        packageName  = intermediate.Substring(1, intermediate.Length - 2);
                        JAVAPATH = System.IO.Path.Combine(@"app\src\main\java\", packageName.Replace(".", @"\"));

                        modulePackageName = packageName + "." + moduleName.ToLower();
                        Directory.CreateDirectory(Path.Combine(JAVAPATH, moduleName.ToLower()));

                        file.Close();
                        return;
                    }
                }
            }


            return;
        }

        public static string replace(string input)
        {
            StringBuilder builder = new StringBuilder(input);
            builder.Replace("|", packageName);
            builder.Replace("^", modulePackageName);
            builder.Replace("~", moduleName);
            builder.Replace("%", moduleName.ToLower());
            return builder.ToString();
        }
    }
}
