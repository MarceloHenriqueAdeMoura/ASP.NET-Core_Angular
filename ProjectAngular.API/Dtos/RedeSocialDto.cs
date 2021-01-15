using System.ComponentModel.DataAnnotations;

namespace ProjectAngular.API.Dtos
{
    public class RedeSocialDto
    {        
        public int Id { get; set; }

        [Required(ErrorMessage="{0} required")]
        [StringLength(100, MinimumLength=5, ErrorMessage="Campo deve ter entre 5 e 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage="{0} required")]
        public string Url { get; set; } 
    }
}