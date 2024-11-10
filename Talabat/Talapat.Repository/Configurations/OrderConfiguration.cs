using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasConversion(status => status.ToString(),
                                  status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status));

            builder.OwnsOne(o => o.CustomerInformation, o => o.WithOwner());

            builder.HasOne(o => o.DeliveryType)
                   .WithMany()
                   .HasForeignKey(o=>o.DeliveryId);

            builder.Property(o => o.SupTotal).HasColumnType("decimal(18,2)");

            builder.Property(o => o.Total).HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DeliveryType)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
