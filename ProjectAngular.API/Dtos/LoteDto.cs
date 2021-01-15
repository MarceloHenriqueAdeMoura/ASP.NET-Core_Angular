using System.ComponentModel.DataAnnotations;

namespace ProjectAngular.API.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage="{0} required!")]
        public string Nome { get; set; }

        [Required(ErrorMessage="{0} required!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [DataType(DataType.Date)]
        public string DataInicio { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [DataType(DataType.Date)]
        public string DataFim { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [Range(4, 10000)]
        public int Quantidade { get; set; }
    }
}