using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsEvents.Web.Models;
using SportsEvents.Web.Repository;

namespace SportsEvents.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SportsEventsDbContext>
    {
        public Configuration()
        {
            ContextKey = "SportsEvents.Web.Repository.SportsEventsDbContext";
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SportsEventsDbContext context)
        {
            base.Seed(context);
            if (!context.Events.Any())
            {
                var usermanager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole("Organizer"));
                var rand = new Random(DateTime.Now.Second);
                var advertisements = new List<Advertisement>();
                var countries = new List<Country>();
                var cities = new List<City>();
                var events = new List<Event>();

                for (int i = 0; i < 20; i++)
                {
                    var country = new Country() { Name = Ipsum.GetWord() };
                    ;
                    for (int j = 0; j < 20; j++)
                    {
                        cities.Add(new City() { Name = Ipsum.GetWord(), Country = country, CountryName = country.Name });
                    }
                }


                for (int i = 0; i < 20; i++)
                {
                    advertisements.Add(new Advertisement()
                    {
                        Image = "https://placehold.it/600x600?text=Ad+" + Ipsum.GetWord(),
                        Priority = rand.Next(1, 11),
                        Prelogin = rand.Next(0, 2) == 1,
                        Keywords = Ipsum.GetWord()
                    });
                }
                for (int i = 0; i < 30; i++)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = Ipsum.GetWord(),
                        Email = Ipsum.GetWord() + "@" + Ipsum.GetWord() + ".com",
                        Address = new Address()
                    };

                    usermanager.Create(user,"idkwmpsb");
                }
                context.Advertisements.AddRange(advertisements);
                var organizers = context.Users.ToList();
                for (var i = 0; i < 20; i++)
                {


                    var sport = new Sport { Name = Ipsum.GetPhrase(rand.Next(1, 4)) };
                    var eventType = new EventType { Name = Ipsum.GetPhrase(rand.Next(1, 4)) };
                    context.Sports.Add(sport);
                    context.EventTypes.Add(eventType);

                    for (var j = 0; j < 20; j++)
                    {
                        var pictures = new List<Picture>();
                        for (int k = 0; k < rand.Next(1, 5); k++)
                        {
                            pictures.Add(new Picture() { Url = "https://placehold.it/1000x800?text=" + Ipsum.GetWord() });
                        }

                        var description = Ipsum.GetPhrase(rand.Next(1, 10));
                        var beginDate = DateTime.Now.Date + TimeSpan.FromDays(rand.Next(1, 15));
                        var detail = Ipsum.GetPhrase(rand.Next(40, 200));
                        var endDate = beginDate + TimeSpan.FromDays(rand.Next(1, 15));
                        var organizer = organizers[rand.Next(organizers.Count)];
                        var address = new Address
                        {
                            LineOne = Ipsum.GetPhrase(rand.Next(1, 10)),
                            LineTwo = Ipsum.GetPhrase(rand.Next(1, 10))
                        };
                        if (!String.IsNullOrEmpty(description))
                        {
                            description = description.Substring(0, 1).ToUpper() + description.Substring(1);

                        }
                        if (!String.IsNullOrEmpty(detail))
                        {
                            detail = detail.Substring(0, 1).ToUpper() + detail.Substring(1);
                        }
                        var @event = new Event
                        {
                            BeginDate = beginDate,
                            BeginTime = DateTime.Now,
                            EndTime = DateTime.Now,
                           
                            Address = address,
                            Description = description,
                            Details = detail,
                            EndDate = endDate,
                            StartingPrice = rand.Next(0, 1000),
                           
                            VideoLink = "https://placehold.it/600x400?text=" + Ipsum.GetWord(),
                            Pictures = pictures,
                            Sport = sport,
                            SportName = sport.Name,  
                            EventType = eventType,
                            EventTypeName = eventType.Name,
                            Organizer = organizer,
                            City = cities[rand.Next(cities.Count)],
                            OrganizerName = organizer.UserName,
                            IsFeatured = rand.Next(2) == 1,
                            
                        };
                        @event.RegisterRequestVisitors=new List<ApplicationUser>();
                        @event.BookmarkerVisitors= new List<ApplicationUser>();
                        @event.RegisteredVisitors = new List<ApplicationUser>();
                        @event.ClickerUsers  = new List<ApplicationUser>();
                        for (int k= 0; k < rand.Next(organizers.Count)/2;k++)
                        {
                            @event.RegisterRequestVisitors.Add(organizers[k]);
                            
                        }
                        for (int k = 0; k < rand.Next(organizers.Count) / 2; k++)
                        {
                            @event.RegisteredVisitors.Add(organizers[k]);

                        }
                        for (int k = 0; k < rand.Next(organizers.Count) / 2; k++)
                        {
                            @event.ClickerUsers.Add(organizers[k]);

                        }
                        for (int k = 0; k < rand.Next(organizers.Count) / 2; k++)
                        {
                            @event.BookmarkerVisitors.Add(organizers[k]);

                        }
                        events.Add(@event);
                    }
                }
                context.Events.AddRange(events);
            }
        }
    }
}