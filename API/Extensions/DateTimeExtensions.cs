namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        //Adding a DateTime extension method to calculate Age:2
        public static int CalcuateAge(this DateTime dob)
        {
            var age = DateTime.Today.Year - dob.Year;
            if (dob.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}
