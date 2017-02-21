using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace ModuleAdder
{
    class Program
    {
        static string JAVAPATH;
        static string packageName;
        static string moduleName;
        static string modulePackageName;

        const string MANIFESTPATH = @"app\src\main\AndroidManifest.xml";
        const string LAYOUTPATH = @"app\src\main\res\layout\";
        const string GRADLEPATH = @"app\build.gradle";

        static void Main(string[] args)
        {
            greeting();

            getPackageName();

            addModule();


        }
        public static void greeting()
        {
            write("Assumptions:");
            write("this exe is located inside the android project folder");
            write("this project is not different in structure from a default android studio project circa Jan 2017");

            write("Please enter you new module name. The first letter should be capital");
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
                        packageName = intermediate.Substring(1, intermediate.Length - 2);
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

        public static void addModule()
        {
            populate();
            addActivityToManifest();
        }

        public static void addActivityToManifest()
        {
            string endTag = @"</application>";
            var lineToAdd = @"<activity android:name="".%.~Activity_"" />";

            var txtLines = File.ReadAllLines(MANIFESTPATH).ToList();
            int index = txtLines.IndexOf(endTag);
            if (index == -1)
            {
                write("application tag not found in project file");
            }
            else
            {
                txtLines.Insert(index, replace(lineToAdd));
                File.WriteAllLines(MANIFESTPATH, txtLines);
            }

            //try
            //{
            //    string endTag = @"           </application>";
            //    var lineToAdd = @"<activity android:name="".%.~Activity_"" />";

            //    var txtLines = File.ReadAllLines(MANIFESTPATH).ToList();
            //    txtLines.Insert(txtLines.IndexOf(endTag), replace(lineToAdd));
            //    File.WriteAllLines(MANIFESTPATH, txtLines);
            //}
            //catch
            //{
            //    write("--- An error occured while writing to the Android Manifest");
            //}
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

        public static void populate()
        {
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Activity.java"), replace(Files.MAINACTIVITY));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Fragment.java"), replace(Files.MAINFRAGMENT));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Contract.java"), replace(Files.MAINCONTRACT));
            File.WriteAllText(Path.Combine(JAVAPATH, moduleName.ToLower(), moduleName + "Presenter.java"), replace(Files.MAINPRESENTER));
            File.WriteAllText(Path.Combine(JAVAPATH, "ActivityUtils.java"), replace(Files.ACTIVITYUTILS));

            File.WriteAllText(Path.Combine(LAYOUTPATH, "activity_" + moduleName.ToLower() + ".xml"), replace(Files.ACTIVITYLAYOUT));
            File.WriteAllText(Path.Combine(LAYOUTPATH, "fragment_" + moduleName.ToLower() + ".xml"), replace(Files.FRAGMENTLAYOUT));
        }
    }
}
