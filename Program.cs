using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClarionFactorTableRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            //read our request - only needed for POST scenario
            //String text = System.IO.File.ReadAllText(args[0]);
            //ASCIIEncoding encoding = new ASCIIEncoding ();
            //byte[] bytes = encoding.GetBytes (text);

            //create a web request for a specific factor table
            WebRequest req = WebRequest.Create(@"https://hamilton.insuredapp.com/api/v1/rest/service/factor/1473.json");
            req.Method = "GET";  //code commented out was used for POST scenario
            req.ContentType = "raw";
            // req.ContentLength = bytes.Length;
            req.ContentLength = 0;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("Access ID:key goes here"));
            //Stream newStream = req.GetRequestStream();
            //newStream.Write(bytes, 0, bytes.Length);
            //newStream.Close();

            //read the response
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
			Stream receiveStream = resp.GetResponseStream();

            //print it out
			Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
			StreamReader readStream = new StreamReader( receiveStream, encode );
            Console.WriteLine("\r\nResponse stream received.");
			Char[] read = new Char[256];
			int count = readStream.Read( read, 0, 256 );
			while (count > 0) 
				{
					String str = new String(read, 0, count);
					Console.Write(str);
					count = readStream.Read(read, 0, 256);
				}
			Console.WriteLine("");

            //cleanup
			resp.Close();
			readStream.Close();        }
    }
}
