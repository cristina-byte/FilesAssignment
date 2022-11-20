using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace FilesAssignment
{
    internal class Calendar
    {
        private Rijndael myRijndael = Rijndael.Create();
        private List<Meeting> _meetings;
        public List<Meeting> Meetings 
        { 
            get { return _meetings.DefaultIfEmpty().ToList<Meeting>();} 
            set { _meetings = value; }
        }
       
        public Calendar()
        {
            _meetings = new List<Meeting>();
        }

        public void WriteToFile(string path)
        {
            byte[] encrypted = this.EncryptStringToBytes(string.Join('\n', Meetings), myRijndael.Key, myRijndael.IV);
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            Console.WriteLine("Length:" + encrypted.Length);
            fs.Write(encrypted, 0, encrypted.Length);
            fs.Close();

            FileInfo f=new FileInfo(path);
            Console.WriteLine("\nFile length:"+f.Length);
        }  

        public void ReadFromFile(string path)
        {
            //get length of the fille
            FileInfo fileInfo = new FileInfo(path);
            byte[] bytes = new byte[fileInfo.Length];
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            int readBytes = fs.Read(bytes,0,Int32.Parse(fileInfo.Length.ToString()));
            fs.Close();

            //decrypt read bytes
            string data=this.DecryptStringFromBytes(bytes, myRijndael.Key, myRijndael.IV);
            Console.WriteLine("\ntextul din fisier decriptat este:" + data);
        }
    }
}
