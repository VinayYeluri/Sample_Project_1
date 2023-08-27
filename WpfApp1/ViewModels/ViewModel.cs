using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ViewModel: ObservableObject
    {      
        /// <summary>
        /// This property is binded to the Combobox Itemsource which consists of countries list from csv file.
        /// </summary>
        public ObservableCollection<string> CountriesList { get; set; }
        
        public ViewModel()
        {
            CountriesList = new ObservableCollection<string>();
            SortCommand = new RelayCommand(SortMethod);
            FilterCommand = new RelayCommand(FilterMethod);
            LoadCSVData();
        }

        /// <summary>
        /// It will store the collection of persons data from csv file and is binded to the ItemsControl Itemssource to project the values into the labels
        /// </summary>
        private ObservableCollection<Person> _personsList;
        public ObservableCollection<Person> PersonsList
        {
            get 
            { 
                return _personsList; 
            }
            set 
            { 
                _personsList = value; 
                OnPropertyChanged(nameof(PersonsList)); 
            }
        }

        /// <summary>
        /// this property is binded to the selectedtext of combobox
        /// </summary>
        private string _selectedCountry;
        public string SelectedCountry
        {
            get 
            { 
                return _selectedCountry; 
            }
            set 
            { 
                _selectedCountry = value; 
                OnPropertyChanged(nameof(SelectedCountry)); 
            }
        }

        /// <summary>
        /// This command is binded to the sort button
        /// </summary>
        private RelayCommand _sortCommand;
        public RelayCommand SortCommand
        {
            get 
            { 
                return _sortCommand; 
            }
            set 
            { 
                _sortCommand = value; 
            }
        }

        /// <summary>
        /// This command is binded to the filter button
        /// </summary>
        private RelayCommand _filterCommand;
        public RelayCommand FilterCommand
        {
            get 
            { 
                return _filterCommand; 
            }
            set 
            { 
                _filterCommand = value; 
            }
        }

        /// <summary>
        /// this method is used to load the data into the PersonsList collection
        /// </summary>
        private void LoadCSVData()
        {
            // Define the path to your CSV file.
            string csvPath = @"C:\Users\vinay\Downloads\PersonsDemo.csv";

            // Read all lines of the CSV file.
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(csvPath).ToList();
            
            //this will remove the header of the csv file which has column titles
            lines.RemoveAt(0); 
            PersonsList = new ObservableCollection<Person>();
            
            foreach (string line in lines)
            {
                // Split each line by comma
                string[] columns = line.Split(',');
                PersonsList.Add(new Person { Value = columns[0] + "\n" + columns[1] + "\n" + columns[columns.Length - 1], Name = columns[0], Country = columns[1], PhoneNumber = columns[columns.Length - 1] });
                
                //This condition is used to collect the countries to bind with combobox items source.
                if (!CountriesList.Contains(columns[1]))
                {
                    CountriesList.Add(columns[1]);
                }
            }
        }

        /// <summary>
        /// This method will sort the persons data per Country and Name when the sort button is pressed.
        /// </summary>
        private void SortMethod()
        {
            LoadCSVData();
            var query = PersonsList.OrderBy(p => p.Country).ThenBy(p => p.Name);
            PersonsList = new ObservableCollection<Person>(query);
        }

        /// <summary>
        /// This methos will filter the persons data per Country. When the filter button pressed the selected(combobox selection) country data will be projected.
        /// </summary>
        private void FilterMethod()
        {
            LoadCSVData();
            var query = PersonsList.Where(p => p.Country == SelectedCountry);
            PersonsList = new ObservableCollection<Person>(query);
        }       
    }
}
