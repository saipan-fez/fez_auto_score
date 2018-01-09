using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FEZAutoScore.Migrations
{
    public partial class Score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    記録日時 = table.Column<DateTime>(nullable: false),
                    Map名 = table.Column<string>(nullable: false),
                    PC与ダメージ = table.Column<int>(nullable: false),
                    キルダメージボーナス = table.Column<int>(nullable: false),
                    キル数 = table.Column<int>(nullable: false),
                    クリスタル採掘量 = table.Column<int>(nullable: false),
                    クリスタル運用ボーナス = table.Column<int>(nullable: false),
                    スキル1 = table.Column<string>(nullable: false),
                    スキル2 = table.Column<string>(nullable: false),
                    スキル3 = table.Column<string>(nullable: false),
                    スキル4 = table.Column<string>(nullable: false),
                    スキル5 = table.Column<string>(nullable: false),
                    スキル6 = table.Column<string>(nullable: false),
                    スキル7 = table.Column<string>(nullable: false),
                    スキル8 = table.Column<string>(nullable: false),
                    デッド数 = table.Column<int>(nullable: false),
                    備考 = table.Column<string>(nullable: true),
                    召喚行動ボーナス = table.Column<int>(nullable: false),
                    召喚解除ボーナス = table.Column<int>(nullable: false),
                    建築与ダメージ = table.Column<int>(nullable: false),
                    建築数 = table.Column<int>(nullable: false),
                    建築物破壊数 = table.Column<int>(nullable: false),
                    戦争継続時間 = table.Column<TimeSpan>(nullable: false),
                    戦闘 = table.Column<int>(nullable: false),
                    支援 = table.Column<int>(nullable: false),
                    結果 = table.Column<int>(nullable: false),
                    職業 = table.Column<int>(nullable: false),
                    貢献度 = table.Column<int>(nullable: false),
                    集計対象 = table.Column<bool>(nullable: false),
                    領域 = table.Column<int>(nullable: false),
                    領域ダメージボーナス = table.Column<int>(nullable: false),
                    領域破壊ボーナス = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.記録日時);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");
        }
    }
}
