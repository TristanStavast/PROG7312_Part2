using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG7312_Part2.Models.Common.Event;
using System.Text.Json;

namespace PROG7312_Part2.Pages
{
    public class LocalEventsModel : PageModel
    {
        //Data structures for all data (Queue, SortedDictionary, Dictionary
        public Queue<Event> UpcomingEvents { get; set; } = new Queue<Event>();
        public List<Event> SearchResults { get; private set; }
        public bool SearchPerformed { get; private set; } = false;

        public SortedDictionary<DateOnly, List<Event>> LocalEventsDic = new SortedDictionary<DateOnly, List<Event>>();
        public Dictionary<string, int> RecommendedCategories { get; private set; } = new Dictionary<string, int>();
       
        //Initializing everything on the page load
        public void OnGet()
        {
            InitializeEvent();
            GetUpcomingEvents(LocalEventsDic);
            LoadRecommendedCategories();
        }

        //Initialising on each search on page
        public void OnPostSearch(string category, DateTime? date)
        {
            InitializeEvent();  // Ensure events are initialized
            SearchResults = SearchEvents(LocalEventsDic, category, date);
            SearchPerformed = true;

            LoadRecommendedCategories() ;
            UpdateRecommendedCategories(category);
            SaveRecommendedCategories();
        }

        //Adding events to the dictionary
        public void AddEvent(Event localEvent)
        {
            if (LocalEventsDic.ContainsKey(localEvent.Date))
            {
                LocalEventsDic[localEvent.Date].Add(localEvent);
            }
            else
            {
                LocalEventsDic.Add(localEvent.Date, new List<Event> { localEvent });
            }
        }

        #region Adding Events
        public void InitializeEvent()
        {
            #region Music Events
            // Music Events
            var ev1 = new Event { Title = "Music Concert", Category = "Music", Date = new DateOnly(2023, 10, 20), CardDisplayInfo = new EventCardDisplayInfo { CardColor = EventCardColors.music } }; // Future event
            var ev2 = new Event { Title = "Jazz Night", Category = "Music", Date = new DateOnly(2024, 10, 15) }; // Recent event
            var ev3 = new Event { Title = "OktoberFest", Category = "Music", Date = new DateOnly(2024, 10, 26), CardDisplayInfo = new EventCardDisplayInfo {CardColor = EventCardColors.music}  }; // Future event
            var ev4 = new Event { Title = "EDM Festival", Category = "Music", Date = new DateOnly(2024, 10, 28) }; // Future event
            var ev5 = new Event { Title = "Classical Evening", Category = "Music", Date = new DateOnly(2024, 11, 1), CardDisplayInfo = new EventCardDisplayInfo {CardColor = EventCardColors.music}  }; // Upcoming event
            #endregion

            #region Education Events
            // Education Events
            var ev6 = new Event { Title = "Reeces Pieces Workshop", Category = "Education", Date = new DateOnly(2025, 1, 12) }; // Future event
            var ev7 = new Event { Title = "Science Fair", Category = "Education", Date = new DateOnly(2024, 10, 30) }; // Upcoming event
            var ev8 = new Event { Title = "VC Open Day", Category = "Education", Date = new DateOnly(2025, 2, 25) }; // Future event
            var ev9 = new Event { Title = "Career Day", Category = "Education", Date = new DateOnly(2025, 3, 30) }; // Future event
            var ev10 = new Event { Title = "Check Reece Spelling Day", Category = "Education", Date = new DateOnly(2025, 4, 12) }; // Future event
            #endregion

            #region Arts Events
            // Arts Events
            var ev11 = new Event { Title = "Art Exhibition", Category = "Arts", Date = new DateOnly(2024, 11, 23) }; // Future event
            var ev12 = new Event { Title = "Photography Workshop", Category = "Arts", Date = new DateOnly(2024, 11, 5) }; // Upcoming event
            var ev13 = new Event { Title = "Sculpture Fair", Category = "Arts", Date = new DateOnly(2025, 1, 5) }; // Future event
            var ev14 = new Event { Title = "Painting Masterclass", Category = "Arts", Date = new DateOnly(2025, 2, 15) }; // Future event
            var ev15 = new Event { Title = "Cultural Festival", Category = "Arts", Date = new DateOnly(2025, 3, 10) }; // Future event
            #endregion

            #region Sports Events
            // Sports Events
            var ev16 = new Event { Title = "SA20", Category = "Sports", Date = new DateOnly(2025, 1, 15), CardDisplayInfo = new EventCardDisplayInfo {CardColor = EventCardColors.music} }; // Future event
            var ev17 = new Event { Title = "Nicks 5m Dash", Category = "Sports", Date = new DateOnly(2024, 11, 3) }; // Upcoming event
            var ev18 = new Event { Title = "Marathon 2025", Category = "Sports", Date = new DateOnly(2025, 2, 20), CardDisplayInfo = new EventCardDisplayInfo { CardColor = EventCardColors.music } }; // Future event
            var ev19 = new Event { Title = "URC Final", Category = "Sports", Date = new DateOnly(2025, 4, 5) }; // Future event
            var ev20 = new Event { Title = "Swimming Gala", Category = "Sports", Date = new DateOnly(2025, 5, 10), CardDisplayInfo = new EventCardDisplayInfo { CardColor = EventCardColors.music } }; // Future event
            #endregion

            #region Adding All Events
            // Adding all events to the main event list
            AddEvent(ev1);
            AddEvent(ev2);
            AddEvent(ev3);
            AddEvent(ev4);
            AddEvent(ev5);
            AddEvent(ev6);
            AddEvent(ev7);
            AddEvent(ev8);
            AddEvent(ev9);
            AddEvent(ev10);
            AddEvent(ev11);
            AddEvent(ev12);
            AddEvent(ev13);
            AddEvent(ev14);
            AddEvent(ev15);
            AddEvent(ev16);
            AddEvent(ev17);
            AddEvent(ev18);
            AddEvent(ev19);
            AddEvent(ev20);
            #endregion
        }
        #endregion

