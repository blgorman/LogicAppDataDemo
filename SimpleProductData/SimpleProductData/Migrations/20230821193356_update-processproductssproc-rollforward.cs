using Microsoft.EntityFrameworkCore.Migrations;
using SimpleProductData.Migrations.Scripts;

#nullable disable

namespace SimpleProductData.Migrations
{
    /// <inheritdoc />
    public partial class updateprocessproductssprocrollforward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("SimpleProductData.Migrations.Scripts.Procedures.ProcessNewProductsFromStaging.ProcessNewProductsFromStaging.v2.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("SimpleProductData.Migrations.Scripts.Procedures.ProcessNewProductsFromStaging.ProcessNewProductsFromStaging.v1.sql");
        }
    }
}
