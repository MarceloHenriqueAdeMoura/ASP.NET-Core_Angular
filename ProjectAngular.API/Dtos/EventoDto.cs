using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectAngular.API.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        public string Tema { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [StringLength(100, MinimumLength=3, ErrorMessage="Campo deve ter entre 3 e 100 caracteres!")]
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [Phone]
        public string Telefone { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage="Campo {0} obrigatório!")]
        [Range(5, 10000, ErrorMessage="Quantidade de pessoas é entre 4 e 10.000")]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedeSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}