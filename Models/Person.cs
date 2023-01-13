using System.Text.RegularExpressions;

namespace tech_test_payment_api.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public bool IsCpfMatch(string cpf)
        {
            Regex CPF = new Regex(@"\d{3}\.?\d{3}\.?\d{3}\-?\d{2}");
            return CPF.IsMatch(cpf);
        }
    }
}