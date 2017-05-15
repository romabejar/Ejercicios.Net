using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLSpliter
{
    class XMLUtils
    {

        private static XMLUtils instance;

        private XMLUtils() { }

        public static XMLUtils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XMLUtils();
                }
                return instance;
            }
        }

        public void splitXML(string path, string filename)
        {
            string filepath = path + filename;
            XmlReader reader = XmlReader.Create(@filepath);
            StringBuilder XmlDeclaration = new StringBuilder();
            StreamWriter writer = null;
            int fileNumber = 1;
            string outputfilename = "Archivo";
            string outputfilepath;
            int numOfTabs = 1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Whitespace:
                        if (writer != null)
                        {
                            writer.WriteLine("");
                        }
                        break;

                    case XmlNodeType.XmlDeclaration:
                        XmlDeclaration.Append("<?" + reader.Name);
                        if (reader.HasAttributes)
                        {
                            reader.MoveToFirstAttribute();
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                XmlDeclaration.Append(" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"");
                                reader.MoveToNextAttribute();
                            }
                            XmlDeclaration.Append("?>");
                        }
                        break;

                    case XmlNodeType.Element:
                        if (reader.Name == "Product")
                        {
                            outputfilepath = path + outputfilename + fileNumber.ToString() + ".xml";
                            ++fileNumber;
                            writer = new StreamWriter(@outputfilepath);
                            writer.WriteLine(XmlDeclaration);
                            writer.WriteLine("<line>");
                        }
                        if (writer != null)
                        {
                            bool isEmptyElement = reader.IsEmptyElement;
                            for (int j = 0; j < numOfTabs; ++j) writer.Write("\t");
                            if (!isEmptyElement) ++numOfTabs;
                            writer.Write("<" + reader.Name);
                            if (reader.HasAttributes)
                            {
                                reader.MoveToFirstAttribute();
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    writer.Write(" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"");
                                    reader.MoveToNextAttribute();
                                }

                            }
                            if (isEmptyElement) writer.Write("/>");
                            else writer.WriteLine(">");
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (writer != null)
                        {
                            --numOfTabs;
                            for (int j = 0; j < numOfTabs; ++j) writer.Write("\t");
                            writer.WriteLine("</" + reader.Name + ">");
                        }
                        if (reader.Name == "Product")
                        {
                            writer.WriteLine("</line>");
                            writer.Flush();
                            writer.Close();
                            writer = null;
                        }
                        break;

                    case XmlNodeType.Text:
                        if (writer != null)
                        {
                            writer.WriteLine(reader.Value);
                        }

                        break;

                }

            }
        }

        public void splitXMLWithInterruptionDealing(string path, string filename)
        {
            string filepath = path + filename;
            XmlReader reader = XmlReader.Create(@filepath);
            StringBuilder XmlDeclaration = new StringBuilder();
            StreamWriter writer = null;
            int fileNumber = 1;
            string outputfilename = "Archivo";
            string outputfilepath;
            int numOfTabs = 1;

            int auxNumOfReads = 0;

            if (Properties.Settings.Default.InterruptionHasOcurred)
            {
                // advance reader to the beggining of the interrupted file
                for (int i = 0; i < Properties.Settings.Default.NumOfReads; ++i) reader.Read();
                // set the fileNumber to the interrupted one
                fileNumber = Properties.Settings.Default.FilesCompleted;
                // set num of reads to last failed one
                auxNumOfReads = Properties.Settings.Default.NumOfReads;
                XmlDeclaration.Append(Properties.Settings.Default.XmlDeclaration);
            }
            //int prova = 0;
            Properties.Settings.Default.InterruptionHasOcurred = true;
            Properties.Settings.Default.Save();

            while (reader.Read())
            {
                ++auxNumOfReads;
                switch (reader.NodeType)
                {
                    case XmlNodeType.Whitespace:
                        if (writer != null)
                        {
                            writer.WriteLine("");
                        }
                        break;

                    case XmlNodeType.XmlDeclaration:
                        XmlDeclaration.Append("<?" + reader.Name);
                        if (reader.HasAttributes)
                        {
                            reader.MoveToFirstAttribute();
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                XmlDeclaration.Append(" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"");
                                reader.MoveToNextAttribute();
                            }
                            XmlDeclaration.Append("?>");
                            Properties.Settings.Default.XmlDeclaration = XmlDeclaration.ToString();
                            Properties.Settings.Default.Save();
                        }
                        break;

                    case XmlNodeType.Element:
                        if (reader.Name == "Product")
                        {
                            outputfilepath = path + outputfilename + fileNumber.ToString() + ".xml";
                            ++fileNumber;
                            writer = new StreamWriter(@outputfilepath);
                            writer.WriteLine(XmlDeclaration);
                            writer.WriteLine("<line>");
                           // prova++;
                            //if (prova >= 2) return;
                        }
                        if (writer != null)
                        {
                            bool isEmptyElement = reader.IsEmptyElement;
                            for (int j = 0; j < numOfTabs; ++j) writer.Write("\t");
                            if (!isEmptyElement) ++numOfTabs;
                            writer.Write("<" + reader.Name);
                            if (reader.HasAttributes)
                            {
                                reader.MoveToFirstAttribute();
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    writer.Write(" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"");
                                    reader.MoveToNextAttribute();
                                }

                            }
                            if (isEmptyElement) writer.Write("/>");
                            else writer.WriteLine(">");
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (writer != null)
                        {
                            --numOfTabs;
                            for (int j = 0; j < numOfTabs; ++j) writer.Write("\t");
                            writer.WriteLine("</" + reader.Name + ">");
                        }
                        if (reader.Name == "Product")
                        {
                            writer.WriteLine("</line>");
                            writer.Flush();
                            writer.Close();
                            writer = null;

                            Properties.Settings.Default.NumOfReads = auxNumOfReads;
                            Properties.Settings.Default.FilesCompleted = fileNumber;
                            Properties.Settings.Default.Save();

                        }
                        break;

                    case XmlNodeType.Text:
                        if (writer != null)
                        {
                            writer.WriteLine(reader.Value);
                        }

                        break;

                }

            }
            Properties.Settings.Default.InterruptionHasOcurred = false;
            Properties.Settings.Default.NumOfReads = 0;
            Properties.Settings.Default.FilesCompleted = 0;
            Properties.Settings.Default.XmlDeclaration = "";
            Properties.Settings.Default.Save();
        }

        public bool enoughSpaceInDisk(string path, string filename)
        {
            bool result = true;

            string filepath = path + filename;
            XmlReader reader = XmlReader.Create(@filepath);
            StringBuilder XmlDeclaration = new StringBuilder();
            string drive = Path.GetPathRoot(path);
            DriveInfo driveInfo = new DriveInfo(drive);
            long driveSpace = driveInfo.TotalFreeSpace;
            long requiredSpace = 0;
            int numOfTabs = 1;
            bool writer = false;            

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Whitespace:
                        if (writer)
                        {
                            requiredSpace += "\r\n".Length;
                        }
                        break;

                    case XmlNodeType.XmlDeclaration:
                        
                        XmlDeclaration.Append("<?" + reader.Name);
                        if (reader.HasAttributes)
                        {
                            reader.MoveToFirstAttribute();
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                XmlDeclaration.Append(" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"");
                                reader.MoveToNextAttribute();
                            }
                            XmlDeclaration.Append("?>");
                        }
                        break;

                    case XmlNodeType.Element:
                        if (reader.Name == "Product")
                        {
                            requiredSpace += XmlDeclaration.ToString().Length;
                            requiredSpace += "<line>".Length;
                            writer = true;
                        }
                        if (writer)
                        {
                            bool isEmptyElement = reader.IsEmptyElement;
                            for (int j = 0; j < numOfTabs; ++j) requiredSpace += "\t".Length;
                            if (!isEmptyElement) ++numOfTabs;
                            requiredSpace += ("<" + reader.Name).Length;
                            if (reader.HasAttributes)
                            {
                                reader.MoveToFirstAttribute();
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    requiredSpace += (" " + reader.Name + "=\"" + reader.GetAttribute(i) + "\"").Length;
                                    reader.MoveToNextAttribute();
                                }

                            }
                            if (isEmptyElement) requiredSpace += "/>\r\n".Length;
                            else requiredSpace += ">\r\n".Length;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (writer)
                        {
                            --numOfTabs;
                            for (int j = 0; j < numOfTabs; ++j) requiredSpace += "\t".Length;
                            requiredSpace += ("</" + reader.Name + ">").Length;
                        }
                        if (reader.Name == "Product")
                        {
                            requiredSpace += "</line>".Length;
                            writer = false;
                        }
                        break;

                    case XmlNodeType.Text:
                        if (writer)
                        {
                            requiredSpace += (reader.Value).Length;
                        }

                        break;

                }

            }

            return requiredSpace <= driveSpace;
        }
    }
}
