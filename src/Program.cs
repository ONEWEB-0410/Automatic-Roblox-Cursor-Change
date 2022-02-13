using System;
using System.IO;
using System.Threading;
using System.Linq;

namespace ARCC
{
    class Program
    {
        public static string GetNewestDirectory(string path)
        {
            var directory = new DirectoryInfo(path).GetDirectories().OrderByDescending(o => o.CreationTime).FirstOrDefault();
            return directory.Name;
        }

        static string GetRobloxVersion()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string roblox_version_path = userProfile + @"\AppData\Local\Roblox\Versions\";

            string roblox_newest_version = GetNewestDirectory(roblox_version_path);

            string roblox_newest_version_path = roblox_version_path + roblox_newest_version;

            Thread.Sleep(2000);

            return roblox_newest_version_path;
        }


        public static void Return()
        {

            Console.WriteLine("Do you wish to return back to the menu y/n ?");
            string x = Console.ReadLine();
            switch (x)
            {
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


        public static void Delete(string[] files)
        {

            foreach (string n in files)
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
                            case "y":

                                string content_path = GetRobloxVersion();

                                if (Directory.Exists(content_path))
                                {
                                    Console.WriteLine("Loading...");
                                    string full_path = content_path + @"\content\textures\Cursors\KeyboardMouse";
                                    Console.WriteLine(full_path);
                                    if (Directory.Exists(full_path))
                                    {

                                        string[] old_files = Directory.GetFiles(full_path, "*.png");


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
                                                catch (IOException copyError)
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




