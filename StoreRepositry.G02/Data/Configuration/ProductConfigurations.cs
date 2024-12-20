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
    public class ProductConfigurations: IEntityTypeConfiguration<Product>
    {
        

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                            .HasMaxLength(200).IsRequired();
            builder.Property(p => p.Description)
                .HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.PictureUrl).IsRequired(false);
            builder.Property(p => p.Price)
                        .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Brand)
                        .WithMany()
                        .HasForeignKey(p => p.BrandId)
                        .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(p => p.Type)
                    .WithMany()
                    .HasForeignKey(p => p.TypeId)
                    .OnDelete(DeleteBehavior.SetNull);
            builder.Property(p => p.BrandId).IsRequired();
            builder.Property(p => p.TypeId).IsRequired();
        }
    }
}
