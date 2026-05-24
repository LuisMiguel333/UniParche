using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniParche.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddReactionTypeToLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReactionType",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactionType",
                table: "Likes");
        }
    }
}
