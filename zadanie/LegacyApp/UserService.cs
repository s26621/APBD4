using System;
using System.Collections.Generic;

namespace LegacyApp
{
    public interface IClientRepository
    {
        Client GetById(int idClient);
    }

    public interface ICreditLimitService
    {
        int GetCreditLimit(string lastname, DateTime birthdate);
    }
    
    public class UserService
    {

        private readonly int _minAge = 21;
        private readonly int _minCreditLimit = 500;

        private IClientRepository _clientRepository;
        private ICreditLimitService _creditLimitService;

        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }
        
        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }
        
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
                    return age < _minAge;
                }
            };
            foreach (Predicate<string> check in checkList)
            {
                if (check.Invoke("")) return false;
            }
            
            var client = _clientRepository.GetById(clientId);

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
                    
                   user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth)*2;
                    
                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    break;
                }
            }
            UserDataAccess.AddUser(user);
            return !(user.HasCreditLimit && user.CreditLimit < _minCreditLimit);
        }
    }
}
