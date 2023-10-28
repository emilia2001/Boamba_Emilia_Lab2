using Boamba_Emilia_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Models
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
