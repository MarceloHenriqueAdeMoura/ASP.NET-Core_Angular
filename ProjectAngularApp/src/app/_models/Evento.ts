import { Lote } from "./Lote";
import { RedeSocial } from "./RedeSocial";
import { Palestrante } from "./Palestrante";

export class Evento {
  
  constructor() {   
  }

  id: number;
  tema: string; 
  local: string;
  dataEvento: Date;
  telefone: string;
  email: string;
  qtdPessoas: number; 
  imagemUrl: string;
  lotes: Lote[];
  redeSociais: RedeSocial[]; 
  palestranteEventos: Palestrante[];
}
