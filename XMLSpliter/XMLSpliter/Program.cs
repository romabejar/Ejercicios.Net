using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSpliter
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("This program needs 2 arguments:");
            Console.WriteLine("\t" + "first argument: path to the xml file, example:");
            Console.WriteLine("\t\t" + "C:\\Users\\Roma\\Desktop\\");
            Console.WriteLine("\t" + "second argument: filename of an xml");
            Console.WriteLine("\t\t" + "test.xml");
            Console.WriteLine();
            Console.WriteLine("This program has the following preconditions to work:");
            Console.WriteLine("- Both arguments must be present");
            Console.WriteLine("- The path in the first argument must be formated like the example");
            Console.WriteLine("- The path in the first argument must exist");
            Console.WriteLine("- The file of the second argumetn must exist in first argument path");

            if (args.Length != 2) return;

            string path = args[0];          // "C:\\Users\\Roma\\Desktop\\";
            string filename = args[1];      // "test.xml";
            if( XMLUtils.Instance.enoughSpaceInDisk(path, filename))
            {
                //XMLUtils.Instance.splitXML(path, filename);
                XMLUtils.Instance.splitXMLWithInterruptionDealing(path, filename);
            }
            
        }
    }
}
