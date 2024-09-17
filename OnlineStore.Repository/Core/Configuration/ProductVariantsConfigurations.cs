using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Core.Configuration
{
    internal class ProductVariantsConfigurations : IEntityTypeConfiguration<ProductVariants>
    {
        public void Configure(EntityTypeBuilder<ProductVariants> builder)
        {
            builder.Property(p => p.PrecentageOfSales).HasComputedColumnSql("((CAST([TotalQuantity] - [Quantity] AS DECIMAL) / [TotalQuantity]) * 100)");
            builder.Property(p => p.Color).HasConversion(
                c => c.ToString(),
                stringColor => (Color)Enum.Parse(typeof(Color), stringColor, true)
                );
            builder.Property(p => p.Size).HasConversion(
                s => s.ToString(),
                stringSize => (Size) Enum.Parse(typeof(Size) , stringSize,true)
                );
        }
    }
}
