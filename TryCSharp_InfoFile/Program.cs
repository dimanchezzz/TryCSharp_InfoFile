using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Shell32;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.Globalization;
using System.Reflection;

namespace TryCSharp_InfoFile
{
    class Program
    {
        static string path = "..\\..\\FirstTest.docx";


       // private static SHA256 Sha256 = SHA256.Create();
        static void Main(string[] args)
        {
            //getHashAfterAttr();//атрибуты файла изменяются 
            // GetInfoShell();
            // BytesToString(GetHashSha256(path));
            getInfoNuGet();
            //BytesToString(GetHashSha256(path));
            Console.ReadKey();

        }
        private static SHA256 Sha256 = SHA256.Create();
        private static byte[] GetHashSha256(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                return Sha256.ComputeHash(stream);
            }
        }
        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }
        public static void GetInfoShell()
        {
            List<string> arrHeaders = new List<string>();
            Shell shell = new Shell();
            Folder objFolder;
            objFolder = shell.NameSpace(@"C:\Other\AppSysTech");
            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = objFolder.GetDetailsOf(null, i);
                if (String.IsNullOrEmpty(header))
                    break;
                arrHeaders.Add(header);
            }
            foreach (Shell32.FolderItem2 item in objFolder.Items())
            {
                for (int i = 0; i < arrHeaders.Count; i++)
                {
                    Console.WriteLine(
                      $"{i}\t{arrHeaders[i]}: {objFolder.GetDetailsOf(item, i)}");
                }
            }
        }
        public static void getInfoNuGet()
        {
            string filePath = @"C:\\var\\log\\ecm\\hello.docx";
            var file = ShellFile.FromFilePath(filePath);
            FileInfo fi = new FileInfo(filePath);
            long size = fi.Length;
            Console.WriteLine("File Size in Bytes: {0}", size);
            Type typeShell = typeof(ShellProperties.PropertySystem);
            var fileObject = file.Properties.System;
            PropertyInfo[] isAllProp = fileObject.GetType().GetProperties();
            var before = BytesToString(GetHashSha256(filePath));
            Console.WriteLine("Hash before:" + before);           
            file.Properties.System.Comment.Value = "Hello KittysssAAA";
            Console.WriteLine(file.Properties.System.Comment.Value);
            var after = BytesToString(GetHashSha256(filePath));
            Console.WriteLine("Hash after: " + after);
            fileObject.GetType().GetProperty("Comment").SetValue(fileObject, "11111",null);
            Console.WriteLine();


            //PropertyInfo[] isAllProp = fileObject.GetType().GetProperties();
            //foreach (var item in isAllProp)
            //{
            //    var propDescription = item.GetValue(fileObject);
            //    var valueProp = propDescription.GetType().GetProperty("Value");
            //    var value = valueProp?.GetValue(propDescription);
            //    Console.WriteLine(item.Name + " : " + value);
            //}
            //byte[] bb = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            // new string[]{"212123"};
            //Console.WriteLine("total freespase" + file.Properties.System.Capacity.Value);
            //Console.WriteLine("amount freespase" + file.Properties.System.FreeSpace.Value);   


            //file.Properties.System.ItemName.Value = "this";
            //Console.WriteLine(file.Properties.System.PriorityText.Value);
            //Console.WriteLine("#$%^&*@#$%^&");

            //file.Properties.System.Comment.Value = "Hello kitty";

            //Console.WriteLine(file.Properties.System.PriorityText.Value);


            // file.Properties.System.Message.BccAddress.Value = new string[] { "212123" };

            FileInfo fi_new = new FileInfo(filePath);
            Console.WriteLine("file.size.value = " + file.Properties.System.Size.Value);
            long size_new = fi_new.Length;
            Console.WriteLine("File Size in Bytes: {0}", size_new);
            Console.WriteLine("После изменения:  " + after);

            if (before == after)
            {
                Console.WriteLine(" хеш Не поменялся");
            }
            else
            {
                Console.WriteLine(" хеш Поменялся");
            }
            // Console.WriteLine(file.Properties.System.Video.Director.Value);

            //Console.WriteLine(file.Properties.System.ContentType.Value);

            //Console.WriteLine("after " + BytesToString(GetHashSha256(path)));
            //Console.WriteLine(oldTitle);
            //file.Properties.System.ApplicationName.Value = "3333";
            //Console.WriteLine("after " + BytesToString(GetHashSha256(path)));
            //Console.WriteLine("Before " + BytesToString(GetHashSha256(filePath)));
            //file.Properties.System.Title.Value = "Hellos2";
            //Console.WriteLine("after " + BytesToString(GetHashSha256(filePath)));

        }

        public static void getHashAfterAttr()
        {
            FileInfo file = new FileInfo(path);
            FileAttributes attributes = File.GetAttributes(path);
            Console.WriteLine(attributes.ToString());
            Console.WriteLine("Start " + BytesToString(GetHashSha256(path)));
            File.SetAttributes(path, System.IO.FileAttributes.Archive);
            //To compressed file.
            Console.WriteLine("Archive " + BytesToString(GetHashSha256(path)));
            File.SetAttributes(path, System.IO.FileAttributes.Compressed);
            //To reserved for future use file.
            Console.WriteLine("Compressed " + BytesToString(GetHashSha256(path)));
            File.SetAttributes(path, System.IO.FileAttributes.Device);
            Console.WriteLine("Device " + BytesToString(GetHashSha256(path)));
            //To set the file is a directory.
            File.SetAttributes(path, System.IO.FileAttributes.Directory);
            Console.WriteLine("Directory " + BytesToString(GetHashSha256(path)));
            //To make data  encrypted.
            File.SetAttributes(path, System.IO.FileAttributes.Encrypted);
            Console.WriteLine("Encrypted " + BytesToString(GetHashSha256(path)));
            //To hide file
            File.SetAttributes(path, System.IO.FileAttributes.Hidden);
            Console.WriteLine("Hidden " + BytesToString(GetHashSha256(path)));
            //The file or directory includes data integrity support.
            //When this value is applied to a file, all data streams in the file 
            //have integrity support.When this value is applied to a directory, 
            //all new files and subdirectories within that directory, by default, include integrity support.
            File.SetAttributes(path, System.IO.FileAttributes.IntegrityStream);
            Console.WriteLine("IntegrityStream " + BytesToString(GetHashSha256(path)));
            //To set file has no  special attributes
            File.SetAttributes(path, System.IO.FileAttributes.Normal);
            Console.WriteLine("Normal " + BytesToString(GetHashSha256(path)));
            //The file or directory is excluded from the data integrity scan.
            //When this value is applied to a directory, by default, 
            //all new files and subdirectories within that directory are excluded from data integrity.
            File.SetAttributes(path, System.IO.FileAttributes.NoScrubData);
            Console.WriteLine("NoScrubData " + BytesToString(GetHashSha256(path)));
            //The file will not be indexed by the operating system's content indexing service.
            File.SetAttributes(path, System.IO.FileAttributes.NotContentIndexed);
            Console.WriteLine("NotContentIndexed " + BytesToString(GetHashSha256(path)));
            // To set file offline.The data of the file is not immediately available.
            File.SetAttributes(path, System.IO.FileAttributes.Offline);
            Console.WriteLine("Offline " + BytesToString(GetHashSha256(path)));
            //To set read only attributes
            File.SetAttributes(path, System.IO.FileAttributes.ReadOnly);
            Console.WriteLine("ReadOnly " + BytesToString(GetHashSha256(path)));
            //To set reparse point attributes
            //The file contains a reparse point, 
            //which is a block of user - defined data associated with a file or a directory.
            File.SetAttributes(path, System.IO.FileAttributes.ReparsePoint);
            Console.WriteLine("ReparsePoint " + BytesToString(GetHashSha256(path)));
            //To set Sparse File attributes
            //The file contains a reparse point, 
            //which is a block of user - defined data associated with a file or a directory.
            File.SetAttributes(path, System.IO.FileAttributes.SparseFile);
            Console.WriteLine("SparseFile " + BytesToString(GetHashSha256(path)));
            //To set file part of operating system or is used by the operating system.
            File.SetAttributes(path, System.IO.FileAttributes.System);
            Console.WriteLine("System " + BytesToString(GetHashSha256(path)));
            //To file make temporary
            File.SetAttributes(path, System.IO.FileAttributes.Temporary);
            Console.WriteLine("Temporary " + BytesToString(GetHashSha256(path)));
            File.SetAttributes(path, System.IO.FileAttributes.Archive);
            Console.WriteLine("Archive " + BytesToString(GetHashSha256(path)));
        }
    }
}
