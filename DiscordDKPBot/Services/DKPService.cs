using Discord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscordDKPBot.Services
{
    public class DKPService
    {
        

        public static void AwardDKP(User user, int dkpAmount)
        {
            var serverIdAsString = user.Server.Id.ToString();
            var userIdAsString = user.Id.ToString();
            string filePath = @"C:\Users\TempoTune\Documents\Visual Studio 2017\Projects\DiscordDKPBot\DiscordDKPBot\DKP\"+(serverIdAsString)+ ".txt";
            if (!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }
            string[] lines = File.ReadAllLines(filePath);

            var list = lines
                    .Select(data =>
                    {
                        var split = data.Split(',');
                        return new Entity
                        {
                            username = split[0],
                            user_id = split[1],
                            dkp = int.Parse(split[2]),
                            server_id = split[3]
                        };

                    });


            var entity = list.Where(input => input.user_id == userIdAsString).Where(s => s.server_id == serverIdAsString).FirstOrDefault();
            List<string> output = new List<string>();

            List<Entity> users = list.ToList<Entity>();

            if (entity != null)
            {
                var split = ',';
                string str = File.ReadAllText(filePath);
                str = str.Replace(Convert.ToString(entity.user_id + split + entity.dkp + split + entity.server_id), Convert.ToString(entity.user_id + split + (entity.dkp += dkpAmount) + split + entity.server_id));
                File.WriteAllText(filePath, str);

            }

            else
            {
                if (user.Nickname != null)
                { 
                    Console.WriteLine($"{ user.Nickname },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    output.Add($"{ user.Nickname },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                }
                else
                {
                    Console.WriteLine($"{ user.Name },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    output.Add($"{ user.Name },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                }

            }
            

        }

        public static void SubDKP(User user, int dkpAmount, string item)
        {
            var serverIdAsString = user.Server.Id.ToString();
            var userIdAsString = user.Id.ToString();
            string filePath = @"C:\Users\TempoTune\Documents\Visual Studio 2017\Projects\DiscordDKPBot\DiscordDKPBot\DKP\" + (serverIdAsString) + ".txt";
            if (!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }
            string[] lines = File.ReadAllLines(filePath);

            var list = lines
                    .Select(data =>
                    {
                        var split = data.Split(',');
                        return new Entity
                        {
                            username = split[0],
                            user_id = split[1],
                            dkp = int.Parse(split[2]),
                            server_id = split[3]
                        };

                    });


            var entity = list.Where(input => input.user_id == userIdAsString).Where(s => s.server_id == serverIdAsString).FirstOrDefault();
            List<string> output = new List<string>();

            List<Entity> users = list.ToList<Entity>();

            if (entity != null)
            {
                var split = ',';
                string str = File.ReadAllText(filePath);
                str = str.Replace(Convert.ToString(entity.user_id + split + entity.dkp + split + entity.server_id), Convert.ToString(entity.user_id + split + (entity.dkp -= dkpAmount) + split + entity.server_id));
                File.WriteAllText(filePath, str);

            }

            else
            {
                if (user.Nickname != null)
                {
                    Console.WriteLine($"{ user.Nickname },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    output.Add($"{ user.Nickname },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                }
                else
                {
                    Console.WriteLine($"{ user.Name },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    output.Add($"{ user.Name },{userIdAsString},{dkpAmount},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                }

            }
        }

        public static void CheckDKP(User user , out int dkpTotal)
        {
            var serverIdAsString = user.Server.Id.ToString();
            var userIdAsString = user.Id.ToString();
            string filePath = @"C:\Users\TempoTune\Documents\Visual Studio 2017\Projects\DiscordDKPBot\DiscordDKPBot\DKP\" + (serverIdAsString) + ".txt";
            if (!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            var list = lines
                    .Select(data =>
                    {
                        var split = data.Split(',');
                        return new Entity
                        {
                            username = split[0],
                            user_id = split[1],
                            dkp = int.Parse(split[2]),
                            server_id = split[3]
                        };

                    });


            var entity = list.Where(input => input.user_id == userIdAsString).Where(s => s.server_id == serverIdAsString).FirstOrDefault();
            List<string> output = new List<string>();

            List<Entity> users = list.ToList<Entity>();


            if (entity != null)
            {
                dkpTotal = entity.dkp;
            }

            else
            {
                if (user.Nickname != null)
                {
                    Console.WriteLine($"{ user.Nickname },{userIdAsString},{0},{serverIdAsString}");
                    output.Add($"{ user.Nickname },{userIdAsString},{0},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                    dkpTotal = 0;
                }
                else
                {
                    Console.WriteLine($"{ user.Name },{userIdAsString},{0},{serverIdAsString}");
                    output.Add($"{ user.Name },{userIdAsString},{0},{serverIdAsString}");
                    Console.WriteLine("Adding a new character");
                    File.AppendAllLines(filePath, output);
                    dkpTotal = 0;
                }

            }



        }

        /*public static void Leaderboard(User name)
        {
            var serverIdAsString = name.Server.Id.ToString();
            var userIdAsString = name.Id.ToString();
            string filePath = @"C:\Users\TempoTune\Documents\dkp.txt";

            string[] lines = File.ReadAllLines(filePath);

            var list = lines
                    .Select(data =>
                    {
                        var split = data.Split(',');
                        return new Entity
                        {
                            username = split[0],
                            user_id = split[1],
                            dkp = int.Parse(split[2]),
                            server_id = split[3]
                        };

                    });


            var entity = list.Where(e => e.user_id == userIdAsString).Where(s => s.server_id == serverIdAsString).FirstOrDefault();
            var server = list.Where(s => s.server_id == serverIdAsString).FirstOrDefault();
            List<string> output = new List<string>();

            List<Entity> users = list.ToList<Entity>();
            string Leaderboard = @"C:\Users\TempoTune\Documents\Visual Studio 2017\Projects\DiscordDKPBot\leaderboard.txt";
            users.OrderByDescending(r => r.dkp);
            var query = server.dkp;
            foreach (int dkp in query)
            {
                Console.WriteLine(dkp);
            }

            if (entity != null)
            {
                foreach (var user in users)
                {
                    if (user == server)
                    {
                        for (int index = 0; index < 10; ++index)
                        { 
                            File.AppendAllText(Leaderboard, index + ' ' + entity.username.ElementAtOrDefault(index) + " has a total " + entity.dkp + " points");
                        }
                    }
                    
                }
            }



        }*/
    }
}
