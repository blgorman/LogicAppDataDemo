using Microsoft.EntityFrameworkCore.Migrations;
using SimpleProductData.Migrations.Scripts;

#nullable disable

namespace SimpleProductData.Migrations
{
    /// <inheritdoc />
    public partial class createprocedureprocessnewproductsfromstaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("SimpleProductData.Migrations.Scripts.Procedures.ProcessNewProductsFromStaging.ProcessNewProductsFromStaging.v0.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ProcessNewProductsFromStaging");
        }
    }
}
