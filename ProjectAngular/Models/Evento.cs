using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAngular.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Tema { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }

        public Evento()
        {
        }
        public Evento(int id, string tema, string local, DateTime dataEvento, int qtdPessoas, string lote)
        {
            Id = id;
            Tema = tema;
            Local = local;
            DataEvento = dataEvento;
            QtdPessoas = qtdPessoas;
            Lote = lote;
        }
    }
}
