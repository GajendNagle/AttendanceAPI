namespace PMPoshanWithAngular.Server.Helper
{
    public class DateHelper
    {

        public static (DateTime parsedFromDate, DateTime parsedToDate)
     ParseDateRange(string? fromDate, string? toDate)
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            string[] formats = {  "dd-MM-yyyy" };

            DateTime parsedFromDate;
            DateTime parsedToDate;

            //  From Date
            if (string.IsNullOrWhiteSpace(fromDate) ||
                fromDate.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                parsedFromDate = new DateTime(2022, 9, 21); // default
            }
            else if (DateTime.TryParseExact(
                        fromDate.Replace("/", "-"),
                        formats,
                        culture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime from))
            {
                parsedFromDate = from.Date;

            }
            else
            {
                parsedFromDate = new DateTime(2022, 9, 21); // invalid format
            }

            // To Date
            if (string.IsNullOrWhiteSpace(toDate) ||
                toDate.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                parsedToDate = DateTime.Today;
            }
            else if (DateTime.TryParseExact(
                        toDate.Replace("/", "-"),
                        formats,
                        culture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime to))
            {
                parsedToDate = to.Date; // valid input
            }
            else
            {
                parsedToDate = DateTime.Today; // invalid format
            }

            return (parsedFromDate, parsedToDate);
        }

    }
}
