﻿using Friendly.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Friendly.WebAPI
{
    public static class SeedData
    {

        private static Random gen = new Random();
        public static List<Post> GetPosts(int userId)
        {
            List<Post> postsUser1 = new List<Post>
                {
                    new Post
                {
                    ImagePath = "running-track.jpg",
                    UserId = userId,
                    HobbyId = 7,
                    Description = "Neko za trcanje u sjevernom, vrijeme 06:00? ",
                    Latitude = 44.128014,
                    Longitude = 18.117826
                },
                    new Post
                {
                    UserId = userId,

                    HobbyId = 2,
                    Description = "Basket danas u 15:00 poligon kod sahat kule.",
                    DateCreated = RandomDay(),
                    ImagePath = "basketball.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826
                    },

                      new Post
                {
                                        UserId = userId,

                    HobbyId = 10,
                    Description = "Kickbox training today at 13:00, aynone interested? Please like this post at least an hour before the training. Bring your gear there's gona be spar.",
                    DateCreated = RandomDay(),
                    ImagePath = "kickbox.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826
                    },
                        new Post
                {
                                      UserId = userId,

                    HobbyId = 10,
                    Description = "I am new in town is there a gym to recommend (student's price) and leave a like if you are interested in training w me. Thx :D ",
                    DateCreated = RandomDay(),
                    ImagePath = "gym.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826

                    },
                          new Post
                {
                                        UserId = userId,

                    HobbyId = 51,
                    Description = "Looking for fellow gamers to join me in an epic Dungeons & Dragons campaign. Beginners welcome!",
                    DateCreated = RandomDay(),
                    ImagePath = "gaming.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826


                    },   new Post
                {
                                       UserId = userId,

                    HobbyId = 61,
                    Description = "Photography enthusiasts, let's explore the city and capture some stunning shots at golden hour tomorrow evening.",
                    DateCreated = RandomDay(),
                    ImagePath = "photography.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826

                    },
                            new Post
                {
                                        UserId = userId,

                    HobbyId = 91,
                    DateCreated = RandomDay(),
                    Description = "Calling all collectors! Let's organize a swap meet to exchange and showcase our treasures. Reply if interested!",
                    ImagePath = "collecting.jpg",
                    Latitude = 44.128014,
                    Longitude = 18.117826

                    }
            };

            return postsUser1;
        }
        static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        public static async Task Initialize(FriendlyContext dbContext, IServiceProvider services)
        {

            if (!dbContext.ReportReason.Any())
            {
                await dbContext.ReportReason.AddRangeAsync(
                new ReportReason
                {
                    Description = "Spam"
                },
                new ReportReason
                {
                    Description = "Hate speech"
                },
                 new ReportReason
                 {
                     Description = "Abuse"
                 }
                    );
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Country.Any())
            {
                await dbContext.AddRangeAsync(new Country
                {
                    Name = "Bosnia and Herzegovina"
                }, new Country
                {
                    Name = "Croatia"
                },
                new Country
                {
                    Name = "Serbia"
                },
                new Country
                {
                    Name = "Montenegro"
                }
                );

                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.City.Any())
            {

                dbContext.AddRange(new City
                {
                    Name = "Kakanj",
                    CountryId = 1,
                }, new City
                {
                    Name = "Sarajevo",
                    CountryId = 1,

                },
               new City
               {
                   CountryId = 1,
                   Name = "Mostar"
               },
               new City
               {
                   Name = "Banja Luka",
                   CountryId = 1,
               },
                new City
                {
                    Name = "Podgorica",
                    CountryId = 4,
                },
                new City
                {
                    Name = "Nikšić",
                    CountryId = 4,
                },
                 new City
                 {
                     Name = "Beograd",
                     CountryId = 3,
                 },
                   new City
                   {
                       Name = "Priboj",
                       CountryId = 3,
                   },
                    new City
                    {
                        Name = "Zagreb",
                        CountryId = 2,
                    },
                     new City
                     {
                         Name = "Split",
                         CountryId = 2,
                     },
                     new City
                     {
                         Name = "Dubrovnik",
                         CountryId = 2,
                     }
               );
            }


            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(new Microsoft.AspNetCore.Identity.IdentityRole<int>
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new Microsoft.AspNetCore.Identity.IdentityRole<int>
                {
                    Name = "User",
                    NormalizedName = "USER",
                }
                );
                await dbContext.SaveChangesAsync();
            }


            Database.User user1 = new Database.User
            {
                FirstName = "Atif",
                LastName = "Delibasic",
                Email = "atif.delibasic@gmail.com",
                UserName = "atif.delibasic@gmail.com",
                Description = "I love programming",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ProfileImageUrl = "atif.jpg",
                BirthDate = DateTime.Now
            };

            if (!dbContext.Users.Any())
            {
                var userManager = services.GetRequiredService<UserManager<Database.User>>();


                var result = await userManager.CreateAsync(user1, "Lozinka123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user1, "User");

                }

                string[] firstNames = { "John", "Emily", "Michael", "Sophia", "William", "Isabella", "James", "Olivia", "Benjamin", "Emma" };
                string[] lastNames = { "Smith", "Johnson", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas" };

                for (int i = 0; i < 10; i++)
                {
                    var user = new Database.User
                    {
                        FirstName = firstNames[i % firstNames.Length],
                        LastName = lastNames[i % lastNames.Length],
                        Email = $"{firstNames[i % firstNames.Length].ToLower()}.{lastNames[i % lastNames.Length].ToLower()}@example.com",
                        UserName = $"{firstNames[i % firstNames.Length].ToLower()}.{lastNames[i % lastNames.Length].ToLower()}@example.com",
                        Description = $"This is user {i + 1} description",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        BirthDate = DateTime.Now,
                        CityId = gen.Next(1,3)
                    };

                    var res = await userManager.CreateAsync(user, "Lozinka123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }

                    await dbContext.Feedback.AddAsync(new Feedback
                    {
                        UserId = user.Id,
                        Text = "This is a feedback message."
                    });

                    await dbContext.RateApp.AddAsync(new RateApp
                    {
                        UserId = user.Id,
                        Rating = gen.Next(1, 5),
                    });
                }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.UserRoles.Any())
            {
                dbContext.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1,

                });

                await dbContext.SaveChangesAsync();
            }


            if (!dbContext.HobbyCategory.Any())
            {

                Dictionary<string, string[]> categoryHobbies = new Dictionary<string, string[]>
{
    { "Sports", new string[] { "Football", "Basketball", "Tennis", "Swimming", "Golf", "Cycling", "Running", "Volleyball", "Surfing", "Martial arts" } },
    { "Arts & Crafts", new string[] { "Painting", "Drawing", "Sculpting", "Pottery", "Knitting", "Crocheting", "Origami", "Jewelry making", "Woodworking", "Calligraphy" } },
    { "Music", new string[] { "Playing guitar", "Piano", "Singing", "Drumming", "Violin", "Flute", "Saxophone", "DJing", "Ukulele", "Harmonica" } },
    { "Cooking & Baking", new string[] { "Baking cakes", "Cooking Italian cuisine", "Grilling", "Making sushi", "Cake decorating", "Bread making", "Brewing beer", "Preserving foods", "Candy making", "BBQ smoking" } },
    { "Outdoor Activities", new string[] { "Hiking", "Camping", "Fishing", "Rock climbing", "Kayaking", "Bird watching", "Geocaching", "Horseback riding", "Archery", "Canyoning" } },
    { "Gaming", new string[] { "Video gaming", "Board gaming", "Tabletop role-playing games", "Card games (e.g., Poker)", "Chess", "Puzzle games", "Escape room games", "Mobile gaming", "Retro gaming", "VR gaming" } },
    { "Photography", new string[] { "Landscape photography", "Portrait photography", "Wildlife photography", "Street photography", "Macro photography", "Fashion photography", "Aerial photography", "Event photography", "Astrophotography", "Underwater photography" } },
    { "Writing", new string[] { "Fiction writing", "Poetry", "Journaling", "Screenwriting", "Blogging", "Creative non-fiction", "Copywriting", "Songwriting", "Playwriting", "Technical writing" } },
    { "Dancing", new string[] { "Ballet", "Hip-hop", "Salsa", "Ballroom dancing", "Tango", "Breakdancing", "Jazz dancing", "Contemporary dance", "Tap dancing", "Swing dancing" } },
    { "Collecting", new string[] { "Stamp collecting", "Coin collecting", "Antiques collecting", "Comic book collecting", "Vinyl records collecting", "Action figures collecting", "Vintage cars collecting", "Postcard collecting", "Art collecting", "Toy trains collecting" } }
};


                foreach (var category in categoryHobbies)
                {
                    HobbyCategory newCategory = new HobbyCategory
                    {
                        Name = category.Key
                    };


                    dbContext.HobbyCategory.Add(newCategory);
                    await dbContext.SaveChangesAsync();

                    HobbyCategory hc = await dbContext.HobbyCategory.SingleOrDefaultAsync(x => x.Name == category.Key);

                    foreach (var hobby in category.Value)
                    {
                        //newCategory.AddHobby(hobby);
                        Hobby hobbyDb = new Hobby
                        {
                            Title = hobby,
                            HobbyCategoryId = hc.Id,
                            Description = "THis is a description"
                        };

                        dbContext.Hobby.Add(hobbyDb);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }


            if (!dbContext.UserHobbies.Any())
            {
                List<User> users = await dbContext.Users.ToListAsync();
                List<Hobby> hobbies = await dbContext.Hobby.ToListAsync();

                foreach (var user in users)
                {
                    int i = 0;
                    foreach (var hobby in hobbies)
                    {
                        if (i == 5)
                        {
                            break;
                        }

                        int random = gen.Next(1, 99);

                        var kurac = await dbContext.UserHobbies.SingleOrDefaultAsync(x => x.UserId == user.Id && x.HobbyId == random);

                        if (kurac != null)
                        {
                            i++;
                            continue;
                        }

                        UserHobby userHobby = new UserHobby
                        {
                            HobbyId = random,
                            UserId = user.Id
                        };
                        i++;

                        await dbContext.AddAsync(userHobby);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            if (!dbContext.Post.Any())
            {

                List<User> users = await dbContext.Users.ToListAsync();
                foreach (var user in users)
                {
                    await dbContext.AddRangeAsync(GetPosts(user.Id));
                }

                await dbContext.SaveChangesAsync();

                List<Post> posts = await dbContext.Post.ToListAsync();
                Random r = new Random();
                foreach (var post in posts)
                {
                    if (post.UserId != 1 && (post.Id > 1 && post.Id < 25))
                    {
                        await dbContext.AddAsync(new Report
                        {
                            PostId = post.Id,
                            ReportReasonId = gen.Next(1, 2),
                            UserId = 1,
                            AdditionalComment = "This post is offensive"
                        }
                             );
                    }


                    for (int i = 0; i < 5; i++)
                    {
                        int userId = r.Next(1, 10);


                        Like like = new Like
                        {
                            UserId = userId,
                            PostId = post.Id
                        };

                        Notification n = new Notification
                        {
                            RecipientId = userId,
                            SenderId = r.Next(2, 10),
                            Message = "Liked your post"
                        };

                        Comment comment = new Comment
                        {
                            UserId = userId,
                            PostId = post.Id,
                            Text = "This is a random comment."
                        };

                        await dbContext.AddAsync(like);
                        await dbContext.AddAsync(comment);
                        await dbContext.AddAsync(n);
                    }
                }

                await dbContext.SaveChangesAsync();

            }

            if (!dbContext.Friendship.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    Friendship friendship = new Friendship
                    {
                        UserId = 1,
                        FriendId = i + 1,
                        Status = FriendshipStatus.Friends
                    };

                    await dbContext.AddAsync(friendship);
                }

                Friendship f = new Friendship
                {
                    UserId = 9,
                    FriendId = 1,
                    Status = FriendshipStatus.Pending
                };

                await dbContext.AddAsync(f);

                Friendship f1 = new Friendship
                {
                    UserId = 10,
                    FriendId = 1,
                    Status = FriendshipStatus.Pending
                };

                await dbContext.AddAsync(f1);

                await dbContext.SaveChangesAsync();
            }
            List<string> questions = new List<string>
                {
                    "Hey, how are you doing today?",
                    "Where did you go hiking?",
                    "How was the view?",
                    "Have you seen the latest movie?",
                    "What did you think of the plot twists?",
                    "Any other recommendations?",
                    "Did you enjoy the new series on Netflix?"
                };

                        List<string> answers = new List<string>
                {
                    "I'm good, thanks! How about you?",
                    "I went to Blue Ridge Mountains.",
                    "The view was amazing!",
                    "Not yet, but I've heard great things about it!",
                    "They were mind-blowing.",
                    "If you like thrillers, you should check out the new series on Netflix.",
                    "Yes, I did! It was fantastic."
                };


             
            if (!dbContext.Message.Any())
            {
                List<Message> messages = new List<Message>();

                List<User> users = await dbContext.Users.ToListAsync();

                foreach (var user in users)
                {
                    if(user.Id == 1)
                    {
                        continue;
                    }

                    for (int i = 0; i < answers.Count; i++)
                    {
                        var question = new Message
                        {
                            Content = questions[i],
                            SenderId = user.Id,
                            RecipientId = 1
                        };

                        var answer = new Message
                        {
                            Content = answers[i],
                            SenderId = 1,
                            RecipientId = user.Id
                        };

                        await dbContext.AddAsync(question);
                        await dbContext.AddAsync(answer);
                    }
                }
                await dbContext.SaveChangesAsync();
            }


            if (!dbContext.FITPassport.Any())
            {
                for (int i = 1 ; i < 10; i++)
                {
                    var pass = new FITPassport
                    {
                        UserId = i,
                        isActive = i % 2 == 0,
                        ExpireDate = new DateTime(2025, 1, 1),
                    };
                    await dbContext.AddAsync(pass);
                }
                await dbContext.SaveChangesAsync();
            }
        }

    }

}


