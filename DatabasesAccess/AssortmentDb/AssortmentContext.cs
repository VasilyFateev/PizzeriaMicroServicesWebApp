using DatabaseModels.AssortmentDatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DatabasesAccess.AssortmentDb
{
	public class AssortmentContext : DbContext
	{
		public AssortmentContext(DbContextOptions<AssortmentContext> options) : base(options) { }
		public AssortmentContext() { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Variation> Variations { get; set; }
		public DbSet<VariationOption> VariationsOption { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductItem> ProductItems { get; set; }
		public DbSet<ProductConfiguration> ProductConfigurations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId)
				.HasConstraintName("FK_product_category_id");

			modelBuilder.Entity<Category>()
				.HasMany(c => c.Variations)
				.WithOne(v => v.Category)
				.HasForeignKey(v => v.CategoryId)
				.HasConstraintName("FK_variation_category_id");

			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductItems)
				.WithOne(pi => pi.Product)
				.HasForeignKey(pi => pi.ProductId)
				.HasConstraintName("FK_product_item_product_id");

			modelBuilder.Entity<ProductItem>()
				.HasMany(pi => pi.Configurations)
				.WithOne(pc => pc.ProductItem)
				.HasForeignKey(pc => pc.ProductItemId)
				.HasConstraintName("FK_product_configuration_product_item_id");

			modelBuilder.Entity<Variation>()
				.HasMany(v => v.VariationOptions)
				.WithOne(vo => vo.Variation)
				.HasForeignKey(vo => vo.VariationId)
				.HasConstraintName("FK_variation_option_variation_id");

			modelBuilder.Entity<VariationOption>()
				.HasMany(vo => vo.Configurations)
				.WithOne(pc => pc.VariationOption)
				.HasForeignKey(pc => pc.VariationOptionId)
				.HasConstraintName("FK_product_configuration_variation_option_id");

			modelBuilder.Entity<ProductConfiguration>()
				.HasKey(pc => new { pc.ProductItemId, pc.VariationOptionId });
		}
	}
}
