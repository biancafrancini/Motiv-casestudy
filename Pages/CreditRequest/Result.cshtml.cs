using Microsoft.AspNetCore.Mvc.RazorPages;
using MotivWebApp.Pages.CreditRequest.Services;

namespace MotivWebApp.Pages.CreditRequest
{
    public class ResultModel : PageModel
    {
        // Properties
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int AnnualIncome { get; set; }
        public string EmploymentStatus { get; set; }
        public decimal CreditAmount { get; set; }

        // DataStore instance
        private readonly MockDataStore dataStore;

        public ResultModel()
        {
            dataStore = new Services.MockDataStore();
        }

        // Handler for HTTP GET request
        public void OnGet(string obj_key)
        {
            // Fetch user data from dataStore
            var user_data = dataStore.GetData(obj_key);

            if (user_data != null)
            {
                // Retrieve and convert user data values
                Name = Convert.ToString(user_data["Name"]);
                if (int.TryParse(user_data.ContainsKey("Age") ? user_data["Age"].ToString() : "", out int ageValue))
                {
                    Age = ageValue;
                }
                if (int.TryParse(user_data.ContainsKey("AnnualIncome") ? user_data["AnnualIncome"].ToString() : "", out int annualIncomeValue))
                {
                    AnnualIncome = annualIncomeValue;
                }

                EmploymentStatus = Convert.ToString(user_data["EmploymentStatus"]);

                // Calculate credit amount
                CalculateCreditAmount();
            }
        }

        // Calculate credit amount based on user data
        private void CalculateCreditAmount()
        {
            // Some random credit calculator
            decimal baseCredit = 1000;
            decimal ageMultiplier = Age >= 25 ? 1.2M : 1;
            decimal incomeMultiplier = AnnualIncome >= 50000 ? 1.5M : 1;
            decimal employmentMultiplier = EmploymentStatus == "full-time" ? 1.8M : 1;

            CreditAmount = baseCredit * ageMultiplier * incomeMultiplier * employmentMultiplier;
        }
    }
}
