using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartMote.Controls
{
    public class SelectBox : Picker
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<KeyValuePair<string, object>>), typeof(SelectBox), new ObservableCollection<KeyValuePair<string, object>>(), BindingMode.OneWay, null, propertyChanged: ItemsSourcePropertyChanged);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(SelectBox), null, BindingMode.TwoWay, null, propertyChanged: SelectedItemPropertyChanged);
        public ObservableCollection<KeyValuePair<string, object>> ItemsSource
        {
            get
            {
                return (ObservableCollection<KeyValuePair<string, object>>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public SelectBox()
        {
            SelectedIndexChanged += SelectBox_SelectedIndexChanged;
        }

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectBox = (SelectBox)bindable;

            if (selectBox != null && newValue != null)
            {
                selectBox.Items.Clear();

                foreach (var item in selectBox.ItemsSource)
                {
                    selectBox.Items.Add(item.Key);
                }
            }
        }

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectBox = (SelectBox)bindable;

            if (selectBox != null && newValue != null && newValue != (object)selectBox.ItemsSource.ElementAtOrDefault(selectBox.SelectedIndex))
            {
                selectBox.SelectedIndex = selectBox.ItemsSource.IndexOf((KeyValuePair<string, object>)newValue);
            }
        }

        private void SelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex > 0 && SelectedIndex < ItemsSource.Count)
            { 
                SelectedItem = ItemsSource.ElementAtOrDefault(SelectedIndex);
            }
        }
    }
}
