using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02.Data.Configuration
{
    public class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Name)
                 .HasColumnType("varchar(450)").IsRequired();
        }
    }
}
