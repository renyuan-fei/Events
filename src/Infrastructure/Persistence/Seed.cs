using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public static class Seed
{
  public async static Task SeedData(
      EventsDbContext              eventsContext,
      UserManager<ApplicationUser> userManager)
  {
    if (!userManager.Users.Any())
    {
      var users = new List<ApplicationUser>
      {
          new ApplicationUser
          {
              UserName = "JohnDoe",
              DisplayName = "JohnDoe",
              Email = "johndoe@example.com",
              Bio =
                  "Life is like riding a bicycle. To keep your balance, you must keep moving."
          },
          new ApplicationUser
          {
              UserName = "JaneDoe",
              DisplayName = "JaneDoe",
              Email = "janedoe@example.com",
              Bio = "The only way to do great work is to love what you do."
          },
          new ApplicationUser
          {
              UserName = "MikeSmith",
              DisplayName = "MikeSmith",
              Email = "mikesmith@example.com",
              Bio = "Innovation distinguishes between a leader and a follower."
          },
          new ApplicationUser
          {
              UserName = "LisaRay",
              DisplayName = "LisaRay",
              Email = "lisaray@example.com",
              Bio =
                  "Success is not final, failure is not fatal: It is the courage to continue that counts."
          },
          new ApplicationUser
          {
              UserName = "TomBrown",
              DisplayName = "TomBrown",
              Email = "tombrown@example.com",
              Bio =
                  "Your time is limited, don’t waste it living someone else’s life."
          },
          new ApplicationUser
          {
              UserName = "EmmaStone",
              DisplayName = "EmmaStone",
              Email = "emmastone@example.com",
              Bio = "Be yourself; everyone else is already taken."
          },
          new ApplicationUser
          {
              UserName = "JamesWilson",
              DisplayName = "JamesWilson",
              Email = "jameswilson@example.com",
              Bio = "The way to get started is to quit talking and begin doing."
          },
          new ApplicationUser
          {
              UserName = "OliviaMartin",
              DisplayName = "OliviaMartin",
              Email = "oliviamartin@example.com",
              Bio =
                  "The greatest glory in living lies not in never falling, but in rising every time we fall."
          },
          new ApplicationUser
          {
              UserName = "RobertClark",
              DisplayName = "RobertClark",
              Email = "robertclark@example.com",
              Bio = "Life is what happens when you're busy making other plans."
          },
          new ApplicationUser
          {
              UserName = "SophiaDavis",
              DisplayName = "SophiaDavis",
              Email = "sophiadavis@example.com",
              Bio =
                  "The future belongs to those who believe in the beauty of their dreams."
          }
      };

      var userPhotoUrls = new List<string>
      {
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382849/samples/upscale-face-1.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382849/samples/woman-on-a-football-field.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382847/samples/man-portrait.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382846/samples/man-on-a-street.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382846/samples/man-on-a-escalator.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382846/samples/outdoor-woman.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382844/samples/smile.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382816/samples/people/kitchen-bar.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382819/samples/people/smiling-man.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382821/samples/people/boy-snow-hoodie.jpg"
      };

      var activityPhotoUrls = new List<string>()
      {
          // "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382830/samples/animals/kitten-playing.gif",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382822/samples/animals/three-dogs.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382816/samples/animals/reindeer.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382815/samples/animals/cat.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382826/samples/ecommerce/accessories-bag.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382826/samples/ecommerce/leather-bag-gray.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382825/samples/ecommerce/car-interior-design.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382814/samples/ecommerce/analog-classic.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382828/samples/food/spices.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382818/samples/food/pot-mussels.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382817/samples/food/fish-vegetables.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382815/samples/food/dessert.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382829/samples/landscapes/nature-mountains.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382825/samples/landscapes/beach-boat.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382823/samples/landscapes/architecture-signs.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382819/samples/landscapes/girl-urban-view.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382824/samples/people/bicycle.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382822/samples/people/jazz.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382848/samples/dessert-on-a-plate.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382848/samples/cup-on-a-table.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382847/samples/chair-and-coffee-table.jpg",
          "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382844/samples/breakfast.jpg"
      };

      var activities = new List<Activity>
      {
          // // 猫咪活动
          // new Activity(ActivityId.New(),
          //              "Cat Cafe Gathering",
          //              DateTime.Now.AddDays(2).ToUniversalTime(),
          //              Category.SocialActivities,
          //              "Join us for a purr-fect afternoon of fun at the local cat cafe! Enjoy the company of playful kittens in a cozy setting.",
          //              Address.From("Tokyo", "Cat Cafe Neko no Niwa"),
          //              ActivityStatus.Confirmed),
          // 三只狗活动
          new Activity(ActivityId.New(),
                       "Dog Owners Meetup",
                       DateTime.Now.AddDays(4).ToUniversalTime(),
                       Category.SocialActivities,
                       "A friendly gathering for dog owners to share tips, stories, and enjoy a day out with their furry friends in the park.",
                       Address.From("New York", "Central Park Dog Area"),
                       ActivityStatus.Confirmed),
          // 驯鹿活动
          new Activity(ActivityId.New(),
                       "Reindeer Safari Adventure",
                       DateTime.Now.AddDays(6).ToUniversalTime(),
                       Category.TravelAndOutdoor,
                       "Experience the majestic beauty of reindeer in their natural habitat on our exclusive winter safari adventure in the Arctic.",
                       Address.From("Lapland", "Arctic Reindeer Park"),
                       ActivityStatus.Confirmed),
          // 家猫活动
          new Activity(ActivityId.New(),
                       "Cats and Crafts Workshop",
                       DateTime.Now.AddDays(8).ToUniversalTime(),
                       Category.HobbiesAndPassions,
                       "Get creative at our Cats and Crafts Workshop. Knit, paint, and create with the soothing presence of our calm and friendly cats.",
                       Address.From("San Francisco", "Crafty Cats Studio"),
                       ActivityStatus.Confirmed),
          // 配饰包包活动
          new Activity(ActivityId.New(),
                       "Leather Crafting Workshop",
                       DateTime.Now.AddDays(10).ToUniversalTime(),
                       Category.HobbiesAndPassions,
                       "Discover the art of leather crafting and make your own stylish accessories in our hands-on workshop led by expert artisans.",
                       Address.From("Florence", "Italian Leather School"),
                       ActivityStatus.Confirmed),
          // 灰色皮包活动
          new Activity(ActivityId.New(),
                       "Luxury Handbag Exhibition",
                       DateTime.Now.AddDays(12).ToUniversalTime(),
                       Category.ArtAndCulture,
                       "Explore the world of luxury handbags at our exclusive exhibition featuring the finest leather craftsmanship and design.",
                       Address.From("Milan", "Fashion Expo Center"),
                       ActivityStatus.Confirmed),
          // 汽车内饰设计活动
          new Activity(ActivityId.New(),
                       "Auto Interior Design Expo",
                       DateTime.Now.AddDays(14).ToUniversalTime(),
                       Category.Technology,
                       "Discover the latest trends in auto interior design, featuring innovative technology and luxurious finishes at our annual expo.",
                       Address.From("Stuttgart", "Automobile Design Museum"),
                       ActivityStatus.Confirmed),
          // 经典模拟表活动
          new Activity(ActivityId.New(),
                       "Classic Watch Collectors Meet",
                       DateTime.Now.AddDays(16).ToUniversalTime(),
                       Category.HobbiesAndPassions,
                       "Join fellow watch enthusiasts to admire, discuss and trade timeless pieces at our classic watch collectors meet.",
                       Address.From("Geneva", "Watch Enthusiasts Club"),
                       ActivityStatus.Confirmed),
          // 香料活动
          new Activity(ActivityId.New(),
                       "Spice Market Tour",
                       DateTime.Now.AddDays(18).ToUniversalTime(),
                       Category.HealthAndWellbeing,
                       "Take a sensory journey through our local spice market and discover the secrets of herbs and spices used in traditional cuisine.",
                       Address.From("Marrakech", "Medina Spice Market"),
                       ActivityStatus.Confirmed),
          // 贻贝锅活动
          new Activity(ActivityId.New(),
                       "Mussel Cooking Class",
                       DateTime.Now.AddDays(20).ToUniversalTime(),
                       Category.FoodAndDrink,
                       "Learn to cook delicious mussel dishes in our interactive cooking class, perfect for seafood lovers looking to enhance their culinary skills.",
                       Address.From("Brussels", "Culinary Arts Academy"),
                       ActivityStatus.Confirmed),
          // 鱼蔬菜盘活动
          new Activity(ActivityId.New(),
                       "Healthy Cooking Workshop",
                       DateTime.Now.AddDays(22).ToUniversalTime(),
                       Category.HealthAndWellbeing,
                       "Join our healthy cooking workshop to learn how to prepare nutritious and delicious fish and vegetable meals for a balanced diet.",
                       Address.From("California", "Healthy Living Cooking School"),
                       ActivityStatus.Confirmed),
          // 甜点活动
          new Activity(ActivityId.New(),
                       "Dessert Baking Course",
                       DateTime.Now.AddDays(24).ToUniversalTime(),
                       Category.FoodAndDrink,
                       "Indulge your sweet tooth and learn to bake exquisite desserts in our professional baking course, from classic cakes to modern pastries.",
                       Address.From("Paris", "Le Cordon Bleu"),
                       ActivityStatus.Confirmed),
          // 自然山景活动
          new Activity(ActivityId.New(),
                       "Mountain Hiking Adventure",
                       DateTime.Now.AddDays(26).ToUniversalTime(),
                       Category.TravelAndOutdoor,
                       "Embark on a breathtaking mountain hiking adventure to explore scenic trails, stunning vistas, and untouched natural beauty.",
                       Address.From("Colorado", "Rocky Mountain National Park"),
                       ActivityStatus.Confirmed),
          // 海滩船只活动
          new Activity(ActivityId.New(),
                       "Beach Sailing Day",
                       DateTime.Now.AddDays(28).ToUniversalTime(),
                       Category.SportsAndFitness,
                       "Experience the thrill of sailing along the coast, enjoying the sun, sea, and sand on our exclusive beach sailing day event.",
                       Address.From("Florida", "Sandy Beach Marina"),
                       ActivityStatus.Confirmed),
          // 建筑与标志活动
          new Activity(ActivityId.New(),
                       "Urban Architecture Tour",
                       DateTime.Now.AddDays(30).ToUniversalTime(),
                       Category.ArtAndCulture,
                       "Explore the city's iconic architecture and learn about the history and design of its most significant buildings and monuments.",
                       Address.From("New York", "Cityscape Tours"),
                       ActivityStatus.Confirmed),
          new Activity(ActivityId.New(),
                       "Urban Exploration Tour",
                       DateTime.Now.AddDays(32).ToUniversalTime(),
                       Category.TravelAndOutdoor,
                       "Join our urban exploration tour to discover hidden gems and spectacular views of the cityscape from unique vantage points.",
                       Address.From("Chicago", "City Viewpoint"),
                       ActivityStatus.Confirmed),
          // 自行车活动
          new Activity(ActivityId.New(),
                       "Cycling Around the City",
                       DateTime.Now.AddDays(34).ToUniversalTime(),
                       Category.SportsAndFitness,
                       "Experience the city from a different perspective on our guided cycling tour, perfect for fitness enthusiasts and urban explorers.",
                       Address.From("Amsterdam", "Central Bike Tours"),
                       ActivityStatus.Confirmed),
          // 爵士乐手活动
          new Activity(ActivityId.New(),
                       "Jazz Night Experience",
                       DateTime.Now.AddDays(36).ToUniversalTime(),
                       Category.ArtAndCulture,
                       "Immerse yourself in the soulful sounds of jazz at our exclusive night event, featuring renowned musicians and vibrant performances.",
                       Address.From("New Orleans", "Jazz Club Central"),
                       ActivityStatus.Confirmed),
          // 盘中甜点活动
          new Activity(ActivityId.New(),
                       "Dessert Tasting Event",
                       DateTime.Now.AddDays(38).ToUniversalTime(),
                       Category.FoodAndDrink,
                       "Savor an array of exquisite desserts at our tasting event, where sweet meets artistry in a celebration of flavor and creativity.",
                       Address.From("Vienna", "Dessert Gallery"),
                       ActivityStatus.Confirmed),
          // 桌上的杯子活动
          new Activity(ActivityId.New(),
                       "Coffee Connoisseur Workshop",
                       DateTime.Now.AddDays(40).ToUniversalTime(),
                       Category.FoodAndDrink,
                       "Explore the art of coffee making in our workshop designed for coffee connoisseurs, from brewing techniques to tasting notes.",
                       Address.From("Seattle", "Brew Master's Cafe"),
                       ActivityStatus.Confirmed),
          // // 椅子和咖啡桌活动
          new Activity(ActivityId.New(),
                       "Furniture Design Showcase",
                       DateTime.Now.AddDays(250),
                       Category.HobbiesAndPassions,
                       "Discover innovative furniture designs and modern decor at our showcase event, featuring the work of leading designers and craftsmen.",
                       Address.From("Milan", "Design Expo Hall"),
                       ActivityStatus.Confirmed),
          // // 早餐活动
          // new Activity(ActivityId.New(),
          //              "Gourmet Breakfast Club",
          //              DateTime.Now.AddDays(260),
          //              Category.FoodAndDrink,
          //              "Start your day in style with our gourmet breakfast club, offering a menu of delicious, freshly prepared morning delights.",
          //              Address.From("San Francisco", "Morning Bliss Cafe"),
          //              ActivityStatus.Confirmed)
      };

      var activityIndex = 0;

      for (var i = 0; i < users.Count; i++)
      {
        var result = await userManager.CreateAsync(users[i], "Password123!");

        if (!result.Succeeded) continue;

        var createdUser = await userManager.FindByNameAsync(users[i].UserName);

        // Assuming the Photo class has an Add method according to your earlier implementation
        var userPhotoUrlParts = userPhotoUrls[i].Split('/');
        var userPublicId = string.Join("/", userPhotoUrlParts.Skip(4).Take(userPhotoUrlParts.Length - 5));
        var userPhoto = Photo.Add(userPublicId, userPhotoUrls[i], true, createdUser.Id);

        eventsContext.Photos.Add(userPhoto);

        for (var j = 0; j < 2; j++, activityIndex++)
        {
          if (activityIndex >= activities.Count) break;

          var activityPhotoUrlParts = activityPhotoUrls[activityIndex].Split('/');
          var activityPublicId = string.Join("/", activityPhotoUrlParts.Skip(4).Take(activityPhotoUrlParts.Length - 5));

          var activity = activities[activityIndex];

          var activityPhoto = Photo.Add(activityPublicId,
              activityPhotoUrls[activityIndex], true, activity.Id.Value);
          eventsContext.Photos.Add(activityPhoto);

          var host = Attendee.Create(new UserId(createdUser.Id), true, activity.Id, activity);

          activity.AddAttendee(host);

          eventsContext.Activities.Add(activity);
        }
      }

      await eventsContext.SaveChangesAsync();
    }
  }
}