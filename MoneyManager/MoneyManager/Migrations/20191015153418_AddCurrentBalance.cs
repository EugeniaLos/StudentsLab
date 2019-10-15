using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Migrations
{
    public partial class AddCurrentBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>("CurBalance", "Assets", nullable: true);
            migrationBuilder.Sql(@"UPDATE Assets 
Set CurrentBalance = amountTable.Amount
From Assets
inner join (SELECT table1.AssetId,
		table1.deb - table2.cred AS Amount
FROM  (SELECT AssetId, sum(Amount) deb
	FROM Transactions 
	Inner Join Categories on Transactions.CategoryId = Categories.Id 
	where categories.Type = 1
	group by AssetId) table1 JOIN (SELECT AssetId, sum(Amount) cred
	FROM Transactions 
	Inner Join Categories on Transactions.CategoryId = Categories.Id 
	where categories.Type = 0
	group by AssetId) 
	table2 on table1.AssetId = table2.AssetId) amountTable
	on Assets.Id = amountTable.AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
