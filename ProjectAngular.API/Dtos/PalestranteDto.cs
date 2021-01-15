using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectAngular.API.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [StringLength(100, MinimumLength=5, ErrorMessage="{0} deve ter entre 5 e 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage="{0} required!")]
        public string MiniCurriculo { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage="{0} required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string ImagemUrl { get; set; }
        public List<RedeSocialDto> RedeSociais { get; set; }
        public List<EventoDto> Eventos { get; set; }
    }
}