using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NewsApp.CORE.DBModels;
using NewsApp.DAL.Context;
using System.Data;
using System.Runtime.CompilerServices;

namespace NewsApp.MVC.Seeds
{
    public static class SeedData
    {
        public static async void SeedDataInit(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();

            context.Database.Migrate();


            await CreateRole(roleManager, "admin");
            await CreateRole(roleManager, "director");
            await CreateRole(roleManager, "writer");


            if (context.Users.Count() == 0)
            {
                #region Admins
                await userManager.CreateAsync(new AppUser() { UserName = "admin", Name = "Karen", Surname = "Perkson", Email = "adminuser@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                #endregion

                #region Directors
                await userManager.CreateAsync(new AppUser() { UserName = "director1", Name = "Jim", Surname = "Luther", Email = "director1@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director2", Name = "Kaycee", Surname = "Kerb", Email = "director2@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director3", Name = "Kamil", Surname = "Sarı", Email = "director3@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director4", Name = "Gaye", Surname = "Yesilova", Email = "director4@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director5", Name = "Alexa", Surname = "Abra", Email = "director5@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director6", Name = "Kristine", Surname = "Spike", Email = "director6@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director7", Name = "Emma", Surname = "Lasudashj", Email = "director7@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                #endregion

                #region Authors
                await userManager.CreateAsync(new AppUser() { UserName = "writer1", Name = "Anna", Surname = "Druawehks", Email = "writer1@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer2", Name = "Bayram", Surname = "Telli", Email = "writer2@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer3", Name = "Emre", Surname = "Tütün", Email = "writer3@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer4", Name = "Kerem", Surname = "Kuru", Email = "writer4@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer5", Name = "Ayşegül", Surname = "Türkmen", Email = "writer5@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer6", Name = "Hanzade", Surname = "Cakas", Email = "writer6@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer7", Name = "Emre", Surname = "Elibol", Email = "writer7@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer8", Name = "Fatma", Surname = "Sabuncuoglu", Email = "writer8@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer9", Name = "Erhan", Surname = "Kuruscu", Email = "writer9@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer10", Name = "Nihat", Surname = "Tabutcu", Email = "writer10@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer11", Name = "Hale", Surname = "Erdinç", Email = "writer11@test.com", CreatedDate = DateTime.Now, IsSubscriber = true }, "Password123.");
                #endregion

                #region UnApprovedUsers
                await userManager.CreateAsync(new AppUser() { UserName = "canerelibol", Name = "Caner", Surname = "Elibol", Email = "canerelibol@test.com",Phone = "555 333 22 11",BirthDate=new DateTime(1992,5,13), HomeLand="Adana", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "yelizuzun", Name = "Yeliz", Surname = "Uzun", Email = "yelizuzun@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1994, 8, 13), HomeLand="Ankara", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "seymaisik", Name = "Şeyma", Surname = "Işık", Email = "seymaisik@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1999, 1, 27), HomeLand = "Zonguldak", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "emir erbilen", Name = "Emir", Surname = "Erbilen", Email = "emirerbilen@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1999, 3, 19), HomeLand = "İstanbul", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "halilbicak", Name = "Halil", Surname = "Bıçak", Email = "halilbicak@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1989, 4, 5), HomeLand = "Ankara", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "fundayigi", Name = "Funda", Surname = "Yigit", Email = "fundayigit@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1992, 5, 13), HomeLand = "İzmir", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "semaaygul", Name = "Sema", Surname = "Aygül", Email = "semaaygul@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1994, 8, 13), HomeLand = "Ankara", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "erkutzaman", Name = "Erkut", Surname = "Zaman", Email = "erkutzaman@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1999, 1, 27), HomeLand = "Zonguldak", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "handantekinoglu", Name = "Handan", Surname = "Tekinoğlu", Email = "handantekinoglu@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1999, 3, 19), HomeLand = "İstanbul", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "samizorlu", Name = "Sami", Surname = "Zorlu", Email = "samizorlu@test.com", Phone = "555 333 22 11", BirthDate = new DateTime(1989, 4, 5), HomeLand = "Ankara", CreatedDate = DateTime.Now, IsSubscriber = false }, "Password123.");
                #endregion

            }
            var admin = await userManager.FindByEmailAsync("adminuser@test.com");
            var director1 = await userManager.FindByEmailAsync("director1@test.com");
            var director2 = await userManager.FindByEmailAsync("director2@test.com");
            var director3 = await userManager.FindByEmailAsync("director3@test.com");
            var director4 = await userManager.FindByEmailAsync("director4@test.com");
            var director5 = await userManager.FindByEmailAsync("director5@test.com");
            var director6 = await userManager.FindByEmailAsync("director6@test.com");
            var director7 = await userManager.FindByEmailAsync("director7@test.com");
            var writer1 = await userManager.FindByEmailAsync("writer1@test.com");
            var writer2 = await userManager.FindByEmailAsync("writer2@test.com");
            var writer3 = await userManager.FindByEmailAsync("writer3@test.com");
            var writer4 = await userManager.FindByEmailAsync("writer4@test.com");
            var writer5 = await userManager.FindByEmailAsync("writer5@test.com");
            var writer6 = await userManager.FindByEmailAsync("writer6@test.com");
            var writer7 = await userManager.FindByEmailAsync("writer7@test.com");
            var writer8 = await userManager.FindByEmailAsync("writer8@test.com");
            var writer9 = await userManager.FindByEmailAsync("writer9@test.com");
            var writer10 = await userManager.FindByEmailAsync("writer10@test.com");
            var writer11 = await userManager.FindByEmailAsync("writer11@test.com");



            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.AddToRoleAsync(director1, "director");
            await userManager.AddToRoleAsync(director2, "director");
            await userManager.AddToRoleAsync(director3, "director");
            await userManager.AddToRoleAsync(director4, "director");
            await userManager.AddToRoleAsync(director5, "director");
            await userManager.AddToRoleAsync(director6, "director");
            await userManager.AddToRoleAsync(director7, "director");
            await userManager.AddToRoleAsync(writer1, "writer");
            await userManager.AddToRoleAsync(writer2, "writer");
            await userManager.AddToRoleAsync(writer3, "writer");
            await userManager.AddToRoleAsync(writer4, "writer");
            await userManager.AddToRoleAsync(writer5, "writer");
            await userManager.AddToRoleAsync(writer6, "writer");
            await userManager.AddToRoleAsync(writer7, "writer");
            await userManager.AddToRoleAsync(writer8, "writer");
            await userManager.AddToRoleAsync(writer9, "writer");
            await userManager.AddToRoleAsync(writer10, "writer");
            await userManager.AddToRoleAsync(writer11, "writer");


            var users = await context.Users.ToListAsync();

            if (context.Categories.Count() == 0)
            {
                context.Categories.AddRange(
                    new List<Category>
                    {
                        new Category(){Name = "Sports"},
                        new Category(){Name = "Politics"},
                        new Category(){Name = "Technology"},
                        new Category(){Name = "Magazine"},
                        new Category(){Name = "Business"},
                        new Category(){Name = "Culture"},
                        new Category(){Name = "Travel"},
                    });
            };

            await context.SaveChangesAsync();

            var sportsCategory = await context.Categories.Where(x => x.Name == "Sports").FirstOrDefaultAsync();
            var politicsCategory = await context.Categories.Where(x => x.Name == "Politics").FirstOrDefaultAsync();
            var technologyCategory = await context.Categories.Where(x => x.Name == "Technology").FirstOrDefaultAsync();
            var magazineCategory = await context.Categories.Where(x => x.Name == "Magazine").FirstOrDefaultAsync();
            var businessCategory = await context.Categories.Where(x => x.Name == "Business").FirstOrDefaultAsync();
            var cultureCategory = await context.Categories.Where(x => x.Name == "Culture").FirstOrDefaultAsync();
            var travelCategory = await context.Categories.Where(x => x.Name == "Travel").FirstOrDefaultAsync();

            var userCategories = await context.UserCategories.ToListAsync();
            if (userCategories.Count == 0)
            {
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director1.Id, CategoryId = sportsCategory!.Id });
                director1.UserCategoryId = sportsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director2.Id, CategoryId = politicsCategory!.Id });
                director2.UserCategoryId = politicsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director3.Id, CategoryId = sportsCategory!.Id });
                director3.UserCategoryId = sportsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director4.Id, CategoryId = magazineCategory!.Id });
                director4.UserCategoryId = magazineCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director5.Id, CategoryId = businessCategory!.Id });
                director5.UserCategoryId = businessCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director6.Id, CategoryId = cultureCategory!.Id });
                director6.UserCategoryId = cultureCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = director7.Id, CategoryId = travelCategory!.Id });
                director7.UserCategoryId = travelCategory.Id;


                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer1.Id, CategoryId = sportsCategory!.Id });
                writer1.UserCategoryId = sportsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer10.Id, CategoryId = sportsCategory!.Id });
                writer10.UserCategoryId = sportsCategory.Id;

                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer2.Id, CategoryId = politicsCategory!.Id });
                writer2.UserCategoryId = politicsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer8.Id, CategoryId = politicsCategory!.Id });
                writer8.UserCategoryId = politicsCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer11.Id, CategoryId = politicsCategory!.Id });
                writer11.UserCategoryId = politicsCategory.Id;

                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer3.Id, CategoryId = technologyCategory!.Id });
                writer3.UserCategoryId = technologyCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer9.Id, CategoryId = technologyCategory!.Id });
                writer9.UserCategoryId = technologyCategory.Id;

                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer4.Id, CategoryId = magazineCategory!.Id });
                writer4.UserCategoryId = magazineCategory.Id;

                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer5.Id, CategoryId = businessCategory!.Id });
                writer5.UserCategoryId = businessCategory.Id;

                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer6.Id, CategoryId = cultureCategory!.Id });
                writer6.UserCategoryId = cultureCategory.Id;
                await context.UserCategories.AddAsync(new AppUserCategory() 
                { UserId = writer7.Id, CategoryId = travelCategory!.Id });
                writer7.UserCategoryId = travelCategory.Id;

            }

            var categories = context.Categories.ToList();

            if (context.Posts.Count() == 0)
            {
                await context.Posts.AddRangeAsync(
                    new List<Post>
                    {
                        #region Sports Posts
                        new Post(){
                            Title ="Bournemouth 4-3 Luton: Cherries in 20-year high with 'unreal' Premier League comeback",
                            Content = "On a night when all eyes were on the Champions League, the Premier League witnessed its biggest comeback win in more than 20 years.\r\n\r\nAway from the television cameras and the drama in Madrid and Dortmund, Bournemouth and Luton played out one of the most extraordinary Premier League matches of this or any recent season.\r\n\r\nBournemouth won 4-3, having been three goals down at the break.\r\n\r\nDominic Solanke's exquisite turn and finish started the comeback five minutes after half-time, and after Illia Zabarnyi bundled in a header for 2-3, Antoine Semenyo completed the comeback with a pair of powerful finishes - the second with only six minutes remaining.\r\n\r\n\"It's unreal,\" Semenyo told BBC Match of the Day. \"It is an achievement of mine just playing in the Premier League so to get a winning goal for the team, I'm buzzing.\"\r\n\r\nIt meant Bournemouth became only the fifth team in Premier League history to win a match in which they trailed by three goals, and just the third to do so in a game where they were 3-0 down at half-time.\r\n\r\nThe others to achieve this feat were Manchester United in beating Spurs 5-3 in September 2001 and Wolves v Leicester in October 2003.\r\n\r\nAccording to Semenyo, there was no half-time tirade from Bournemouth manager Andoni Iraola at half-time. In fact he seemed to say very little to his team, as the Cherries were out for the second half early - so early in fact that it caught the forward off guard.\r\n\r\n\"I was on the bike actually when everyone was running out so I had to scurry out quickly. It was because we were ready to go and put a performance on for the fans and for ourselves.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/FBEA/production/_132909446_gettyimages-2081879652.jpg",
                            Creator = writer1,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="'Nadal's success made Roland Garros feel like the Bernabeu'",
                            Content ="The 'unbelievable' numbers behind Nadal's success\r\nNo player in tennis history has been as synonymous with a tournament as Nadal has been with the French Open.\r\n\r\nThe Spanish left-hander’s dominance at Roland Garros is why he is known as the 'King of Clay'.\r\n\r\nThe facts and figures - supplied by OptaAce and the International Tennis Federation - clearly illustrate his dominance:\r\n\r\nNadal is the only player in history to win 14 singles titles at one Grand Slam tournament\r\n\r\nNadal has won 112 of his 115 matches there – the highest winning percentage of any singles player at a major\r\n\r\nNadal is the only player to win five successive titles at Roland Garros (2010-2014)\r\n\r\nNadal is the only man to win four Grand Slam titles without dropping a set (2008, 2010, 2017 and 2020)\r\n\r\nNadal has only ever lost to two men – Robin Soderling (2009 fourth round) and Novak Djokovic (2015 quarter-finals, 2021 semi-finals)\r\n\r\nFive of Nadal’s victories in Paris are among the top 20 most dominant Grand Slam men's singles title runs by games dropped\r\n\r\nNadal won the first 16 of his 22 Grand Slams under the tutelage of his uncle Toni, who watched him hit a tennis ball for the first time aged three and coached him until 2017.\r\n\r\n\"When Rafael was young I tried to teach him how important it was to improve every day, every single match and every single tournament. We weren't thinking Roland Garros was more important than Wimbledon or the US Open,\" Toni Nadal told BBC Sport.\r\n\r\n\"I grew up watching Bjorn Borg, I could see how good Borg was on clay and saw him win Roland Garros six times.\r\n\r\n\"For me that was unbelievable - six times. Years later I saw my nephew win it 14 times. It is incredible what we have seen him do.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/cddb/live/98e0c3a0-0e0f-11ef-9e85-378926d0f2ef.png.webp",
                            Creator = writer10,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Korda's dominance 'great' for women's golf - Hall",
                            Content ="When Nelly Korda pulled out of an LPGA Tour event a couple of weeks ago, she received a text message that said \"thanks for giving us a chance\".\r\n\r\nKorda has won six out of her past seven events, including last month's major, the Chevron Championship.\r\n\r\nThe 25-year-old tops the world rankings with double the points of her nearest rival, fellow American Lilia Vu.\r\n\r\nThe tongue-in-cheek words were sent to Korda by Georgia Hall, the English number two, who says her friend's dominance in 2024 is just what the women's game needs.\r\n\r\n\"In a way I hope it continues like that because it's just amazing to see,\" said the world number 32.\r\n\r\n\"I just can't get my head around it. Six out of seven is just unbelievable.\r\n\r\n\"It's great for the game in general. Although we obviously want to win more than anything, for her to keep winning like she is, it's just doing great things for us, promoting the tournaments and the Tour in general.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/a59f/live/c4f72be0-1991-11ef-b6ee-bdb02dc7c7a5.jpg.webp",
                            Creator = writer10,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="No England call, but has Henderson's Ajax move worked out?",
                            Content ="First it was disbelief, but that feeling gradually gave way to a genuine hope among Ajax fans when they heard their club were attempting to sign Jordan Henderson.\r\n\r\nIt was reminiscent of summer 2014, when the Amsterdam side were linked to Samuel Eto'o.\r\n\r\nBut where the Cameroon striker signed for Everton, this time the rumours became reality when Ajax announced ex-Liverpool midfielder Henderson's arrival on a two-and-a-half-year contract in January.\r\n\r\nThe 33-year-old was available after leaving Al-Ettifaq, bringing a premature end to his much-debated stay in Saudi Arabia.\r\n\r\nIn a transfer window where there was little excitement otherwise, this was one of the big stories - both in the Netherlands and abroad.\r\n\r\nAjax really needed some experience - many older players left the club in recent years with mostly youngsters replacing them - and Henderson needed to be playing competitive football to stand a chance of making England's Euro 2024 squad.\r\n\r\nA difficult start to the season remarkably saw Ajax bottom of the table for a short while. Perhaps even worse was their elimination from the Dutch Cup by amateur side Hercules.\r\n\r\nAt that point a managerial change had already taken place, with Maurice Steijn leaving the club after only four months in charge.\r\n\r\nBehind the scenes, director of football Sven Mislintat was sacked.\r\n\r\nThe former Arsenal head of recruitment encountered much resistance with his policy of signing young players who were thrown in at the deep end, with experience clearly lacking.\r\n\r\nJohn van 't Schip took over as interim manager, and with the former player in charge things started to look brighter.\r\n\r\nHenderson was keen to buy into the project as well.\r\n\r\n\"I'll eat, breathe and sleep Ajax and dedicate myself every single day, every single session to try to be better and help this team and this club be better,\" he said during his unveiling.\r\n\r\nHis commitment to the cause could be seen in those early weeks, travelling on his own to an away game at Heracles in which he was not involved to support his new team-mates.\r\n\r\nA week later, Henderson made his much-anticipated debut for the club in a 1-1 draw against league leaders - and eventual champions - PSV.\r\n\r\nHe got praise for his calmness and coaching of the team, while also creating a few opportunities.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/1e3f/live/2a1ca570-1877-11ef-abbd-8dabb36280e8.jpg.webp",
                            Creator = writer10,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Ben White: Arsenal defender signs new long-term contract",
                            Content = "Arsenal defender Ben White has signed a new four-year contract with the club.\r\n\r\nThe 26-year-old joined the Gunners for a fee of £50m in 2021 and is a key part of Mikel Arteta's side, playing at right-back and centre-back.\r\n\r\nHe played in midweek as Arsenal reached the quarter-finals of the Champions League for the first time in 14 years by beating Porto.\r\n\r\nSince joining from Brighton, he has made 97 Premier League appearances, scoring four goals.\r\n\r\nHe has played 27 times in the league for Arteta's side this season with the Gunners top of the table, above Liverpool on goal difference.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/0BCE/production/_132922030_gettyimages-2080635293.jpg",
                            Creator = writer1,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="How important is FA Cup final for Ten Hag's future?",
                            Content ="Sir Jim Ratcliffe has had a positive start as Manchester United co-owner.\r\n\r\nHe has detailed his grand plan for a new stadium and said he wants to knock Manchester City off their perch.\r\n\r\nHe met fans groups in person and is appointing respected industry figures to key jobs.\r\n\r\nThe goodwill allowed him to make unpopular decisions within the club, including axing the traditional staff trips to cup finals, and delivering some harsh words to employees without the hysteria that would have accompanied the same moves if they had been made by the Glazer family.\r\n\r\nBut, as one fan representative observed in private a couple of months ago, \"this is the easy bit, it’s when he has to do something that the real judgement will come\".\r\n\r\nThat moment is approaching.\r\n\r\nUnited go into the FA Cup final with City knowing defeat will mean no European football next season for only the second time since 1981-82, excluding the five-year period when all English clubs were banned.\r\n\r\nThey finished eighth in the Premier League, one place below the David Moyes season of 2013-14, and their lowest since 1989-90.\r\n\r\nThat season was memorable for the FA Cup win that propelled United to two decades of dominance under Sir Alex Ferguson but, before that, the famous ‘Ta-ra Fergie’ banner during a home defeat by Crystal Palace in December that told the Scot it was time to go.\r\n\r\nIt was a different era, there were hardly any mobile phones let alone social media. But the pressure was huge. United’s board stood firm.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/976/cpsprodpb/1421/live/4f7a2cb0-1936-11ef-ba00-37bcc11aa4c3.jpg.webp",
                            Creator = writer1,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Sancho sparkles on important night for Dortmund",
                            Content = "Borussia Dortmund reached the Champions League quarter-finals for the first time in three years thanks to goals from Jadon Sancho and Marco Reus against PSV Eindhoven.Sancho's low driving shot from the edge of the area gave Dortmund the lead in the third minute.\r\n\r\nEdin Terzic's side had a second goal ruled out as Niclas Fullkrug was deemed to be offside in the build-up.\r\n\r\nSubstitute Reus pounced on a PSV mistake deep in stoppage time to finish one-on-one against away goalkeeper Walter Benitez and settled the tie.\r\n\r\n'No magic moments' - has Champions League become boring?\r\nChampions League draw to take place on Friday\r\nPSV had numerous opportunities to equalise, with Hirving Lozano particularly wasteful, and Peter Bosz's side - who are 10 points clear at the top of the Eredivisie - were punished for their squandering.\r\n\r\nLozano, brought on as a second-half substitute, slashed wide from close range, shot straight at home goalkeeper Gregor Kobel and also missed the target from the edge of the area.\r\n\r\nThe best chance for PSV came a minute before Dortmund's late second goal.\r\n\r\nLuuk de Jong got on the end of a one-two but he leaned back and fired over from close range as PSV's time in the Champions League came to a frustrating end.\r\n\r\nThe home side could have been ahead even earlier in the contest when Chelsea loanee Ian Maatsen's fierce strike was tipped over by Benitez, before Sancho set the tone soon after and his substitute Reus secured Dortmund's place in the last eight with his late finish.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/4254/production/_132908961_gettyimages-2081965236.jpg",
                            Creator = writer1,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Tom Lockyer: Luton Town captain thanks 'hero' medics who saved his life at Bournemouth",
                            Content = "Luton Town captain Tom Lockyer called the medics who saved his life \"heroes\" as he returned to Bournemouth to thank them.\r\n\r\nThe 29-year-old collapsed on the Vitality Stadium pitch after suffering cardiac arrest when the clubs met on 16 December.\r\n\r\nThat match was abandoned, and Lockyer attended Wednesday's rearranged fixture and met the medics before the game.\r\n\r\n\"I got a little bit emotional,\" Lockyer told BBC Radio 5 Live.\r\n\r\n\"I am quite numb to the whole thing but being back here, I came into the tunnel area and saw the paramedics that saved my life.\r\n\r\n\"I recognised them straight away. What do you say in that situation other than 'thank you'? They are heroes and they saved my life.\"\r\n\r\nLockyer shook the hands of the medics when he came on to the pitch before Wednesday's Premier League match and received a standing ovation from both sets of fans who chanted his name.\r\n\r\nSupporters also applauded in the 59th minute, which was when the defender collapsed in the original fixture.\r\n\r\n\"I hope that is for the paramedics and not myself,\" said Lockyer prior to the game.\r\n\r\n\"It is going to be emotional. When it happened it was important for people to see me doing well.\r\n\r\n\"A lot of that crowd that were there that day are here tonight so for them to see me walking around and doing well is important.\"\r\n\r\nLockyer has not played since his cardiac arrest, and has previously said he was \"technically dead\" for nearly three minutes.\r\n\r\nHe was hospitalised for five days and was subsequently fitted with an implantable cardioverter defibrillator.\r\n\r\nBournemouth fans raised more than £2,000 to put on four coaches for Luton fans to make the 240-mile round trip from Bedfordshire to the south coast for the rearranged game.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/0EC4/production/_132908730_hi092998422.jpg",
                            Creator = writer1,
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        #endregion
                        #region Politic Posts
                        //Politic News
                        new Post(){
                            Title ="Russia election: Stage-managed vote will give Putin another term",
                            Content = "As I walk around Borovsk, two things strike me about this town 60 miles (100km) from Moscow.\r\n\r\nFirst, there is almost no sign of the presidential election coming up this weekend.\r\n\r\nI see few election banners or billboards and no political flyers being handed out.\r\n\r\nNot surprising, really. The absence of election preparations mirrors the absence of drama surrounding a stage-managed event that will hand Vladimir Putin a fifth term in the Kremlin.\r\n\r\nThe other thing you can't help noticing in Borovsk is the street art. It's everywhere.Much of it has been created by street artist Vladimir Ovchinnikov. All over town his work stares down from walls and buildings.\r\n\r\nMost of his paintings are uncontroversial. Like the giant globe recounting the town's history. Or the image of a famous footballer.\r\n\r\nIncreasingly, though, when Vladimir paints a picture of today's Russia, it turns out very dark.\r\n\r\n\"I call this one Pinnacle of Ambition,\" the 86-year-old artist tells me. The painting he's showing me at home features a man in a martial arts uniform walking a tightrope over a mountain of human skulls.\r\n\r\n\"This is what the ambition of someone high up in power can lead to.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1536/cpsprodpb/DC0A/production/_132903365_gettyimages-2054049238.jpg.webp",
                            Creator = writer2,
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title = "Three more Israeli hostages' bodies found in Gaza",
                            Content = "The bodies of three more Israeli hostages have been recovered from Gaza, the Israel Defense Forces (IDF) have said.\r\nThey are those of Hanan Yablonka, Michel Nisenbaum and Orion Hernandez, it said in a statement.\r\nThe IDF said the bodies were recovered from the northern town of Jabalia overnight in a joint operation with Israel's domestic intelligence agency.\r\nIt comes one week after three other hostages' bodies were retrieved from Gaza.\r\nThe dead hostages were among 252 people who were taken captive when Hamas gunmen attacked Israel on 7 October, killing about 1,200 people.\r\nThere are about 130 still held in Gaza, Israel says.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1536/cpsprodpb/0d74/live/c57a0060-19a5-11ef-baa7-25d483663b8e.jpg.webp",
                            Creator = writer8,
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title = "What do China's drills around Taiwan tell us?",
                            Content = "It's day two of China's military drills in the skies and seas around Taiwan - and the latest exercises show how the People's Liberation Army would surround the island on all sides by ships and aircraft.\r\nIt's a rehearsal of how Beijing would take the island, as it has vowed to do repeatedly.\r\nChina has already set a new normal in the Taiwan Strait, steadily ramping up miliatry pressure on the self-governed island.\r\nSo what is different about these drills - and what do they tell us?\r\nPropaganda vs reality\r\nIt’s hard to tell exactly what is going on, but from what Beijing has made public, the areas covered by these drills are perhaps the biggest we’ve seen yet, and include much of the Taiwan Strait, the Bashi Channel (which separates Taiwan from the Philippines) and large swathes of the Pacific along Taiwan’s east coast.\r\nThursday's drills focused on encircling the island, simulating a full-scale attack, minus the actual landing of troops, Taiwan military expert Chieh Chung says.\r\nHe thinks the inclusion of all of Taiwan’s off-shore islands demonstrates China’s plan to eliminate facilities that could launch a counter-attack against the PLA.\r\nHe also thinks this two-day drill will not be the last Taiwan will have to endure this year – hence the name “Joint Sword 2024-A”. Friday's footage shows the People's Liberation Army (PLA) preparing for a mock strike on Taiwan’s main cities and ports.\r\nA dramatic video released by the PLA’s Eastern Command, shows fleets of ships approaching Taiwan, accompanied by the words “Push in!” “Encircle!” “Lock!” .\r\nThe whole of Taiwan is then highlighted in orange – presumably indicating total control.\r\nChina has also released a video of a PLA colonel explaining the purpose of the drills in politically-charged language: \"As we can see we have set two exercise areas in the sea and air space near the eastern part of the island, mainly to block the escape of 'Taiwan independence' separatists and break through their comfort zone,\" he says.\r\nThe island's eastern coast hosts crucial military infrastructure and because of its proxomity to Japan's southern islands, is also a reliable resupply route for Taiwan's allies, including the US.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/ddad/live/10802f80-19a1-11ef-8810-7ddce6503835.jpg.webp",
                            Creator = writer11,
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Denmark to start conscripting women for military service",
                            Content = "Denmark has announced plans to extend military conscription to women for the first time and increase the standard service time.\r\n\r\nIt also wants to boost its defence budget by nearly $6bn (£4.6bn) in the next five years to meet Nato targets.\r\n\r\n\"We do not rearm because we want war. We are rearming because we want to avoid it,\" said PM Metter Frederiksen.\r\n\r\nTensions in Europe have spiked since Russia's full-scale invasion of Ukraine in February 2022.\r\n\r\nUnveiling the reforms on Wednesday, Ms Frederiksen said the government was seeking to achieve \"full equality between the sexes\".\r\n\r\nMeanwhile, Defence Minister Troels Lund Poulsen said: \"More robust conscription, including full gender equality, must contribute to solving defence challenges, national mobilisation and manning our armed forces.\"\r\n\r\nWomen in the Scandinavian country can already volunteer for military service.\r\n\r\n\r\nNow the government plans to introduce female conscription from 2026, making it only the third European nation - alongside Norway and Sweden - to require women to serve in the armed forces.\r\n\r\nIt also says the conscription service will be extended from four to 11 months for both men and women.\r\n\r\nLast year, 4,700 people served military service, of which about 25% were women. This number will be increased to 5,000 per year.\r\n\r\nDenmark's armed forces currently number about 20,000 active personnel, including some 9,000 professional troops.\r\n\r\nThe country, which has a total population of nearly six million, is also raising its military spending from the current 1.4% of GDP to 2% to meet targets set by the Nato military alliance.\r\n\r\nLast year, lawmakers voted to abolish a springtime public holiday to boost spending on the military.\r\n\r\nDenmark has been one of the staunchest supporters of Ukraine, providing it with advanced weapons and funds, and also training Ukrainian pilots on US-made F-16 war planes.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/E7F4/production/_132908395_denmark_soldiers_getty.jpg.webp",
                            Creator = writer2,
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Jacob Zuma - the political wildcard in South Africa's election",
                            Content = "Despite being a disgraced former president who was sent to jail, Jacob Zuma is turning out to be the political wildcard in South Africa's election campaign.\r\n\r\nThis follows his dramatic decision to ditch the governing African National Congress (ANC) for the newly formed party uMkhonto we Sizwe, meaning Spear of the Nation.\r\n\r\nThe 81-year-old is leading its campaign in the 29 May general election, urging people to turn their backs on the ANC led by his successor, President Cyril Ramaphosa.\r\n\r\n\"Zuma is, as ever, playing a mischievous hand,\" political analyst Richard Calland told the BBC.\r\n\r\n\"He doesn't want power, but leverage in the ANC. He wants to dethrone Ramaphosa for a more pliable leader,\" he said.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/F398/production/_132906326_gettyimages-2002504299_976.jpg.webp",
                            Creator = writer11,
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        #endregion
                        #region Technology
                        //Technology
                        new Post(){
                            Title ="AI and drones in £800m Budget technology package",
                            Content = "The Budget will contain an £800m package of technology reforms aimed at freeing up NHS and police time, the Treasury has announced.\r\n\r\nChancellor Jeremy Hunt said ahead of the 6 March announcement that there was \"too much waste in the system\".\r\n\r\nAs part of the reforms, AI will be used to cut NHS scan times by a third and the police will deploy drones to incidents such as traffic collisions.\r\n\r\nLabour said the package amounted to \"spin without substance\".\r\n\r\nElsewhere, Mr Hunt also hinted that civil service staff numbers could be cuts by tens of thousands.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/4677/production/_132793081_93e8b0396b7b29788b4ae34c5c192039eef7fd3e-1.jpg.webp",
                            Creator = writer3,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Google to make Pixel phones and drones in India",
                            Content = "Tech giant Google will soon begin making its Pixel smartphones in India, sources aware of the development have told the BBC.\r\nGoogle is set to manufacture the phones at an existing Foxconn facility in the southern state of Tamil Nadu.\r\nThe firm is also set to independently manufacture drones in the state.\r\nIndia has emerged as a key destination for global firms looking to diversify supply lines outside China in the midst of geopolitical tensions with the West.\r\nLast year, Google had announced plans to make Pixel smartphones in India, beginning with the Pixel 8.\r\n\"India is a priority market for Pixel smartphones, and we’re committed to bringing the best of our hardware and underlying built-in software capabilities to people across the country,\" it said in a blog post.\r\nOn Friday, sources told the BBC that Alphabet's Google would make advanced versions of Pixel smartphones at the Tamil Nadu facility and that manufacturing would begin within this calendar year.\r\nGoogle and Foxconn have signed a contract to this end, the source said.\r\nFoxconn currently has two facilities in Tamil Nadu. At one of its facilities near Chennai city, it assembles Apple's iPhones.\r\nGoogle's decision to make Pixel phones in Tamil Nadu came after state officials met company executives recently.\r\nAccording to a statement from the Tamil Nadu government, officials from Google are also set to meet state Chief Minister MK Stalin in Chennai soon.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/bba8/live/a8639380-199e-11ef-8810-7ddce6503835.jpg.webp",
                            Creator = writer9,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Robotic arm development aims to help stroke patients",
                            Content = "Mr Greig said the technology is \"lightweight and cheap\" and uses material which is comfortable for the patient to wear.\r\nHe told BBC Scotland News he had \"no knowledge\" of robotics prior to the project.\r\nHe said: \"The first challenge was designing something big enough and strong enough to move the elbow which takes considerable force.\r\n\"The user can go home and get therapy without being one-on-one.\r\n\"Physiotherapists could also use this with a class of patients, so that they can work with multiple people at the one time.\"\r\nRead more stories from north east Scotland, Orkney and Shetland\r\nListen to the latest news from north east Scotland on BBC Sounds\r\nThe robotic arm is currently controlled using a laptop but further development could allow a phone or device on a desk to connect.\r\nIt tries to mimic the same movements that physios will encourage during sessions.\r\nAir is pumped into the device from a small compressor into strips of material which inflate and press together to help move the limb and recreate something like a bicep curl.\r\nIt is not yet clear when the robotic arm will be ready to move onto the trial stage.\r\nMr Greig said: \"It is a medical device, so there’s a lot of testing and we need to do, but there’s no reason this can’t go to market.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/8a00/live/f7a44500-190a-11ef-876b-c77a1c45f857.jpg.webp",
                            Creator = writer3,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="New technology aims to tackle ghost buses",
                            Content = "New technology should reduce the number of “ghost buses” which show up on digital timetables after being cancelled, transport bosses said.\r\n\r\nBus operators said they were working to speed up the reporting of cancelled services after commuters were left waiting for buses that never turned up.\r\n\r\nThe West Yorkshire Combined Authority transport committee launched a working group to look at the problem in January 2023 following complaints from passengers.\r\n\r\nA report to Monday's committee meeting said an upgrade of electronic ticket machines to 4G mobile technology should improve the tracking of vehicles.\r\n\r\nSpeaking at the meeting, councillor Oliver Edwards asked bus operators Transdev and First for a commitment that future cancellations would be reported immediately.\r\n\r\nMr Edwards, who represents the Guiseley and Rawdon ward on Leeds City Council for Labour, said: “It’s a big issue in the area I represent and it’s an issue in many wards, especially outside of the city centre.\r\n\r\n“It can be very serious for people if they don’t know whether the bus is coming.”\r\n\r\nMr Edwards also criticised First for not sharing bus performance data, the Local Democracy Reporting Service said.\r\n\r\nHe said: “When we’ve asked for data, we’ve either been told it will be provided, but several months later we are still waiting for that, or we’ve been told it can’t be shared because of commercial sensitivity.\r\n\r\n“What commercial sensitivity is it when there’s no competition on that route?\"\r\n\r\n'We do pretty well'\r\nBrandon Jones, head of external relations at First, said the reporting of cancellations was improving after computer software changes.\r\n\r\n“There are some issues around data flow,\" he told the meeting.\r\n\r\n\"We are very aware of these issues that occur for customers - the vast majority of the time it works well.”\r\n\r\nResponding to the criticism about performance data, he said: “We do provide a lot of data through the combined authority already and we have had conversations around opening that up. ”\r\n\r\nPaul Turner, Transdev commercial director, said cancellation systems were being upgraded at his company.\r\n\r\n“I think we do pretty well at getting our cancellations up to date,\" he said.\r\n\r\n“We certainly aren’t perfect at doing it, but rest assured we do keep on top of that and ensure that we are getting the processes right behind it.”",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1536/cpsprodpb/001a/live/f9c0c720-bedb-11ee-bbce-5750150baf3b.jpg.webp",
                            Creator = writer9,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Gloucestershire farmers offered vehicle tracking system",
                            Content = "A new Home Office-approved vehicle covert tracking service could help protect vulnerable landowners from rural crime in Gloucestershire.\r\n\r\nIndustry experts AX Innovation Limited demonstrated the system to landowners at Dumbleton near Tewkesbury on Tuesday.\r\n\r\nAX Innovation claim it has near 100% stolen vehicle recovery rate.\r\n\r\nSenior police staff say rural crime is making farmers and countryside communities fearful\r\n\r\nThe system, which uses GPS technology to alert owners if their property has been taken outside a designated area, is being offered to landowners at a reduced rate as part of the Home Office Safer Streets Fund.\r\n\r\nGloucestershire Police and Crime Commissioner (PCC) Chris Nelson said: \"The scale, cost, social impact and other effects of crime in rural areas are underestimated, under-reported and not fully understood.\r\n\r\n'Vulnerable and anxious'\r\n\"As a result, farming communities are facing fear and intimidation from groups of criminals.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/148C1/production/_119716148_hi053450690.jpg.webp",
                            Creator = writer9,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="US lawmakers quiz Musk's Starlink over Russia claims",
                            Content = "House Democrats are demanding answers from Elon Musk's SpaceX amid claims that its technology is being used by Russian forces in Ukraine.\r\n\r\nKyiv released intercepted communications last month indicating Russia had obtained Starlink terminals.\r\n\r\nMr Musk recently denied that any terminals - which are key to Ukraine's army operations - were sold to Russia.\r\n\r\nBut in a letter seen by the BBC, two senior Democrats said they were \"concerned\" by the recent allegations.\r\n\r\nIn the letter sent to SpaceX President Gwynne Shotwell on Wednesday, Reps Robert Garcia and Jamie Raskin wrote that it was \"alarming\" that Russian troops might have obtained Starlink technology in violation of US sanctions.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/B0D1/production/_132856254_soldierstarlink.jpg.webp",
                            Creator = writer3,
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        #endregion
                        #region Businness
                        //Businness
                        new Post(){
                            Title ="Boeing boss's $33m pay package approved",
                            Content = "Boeing shareholders have signed off on the plane-maker's plan to grant outgoing boss Dave Calhoun a 2023 pay package worth nearly $33m (£25m).\r\n\r\nA majority voted in favour of the plan, which had drawn criticism as the company grapples with a crisis sparked by the mid-air blowout of a panel on one of its planes in January.\r\n\r\nSuch pay votes, held at the company's annual meetings, are not binding.\r\n\r\nA breakdown of the vote was not immediately available.\r\n\r\nAhead of the meeting, at least one prominent shareholder advisory group had criticised the plan and it also drew attention from some investors who spoke at the event.\r\n\r\nThe firm's decision to keep outgoing boss Dave Calhoun on the company's board of directors had raised questions as well, but was also approved.\r\n\r\nMr Calhoun's compensation package included salary of $1.4m (£1.1m) and stock awards worth about $30m (£23m) when granted.\r\n\r\nThat compared to a package of roughly $22.6m (£17m) in 2022.\r\n\r\nIn a question-and answer session after the vote, the firm was asked how the compensation for Mr Calhoun and others were \"justified\" given the severe challenges now facing the company.\r\n\r\nNew board chairman Steve Mollenkopf said the board had reduced some 2024 awards for executives after the accident and moved swiftly to overhaul the design of its pay incentives.\r\n\r\nThat included giving product safety the primary weight in determining performance, instead of financial factors - such as cash flow and share price - as had been the case previously, he said.\r\n\r\nBut both he and Mr Calhoun acknowledged the strains facing the company, some of which Mr Calhoun described as \"potentially existential\" in nature.\r\n\r\nThe Alaska Airlines incident revived questions about the firm's manufacturing and safety procedures and has spawned numerous investigations and lawsuits.\r\n\r\nJust days ago, the US Department of Justice (DOJ) said it was considering whether to prosecute Boeing over deadly crashes involving its 737 Max aircraft in 2018 and 2019, after determining it had breached a deal that shielded it from criminal charges.\r\n\r\nIn March, Boeing said Mr Calhoun would step down by the end of the year.\r\n\r\nThe search for his replacement is a key focus of the company now, Mr Mollenkopf said.\r\n\r\n\"The months and years ahead are critically important for our company as we take the necessary steps to regain the trust lost in recent times,\" he said.\r\n\r\nIn putting the pay package to shareholders earlier this year, the company praised how Mr Calhoun had steered the company through challenges such as Covid since 2020.\r\n\r\nIt said he had responded to the Alaska Airlines blowout \"in the right way\".\r\n\r\n\"The 737 MAX accidents and COVID have combined to create tremendous stress on the Company’s manufacturing operations and supply chain,\" it said.\r\n\r\n\"However, the Board believes that Mr. Calhoun’s primary focus on safety, quality and transparency is exactly what Boeing has needed, and continues to need.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/74fe/live/67c9f780-1460-11ef-a5f9-c9e97f2e93cf.jpg.webp",
                            Creator = writer5,
                            Category =  categories.Where(x => x.Name == "Business").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Cuba laments collapse of iconic sugar industry",
                            Content="The men of the Yumuri sugar co-operative in Cuba have worked the cane fields around the city of Cienfuegos since they were old enough to wield a machete.\r\n\r\nCutting cane is all Miguel Guzmán has ever known. He comes from a family of farm hands and started the tough, thankless work as a teenager.\r\n\r\nFor hundreds of years, sugar was the mainstay of the Cuban economy. It was not just the island's main export but also the cornerstone of another national industry, rum.\r\n\r\nOlder Cubans remember when the island was essentially built on the backs of families like Mr Guzmán's.\r\n\r\nToday, though, he readily admits he has never seen the sugar industry as broken and depressed as it is now - not even when the Soviet Union's lucrative sugar quotas dried up after the Cold War.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/3A6B/production/_133255941_peloton.jpg.webp",
                            Creator = writer5,
                            Category =  categories.Where(x => x.Name == "Business").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Musk opposes US tariffs on Chinese electric cars",
                            Content="Tesla boss Elon Musk says he opposes US tariffs on Chinese electric vehicles (EVs), just days after President Joe Biden quadrupled levies on EVs imported from China.\r\n\"Neither Tesla nor I asked for these tariffs\", the multi-billionaire told a technology conference in Paris via video link.\r\nMr Musk's comments are at odds with a warning he made in January that Chinese car makers would \"demolish\" competitors from other countries if there were no trade barriers.\r\nLast week, the White House said new measures, including a 100% tariff on EVs from China, were a response to unfair policies and intended to protect US jobs.\r\n\"In fact, I was surprised when they were announced. Things that inhibit freedom of exchange or distort the market are not good,\" Mr Musk said on Thursday.\r\n“Tesla competes quite well in the market in China with no tariffs and no deferential support. I’m in favour of no tariffs,\" he added.\r\nMr Biden has maintained a number of tariffs on China that were introduced by his predecessor Donald Trump, while increasing trade pressure on Beijing.\r\nLast week, Mr Biden vowed to not let China \"unfairly control the market\" for electric vehicles and other key goods, including batteries, computer chips and basic medical supplies.\r\nChina said it was opposed to the tariff hikes and would take retaliatory measures.\r\nThis week, China launched an anti-dumping probe into imports of a widely used plastic from the US, EU, Taiwan and Japan.\r\nThe announcement from the Ministry of Commerce that it will investigate imports of polyoxymethylene copolymer - which is used in electronics and cars - was seen as a signal that China will hit back in its trade disputes with the US and Europe.\r\nAlso this week, China signalled it could hit cars with large engines imported from the EU and US with tariffs of as much as 25%.\r\nThe China Chamber of Commerce to the EU said it had been told about the potential move by what it called \"insiders\".\r\nThe European Commission (EC), which oversees the EU's trade policies, has given itself a 4 July deadline to decide whether to impose measures against imports of Chinese-made EVs.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/200a/live/29096530-197e-11ef-b109-d7399a006ab0.jpg.webp",
                            Creator = writer5,
                            Category =  categories.Where(x => x.Name == "Business").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Mike Lynch defends himself in Autonomy fraud trial",
                            Content="British tech magnate Mike Lynch has appeared in court in the US to rebut claims that he committed fraud to inflate the value of his company ahead of its sale to Hewlett-Packard in 2011.\r\nHe told the jury that he was uninvolved with the transactions that had been described in the trial, reportedly describing watching the proceedings as \"surreal\".\r\nMr Lynch co-founded the software firm Autonomy in 1996, which grew to be one of the UK's biggest companies.\r\nAn entrepreneur who once drew comparisons to Bill Gates and Steve Jobs, he now faces up to 25 years in prison if convicted.\r\nProsecutors in San Francisco have spent weeks questioning witnesses, aiming to convince the jury of their case.\r\nThey claim that Mr Lynch was the \"driving force\" behind a scheme, using back-dated contracts and other manoeuvres to inflate the value of the firm, which ultimately sold for more than $11bn (£8.6bn).\r\nAt the time, the deal ranked as the largest-ever takeover of a British technology business.\r\nJust a year later, HP wrote down the value of Autonomy by $8.8bn.\r\nHP claimed it had been duped into overpaying for the company, which was known for software that could extract useful information from \"unstructured\" sources such as phone calls, emails or video.\r\nMr Lynch's team has argued that HP did not properly vet the deal.\r\nIt has also worked to distance Mr Lynch from other executives, including its former chief financial office, who was already successfully prosecuted for fraud.\r\nUnder questioning from his lawyer on Thursday, Mr Lynch said his focus at the firm was technology and marketing and he left others in charge of the numbers.\r\nHe made the case that business was complex, saying: \"If you take a microscope to a spotless kitchen you’ll find bacteria.\"\r\nMr Lynch, a former UK government adviser who sat on the boards of the BBC and the British Library, vigorously fought attempts to bring him to trial in the US.\r\nHe was eventually extradited after a UK judge ruled in favour of HP in a similar civil fraud case in 2022. HP is seeking a reported $4bn in that case.\r\nAs well as Mr Lynch, Autonomy's former finance executive Stephen Chamberlain is also on trial.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/3073/live/7423a870-1940-11ef-965b-f782a3c6e70b.jpg.webp",
                            Creator = writer5,
                            Category =  categories.Where(x => x.Name == "Business").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        #endregion
                        #region Culture
                        //Culture
                        new Post(){
                            Title ="Sean 'Diddy' Combs: Video appears to show rap mogul beating girlfriend Cassie in 2016",
                            Content="CCTV has emerged appearing to show rap mogul Sean \"Diddy\" Combs attacking singer Casandra \"Cassie\" Ventura in a hotel hallway in 2016.\r\n\r\nThe video, aired by CNN, has surfaced in the wake of allegations by Ms Ventura about her ex-boyfriend and producer in a lawsuit last year.\r\n\r\nShe has not commented, but her lawyer said it confirms \"the disturbing and predatory behaviour of Mr Combs\".\r\n\r\nA lawyer for Mr Combs has not responded to a BBC request for comment.\r\n\r\nThe BBC has not independently verified the video, which appears to be a compilation of surveillance footage angles dated 5 March 2016.\r\n\r\n\r\nAccording to CNN, it was filmed at the now-closed InterContinental Hotel in Century City, Los Angeles.\r\n\r\nThe clip appears to show Ms Ventura leaving a hotel room to walk towards a row of elevators.\r\n\r\nA shirtless man is seen holding a towel around his waist, and hurrying down a hallway.\r\n\r\nCassie settles legal case accusing Diddy of rape\r\nWhat we know about the accusations against Diddy\r\nWhen he catches up to her, he grabs her and throws her on the floor, causing her to drop some luggage.\r\n\r\n\r\nHe kicks her while she is on the ground, before picking up her bags and kicking her a second time then attempting to drag her by her shirt.\r\n\r\nThe attacker is seen leaving for a moment, before returning and shoving Ms Ventura as she stands up. He then sits in a chair near the lifts and throws an object.\r\n\r\nA lawyer for R&B singer Ms Ventura, Douglas Wigdor, said in a statement: \"The gut-wrenching video has only further confirmed the disturbing and predatory behaviour of Mr Combs.\r\n\r\n\"Words cannot express the courage and fortitude that Ms Ventura has shown in coming forward to bring this to light.\"\r\n\r\nThe Los Angeles District Attorney's Office said on Friday that the assault captured in the video might be too old to prosecute.\r\n\r\n\r\n\"We find the images extremely disturbing and difficult to watch,\" it said in a statement.\r\n\r\n\"If the conduct depicted occurred in 2016, unfortunately we would be unable to charge as the conduct would have occurred beyond the timeline where a crime of assault can be prosecuted.\"\r\n\r\nIn a now-settled federal lawsuit last year, Ms Ventura alleged that \"around March 2016\" Mr Combs \"became extremely intoxicated and punched Ms Ventura in the face, giving her a black eye\".\r\n\r\n\"After he fell asleep, Ms Ventura tried to leave the hotel room, but as she exited, Mr Combs awoke and began screaming at Ms Ventura.\r\n\r\n\"He followed her into the hallway of the hotel while yelling at her. He grabbed at her, and then took glass vases in the hallway and threw them at her, causing glass to crash around them as she ran to the elevator to escape,\" the documents said.\r\n\r\n\r\nThe lawsuit alleged that the rap mogul had purchased the footage from the hotel for $50,000 (£39,000).\r\n\r\nHer legal action against Mr Combs, which accused him of rape and sexual trafficking over a decade, was settled for an undisclosed sum one day after it was filed in November last year.\r\n\r\nMr Combs' lawyer, Benjamin Brafman, said at the time that the settlement \"is in no way an admission of wrongdoing\".\r\n\r\n\"Mr Combs' decision to settle the lawsuit does not in any way undermine his flat-out denial of the claims. He is happy they got to a mutual settlement and wishes Ms Ventura the best.\"\r\n\r\nSince then, several other women have filed lawsuits accusing the rapper of sexual misconduct.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/176BC/production/_133323959_gettyimages-514346660.jpg.webp",
                            Creator = writer6,
                            Category =  categories.Where(x => x.Name == "Culture").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Hit Me Hard And Soft: What makes Billie Eilish's records 'eco-friendly'?",
                            Content="Each year, the UK's vinyl habit is estimated to produce the same amount of emissions as 400 people.\r\n\r\nBut Billie Eilish is hoping to change the record with her new album Hit Me Hard and Soft, which came out on Friday.\r\n\r\nAlbums will be pressed on to recycled or eco-vinyl and the packaging will also be made from recycled materials.\r\n\r\nThere's scepticism about how much difference that can really make when it's linked to a huge world tour.\r\n\r\nBut Billie is keen not to be the Bad Guy, and has also been praised for drawing attention to sustainability in the music industry.\r\n\r\n\r\nIn an interview last month, the singer told Billboard she and her team were doing everything they could to minimise waste \"in every aspect\" of her music.\r\n\r\n\"My parents have always kept me well informed and hyper-aware that every choice we make and every action we take has an impact somewhere or on someone, good or bad, and that has always stuck with me,\" she said.\r\n\r\nAt a record press in South Wigston, Leicestershire, BBC Newsbeat was offered a behind-the-scenes glimpse of the process of making records more sustainable.\r\n\r\n\"Factories are so different to when I first started,\" says Karen Emanuel, CEO of vinyl manufacturers Key Production.\r\n\r\nMost important are the ingredients. Records are made from PVC, a type of plastic which takes centuries to decompose.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/128F5/production/_133312067_gettyimages-2066804427.jpg.webp",
                            Creator = writer6,
                            Category =  categories.Where(x => x.Name == "Culture").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        #endregion
                        #region Magazine
                        //Magazine
                        new Post(){
                            Title ="Who won the Kendrick Lamar v Drake beef?",
                            Content="\"Tryna strike a chord and it's probably A-minor.\"\r\n\r\nWith every second Kendrick Lamar holds on to the last letter of \"minor\" in Not Like Us, the inflammatory song about his fellow rapper Drake, his raspy vocals reverberate through hip-hop and popular culture.\r\n\r\nIt's an explosive allegation, made without evidence, that calls into question Drake's conduct with young women - an allegation now heard around the world. Drake, one of the world's biggest artists, vehemently denies it.\r\n\r\n Since its release on 4 May, Not Like Us has been dissected on social media, played at NBA basketball games and boomed from DJ booths at parties from London to Los Angeles; New York to Atlanta, piercing the public consciousness. \r\n\r\nAnd it is only one of nine songs that make up a mind-boggling, escalating conflict between two modern rap titans, involving unevidenced accusations of domestic violence, secret children and paedophilia - all denied.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/16237/production/_133297609_1_comp_drake_kendrick_lamar_getty.jpg.webp",
                            Creator = writer4,
                            Category =  categories.Where(x => x.Name == "Magazine").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                         new Post(){
                            Title ="My fake food was good enough for Barbie",
                            Content="An artist who creates fake food has been left \"stunned\" by the response since her work featured in the hit Barbie film last year.\r\n\r\nKerry Samantha Boyes saw her \"raspberry ripple ice creams\" take a staring role in the opening beach scenes of the Hollywood hit.\r\n\r\nShe makes high-quality and realistic sculptures of food for a range of uses from historic houses and museums to films and TV shows.\r\n\r\nSince launching her Fake Food Workshop business six years ago as a kitchen table start-up, it has grown rapidly.\r\n\r\nThe mother-of-three now has her own fake food store and studio set-up in south-west Scotland with an ever-growing list of illustrious clients all around the world.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/7c8b/live/9dd8c890-06fc-11ef-bb9e-7d83bdea22d0.jpg.webp",
                            Creator = writer4,
                            Category =  categories.Where(x => x.Name == "Magazine").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        #endregion
                        #region Travel
                        //Travel
                        new Post(){
                            Title ="Seven new Bridgerton filming locations you can visit",
                            Content="Bridgerton is back, and from London palaces to Bath ballrooms, here are some of the best places to experience Regency-era England.\r\n\r\nThe 16 May release of season three of Netflix's mega hit Bridgerton has inspired fans to do more than just watch the show's Regency-era high society seduction and scandal unfold. Fans of the series are increasingly booking trips to some the show's iconic filming locations, with the British rail- and bus-booking platform Trainline reporting that trips to Bridgerton-based destinations across England increasing by an average of 50% in the last year, and 135% year over year among American travellers.\r\n\r\nAccording to VisitBritain CEO Patricia Yates, Bridgerton's newest season presents an opportunity to \"promote [Britain's] world-renowned history and heritage and associated experiences to a global audience, inspiring visitors to come and see the filming locations and destinations for themselves.\"\r\n\r\nFrom London palaces to Bath ballrooms, here are seven places around England where fans can experience Bridgerton's lavish drama for themselves.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/images/ic/1024xn/p0hyrhq8.jpg.webp",
                            Creator = writer7,
                            Category =  categories.Where(x => x.Name == "Travel").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Tijuca National Park: The fight to rewild the world's largest urban forest",
                            Content="After the forest was decimated by colonial plantations, an ambitious rewilding programme is now aiming to ensure the survival of Rio de Janeiro's ancient rainforest.\r\n\r\nOne minute I'm choked by fumes under a concrete overpass. Minutes later, I'm soaked in sweat, sticky with the respiration of a forest. Now, that's not something you feel in every city – especially one with more than six million inhabitants.\r\n\r\nRio de Janeiro might be known for its beaches, football and Carnival, but few realise that it contains the world's largest urban forest: the Tijuca forest.\r\n\r\nTijuca is no typical forest. First protected in 1861 – a decade before the first national park in the United States – Tijuca forest is a 40 sq km chunk of Atlantic Forest, a once-vast biome that covered 1,000,000 sq km of Brazilian coastline. Today, roughly 15% of the Atlantic Forest remains, decimated by sugarcane and coffee plantations as well as logging by the European colonists that first stepped on Brazil's shores in the 16th Century.\r\n\r\nIn Rio de Janeiro, the loss of Atlantic Forest over the next 200 years was nearly a death knell for the young settlement. Rivers that quenched the city dried up and drought was imminent. Nineteenth-century Emperor Peter II had a solution: bring back the forest. So in the 1860s, farmers and city dwellers living on forest land were expropriated and enslaved Africans were ordered to plant more than 100,000 trees. However, they didn't bring back many of the animal species that once thrived in the forest.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/images/ic/1024xn/p0hy8h7g.jpg.webp",
                            Creator = writer7,
                            Category =  categories.Where(x => x.Name == "Travel").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Estonia's naked wellness tradition to cleanse both body and soul",
                            Content="Used for centuries by rural Estonians to heal their aches and pains, smoke saunas are a soulful experience that clears the mind and cleanses the spirit.\r\n\r\nIt's an uncommonly sunny March afternoon in Estonia but I'm in the dark cocoon of a smoke sauna, lying on a bench, completely naked. My feet are propped up on a sooty wooden beam and my head rests on a viht. This small bundle of thin oak branches is meant for lashing my bare body to slough off dead skin cells and boost circulation, but for the moment, it's a pillow. The dried leaves are pliant, though, after being soaked in water. Their earthy smell and the tang of smoke fill my nostrils. The air is damp, and beads of sweat cover my body.\r\n\r\nEda Veeroja, the owner of Mooska Smoke Sauna, is also naked. She drizzles water onto hot rocks piled on top of the brick stove. \"Olen tuul üle väljade… Sind hoian, hoian endas [I am the wind across the fields… I hold you, I hold you]\" she sings, the tune like a lullaby, the words hanging in the air like leil, the steam rising off the rocks.\r\n\r\nMooska is located in the south-eastern corner of Estonia, about 20km from the Russian border as the crow flies. It is part of Vana Võromaa, or Old Võromaa, which encompasses present-day Võru and Polva counties plus parts of Tartu and Valga counties. This remote region of rolling hills is the ancestral home of Võro, a Finno-Ugric language similar to Estonian with approximately 70,000 native speakers.\r\n\r\nVeeroja has spent the past eight hours preparing the sauna. During the six-hour heating process, she fed logs to the stove inside the steam room. As a smoke sauna has no chimney, the steam room filled with smoke, the hot air rising to the ceiling, leaving just enough clear air below to allow her to continue stoking the fire. Once the interior temperature climbed over 80C, she opened a small hatch in the ceiling, ventilating the sauna for two hours before we went in.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/images/ic/1024xn/p0hxwz5m.jpg.webp",
                            Creator = writer7,
                            Category =  categories.Where(x => x.Name == "Travel").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = false,
                        },
                        new Post(){
                            Title ="Tawaraya: Sleep like a shōgun-era samurai at Kyoto's finest ryokan",
                            Content="Run by the same family for the past 12 generations, this more-than-300-year-old inn is considered one of the finest in all of Japan.\r\n\r\nEvery Monday night for the past 10 weeks, I have been glued to my couch, drinking in every moment of the critically acclaimed samurai series Shōgun. \r\n\r\nThe show, based on James Clavell's 1975 bestselling novel about Japan's violent feudal past, details the tug-of-war between rival lords vying to be the shōgun, (supreme military ruler) of Japan in the years just before the Tokugawa shogunate (1603-1868). The limited series drew more than nine million viewers across its various streaming platforms in its first few weeks, making it one of the most-watched show of 2024 so far. \r\n\r\nAs I watched scenes of the translator Toda Mariko and English sailor John Blackthorne seated on tatami mats framed by shoji screens, peering out of their wood-panelled rooms towards a private garden, I was transported to my own travels in Japan – particularly my experience staying at Tawaraya. Considered the finest ryokan (traditional Japanese inn) in Kyoto – and one of the finest in Japan – Tawaraya opened in 1709 during the heart of the Tokugawa shogunate and once hosted the types of people the show depicts: samurai, daimyō feudal lords and members of the Tokugawa clan itself. Today, the inn has been operated by the same family for 12 generations spanning more than 300 years.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/images/ic/1024xn/p0hxwz5m.jpg.webp",
                            Creator = writer7,
                            Category =  categories.Where(x => x.Name == "Travel").FirstOrDefault(),
                            IsPrivateOnly = false,
                            IsPublished = true,
                        },
                        #endregion

                    }
                    );
            }

            await context.SaveChangesAsync();
        }


        private static async Task CreateUser(UserManager<AppUser> userManager, string username, string email, string password)
        {
            var isUserExist = userManager.FindByEmailAsync(email);
            if (isUserExist == null)
            {
                var newUser = new AppUser
                {
                    UserName = username,
                    Email = email,
                    CreatedDate = DateTime.Now
                };

                await userManager.CreateAsync(newUser, password);
            }
        }


        private static async Task CreateRole(RoleManager<AppRole> roleManager, string roleName)
        {
            var existingRole = await roleManager.RoleExistsAsync(roleName);

            if (!existingRole)
            {
                var newRole = new AppRole
                {
                    Name = roleName,

                };

                await roleManager.CreateAsync(newRole);
            }
        }

        private static async Task CreateNews(AppDbContext context, AppUser user, Category category, string postImage, string content, string title, bool published)
        {
            var ExistingNews = await context.Posts.Where(post => post.Title == title).FirstOrDefaultAsync();

            if (ExistingNews == null)
            {
                var createdNew = new Post
                {
                    CategoryId = category.Id,
                    CreatorId = user.Id,
                    Content = content,
                    Title = title,
                    Image = postImage,
                    IsPublished = published,
                    CreatedAt = DateTime.Now,
                };

                await context.Posts.AddAsync(createdNew);
                await context.SaveChangesAsync();
            }
        }


    }
}
