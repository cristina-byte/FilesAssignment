
using System.IO.Compression;
using System.Security.Cryptography;

namespace FilesAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calendar calendar = new Calendar();
            
            //add Meeting objects to calendar 
            calendar.Meetings = new List<Meeting>
            {
                new Meeting("Web site restructuring","online",new DateTime(2022,11,23,12,0,0),1,new TimeSpan(2,0,0)),
                new Meeting("Career Expo Event","online",new DateTime(2022,12,2,10,30,0),5,new TimeSpan(1,30,0)),
                new Meeting("Internship Lecture","online",new DateTime(2022,12,2,15,0,0),2,new TimeSpan(2,30,0)),
                new Meeting("Meeting with client","online",new DateTime(2022,12,2,19,45,0),6,new TimeSpan(0,30,0)),
                new Meeting("C# Training","online",new DateTime(2022,12,9,10,15,0),7,new TimeSpan(3,30,0)),
                new Meeting("Java Training","online",new DateTime(2022,12,17,10,0,0),1,new TimeSpan(1,0,0)),
                new Meeting("ASP.NET Lecture","online",new DateTime(2023,1,7,11,0,0),2,new TimeSpan(45,0,0)),
                new Meeting("Solid Principles","str. Mihai Eminescu 1",new DateTime(2023,1,10,13,0,0),11,new TimeSpan(1,0,0)),
                new Meeting("Project Presentation","str. Cluj",new DateTime(2023,1,10,15,0,0),7,new TimeSpan(0,30,0)),
                new Meeting("Agile","online",new DateTime(2022,12,20,10,0,0),67,new TimeSpan(1,30,0))
            };

            //Encrypt data and write to meetings.dat file
            calendar.WriteToFile("meetings.dat");

            //read from meetings.dat file , decrypt data and print them
            calendar.ReadFromFile("meetings.dat");

            //compress the file users.txt
            calendar.CompressFile("users.txt", "compressed.gz");
        }
    }
}