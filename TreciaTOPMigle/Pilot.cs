using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreciaTOPMigle
{
    public class Pilot
    {
        public Pilot()
        {

        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        /*[Key]*///LicenceNumb
        public int PilotId { get; set; }
        public string TelephoneNumb { get; set; }
        //[ForeignKey("Balloon")]
        public virtual Balloon Balloon { get; set;}

        public IList<Order> Orders { get; set; }

    }
}
