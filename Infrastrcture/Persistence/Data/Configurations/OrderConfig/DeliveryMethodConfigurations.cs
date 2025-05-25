﻿using DomainLayer.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations.OrderConfig
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasMaxLength(50);
        }
    }
}
