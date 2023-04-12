using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Settings.Device
{
    public partial class MFLcddSPIDisplayPanel : UserControl
    {
        /// <summary>
        /// Gets raised whenever config object has changed
        /// </summary>
        public event EventHandler Changed;

        private MobiFlight.Config.LcdSPIDisplay config;
        bool initialized = false;

        public MFLcddSPIDisplayPanel()
        {
            InitializeComponent();
        }

        public MFLcddSPIDisplayPanel(MobiFlight.Config.LcdSPIDisplay config, List<MobiFlightPin> Pins) : this()
        {
            //// TODO: Complete member initialization
            this.config = config;
            NameTextBox.Text = config.Name;
            // TODO: Remove
            //AddressComboBox.SelectedItem = "0x" + config.Address.ToString("X2");
            ColTextBox.Text = config.Cols.ToString();
            LinesTextBox.Text = config.Lines.ToString();
            HeightTextBox1.Text = config.tftheight.ToString();
            WidthTextBox1.Text = config.tftwidth.ToString();
            MOSITextBox1.Text = config.mosi.ToString();
            ChipSelectTextBox1.Text = config.chipselect.ToString();
            DataCommandTextBox1.Text = config.datacommand.ToString();
            ResetTextBox1.Text = config.reset.ToString();

            ////setValues();

            initialized = true;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized) return;
            setValues();
            if (Changed!=null)
                Changed(config, new EventArgs());
        }

        private void setValues()
        {
            config.Name     = NameTextBox.Text;
            // TODO: Remove
            //config.Address  = Byte.Parse(AddressComboBox.Text.Replace("0x",""), System.Globalization.NumberStyles.HexNumber);
            byte Cols;
            if (Byte.TryParse(ColTextBox.Text, out Cols)) {
                config.Cols = Cols;
            }
            byte Lines;
            if (Byte.TryParse(LinesTextBox.Text, out Lines)) { 
                config.Lines = Lines;
            }
            byte tftheight;
            if (Byte.TryParse(HeightTextBox1.Text, out tftheight))
            {
                config.tftheight = tftheight;
            }
            byte tftwidth;
            if (Byte.TryParse(WidthTextBox1.Text, out tftwidth))
            {
                config.tftwidth = tftwidth;
            }
            byte mosi;
            if (Byte.TryParse(MOSITextBox1.Text, out mosi))
            {
                config.mosi = mosi;
            }
            byte chipselect;
            if (Byte.TryParse(ChipSelectTextBox1.Text, out chipselect))
            {
                config.chipselect = chipselect;
            }
            byte datacommand;
            if (Byte.TryParse(DataCommandTextBox1.Text, out datacommand))
            {
                config.datacommand = datacommand;
            }
            byte reset;
            if (Byte.TryParse(ResetTextBox1.Text, out reset))
            {
                config.reset = reset;
            }
        }

        private void ColTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                byte value = byte.Parse((sender as TextBox).Text);
            } catch (Exception ex)
            {                
                e.Cancel = true;
                displayError(
                    sender as Control,
                    String.Format(
                        i18n._tr("uiMessageValidNumberInRange"),
                        byte.MinValue.ToString(),
                        byte.MaxValue.ToString()
                    )
                );
                return;
            }

            removeError(sender as Control);

            value_Changed(sender, e);
        }

        private void displayError(Control control, String message)
        {
            errorProvider.SetIconAlignment(control, ErrorIconAlignment.TopRight);
            errorProvider.SetError(
                    control,
                    message);
            MessageBox.Show(message, i18n._tr("Hint"));
        }
        private void removeError(Control control)
        {
            errorProvider.SetError(
                    control,
                    "");
        }

        // TODO: Remove
        /*private void AddressComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);
        }*/
    }
}
