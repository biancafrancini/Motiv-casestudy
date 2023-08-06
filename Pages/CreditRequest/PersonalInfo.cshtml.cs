using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotivWebApp.Pages.CreditRequest.Services;

namespace MotivWebApp.Pages.CreditRequest
{
    // Represents the page model for collecting personal information
    public class PersonalInfoModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public int AnnualIncome { get; set; }

        [BindProperty]
        public string EmploymentStatus { get; set; }

        private readonly MockDataStore dataStore;

        public PersonalInfoModel()
        {
            // Initialize the data store
            dataStore = new Services.MockDataStore();
        }

        // Handles the HTTP POST request
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var obj_key = SaveDataToStore();

            // Redirects to the result page with the object key as a parameter
            return RedirectToPage("/CreditRequest/Result", new { obj_key=obj_key });

        }

        // Saves the collected data to the data store
        private string SaveDataToStore()
        {
            var obj_key = dataStore.CreateData();
            dataStore.SaveData(obj_key, "Name", Name);
            dataStore.SaveData(obj_key, "Email", Email);
            dataStore.SaveData(obj_key, "Age", Age);
            dataStore.SaveData(obj_key, "AnnualIncome", AnnualIncome);
            dataStore.SaveData(obj_key, "EmploymentStatus", EmploymentStatus);
            return obj_key;
        }
    }
}


