namespace PROG7312_Part2.Models.Common.Event
{
    public class Event
    {
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public string Category { get; set; }
        public EventCardDisplayInfo CardDisplayInfo { get; set; } = new EventCardDisplayInfo { CardColor = EventCardColors.standard};
    }

    public class EventCardDisplayInfo
    {
        public string CardColor { get; set; } = EventCardColors.standard;
    }

    public static class EventCardColors
    {
        public const string standard = "#d5d8dc";
        public const string music = "#45b39d";
        public const string education = "#8fb8ef";
        public const string art = "#efc58f";
        public const string sport = "#ef8fbc";



    }
}
