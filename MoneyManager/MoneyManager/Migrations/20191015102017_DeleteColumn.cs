using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Migrations
{
    public partial class DeleteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var b = migrationBuilder.Sql(@"UPDATE dbo.Transactions SET dbo.Transactions.CategoryId = dbo.Categories.ParentId
                                           FROM dbo.Transactions
                                           INNER JOIN dbo.Categories
                                           ON dbo.Transactions.CategoryId = dbo.Categories.Id
                                           WHERE dbo.Categories.Id
                                           IN (SELECT dbo.Categories.Id FROM dbo.Categories WHERE dbo.Categories.ParentId != 0)");
            migrationBuilder.DropColumn("ParentId", "dbo.Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
