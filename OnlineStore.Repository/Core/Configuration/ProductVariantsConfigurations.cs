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
           
        }
    }
}
