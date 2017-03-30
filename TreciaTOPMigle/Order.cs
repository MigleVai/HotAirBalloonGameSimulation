using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreciaTOPMigle
{
    public class Order
    {
        public Order()
        {

        }
        //Key
        //[Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public State OrderState { get; set; }
        public int PassangerAmount { get; set; }
        //[ForeignKey("Pilot")]
        public int PilotId { get; set; }
        public virtual Pilot Pilot { get; set; }
        //ForeignKey
        //[ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
