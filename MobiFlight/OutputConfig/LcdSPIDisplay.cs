using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobiFlight.OutputConfig
{
    public class LcdSPIDisplay : IXmlSerializable, ICloneable
    {
        public const string Type = "LcdSPIDisplay";
        //TODO: Remove
        //public String Address { get; set; }
        public String MOSI { get; set; }
        public String ChipSelect { get; set; }
        public String DataCommand { get; set; }
        public String Reset { get; set; }
        public String Cols { get; set; }
        public String Height { get; set; }
        public String Width { get; set; }


        public List<String> Lines { get; set; }

        public LcdSPIDisplay()
        {
            Lines = new List<string>();
        }

        public override bool Equals(object obj)
        {
            bool linesAreEqual = true && Lines.Count == (obj as LcdSPIDisplay).Lines.Count;
            if (linesAreEqual)
            for(int i=0; i!=Lines.Count; i++)
            {
                 linesAreEqual = linesAreEqual && (Lines[i] == (obj as LcdSPIDisplay).Lines[i]);
            }
            
            return (
                obj != null && obj is LcdSPIDisplay &&
                this.MOSI == (obj as LcdSPIDisplay).MOSI &&
                linesAreEqual
            );
        }

        public object Clone()
        {
            LcdSPIDisplay clone = new LcdSPIDisplay();
            clone.MOSI = MOSI;
            foreach(string line in Lines)
            {
                clone.Lines.Add(line);
            }

            return clone;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader["mosi"] != null && reader["mosi"] != "")
            {
                MOSI = reader["mosi"].ToString();
            }
            reader.Read();

            if (reader.LocalName == "line")
            {
                while (reader.LocalName == "line")
                {
                    if (!reader.IsEmptyElement)
                    {
                        reader.Read();
                        Lines.Add(reader.Value);
                        if (reader.NodeType == XmlNodeType.Text)
                            reader. Read();
                        reader.ReadEndElement(); //line
                    }
                    else
                    {
                        Lines.Add("");
                        reader.Read();
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("mosi", MOSI);

            foreach (string line in Lines) {
                writer.WriteElementString("line", line);
            }
        }
    }
}
