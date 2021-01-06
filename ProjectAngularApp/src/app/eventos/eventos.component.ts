import { Component, OnInit, TemplateRef } from '@angular/core';
import { error } from 'protractor';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
/** eventos component*/
export class EventosComponent implements OnInit {
  
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  
  mostrarImagem = false;
  larguraImg = 50;
  margemImg = 2;
  
  _filtroLista: string;
  
  registerForm: FormGroup;
  
  modoSalvar: string;
  bodyDeletarEvento: string;
  
  /** eventos ctor */
  constructor(
    private eventoService: EventoService, 
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private localeService: BsLocaleService
    ) {
      this.localeService.use('pt-br');
    }
    
    get filtroLista(): string {
      return this._filtroLista;
    }
    
    set filtroLista(value: string) {
      this._filtroLista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
    }  
    
    openModal(template: any){
      this.registerForm.reset();
      template.show();
    }
    
    ngOnInit() {
      this.validation();
      this.getEventos();
    }
    
    filtrarEvento(filtrarPor: string): Evento[] {
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.eventos.filter(evento =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
      }
      
      alternarImagem() {
        this.mostrarImagem = !this.mostrarImagem;
      }
      
      validation(){
        this.registerForm = this.formBuilder.group({
          tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
          local: ['', Validators.required],
          dataEvento: ['', Validators.required],
          qtdPessoas: ['', [Validators.required, Validators.max(10000)]],
          imagemUrl: ['', Validators.required],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
        });
      }
      
      novoEvento(template: any){
        this.modoSalvar = 'post';
        this.openModal(template);
      }
      
      getEventos() {
        this.eventoService.getEvento().subscribe(
          (_eventos: Evento[]) => {
            this.eventos = _eventos;
            this.eventosFiltrados = this.eventos;
            console.log(_eventos);
          }, error => {
            console.log(error);
          });
        }
        
        editarEvento(evento: Evento, template: any){          
          this.modoSalvar = 'put';
          this.openModal(template);
          this.evento = evento;
          this.registerForm.patchValue(evento);
        }
        
        salvarEvento(template: any){
          if(this.registerForm.valid){
            if(this.modoSalvar == 'post'){
              this.evento = Object.assign({}, this.registerForm.value);
              this.eventoService.postEvento(this.evento).subscribe(
                (novoEvento: Evento) =>{                  
                  template.hide();
                  this.getEventos();
                }, error =>{
                  console.log(error);
                });                
              }else{
                this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
                this.eventoService.putEvento(this.evento).subscribe(
                  () =>{                  
                    template.hide();
                    this.getEventos();
                  }, error =>{
                    console.log(error);
                  });
                }
              }
            }

            excluirEvento(evento: Evento, template: any) {
              this.openModal(template);
              this.evento = evento;
              this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
            }
            
            confirmeDelete(template: any) {
              this.eventoService.deleteEvento(this.evento.id).subscribe(
                () => {
                    template.hide();
                    this.getEventos();
                  }, error => {
                    console.log(error);
                  }
              );
            }
          }
          
          
          