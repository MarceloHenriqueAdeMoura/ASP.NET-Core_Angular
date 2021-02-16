using System.ComponentModel.DataAnnotations;

namespace ProjectAngular.API.Dtos
{
    public class RedeSocialDto
    {        
        public int Id { get; set; }

        [Required(ErrorMessage="{0} required")]    
        public string Nome { get; set; }

        [Required(ErrorMessage="{0} required")]
        public string Url { get; set; } 
    }
}