        //Using HashSet to obtain unique dates for all events
        public HashSet<DateOnly> GetUniqueDates(string category)
        {
            var uniqueDates = new HashSet<DateOnly>();

            foreach (var events in LocalEventsDic.Values)
            {
                foreach (var ev in events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)))
                {
                    uniqueDates.Add(ev.Date);
                }
            }
            return uniqueDates;
        }

        //Getting all the upcoming dates
        public void GetUpcomingEvents(SortedDictionary<DateOnly, List<Event>> events)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            var upComingEvents = events.Where(e => e.Key >= currentDate)
                 .SelectMany(e => e.Value)
                 .OrderBy(e => e.Date);

            foreach (var upComingEvent in upComingEvents)
            {
                UpcomingEvents.Enqueue(upComingEvent);
            }
        }

        //Temp list to search for events
        public List<Event> SearchEvents(SortedDictionary<DateOnly, List<Event>> eventsDictionary, string category, DateTime? searchDate = null)
        {
            var filteredEvents = eventsDictionary.SelectMany(e => e.Value)
                .Where(ev => ev.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            if(searchDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.Date.Equals(DateOnly.FromDateTime(searchDate.Value)));
            }
            return filteredEvents.OrderBy(e => e.Date).ToList();
        }

        //Updating recommended categories
        private void UpdateRecommendedCategories(string category)
        {
            if(RecommendedCategories.ContainsKey(category))
            {
                RecommendedCategories[category]++;
            }
            else
            {
                RecommendedCategories[category] = 1;
            }
        }

        //Saving temp data using json serializer
        private void SaveRecommendedCategories()
        {
            TempData["RecommendedCategories"] = JsonSerializer.Serialize(RecommendedCategories);
        }

        //Loading serialisation
        private void LoadRecommendedCategories()
        {
            if(TempData.TryGetValue("RecommendedCategories", out var data))
            {
                RecommendedCategories = JsonSerializer.Deserialize<Dictionary<string, int>>(data.ToString());
            }
        }

        //Getting recommended events
        public List<Event> GetRecommendedEvents()
        {
            var recommendedEvents = new List<Event>();

            foreach (var cat in RecommendedCategories.Keys)
            {
                var eventInCat = LocalEventsDic.SelectMany(e => e.Value)
                    .Where(ev => ev.Category.Equals(cat, StringComparison.OrdinalIgnoreCase));

                recommendedEvents.AddRange(eventInCat);
            }
            return recommendedEvents;
        }
        
    }

    

    
}
