using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZip
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter Destination Path:");
            String destinationPath = Console.ReadLine();

            int count = 0;
            //String tempFilePath = @"C:\Users\Jase\Desktop\assignment02\TempExtract";
            String tempFilePath = destinationPath + @"\TempExtract";

            Console.WriteLine("Enter the path of the folder containing each students' .zip file:");
            String submissionFolderPath = Console.ReadLine();

            Console.WriteLine("Enter the .java file name to be extracted(inlcude .java)");
            String submissionFileName = Console.ReadLine();

            //string[] filePaths = Directory.GetFiles(@"C:\Users\Jase\Desktop\submissions", "*.zip");
            string[] filePaths = Directory.GetFiles(submissionFolderPath, "*.zip");
            SortedSet<string> failedPaths = new SortedSet<string>();

            foreach (string file in filePaths)
            {
                try
                {
                    ZipFile.ExtractToDirectory(file, tempFilePath);
                }
                catch (Exception)
                {
                    failedPaths.Add(file);
                }

                //string[] extractedFiles = Directory.GetFiles(tempFilePath, "NetworkGraph.java", SearchOption.AllDirectories);
                string[] extractedFiles = Directory.GetFiles(tempFilePath, submissionFileName, SearchOption.AllDirectories);

                if (extractedFiles.Length > 0)
                {
                    //Directory.CreateDirectory(@"C:\Users\Jase\Desktop\folder\Submission" + count.ToString("000"));
                    Directory.CreateDirectory(destinationPath + @"\submission" + count.ToString("000"));

                    //String extractedFileName = file.Substring(file.LastIndexOf(@"\"));
                    //extractedFileName = extractedFileName.Remove(extractedFileName.LastIndexOf(@".zip"));

                    //File.Move(extractedFiles[0], @"C:\Users\Jase\Desktop\folder\Submission" + count.ToString("000") + "\\NetworkGraph" + count++.ToString("000") + ".Java");
                    File.Move(extractedFiles[0], destinationPath + @"\submission" + count++.ToString("000") + @"\" + submissionFileName);
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(tempFilePath);

                foreach (FileInfo s in di.GetFiles())
                {
                    s.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            if (failedPaths.Count != 0)
                foreach (string s in failedPaths)
                    Console.WriteLine(s);
            else
                Console.WriteLine("No failed entries");
            Console.Read();
        }
    }
}