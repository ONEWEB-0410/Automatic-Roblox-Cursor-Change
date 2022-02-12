using System;
using System.IO;
using System.Threading;

namespace AutomaticCursorChange
{
    class Program
    {


        public static void Return()
        {

            Console.WriteLine("Do you wish to return back to the menu y/n ?");
            string x = Console.ReadLine();
            switch(x){
                case "y":
                    Console.WriteLine("Select one of the available options");
                    string answer = Console.ReadLine();
                    startProcess(answer);
                    break;
                case "n":
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(2000);
                    break;
                default:
                    Console.WriteLine("Invalid argument!");
                    Thread.Sleep(2000);
                    break;
            }

        }


        public static void Delete(string [] files)
        {

            foreach(string n in files)
            {
                File.Delete(n);
            }
        }


        public static void startProcess(string answer)
        {

            if (true)
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var version_folder = userProfile + @"\AppData\Local\Roblox\Versions";
                var filePath1 = Environment.ExpandEnvironmentVariables(version_folder);
                
                

                switch (answer)
                {
                    case "Old":
                        Console.WriteLine("Your choise is set up!");
                        Return();
                        break;
                    case "Custom":
                        Console.WriteLine("Do you wish to proceed with creating your own preset y/n ? ");
                        string custom_answer = @Console.ReadLine();
                        switch (custom_answer)
                        {
                            case "n":
                                Thread.Sleep(2000);
                                break;
                            default:


                                if (Directory.Exists(filePath1))
                                {
                                    string[] files = Directory.GetDirectories(filePath1);
                                    foreach (string f in files)
                                    {
                                        string[] files2 = Directory.GetDirectories(f);
                                        foreach(string f2 in files2)
                                        {

                                            FileInfo fi = new FileInfo(f2);
                                            DateTime creationTime = fi.CreationTime;
                                            Console.WriteLine(">" + f2 + "\n " + " > Creation time: {0}", creationTime);

                                        }
                                    }
                                    Console.WriteLine(@"Please copy the path that is similar to  .../roblox/version/.../content  and is the most recent one (you can easily determine which one is the most recent one as the creation date is provided) ");

                                    string content_path = Console.ReadLine();


                                    if (Directory.Exists(content_path))
                                    {
                                        Console.WriteLine("Loading...");
                                        string full_path = content_path + @"\textures\Cursors\KeyboardMouse";
                                        if (Directory.Exists(full_path))
                                        {

                                            string [] old_files = Directory.GetFiles(full_path, "*.png");


                                            Console.WriteLine("Please provide the path of the image(s) you wish to make your own preset");
                                            string custom_dir = Console.ReadLine(); // The dir with the custom images

                                            if (Directory.Exists(custom_dir)) // If the custom dir exists
                                            {
                                                string[] custom_files = Directory.GetFiles(custom_dir, "*.png"); // Get the files from the custom dir that are .png type

                                                string backup = full_path + @"/roblox_default_files"; // The backup dir

                                                if (!Directory.Exists(backup)) // If the backup dir does not exist
                                                {
                                                    Directory.CreateDirectory(backup); // Create the backup dir

                                                    try
                                                    {

                                                        foreach (string h in old_files) // For each file in the old files ->
                                                        {

                                                            string hName = h.Substring(full_path.Length + 1);
                                                            File.Copy(Path.Combine(full_path, hName), Path.Combine(backup, hName));
                                                        }
                                                        Delete(old_files); // Delete the old files
                                                        Console.WriteLine("> [...] - Backup has succesfully been created!");
                                                    }
                                                    catch(IOException copyError)
                                                    {
                                                        Console.WriteLine(copyError.Message);
                                                        Console.WriteLine("> [WARNING] - Backup has NOT been created! The old files have not been deleted!");
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    Console.WriteLine("> [...] - Backup succesfully loaded! ");
                                                    Delete(old_files);
                                                }

                                                foreach (string s in custom_files)
                                                {

                                                    // Remove path from the file name.
                                                    string sName = s.Substring(custom_dir.Length + 1);

                                                    try
                                                    {
                                                        // Will not overwrite if the destination file already exists.
                                                        File.Copy(Path.Combine(custom_dir, sName), Path.Combine(full_path, sName));
                                                        Console.WriteLine("> Operation Completed.");
                                                        Thread.Sleep(2000);
                                                    }

                                                    // Catch exception if the file was already copied.
                                                    catch (IOException copyError)
                                                    {
                                                        Console.WriteLine(copyError.Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("> [ERROR] - Invalid path");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("> [ERROR] - Something went wrong");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("> [ERROR] - The given path is not valid");
                                    }
                                }


                                break;
   
                        }
                        Thread.Sleep(2000);
                        break;
                    case "Classic":
                        Console.WriteLine("Your choise is set up!");
                        Return();
                        break;
                    case "New":
                        Console.WriteLine("Coming Soon");
                        Return();
                        break;
                }
            } 
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Avaiable Options:\n • 'New': Roblox's new cursors\n • 'Old': Roblox's old cursors\n • 'Classic': Roblox's classic cursors\n • 'Custom': Your own preset\n Supported Files: .png ");
            string answer = Console.ReadLine();
            startProcess(answer);
        }

    }
}
