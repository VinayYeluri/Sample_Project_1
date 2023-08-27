using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Person
    {
        /// <summary>
        /// to store the name of the person
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// to store the country of the person
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// to store the phonenumber of the person
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// to store the name, country, phone number of the person in a sigle unit for the data projection in the form of label
        /// </summary>
        public string? Value { get; set; }
    }
}
