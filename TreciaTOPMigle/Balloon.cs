using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreciaTOPMigle
{
    public class Balloon
    {
        public Balloon()
        {

        }
        public string Color { get; set; }
        [Key]
        public int BalloonId { get; set; }
        public int PassangerNumb { get; set; }
        //[ForeignKey("Pilot")]
        public int PilotId { get; set; }
        public virtual Pilot Pilot { get; set; }

    }
}
