using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.Utils
{
    public class Enumerations
    {
        public enum AssetStatus
        {
            [Description("Good")]
            Good = 0,
            [Description("Bad")]
            Bad = 1,
         
        }

        public enum PaymentStatus
        {
            [Description("Pending")]
            Pending = 0,
            [Description("Paid")]
            Paid = 1,

        }


        public enum AccountStatus
        {
            [Description("Disabled")]
            Disabled = 0,
            [Description("Active")]
            Active = 1,

        }


    }
}
