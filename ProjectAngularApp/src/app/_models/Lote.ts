export interface Lote {
  id: number;
  nome: string; 
  preco: string;
  dataInicio?: Date;
  dataFim?: Date;
  quantidade: number;
  eventoId: number; 
}
