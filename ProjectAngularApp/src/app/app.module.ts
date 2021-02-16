import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule  } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from "ngx-bootstrap/tabs";
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule} from 'ngx-bootstrap/modal';
import { NgxCurrencyModule } from "ngx-currency";
import { NgxMaskModule } from 'ngx-mask';
import { ToastrModule } from 'ngx-toastr';

import { EventoService } from './_services/evento.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { EventoEditComponent } from './eventos/eventoEdit/eventoEdit.component';
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContatosComponent } from './contatos/contatos.component';
import { TituloComponent } from "./_shared/titulo/titulo.component";

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthInterceptor } from './auth/auth.interceptor';



@NgModule({
  declarations: [				
    AppComponent,
    NavMenuComponent,
    EventosComponent,
    EventoEditComponent,
    PalestrantesComponent,
    DashboardComponent,
    ContatosComponent,
    TituloComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    DateTimeFormatPipePipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    NgxCurrencyModule,
    NgxMaskModule.forRoot(),
    ToastrModule.forRoot()
  ],
  providers: [
    EventoService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
