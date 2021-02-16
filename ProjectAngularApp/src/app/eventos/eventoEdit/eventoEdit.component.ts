import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';
import { EventoService } from 'src/app/_services/evento.service';

@Component({
  selector: 'app-eventoEdit',
  templateUrl: './eventoEdit.component.html',
  styleUrls: ['./eventoEdit.component.css']
})
export class EventoEditComponent implements OnInit {
  
  titulo = 'Editar Evento';  
  imagemUrl = 'assets/img/upload.png'
  dataEvento: string;
  
  evento = new Evento();
  registerForm: FormGroup;
  file: File;
  fileNameToUpdate: string;
  dataAtual: any;
  
 constructor(
   private eventoService: EventoService, 
   private formBuilder: FormBuilder,
   private localeService: BsLocaleService,
   private toastr: ToastrService,
   private router: ActivatedRoute
   ) {
     this.localeService.use('pt-br');
  }
   
  get lotes(): FormArray{
    return <FormArray>this.registerForm.get('lotes');
  }
  
  get redeSociais(): FormArray{
    return <FormArray>this.registerForm.get('redeSociais');
  }    
  
  ngOnInit() {
    this.validation();     
    this.carregarEvento(); 
  }
  
  validation(){
    this.registerForm = this.formBuilder.group({
      id: [],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(10000)]],
      imagemUrl: [''],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.formBuilder.array([]),
      redeSociais: this.formBuilder.array([])
    });
  }
  
  carregarEvento(){
    const idEvento = +this.router.snapshot.paramMap.get('id');
    this.eventoService.getEventoById(idEvento).subscribe((evento: Evento) =>{
      this.evento = Object.assign({}, evento);
      this.fileNameToUpdate = this.evento.imagemUrl.toString();
      this.imagemUrl = `http://localhost:5000/resources/imagens/${this.evento.imagemUrl}?_ts=${this.dataAtual}`;
      this.evento.imagemUrl = '';
      this.registerForm.patchValue(this.evento);
      
      this.evento.lotes.forEach(lote => {
        this.lotes.push(this.criaLote(lote));
      });
      
      this.evento.redeSociais.forEach(redeSocial => {
        this.redeSociais.push(this.criaRedeSocial(redeSocial));
      });
    })
  }
  
  criaLote(lote: any): FormGroup {
    return this.formBuilder.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim]
    });
  }
  
  criaRedeSocial(redeSocial: any): FormGroup {
    return this.formBuilder.group({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required]
    });
  }
  
  adicionarLote(){
    this.lotes.push(this.criaLote({ id: 0 }));
  }
  
  removerLote(id: number){
    this.lotes.removeAt(id);
  }
  
  adicionarRedeSocial(){
    this.redeSociais.push(this.criaRedeSocial({ id: 0 }));
  }
  
  removerRedeSocial(id: number){
    this.redeSociais.removeAt(id);
  }
  
  onFileChange(file: FileList){
    const reader = new FileReader();
    
    reader.onload = (event: any) => this.imagemUrl = event.target.result;
    
    //this.file = event.target.files;
    reader.readAsDataURL(file[0]);
  }

  uploadImage(){
    if (this.registerForm.get('imagemUrl').value != '') {
      this.eventoService.postUpload(this.file, this.fileNameToUpdate).subscribe(
        () => { 
          this.dataAtual = new Date().getMilliseconds().toString();
          this.imagemUrl = `http://localhost:5000/resources/imagens/${this.evento.imagemUrl}?_ts=${this.dataAtual}`;
        }
      );
    }
  }
  
  salvarEvento(){
    this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
    
    this.evento.imagemUrl = this.fileNameToUpdate;
    this.uploadImage();
    
    this.eventoService.putEvento(this.evento).subscribe(
      () =>{
        this.toastr.success('Editado com sucesso!');
      }, error =>{
        this.toastr.error(`Erro ao editar: ${error}`);
        console.log(error);
      }
    );
  }    
}  