using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Migrations
{
    public partial class DeleteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var b = migrationBuilder.Sql(@"UPDATE dbo.Transaction SET dbo.Transaction.CategoryId = dbo.Category.ParentId
                                           FROM dbo.Transaction
                                           INNER JOIN dbo.Category
                                           ON dbo.Transaction.CategoryId = dbo.Category.Id
                                           WHERE dbo.Category.Id
                                           IN (SELECT dbo.Category.Id FROM dbo.Category WHERE dbo.Category.ParentId IS NOT NULL)");
            migrationBuilder.DropColumn("ParentId", "dbo.Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
