using System;
using System.Collections.Generic;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth,  int clientId)
        {
            List<Predicate<string>> checkList = new List<Predicate<string>>
            {
                s => string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName),
                s =>!email.Contains('@') && email.Contains('.'),
                s =>
                {
                    var now = DateTime.Now;
                    var age = now.Year - dateOfBirth.Year;
                    if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
                    return age < 21;
                }
            };
            foreach (Predicate<string> check in checkList)
            {
                if (check.Invoke("")) return false;
            }
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            switch(client.Type)
            {
                case "VeryImportantClient":
                {
                    user.HasCreditLimit = false;
                    break;
                }
               case "ImportantClient":
               {
                    using (var userCreditService = new UserCreditService())
                    {
                        user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth)*2;
                    }
                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    }
                    break;
                }
            }
            UserDataAccess.AddUser(user);
            return !(user.HasCreditLimit && user.CreditLimit < 500);
        }
    }
}
