using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc247.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]  //requerido  ----obrigatorio
        //o campo tal pode ser entre  e 
        //[StringLength(200, MinimumLength = 3, ErrorMessage = "Name size should be between 3 and 200")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]//tipo de validação dos atributos
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "{0} required")]  //requerido  ----obrigatorio
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]  //requerido  ----obrigatorio
        [Display(Name = "Birth Date")] //Annotations anotações  no label
        [DataType(DataType.Date)] //Annotations anotações
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //Annotations anotações
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "{0} required")]  //requerido  ----obrigatorio
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]  // Ranger === faixa
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        //cria uma coleção e instancia pra inicialiazr uma lista

        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        //adicionar e remover venda deste vendedor
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // alistinha de venda associada ao vendedor
        //vou utilizar o linq e delegar
        // num intervalo de datas
        public double TotalSales(DateTime initial, DateTime final)
        {        //linq ele filtra e delega uma expressao lambda           queremos a soma das vendas
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
