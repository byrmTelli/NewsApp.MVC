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
            await CreateRole(roleManager, "sports");
            await CreateRole(roleManager, "politic");
            await CreateRole(roleManager, "culture");
            await CreateRole(roleManager, "technology");
            await CreateRole(roleManager, "magazine");
            await CreateRole(roleManager, "subscriber");
            await CreateRole(roleManager, "writer");


            if (context.Users.Count() == 0)
            {
                await userManager.CreateAsync(new AppUser() { UserName = "admin",Name="Karen",Surname ="Perkson", Email = "adminuser@test.com", CreatedDate = DateTime.Now }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director1", Name="Jim",Surname="Luther", Email = "director1@test.com", CreatedDate = DateTime.Now }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "director2", Name = "Kaycee",Surname="Kerb", Email = "directory2@test.com", CreatedDate = DateTime.Now }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer1",Name="Anna",Surname="Druawehks", Email = "writer1@test.com", CreatedDate = DateTime.Now }, "Password123.");
                await userManager.CreateAsync(new AppUser() { UserName = "writer2",Name="Bayram",Surname="Telli", Email = "writer2@test.com", CreatedDate = DateTime.Now }, "Password123.");
            }
            var admin =await userManager.FindByEmailAsync("adminuser@test.com");
            await userManager.AddToRoleAsync(admin, "admin");
            var users = context.Users.ToList();

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


            var categories = context.Categories.ToList();

            if (context.Posts.Count() == 0)
            {
                context.Posts.AddRange(
                    new List<Post>
                    {
                        new Post(){
                            Title ="Bournemouth 4-3 Luton: Cherries in 20-year high with 'unreal' Premier League comeback",
                            Content = "On a night when all eyes were on the Champions League, the Premier League witnessed its biggest comeback win in more than 20 years.\r\n\r\nAway from the television cameras and the drama in Madrid and Dortmund, Bournemouth and Luton played out one of the most extraordinary Premier League matches of this or any recent season.\r\n\r\nBournemouth won 4-3, having been three goals down at the break.\r\n\r\nDominic Solanke's exquisite turn and finish started the comeback five minutes after half-time, and after Illia Zabarnyi bundled in a header for 2-3, Antoine Semenyo completed the comeback with a pair of powerful finishes - the second with only six minutes remaining.\r\n\r\n\"It's unreal,\" Semenyo told BBC Match of the Day. \"It is an achievement of mine just playing in the Premier League so to get a winning goal for the team, I'm buzzing.\"\r\n\r\nIt meant Bournemouth became only the fifth team in Premier League history to win a match in which they trailed by three goals, and just the third to do so in a game where they were 3-0 down at half-time.\r\n\r\nThe others to achieve this feat were Manchester United in beating Spurs 5-3 in September 2001 and Wolves v Leicester in October 2003.\r\n\r\nAccording to Semenyo, there was no half-time tirade from Bournemouth manager Andoni Iraola at half-time. In fact he seemed to say very little to his team, as the Cherries were out for the second half early - so early in fact that it caught the forward off guard.\r\n\r\n\"I was on the bike actually when everyone was running out so I had to scurry out quickly. It was because we were ready to go and put a performance on for the fans and for ourselves.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/FBEA/production/_132909446_gettyimages-2081879652.jpg",
                            Creator = users[0],
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Ben White: Arsenal defender signs new long-term contract",
                            Content = "Arsenal defender Ben White has signed a new four-year contract with the club.\r\n\r\nThe 26-year-old joined the Gunners for a fee of £50m in 2021 and is a key part of Mikel Arteta's side, playing at right-back and centre-back.\r\n\r\nHe played in midweek as Arsenal reached the quarter-finals of the Champions League for the first time in 14 years by beating Porto.\r\n\r\nSince joining from Brighton, he has made 97 Premier League appearances, scoring four goals.\r\n\r\nHe has played 27 times in the league for Arteta's side this season with the Gunners top of the table, above Liverpool on goal difference.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/0BCE/production/_132922030_gettyimages-2080635293.jpg",
                            Creator = users[1],
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Sancho sparkles on important night for Dortmund",
                            Content = "Borussia Dortmund reached the Champions League quarter-finals for the first time in three years thanks to goals from Jadon Sancho and Marco Reus against PSV Eindhoven.Sancho's low driving shot from the edge of the area gave Dortmund the lead in the third minute.\r\n\r\nEdin Terzic's side had a second goal ruled out as Niclas Fullkrug was deemed to be offside in the build-up.\r\n\r\nSubstitute Reus pounced on a PSV mistake deep in stoppage time to finish one-on-one against away goalkeeper Walter Benitez and settled the tie.\r\n\r\n'No magic moments' - has Champions League become boring?\r\nChampions League draw to take place on Friday\r\nPSV had numerous opportunities to equalise, with Hirving Lozano particularly wasteful, and Peter Bosz's side - who are 10 points clear at the top of the Eredivisie - were punished for their squandering.\r\n\r\nLozano, brought on as a second-half substitute, slashed wide from close range, shot straight at home goalkeeper Gregor Kobel and also missed the target from the edge of the area.\r\n\r\nThe best chance for PSV came a minute before Dortmund's late second goal.\r\n\r\nLuuk de Jong got on the end of a one-two but he leaned back and fired over from close range as PSV's time in the Champions League came to a frustrating end.\r\n\r\nThe home side could have been ahead even earlier in the contest when Chelsea loanee Ian Maatsen's fierce strike was tipped over by Benitez, before Sancho set the tone soon after and his substitute Reus secured Dortmund's place in the last eight with his late finish.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/4254/production/_132908961_gettyimages-2081965236.jpg",
                            Creator = users[2],
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Tom Lockyer: Luton Town captain thanks 'hero' medics who saved his life at Bournemouth",
                            Content = "Luton Town captain Tom Lockyer called the medics who saved his life \"heroes\" as he returned to Bournemouth to thank them.\r\n\r\nThe 29-year-old collapsed on the Vitality Stadium pitch after suffering cardiac arrest when the clubs met on 16 December.\r\n\r\nThat match was abandoned, and Lockyer attended Wednesday's rearranged fixture and met the medics before the game.\r\n\r\n\"I got a little bit emotional,\" Lockyer told BBC Radio 5 Live.\r\n\r\n\"I am quite numb to the whole thing but being back here, I came into the tunnel area and saw the paramedics that saved my life.\r\n\r\n\"I recognised them straight away. What do you say in that situation other than 'thank you'? They are heroes and they saved my life.\"\r\n\r\nLockyer shook the hands of the medics when he came on to the pitch before Wednesday's Premier League match and received a standing ovation from both sets of fans who chanted his name.\r\n\r\nSupporters also applauded in the 59th minute, which was when the defender collapsed in the original fixture.\r\n\r\n\"I hope that is for the paramedics and not myself,\" said Lockyer prior to the game.\r\n\r\n\"It is going to be emotional. When it happened it was important for people to see me doing well.\r\n\r\n\"A lot of that crowd that were there that day are here tonight so for them to see me walking around and doing well is important.\"\r\n\r\nLockyer has not played since his cardiac arrest, and has previously said he was \"technically dead\" for nearly three minutes.\r\n\r\nHe was hospitalised for five days and was subsequently fitted with an implantable cardioverter defibrillator.\r\n\r\nBournemouth fans raised more than £2,000 to put on four coaches for Luton fans to make the 240-mile round trip from Bedfordshire to the south coast for the rearranged game.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/onesport/cps/976/cpsprodpb/0EC4/production/_132908730_hi092998422.jpg",
                            Creator = users[0],
                            Category =  categories.Where(x => x.Name == "Sports").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        //Politic News
                        new Post(){
                            Title ="Russia election: Stage-managed vote will give Putin another term",
                            Content = "As I walk around Borovsk, two things strike me about this town 60 miles (100km) from Moscow.\r\n\r\nFirst, there is almost no sign of the presidential election coming up this weekend.\r\n\r\nI see few election banners or billboards and no political flyers being handed out.\r\n\r\nNot surprising, really. The absence of election preparations mirrors the absence of drama surrounding a stage-managed event that will hand Vladimir Putin a fifth term in the Kremlin.\r\n\r\nThe other thing you can't help noticing in Borovsk is the street art. It's everywhere.Much of it has been created by street artist Vladimir Ovchinnikov. All over town his work stares down from walls and buildings.\r\n\r\nMost of his paintings are uncontroversial. Like the giant globe recounting the town's history. Or the image of a famous footballer.\r\n\r\nIncreasingly, though, when Vladimir paints a picture of today's Russia, it turns out very dark.\r\n\r\n\"I call this one Pinnacle of Ambition,\" the 86-year-old artist tells me. The painting he's showing me at home features a man in a martial arts uniform walking a tightrope over a mountain of human skulls.\r\n\r\n\"This is what the ambition of someone high up in power can lead to.\"",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1536/cpsprodpb/DC0A/production/_132903365_gettyimages-2054049238.jpg.webp",
                            Creator = users[2],
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Denmark to start conscripting women for military service",
                            Content = "Denmark has announced plans to extend military conscription to women for the first time and increase the standard service time.\r\n\r\nIt also wants to boost its defence budget by nearly $6bn (£4.6bn) in the next five years to meet Nato targets.\r\n\r\n\"We do not rearm because we want war. We are rearming because we want to avoid it,\" said PM Metter Frederiksen.\r\n\r\nTensions in Europe have spiked since Russia's full-scale invasion of Ukraine in February 2022.\r\n\r\nUnveiling the reforms on Wednesday, Ms Frederiksen said the government was seeking to achieve \"full equality between the sexes\".\r\n\r\nMeanwhile, Defence Minister Troels Lund Poulsen said: \"More robust conscription, including full gender equality, must contribute to solving defence challenges, national mobilisation and manning our armed forces.\"\r\n\r\nWomen in the Scandinavian country can already volunteer for military service.\r\n\r\n\r\nNow the government plans to introduce female conscription from 2026, making it only the third European nation - alongside Norway and Sweden - to require women to serve in the armed forces.\r\n\r\nIt also says the conscription service will be extended from four to 11 months for both men and women.\r\n\r\nLast year, 4,700 people served military service, of which about 25% were women. This number will be increased to 5,000 per year.\r\n\r\nDenmark's armed forces currently number about 20,000 active personnel, including some 9,000 professional troops.\r\n\r\nThe country, which has a total population of nearly six million, is also raising its military spending from the current 1.4% of GDP to 2% to meet targets set by the Nato military alliance.\r\n\r\nLast year, lawmakers voted to abolish a springtime public holiday to boost spending on the military.\r\n\r\nDenmark has been one of the staunchest supporters of Ukraine, providing it with advanced weapons and funds, and also training Ukrainian pilots on US-made F-16 war planes.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/E7F4/production/_132908395_denmark_soldiers_getty.jpg.webp",
                            Creator = users[2],
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Jacob Zuma - the political wildcard in South Africa's election",
                            Content = "Despite being a disgraced former president who was sent to jail, Jacob Zuma is turning out to be the political wildcard in South Africa's election campaign.\r\n\r\nThis follows his dramatic decision to ditch the governing African National Congress (ANC) for the newly formed party uMkhonto we Sizwe, meaning Spear of the Nation.\r\n\r\nThe 81-year-old is leading its campaign in the 29 May general election, urging people to turn their backs on the ANC led by his successor, President Cyril Ramaphosa.\r\n\r\n\"Zuma is, as ever, playing a mischievous hand,\" political analyst Richard Calland told the BBC.\r\n\r\n\"He doesn't want power, but leverage in the ANC. He wants to dethrone Ramaphosa for a more pliable leader,\" he said.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/F398/production/_132906326_gettyimages-2002504299_976.jpg.webp",
                            Creator = users[2],
                            Category =  categories.Where(x => x.Name == "Politics").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="AI and drones in £800m Budget technology package",
                            Content = "The Budget will contain an £800m package of technology reforms aimed at freeing up NHS and police time, the Treasury has announced.\r\n\r\nChancellor Jeremy Hunt said ahead of the 6 March announcement that there was \"too much waste in the system\".\r\n\r\nAs part of the reforms, AI will be used to cut NHS scan times by a third and the police will deploy drones to incidents such as traffic collisions.\r\n\r\nLabour said the package amounted to \"spin without substance\".\r\n\r\nElsewhere, Mr Hunt also hinted that civil service staff numbers could be cuts by tens of thousands.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/4677/production/_132793081_93e8b0396b7b29788b4ae34c5c192039eef7fd3e-1.jpg.webp",
                            Creator = users[3],
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="New technology aims to tackle ghost buses",
                            Content = "New technology should reduce the number of “ghost buses” which show up on digital timetables after being cancelled, transport bosses said.\r\n\r\nBus operators said they were working to speed up the reporting of cancelled services after commuters were left waiting for buses that never turned up.\r\n\r\nThe West Yorkshire Combined Authority transport committee launched a working group to look at the problem in January 2023 following complaints from passengers.\r\n\r\nA report to Monday's committee meeting said an upgrade of electronic ticket machines to 4G mobile technology should improve the tracking of vehicles.\r\n\r\nSpeaking at the meeting, councillor Oliver Edwards asked bus operators Transdev and First for a commitment that future cancellations would be reported immediately.\r\n\r\nMr Edwards, who represents the Guiseley and Rawdon ward on Leeds City Council for Labour, said: “It’s a big issue in the area I represent and it’s an issue in many wards, especially outside of the city centre.\r\n\r\n“It can be very serious for people if they don’t know whether the bus is coming.”\r\n\r\nMr Edwards also criticised First for not sharing bus performance data, the Local Democracy Reporting Service said.\r\n\r\nHe said: “When we’ve asked for data, we’ve either been told it will be provided, but several months later we are still waiting for that, or we’ve been told it can’t be shared because of commercial sensitivity.\r\n\r\n“What commercial sensitivity is it when there’s no competition on that route?\"\r\n\r\n'We do pretty well'\r\nBrandon Jones, head of external relations at First, said the reporting of cancellations was improving after computer software changes.\r\n\r\n“There are some issues around data flow,\" he told the meeting.\r\n\r\n\"We are very aware of these issues that occur for customers - the vast majority of the time it works well.”\r\n\r\nResponding to the criticism about performance data, he said: “We do provide a lot of data through the combined authority already and we have had conversations around opening that up. ”\r\n\r\nPaul Turner, Transdev commercial director, said cancellation systems were being upgraded at his company.\r\n\r\n“I think we do pretty well at getting our cancellations up to date,\" he said.\r\n\r\n“We certainly aren’t perfect at doing it, but rest assured we do keep on top of that and ensure that we are getting the processes right behind it.”",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1536/cpsprodpb/001a/live/f9c0c720-bedb-11ee-bbce-5750150baf3b.jpg.webp",
                            Creator = users[3],
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="Gloucestershire farmers offered vehicle tracking system",
                            Content = "A new Home Office-approved vehicle covert tracking service could help protect vulnerable landowners from rural crime in Gloucestershire.\r\n\r\nIndustry experts AX Innovation Limited demonstrated the system to landowners at Dumbleton near Tewkesbury on Tuesday.\r\n\r\nAX Innovation claim it has near 100% stolen vehicle recovery rate.\r\n\r\nSenior police staff say rural crime is making farmers and countryside communities fearful\r\n\r\nThe system, which uses GPS technology to alert owners if their property has been taken outside a designated area, is being offered to landowners at a reduced rate as part of the Home Office Safer Streets Fund.\r\n\r\nGloucestershire Police and Crime Commissioner (PCC) Chris Nelson said: \"The scale, cost, social impact and other effects of crime in rural areas are underestimated, under-reported and not fully understood.\r\n\r\n'Vulnerable and anxious'\r\n\"As a result, farming communities are facing fear and intimidation from groups of criminals.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/148C1/production/_119716148_hi053450690.jpg.webp",
                            Creator = users[3],
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        },
                        new Post(){
                            Title ="US lawmakers quiz Musk's Starlink over Russia claims",
                            Content = "House Democrats are demanding answers from Elon Musk's SpaceX amid claims that its technology is being used by Russian forces in Ukraine.\r\n\r\nKyiv released intercepted communications last month indicating Russia had obtained Starlink terminals.\r\n\r\nMr Musk recently denied that any terminals - which are key to Ukraine's army operations - were sold to Russia.\r\n\r\nBut in a letter seen by the BBC, two senior Democrats said they were \"concerned\" by the recent allegations.\r\n\r\nIn the letter sent to SpaceX President Gwynne Shotwell on Wednesday, Reps Robert Garcia and Jamie Raskin wrote that it was \"alarming\" that Russian troops might have obtained Starlink technology in violation of US sanctions.",
                            CreatedAt = DateTime.Now,
                            Image = "https://ichef.bbci.co.uk/news/1024/cpsprodpb/B0D1/production/_132856254_soldierstarlink.jpg.webp",
                            Creator = users[3],
                            Category =  categories.Where(x => x.Name == "Technology").FirstOrDefault(),
                            IsPrivateOnly = true,
                            IsPublished = true,
                        }
                    }
                    );
            }

            context.SaveChanges();
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
