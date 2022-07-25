namespace System {
    public static class DateTimeExtensions {
        /*
            Good Morning
            5:00 AM — 11:59 AM

            Good Afternoon
            12:00 PM — 4:59 PM

            Good Evening
            5:00 PM — 4:59 AM
        */
        public static string ToGreeting(this DateTime dateTime) {
            if (dateTime.Hour >= 5 && dateTime.Hour < 12)
                return "GOOD MORNING";

            if (dateTime.Hour >= 12 && dateTime.Hour < 17)
                return "GOOD AFTERNOON";

            return "GOOD EVENING";
        }
    }
}