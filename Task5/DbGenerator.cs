using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.Models;

namespace Task5
{
    public class DbGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>())) {
                if (!context.Teams.Any()) {
                    Team teamA = new Team();
                    teamA.TeamId = 1;
                    teamA.Name = "a";
                    teamA.NumeberOfPlayers = 6;

                    Team teamB = new Team();
                    teamB.TeamId = 2;
                    teamB.Name = "b";
                    teamB.NumeberOfPlayers = 6;

                    context.Teams.Add(teamA);
                    context.Teams.Add(teamB);
                    context.SaveChanges();
                }

                if (!context.Players.Any()) {
                    Player playerA = new Player();
                    playerA.PlayerId = 1;
                    playerA.Name = "A";
                    playerA.TeamId = 1;

                    Player playerA1 = new Player();
                    playerA1.PlayerId = 2;
                    playerA1.Name = "A1";
                    playerA1.TeamId = 1;

                    Player playerA2 = new Player();
                    playerA2.PlayerId = 3;
                    playerA2.Name = "A2";
                    playerA2.TeamId = 1;

                    Player playerA3 = new Player();
                    playerA3.PlayerId = 4;
                    playerA3.Name = "A3";
                    playerA3.TeamId = 1;

                    Player playerA4 = new Player();
                    playerA4.PlayerId = 5;
                    playerA4.Name = "A4";
                    playerA4.TeamId = 1;

                    Player playerA5 = new Player();
                    playerA5.PlayerId = 6;
                    playerA5.Name = "A5";
                    playerA5.TeamId = 1;

                    context.Players.Add(playerA);
                    context.Players.Add(playerA1);
                    context.Players.Add(playerA2);
                    context.Players.Add(playerA3);
                    context.Players.Add(playerA4);
                    context.Players.Add(playerA5);


                    Player playerB = new Player();
                    playerB.PlayerId = 7;
                    playerB.Name = "B";
                    playerB.TeamId = 2;

                    Player playerB1 = new Player();
                    playerB1.PlayerId = 8;
                    playerB1.Name = "B1";
                    playerB1.TeamId = 2;

                    Player playerB2 = new Player();
                    playerB2.PlayerId = 9;
                    playerB2.Name = "B2";
                    playerB2.TeamId = 2;

                    Player playerB3 = new Player();
                    playerB3.PlayerId = 10;
                    playerB3.Name = "B3";
                    playerB3.TeamId = 2;

                    Player playerB4 = new Player();
                    playerB4.PlayerId = 11;
                    playerB4.Name = "B4";
                    playerB4.TeamId = 2;

                    Player playerB5 = new Player();
                    playerB5.PlayerId = 12;
                    playerB5.Name = "B5";
                    playerB5.TeamId = 2;

                    context.Players.Add(playerB);
                    context.Players.Add(playerB1);
                    context.Players.Add(playerB2);
                    context.Players.Add(playerB3);
                    context.Players.Add(playerB4);
                    context.Players.Add(playerB5);
                    context.SaveChanges();
                }

                if (!context.Statusses.Any()) {
                    Statuss status1 = new Statuss();
                    status1.StatussId = 1;
                    status1.Name = "Not started";

                    Statuss status2 = new Statuss();
                    status2.StatussId = 2;
                    status2.Name = "Started";

                    Statuss status3 = new Statuss();
                    status3.StatussId = 3;
                    status3.Name = "Finished";

                    Statuss status4 = new Statuss();
                    status4.StatussId = 4;
                    status4.Name = "Canceled";

                    context.Statusses.Add(status1);
                    context.Statusses.Add(status2);
                    context.Statusses.Add(status3);
                    context.Statusses.Add(status4);
                    context.SaveChanges();
                }
            }
        }
    }
}
