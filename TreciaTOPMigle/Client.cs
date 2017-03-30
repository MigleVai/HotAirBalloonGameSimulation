using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreciaTOPMigle
{
    public class Client
    {
        public Client()
        {
            
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string TelephoneNumb { get; set; }
        //[Key]
        public int ClientId { get; set;}
        public IList<Order> Orders { get; set; }
    }
}
