import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  registerForm: FormGroup;
  user: User;
  
  constructor(private authService: AuthService, public router: Router, public formBuilder: FormBuilder, private toastr: ToastrService) { }
  
  ngOnInit() {
    this.validation();
  }
  
  validation(){
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords: this.formBuilder.group({
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword: ['', Validators.required]
      }, {validator: this.compararSenha})      
    });
  }

  compararSenha(formGroup: FormGroup){
    const confirmSenhaCtrl = formGroup.get('confirmPassword');
    if(confirmSenhaCtrl.errors == null || 'mismatch' in confirmSenhaCtrl.errors){
      if(formGroup.get('password').value != confirmSenhaCtrl.value){
        confirmSenhaCtrl.setErrors({mismatch: true});
      }else{
        confirmSenhaCtrl.setErrors(null);
      }      
    }
  }

  cadastrarUsuario(){
    if(this.registerForm.valid){
      this.user = Object.assign({password: this.registerForm.get('passwords.password').value}, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
        this.router.navigate(['/user/login']);
        this.toastr.success('Cadastrado realizado!');
      },
      error => {
        const erro = error.error;
        erro.forEach(element => {
          switch (element.code) {
            case 'DuplicateUserName':
              this.toastr.error('Usuário já cadastrado!');
              break;          
            default:
              this.toastr.error(`Erro no cadastro! CODE: ${element.code}`);
              break;
          }
        });
      })
    }
  }
  
}
