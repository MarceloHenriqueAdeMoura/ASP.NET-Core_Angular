import { RedeSocial } from "./RedeSocial";
import { Evento } from "./Evento";

export interface Palestrante {
  id: number;
  nome: string;
  miniCurriculo: string;
  telefone: string;
  email: string;
  imagemUrl: string;
  redeSociais: RedeSocial[];
  palestranteEventos: Evento[];
}
