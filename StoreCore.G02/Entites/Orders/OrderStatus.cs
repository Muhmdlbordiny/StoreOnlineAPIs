using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites.Orders
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentSuccess")]

        PaymentSuccess,
        [EnumMember(Value = "PaymentFailed")]

        PaymentFailed
    }
}
