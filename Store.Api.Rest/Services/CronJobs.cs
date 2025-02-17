namespace Store.Api.Rest.Services
{
    public class CronJobs : ICronJobs
    {
        public void GetAppUpDateTime()
        {
            //Task.Delay(10);
            Console.WriteLine($"DateTime from Hangfire : {DateTime.Now.ToShortTimeString()} ");
        }

        public void GetRandomNumber()
        {
            //Task.Delay(10);
            Console.WriteLine($"Random is : {(new Random()).NextInt64().ToString()} ");
        }
    }
}
