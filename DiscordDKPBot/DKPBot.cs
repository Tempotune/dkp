using Discord;
using Discord.Commands;
using DiscordDKPBot.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordDKPBot
{
    class DKPBot
    {
        DiscordClient client;
        CommandService commands;
        Random rand;
        System.Timers.Timer aTimer = new System.Timers.Timer();

        public DKPBot()
        {
            rand = new Random();
            

            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });


            client.UsingCommands(input =>
            {
                input.PrefixChar = '!';
                input.AllowMentionPrefix = true;
            });
            commands = client.GetService<CommandService>();


            // Point System

            // Add Commands

            //Dice Roll Commands
            AutomaticDKP();
            DiceRoll();
            AwardSystem();
            Check();




            // End Commands
            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MjkxMDcwOTEzNDg5NjY2MDQ4.C6rfdA.EK6Giq2cwinvnMbj0b8ooTpJYEY", TokenType.Bot);
                client.SetGame("type !h for help");
            });
        }
        
            

        private void Check()
        {
            commands.CreateCommand("check")
                .Do(async (e) =>
                {
                    await checking(e);

                    
                }); 
        }

        private async Task checking(CommandEventArgs e)
        {
            var amountUsers = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f).Count();
            var notdeafened = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => !f.IsSelfDeafened);
            var user = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f);
            var amount = 0;
            int index;
            for (index = 0; index < amountUsers; ++index )
            {
                var check = notdeafened.ElementAtOrDefault(index);
                if (check != false)
                {
                    var checking = user.ElementAtOrDefault(index);
                    amount += 1;
                }
                
            }
            await e.Channel.SendMessage(string.Format("`{0}`",amount));
        }

        private void AwardSystem()
        {
            commands.CreateCommand("h")
                .Do(async (e) =>
               {
                   string text = File.ReadAllText(@"C:\Users\TempoTune\Documents\Visual Studio 2017\Projects\DiscordDKPBot\help.txt");
                   await e.Channel.SendMessage(text);
               });
            commands.CreateCommand("add")
                    .Parameter("dkp", ParameterType.Multiple)
                    .Do(async (e) =>
                    {
                        await AwardUserDKP(e);
                    });
            commands.CreateCommand("sub")
                .Parameter("dkp", ParameterType.Multiple)
                .Do(async (e) =>
                {
                    await SubUserDKP(e);
                });
            commands.CreateCommand("dkp")
                .Parameter("user", ParameterType.Optional)
                .Do(async (e) =>
                {
                    await DkpCount(e);
                });
            commands.CreateCommand("roles")
                .Parameter("user", ParameterType.Optional)
                .Do(async (e) =>
                {
                    await createroles(e);
                });
        }

        private async Task createroles(CommandEventArgs e)
        {
            if (e.User.ServerPermissions.Administrator == true)
            {
                if (e.Server.GetUser(e.Server.CurrentUser.Id).ServerPermissions.ManageRoles == true | e.Server.GetUser(e.Server.CurrentUser.Id).ServerPermissions.Administrator == true)
                {
                    Console.WriteLine("Managing roles");
                    if (e.Server.FindRoles("BM") == null)
                    {
                        await e.Server.CreateRole("BM",null,null,false,false);
                    }
                    else if (e.Server.FindRoles("KFM") == null)
                    {
                        await e.Server.CreateRole("KFM");
                    }
                    else if (e.Server.FindRoles("SF") == null)
                    {
                        await e.Server.CreateRole("SF");
                    }
                    else if (e.Server.FindRoles("DES") == null)
                    {
                        await e.Server.CreateRole("DES");
                    }
                    else if (e.Server.FindRoles("BD") == null)
                    {
                        await e.Server.CreateRole("BD");
                    }
                    else if (e.Server.FindRoles("SIN") == null)
                    {
                        await e.Server.CreateRole("SIN");
                    }
                    else if (e.Server.FindRoles("FM") == null)
                    {
                        await e.Server.CreateRole("FM");
                    }
                    else if (e.Server.FindRoles("SUM") == null)
                    {
                        await e.Server.CreateRole("SUM");
                    }
                    else if (e.Server.FindRoles("WL") == null)
                    {
                        await e.Server.CreateRole("WL");
                    }
                    else if (e.Server.FindRoles("Animus") == null)
                    {
                        await e.Server.CreateRole("Animus");
                    }
                    else if (e.Server.FindRoles("Animus") == null)
                    {
                        await e.Server.CreateRole("Animus");
                    }
                    else if (e.Server.FindRoles("Energy") == null)
                    {
                        await e.Server.CreateRole("Energy");
                    }
                    else if (e.Server.FindRoles("Ferocity") == null)
                    {
                        await e.Server.CreateRole("Ferocity");
                    }
                    else if (e.Server.FindRoles("Frost") == null)
                    {
                        await e.Server.CreateRole("Frost");
                    }
                    else if (e.Server.FindRoles("Shadow") == null)
                    {
                        await e.Server.CreateRole("Shadow");
                    }
                    else if (e.Server.FindRoles("Fire") == null)
                    {
                        await e.Server.CreateRole("Fire");
                    }
                    else if (e.Server.FindRoles("Earth") == null)
                    {
                        await e.Server.CreateRole("Earth");
                    }
                    else if (e.Server.FindRoles("Lightning") == null)
                    {
                        await e.Server.CreateRole("Lightning");
                    }
                    else if (e.Server.FindRoles("Wind") == null)
                    {
                        await e.Server.CreateRole("Wind");
                    }
                    else if (e.Server.FindRoles("Earring") == null)
                    {
                        await e.Server.CreateRole("Earring");
                    }
                    else if (e.Server.FindRoles("Ring") == null)
                    {
                        await e.Server.CreateRole("Ring");
                    }
                }
                else
                {
                    await e.Channel.SendMessage("This bot does not have permission to manage roles");
                }
            }
        }

        private async Task DkpCount(CommandEventArgs e)
        {
            var username = e.Args[0];

            var user = e.Server.FindUsers(username).FirstOrDefault();
            int dkpTotal;

            if (user != null | username == "" | username == "leaderboard" | username == "lb")
            {
                if (user != null | username =="leaderboard" | username =="lb")
                {
                    if (username != "leaderboard" && username != "lb" )
                    {
                        DKPService.CheckDKP(user, out dkpTotal);
                        await e.Channel.SendMessage(string.Format("{0} `has a total of {1} DKP.`", username, dkpTotal));
                    }
                    /*else
                    {
                        Console.WriteLine("WORKING");
                        var users = e.User;
                        DKPService.Leaderboard(users);
                    } */
                }
                else
                {
                    user = e.Server.FindUsers(e.User.Name).FirstOrDefault();
                    DKPService.CheckDKP(user, out dkpTotal);
                    
                    await e.Channel.SendMessage(string.Format("{0} `has a total of {1} DKP.`", e.User.Mention, dkpTotal));

                }

            }
            else
            {
                await e.Channel.SendMessage(string.Format("`Could not find user: {0}`", username));
            }
        }

        private void AutomaticDKP()
        {
            commands.CreateGroup("adkp", auto =>
            {
                auto.CreateCommand("on")
                    .Parameter("tick", ParameterType.Required)
                    .Do(async (e) =>
                    {
                        var pointGain = (Convert.ToInt32(e.Args[0]));
                        void OnTimedEvent(object source, ElapsedEventArgs f)
                        {
                            TellAsync();
                        }
                        async void TellAsync()
                        {
                            await e.Channel.SendMessage(String.Format("` 1 hour has passed each user in Raid Voice Channel has gained {0} points `", pointGain));
                            var amountUsers = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f).Count();
                            var notdeafened = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => !f.IsSelfDeafened);
                            var users = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f);
                            int index;
                            for (index = 0; index < amountUsers; ++index)
                            {
                                var check = notdeafened.ElementAtOrDefault(index);
                                if (check != false)
                                {
                                    var dream = users.ElementAtOrDefault(index);
                                    Console.WriteLine(dream);
                                    DKPService.AwardDKP(dream, pointGain);
                                }
                            }
                        }


                        if (pointGain > 0 && aTimer.Enabled == false && e.User.ServerPermissions.Administrator)
                        {
                            await e.Channel.SendMessage(string.Format("`Automatic DKP adder is now ON for {0} DKP every hour.`", pointGain));
                            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            aTimer.Interval = 1000 * 60 * 60;
                            aTimer.Start();
                        }
                        else if (aTimer.Enabled == true)
                        {
                            await e.Channel.SendMessage("`DKP Timer is already activated, please disable it first.`");
                        }
                        else if (!e.User.ServerPermissions.Administrator)
                        {
                            await e.Channel.SendMessage("`you are not an admin permissions.`");
                        }

                        else
                        {
                            await e.Channel.SendMessage("`Try again`");
                        }
                    });
                auto.CreateCommand("off")
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator)
                    {
                        await e.Channel.SendMessage("Automatic DKP adder is now OFF.");
                        aTimer.Dispose();
                    }
                    else
                    {
                        await e.Channel.SendMessage("`You do not have admin permissions.`");
                    }
                    
                });

            });
        }







        // private void COMMANDS
        private void DiceRoll()
        {
            commands.CreateCommand("Dice")
                .Parameter("number", ParameterType.Required)
                .Do(async (e) =>
                {
                    var number = Convert.ToInt32(e.Args[0]);
                    int randomrollIndex = rand.Next(1, ++number);
                    if (--number > 1)
                    {
                        await e.Channel.SendMessage(string.Format("`number roll is  ` ```{0}```", randomrollIndex));
                    }
                    else
                    {
                        await e.Channel.SendMessage("`Please enter a number higher than 1`");
                    }
                });
        }
        private async Task SubUserDKP(CommandEventArgs e)
        {
            var username = e.Args[0];

            var user = e.Server.FindUsers(username).FirstOrDefault();

            if (user != null && e.User.ServerPermissions.Administrator)
            {
                
                var dkpAmount = Convert.ToInt32(e.Args[1]);
                var item = e.Args[2];
                if (item == "mat" | item == "earring" | item == "ring" | item == "ss")
                {
                    DKPService.SubDKP(user, dkpAmount, item);
                    await e.Channel.SendMessage(string.Format("`{0} has lost {1} DKP, but has acquired their {2}`", username, dkpAmount, item));
                }
                else
                {
                    await e.Channel.SendMessage("invalid command, please use the right format: !sub @user {amount} {item}");
                }
            }
            else if (!e.User.ServerPermissions.Administrator)
            {
                await e.Channel.SendMessage("`You do not have admin permissions.`");
            }
            else
            {
                await e.Channel.SendMessage(string.Format("`Could not find user: {0}`", username));
            }
        }

        private async Task AwardUserDKP(CommandEventArgs e)
        {
            var username = e.Args[0];

            var user = e.Server.FindUsers(username).FirstOrDefault();

            if (user != null | username == "all")
            {
                if (user != null && username != "all" && e.User.ServerPermissions.Administrator)
                {
                    var dkpAmount = Convert.ToInt32(e.Args[1]);
                    Console.WriteLine(user);
                    DKPService.AwardDKP(user, dkpAmount);
                    await e.Channel.SendMessage(string.Format("{0} `has been awarded {1} DKP.`", username, dkpAmount));
                }
                else if (!e.User.ServerPermissions.Administrator)
                {
                    await e.Channel.SendMessage("`You do not have admin permissions.`");
                }
                else
                {
                    var dkpAmount = Convert.ToInt32(e.Args[1]);
                    var amountUsers = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f).Count();
                    var notdeafened = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => !f.IsSelfDeafened);
                    var users = e.Server.FindChannels("Raid", ChannelType.Voice).FirstOrDefault().Users.Select(f => f);
                    int index;
                    for (index = 0; index < amountUsers; ++index)
                    {
                        var check = notdeafened.ElementAtOrDefault(index);
                        if (check != false)
                        {
                            var dream = users.ElementAtOrDefault(index);
                            Console.WriteLine(dream);
                            DKPService.AwardDKP(dream, dkpAmount);
                        }
                    }

                    await e.Channel.SendMessage(string.Format("`Undeafened users in the voice channel have been awarded {1} DKP.`", username, dkpAmount));
                }

            }
            else
            {
                await e.Channel.SendMessage(string.Format("`Could not find user: {0}`", username));
            }

        }

        // SOMETHING ELSE
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
