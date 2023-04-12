using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using static System.Windows.Forms.LinkLabel;

namespace MobiFlight.Config
{
    public class LcdSPIDisplay : BaseDevice
    {
        // We must use hardware SPI
        // FYI Mega SCK is pin 52, MOSI is 51, UNO/NanoPro micro etc SCK is pin 13 and MOSI is 11
        // Leonardo Pro micro SCK is pin 15 and MOSI is 16

        /*
        Sugested TFT connections for UNO and Atmega328 based boards
        sclk 13  // Don't change, this is the hardware SPI SCLK line
        mosi 11  // Don't change, this is the hardware SPI MOSI line
        cs   10  // Chip select for TFT display
        dc    9   // Data/command line
        rst   7   // Reset, you could connect this to the Arduino reset pin

        Suggested TFT connections for the MEGA and ATmega2560 based boards
        sclk 52  // Don't change, this is the hardware SPI SCLK line
        mosi 51  // Don't change, this is the hardware SPI MOSI line
        cs   47  // TFT chip select line
        dc   48  // TFT data/command line
        rst  44  // you could alternatively connect this to the Arduino reset
        */

        const ushort _paramCount = 4;

        // TODO: Remove
        //[XmlAttribute]
        //public Byte Address = 0x27;
        [XmlAttribute]
        public Byte Cols = 16;
        [XmlAttribute]
        public Byte Lines = 2;
        [XmlAttribute]
        public Byte tftwidth = 120;
        [XmlAttribute]
        public Byte tftheight = 120;
        [XmlAttribute]
        public Byte sclk = 13;
        [XmlAttribute]
        public Byte mosi = 11;
        [XmlAttribute]
        public Byte chipselect = 10;
        [XmlAttribute]
        public Byte datacommand = 9;
        [XmlAttribute]
        public Byte reset = 7;

        public LcdSPIDisplay() { Name = "LcdDisplaySPI"; _type = DeviceType.LcdSPIDisplay; }

        override public String ToInternal()
        {
            return base.ToInternal() + Separator

                 // TODO: Remove
                 /*
                  + Address + Separator                 
                  + Cols + Separator
                  + Lines + Separator
                  + Name + End;
                 */

                 + tftwidth + Separator
                 + tftheight + Separator
                 + sclk + Separator
                 + mosi + Separator
                 + chipselect + Separator
                 + datacommand + Separator
                 + reset + Separator
                 + Cols + Separator
                 + Lines + Separator
                 // TODO: Remove
                 //+ Address + Separator
                 + Name + End;
        }

        override public bool FromInternal(String value)
        {
            if (value.Length == value.IndexOf(End) + 1) value = value.Substring(0, value.Length - 1);
            String[] paramList = value.Split(Separator);
            if (paramList.Count() != _paramCount + 1)
            {
                throw new ArgumentException("Param count does not match. " + paramList.Count() + " given, " + _paramCount + " expected");
            }

            // TODO: Remove
            /*
            Address = byte.Parse(paramList[1]);
            Cols = byte.Parse(paramList[2]);
            Lines = byte.Parse(paramList[3]);
            Name = paramList[4];
            */
            //Address = byte.Parse(paramList[1]);

            Cols = byte.Parse(paramList[2]);
            Lines = byte.Parse(paramList[3]);
            Name = paramList[4];
            tftwidth = byte.Parse(paramList[5]);
            tftheight = byte.Parse(paramList[6]);
            sclk = byte.Parse(paramList[7]);
            mosi = byte.Parse(paramList[8]);
            chipselect = byte.Parse(paramList[9]);
            datacommand = byte.Parse(paramList[10]);
            reset = byte.Parse(paramList[11]);

            return true;
        }

        public override bool Equals(object obj)
        {
            LcdSPIDisplay other = obj as LcdSPIDisplay;
            if (other == null)
            {
                return false;
            }

            // TODO: Remove
            /*
            return this.Name == other.Name
                && this.Cols == other.Cols
                && this.Lines == other.Lines
                && this.Address == other.Address
                && this.Type == other.Type;
            */

            return this.Name == other.Name
               && this.Cols == other.Cols
               && this.Lines == other.Lines
               && this.tftwidth == other.tftwidth
               && this.tftheight == other.tftheight
               && this.sclk == other.sclk
               && this.mosi == other.mosi
               && this.chipselect == other.chipselect
               && this.datacommand == other.datacommand
               && this.reset == other.reset
               // TODO: Remove
               //&& this.Address == other.Address
               && this.Type == other.Type;
        }

        public override string ToString()
        {
            // TODO: Remove
            //return Type + ":" + Name + " Cols:" + Cols + " Lines:" + Lines + " Address:" + Address;
            return Type + ":" + Name + "Cols:" + Cols + " Lines:" + Lines + tftwidth + ":" + tftheight + " sclk:" + sclk + " mosi:" + mosi + " chipselect:" + chipselect + " datacommand:" + datacommand + " reset:" + reset;
        }
    }
}
