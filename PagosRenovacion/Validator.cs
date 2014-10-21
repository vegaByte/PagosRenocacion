using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PagosRenovacion
{
    public class Validator
    {

        public ComboBox fillListBoxWithRange(ComboBox comboBox,int valIni,int valFinal)
        {
            foreach(object obj in comboBox.Items)
                comboBox.Items.RemoveAt(0);

            for (int x = valIni; x <= valFinal; x++)
            {
                comboBox.Items.Add(x);
            }
            return comboBox;
        }

        public void validaFloat(object sender, KeyEventArgs e)
        {
            bool yaHayPunto = false;
            int numDecimales = 0;
            //MessageBox.Show(e.Key.ToString());
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                e.Key == Key.OemPeriod || e.Key == Key.Tab)
            {
                if (e.Key == Key.OemPeriod)
                {
                    foreach (char ch in (sender as TextBox).Text.ToCharArray())
                    {
                        if (ch.Equals('.'))
                            yaHayPunto = true;                        
                    }
                    e.Handled = yaHayPunto;
                }
                else
                {
                    foreach (char ch in (sender as TextBox).Text.ToCharArray())
                    {
                        if (ch.Equals('.'))
                            yaHayPunto = true;
                        else
                            if (yaHayPunto)
                                numDecimales++;
                    }
                    if (numDecimales == 2)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else
                e.Handled = true;
            //try
            //{
            //    float tmp;
            //    float.Parse((sender as TextBox).Text, out tmp);
            //    MessageBox.Show(tmp.ToString());
            //}
            //catch
            //{
            //    MessageBox.Show("No float");
            //}
        }

        public bool ValidaString(string str)
        {
            str = RecortaString(str);

            if (str.Equals(""))
            {
                MessageBox.Show("Favor de llenar todos los campos.", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public string RecortaString(string str)
        {
            if (str.Equals(""))
                return str;

            //elimina los espacios al comienzo de la caden
            while (str.ElementAt(0).Equals(' '))
            {
                str = str.Substring(1, str.Length - 1);
                if (str.Equals(""))
                    break;
            }
            return str;
        }
        
        public bool ValidaCerrar()
        {
            var vtnEmergente = MessageBox.Show("Realmente desea salir de esta ventana?\nLos datos que no hayan sido guardados se perderan.", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (vtnEmergente == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }

        public bool ValidaSeleccionNoNull(DataGrid grid)
        {
            if (grid.SelectedIndex != -1)
                return true;
            else
            {
                MessageBox.Show("Seleccione el registro que desee editar.", "Item no seleccionado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public bool ValidaDatePickerNoNull(DatePicker picker)
        {
            if (picker.SelectedDate != null)
                return true;
            else
            {
                MessageBox.Show("Seleccione las fechas correspondientes", "Fecha no seleccionada", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public bool ValidaNotasContrato(Grid panel)
        {
            bool temp = true;
            foreach (StackPanel spanel in panel.Children)
            {
                foreach (UIElement element in spanel.Children)
                {
                    if (element is DatePicker && temp)
                    {
                        if ((element as DatePicker).SelectedDate == null)
                        {
                            temp = false;
                            MessageBox.Show("Favor de seleccioinar las fechas a todas la notas.", "Fecha no seleccionada", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                        }
                    }
                }
            }
            return temp;
        }

    }
}
