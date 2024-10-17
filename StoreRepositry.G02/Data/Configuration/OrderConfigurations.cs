using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreCore.G02.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02.Data.Configuration
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(O => O.Status)
                .HasConversion(Os => Os.ToString(),
                Os=>(OrderStatus)Enum.Parse(typeof(OrderStatus),Os));
            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany().HasForeignKey(O => O.DeliveryMethodId);
        }
    }
}
